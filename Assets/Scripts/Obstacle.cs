using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : LaneObjectBase
{
    public override void Action()
    {
        GameManager.Instance.GamedState = GameManager.GameState.GameOver;
    }
}
