using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private int _point = 0;
    private int _score = 0;
    [SerializeField] string _gameOverSceneName;
    /// <summary>次遷移するシーン名</summary>
    private string _changeSceneName = "";
    /// <summary>現在のゲーム状態</summary>
    GameState gameState = GameState.None;
    /// <summary>現在のゲーム状態</summary>
    public GameState GamedState { get { return gameState; } set { gameState = value; } }

    private void Update()
    {
        switch(gameState)
        {
            case GameState.GameOver:
                _changeSceneName = _gameOverSceneName;
                SceneChange();
                break;
        }
    }
    /// <summary>Pointをプラス１するメソッド</summary>
    public void PointPlus()
    {
        _point += 1;
    }
    /// <summary>ゲーム状態管理するenum</summary>
    public enum GameState
    {
        None,
        Game,
        GameClear,
        GameOver,
    }
    /// <summary> Point/Scoreをリセットするメソッド</summary>
    public void PointScoreReset()
    {
        _score = 0;
        _point = 0;
    }
    /// <summary>次シーン遷移するシーン名の更新</summary>
    /// <param name="sceneName">次遷移するシーン名</param>
    public void ChangeSceneNameLoad(string sceneName)
    {
        _changeSceneName = sceneName;
    }
    /// <summary>シーン遷移メソッド</summary>
    public void SceneChange()
    {
        SceneManager.LoadScene(_changeSceneName);
    }
}
