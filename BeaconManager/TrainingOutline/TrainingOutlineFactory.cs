using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconManager.TrainingOutline
{
    public static class TrainingOutlineFactory
    {
        public const string DEFAULT_OUTLINE_NAME = "מתאר כללי";
        private static readonly List<int> DEFAULT_OUTLINE_STATIONS =
            new List<int> { 1, 2, 3, 4, 5, 6, 7 ,8 , 9, 10,
        11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36};

        /// <summary>
        /// Creates a new training outline. 
        /// </summary>
        /// <param name="name"> The name of the training outline. </param>
        /// <param name="includedStations"> The selection of the PC stations. </param>
        /// <returns> The created training outline. </returns>
        public static TrainingOutline Create(string name, List<int> includedStations)
        {
            return new TrainingOutline(name, includedStations);
        }

        /// <summary>
        /// Creates a default training outline that is always located in the buffer, even if all the other outlines are deleted.
        /// This outline can not be deleted by the User. The default outline is instantiated on xml deserialization. 
        /// </summary>
        /// <returns> Default training outline - which include all the PC stations. </returns>
        public static TrainingOutline CreateDefault()
        {
            return Create(DEFAULT_OUTLINE_NAME, DEFAULT_OUTLINE_STATIONS);
        }
        
    }
}
