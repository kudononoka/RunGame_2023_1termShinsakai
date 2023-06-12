using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForwardDirChange : MonoBehaviour
{
    [SerializeField, Header("‹È‚ª‚é•ûŒü")] Vector3 _nextDir;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Transform _playerTra = other.transform;
            _playerTra.rotation = Quaternion.LookRotation(_nextDir);
        }
    }
}
