using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPlus : LaneObjectBase
{
    [SerializeField, Header("エネルギー回復値")] float _recoveryNum;
    public override void Action()
    {
        FindObjectOfType<Drone>().EnergyRecovery(_recoveryNum);
    }
}
