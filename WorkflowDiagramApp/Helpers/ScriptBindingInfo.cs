using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagramApp.Helpers {
    public class ScriptBindingInfo {
        public ScriptBindingInfo() {
            ParserInfo = new ScriptParserInfo();
            FormatString = null;
            FullFormatString = null;
        }
        public ScriptBindingInfo(string bindingPath) : this() {
            BindingText = bindingPath;
        }

        string bindingText;
        public string BindingText {
            get { return bindingText; }
            set {
                if(BindingText == value)
                    return;
                bindingText = value;
                OnBindingTextChanged();
            }
        }

        string formatString;
        public string FormatString {
            get { return formatString; }
            protected set { formatString = value; }
        }
        protected string FullFormatString { get; set; }

        [Browsable(false)]
        public ScriptTokenInfo[] BindingPath { get { return ParserInfo.PropertyPath; } }
        public object BindingContext { get; set; }
        
        protected virtual void OnBindingTextChanged() {
            if(BindingText == null) {
                ParserInfo = new ScriptParserInfo();
                FormatString = null;
                FullFormatString = null;
                return;
            }

            string path = BindingText;
            if(BindingText.Contains(':')) {
                string[] parts = BindingText.Split(':');
                FormatString = parts[1].Trim();
                if(string.IsNullOrEmpty(FormatString))
                    FullFormatString = "{0}";
                else 
                    FullFormatString = "{0:" + FormatString + "}";
                path = parts[0];
            }

            ParserInfo = ScriptBindingInfoParser.Parse(path);
            if(ParserInfo.Error) {
                BindingError = ParserInfo.Errors[0].Text;
                return;
            }

            //string[] items = path.Split('.');
            //List<string> list = new List<string>();
            //foreach(string str in items) {
            //    string tr = str.Trim();
            //    if(string.IsNullOrEmpty(tr))
            //        continue;
            //    list.Add(tr);
            //}

            //BindingPath = new string[ParserInfo.PropertyPath.Length];
            //Property = new PropertyInfo[ParserInfo.PropertyPath.Length];
            //PropertyOwner = new object[ParserInfo.PropertyPath.Length];
            //for(int i = 0; i < BindingPath.Length; i++) {
            //    BindingPath[i] = ParserInfo.PropertyPath[i].Token;
            //}
        }

        protected ScriptParserInfo ParserInfo { get; private set; }
        public PropertyInfo LastProperty { get { return ParserInfo.PropertyPath[ParserInfo.PropertyPath.Length - 1].PropertyInfo; 
                //Property[BindingPath.Length - 1]; 
            } }
        public object LastOwner { 
            get {
                if(ParserInfo.PropertyPath.Length == 1)
                    return BindingContext;
                return ParserInfo.PropertyPath[ParserInfo.PropertyPath.Length - 1].Owner;
                //if(BindingPath.Length == 1)
                //    return BindingContext;
                //return PropertyOwner[BindingPath.Length - 1]; 
            } 
        }
        object valueCore;
        public object Value {
            get { return valueCore; }
            set {
                if(object.Equals(valueCore, value))
                    return;
                if(BindingError != null)
                    return;
                LastProperty.SetValue(LastOwner, value);
                valueCore = value;
            }
        }

        public object GetValue() {
            object value = BindingContext;
            BindingError = null;
            this.valueCore = null;
            if(ParserInfo == null)
                return null;
            if(AllowCacheObject && LastOwner != null) {
                value = LastProperty.GetValue(LastOwner);
            }
            else {
                for(int i = 0; i < ParserInfo.PropertyPath.Length; i++) {
                    ScriptTokenInfo t = ParserInfo.PropertyPath[i];
                    t.Owner = value;
                    if(t.Type == ScriptTokenType.IndexedValue) {
                        if(value is PropertyStorage) {
                            string name = t.Token;
                            value = ((PropertyStorage)value)[name];
                            t.PropertyInfo = null;
                            if(value == null)
                                return null;
                        }
                    }
                    else {
                        PropertyInfo p = null;
                        value = GetValue(value, t.Token, out p);
                        t.PropertyInfo = p;
                        if(value == null)
                            return null;
                    }
                }
            }

            MethodOwner = value;
            CheckApplyMethod(value);
            
            this.valueCore = value;
            return value;
        }

        protected object MethodOwner { get; set; }

        private void CheckApplyMethod(object value) {
            if(!string.IsNullOrEmpty(Method)) {
                if(MethodInfo == null) {
                    MethodInfo = FindMethod(Method);
                    if(MethodInfo == null) {
                        BindingError = "MethodNotFound " + Method;
                        return;
                    }
                    ParameterInfo[] p = MethodInfo.GetParameters();
                    if(p.Length <= 3)
                        ParameterInfo = p;
                    else
                        ParameterInfo = null;
                }
                if(MethodInfo != null) {
                    if(ParameterInfo != null) {
                        switch(ParameterInfo.Length) {
                            case 0:
                                value = MethodInfo.Invoke(value, new object[] { });
                                break;
                            case 1:
                                value = MethodInfo.Invoke(value, new object[] { Convert.ChangeType(Param1, ParameterInfo[0].ParameterType) });
                                break;
                            case 2:
                                value = MethodInfo.Invoke(value, new object[] { 
                                    Convert.ChangeType(Param1, ParameterInfo[0].ParameterType),
                                    Convert.ChangeType(Param2, ParameterInfo[1].ParameterType)});
                                break;
                            case 3:
                                value = MethodInfo.Invoke(value, new object[] {
                                    Convert.ChangeType(Param1, ParameterInfo[0].ParameterType),
                                    Convert.ChangeType(Param2, ParameterInfo[1].ParameterType),
                                    Convert.ChangeType(Param3, ParameterInfo[2].ParameterType)});
                                break;
                        }
                    }
                    else
                        value = MethodInfo.Invoke(value, new object[] { });
                }
            }
        }

        protected internal MethodInfo MethodInfo { get; set; }
        protected ParameterInfo[] ParameterInfo { get; set; }

        public string GetString() {
            object value = GetValue();
            if(value == null)
                return "[null]";
            if(FullFormatString == null)
                return Convert.ToString(value);
            return string.Format(FullFormatString, value);
        }

        protected static Dictionary<Type, Dictionary<string, PropertyInfo>> PropertiesHash { get; } = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        public string BindingError { get; internal set; }
        public List<ScriptCodeError> Errors { get { return ParserInfo.Errors; } }

        public static Dictionary<string, PropertyInfo> GetPublicProperties(Type type) {
            Dictionary<string, PropertyInfo> props = null;
            if(PropertiesHash.TryGetValue(type, out props))
                return props;
            if(!PropertiesHash.ContainsKey(type))
                PropertiesHash.Add(type, new Dictionary<string, PropertyInfo>());

            Type[] inter = type.GetInterfaces();
            for(int i = 0; i < inter.Length; i++) {
                Type tp = inter[i];
                var propertyInfos = new List<PropertyInfo>();
                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(tp);
                queue.Enqueue(tp);
                while(queue.Count > 0) {
                    var subType = queue.Dequeue();
                    foreach(var subInterface in subType.GetInterfaces()) {
                        if(considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                AddDictionary(PropertiesHash[type], propertyInfos.ToArray());
            }
            AddDictionary(PropertiesHash[type], 
                type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance));
            return PropertiesHash[type];
        }

        protected static void AddDictionary(Dictionary<string, PropertyInfo> dic, PropertyInfo[] propertyInfo) {
            foreach(var p in propertyInfo) {
                if(dic.ContainsKey(p.Name))
                    continue;
                dic.Add(p.Name, p);
            }
        }

        private object GetValue(object dataContext, string propertyName, out PropertyInfo p) {
            p = null;
            if(dataContext == null)
                return null;
            Dictionary<string, PropertyInfo> dic = GetPublicProperties(dataContext.GetType());
            if(dic.TryGetValue(propertyName, out p)) {
                return p.GetValue(dataContext);
            }
            if(dataContext is PropertyStorage)
                return null;
            BindingError = "Not Found: " + dataContext.GetType() + "." + propertyName;
            return null;
        }

        public bool SetValue(string value) {
            if(BindingError != null)
                return false;
            if(LastOwner == null)
                return false;
            if(LastProperty == null)
                return false;
            if(Value == null)
                return false;
            LastProperty.SetValue(LastOwner, Convert.ChangeType(value, Value.GetType()));
            return true;
        }

        public Type GetValueType() {
            return Value == null ? LastProperty?.PropertyType : Value.GetType();
        }

        string param1;
        public string Param1 {
            get { return param1; }
            set {
                if(Param1 == value)
                    return;
                param1 = value;
                OnParameterChanged();
            }
        }

        string param2;
        public string Param2 {
            get { return param2; }
            set {
                if(Param2 == value)
                    return;
                param2 = value;
                OnParameterChanged();
            }
        }

        string param3;
        public string Param3 {
            get { return param3; }
            set {
                if(Param3 == value)
                    return;
                param3 = value;
                OnParameterChanged();
            }
        }

        protected virtual void OnParameterChanged() { }

        protected static Dictionary<string, Dictionary<string, MethodInfo>> MethodsCore { get; } = new Dictionary<string, Dictionary<string, MethodInfo>>();
        public List<MethodInfo> GetAvailableMethods() {
            List<MethodInfo> res = new List<MethodInfo>();
            if(Value == null)
                return res;
            List<Type> types = Value.GetType().GetInterfaces().ToList();
            types.Insert(0, Value.GetType());
            foreach(Type tp in types) {
                Dictionary<string, MethodInfo> list = GetMethods(tp);
                if(list != null)
                    res.AddRange(list.Values);
            }

            return res;
        }

        static Dictionary<string, MethodInfo> GetMethods(Type tp) {
            Dictionary<string, MethodInfo> dic = null;
            if(MethodsCore.TryGetValue(tp.Name, out dic))
                return dic;
            MethodInfo[] methods = tp.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            List<MethodInfo> filtered = methods.Where(m => m.GetParameters().Length <= 3).ToList();
            dic = new Dictionary<string, MethodInfo>();
            MethodsCore.Add(tp.Name, dic);
            filtered.ForEach(m => {
                if(!dic.ContainsKey(m.Name))
                    dic.Add(m.Name, m);
                });
            return dic;
        }

        public MethodInfo FindMethod(string name) {
            if(MethodOwner == null || name == null)
                return null;
            string[] split = name.Split('.');
            Type tp = null;
            if(split.Length == 2)
                tp = MethodOwner.GetType().GetInterface(split[0]);
            else
                tp = MethodOwner.GetType();
            if(tp == null)
                return null;
            Dictionary<string, MethodInfo> dic = GetMethods(tp);
            if(dic == null)
                return null;
            MethodInfo res = null;
            if(dic.TryGetValue(name, out res))
                return res;

            List<Type> types = MethodOwner.GetType().GetInterfaces().ToList();
            foreach(Type tp2 in types) {
                Dictionary<string, MethodInfo> list = GetMethods(tp2);
                list.TryGetValue(name, out res);
                if(res != null)
                    return res;
            }

            return null;
        }

        string method;
        public string Method {
            get { return method; }
            set {
                if(Method == value)
                    return;
                method = value;
                OnMethodChanged();
            }
        }

        public bool AllowCacheObject { get; set; }

        protected virtual void OnMethodChanged() {
            MethodInfo = null;
            ParameterInfo = null;
        }
    }

    public class ScriptBindingManager { 
        public static bool ContainsBindings(string text) {
            if(string.IsNullOrEmpty(text))
                return false;
            return text.Contains('{');
        }
        public ScriptBindingManager() { }

        string bindingText;
        public string BindingText {
            get { return bindingText; }
            set {
                if(BindingText == value)
                    return;
                bindingText = value;
                OnBindingTextChanged();
            }
        }

        object dataContext;
        public object BindingContext {
            get { return dataContext; }
            set {
                if(BindingContext == value)
                    return;
                dataContext = value;
                OnDataContextChanged();
            }
        }

        protected string ResultString { get; set; }

        protected virtual void OnDataContextChanged() {
            ResultString = null;
        }

        protected virtual void OnBindingTextChanged() {
            Parts.Clear();

            int prevIndex = 0;
            int index = BindingText.IndexOf('{', 0);
            if(index == -1)
                return;
            StringBuilder builder = new StringBuilder();
            while(index > -1) {
                string text = BindingText.Substring(prevIndex, index - prevIndex);
                int endIndex = BindingText.IndexOf('}', index);
                if(endIndex == -1)
                    throw new Exception("Closed bracket not found in binding text " + BindingText);
                Parts.Add(text);
                string bindingPath = BindingText.Substring(index + 1, endIndex - index - 1);
                Parts.Add(new ScriptBindingInfo(bindingPath));
                prevIndex = endIndex + 1;
                index = BindingText.IndexOf('{', prevIndex);
                if(index == -1) {
                    text = BindingText.Substring(prevIndex, BindingText.Length - prevIndex);
                    if(!string.IsNullOrEmpty(text))
                        Parts.Add(text);
                }
            }

            ResultString = null;
        }

        public override string ToString() {
            if(ResultString == null)
                ResultString = GetString();
            return ResultString;
        }

        protected List<object> Parts { get; } = new List<object>();
        public string BindingError { get; internal set; }

        public string GetString() {
            StringBuilder b = new StringBuilder();
            foreach(object part in Parts) {
                if(part is string) {
                    b.Append((string)part);
                }
                else { 
                    ScriptBindingInfo info = (ScriptBindingInfo)part;
                    info.BindingContext = BindingContext;
                    b.Append(info.GetString());
                    if(info.BindingError != null)
                        BindingError = info.BindingError;
                }
            }
            return b.ToString();
        }
    }

    public class ScriptParameterInfo {
        public string Value { get; set; }
        public ScriptPropertyType ParameterType { get; set; }
    }

    public class ScriptParserInfo {
        public List<ScriptTokenInfo> Tokens { get; } = new List<ScriptTokenInfo>();
        public ScriptTokenInfo[] PropertyPath { get; set; } = new ScriptTokenInfo[0];
        public string MethodName { get; set; }
        public List<ScriptTokenInfo> Parameters { get; set; } = new List<ScriptTokenInfo>();
        public bool Error => Errors.Count > 0;
        public List<ScriptCodeError> Errors { get; } = new List<ScriptCodeError>();
        public ScriptTokenInfo RightPart { get; set; }
    }

    public class ScriptCodeError {
        public string Error { get; set; }
        public string Text { get; set; }
        public StringLine Line { get; set; }
        public int Position { get { return Line == null ? 0 : Line.Position; } }
        public int Row { get { return Line == null ? 0 : Line.Row; } }
    }

    public class StringLine {
        public string Line { get; set; }
        public int Position { get; set; }
        public int Row { get; set; }
    }

    public enum ScriptTokenType { 
        Identifier,
        IntegerConstant,
        HexadecimalConstant,
        FloatConstant,
        StringConstant,
        CharConstant,
        Separator,
        IndexedValue,
        BooleanConstant
    }

    public class ScriptTokenInfo {
        public string Token { get; set; }
        public ScriptTokenType Type { get; set; }
        public int Position { get; set; }
        public int Length { get; set; }
        public bool IsError { get { return !string.IsNullOrEmpty(ErrorText); } }
        public string ErrorText { get; internal set; }
        public char Separator { get; set; }
        public int EndPosition { get; internal set; }
        public bool IsNumber { get { 
                return Type == ScriptTokenType.FloatConstant || 
                    Type == ScriptTokenType.HexadecimalConstant || 
                    Type == ScriptTokenType.IntegerConstant; } }

        internal bool IsSeparator(char v) {
            return Type == ScriptTokenType.Separator && Separator == v;
        }
        
        public ScriptTokenInfo IndexValue { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public object Owner { get; set; }
        public bool IsValue { get { return Type != ScriptTokenType.Identifier && Type != ScriptTokenType.IndexedValue && Type != ScriptTokenType.Separator; } }
    }

    public static class ScriptBindingInfoParser {
        public static ScriptParserInfo Parse(string text) {
            ScriptParserInfo info = new ScriptParserInfo();
            int index = 0;
            while(index < text.Length) {
                index = SkipWhitespace(text, index);
                ScriptTokenInfo token = ParseToken(info, text, index);
                if(token == null) {
                    break;
                }
                info.Tokens.Add(token);
                index = token.EndPosition;
            }
            OptimizeTokens(info);
            Analyze(info);
            return info;
        }

        public enum ScriptAnalyzerState {
            None,
            MethodParameters,
            Parameter,
            ParameterSeparatorOrCloseBracket,
            End,
            RightPart
        }

        private static void Analyze(ScriptParserInfo info) {
            List<ScriptTokenInfo> t = info.Tokens;
            int i = 0;
            ScriptAnalyzerState state = ScriptAnalyzerState.None;
            List<ScriptTokenInfo> propertyPath = new List<ScriptTokenInfo>();
            List<ScriptTokenInfo> parameters = new List<ScriptTokenInfo>();
            ScriptTokenInfo methodName = null;
            ScriptTokenInfo indexedValue = null;
            ScriptTokenInfo rightPart = null;
            while(i < t.Count) {
                if(state == ScriptAnalyzerState.RightPart) {
                    if(RuleParameterValue(t[i])) {
                        rightPart = t[i];
                        if(i + 1 >= t.Count || t[i + 1].IsSeparator(';'))
                            break;
                        else {
                            info.Errors.Add(new ScriptCodeError() { Error = "Unexpected token", Text = "Expected end of line or ';' but found '" + t[i + 1].Token + "'" });
                            return;
                        }
                    }
                    else {
                        info.Errors.Add(new ScriptCodeError() { Error = "Unexpected token", Text = "Expected constant value, but found '" + t[i].Token + "'" });
                        return;
                    }
                }
                if(state == ScriptAnalyzerState.None) {
                    if(t[i].Type == ScriptTokenType.Identifier) {
                        if(i + 1 >= t.Count) {
                            propertyPath.Add(t[i]);
                            break;
                        }
                        if(t[i + 1].IsSeparator('.')) {
                            propertyPath.Add(t[i]);
                            i += 2;
                            continue;
                        }
                        if(t[i + 1].IsSeparator('[')) {
                            propertyPath.Add(t[i]);
                            i++;
                            continue;
                        }
                        if(t[i + 1].IsSeparator('(')) {
                            methodName = t[i];
                            i++;
                            continue;
                        }
                        if(t[i + 1].IsSeparator('=')) {
                            propertyPath.Add(t[i]);
                            i +=2;
                            state = ScriptAnalyzerState.RightPart;
                            continue;
                        }
                        info.Errors.Add(new ScriptCodeError() { Error = "Unexpected token", Text = "Expected end of line, =, '.' or open bracket, but found '" + t[i + 1].Token + "'" });
                        break;
                    }
                    else if(RuleIndexedValue(t, i, out indexedValue)) {
                        indexedValue.Type = ScriptTokenType.IndexedValue;
                        propertyPath.Add(indexedValue);
                        i += 3;
                        if(i >= t.Count)
                            break;
                        if(t[i].IsSeparator('.'))
                            i++;
                        else if(t[i].IsSeparator('=')) {
                            i++;
                            state = ScriptAnalyzerState.RightPart;
                        }
                        continue;
                    }
                    else if(t[i].IsSeparator('(')) {
                        if(methodName == null) {
                            info.Errors.Add(new ScriptCodeError() { Error = "Expected method name before '(' but found nothing." });
                            return;
                        }
                        i++;
                        state = ScriptAnalyzerState.MethodParameters;
                        continue;
                    }
                    else {
                        info.Errors.Add(new ScriptCodeError() { Error = "Unexpected token", Text = "Expected property or method name but found " + t[i].Token });
                        break;
                    }

                }
                else if(state == ScriptAnalyzerState.MethodParameters) {
                    RuleParameters(info, t, i, parameters);
                    break;
                }
            }
            info.PropertyPath = propertyPath.ToArray(); //.AddRange(propertyPath);
            info.MethodName = methodName?.Token;
            info.Parameters.AddRange(parameters);
            info.RightPart = rightPart;
        }

        private static bool RuleParameters(ScriptParserInfo info, List<ScriptTokenInfo> t, int start, List<ScriptTokenInfo> parameters) {
            int i = start;
            ScriptAnalyzerState state = ScriptAnalyzerState.Parameter;
            while(i < t.Count) {
                if(state == ScriptAnalyzerState.Parameter) {
                    if(RuleParameterValue(t[i])) {
                        parameters.Add(t[i]);
                        i++;
                        state = ScriptAnalyzerState.ParameterSeparatorOrCloseBracket;
                        continue;
                    }
                    else if(t[i].IsSeparator(')')) {
                        i++;
                        state = ScriptAnalyzerState.End;
                        return true;
                    }
                    else {
                        info.Errors.Add(new ScriptCodeError() { Error = "Unexpected token.", Text = "Expected , or ) but found " + t[i].Token });
                        return false;
                    }
                }
                else if(state == ScriptAnalyzerState.ParameterSeparatorOrCloseBracket) {
                    if(t[i].IsSeparator(',')) {
                        i++;
                        state = ScriptAnalyzerState.Parameter;
                        continue;
                    }
                    else if(t[i].IsSeparator(')')) {
                        i++;
                        state = ScriptAnalyzerState.End;
                        return true;
                    }
                }
            }
            info.Errors.Add(new ScriptCodeError() { Error = "Unexpected end of line.", Text = "Expected ) but found EOL" });
            return false;
        }

        private static bool RuleParameterValue(ScriptTokenInfo info) {
            return info.Type == ScriptTokenType.FloatConstant ||
                info.Type == ScriptTokenType.HexadecimalConstant ||
                info.Type == ScriptTokenType.IntegerConstant ||
                info.Type == ScriptTokenType.StringConstant ||
                info.Type == ScriptTokenType.CharConstant || 
                info.Type == ScriptTokenType.BooleanConstant;
        }

        private static bool RuleIndexedValue(List<ScriptTokenInfo> tokens, int startIndex, out ScriptTokenInfo result) {
            result = null;
            if(startIndex + 3 > tokens.Count)
                return false;
            if(tokens[startIndex].IsSeparator('[') && RuleParameterValue(tokens[startIndex+1]) && tokens[startIndex + 2].IsSeparator(']')){
                result = tokens[startIndex + 1];
                return true;
            }
            return false;
        }

        private static void OptimizeTokens(ScriptParserInfo info) {
            for(int i = 1; i < info.Tokens.Count;) {
                if(i == 0) {
                    i++;
                    continue;
                }
                if(info.Tokens[i].IsNumber && info.Tokens[i - 1].IsSeparator('-')) {
                    int end = info.Tokens[i].EndPosition;
                    info.Tokens[i].Position = info.Tokens[i - 1].Position;
                    info.Tokens[i].Length = end - info.Tokens[i].Position;
                    info.Tokens[i].Token = "-" + info.Tokens[i].Token;
                    info.Tokens.RemoveAt(i - 1);
                    i--;
                    continue;
                }
                else if(info.Tokens[i].Type == ScriptTokenType.IntegerConstant && info.Tokens[i - 1].IsSeparator('.')) {
                    int end = info.Tokens[i].EndPosition;
                    info.Tokens[i].Position = info.Tokens[i - 1].Position;
                    info.Tokens[i].Length = end - info.Tokens[i].Position;
                    info.Tokens[i].Token = "." + info.Tokens[i].Token;
                    info.Tokens[i].Type = ScriptTokenType.FloatConstant;
                    info.Tokens.RemoveAt(i - 1);
                    i--;
                    continue;
                }
                else i++;
            }
        }

        static int[] symbolClass = new int[]
                {
//					0 1 2 3 4 5 6 7 8 9 a b c d e f
					0,0,0,0,0,0,0,0,0,3,3,0,0,3,0,0, //0
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //1
					3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3, //2
					1,1,1,1,1,1,1,1,1,1,3,3,3,3,3,3, //3
					3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2, //4
					2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,2, //5
					3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2, //6
					2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,0, //7
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //8
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //9
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //a
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //b
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //c
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //d
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //e
					0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0  //f
				};

        private static bool IsSeparator(char c) {
            return c < 256 && symbolClass[(int)c] == 3;
        }
        private static ScriptTokenInfo ParseToken(ScriptParserInfo info, string text, int start) {
            if(start >= text.Length)
                return null;
            int index = start;
            char c = text[index];
            if(c == '\"')
                return ParseString(info, text, index, '\"');
            if (c == '\'')
                return ParseCharConstant(info, text, index);
            if(char.IsDigit(c))
                return ParseNumberConstant(info, text, index);
            if(char.IsLetter(c) || c == '_')
                return ParseIdentifier(info, text, index);
            if (c == '.' && IsNextDigit(text, index + 1))
                return ParseNumberConstant(info, text, index);
            if (IsSeparator(c))
                return ParseSeparator(text, index);
            return null;
        }

        private static bool IsNextDigit(string text, int index)
        {
            while (index < text.Length) {
                if (char.IsWhiteSpace(text[index]))
                    continue;
                return char.IsDigit(text[index]);
            }
            return false;
        }

        private static ScriptTokenInfo ParseIdentifier(ScriptParserInfo res, string text, int index) {
            ScriptTokenInfo info = new ScriptTokenInfo();
            info.Position = index;
            StringBuilder b = new StringBuilder();
            bool firstTime = true;
            while(index < text.Length) {
                char c = text[index];
                if(char.IsLetter(c) || (!firstTime && char.IsDigit(c)) || c == '_')
                    b.Append(c);
                else 
                    break;
                firstTime = false;
                index++;
            }
            info.Token = b.ToString();
            info.Length = index - info.Position;
            info.EndPosition = index;
            info.Type = ScriptTokenType.Identifier;
            if(info.Token == "false" || info.Token == "true")
                info.Type = ScriptTokenType.BooleanConstant;
            return info;
        }

        private static ScriptTokenInfo ParseSeparator(string text, int index) {
            ScriptTokenInfo info = new ScriptTokenInfo();
            info.Position = index;
            info.Token = text[index].ToString();
            info.Type = ScriptTokenType.Separator;
            info.Length = 1;
            info.EndPosition = index + 1;
            info.Separator = text[index];
            return info;
        }

        private static ScriptTokenInfo ParseCharConstant(ScriptParserInfo res, string text, int index) {
            ScriptTokenInfo info = ParseString(res, text, index, '\'');
            info.Type = ScriptTokenType.CharConstant;
            return info;
        }

        private static bool StartsWith(string text, int start, string textToFind) {
            if(start + textToFind.Length > text.Length)
                return false;
            return text.Substring(start).StartsWith(textToFind);
        }

        private static ScriptTokenInfo ParseNumberConstant(ScriptParserInfo res, string text, int start) {
            ScriptTokenInfo info = null;
            if(StartsWith(text, start, "0x"))
                info = ParseHexadecimalConstant(res, text, start);
            else 
                info = ParseSimpleNumberConstant(res, text, start);
            return info;
        }

        private static ScriptTokenInfo ParseSimpleNumberConstant(ScriptParserInfo res, string text, int start) {
            ScriptTokenInfo info = new ScriptTokenInfo();
            info.Position = start;
            info.Type = ScriptTokenType.IntegerConstant;
            StringBuilder b = new StringBuilder();
            int index = start;

            bool dotFound = false;

            while(index < text.Length) {
                if(text[index] == '.') {
                    if(dotFound)
                        break;
                    else {
                        info.Type = ScriptTokenType.FloatConstant;
                        dotFound = true;
                    }
                }
                else if(!char.IsDigit(text[index])) {
                    info.Type = dotFound ? ScriptTokenType.FloatConstant : ScriptTokenType.IntegerConstant;
                    break;
                }
                b.Append(text[index]);
                index++;
            }
            info.Token = b.ToString();
            info.Length = index - start;
            info.EndPosition = index;
            return info;
        }

        private static ScriptTokenInfo ParseHexadecimalConstant(ScriptParserInfo res, string text, int start) {
            ScriptTokenInfo info = new ScriptTokenInfo();
            info.Position = start;
            info.Type = ScriptTokenType.HexadecimalConstant;
            StringBuilder b = new StringBuilder();
            int index = start;
            char[] allowedLetters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f' };
            if(text[index] != '0') {
                res.Errors.Add(new ScriptCodeError() { Error = "Unexpected symbol", Text = "Expected 0 for hexadecimal value, but got '" + text[index] + "'."});
                info.EndPosition = index;
                return info;
            }
            index++;
            if(index >= text.Length) {
                res.Errors.Add(new ScriptCodeError() { Error = "Unexpected end of line", Text = "Expected x for hexadecimal value, but got end of line" });
                info.EndPosition = index;
                return info;
            }
            index++;
            if(index >= text.Length) {
                res.Errors.Add(new ScriptCodeError() { Error = "Unexpected end of line", Text = "Expected at least one digit or letter, but got end of line" });
                info.EndPosition = index;
                return info;
            }
            b.Append("0x");
            if(!IsDigitOrLetters(text[index], allowedLetters)) {
                res.Errors.Add(new ScriptCodeError() { Error = "Unexpected symbol", Text = "Expected at least one digit or letter, but got '" + text[index] + "'" });
                info.EndPosition = index;
                return info;
            }
            while(index < text.Length) {
                if(IsDigitOrLetters(text[index], allowedLetters))
                    b.Append(text[index]);
                else
                    break;
            }
            info.Token = b.ToString();
            info.Length = info.Token.Length;
            info.EndPosition = index;
            return info;
        }

        private static bool IsDigitOrLetters(char ch, char[] allowedLetters) {
            if(char.IsDigit(ch))
                return true;
            return allowedLetters.Contains(ch);
        }

        private static ScriptTokenInfo ParseString(ScriptParserInfo res, string text, int start, char quoteChar) {
            ScriptTokenInfo info = new ScriptTokenInfo();
            info.Position = start;
            info.Type = ScriptTokenType.StringConstant;
            StringBuilder b = new StringBuilder();
            int index = start + 1;
            while(true) {
                if(index >= text.Length) {
                    info.Token = b.ToString();
                    res.Errors.Add(new ScriptCodeError() { Error = "Unexpected end of line", Text = "Expected close " + quoteChar + ", but found end of line" });
                    break;
                }
                if(text[index] == quoteChar) {
                    info.Token = b.ToString();
                    info.Length = index - start + 1;
                    info.Type = ScriptTokenType.StringConstant;
                    index++;
                    break;
                }
                if(text[index] == '\\') {
                    b.Append(text[index]);
                    index++; //Skip escape symbol
                }
                b.Append(text[index]);
                index++;
            }
            info.EndPosition = index;
            return info;
        }

        private static int SkipWhitespace(string text, int index) {
            while(index < text.Length && char.IsWhiteSpace(text[index])) index++;
            return index;
        }
    }

    //public static class FastValueConverter {
    //    static double[] divider = new double[] {
    //        1,
    //        0.1,
    //        0.01,
    //        0.001,
    //        0.0001,
    //        0.00001,
    //        0.000001,
    //        0.0000001,
    //        0.00000001,
    //        0.000000001,
    //        0.0000000001,
    //        0.00000000001,
    //        0.000000000001,
    //        0.0000000000001,
    //        0.00000000000001,
    //        0.000000000000001,
    //        0.0000000000000001,
    //        0.00000000000000001,
    //        0.000000000000000001,
    //        0.0000000000000000001,
    //        0.00000000000000000001,
    //        0.000000000000000000001,
    //        0.0000000000000000000001
    //    };
    //    static double[] multiplier = new double[] {
    //        1,
    //        10,
    //        100,
    //        1000,
    //        10000,
    //        100000,
    //        1000000,
    //        10000000,
    //        100000000,
    //        1000000000
    //    };
    //    static int GetExponent(string str, int startIndex) {
    //        int exponent = 0;
    //        if(str[startIndex] == '-') {
    //            startIndex++;
    //            for(int i = startIndex; i < str.Length; i++)
    //                exponent = (exponent << 3) + (exponent << 1) + str[i] - 0x30;
    //            return -exponent;
    //        }
    //        for(int i = startIndex; i < str.Length; i++)
    //            exponent = (exponent << 3) + (exponent << 1) + str[i] - 0x30;
    //        return exponent;
    //    }

    //    static int GetExponent(char[] str, int startIndex, int endIndex) {
    //        int exponent = 0;
    //        if(str[startIndex] == '-') {
    //            startIndex++;
    //            for(int i = startIndex; i < endIndex; i++)
    //                exponent = (exponent << 3) + (exponent << 1) + str[i] - 0x30;
    //            return -exponent;
    //        }
    //        for(int i = startIndex; i < str.Length; i++)
    //            exponent = (exponent << 3) + (exponent << 1) + str[i] - 0x30;
    //        return exponent;
    //    }

    //    static double ParseExponent(string str, double value, int startIndex) {
    //        int exponent = GetExponent(str, startIndex);
    //        if(exponent < 0)
    //            return value * divider[-exponent];
    //        return value * multiplier[exponent];
    //    }

    //    static double ParseExponent(char[] str, double value, int startIndex, int end) {
    //        int exponent = GetExponent(str, startIndex, end);
    //        if(exponent < 0)
    //            return value * divider[-exponent];
    //        return value * multiplier[exponent];
    //    }

    //    public static int ConvertPositiveInteger(string str) {
    //        int start = 0;
    //        return ConvertPositiveInteger(str, ref start);
    //    }
    //    public static int ConvertPositiveInteger(string str, ref int startIndex) {
    //        int value = 0;
    //        try {
    //            int length = str.Length;
    //            for(int i = startIndex; i < length; i++) {
    //                char c = str[i];
    //                if(c == ',') {
    //                    startIndex = i;
    //                    break;
    //                }
    //                value = (value << 3) + (value << 1) + str[i] - 0x30;
    //            }
    //        }
    //        catch(Exception e) {
    //            Telemetry.Default.TrackEvent("convert int exception", new string[] { "value", str }, true);
    //            Telemetry.Default.TrackException(e, new string[,] { { "value", str } });
    //        }
    //        return value;
    //    }

    //    public static long ConvertPositiveLong(string str) {
    //        int stIndex = 0;
    //        return ConvertPositiveLong(str, ref stIndex);
    //    }
    //    public static long ConvertPositiveLong(string str, ref int startIndex) {
    //        long value = 0;
    //        try {
    //            int length = str.Length;
    //            for(int i = startIndex; i < length; i++) {
    //                char c = str[i];
    //                if(c == ',') {
    //                    startIndex = i;
    //                    break;
    //                }
    //                value = (value << 3) + (value << 1) + str[i] - 0x30;
    //            }
    //        }
    //        catch(Exception e) {
    //            Telemetry.Default.TrackEvent("convert int exception", new string[] { "value", str }, true);
    //            Telemetry.Default.TrackException(e, new string[,] { { "value", str } });
    //        }
    //        return value;
    //    }

    //    public static long ConvertPositiveLong(byte[] str, ref int startIndex) {
    //        long value = 0;
    //        try {
    //            int length = str.Length;
    //            for(int i = startIndex; i < length; i++) {
    //                byte c = str[i];
    //                if(c == ',') {
    //                    startIndex = i;
    //                    break;
    //                }
    //                value = (value << 3) + (value << 1) + str[i] - 0x30;
    //            }
    //        }
    //        catch(Exception e) {
    //            Telemetry.Default.TrackException(e);
    //        }
    //        return value;
    //    }

    //    public static double Convert(string str) {
    //        try {
    //            if(string.IsNullOrEmpty(str))
    //                return 0.0;
    //            long value = 0;
    //            long fix = 0;
    //            int length = str.Length;
    //            int startIndex = 0;
    //            int sign = 1;
    //            if(str[0] == '-') {
    //                startIndex = 1;
    //                sign = -1;
    //            }
    //            for(int i = startIndex; i < length; i++) {
    //                char c = str[i];
    //                if(c == '.' || c == ',') {
    //                    for(int j = i + 1; j < length; j++) {
    //                        if(str[j] == '-' || str[j] == 'e' || str[j] == 'E')
    //                            return ParseExponent(str, sign * value + fix * divider[j - i - 1], j + 1);
    //                        fix = (fix << 3) + (fix << 1) + str[j] - 0x30;
    //                    }
    //                    return sign * value + fix * divider[length - i - 1];
    //                }
    //                if(str[i] == '-' || str[i] == 'e' || str[i] == 'E')
    //                    return sign * ParseExponent(str, value, i + 1);
    //                value = (value << 3) + (value << 1) + str[i] - 0x30;
    //            }
    //            if(value < 0) {
    //                Telemetry.Default.TrackEvent("convert double exception", new string[] { "value", str, "converted", value.ToString() }, true);
    //            }
    //            return sign * value;
    //        }
    //        catch(Exception e) {
    //            Telemetry.Default.TrackEvent("convert double exception", new string[] { "value", str }, true);
    //            Telemetry.Default.TrackException(e, new string[,] { { "value", str } });
    //            return System.Convert.ToDouble(str);
    //        }
    //    }
    //    public static double Convert(char[] str, int start, int end) {
    //        try {
    //            int value = 0;
    //            int fix = 0;
    //            int length = end;
    //            int startIndex = start;
    //            int sign = 1;
    //            if(str[start] == '-') {
    //                startIndex = start + 1;
    //                sign = -1;
    //            }
    //            for(int i = startIndex; i < length; i++) {
    //                char c = str[i];
    //                if(c == '.' || c == ',') {
    //                    for(int j = i + 1; j < length; j++) {
    //                        if(str[j] == '-' || str[j] == 'e' || str[j] == 'E')
    //                            return ParseExponent(str, sign * value + fix * divider[j - i - 1], j + 1, end);
    //                        fix = (fix << 3) + (fix << 1) + str[j] - 0x30;
    //                    }
    //                    return sign * (value + fix * divider[length - i - 1]);
    //                }
    //                if(str[i] == '-' || str[i] == 'e' || str[i] == 'E')
    //                    return sign * ParseExponent(str, value, i + 1, end);
    //                value = (value << 3) + (value << 1) + str[i] - 0x30;
    //            }
    //            if(value < 0) {
    //                Telemetry.Default.TrackEvent("convert double exception", new string[] { "value", str.ToString(), "converted", value.ToString() }, true);
    //            }
    //            return sign * value;
    //        }
    //        catch(Exception e) {
    //            Telemetry.Default.TrackEvent("convert double exception", new string[] { "value", str.ToString() }, true);
    //            Telemetry.Default.TrackException(e, new string[,] { { "value", str.ToString() } });
    //            return System.Convert.ToDouble(str);
    //        }
    //    }

    //    public static bool IsDigit(byte v) {
    //        return v >= '0' && v <= '9';
    //    }
    //}

}
