using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private int _point = 0;
    private int _score = 0;
    [SerializeField] string _gameOverSceneName;
    /// <summary>���J�ڂ���V�[����</summary>
    private string _changeSceneName = "";
    /// <summary>���݂̃Q�[�����</summary>
    GameState gameState = GameState.None;
    /// <summary>���݂̃Q�[�����</summary>
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
    /// <summary>Point���v���X�P���郁�\�b�h</summary>
    public void PointPlus()
    {
        _point += 1;
    }
    /// <summary>�Q�[����ԊǗ�����enum</summary>
    public enum GameState
    {
        None,
        Game,
        GameClear,
        GameOver,
    }
    /// <summary> Point/Score�����Z�b�g���郁�\�b�h</summary>
    public void PointScoreReset()
    {
        _score = 0;
        _point = 0;
    }
    /// <summary>���V�[���J�ڂ���V�[�����̍X�V</summary>
    /// <param name="sceneName">���J�ڂ���V�[����</param>
    public void ChangeSceneNameLoad(string sceneName)
    {
        _changeSceneName = sceneName;
    }
    /// <summary>�V�[���J�ڃ��\�b�h</summary>
    public void SceneChange()
    {
        SceneManager.LoadScene(_changeSceneName);
    }
}
