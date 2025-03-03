using Speedsters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMover : MonoBehaviour
{
    [SerializeField] List<Transform> _checkPoints;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] float _speed = 6f;


    int _checkPointIndex = 0;
    private void Update()
    {
        if(_checkPoints == null)
        {
            return;
        }
        var distanceToCheckPoint = Vector3.Distance(_checkPoints[_checkPointIndex].position, transform.position);
        if (distanceToCheckPoint < 1f)
        {
            UpdateToNextCheckPoint();
        }
        else
        {
            MoveToCheckPoint();
        }
    }
    void UpdateToNextCheckPoint()
    {
        _checkPointIndex++;
        _checkPointIndex %= _checkPoints.Count;
    }
    void MoveToCheckPoint()
    {
        _agent.MoveToSpeedster(_checkPoints[_checkPointIndex].position);
    }

}
