using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>��Q���p�X�N���v�g</summary>
public class Obstacle : LaneObjectBase
{
    public override void Action()
    {
        StartCoroutine(GameOverActive());
    }
    IEnumerator GameOverActive()
    {
        GameManager.Instance.GamedState = GameManager.GameState.GameOver;
        yield return new WaitForSeconds(0.01f);
        FindObjectOfType<FadeSceneTrigger>().Trigger();
    }
}
