using Klak.Math;
using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Input", "Float")]
    [AddComponentMenu("Klak/Wiring/Input/Float Input")]
    public class FloatInput : InputNode
    {
        
        #region Editable properties
        
        [SerializeField]
        float _value = 0.0f;
        
        [SerializeField]
        FloatInterpolator.Config _interpolator;

        #endregion

        #region Node I/O
        
        [SerializeField, Outlet]
        FloatEvent _valueEvent = new FloatEvent();

        #endregion


        [Inlet]
        public void Trigger()
        {
            _floatValue.targetValue = _value;
            _valueEvent.Invoke(_floatValue.Step());
        }

        #region MonoBehaviour functions

        FloatInterpolator _floatValue;

        void Start()
        {
            _floatValue = new FloatInterpolator(0, _interpolator);
        }

        //void Update()
        //{
        //    _floatValue.targetValue = _value;
        //    _valueEvent.Invoke(_floatValue.Step());
        //}

        #endregion
    }
}