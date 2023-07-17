// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.UniqueConstraintException
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using System;

namespace SilkFlo.Data.Persistence
{
  public class UniqueConstraintException : Exception
  {
    public UniqueConstraintException(string message)
      : base(message)
    {
    }
  }
}
