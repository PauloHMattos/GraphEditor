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

        [Outlet]
        public IntEvent IntegerOutput;

        [Outlet]
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
            switch (RoundMethod)
            {
                case RoundMethodTypes.Round:
                    _outputInt = Mathf.RoundToInt(_outputFloat);
                    break;
                case RoundMethodTypes.Trunc:
                    _outputInt = (int)_outputFloat;
                    break;
                case RoundMethodTypes.Ceil:
                    _outputInt = Mathf.CeilToInt(_outputFloat);
                    break;
                case RoundMethodTypes.Floor:
                    _outputInt = Mathf.FloorToInt(_outputFloat);
                    break;
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
