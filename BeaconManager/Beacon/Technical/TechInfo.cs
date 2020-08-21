using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Internal;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 18/5/2020
/// </summary>
namespace BeaconManager.Beacon.Technical
{
    internal struct TechInfo
    {
        internal string IP { get; set; }
        internal string ComputerName { get; set; }
        internal string MacAdress { get; set; }

        private const string DEFAULT_IP = "0.0.0.0";
        private const string DEFAULT_COMPUTER_NAME = "ZI0500MALLI000";
        private const string DEFAULT_MAC_ADRESS = "00:00:00:00:00:00";

        private const string MALI1_PREFIX = "ZI0500MALI1";
        private const string MALLI_PREFIX = "ZI0500MALLI"; 
        private const int PREFIX_LENGTH = 11;
        private const int VALID_NAME_LENGTH = 15;

        /// <summary>
        /// Tech-Info struct constructor - validates correct User input before initiating the struct.
        /// </summary>
        /// <param name="ip"> IP of the PC station. </param>
        /// <param name="computerName"> Computer Name of the PC station. </param>
        /// <param name="macAdress"> MAC Adress of the PC station. </param>
        internal TechInfo(string ip, string computerName, string macAdress)
        {
            IP = CheckIP(ip) ? ip : DEFAULT_IP;
            ComputerName = CheckComputerName(computerName) ? computerName : DEFAULT_COMPUTER_NAME;
            MacAdress = CheckMacAdress(macAdress) ? macAdress : DEFAULT_MAC_ADRESS;
        }

        /// <summary>
        /// Validates the IP input.
        /// </summary>
        /// <param name="ip"> IP string input. </param>
        /// <returns> Validation results. </returns>
        private static bool CheckIP(string ip)
        {
            if (!string.IsNullOrEmpty(ip))
            {
                var numOfDots = ip.Count(x => x == '.');
                if (numOfDots != 3)
                    return false;

                string[] ipSegments = ip.Split('.');
                
                foreach(var ipSegment in ipSegments)
                {
                    byte temp;
                    if (!byte.TryParse(ipSegment, out temp))
                        return false;
                }

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Computer Name input Validation.
        /// </summary>
        /// <param name="computerName"> Computer Name string input. </param>
        /// <returns> Validation results. </returns>
        private static bool CheckComputerName(string computerName)
        {
            if ( !(string.IsNullOrEmpty(computerName)) )
            {
                if (!(computerName.Length == VALID_NAME_LENGTH))
                    return false; ;

                var namePrefix = computerName.Substring(0, PREFIX_LENGTH);
                if (namePrefix.Equals(MALLI_PREFIX) || namePrefix.Equals(MALI1_PREFIX))
                {
                    var nameSuffix = computerName.Substring(PREFIX_LENGTH + 1);
                    nameSuffix = nameSuffix.TrimStart('0');

                    byte temp;
                    return (byte.TryParse(nameSuffix, out temp));
                }
                return false;
            }
            else
                return false;
        }

        /// <summary>
        /// MAC Adress input validation.
        /// </summary>
        /// <param name="macAdress"> MAC Adress input. </param>
        /// <returns> Validation results. </returns>
        private static bool CheckMacAdress(string macAdress)
        {
            var numOfSemicolons = macAdress.Count(x => x == ':');
            if (numOfSemicolons != 5)
                return false;

            string[] segments = macAdress.Split(':');

            foreach (var segment in segments)
            {
                char firstChar = char.Parse(segment.Substring(0, 1));
                char secondChar = char.Parse(segment.Substring(1));

                if (!char.IsLetterOrDigit(firstChar) || !char.IsLetterOrDigit(secondChar))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// String representation of TechInfo for debugging.
        /// </summary>
        /// <returns> String representation of TechInfo for debugging. </returns>
        internal new string ToString()
        {
            return IP + '\t' + ComputerName + '\t' + MacAdress;
        }

    }
}
