using System;

namespace SilkFlo.Email
{
  public class BookMark
  {
    public BookMark(string name, string value)
    {
      this.Name = name;
      this.Value = value;
    }

    public string Name { get; }

    public string Value { get; }
  }
}
