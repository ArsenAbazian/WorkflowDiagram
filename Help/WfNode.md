
## WfNode class

The WfNode class is a kind of black box that can accept input values, process them and return the result, as well as pass the execution to the next nodes along the selected branches.
 
![WfNode scheme](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/Node.png)
The typical node.

Every node has input and output connection points. You can access them via the Inputs and Outputs properties, using connection point's name. Via connectors node receives data, make operations based on this data and then produce result values and set them to the output connection points. Then this values 'flows' via connectors to the next nodes and so on... Think of node as function, input connection points as function's parameters, and output connection points as return value.

The WfNode class is the base class for all the nodes you create. This base class has only one input connection point, called "Run". Even there is no other input connections you can connect output from previous node to the "Run" input, and pass execution to it. You can also consider "Run" input connection as Enabled property.

##Basic Flow
Basic node should define inputs and outputs. When it is visit by WfRunner the node using data from input connections should perform it's operation and pass operation result to correspoding outputs. Some outputs can be skipped. 

Lets see how basic node works on the example of WfEqualityNode. WfEqualityNode has 2 input connections In1 and In2 and 2 output connections True and False. The WfEqualityNode class gets 2 input values and pass them to the object.Equals method. If object.Equals return true, the WfEqualityNode should pass execution to the True output connection and skip False output connection. Otherwice it should skip True output connecton and should pass execution to the False output connection. 
Here is the simplified code: 

```csharp
public class WfEqualityNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Equality";
        public override string Type => "Equality";
        public override string Header => "==";

        public WfEqualityNode() { }
        
        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In1", Text = "In1", Requirement = WfRequirementType.Mandatory },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In2", Text = "In2", Requirement = WfRequirementType.Mandatory }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "True", Text = "True"  },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "False", Text = "False"  }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            object value1 = Inputs["In1"].Value;
            object value2 = Inputs["In2"].Value;

            bool result = object.Equals(value1, value2);
            
            DataContext = result;
            if(result) {
                Outputs["True"].OnVisit(runner, true);
                Outputs["False"].OnSkipVisit(runner, null);
            }
            else {
                Outputs["False"].OnVisit(runner, true);
                Outputs["True"].OnSkipVisit(runner, null);
            }
        }
    }
```

Let's quickly go through the methods and properties of the node.

The type property returns "Equality". The node will be displayed on the Visual Designer toolbox with this name.
The Header property returns "==". This will be displayed in the node's header in Visual Editor's diagram area.
The GetDefaultInputs method return two input connection points: "In1" and "In2". This connections are mandatory, which means that there should be at least one connection from other nodes to each of input points of the WfEqualityNode.
The GetDefaultOutputs method returns two output connection points: "True" and "False". This connections are optional by default, i.e. node can have zero output connections. 
The OnInitializeCore method returns true, because there is no additional initialization sould be made for this node.
Now let's look at the OnVisitCore method. If WfRunner calls node's OnVisitCore, this means that it already visited all the nodes and connection points which current node depends on. Now we get values from input connections and check if they are equal: 

```csharp
    object value1 = Inputs["In1"].Value;
    object value2 = Inputs["In2"].Value;

    bool result = object.Equals(value1, value2);
```
After that we will save result to node's DataContext. 

```csharp
    DataContext = result;
```
We should do this always, because we can then reflect this value visually in diagram area.

```csharp
    if(result) {
        Outputs["True"].OnVisit(runner, true);
        Outputs["False"].OnSkipVisit(runner, null);
    }
    else {
        Outputs["False"].OnVisit(runner, true);
        Outputs["True"].OnSkipVisit(runner, null);
    }
```
Finally we depend on result value pass execution to one of output connections.
As we can see, this node acts as a switch and selects one of two branches to execute depending on the input values.

So you can create various nodes that will perform the operations you need.

## WfNode Members

### Methods
```charp
void Assign(WfNode node)
```
Assign all the properties from specified node

```charp
CheckDependency(int visitIndex) 
```
Determines whether all the output connection points from the all previous connected nodes were visited (or skipped) by Runner. Node will not be executed until all the nodes on which it depend are visited.

```charp
object Clone()
```
Create a copy of the node

```csharp
void Connect(string outputName, WfNode node, string inputName)
```
Connect output point with the name specified in outputName parameter to the node's input point, specified in inputName parameter.

```csharp
string ConstrainStringValue(string value)
```
Trim string value and if it null, replace with string.Empty

```csharp
virtual WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type)
```
Creates WfConnectionPointObject with point type (In,Out) specified in type parameter. This method can be overrided in WfNode descendant to create specific connection point.

```csharp
virtual object CreateImage()
```
Create an object which can be threated as image (in Win or Web), and used to identify WfNode in toolbar list. 

```csharp
virtual WfConnectionPointCollection CreateInputCollection()
```
Creates collection object which holds node's input connection points. This method can be overrided in descendant classes, to return specific collection

```csharp
virtual WfConnectionPointCollection CreateOutputCollection()
```
Creates collection object which holds node's output connection points. This method can be overrided in descendant classes, to return specific collection

```csharp
abstract List<WfConnectionPoint> GetDefaultInputs()
```
Create input connection points for node. This method is abstract, and should be overriden in descendant classes.

```csharp
abstract List<WfConnectionPoint> GetDefaultOutputs()
```
Create output connection points for node. This method is abstract, and should be overriden in descendant classes.

```csharp
List<WfConnector> GetInputConnectors()
```
Get all input connections for node.

```csharp
List<WfNode> GetNextNodes()
```
Get all output nodes connected to the node.

```csharp
List<WfNode> GetNodesFromVisitedPoints(int visitIndex)
```
For specific nodes, depend on input values and other parameters some output connections should be processed and some should be skipped. This method returns all next nodes connected to output points, that should be processed.

```csharp
List<WfConnector> GetOutputConnectors()
```
Get all output connections for node.

```csharp
bool IsVisited(int visitIndex)
```
Determines if node was visited by WfRunner. Each time WfRunner visit node, it marks node by setting the WfNode.VisitIndex property. The value specifed in the visitIndex parameter holds this mark.

```csharp
virtual void OnEndDeserialize()
```
This method called after WfNode deserialized from xml file. This is good place to made additional initializations for your custom node.

```csharp
abstract bool OnInitializeCore(WfRunner runner)
```
This method is called when WfRunner initialize all nodes before executing document and start visiting nodes. You can do additional initializations for your custom node.

```csharp
virtual void OnPointAdded(WfConnectionPoint point)
```
This method called when WfConnectionPoint added to the Inputs/Outputs point collections. 

```csharp
virtual void OnPointRemoved(WfConnectionPoint point)
```
This method called when WfConnectionPoint removed from the Inputs/Outputs point collections. 

```csharp
protected internal void OnPropertyChanged(string name)
```
Call this method when some custom property of your custom node is changed.

```csharp
virtual void OnRemoved()
```
This method is called when node removed from WfDocument.Nodes collection.

```csharp
void OnVisit(WfRunner runner)
```
This method is called when WfRunner visit node. Call this method if you implement your own WfRunner descendant and overrides RunOnce or Run methods. This method is not for override.

```csharp
abstract void OnVisitCore(WfRunner runner)
```
Override this method in your custom node. This method is called when WfRunner visits node. In it can execute it's custom operations.

```csharp
void Reset()
```
This method is called by the WfRunner before initialize nodes. 

```csharp
virtual void ResetCore()
```
Override this method to make additional reset logic for your custom node.

### Properties
```csharp
virtual string Category
```
This property returs category name which node is belongs to. All nodes will be grouped in nodes toolbar by Category.

```csharp
object DataContext
```
Additional data that associated with this node.

```csharp
string Description
```
Nodes description.

```csharp
List<WfDiagnosticInfo> Diagnostic
```
Collection of diagnostic messages (errors, warnings, information) which can happen during initialization and execution.

```csharp
string DisplayText
```

```csharp
string Header
```
Text displayed in the node's header area.

```csharp
WfDocument Document
```
This property contains document which node belongs to.

```csharp
bool Enabled
```
Returns value showing wether node is Enabled.

```csharp
bool HasErrors
```
Return true if Node contains errors in the Diagnostic property.

```csharp
bool HasInputConnections
```

```csharp
bool HasOutputConnections
```

```csharp
float Height
```
Height of the node

```csharp
float Width
```
Width of the node

```csharp
float X
```
X coordinate of the node

```csharp
float Y
```
Y coordinate of the node

```csharp
Guid Id
```
Nodes unique identifier. Initialized in the constructor.

```csharp
object Image
```
Object that represents and image for the node. Used in the node's toolbox

```csharp
WfConnectionPointCollection Inputs
```
Input connection points

```csharp
bool IsInitialized
```
Determines if node is initialized by WfRunner

```csharp
bool IsStartNode
```
Determines if node is start node, i.e. has no dependencies from other node. Such nodes executed first by the WfRunner

```csharp
string Name
```
Node's name.

```csharp
int OrderIndex
```
Node's order index. If multiple nodes can be executed at some time, you can manipulate execution order by settings this property.

```csharp
WfConnectionPointCollection Outputs
```
Output connection points.

```csharp
BindingList<WfConnectionPoint> Points
```
Returns all the input and output connection points.

```csharp
string Text
```

```csharp
string Type
```
Node's type. Consider it as a class name or type name.

```csharp
int VisitIndex
```
Node's visitIndex. When WfRunner visit node, it marks it by settings this value. Next time WfRunner visit node with VisitIndex higher that previous. 

```csharp
string VisualTemplateName
```
Name representing visual template for your custom node.



