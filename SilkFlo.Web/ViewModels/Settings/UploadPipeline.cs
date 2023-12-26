
namespace SilkFlo.Web.ViewModels.Settings
{
    public class UploadPipeline
    {
        public UploadPipeline(string tab, bool isPractice)
        {
            Tab = tab;
            IsPractice = isPractice;
        }

        public string Tab { get; }

        public bool IsPractice { get; }
    }
}
