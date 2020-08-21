using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Navigation;

/// <summary>
/// @Author Daniel Dovgun
/// @Version 15/6/2020
/// </summary>
namespace BeaconManager.Beacon.Hitches
{
    /// <summary>
    /// Represents a hitch of the PC station.
    /// </summary>
    internal class Hitch
    {
        /// <summary>
        /// List of all possible station`s hitches.
        /// </summary>
        internal enum HitchType
        {
            None,
            Domain,
            AD_DC,
            IPConflict,
            NameConflict,
            Screen,
            Keyboard,
            Mouse,
            ImageReinstallation,
            Other      
        };

        internal List<HitchType> Hitches { get; set; }
        internal string Description { get; set; }
        internal bool RequireUser { get; private set; }


        /// <summary>
        /// PC station`s hitch defualt constructor (Call when there are no hitches).
        /// </summary>
        internal Hitch()
        {
            Hitches = new List<HitchType> { HitchType.None };
            Description = "";
            RequireUser = false;
        }
        /// <summary>
        /// PC station`s hitch constructor.
        /// </summary>
        /// <param name="stationHitch"></param>
        /// <param name="description"></param>
        internal Hitch(List<HitchType> stationHitches, string description)
        {
            if (string.IsNullOrEmpty(Description))
                Description = "";

            foreach (var hitch in stationHitches)
            {
                if (hitch != HitchType.None)
                    Hitches.Add(hitch);   
            }

            RequireUser = CheckUserRequirement(stationHitches);
        }


        /// <summary>
        /// Adds a specific hitch to the PC station hitch list. 
        /// Returns fasle value if the input hitch already exists in the hitch list.
        /// </summary>
        /// <param name="newHitch"> Type of hitch we wish to add to the hitch list. </param>
        /// <returns> Was the operation successfull. </returns>
        internal bool AddHitch(HitchType newHitch)
        {
            foreach (var hitch in Hitches)
            {
                if (newHitch == hitch)
                    return false;
            }
            Hitches.Add(newHitch);
            return true;
        }
        /// <summary>
        /// Removes a specific hitch from the PC station hitch list.
        /// Throws false value if the input was HitchType.None.
        /// </summary>
        /// <param name="hitchName"> Type of the hitch we wish to remove from the hitch list. </param>
        /// <returns> Was the operation successfull. </returns>
        internal bool RemoveHitch(HitchType hitchName)
        {
            if (hitchName == HitchType.None)
                return false;
            else
            {
                Hitches.Remove(hitchName);
                return true;
            }
        }


        /// <summary>
        /// Checks if the given hitch require an administrator user to solve.
        /// </summary>
        /// <param name="stationHitch"> Single type. </param>
        /// <returns> Is the given hitch require an administrator user to solve. </returns>
        private bool CheckUserRequirement(HitchType hitch)
        {
            if (RequireUser == true)
                return true;
            else
            {
                if (hitch == HitchType.Domain || hitch == HitchType.AD_DC ||
                   hitch == HitchType.IPConflict || hitch == HitchType.NameConflict)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Checks if the given hitches require an administrator user to solve.
        /// </summary>
        /// <param name="stationHitches"> List of hitches. </param>
        /// <returns>Is the given hitches require an administrator user to solve.</returns>
        private bool CheckUserRequirement(List<HitchType> stationHitches)
        {
            foreach (var hitch in stationHitches)
            {
                if(hitch == HitchType.Domain || hitch == HitchType.AD_DC ||
                   hitch == HitchType.IPConflict || hitch == HitchType.NameConflict)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
