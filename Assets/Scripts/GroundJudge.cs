using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundJudge : MonoBehaviour
{
    bool _isGround = true;
    public bool IsGround => _isGround;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = false;
        }
    }
}
