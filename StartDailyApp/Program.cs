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
      Display("");
      Console.ForegroundColor = ConsoleColor.White;
      Display("Starting your daily applications");
      Display("");
      // remove unwanted task bar menu items like ie, excel, word, ppt forced by GPO
      // start my daily applications
      string userNameProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      string taskBar = $@"{userNameProfile}\AppData\Roaming\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar";
      string excel = Path.Combine(taskBar, "Excel");
      string ie11 = Path.Combine(taskBar, "Internet Explorer 11");
      string word = Path.Combine(taskBar, "Word");
      string ppt = Path.Combine(taskBar, "PowerPoint");
      string chrome = Path.Combine(taskBar, "Google Chrome (2)");
      try
      {
        if (File.Exists(excel))
        {
          File.Delete(excel);
          Display("Excel shorcut in the taskbar deleted");
        }
        else
        {
          Display("No Excel shorcut in the taskbar found");
        }
      }
      catch (Exception)
      {
        Display("Excel shorcut in the taskbar could not be deleted");
      }

      try
      {
        if (File.Exists(ie11))
        {
          File.Delete(ie11);
          Display("iExplore shorcut in the taskbar deleted");
        }
        else
        {
          Display("No iExplore shorcut in the taskbar found");
        }
      }
      catch (Exception)
      {
        Display("iExplore shorcut in the taskbar could not be deleted");
      }

      try
      {
        if (File.Exists(word))
        {
          File.Delete(word);
          Display("Word shorcut in the taskbar deleted");
        }
        else
        {
          Display("No Word shorcut in the taskbar found");
        }
      }
      catch (Exception)
      {
        Display("Word shorcut in the taskbar could not be deleted");
      }

      try
      {
        if (File.Exists(ppt))
        {
          File.Delete(ppt);
          Display("PowerPoint shorcut in the taskbar deleted");
        }
        else
        {
          Display("No PowerPoint shorcut in the taskbar found");
        }
      }
      catch (Exception)
      {
        Display("PowerPoint shorcut in the taskbar could not be deleted");
      }

      //string iconCache = $@"{userNameProfile}\AppData\Local";
      //try
      //{
      //  if (File.Exists(iconCache))
      //  {
      //    File.Delete(iconCache);
      //  }
      //}
      //catch (Exception)
      //{
      //  Display($"error while trying to delete the file {iconCache}");
      //}

      // RDP manager
      string RdpManager = $@"{userNameProfile}\Documents\RDPManager.rdg";
      if (File.Exists(RdpManager))
      {
        StartProcess(RdpManager);
        Console.ForegroundColor = ConsoleColor.Green;
        Display("RDP Manager has been started");
        Display("");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Display("RDP manager couldn't be found on this computer");
        Display("");
      }

      string outlook2016 = @"C:\Program Files (x86)\Microsoft Office\root\Office16\outlook.exe";
      if (File.Exists(outlook2016))
      {
        StartProcess(outlook2016);
        Console.ForegroundColor = ConsoleColor.Green;
        Display("Outlook 2016  has been started");
        Display("");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Display("Outlook 2016 couldn't be found on this computer");
        Display("");
      }

      string ssms = @"C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\ssms.exe";
      if (File.Exists(ssms))
      {
        StartProcess(ssms);
        Console.ForegroundColor = ConsoleColor.Green;
        Display("SQL Server Management Studio has been started");
        Display("");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Display("SQL Server Management Studio couldn't be found on this computer");
        Display("");
      }

      string vs2017 = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\DEVENV.exe";
      if (File.Exists(vs2017))
      {
        StartProcess(vs2017);
        Console.ForegroundColor = ConsoleColor.Green;
        Display("Visual Studio 2017 has been started");
        Display("");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Display("Visual Studio 2017 couldn't be found on this computer");
        Display("");
      }

      string explorer = @"C:\Windows\explorer.exe";
      if (File.Exists(explorer))
      {
        StartProcess(explorer);
        Console.ForegroundColor = ConsoleColor.Green;
        Display("Windows Explorer has been started");
        Display("");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Display("Windows Explorer couldn't be found on this computer");
        Display("");
      }

      string firefox = @"C:\Program Files\Mozilla Firefox\firefox.exe";
      if (File.Exists(firefox))
      {
        StartProcess(firefox);
        Console.ForegroundColor = ConsoleColor.Green;
        Display("Firefox has been started");
        Display("");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Display("Firefox couldn't be found on this computer");
        Display("");
      }

      Console.ForegroundColor = ConsoleColor.White;
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