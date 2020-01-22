using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;


namespace StartDailyApp
{
  internal class Program
  {
    private static void Main()
    {
      Action<string> Display = Console.WriteLine;
      // remove unwanted task bar menu items like ie, excel, word, ppt forced by GPO
      // start my daily applications
      string userNameProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      string iconCache = $@"{userNameProfile}\AppData\Local";
      try
      {
        if (File.Exists(iconCache))
        {
          File.Delete(iconCache);
        }
      }
      catch (Exception)
      {
        Display($"error while trying to delete the file {iconCache}");
      }

      // RDP manager
      string RdpManager = $@"{userNameProfile}\Documents\RDPManager.rdg";
      if (File.Exists(RdpManager))
      {
        StartProcess(RdpManager);
      }

      string office2016 = @"C:\Program Files (x86)\Microsoft Office\root\Office16\outlook.exe";
      if (File.Exists(office2016))
      {
        StartProcess(office2016);
      }

      string ssms = @"C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\ssms.exe";
      if (File.Exists(ssms))
      {
        StartProcess(ssms);
      }

      string vs2017 = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\DEVENV.exe";
      if (File.Exists(vs2017))
      {
        StartProcess(vs2017);
      }

      string explorer = @"C:\Windows\explorer.exe";
      if (File.Exists(explorer))
      {
        StartProcess(explorer);
      }

      string firefox = @"C:\Program Files\Mozilla Firefox\firefox.exe";
      if (File.Exists(firefox))
      {
        StartProcess(firefox);
      }
      

      Display("Press any key to exit:");
      Console.ReadKey();
    }

    public static void StartProcess(string applicationExe, string arguments = "", bool useShellExecute = true, bool createNoWindow = false)
    {
      Process task = new Process
      {
        StartInfo =
        {
          UseShellExecute = useShellExecute,
          FileName = applicationExe,
          Arguments = arguments,
          CreateNoWindow = createNoWindow
        }
      };

      task.Start();
    }

    public static bool DeleteRegistryKey(string keyPath, string keyName)
    {
      bool result = false;
      //string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
      using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true))
      {
        if (key == null)
        {
          result = false;
        }
        else
        {
          key.DeleteValue(keyName);
          result = true;
        }
      }

      return result;
    }
  }

}
