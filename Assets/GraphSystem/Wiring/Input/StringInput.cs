using UnityEngine;

namespace Klak.Wiring
{
    [NodeType("Input", "String")]
    [AddComponentMenu("Klak/Wiring/Input/String Input")]
    public class StringInput : InputNode
    {

        #region Editable properties

        [SerializeField]
        private string _value = "";
        
        #endregion

        #region Node I/O

        [SerializeField, Outlet]
        StringEvent _valueEvent = new StringEvent();

        #endregion

        protected override void InvokeEvents()
        {
            _valueEvent.Invoke(_value);
        }
    }
}