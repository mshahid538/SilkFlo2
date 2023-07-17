namespace SilkFlo.Web.Controllers2.FileUpload
{
    public class DocumentDto : SilkFlo.Data.Core.Domain.Business.Document
    {
        public string FileNameWithExtetnsion { get; set; }
        public string FileNameWithoutExtetnsion { get; set; }
        public string ThumbNail { get; set; }
        public bool IsSelected { get; set; }
        public string Size { get; set; }
        public string User { get; set; }
    }
}
