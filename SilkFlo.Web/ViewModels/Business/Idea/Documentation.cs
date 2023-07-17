
using SilkFlo.Web.Controllers2.FileUpload;
using SilkFlo.Web.Models.Business;
using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea
{
    public class Documentation
    {
        public string Id { get; set; }
        public List<DocumentDTOMain> TDocuments { get; set; } = new();
        public List<DocumentDto> Documents { get; set; } = new();
        public bool AccessAllowed { get; set; }
    }
}
