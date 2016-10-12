using System;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Klak.Wiring
{
    [NodeType("Events", "Timer")]
    [AddComponentMenu("Klak/Wiring/Event/Timer")]
    public class Timer : EventNode
    {
        private Stopwatch _stopwatch;
        public CicleType cicleType;
        public bool random;
        public int min;
        public int max;

        [SerializeField] private int _cicleTime;

        [Inlet]
        public int CicleTime
        {
            set
            {
                _cicleTime = value;
            }
        }

        [SerializeField, Outlet] VoidEvent _voidEvent = new VoidEvent();

        protected override void Start()
        {
            base.Start();
            CalculateTime();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        private void CalculateTime()
        {
            if (random)
            {
                CicleTime = Random.Range(min, max);
            }
        }

        protected override void Update()
        {
            base.Update();
            if(GetElapsed() > _cicleTime)
                ManualTrigger();
        }

        protected override void InvokeEvents()
        {
            CalculateTime();
            base.InvokeEvents();
            _stopwatch.Reset();
            _stopwatch.Start();
            _voidEvent.Invoke();
        }

        public int GetElapsed()
        {
            switch (cicleType)
            {
                case CicleType.Milliseconds:
                    return (int)_stopwatch.ElapsedMilliseconds;

                case CicleType.Seconds:
                    return _stopwatch.Elapsed.Seconds;

                case CicleType.Minutes:
                    return _stopwatch.Elapsed.Minutes;

                case CicleType.Hours:
                    return _stopwatch.Elapsed.Hours;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public enum CicleType
        {
            Milliseconds,
            Seconds,
            Minutes,
            Hours
        }
    }
}