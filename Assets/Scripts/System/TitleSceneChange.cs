using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneChange : MonoBehaviour
{
   public void SceneChange()
    {
        string nextSceneName = GameManager.Instance.ChangeSceneName;
        FadeSceneChangeManager.Instance.ChangeSceneFade(nextSceneName);
    }
}
