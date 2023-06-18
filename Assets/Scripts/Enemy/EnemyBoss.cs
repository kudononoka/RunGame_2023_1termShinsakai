using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    Animator _anim;
    AudioSource _audioSource;
    [Header("Audio")]
    [SerializeField, Header("飛行状態の効果音")] AudioClip _fiyAudio;
    [SerializeField, Header("墜落した時の効果音")] AudioClip _deathAudio;
    bool _isAudioPlay = true;
    [SerializeField, Header("rayをとばす場所")] Transform[] _rayOriginPos;
    [SerializeField, Header("着弾した時のエフェクト")] ParticleSystem[] _effects;
    [SerializeField, Header("地面となるオブジェクトのレイヤー")] LayerMask _layerMask;
    [SerializeField, Header("銃撃時間")] float _attackTime;
    float _attackTimer = 0;
    [SerializeField, Header("rayが当たった時の時間")] float _rayHitTime;
    [SerializeField] EnemyBossBulletEffect[] _bulletEffect;
    [SerializeField, Header("スポットライト")] GameObject[] _spotLights;
    float _rayHitTimer;
    /// <summary>playerの位置</summary>
    Transform _player;
    /// <summary>Attackが始まった時PlayerとBossの距離・この距離を保つように動く</summary>
    float _playerdistancePosZ = 0;
    /// <summary>攻撃開始するかどうか</summary>
    bool _isAttack = false;
    /// <summary>BulletAttackのパターンが同じにならないようにするための変数</summary>
    int _pastPatternsIndex;
    int[][] _bulletSpawnPatterns =
    {
        new int[2]{0, 1},
        new int[2]{0, 2},
        new int[2]{1, 2},
    };
    public bool isAttack { get { return _isAttack; } set { _isAttack = value; } }
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        SpotLightActive(false);
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        Debug.Log(distance);
        //Playerが近くにいたらヘリの音を出す
        if (distance < 100 && !_isAttack && _isAudioPlay)
        {
            _audioSource.Play();
        }
        //攻撃開始
        if (_isAttack)
        {
            //弾の発射場所を設定
            Vector3 pos = transform.position;
            pos.z = _player.transform.position.z + _playerdistancePosZ;
            transform.position = pos;
            Vector3 startdir = _player.transform.position;
            startdir.z += 30;
            if (_attackTimer == 0)
            {
                int index = Random.Range(0, _bulletSpawnPatterns.Length);
                while (_pastPatternsIndex == index)
                {
                    index = Random.Range(0, _bulletSpawnPatterns.Length);
                }
                _pastPatternsIndex = index;
                _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][0]].forward = startdir - _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][0]].position;
                _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][1]].forward = startdir - _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][1]].position;
                EffectsSwitch(false);
            }

            _attackTimer += Time.deltaTime;
            if (_attackTimer > _attackTime + 2)
            {
                _attackTimer = 0;
            }
            else if (_attackTimer > _attackTime)
            {
                BulletAttack();
            }
        }
        
        //Hpが０になったら
        if(_isDeath)
        {
            //攻撃を止める
            _isAttack = false;
            _anim.SetBool("Death", true);
        }
    }
    void BulletAttack()
    {
        _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][0]].Rotate(new Vector3(-6f * Time.deltaTime, 0, 0));
        //射撃用のrayを用意
        Ray ray = new Ray(_rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][0]].position, _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][0]].forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 300, _layerMask))
        {
            _rayHitTimer += Time.deltaTime;
            //秒数を超えたら
            if (_rayHitTimer > _rayHitTime)
            {
                EffectsSwitch(true);
                Debug.Log("当たっているよ");
                //Effect専用の空のオブジェクトに当たった場所を指定
                Vector3 hitPos = hit.point;
                //３レーンうち2レーンにエフェクトを出す
                for (int i = 0; i < 2; i++)
                {
                    hitPos.x = _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][i]].position.x;
                    Action(hitPos, i);
                }
                _rayHitTimer = 0;
            }
        }
        
    }

    public void PlayerDistanceSave()
    {
        _playerdistancePosZ = transform.position.z - _player.position.z;
    }

    void EffectsSwitch(bool isPlay)
    {
        if(isPlay)
        {
            
            for (int i = 0; i < _effects.Length; i++)
            {
                _effects[i].Play();
            }
           
        }
        else
        {
            for (int i = 0; i < _effects.Length; i++)
            {
                _effects[i].Stop();
            }
        }
    }
    
    public void DeathAudioPlay()
    {
        _isAudioPlay = false;
        _audioSource.clip = _deathAudio;
        _audioSource.volume = 1;
        _audioSource.loop = false;
        _audioSource.Play();
    }

    void Action(Vector3 hitPoint, int index)
    {
        _bulletEffect[index].Action(hitPoint);
    }

    public void SpotLightActive(bool isActive)
    {
        foreach(var l in _spotLights)
        {
            l.SetActive(isActive);
        }
    }
}
