using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPlus : LaneObjectBase
{
    [SerializeField, Header("�G�l���M�[�񕜒l")] float _recoveryNum;
    public override void Action()
    {
        FindObjectOfType<Drone>().EnergyRecovery(_recoveryNum);
    }
}
