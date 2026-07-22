using UnityEngine;
using Syste.Collections.Generic;

public class Fox : MonoBehaviour
{
    List<FoxUnlockPosition> areaPositions = new List<FoxUnlockPosition>();

    private Dictionary<string, Vector3> _areaPositions =
        new Dictionary<string, Vector3>();

    void Awake() {
        EvtSystem.EventDispatcher.AddListener<PropagateFlag>(HandleFlag);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (FoxUnlockPosition pair in areaPositions) {
            _areaPositions[pair.areaName] = pair.position.position;
        }
    }

    void HandleFlag(PropagateFlag evt) {
        if (evt.flag == "Bar_Unlocked") {
            gameObject.transform.position = _areaPositions["Bar"];
        } else if (evt.flag == "Temple_Unlocked") {
            gameObject.transform.position = _areaPositions["Temple"];
        } else if (evt.flag == "Palace_Unlocked") {
            gameObject.transform.position = _areaPositions["Palace"];
        }
    }

    void OnDestroy() {
        EvtSystem.EventDispatcher.RemoveListener<PropagateFlag>(HandleFlag);
    }
}
