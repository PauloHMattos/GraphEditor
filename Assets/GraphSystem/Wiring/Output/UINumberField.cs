using System;
using UnityEngine;
using UnityEngine.UI;

namespace Klak.Wiring
{
    [NodeType("Output", "Component", "UI", "Number Field")]
    [AddComponentMenu("Klak/Wiring/Output/UI/Number Field")]
    public class UINumberField : OutputNode
    {
        public Text field;
        public Behaviour behaviour;
        [SerializeField] private float _value;

        public enum Behaviour
        {
            Increment, Decrement, Multiply, Divide
        }

        [Inlet]
        public float value
        {
            set { _value = value; }
        }

        public override bool ShowTriggerInlet
        {
            get { return true; }
        }

        protected override void InvokeEvents()
        {
            base.InvokeEvents();
            float currentValue;
            if (float.TryParse(field.text, out currentValue))
            {
                switch (behaviour)
                {
                    case Behaviour.Increment:
                        currentValue += _value;
                        break;
                    case Behaviour.Decrement:
                        currentValue -= _value;
                        break;
                    case Behaviour.Multiply:
                        currentValue *= _value;
                        break;
                    case Behaviour.Divide:
                        currentValue /= _value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                field.text = currentValue.ToString();
            }
        }
    }
}