// Decompiled with JetBrains decompiler
// Type: EraLauncher.VersionData
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

namespace EraLauncher
{
  public class VersionData
  {
    public string _path;
    public string _Id;
    public string _splashpath;

    public string path
    {
      get => this._path;
      set => this._path = value;
    }

    public string splashpath
    {
      get => this.path + "\\FortniteGame\\Content\\Splash\\Splash.bmp";
      set => this._splashpath = value;
    }

    public string Id
    {
      get => this._Id.Replace(",", ".");
      set => this._Id = value;
    }
  }
}
