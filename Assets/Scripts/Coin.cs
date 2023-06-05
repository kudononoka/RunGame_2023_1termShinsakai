using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : LaneObjectBase
{
    public override void Action()
    {
        GameManager.Instance.PointPlus();
    }
}
