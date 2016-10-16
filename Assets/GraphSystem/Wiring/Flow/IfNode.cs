using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klak.Wiring;
using UnityEngine;

namespace Assets.GraphSystem.Wiring.Flow
{
    [AddComponentMenu("Klak/Wiring/Comparison/If Node")]
    public class IfNode : NodeBase
    {
        [SerializeField] private bool _condition;

        [Inlet]
        public bool Condition
        {
            set { _condition = value; }
        }

        [Outlet, SerializeField] private VoidEvent _trueEvent = new VoidEvent();
        private VoidEvent _falseEvent = new VoidEvent();

        protected override void InvokeEvents()
        {
            if (_condition)
            {
                _trueEvent.Invoke();
            }
            else
            {
                _falseEvent.Invoke();
            }
        }
    }
}
