using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Events")]
    [AddComponentMenu("Klak/Wiring/Event/Enabler")]
    public class Enabler : EventNode
    {
        #region Node I/O

        [SerializeField, Outlet]
        VoidEvent _onEnableEvent = new VoidEvent();
        [SerializeField, Outlet]
        VoidEvent _onDisableEvent = new VoidEvent();

        #endregion

        #region MonoBehaviour functions

        protected override void OnEnable()
        {
            _onEnableEvent.Invoke();
        }
        void OnDisabe()
        {
            _onDisableEvent.Invoke();
        }

        #endregion
    }
}