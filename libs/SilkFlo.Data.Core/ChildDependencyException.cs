using System;

namespace SilkFlo.Data.Core
{
  public class ChildDependencyException : Exception
  {
    public ChildDependencyException()
    {
    }

    public ChildDependencyException(string message)
      : base(message)
    {
    }

    public ChildDependencyException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
