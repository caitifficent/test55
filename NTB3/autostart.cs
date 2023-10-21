/// <summary>
/// Enables or disables the autostart (with the OS) of the application.
/// </summary>
using Microsoft.Win32;
using System.Reflection;
public static class AutoStarter
{
    private const string RUN_LOCATION = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string VALUE_NAME = "NTB.exe";

    /// <summary>
    /// Set the autostart value for the assembly.
    /// </summary>
    public static void SetAutoStart()
    {
        RegistryKey key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
        key.SetValue("NTB.exe", Assembly.GetExecutingAssembly().Location);
    }

    /// <summary>
    /// Returns whether auto start is enabled.
    /// </summary>
    public static bool IsAutoStartEnabled
    {
        get
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION);
            if (key == null)
                return false;

            string value = (string)key.GetValue("NTB.exe");
            if (value == null)
                return false;
            return (value == Assembly.GetExecutingAssembly().Location);
        }
    }

    /// <summary>
    /// Unsets the autostart value for the assembly.
    /// </summary>
    public static void UnSetAutoStart()
    {
        RegistryKey key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
        key.DeleteValue("NTB.exe");
    }
}