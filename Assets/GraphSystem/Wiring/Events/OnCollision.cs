using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Events", "Physics", "3D")]
    [AddComponentMenu("Klak/Wiring/Event/Collision")]
    public class OnCollision : EventNode
    {
        #region Node I/O

        [SerializeField, Outlet]
        CollisionEvent _onCollisionEnter = new CollisionEvent();
        [SerializeField, Outlet]
        CollisionEvent _onCollisionStay = new CollisionEvent();
        [SerializeField, Outlet]
        CollisionEvent _onCollisionExit = new CollisionEvent();

        #endregion

        #region MonoBehaviour functions

        void OnCollisionEnter(Collision collision)
        {
            if(isNodeActive)
                _onCollisionEnter.Invoke(collision);
        }

        void OnCollisionStay(Collision collision)
        {
            if(isNodeActive)
                _onCollisionStay.Invoke(collision);
        }

        void OnCollisionExit(Collision collision)
        {
            if(isNodeActive)
                _onCollisionExit.Invoke(collision);
        }

        #endregion
    }
}