namespace SilkFlo.Web.ViewModels
{
    public class FieldError
    {
        public FieldError(
            string field,
            string fieldInvalid,
            string message)
        {
            Field = field;
            FieldInvalid = fieldInvalid;
            Message = message;
        }

        public string Field { get; }
        public string FieldInvalid { get; }
        public string Message { get; }
    }
}
