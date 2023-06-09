using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField, Header("生成されてからの生存時間")] float _lifeTime;
    [SerializeField, Header("弾の速度")] float _speed;
    /// <summary>進行方向</summary>
    Vector3 _dir = Vector3.forward;
    void Start()
    {
        Destroy(gameObject, _lifeTime);
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _dir * _speed;
    }

    public void TargetPosLoad(Vector3 dir)
    {
        _dir = dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Destroy(gameObject);
        }
    }
}
