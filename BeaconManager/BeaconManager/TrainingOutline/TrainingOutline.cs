using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BeaconManager.TrainingOutline
{
    public class TrainingOutline : IEquatable<TrainingOutline>, IComparable<TrainingOutline>
    {
        public string Name { get; }
        public List<int> IncludedStations { get; }

        /// <summary>
        /// USE TrainingOutLineFactory TO CREATE NEW TRAINING OUTLINES INSTEAD OF THIS CONSTRUCTOR.
        /// </summary>
        /// <param name="name"> The name of the training outline. </param>
        /// <param name="includedStations"> The selection of the PC stations. </param>
        public TrainingOutline(string name, List<int> includedStations)
        {
            Name = name;
            IncludedStations = includedStations;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other"> Other training outline we wish to copy. </param>
        public TrainingOutline(TrainingOutline other)
        {
            Name = other.Name;
            IncludedStations = new List<int>(other.IncludedStations);
        }

        /// <summary>
        /// IEquatable definition. We only checking the name as each object should have a unique name.
        /// </summary>
        /// <param name="other"> Other TrainingOutline object we want to compare to. </param>
        /// <returns> Is the other TrainingOutline object equals to this one? </returns>
        public bool Equals(TrainingOutline other)
        {
            return (Name.Equals(other.Name));
        }

        /// <summary>
        /// IComparable definition. The comparision is alphabetically basedd.
        /// </summary>
        /// <param name="other"> Other TrainingOutline object we want to compare to. </param>
        /// <returns> Which TrainingOutline comes first based on the alphabet order. </returns>
        public int CompareTo(TrainingOutline other)
        {
            return Name.CompareTo(other);
        }
    }
}
