// Decompiled with JetBrains decompiler
// Type: EraLauncher.Properties.Resources
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace EraLauncher.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (EraLauncher.Properties.Resources.resourceMan == null)
          EraLauncher.Properties.Resources.resourceMan = new ResourceManager("EraLauncher.Properties.Resources", typeof (EraLauncher.Properties.Resources).Assembly);
        return EraLauncher.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => EraLauncher.Properties.Resources.resourceCulture;
      set => EraLauncher.Properties.Resources.resourceCulture = value;
    }
  }
}
