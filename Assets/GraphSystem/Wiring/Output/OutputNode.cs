using UnityEngine;

namespace Klak.Wiring
{
    public class OutputNode : NodeBase
    {
        public override Color BackgroundNodeColor
        {
            get { return Color.grey; }
        }

        public override bool ShowTriggerInlet
        {
            get { return false; }
        }
        public override bool UseAutomaticTrigger
        {
            get { return false; }
        }
    }
}