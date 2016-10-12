using UnityEngine;

namespace Klak.Wiring
{
    [AddComponentMenu("Klak/Wiring/Self/Transform")]
    public class TransformNode : NodeBase
    {
        #region Node I/O

        [Inlet]
        public void callEvent()
        {
            InvokeEvents();
        }

        [SerializeField, Outlet]
        TransformEvent _transformEvent = new TransformEvent();
        [SerializeField, Outlet]
        Vector3Event _positionEvent = new Vector3Event();

        #endregion


        #region Private functions

        void InvokeEvents()
        {
            _transformEvent.Invoke(transform);
            _positionEvent.Invoke(transform.position);
        }

        #endregion
    }
}