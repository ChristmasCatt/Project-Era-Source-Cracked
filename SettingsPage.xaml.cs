// Decompiled with JetBrains decompiler
// Type: EraLauncher.SettingsPage
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

using EraLauncher.Misc.Classes;
using IniParser;
using IniParser.Model;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace EraLauncher
{
  public partial class SettingsPage : Page, IComponentConnector, IStyleConnector
  {
    public string NewPath;
    public string CurrentVerstr = "Version name";
    public EraAPI api = new EraAPI();
    private LauncherFunctionsLibrary lfn = new LauncherFunctionsLibrary();
    internal Border FadeBackground;
    internal TextBox PathBox;
    internal Button BrowseButton;
    internal Button DeleteButton;
    internal TextBox NameBox;
    internal Button CloseButton;
    internal Button SaveButton;
    private bool _contentLoaded;

    public SettingsPage() => this.InitializeComponent();

    private void BrowseEvent(object sender, RoutedEventArgs e)
    {
      CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
      commonOpenFileDialog.IsFolderPicker = true;
      commonOpenFileDialog.Multiselect = false;
      commonOpenFileDialog.InitialDirectory = this.PathBox.Text;
      if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
        return;
      new FileIniDataParser().ReadFile(Environment.ExpandEnvironmentVariables(((MainWindow) Application.Current.MainWindow).homevar.configpath));
      if (Directory.Exists(commonOpenFileDialog.FileName + "/FortniteGame/Binaries/Win64/"))
        this.PathBox.Text = commonOpenFileDialog.FileName;
      else
        new ToastContentBuilder().AddArgument("action", "viewConversation").AddArgument("conversationId", 9813).AddText("FortMP Client").AddText("Unable to perform action, you must select a folder that contains folders called 'FortniteGame' and 'Engine'!").Show();
    }

    private void HandleCloseWindow(object sender, RoutedEventArgs e)
    {
      string filePath = Environment.ExpandEnvironmentVariables(((MainWindow) Application.Current.MainWindow).homevar.configpath);
      FileIniDataParser fileIniDataParser = new FileIniDataParser();
      IniData parsedData = fileIniDataParser.ReadFile(filePath);
      foreach (SectionData section in parsedData.Sections)
      {
        if (section.SectionName == "VERSIONS")
        {
          foreach (KeyData key in section.Keys)
          {
            string[] strArray = key.Value.Split(new string[1]
            {
              "||"
            }, StringSplitOptions.None);
            if (((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.path == strArray[1])
            {
              List<VersionData> builds = ((MainWindow) Application.Current.MainWindow).homevar.builds;
              foreach (VersionData versionData in builds)
              {
                if (versionData.path == ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.path)
                {
                  ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.path = this.NewPath;
                  versionData.path = this.NewPath;
                  versionData.splashpath = this.NewPath + "\\FortniteGame\\Content\\Splash\\Splash.bmp";
                  ((MainWindow) Application.Current.MainWindow).homevar.VersionsList.ItemsSource = (IEnumerable) null;
                  ((MainWindow) Application.Current.MainWindow).homevar.VersionsList.ItemsSource = (IEnumerable) builds;
                  key.Value = strArray[0] + "||" + this.NewPath;
                  fileIniDataParser.WriteFile(filePath, parsedData);
                  break;
                }
              }
            }
          }
        }
      }
      foreach (SectionData section in parsedData.Sections)
      {
        if (section.SectionName == "VERSIONS")
        {
          foreach (KeyData key in section.Keys)
          {
            string[] strArray = key.Value.Split(new string[1]
            {
              "||"
            }, StringSplitOptions.None);
            if (((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.Id == strArray[0])
            {
              List<VersionData> builds = ((MainWindow) Application.Current.MainWindow).homevar.builds;
              foreach (VersionData versionData in builds)
              {
                if (versionData.Id == ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.Id)
                {
                  ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.Id = this.CurrentVerstr;
                  versionData.Id = this.CurrentVerstr;
                  ((MainWindow) Application.Current.MainWindow).homevar.VersionsList.ItemsSource = (IEnumerable) null;
                  ((MainWindow) Application.Current.MainWindow).homevar.VersionsList.ItemsSource = (IEnumerable) builds;
                  key.Value = this.CurrentVerstr + "||" + strArray[1];
                  fileIniDataParser.WriteFile(filePath, parsedData);
                  ((MainWindow) Application.Current.MainWindow).homevar.GameVersion.Content = (object) ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.Id;
                  break;
                }
              }
            }
          }
        }
      }
      ((MainWindow) Application.Current.MainWindow).homevar.AdditionalSettingsFrameContent.Content = (object) null;
    }

    private void OnPathBoxLoaded(object sender, RoutedEventArgs e)
    {
      this.PathBox.Text = ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.path;
      this.NameBox.Text = ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion.Id;
    }

    private void HandleRemoveProfile(object sender, RoutedEventArgs e)
    {
      VersionData currentVersion = ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion;
      if (!((MainWindow) Application.Current.MainWindow).homevar.builds.Contains(currentVersion))
        return;
      ((MainWindow) Application.Current.MainWindow).homevar.VersionsList.ItemsSource = (IEnumerable) null;
      ((MainWindow) Application.Current.MainWindow).homevar.builds.Remove(currentVersion);
      ((MainWindow) Application.Current.MainWindow).homevar.RemoveCurrentBuildFromConfig();
      ((MainWindow) Application.Current.MainWindow).homevar.VersionsList.ItemsSource = (IEnumerable) ((MainWindow) Application.Current.MainWindow).homevar.builds;
      ((MainWindow) Application.Current.MainWindow).homevar.CurrentVersion = (VersionData) null;
      this.lfn.ExecuteVersionPure(new VersionData()
      {
        Id = "Select Game Version.",
        path = "placeholder"
      });
      ((MainWindow) Application.Current.MainWindow).homevar.AdditionalSettingsFrameContent.Content = (object) null;
    }

    private void OnPathUpdated(object sender, TextChangedEventArgs e)
    {
      if (!(sender is TextBox textBox))
        return;
      this.NewPath = textBox.Text;
    }

    private void HandleGameNameUpdate(object sender, TextChangedEventArgs e)
    {
      if (!(sender is TextBox textBox))
        return;
      this.CurrentVerstr = textBox.Text;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/EraLauncher;component/settingspage.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.FadeBackground = (Border) target;
          break;
        case 2:
          this.PathBox = (TextBox) target;
          this.PathBox.Loaded += new RoutedEventHandler(this.OnPathBoxLoaded);
          break;
        case 4:
          this.BrowseButton = (Button) target;
          this.BrowseButton.Click += new RoutedEventHandler(this.BrowseEvent);
          break;
        case 5:
          this.DeleteButton = (Button) target;
          this.DeleteButton.Click += new RoutedEventHandler(this.HandleRemoveProfile);
          break;
        case 6:
          this.NameBox = (TextBox) target;
          break;
        case 8:
          this.CloseButton = (Button) target;
          break;
        case 9:
          ((UIElement) target).MouseDown += new MouseButtonEventHandler(this.HandleCloseWindow);
          break;
        case 10:
          this.SaveButton = (Button) target;
          this.SaveButton.Click += new RoutedEventHandler(this.HandleCloseWindow);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IStyleConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 3:
          ((TextBoxBase) target).TextChanged += new TextChangedEventHandler(this.OnPathUpdated);
          break;
        case 7:
          ((TextBoxBase) target).TextChanged += new TextChangedEventHandler(this.HandleGameNameUpdate);
          break;
      }
    }
  }
}
