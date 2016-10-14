using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Input", "Vector3")]
    [AddComponentMenu("Klak/Wiring/Input/Vector")]
    public class Vector3Creator : InputNode
    {
        #region Node I/O
        
        [Inlet]
        public float x
        {
            set
            {
                if (!enabled) return;
                _x = value;
                //InvokeEvent();
            }
        }
        [Inlet]
        public float y
        {
            set
            {
                if (!enabled)
                    return;
                _y = value;
                //InvokeEvent();
            }
        }
        [Inlet]
        public float z
        {
            set
            {
                if (!enabled)
                    return;
                _z = value;
                //InvokeEvent();
            }
        }
        
        [SerializeField, Outlet]
        Vector3Event _outputEvent = new Vector3Event();

        #endregion

        #region Private members

        [SerializeField] float _x,_y,_z;

        protected override void InvokeEvents()
        {
            base.InvokeEvents();
            _outputEvent.Invoke(new Vector3(_x, _y, _z));
        }
        
        #endregion
    }
}