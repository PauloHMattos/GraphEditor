using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Events", "Physics", "2D")]
    [AddComponentMenu("Klak/Wiring/Event/Collision2D")]
    public class OnCollision2D : EventNode
    {
        #region Node I/O

        [SerializeField, Outlet]
        Collision2DEvent _onCollisionEnter = new Collision2DEvent();
        [SerializeField, Outlet]
        Collision2DEvent _onCollisionStay = new Collision2DEvent();
        [SerializeField, Outlet]
        Collision2DEvent _onCollisionExit = new Collision2DEvent();

        #endregion

        #region MonoBehaviour functions

        protected override void Awake()
        {
            triggerMechanism = 0;
            base.Awake();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(isNodeActive)
                _onCollisionEnter.Invoke(collision);
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if(isNodeActive)
                _onCollisionStay.Invoke(collision);
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if(isNodeActive)
                _onCollisionExit.Invoke(collision);
        }

        #endregion
    }
}