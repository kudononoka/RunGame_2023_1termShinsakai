using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIManager : MonoBehaviour
{
    /// <summary>Score—pTextUI</summary>
    [SerializeField] Text _score;
    /// <summary>Count—pTextUI</summary>
    [SerializeField] Text _count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _score.text = $"Score : {GameManager.Instance.Score}";
        if(GameManager.Instance.Count <= 0)
        {
            _count.enabled = false;
        }
        else
        {
            _count.text = $"{GameManager.Instance.Count.ToString("f0")}";
        }
    }
}
