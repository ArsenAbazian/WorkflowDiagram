using System.Net.Mail;
using WorkflowDiagram.UI.Blazor.ServiceModel;

namespace WorkflowDiagram.UI.Blazor.Pages {
    public partial class CompleteRegistration {
        public CompleteRegistration() { }

        protected override void OnInitialized() {
            base.OnInitialized();
            string uri = NavManager.Uri;
            string guidString = uri.Substring(uri.IndexOf('?') + 1);
            if(!DatabaseManager.CompleteRegistrationFor(guidString)) {
                CompleteRegistrationText = "Your registration is not complete. Something went wrong. Possibly there is no such user.";
            }
            else {
                CompleteRegistrationText = "Congratulations! Your registration is complete!";
            }
        }

        public string CompleteRegistrationText { get; set; }
    }
}