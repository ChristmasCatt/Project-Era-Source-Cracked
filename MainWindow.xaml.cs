// Decompiled with JetBrains decompiler
// Type: EraLauncher.MainWindow
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

using EraLauncher.Misc.Classes;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;

namespace EraLauncher
{
  public partial class MainWindow : Window, IComponentConnector
  {
    public Home homevar = new Home();
    public Changelog clvar = new Changelog();
    private LauncherFunctionsLibrary lfn = new LauncherFunctionsLibrary();
    public bool AllowNavigation = false;
    public bool KeepCancel = false;
    public EraAPI api = new EraAPI();
    internal Grid ApplicationBackground;
    internal System.Windows.Controls.Frame PageContent;
    internal Button CloseButton;
    internal Button MinimizeButton;
    internal Border UpperTabsPanel;
    internal Button MainPage_Btn;
    internal Grid StartAnimGrid;
    internal Border BGA;
    internal TranslateTransform BGATransform;
    internal Border EraLOGOA;
    private bool _contentLoaded;

    public MainWindow() => this.InitializeComponent();

    private void Close_Button_Event(object sender, RoutedEventArgs e) => this.Close();

    private void Minimalize_Button_Event(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

    private void MainPage_Btn_Event(object sender, RoutedEventArgs e) => this.lfn.ExecutePage((Page) this.homevar, this.PageContent);

    private void Changelog_Btn_Event(object sender, RoutedEventArgs e) => this.lfn.ExecutePage((Page) this.clvar, this.PageContent);

    private void UpperPanel_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.LeftButton != MouseButtonState.Pressed)
        return;
      this.DragMove();
    }

    private void OnStartAnimationBackgroundLoaded(object sender, RoutedEventArgs e)
    {
    }

    private void Navigating(object sender, NavigatingCancelEventArgs e)
    {
      if (e.NavigationMode != NavigationMode.Back)
        return;
      e.Cancel = true;
    }

    private void HandlePageContentLoaded(object sender, RoutedEventArgs e) => this.lfn.ExecutePage((Page) this.homevar, this.PageContent);

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/EraLauncher;component/mainwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.ApplicationBackground = (Grid) target;
          break;
        case 2:
          this.PageContent = (System.Windows.Controls.Frame) target;
          this.PageContent.Navigating += new NavigatingCancelEventHandler(this.Navigating);
          this.PageContent.Loaded += new RoutedEventHandler(this.HandlePageContentLoaded);
          break;
        case 3:
          ((UIElement) target).MouseMove += new MouseEventHandler(this.UpperPanel_MouseMove);
          break;
        case 4:
          this.CloseButton = (Button) target;
          this.CloseButton.Click += new RoutedEventHandler(this.Close_Button_Event);
          break;
        case 5:
          this.MinimizeButton = (Button) target;
          this.MinimizeButton.Click += new RoutedEventHandler(this.Minimalize_Button_Event);
          break;
        case 6:
          this.UpperTabsPanel = (Border) target;
          break;
        case 7:
          this.MainPage_Btn = (Button) target;
          this.MainPage_Btn.Click += new RoutedEventHandler(this.MainPage_Btn_Event);
          break;
        case 8:
          this.StartAnimGrid = (Grid) target;
          this.StartAnimGrid.Loaded += new RoutedEventHandler(this.OnStartAnimationBackgroundLoaded);
          this.StartAnimGrid.MouseMove += new MouseEventHandler(this.UpperPanel_MouseMove);
          break;
        case 9:
          this.BGA = (Border) target;
          break;
        case 10:
          this.BGATransform = (TranslateTransform) target;
          break;
        case 11:
          this.EraLOGOA = (Border) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
