using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField, Header("生成されてからの生存時間")] float _lifeTime;
    [SerializeField, Header("弾の速度")] float _speed;
    /// <summary>ターゲットとなるものの位置</summary>
    Vector3 _target = default;
    /// <summary>進行方向</summary>
    Vector3 _dir = Vector3.forward;
    void Start()
    {
        Destroy(gameObject, _lifeTime);
        transform.rotation = Quaternion.AngleAxis(90, transform.right);
        _rb = GetComponent<Rigidbody>();
        if(_target != null)
        {
            _dir = (_target - transform.position).normalized;
        }

    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _dir * _speed;
    }

    public void TargetPosLoad(Vector3 pos)
    {
        _target = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(1);
            Destroy(gameObject);
        }
    }
}
