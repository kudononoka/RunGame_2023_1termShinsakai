using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Drone : MonoBehaviour
{
    /// <summary>Player�߂��ɓG�����邩�ǂ���</summary>
    bool _isAttack;
    /// <summary>�e�𔭎˂���Ƃ��̃^�[�Q�b�g</summary>
    Vector3 _target = default;
    [SerializeField, Header("�ePrefab")] GameObject _bulletPrefab;
    [SerializeField, Header("�e�𐶐�����ꏊ")] Transform _SpawnPos;
    [SerializeField, Header("�e�̐����C���^�[�o��")] float _bulletInstanceInterval;
    float _timer;
    [SerializeField, Header("��̒e�����Ɏg���G�l���M�[�l")] float _oneBulletEnergyNum;
    [SerializeField, Header("�G�l���M�[�ő�l")] float _maxEnergyNum;
    //[SerializeField, Header("�G�l���M�[��\���X���C�_�[")] Slider[] _energySilder;
    //int _sliderIndex;
    [SerializeField, Header("�G�l���M�[��\���X���C�_�[")] Slider _energySilder;
    [SerializeField, Header("�G�ƂȂ郌�C���[")] LayerMask _enemyLayer;
    /// <summary>���ݎ����Ă���G�l���M�[�l</summary>
    float _nowEnergyNum;
    Transform _player;
    [SerializeField] Transform _defaltDir;
    [SerializeField] RectTransform _rectTra;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _nowEnergyNum = _maxEnergyNum;
        _energySilder.maxValue = _maxEnergyNum;
        _energySilder.value = _maxEnergyNum;
        _energySilder.minValue = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        _rectTra.position = mousePos;
        if (Input.GetMouseButton(0))
        {
            _isAttack = true;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 3, false);
            RaycastHit hit;
            Vector3 dir = Vector3.zero;
            if(Physics.Raycast(ray, out hit, 100,_enemyLayer))
            {
                dir = hit.transform.position - transform.position;
                _target = hit.point;
               
                Debug.Log("��������");
            }
            else
            {
                dir = _player.transform.forward;
                _target = _defaltDir.position;
            }
            transform.forward = -dir;
        }
        else
        {
            transform.forward = Quaternion.Euler(0, 180, 0) * _player.transform.forward;
            _isAttack = false;
        }

        if (_isAttack)
        {
            _timer += Time.deltaTime;
            if (_timer > _bulletInstanceInterval)
            {
                BulletInstance();
                _timer = 0;
            }
        }

    }

    void BulletInstance()
    {
        if(EnergyConsumption())
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            GameObject bullet = Instantiate(_bulletPrefab, _SpawnPos.position, Quaternion.LookRotation(transform.forward));
            if(_target != null)
            {
                bullet.GetComponent<DroneBullet>().TargetPosLoad(_target);
            }
            
        }
    }
    /// <summary>���ݎ����Ă���G�l���M�[������郁�\�b�h</summary>
    /// <returns>True�̂Ƃ��͂܂��e�𐶐��ł��܂�</returns>
    bool EnergyConsumption()
    {
        if(_oneBulletEnergyNum < _nowEnergyNum)
        {
            _nowEnergyNum -= _oneBulletEnergyNum;
            SliderController();
            return true;
        }
        else
        {
            return false;
        }   
    }

    public void EnergyRecovery(float recoveryNum)
    {
        _nowEnergyNum += recoveryNum;
        SliderController();
    }

    void SliderController()
    {
        _energySilder.value = _nowEnergyNum;
        
    }
}
