using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private int _point = 0;
    private int _score = 0;
    [SerializeField, Header("カウントダウン")] float _count;
    [SerializeField] string _gameOverSceneName;
    /// <summary>次遷移するシーン名</summary>
    private string _changeSceneName = "";
    /// <summary>現在のゲーム状態</summary>
    GameState gameState = GameState.None;
    /// <summary>現在のゲーム状態</summary>
    public GameState GamedState { get { return gameState; } set { gameState = value; } }
    public string ChangeSceneName { get { return _changeSceneName; } set { _changeSceneName = value; } }
    public int Score { get { return _score; } set { _score = value; } }
    public float Count { get { return _count; }}
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            if(_count == 4)
            {
                FindObjectOfType<Player>().motionState = Player.MotionState.None;
            }
            _count -= Time.deltaTime;
            if(_count < 0)
            {
                FindObjectOfType<Player>().motionState = Player.MotionState.Run;
            }
        }
        switch(gameState)
        {
            case GameState.GameOver:
                _changeSceneName = _gameOverSceneName;
                break;
        }
    }
    /// <summary>Pointをプラス１するメソッド</summary>
    public void PointPlus()
    {
        _point += 1;
    }

    public void ScorePlus(int score)
    {
        _score += score;
    }
    /// <summary>ゲーム状態管理するenum</summary>
    public enum GameState
    {
        Title,
        Game,
        GameClear,
        GameOver,
        None,
    }
    /// <summary> Point/Scoreをリセットするメソッド</summary>
    public void PointScoreReset()
    {
        _score = 0;
        _point = 0;
        _count = 4;
    }
    /// <summary>次シーン遷移するシーン名の更新</summary>
    /// <param name="sceneName">次遷移するシーン名</param>
    public void ChangeSceneNameLoad(string sceneName)
    {
        _changeSceneName = sceneName;
    }
    
}
