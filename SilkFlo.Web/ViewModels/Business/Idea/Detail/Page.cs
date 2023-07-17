namespace SilkFlo.Web.ViewModels.Business.Idea.Detail
{
    public class Page
    {
        public Page(
            Models.Business.Idea idea,
            string tab)
        {
            Idea = idea;
            Tab = tab;
        }

        public Models.Business.Idea Idea { get; }
        public string Tab { get; }
    }
}