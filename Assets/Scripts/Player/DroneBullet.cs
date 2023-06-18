using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField, Header("��������Ă���̐�������")] float _lifeTime;
    [SerializeField, Header("�e�̑��x")] float _speed;
    /// <summary>�^�[�Q�b�g�ƂȂ���̂̈ʒu</summary>
    Vector3 _target = default;
    /// <summary>�i�s����</summary>
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
