using Speedsters;
using System.Collections.Generic;
using UnityEngine;

public class SpeedsterObjectController : MonoBehaviour
{
    [Header("Objects")]
    [Tooltip("This is object that will turn on and off with the speed up")]
    [SerializeField] List<GameObject> _objectsToControll;

    const string _className = nameof(SpeedsterObjectController);
    void Start()
    {
        SpeedsterManager.OnSpeedUpStart += OnSpeedUpStart;
        SpeedsterManager.OnSpeedUpEnd += OnSpeedUpStop;
        ToggleObjects(false);
    }
    void OnSpeedUpStart()
    {
        ToggleObjects(true);
    }
    void OnSpeedUpStop()
    {
        ToggleObjects(false);
    }
    void ToggleObjects(bool state)
    {
        foreach (var obj in _objectsToControll)
        {
            obj.SetActive(state);
        }
    }
    private void OnDestroy()
    {
        SpeedsterManager.OnSpeedUpStart -= OnSpeedUpStart;
        SpeedsterManager.OnSpeedUpEnd -= OnSpeedUpStop;

    }

}
