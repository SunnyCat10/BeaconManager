using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeaconManager.TrainingOutline;

namespace BeaconManager.Core
{
    internal class OutlineScroll
    {
        internal int ScrollIndex { get; private set; } = 0;
        internal int ScrollLimit { get; private set; }
        internal readonly Renderer renderer;
        
        public OutlineScroll(Renderer rend)
        {
            ScrollLimit = TrainingOutlineBuffer.Buffer.Count;
            renderer = rend;
        }

        public bool Next()
        {
            if ((ScrollIndex + 1) < ScrollLimit)
            {
                ++ScrollIndex;
                renderer.RenderOutline(TrainingOutlineBuffer.Buffer[ScrollIndex].IncludedStations);
                return true;
            }
            return false;
        }

        public bool Previous()
        {
            if ((ScrollIndex - 1) >= 0)
            {
                --ScrollIndex;
                renderer.RenderOutline(TrainingOutlineBuffer.Buffer[ScrollIndex].IncludedStations);
                return true;
            } 
            return false;
        }

        public void Lengthened()
        {
            ++ScrollLimit;
        }

        public void Shortened()
        {
            --ScrollLimit;
        }

        public string CurrentScrollOutline()
        {
            return TrainingOutlineBuffer.Buffer[ScrollIndex].Name;
        }
    }
}
