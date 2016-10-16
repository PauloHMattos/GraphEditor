using System;
using Klak.Wiring;
using UnityEngine;

namespace Assets.GraphSystem.Wiring.Flow
{
    public class ComparisonNode<T> : NodeBase where T : IComparable<T>
    {
        [SerializeField]
        protected ComparisonType _comparison;

        [SerializeField]
        protected T _value1;
        [SerializeField]
        protected T _value2;
        [SerializeField]

        [Inlet]
        public T Value1
        {
            set
            {
                _value1 = value;
            }
        }

        [Inlet]
        public T Value2
        {
            set
            {
                _value2 = value;
            }
        }
        
        [Outlet, SerializeField]
        protected BoolEvent _result = new BoolEvent();

        protected override void InvokeEvents()
        {
            _result.Invoke(Compare());
        }

        protected virtual bool Compare()
        {
            switch (_comparison)
            {
                case ComparisonType.Equal:
                    return Equal(_value1, _value2);

                case ComparisonType.NotEqual:
                    return !Equal(_value1, _value2);
            }

            var r = _value1.CompareTo(_value2);
            switch (_comparison)
            {
                case ComparisonType.Greater:
                    return r > 0;

                case ComparisonType.GreaterEqual:
                    return r >= 0;

                case ComparisonType.Lesser:
                    return r < 0;

                case ComparisonType.LesserEqual:
                    return r <= 0;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual bool Equal(T value1, T value2)
        {
            return _value1.CompareTo(_value2) == 0;
        }

        [Flags]
        public enum ComparisonType
        {
            Equal,
            NotEqual,
            Greater,
            GreaterEqual,
            Lesser,
            LesserEqual
        }
    }
}