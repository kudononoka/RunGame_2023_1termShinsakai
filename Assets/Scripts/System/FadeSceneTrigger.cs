using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSceneTrigger : MonoBehaviour
{
    public void Trigger(int num = 0)
    {
        string nextSceneName = GameManager.Instance.ChangeSceneName;
        if (GameManager.Instance.GamedState == GameManager.GameState.None && num == 1)
        {
            nextSceneName = "GameScene";
            GameManager.Instance.PointScoreReset();
        }
        FadeSceneChangeManager.Instance.ChangeSceneFade(nextSceneName);
    }
}
