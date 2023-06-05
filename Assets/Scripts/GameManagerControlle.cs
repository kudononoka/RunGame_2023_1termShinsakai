using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerControlle : MonoBehaviour
{
    [SerializeField, Header("���J�ڂ���V�[����")] string _nextSceneName;
    [SerializeField, Header("���݂̃Q�[�����")] GameManager.GameState _nowGameState;
    GameManager _gameManager = null;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.ChangeSceneNameLoad(_nextSceneName);
        if(_nowGameState == GameManager.GameState.None)
        {
            _gameManager.PointScoreReset();
        }
        _gameManager.GamedState = _nowGameState;
    }
}
