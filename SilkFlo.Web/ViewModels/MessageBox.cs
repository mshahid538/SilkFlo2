namespace SilkFlo.Web.ViewModels
{
    public class MessageBox
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public string TrueLabel { get; set; } = "Yes";
        public string FalseLabel { get; set; } = "No";

        /// <summary>
        /// 'return false' is already present
        /// </summary>
        public string No_Javascript { get; set; }

        /// <summary>
        /// 'return false' is already present
        /// </summary>
        public string Yes_Javascript { get; set; }
    }
}
