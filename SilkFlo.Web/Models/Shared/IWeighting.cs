namespace SilkFlo.Web.Models.Shared
{
    public interface IWeighting
    {
        string Id { get; set; }
        string DisplayText { get; set; }
        decimal Weighting { get; set; }
        string Colour { get; set; }
    }
}

