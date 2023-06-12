using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaneObjectBase : MonoBehaviour
{
    public abstract void Action();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Action();
        }
    }
}
