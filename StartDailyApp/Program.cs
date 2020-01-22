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
      string iconCache = @"C:\Users\KWA960\AppData\Local";
      try
      {
        if (File.Exists(iconCache))
        {
          File.Delete(iconCache);
        }
      }
      catch (Exception)
      {
        Display("error while trying to delete the file ");
      }

      string userNameProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      var test = $@"{ userNameProfile}\Documents\RDPManager.rdg";
      StartProcess($@"{userNameProfile}\Documents\RDPManager.rdg");
      StartProcess(@"C:\Program Files (x86)\Microsoft Office\root\Office16\outlook.exe");
      StartProcess(@"C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\ssms.exe");
      StartProcess(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\DEVENV.exe");
      StartProcess(@"C:\Windows\explorer.exe");
      StartProcess(@"C:\Program Files\Mozilla Firefox\firefox.exe");

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
