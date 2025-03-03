using System;
using System.Collections.Generic;
using UnityEngine;

namespace Speedsters
{
    public class SpeedsterManager : MonoBehaviour
    {
        [SerializeField] float _speedsterSpeed = 100;
        bool _isInSpeedMode = false;
        Dictionary<int, float> _agentsSpeedsDict = new();

        public static Action OnSpeedUpStart;
        public static Action OnSpeedUpEnd;
        public float GetSpeedsterSpeed()
        {
            return _speedsterSpeed;
        }
        public float GetAgentSpeed(int instanceId)
        {
            var speed = GetOriginalSpeedOfAgent(instanceId);
            if (IsInSpeedMode())
            {
                return speed / _speedsterSpeed;
            }
            else
            {
                return speed;
            }

        }
        public bool IsInSpeedMode()
        {
            return _isInSpeedMode;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                _isInSpeedMode = !_isInSpeedMode;
                if (_isInSpeedMode)
                {
                    OnSpeedUpStart?.Invoke();
                }
                else
                {
                    OnSpeedUpEnd?.Invoke();
                }
            }
        }
        internal void TryAddNewAgent(int agentId, float speed)
        {
            if (!_agentsSpeedsDict.ContainsKey(agentId))
            {
                _agentsSpeedsDict.Add(agentId, speed);

            }
        }
        public float GetOriginalSpeedOfAgent(int agentId)
        {
            if (_agentsSpeedsDict.ContainsKey(agentId))
            {
                return _agentsSpeedsDict[agentId];
            }
            else return 1;
        }
    }
}
