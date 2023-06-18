using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Drone : MonoBehaviour
{
    /// <summary>Player近くに敵がいるかどうか</summary>
    bool _isAttack;
    /// <summary>弾を発射するときのターゲット</summary>
    Vector3 _target = default;
    [SerializeField, Header("弾Prefab")] GameObject _bulletPrefab;
    [SerializeField, Header("弾を生成する場所")] Transform _SpawnPos;
    [SerializeField, Header("弾の生成インターバル")] float _bulletInstanceInterval;
    float _timer;
    [SerializeField, Header("一つの弾生成に使うエネルギー値")] float _oneBulletEnergyNum;
    [SerializeField, Header("エネルギー最大値")] float _maxEnergyNum;
    //[SerializeField, Header("エネルギーを表すスライダー")] Slider[] _energySilder;
    //int _sliderIndex;
    [SerializeField, Header("エネルギーを表すスライダー")] Slider _energySilder;
    [SerializeField, Header("敵となるレイヤー")] LayerMask _enemyLayer;
    /// <summary>現在持っているエネルギー値</summary>
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
               
                Debug.Log("当たった");
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
    /// <summary>現在持っているエネルギーを消費するメソッド</summary>
    /// <returns>Trueのときはまだ弾を生成できます</returns>
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
