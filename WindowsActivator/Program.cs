using System;
using System.IO;
using System.Management;
using System.Diagnostics;
using System.Security.Principal;

class Program
{
    static void Main()
    {
        // Check if running as admin
        if (!IsRunningAsAdmin())
        {
            Console.WriteLine("This application requires administrative privileges. Please restart as administrator.");
            Console.ReadLine();
            return;
        }

        // Step 1: Get Windows version and edition
        (string version, string edition) = GetWindowsInfo();
        Console.WriteLine($"Detected: {version} {edition}");

        // Step 2: Determine the license key
        string licenseKey = DetermineLicenseKey(version, edition);
        Console.WriteLine($"License Key: {licenseKey}");

        // Step 3: Update the .bat file
        string batFilePath = "activate.bat";
        UpdateBatFile(batFilePath, licenseKey);

        // Step 4: Run the .bat file
        RunBatFile(batFilePath);
    }

    static bool IsRunningAsAdmin()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    static (string version, string edition) GetWindowsInfo()
    {
        string version = "Unknown";
        string edition = "Unknown";

        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                string caption = os["Caption"]?.ToString();
                if (!string.IsNullOrEmpty(caption))
                {
                    string cleanedCaption = caption.Replace("Microsoft", "").Trim();
                    string[] versions = { "Windows 11", "Windows 10" };
                    foreach (var ver in versions)
                    {
                        if (cleanedCaption.StartsWith(ver, StringComparison.OrdinalIgnoreCase))
                        {
                            version = ver;
                            edition = cleanedCaption.Substring(ver.Length).Trim();
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error querying WMI: " + ex.Message);
        }

        return (version, edition);
    }

    static string DetermineLicenseKey(string version, string edition)
    {
        if (version == "Windows 11")
        {
            if (edition.Contains("Home")) return "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99";
            if (edition.Contains("Home N")) return "3KHY7-WNT83-DGQKR-F7HPR-844BM";
            if (edition.Contains("Home Single Language")) return "7HNRX-D7KGG-3K4RQ-4WPJ4-YTDFH";
            if (edition.Contains("Home Country Specific")) return "PVMJN-6DFY6-9CCP6-7BKTT-D3WVR";
            if (edition.Contains("Pro")) return "W269N-WFGWX-YVC9B-4J6C9-T83GX";
            if (edition.Contains("Pro N")) return "MH37W-N47XK-V7XM9-C7227-GCQG9";
            if (edition.Contains("Education")) return "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2";
            if (edition.Contains("Education N")) return "2WH4N-8QGBV-H22JP-CT43Q-MDWWJ";
            if (edition.Contains("Enterprise")) return "NPPR9-FWDCX-D2C8J-H872K-2YT43";
            if (edition.Contains("Enterprise N")) return "DPH2V-TTNVB-4X9Q3-TJR4H-KHJW4";
        }
        else if (version == "Windows 10")
        {
            if (edition.Contains("Home")) return "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99";
            if (edition.Contains("Home N")) return "3KHY7-WNT83-DGQKR-F7HPR-844BM";
            if (edition.Contains("Home Single Language")) return "7HNRX-D7KGG-3K4RQ-4WPJ4-YTDFH";
            if (edition.Contains("Home Country Specific")) return "PVMJN-6DFY6-9CCP6-7BKTT-D3WVR";
            if (edition.Contains("Pro")) return "W269N-WFGWX-YVC9B-4J6C9-T83GX";
            if (edition.Contains("Pro N")) return "MH37W-N47XK-V7XM9-C7227-GCQG9";
            if (edition.Contains("Education")) return "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2";
            if (edition.Contains("Education N")) return "2WH4N-8QGBV-H22JP-CT43Q-MDWWJ";
            if (edition.Contains("Enterprise")) return "NPPR9-FWDCX-D2C8J-H872K-2YT43";
            if (edition.Contains("Enterprise N")) return "DPH2V-TTNVB-4X9Q3-TJR4H-KHJW4";
        }
        return "DEFAULT-KEY-0000"; // Fallback key
    }

    static void UpdateBatFile(string filePath, string licenseKey)
    {
        try
        {
            string batContent = @"
@echo off
echo Activating Windows with key: {0}
slmgr /ipk {0}
slmgr /skms kms8.msguides.com
slmgr /ato
echo Activation complete.
pause
";
            string updatedContent = string.Format(batContent, licenseKey);
            File.WriteAllText(filePath, updatedContent);
            Console.WriteLine($"Updated {filePath} with license key: {licenseKey}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating .bat file: " + ex.Message);
        }
    }

    static void RunBatFile(string filePath)
    {
        try
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true,
            };

            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            Console.WriteLine("Batch file executed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error running .bat file: " + ex.Message);
        }
    }
}