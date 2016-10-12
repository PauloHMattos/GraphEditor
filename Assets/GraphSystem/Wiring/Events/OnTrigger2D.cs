using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Events", "Physics", "2D")]
    [AddComponentMenu("Klak/Wiring/Event/Trigger2D")]
    public class OnTrigger2D : EventNode
    {
        #region Node I/O

        [SerializeField, Outlet]
        Collider2DEvent _onTriggerEnter = new Collider2DEvent();
        [SerializeField, Outlet]
        Collider2DEvent _onTriggerStay = new Collider2DEvent();
        [SerializeField, Outlet]
        Collider2DEvent _onTriggerExit = new Collider2DEvent();

        #endregion

        #region MonoBehaviour functions

        protected override void Awake()
        {
            triggerMechanism = 0;
            base.Awake();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (isNodeActive)
                _onTriggerEnter.Invoke(other);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (isNodeActive)
                _onTriggerStay.Invoke(other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (isNodeActive)
                _onTriggerExit.Invoke(other);
        }

        #endregion
    }
}