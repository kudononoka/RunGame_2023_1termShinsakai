using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    Animator _anim;
    AudioSource _audioSource;
    [Header("Audio")]
    [SerializeField, Header("��s��Ԃ̌��ʉ�")] AudioClip _fiyAudio;
    [SerializeField, Header("�ė��������̌��ʉ�")] AudioClip _deathAudio;
    bool _isAudioPlay = true;
    [SerializeField, Header("ray���Ƃ΂��ꏊ")] Transform[] _rayOriginPos;
    [SerializeField, Header("���e�������̃G�t�F�N�g")] ParticleSystem[] _effects;
    [SerializeField, Header("�n�ʂƂȂ�I�u�W�F�N�g�̃��C���[")] LayerMask _layerMask;
    [SerializeField, Header("�e������")] float _attackTime;
    float _attackTimer = 0;
    [SerializeField, Header("ray�������������̎���")] float _rayHitTime;
    [SerializeField] EnemyBossBulletEffect[] _bulletEffect;
    [SerializeField, Header("�X�|�b�g���C�g")] GameObject[] _spotLights;
    float _rayHitTimer;
    /// <summary>player�̈ʒu</summary>
    Transform _player;
    /// <summary>Attack���n�܂�����Player��Boss�̋����E���̋�����ۂ悤�ɓ���</summary>
    float _playerdistancePosZ = 0;
    /// <summary>�U���J�n���邩�ǂ���</summary>
    bool _isAttack = false;
    /// <summary>BulletAttack�̃p�^�[���������ɂȂ�Ȃ��悤�ɂ��邽�߂̕ϐ�</summary>
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
        //Player���߂��ɂ�����w���̉����o��
        if (distance < 100 && !_isAttack && _isAudioPlay)
        {
            _audioSource.Play();
        }
        //�U���J�n
        if (_isAttack)
        {
            //�e�̔��ˏꏊ��ݒ�
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
        
        //Hp���O�ɂȂ�����
        if(_isDeath)
        {
            //�U�����~�߂�
            _isAttack = false;
            _anim.SetBool("Death", true);
        }
    }
    void BulletAttack()
    {
        _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][0]].Rotate(new Vector3(-6f * Time.deltaTime, 0, 0));
        //�ˌ��p��ray��p��
        Ray ray = new Ray(_rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][0]].position, _rayOriginPos[_bulletSpawnPatterns[_pastPatternsIndex][0]].forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 300, _layerMask))
        {
            _rayHitTimer += Time.deltaTime;
            //�b���𒴂�����
            if (_rayHitTimer > _rayHitTime)
            {
                EffectsSwitch(true);
                Debug.Log("�������Ă����");
                //Effect��p�̋�̃I�u�W�F�N�g�ɓ��������ꏊ���w��
                Vector3 hitPos = hit.point;
                //�R���[������2���[���ɃG�t�F�N�g���o��
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
