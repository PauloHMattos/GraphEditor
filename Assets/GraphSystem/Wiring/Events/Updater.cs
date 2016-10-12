using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Events")]
    [AddComponentMenu("Klak/Wiring/Event/Updater")]
    public class Updater : EventNode
    {
        #region Node I/O

        [SerializeField, Outlet]
        VoidEvent _onUpdateEvent = new VoidEvent();

        [SerializeField, Outlet]
        VoidEvent _onLateUpdateEvent = new VoidEvent();

        [SerializeField, Outlet]
        VoidEvent _onFixedUpdateEvent = new VoidEvent();

        #endregion

        #region MonoBehaviour functions

        void Update()
        {
            if(isNodeActive)
                _onUpdateEvent.Invoke();
        }

        void LateUpdate()
        {
            if(isNodeActive)
                _onLateUpdateEvent.Invoke();
        }

        void FixedUpdate()
        {
            if(isNodeActive)
                _onFixedUpdateEvent.Invoke();
        }

        #endregion
    }
}