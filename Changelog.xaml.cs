// Decompiled with JetBrains decompiler
// Type: EraLauncher.Changelog
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace EraLauncher
{
  public partial class Changelog : Page, IComponentConnector
  {
    public MainWindow MainWindowRef;
    internal Grid ChangelogGridBG;
    internal Border ImageBG;
    internal TextBlock ChangeLog;
    internal Label LatestNewsText;
    private bool _contentLoaded;

    public Changelog() => this.InitializeComponent();

    private void ChangelogTextNewsHandleLoaded(object sender, RoutedEventArgs e)
    {
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/EraLauncher;component/changelog.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.ChangelogGridBG = (Grid) target;
          break;
        case 2:
          this.ImageBG = (Border) target;
          break;
        case 3:
          this.ChangeLog = (TextBlock) target;
          this.ChangeLog.Loaded += new RoutedEventHandler(this.ChangelogTextNewsHandleLoaded);
          break;
        case 4:
          this.LatestNewsText = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
