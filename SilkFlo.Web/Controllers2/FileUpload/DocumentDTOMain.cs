using System.Collections.Generic;

namespace SilkFlo.Web.Controllers2.FileUpload
{
    public class DocumentDTOMain
    {
        public string DocumentTemplateName { get; set; }
        public List<DocumentDto> DocumentDtos { get; set; } = new();
    }
}
