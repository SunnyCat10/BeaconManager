using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Office.Interop.Outlook;
using System.Diagnostics;

using BeaconManager.Constants;
using BeaconManager.Beacon;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 5/15/2020
/// </summary>
namespace BeaconManager.OutlookManager
{
    /// <summary>
    /// Manages the blocked stations email creation and sending processes.
    /// </summary>
    internal static class BlockedMail
    {
        internal static string EmailTo { get; set; } =
            (string.IsNullOrEmpty(BlockedMailSettings.EmailTo)) ? Debugging.EMAIL_EMPTY_TO : BlockedMailSettings.EmailTo;

        internal static string EmailSubject { get; set; } =
            (string.IsNullOrEmpty(BlockedMailSettings.EmailSubject)) ? Debugging.EMAIL_EMPTY_SUBJECT : BlockedMailSettings.EmailSubject;

        internal static string EmailBody { get; set; } = Debugging.EMAIL_EMPTY_BODY;


        /// <summary>
        /// Generates the email.
        /// </summary>
        /// <returns> Is outlook already open? </returns>
        internal static bool GenerateMail()
        {
            if (Process.GetProcessesByName("OUTLOOK").Any())
            {
                return false;
            }
            else
            {
                new Thread(() =>
                {
                    Application OutLookApplicationSession = CreateOutlookSession();
                    CreateEmail(OutLookApplicationSession);
                }).Start();
                return true;
            }
        }

        /// <summary>
        /// Creates a new Outlook application session.
        /// </summary>
        /// <returns> The Outlook application session. </returns>
        private static Application CreateOutlookSession()
        {
            Application OutlookApplication = new Application();
            Folder calFolder = OutlookApplication.Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar) as Folder;
            return OutlookApplication;
        }

        /// <summary>
        /// Creates a new Email in Outlook based on the template we provide in the XML file.
        /// </summary>
        /// <param name="OutlookApplication"> The Outlook application session </param>
        /// <returns> Did the operation completed successfully? </returns>
        private static void CreateEmail(Application OutlookApplication)
        {
            MailItem mailItem = (MailItem)OutlookApplication.CreateItem(OlItemType.olMailItem);
            mailItem.Subject = EmailSubject;
            mailItem.To = EmailTo;
            mailItem.Body = EmailBody;
            mailItem.Importance = OlImportance.olImportanceHigh;
            mailItem.Display(true);
        }

        // Provide some protein :)
        internal static bool EmailBodyBuilder(List<int> userSelection)
        {
            if (userSelection.Count == 0)
                return false;
            else
            {

                string output = "";
                output += BlockedMailSettings.EmailBodyPrefix + '\n';

                foreach (var selection in userSelection)
                {
                    output += BeaconBuffer.Buffer[selection - 1].Info.ID + '\n';
                }

                output += BlockedMailSettings.EmailBodySuffix;

                EmailBody = output;
                return true;
            }
        }
    }
}
