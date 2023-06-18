using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class FadeSceneChangeManager : SingletonMonoBehaviour<FadeSceneChangeManager>
{
    private static CanvasGroup fadeCanvasGroup;
    private static GameObject fadeCanvas = default;
    [SerializeField, Header("Fade�Ɏg��CanvasPrafab")] GameObject _fadeObject;
    [SerializeField, Header("�t�F�[�h�ɂ����鎞��")] float _fadeiotime;
    [SerializeField, Header("�t�F�[�h���I����Ď��̃V�[���ɑJ�ڂ���܂ł̎���")] float _fadewaittime;
    private Sequence sequence;
    private void Awake()
    {
        StartCoroutine(AwakeSet());
    }
    public void ChangeSceneFade(string nextscenename)
    {

        fadeCanvas.GetComponent<Canvas>().sortingOrder = 20;
        var operation = SceneManager.LoadSceneAsync(nextscenename); 
        operation.allowSceneActivation = false;
        sequence = DOTween.Sequence();

        sequence.Append(fadeCanvasGroup.DOFade(1f, _fadeiotime)) 
                .AppendCallback(() =>{sequence.Pause(); operation.allowSceneActivation = true;})
                .AppendInterval(_fadewaittime)                 
                .Append(fadeCanvasGroup.DOFade(0f, _fadeiotime));
   

        sequence.Play();
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        sequence.Play();
    }

    IEnumerator AwakeSet()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        if (fadeCanvas == null)
        {
            fadeCanvas = Instantiate(_fadeObject);
            DontDestroyOnLoad(fadeCanvas);
        }
        yield return new WaitForSeconds(1);
        fadeCanvasGroup = fadeCanvas.GetComponent<CanvasGroup>();
        fadeCanvas.GetComponent<Canvas>().sortingOrder = -1;
    }
}
