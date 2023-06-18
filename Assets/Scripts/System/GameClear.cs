using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : LaneObjectBase
{
    public override void Action()
    {
        FindObjectOfType<Player>().GetComponent<Rigidbody>().isKinematic = true;

        FindObjectOfType<FadeSceneTrigger>().Trigger();
    }
}
