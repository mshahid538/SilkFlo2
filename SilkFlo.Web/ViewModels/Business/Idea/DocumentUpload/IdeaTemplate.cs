using System.Collections.Generic;

namespace SilkFlo.Web.ViewModels.Business.Idea.DocumentUpload
{
    public class IdeaTemplate
    {
        public string SeletcedTemplateName { get; set; }
        public ISet<string> TemplateNames { get; set; } = new HashSet<string>();
    }
}
