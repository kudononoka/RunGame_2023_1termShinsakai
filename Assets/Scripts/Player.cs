using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField,Header("前に進むスピード")] float _forwardMoveSpeed;
    [SerializeField, Header("横移動スピード")] float _xMoveSpeed;
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
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
        _rb.AddForce(Vector3.right * _xMoveDir * _xMoveSpeed, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(0, _rb.velocity.y, _forwardMoveSpeed);
        //_rb.AddForce(Vector3.forward * _forwardMoveSpeed, ForceMode.Force);
    }
}
