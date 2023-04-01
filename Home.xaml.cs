// Decompiled with JetBrains decompiler
// Type: EraLauncher.Home
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

using IniParser;
using IniParser.Model;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace EraLauncher
{
  public partial class Home : Page, IComponentConnector, IStyleConnector
  {
    public string CurrentLauncherDetails;
    public VersionData CurrentVersion;
    public List<VersionData> builds = new List<VersionData>();
    private LauncherFunctionsLibrary lfn = new LauncherFunctionsLibrary();
    public string templastsavedkeyname;
    public SectionData templastsavedsection;
    private int OgraniczenieWersji = 8;
    public string configpath = "%localAppdata%\\ProjectEra\\launcherconfig";
    internal Home HomePage;
    internal Grid GridBG;
    internal Label GameName;
    internal Label GameVersion;
    internal Button LaunchButton;
    internal TextBlock LauncherInformation;
    internal ListView VersionsList;
    internal Button AddVersionButton;
    internal Button RemoveBuild;
    internal System.Windows.Controls.Frame AdditionalSettingsFrameContent;
    private bool _contentLoaded;

    public Home()
    {
      this.InitializeComponent();
      FileIniDataParser fileIniDataParser = new FileIniDataParser();
      Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProjectEra"));
      if (!File.Exists(Environment.ExpandEnvironmentVariables("%localAppdata%\\ProjectEra\\launcherconfig")))
        fileIniDataParser.WriteFile(Environment.ExpandEnvironmentVariables(this.configpath), new IniData());
      foreach (SectionData section in fileIniDataParser.ReadFile(Environment.ExpandEnvironmentVariables(this.configpath)).Sections)
      {
        if (section.SectionName == "VERSIONS")
        {
          foreach (KeyData key in section.Keys)
          {
            string[] strArray = key.Value.Split(new string[1]
            {
              "||"
            }, StringSplitOptions.None);
            this.AddBuild(new VersionData()
            {
              Id = strArray[0],
              path = strArray[1]
            });
          }
        }
      }
    }

    private void SelectVersion_Event(object sender, RoutedEventArgs e)
    {
      string str = ((ContentControl) sender).Content.ToString();
      foreach (VersionData build in this.builds)
      {
        if (build.Id == str)
        {
          this.CurrentVersion = build;
          this.lfn.ExecuteVersionPure(build);
        }
      }
    }

    public void AddBuild(VersionData vdata)
    {
      if (!Directory.Exists(vdata.path + "/FortniteGame/Binaries/Win64/"))
        return;
      vdata.Id.Replace(",", ".");
      string filePath = Environment.ExpandEnvironmentVariables(this.configpath);
      FileIniDataParser fileIniDataParser = new FileIniDataParser();
      IniData parsedData = fileIniDataParser.ReadFile(filePath);
      int num = this.builds.Count + 1;
      if (num < this.OgraniczenieWersji + 1)
      {
        string keyName = "v" + num.ToString();
        parsedData["VERSIONS"][keyName] = vdata.Id + "||" + vdata.path;
        fileIniDataParser.WriteFile(filePath, parsedData);
        this.VersionsList.ItemsSource = (IEnumerable) null;
        this.builds.Add(vdata);
        this.VersionsList.ItemsSource = (IEnumerable) this.builds;
      }
    }

    public void RemoveCurrentBuildFromConfig()
    {
      string filePath = Environment.ExpandEnvironmentVariables(this.configpath);
      FileIniDataParser fileIniDataParser = new FileIniDataParser();
      IniData parsedData = fileIniDataParser.ReadFile(filePath);
      foreach (SectionData section in parsedData.Sections)
      {
        if (section.SectionName == "VERSIONS")
        {
          foreach (KeyData key in section.Keys)
          {
            if (key.Value == this.CurrentVersion.Id + "||" + this.CurrentVersion.path)
            {
              section.Keys.RemoveKey(key.KeyName);
              fileIniDataParser.WriteFile(filePath, parsedData);
              break;
            }
          }
        }
      }
    }

    private void OnDiscordButtonClick(object sender, RoutedEventArgs e) => Process.Start("http://www.discord.gg/erafn");

    private void AddVersionClick(object sender, RoutedEventArgs e)
    {
      this.AdditionalSettingsFrameContent.Content = (object) new AddVersionPage();
      this.AdditionalSettingsFrameContent.Visibility = Visibility.Visible;
    }

    private void RemoveBuildEvent(object sender, RoutedEventArgs e)
    {
      if (this.CurrentVersion != null)
      {
        this.AdditionalSettingsFrameContent.Content = (object) new SettingsPage();
        this.AdditionalSettingsFrameContent.Visibility = Visibility.Visible;
      }
      else
        new ToastContentBuilder().AddArgument("action", "viewConversation").AddArgument("conversationId", 9813).AddText("FortMP Client").AddText("Unable to perform action, you must select a version first!").Show();
    }

    private void HandleNavigatingAdditionalSettingsFrame(object sender, NavigatingCancelEventArgs e)
    {
      if (e.NavigationMode != NavigationMode.Back)
        return;
      e.Cancel = true;
    }

    private void OnBGImageLoadedTEMP(object sender, RoutedEventArgs e)
    {
    }

    [DllImport("kernel32")]
    public static extern IntPtr CreateRemoteThread(
      IntPtr hProcess,
      IntPtr lpThreadAttributes,
      uint dwStackSize,
      UIntPtr lpStartAddress,
      IntPtr lpParameter,
      uint dwCreationFlags,
      out IntPtr lpThreadId);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(
      uint dwDesiredAccess,
      int bInheritHandle,
      int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern int CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool VirtualFreeEx(
      IntPtr hProcess,
      IntPtr lpAddress,
      UIntPtr dwSize,
      uint dwFreeType);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
    public static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr VirtualAllocEx(
      IntPtr hProcess,
      IntPtr lpAddress,
      uint dwSize,
      uint flAllocationType,
      uint flProtect);

    [DllImport("kernel32.dll")]
    private static extern int WriteProcessMemory(
      IntPtr hProcess,
      IntPtr lpBaseAddress,
      byte[] lpBuffer,
      uint nSize,
      int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern int WaitForSingleObject(IntPtr handle, int milliseconds);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenThread(
      Home.ThreadAccess dwDesiredAccess,
      bool bInheritHandle,
      uint dwThreadId);

    [DllImport("kernel32.dll")]
    private static extern uint SuspendThread(IntPtr hThread);

    [DllImport("kernel32.dll")]
    private static extern int ResumeThread(IntPtr hThread);

    public static unsafe bool Inject(int P, string DllPath)
    {
      IntPtr num1 = Home.OpenProcess(2035711U, 1, P);
      if (num1 == IntPtr.Zero)
        return false;
      IntPtr num2 = Home.VirtualAllocEx(num1, (IntPtr) (void*) null, (uint) DllPath.Length, 4096U, 64U);
      if (num2 == IntPtr.Zero)
        return false;
      byte[] bytes = Encoding.ASCII.GetBytes(DllPath);
      if (Home.WriteProcessMemory(num1, num2, bytes, (uint) bytes.Length, 0) == 0)
        return false;
      UIntPtr procAddress = Home.GetProcAddress(Home.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
      Home.CreateRemoteThread(num1, (IntPtr) (void*) null, 0U, procAddress, num2, 0U, out IntPtr _);
      Home.CloseHandle(num1);
      return true;
    }

    public static int PreInject(int ProcessID, string DllPath)
    {
      if (!File.Exists(DllPath))
        return 0;
      if (ProcessID == 0)
        return 1;
      return !Home.Inject(ProcessID, DllPath) ? 2 : 3;
    }

    public void StartFortnite()
    {
      if (File.Exists(Path.Combine(this.CurrentVersion.path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_BE.exe")))
      {
        Process process = new Process()
        {
          StartInfo = {
            FileName = Path.Combine(this.CurrentVersion.path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_BE.exe"),
            UseShellExecute = false
          }
        };
        if (process.Start())
        {
          foreach (ProcessThread thread in (ReadOnlyCollectionBase) process.Threads)
          {
            IntPtr hThread = Home.OpenThread(Home.ThreadAccess.SUSPEND_RESUME, false, (uint) thread.Id);
            if (!(hThread == IntPtr.Zero))
            {
              int num = (int) Home.SuspendThread(hThread);
            }
            else
              break;
          }
        }
      }
      Process process1 = new Process()
      {
        StartInfo = {
          FileName = Path.Combine(this.CurrentVersion.path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"),
          Arguments = "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -NOSSLPINNING -noeac -fromfl=be -fltoken=e8eb05fag41046i3hd23c89c -frombe",
          UseShellExecute = false
        }
      };
      process1.Start();
      process1.WaitForInputIdle();
      string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      Home.PreInject(process1.Id, Path.Combine(directoryName + "\\FortMPClient.dll"));
    }

    private void HandleGameLaunch(object sender, RoutedEventArgs e)
    {
      if (this.CurrentVersion == null)
        return;
      new Thread(new ThreadStart(this.StartFortnite)).Start();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/EraLauncher;component/home.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.HomePage = (Home) target;
          break;
        case 2:
          this.GridBG = (Grid) target;
          break;
        case 3:
          this.GameName = (Label) target;
          break;
        case 4:
          this.GameVersion = (Label) target;
          break;
        case 5:
          this.LaunchButton = (Button) target;
          this.LaunchButton.Click += new RoutedEventHandler(this.HandleGameLaunch);
          break;
        case 6:
          this.LauncherInformation = (TextBlock) target;
          break;
        case 7:
          this.VersionsList = (ListView) target;
          break;
        case 9:
          this.AddVersionButton = (Button) target;
          this.AddVersionButton.Click += new RoutedEventHandler(this.AddVersionClick);
          break;
        case 10:
          this.RemoveBuild = (Button) target;
          this.RemoveBuild.Click += new RoutedEventHandler(this.RemoveBuildEvent);
          break;
        case 11:
          this.AdditionalSettingsFrameContent = (System.Windows.Controls.Frame) target;
          this.AdditionalSettingsFrameContent.Navigating += new NavigatingCancelEventHandler(this.HandleNavigatingAdditionalSettingsFrame);
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
      if (connectionId != 8)
        return;
      ((ButtonBase) target).Click += new RoutedEventHandler(this.SelectVersion_Event);
    }

    public enum ThreadAccess
    {
      TERMINATE = 1,
      SUSPEND_RESUME = 2,
      GET_CONTEXT = 8,
      SET_CONTEXT = 16, // 0x00000010
      SET_INFORMATION = 32, // 0x00000020
      QUERY_INFORMATION = 64, // 0x00000040
      SET_THREAD_TOKEN = 128, // 0x00000080
      IMPERSONATE = 256, // 0x00000100
      DIRECT_IMPERSONATION = 512, // 0x00000200
    }
  }
}
