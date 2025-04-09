# WindowsActivator

A simple C# tool to activate Windows by detecting the operating system version (Windows 10 or 11) and edition (Home, Pro, Enterprise). It generates a batch file with the appropriate license key and runs it to activate Windows using `slmgr`. It uses the kms8.msguides.com KMS server for activation.

##Activation
- Read more about it at:
- Windows 10: [https://msguides.com/windows-10](https://msguides.com/windows-10)
- Windows 11: [https://msguides.com/windows-11](https://msguides.com/windows-11)
  Credits: MS Guides

## Features
- Detects Windows version (10/11) and edition (Home/Pro/Enterprise) using WMI.
- Generates and runs a batch file to activate Windows.
- Available as a single, self-contained `.exe` for easy use.

## Download Pre-Built Executable
For users who just want to run the tool without building it:

1. Go to the [Releases page](https://github.com/TheShadeV/WindowsLicenseActivator/releases).
2. Download `WindowsActivator.zip`.
3. Extract the zip to get `WindowsActivator.exe`.
4. Right-click `WindowsActivator.exe` and select **Run as administrator**.

### Requirements for Pre-Built `.exe`
- No additional software needed (self-contained executable).
- Must be run as administrator due to `slmgr` commands.

## Build and Run from Source
For developers or users who want to modify the tool:

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (or the version matching your project’s `TargetFramework`).
- Visual Studio 2022 (or another IDE that supports C#).

### Steps
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/TheShadeV/WindowsActivator.git

**Note:** This tool is for educational purposes only. You must provide your own valid Windows license keys. Using unauthorized keys or KMS servers may violate Microsoft’s terms of service.
