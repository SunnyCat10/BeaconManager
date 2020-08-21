using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 5/15/2020
/// </summary>
namespace BeaconManager.OutlookManager
{
    internal static class BlockedMailSettings
    {
        internal static string EmailTo { get; set; } = "example@gmail.com";
        internal static string EmailSubject { get; set; } = "משואות תקולות";

        internal static string EmailBodyPrefix { get; set; } = "מצורף בזאת, רשימת המשואות התקולות במאמן";
        internal static string EmailBodySuffix { get; set; } = ",תודה מראש";
    }
}
