// Decompiled with JetBrains decompiler
// Type: EraLauncher.LauncherFunctionsLibrary
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

using IniParser;
using IniParser.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EraLauncher
{
  internal class LauncherFunctionsLibrary
  {
    public void ExecutePage(Page classxyz, System.Windows.Controls.Frame PageContent) => PageContent.Content = (object) classxyz;

    public void WriteToConfig(string CATEGORY, string ITEM, string CONTENT)
    {
      string filePath = Environment.ExpandEnvironmentVariables(((MainWindow) Application.Current.MainWindow).homevar.configpath);
      FileIniDataParser fileIniDataParser = new FileIniDataParser();
      IniData parsedData = fileIniDataParser.ReadFile(filePath);
      parsedData[CATEGORY][ITEM] = CONTENT;
      fileIniDataParser.WriteFile(filePath, parsedData);
    }

    public void ExecuteVersionPure(VersionData data) => ((MainWindow) Application.Current.MainWindow).homevar.GameVersion.Content = (object) data.Id.Replace(",", ".");
  }
}
