// Decompiled with JetBrains decompiler
// Type: SilkFlo.Data.Persistence.TransactionItem
// Assembly: SilkFlo.Data.ZipFileStorage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BC78CDC2-416F-4981-AF6D-F81E52FD30E4
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Data.ZipFileStorage.dll

using SilkFlo.Data.Core.Domain;

namespace SilkFlo.Data.Persistence
{
  internal class TransactionItem
  {
    internal Abstract Entity { get; set; }

    internal Action Action { get; set; }
  }
}
