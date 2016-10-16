using UnityEngine;

namespace Klak.Wiring
{
    public class EventNode : NodeBase
    {
        public override Color BackgroundNodeColor
        {
            get { return Color.cyan; }
        }

        public override bool ShowTriggerInlet
        {
            get { return false; }
        }

        public override bool UseAutomaticTrigger
        {
            get { return false; }
        }

        protected override void Awake()
        {
            triggerMechanism = 0;
            base.Awake();
        }
    }
}