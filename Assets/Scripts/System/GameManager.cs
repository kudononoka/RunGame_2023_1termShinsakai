using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private int _point = 0;
    private int _score = 0;
    [SerializeField, Header("�J�E���g�_�E��")] float _count;
    [SerializeField] string _gameOverSceneName;
    /// <summary>���J�ڂ���V�[����</summary>
    private string _changeSceneName = "";
    /// <summary>���݂̃Q�[�����</summary>
    GameState gameState = GameState.None;
    /// <summary>���݂̃Q�[�����</summary>
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
    /// <summary>Point���v���X�P���郁�\�b�h</summary>
    public void PointPlus()
    {
        _point += 1;
    }

    public void ScorePlus(int score)
    {
        _score += score;
    }
    /// <summary>�Q�[����ԊǗ�����enum</summary>
    public enum GameState
    {
        Title,
        Game,
        GameClear,
        GameOver,
        None,
    }
    /// <summary> Point/Score�����Z�b�g���郁�\�b�h</summary>
    public void PointScoreReset()
    {
        _score = 0;
        _point = 0;
        _count = 4;
    }
    /// <summary>���V�[���J�ڂ���V�[�����̍X�V</summary>
    /// <param name="sceneName">���J�ڂ���V�[����</param>
    public void ChangeSceneNameLoad(string sceneName)
    {
        _changeSceneName = sceneName;
    }
    
}
