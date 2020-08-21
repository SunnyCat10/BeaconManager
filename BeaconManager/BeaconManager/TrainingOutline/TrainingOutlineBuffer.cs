using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconManager.TrainingOutline
{
    public static class TrainingOutlineBuffer
    {
        public static List<TrainingOutline> Buffer { get; private set; }

        /// <summary>
        /// Set up a training outline buffer and deserialize data from xml into it.
        /// Should run at program start.
        /// </summary>
        public static void Load()
        {
            Buffer = TrainingBufferSerialization.Deserialize();
        }

        /// <summary>
        /// Adds a new training outline. Both to the buffer and the XML file.
        /// </summary>
        /// <param name="trainingOutline"> The new training outline we wish to add. </param>
        /// <returns> false if there was a name conflict </returns>
        public static bool Add(TrainingOutline trainingOutline)
        {
            if (!CheckNameConflict(trainingOutline.Name))
            {
                TrainingBufferSerialization.SerializeOutline(trainingOutline);
                Buffer.Add(new TrainingOutline(trainingOutline));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a specific training outline by name.
        /// </summary>
        /// <param name="target"> The name of the outline that should be removed. </param>
        /// <returns> True if the target was deleted, false if the target is the default outline which can not be deleted. </returns>
        public static bool Remove(string target)
        {
            if (!CheckDefault(target)) // The default training preset is hardcoded and can not be delted by the User.
            {
                TrainingBufferSerialization.RemoveOutline(target);
                TrainingOutline removedOutline = Buffer.Single(outline => outline.Name == target);
                Buffer.Remove(removedOutline);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks for name conflict inside the buffer.
        /// </summary>
        /// <param name="name"> The name we want to check for name conflict. </param>
        /// <returns> Is there are a name conflict in the buffer? </returns>
        private static bool CheckNameConflict(string name)
        {
            foreach (var outline in Buffer)
            {
                if (outline.Name.Equals(name))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the name is the default training outline preset.
        /// </summary>
        /// <param name="name"> outline we want to compare. </param>
        /// <returns> Is the outline`s name equals to the defauly training outline. </returns>
        private static bool CheckDefault(string name)
        {
            return (name.Equals(TrainingOutlineFactory.DEFAULT_OUTLINE_NAME));
        }
    }
}
