using UnityEngine;

namespace Klak.Wiring
{
    public class DecomposerNode : NodeBase
    {
        public override Color BackgroundNodeColor
        {
            get { return Color.green; }
        }

        public override bool ShowTriggerInlet
        {
            get { return false; }
        }
    }
}