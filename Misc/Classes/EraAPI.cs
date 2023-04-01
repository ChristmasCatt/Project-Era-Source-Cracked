// Decompiled with JetBrains decompiler
// Type: EraLauncher.Misc.Classes.EraAPI
// Assembly: EraLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC7191F1-5421-41FB-822D-45081B4054A4
// Assembly location: C:\Users\z9jpo\Desktop\fortnite\FortniteLauncher.exe

using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace EraLauncher.Misc.Classes
{
  public class EraAPI
  {
    public string GetItemContentFromCloudstorageList(string _string) => _string.Split(new string[1]
    {
      ";"
    }, StringSplitOptions.None)[1];

    public object GetJsonStringElement(string url)
    {
      WebClient webClient = new WebClient();
      try
      {
        webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        return JsonConvert.DeserializeObject(new StreamReader(webClient.OpenRead(url)).ReadToEnd());
      }
      catch
      {
        return (object) "Couldn't connect to the central server!";
      }
    }
  }
}
