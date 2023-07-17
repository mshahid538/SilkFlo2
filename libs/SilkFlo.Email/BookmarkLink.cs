using System;

namespace SilkFlo.Email
{
  public class BookmarkLink : BookMark
  {
    public BookmarkLink(string name, string value, string displayName)
      : base(name, value)
    {
      this.DisplayName = displayName;
    }

    public string DisplayName { get; set; }
  }
}
