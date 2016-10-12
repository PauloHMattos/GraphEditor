namespace Klak.Wiring
{
    public class EventNode : NodeBase
    {
        public override bool ShowTriggerInlet
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