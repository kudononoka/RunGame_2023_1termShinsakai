using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerControlle : MonoBehaviour
{
    [SerializeField, Header("次遷移するシーン名")] string _nextSceneName;
    [SerializeField, Header("現在のゲーム状態")] GameManager.GameState _nowGameState;
    GameManager _gameManager = null;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.ChangeSceneNameLoad(_nextSceneName);
        if(_nowGameState == GameManager.GameState.Title)
        {
            _gameManager.PointScoreReset();
        }
        _gameManager.GamedState = _nowGameState;
    }
}
