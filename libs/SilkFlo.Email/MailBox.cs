using System;

namespace SilkFlo.Email
{
  public class MailBox
  {
    public MailBox(string name, string address)
    {
      this.Name = name;
      this.Address = address;
    }

    public string Name { get; }

    public string Address { get; }
  }
}
