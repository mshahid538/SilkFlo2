// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.InvalidFieldsException
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using System;
using System.Collections.Generic;

namespace SilkFlo.Data.Persistence
{
  public class InvalidFieldsException : Exception
  {
    public InvalidFieldsException(string text)
      : base(text)
    {
      this.Messages = new Dictionary<string, string>();
            //foreach (string str in text.Split("; "))
            //{
            //  string[] strArray = str.Split(": ");
            //  this.Messages.Add(strArray[0], strArray[1]);
            //}
            foreach (string str in text.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] strArray = str.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length >= 2)
                {
                    string key = strArray[0];
                    string value = strArray[1];
                    this.Messages.Add(key, value);
                }
            }

        }

        public new string Message
    {
      get
      {
        string message1 = "";
        foreach (KeyValuePair<string, string> message2 in this.Messages)
          message1 = message1 + message2.Key + ": " + message2.Value + this.Delimiter;
        return message1;
      }
    }

    public Dictionary<string, string> Messages { get; }

    public string Delimiter { get; set; } = "; ";
  }
}
