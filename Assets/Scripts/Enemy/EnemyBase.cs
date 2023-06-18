using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField, Header("Hp最大値")] int _moxHp;
    [SerializeField, Header("スコア加算")] int _scorePlusNum;
    [SerializeField]int _nowHp;
    protected bool _isDeath;

    private void Awake()
    {
        _nowHp = _moxHp;
    }
    public void Damage(int damage)
    {
        _nowHp -= damage;
        if( _nowHp <= 0 )
        {
            _isDeath = true;
            GameManager.Instance.ScorePlus(_scorePlusNum);
        }
    }
}
