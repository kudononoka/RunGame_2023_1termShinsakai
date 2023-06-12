using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rb;
    Animator _anim;
    MotionState _motionState = MotionState.Run;
    [SerializeField, Header("Player�̐ڒn����X�N���v�g")]GroundJudge _groundJudge;
    [SerializeField,Header("�O�ɐi�ރX�s�[�h")] float _forwardMoveSpeed;
    [SerializeField, Header("���ړ��X�s�[�h")] float _xMoveSpeed;
    float _xMoveDir;
    float _xMoveCount;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _xMoveDir = 0;
        if (Input.GetKeyDown(KeyCode.A) && _xMoveCount > -1)
        {
            _xMoveDir = -1;
            _xMoveCount--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _xMoveCount < 1)
        {
            _xMoveDir = 1;
            _xMoveCount++;
        }
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            _rb.AddForce(Vector3.up * 7, ForceMode.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            _anim.SetTrigger("Sliding");
        }
        _rb.AddForce(transform.right * _xMoveDir * _xMoveSpeed, ForceMode.Impulse);

        _anim.SetBool("Jump", !_groundJudge.IsGround);
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.rotation *  new Vector3(0, _rb.velocity.y, _forwardMoveSpeed);
        //_rb.AddForce(Vector3.forward * _forwardMoveSpeed, ForceMode.Force);
    }

    /// <summary>Player�̓�����Ԃ��Ǘ�����enum</summary>
    enum MotionState
    {
        Run,
        Jump,
        Sliding,
    }
}
