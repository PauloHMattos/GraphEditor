using System;
using Klak.Wiring;
using UnityEngine;
using B83.ExpressionParser;

namespace Assets.Klak.Wiring
{
    [NodeType("MathParser")]
    [AddComponentMenu("Klak/Wiring/MathParser")]
    public class MathParser : NodeBase
    {
        [Multiline(5)]
        [SerializeField] private string _text;
        public RoundMethodTypes RoundMethod;

        private ExpressionParser _parser = new ExpressionParser();
        private int _outputInt;
        private float _outputFloat;

        [SerializeField, Outlet]
        public IntEvent IntegerOutput;

        [SerializeField, Outlet]
        public FloatEvent FloatOutput;

        [Inlet]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
            }
        }

        protected override void InvokeEvents()
        {
            Evaluate();
            FloatOutput.Invoke(_outputFloat);
            IntegerOutput.Invoke(_outputInt);
            Debug.Log(_outputFloat);
            Debug.Log(_outputInt);
        }

        private void Evaluate()
        {
            _outputFloat = (float)_parser.Evaluate(_text);
            _outputInt = Round(_outputFloat, RoundMethod);
        }

        public static int Round(float value, RoundMethodTypes roundMethod)
        {
            switch (roundMethod)
            {
                case RoundMethodTypes.Round:
                    return Mathf.RoundToInt(value);
                case RoundMethodTypes.Trunc:
                    return (int)value;
                case RoundMethodTypes.Ceil:
                    return Mathf.CeilToInt(value);
                case RoundMethodTypes.Floor:
                    return Mathf.FloorToInt(value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public enum RoundMethodTypes
        {
            Round,
            Trunc,
            Ceil,
            Floor
        }
    }
}
