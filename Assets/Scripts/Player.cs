using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField,Header("�O�ɐi�ރX�s�[�h")] float _forwardMoveSpeed;
    [SerializeField, Header("���ړ��X�s�[�h")] float _xMoveSpeed;
    float _xMoveDir;
    float _xMoveCount;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _xMoveDir = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && _xMoveCount > -1)
        {
            _xMoveDir = -1;
            _xMoveCount--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && _xMoveCount < 1)
        {
            _xMoveDir = 1;
            _xMoveCount++;
        }
        _rb.AddForce(Vector3.right * _xMoveDir * _xMoveSpeed, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector3.forward * _forwardMoveSpeed;
    }
}
