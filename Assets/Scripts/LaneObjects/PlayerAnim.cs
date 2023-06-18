using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Animator playerAnim = collision.gameObject.GetComponent<Animator>();
            playerAnim.SetBool("Sliding2", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Animator playerAnim = collision.gameObject.GetComponent<Animator>();
            playerAnim.SetBool("Sliding2", false);
        }
    }
}
