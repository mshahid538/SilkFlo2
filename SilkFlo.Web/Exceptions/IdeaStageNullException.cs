using System;

namespace SilkFlo.Web.Exceptions
{
    public class IdeaStageNullException : Exception
    {
        public IdeaStageNullException() : base("Could not find IdeaStage in the database.") { }
    }
}