namespace WorkflowDiagram.UI.Blazor.Components {
    public partial class MessageBoxComponent {
        static MessageBoxComponent Current;
        public static void Show(string title, string text) {
            if(Current == null)
                return;
            Current.Text = text;
            Current.Title = title;
            Current.ShowPopup();
        }

        public static void Show(string title, string text, Action okAction) {
            if(Current == null)
                return;
            Current.OkAction = okAction;
            Show(title, text);
        }

        private Action OkAction { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
}
