namespace SilkFlo.Web.ViewModels.Business.Idea
{
    public class ProcessOwner
    {
        public Models.Business.Idea Idea { get; set; }

        // Modal: SearchProcessOwner_Modal
        // CoE: SearchProcessOwner_COE
        public string ModalId { get; set; }

        // Modal: Modal.ProcessOwner
        // CoE: Business.Idea.ProcessOwner
        public string TargetFullnameId { get; set; }


        // Modal: Modal.ProcessOwnerId
        // CoE: Business.Idea.ProcessOwnerId
        public string TargetIdId { get; set; }

        // Modal: Business.Idea.ProcessOwnerId
        // CoE: Business.Idea.ProcessOwnerId
        public string TargetIdName { get; set; }


        // Modal: Modal.ProcessOwnerEmail
        // CoE: Business.Idea.ProcessOwnerEmailId
        public string TargetEmailId { get; set; }
    }
}
