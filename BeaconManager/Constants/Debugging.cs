using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 5/15/2020
/// </summary>
namespace BeaconManager.Constants
{
    /// <summary>
    /// Debugging messages container class.
    /// </summary>
    internal static class Debugging
    {
        internal const string EMAIL_EMPTY_TO = "ERROR: [Email to] setting is missing.";
        internal const string EMAIL_EMPTY_SUBJECT = "ERROR: [Email subject] setting is missing.";
        internal const string EMAIL_EMPTY_BODY = "ERROR: [Email body] setting is missing.";
    }
}
