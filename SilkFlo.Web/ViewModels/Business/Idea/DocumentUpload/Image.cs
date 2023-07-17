namespace SilkFlo.Web.ViewModels.Business.Idea.DocumentUpload
{
    public class Image
    {
        public string Id { get; set; }
        public string FilenameBackend { set; get; }
        public string Filename { set; get; }
        public bool IsNew
        {
            get
            {
                return string.IsNullOrWhiteSpace(Id);
            }
        }
    }
}
