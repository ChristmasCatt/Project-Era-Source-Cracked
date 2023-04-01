// Decompiled with JetBrains decompiler
// Type: EraLauncher.AddVersionPage
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.CodeDom.Compiler;
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
  public partial class AddVersionPage : Page, IComponentConnector, IStyleConnector
  {
    public string LastGamePath;
    public string CurrentVerstr = "Version name";
    internal Border FadeBackground;
    internal TextBox PathBox;
    internal Button BrowseButton;
    internal Button SaveButton;
    internal TextBox NameBox;
    internal Button CloseButton;
    private bool _contentLoaded;

    public AddVersionPage()
    {
      this.InitializeComponent();
      ((MainWindow) Application.Current.MainWindow).AllowNavigation = false;
    }

    private void BrowseEvent(object sender, RoutedEventArgs e)
    {
      CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
      commonOpenFileDialog.IsFolderPicker = true;
      commonOpenFileDialog.Multiselect = false;
      if (commonOpenFileDialog.ShowDialog() != CommonFileDialogResult.Ok)
        return;
      this.LastGamePath = commonOpenFileDialog.FileName;
      this.PathBox.Text = commonOpenFileDialog.FileName;
    }

    private void AttemptAdd(object sender, RoutedEventArgs e)
    {
      if (Directory.Exists(this.LastGamePath + "/FortniteGame/Binaries/Win64/"))
      {
        if (this.CurrentVerstr.Length < 15)
        {
          ((MainWindow) Application.Current.MainWindow).homevar.AddBuild(new VersionData()
          {
            Id = this.CurrentVerstr.Replace(".", ","),
            path = this.LastGamePath
          });
          this.AttemptClose((object) this, new RoutedEventArgs());
        }
        else
          new ToastContentBuilder().AddArgument("action", "viewConversation").AddArgument("conversationId", 9813).AddText("FortMP Client").AddText("Version profile name must contain less than 16 characterss!").Show();
      }
      else
        new ToastContentBuilder().AddArgument("action", "viewConversation").AddArgument("conversationId", 9813).AddText("FortMP Client").AddText("Unable to perform action, you must select a folder that contains folders called 'FortniteGame' and 'Engine'!").Show();
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
      if (!(sender is TextBox textBox))
        return;
      this.CurrentVerstr = textBox.Text;
    }

    private void AttemptClose(object sender, RoutedEventArgs e) => ((MainWindow) Application.Current.MainWindow).homevar.AdditionalSettingsFrameContent.Content = (object) null;

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/EraLauncher;component/addversionpage.xaml", UriKind.Relative));
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
          break;
        case 3:
          this.BrowseButton = (Button) target;
          this.BrowseButton.Click += new RoutedEventHandler(this.BrowseEvent);
          break;
        case 4:
          this.SaveButton = (Button) target;
          this.SaveButton.Click += new RoutedEventHandler(this.AttemptAdd);
          break;
        case 5:
          this.NameBox = (TextBox) target;
          break;
        case 7:
          this.CloseButton = (Button) target;
          break;
        case 8:
          ((UIElement) target).MouseDown += new MouseButtonEventHandler(this.AttemptClose);
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
      if (connectionId != 6)
        return;
      ((TextBoxBase) target).TextChanged += new TextChangedEventHandler(this.OnTextChanged);
    }
  }
}
