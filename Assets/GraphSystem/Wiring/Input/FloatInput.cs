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
        
        //[SerializeField]
        //FloatInterpolator.Config _interpolator = new FloatInterpolator.Config();

        #endregion

        #region Node I/O
        
        [SerializeField, Outlet]
        FloatEvent _valueEvent = new FloatEvent();

        #endregion
        
        #region MonoBehaviour functions

        //FloatInterpolator _floatValue;

        //protected override void Start()
        //{
        //    base.Start();
        //    _floatValue = new FloatInterpolator(0, _interpolator);
        //}

        protected override void InvokeEvents()
        {
            //_floatValue.targetValue = _value;
            //_valueEvent.Invoke(_floatValue.Step());
            _valueEvent.Invoke(_value);
        }

        #endregion
    }
}