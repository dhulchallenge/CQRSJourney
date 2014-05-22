namespace Common
{
    using System;
    using Microsoft.WindowsAzure;
    public class MaintenanceMode
    {
        public const string MaintenanceModeSettingName = "MaintenanceMode";

        public static bool IsInMaintainanceMode { get; internal set; }

        public static void RefreshIsInMaintainanceMode()
        {
            var settingValue = CloudConfigurationManager.GetSetting(MaintenanceModeSettingName);
            IsInMaintainanceMode = (!string.IsNullOrEmpty(settingValue) &&
                                    string.Equals(settingValue, "true", StringComparison.OrdinalIgnoreCase));
        }
    }
}
