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

        protected override void Update()
        {
            base.Update();
            if(isNodeActive)
                _onUpdateEvent.Invoke();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (isNodeActive)
                _onLateUpdateEvent.Invoke();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (isNodeActive)
                _onFixedUpdateEvent.Invoke();
        }

        #endregion
    }
}