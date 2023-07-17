// Decompiled with JetBrains decompiler
// Type: SilkFlo.Email.Properties.Resources
// Assembly: SilkFlo.Email, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D77EE497-8F15-4945-A4E8-D764A145CA78
// Assembly location: C:\workspace\SilkFlo\Library\SilkFlo.Email.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SilkFlo.Email.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  public class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
      get
      {
        if (SilkFlo.Email.Properties.Resources.resourceMan == null)
          SilkFlo.Email.Properties.Resources.resourceMan = new ResourceManager("SilkFlo.Email.Properties.Resources", typeof (SilkFlo.Email.Properties.Resources).Assembly);
        return SilkFlo.Email.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
      get => SilkFlo.Email.Properties.Resources.resourceCulture;
      set => SilkFlo.Email.Properties.Resources.resourceCulture = value;
    }

    public static string Email_Confirmation_Template => SilkFlo.Email.Properties.Resources.ResourceManager.GetString(nameof (Email_Confirmation_Template), SilkFlo.Email.Properties.Resources.resourceCulture);

    public static string Email_Changed_Confirmation_Template => SilkFlo.Email.Properties.Resources.ResourceManager.GetString("Email Changed Confirmation Template", SilkFlo.Email.Properties.Resources.resourceCulture);

    public static string Email_Password_Reset_Template => SilkFlo.Email.Properties.Resources.ResourceManager.GetString(nameof (Email_Password_Reset_Template), SilkFlo.Email.Properties.Resources.resourceCulture);
  }
}
