using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerControlle : MonoBehaviour
{
    [SerializeField, Header("Ÿ‘JˆÚ‚·‚éƒV[ƒ“–¼")] string _nextSceneName;
    [SerializeField, Header("Œ»İ‚ÌƒQ[ƒ€ó‘Ô")] GameManager.GameState _nowGameState;
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
