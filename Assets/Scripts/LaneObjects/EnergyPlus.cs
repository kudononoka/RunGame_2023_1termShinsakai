using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPlus : LaneObjectBase
{
    /// <summary>エネルギー回復値</summary>
    [SerializeField, Header("エネルギー回復値")] float _recoveryNum;
    AudioSource _audioSource;
    /// <summary>エネルギー回復値</summary>
    [SerializeField, Header("効果音")] AudioClip _audio;
    public override void Action()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_audio);
        FindObjectOfType<Drone>().EnergyRecovery(_recoveryNum);
        gameObject.SetActive(false);
    }

    //IEnumerator AudioControlle()
    //{
    //    AudioSource _audioSource = GetComponent<AudioSource>();
    //    _audioSource.PlayOneShot(_audioSource.clip);
    //    yield return new WaitForSeconds(1);
    //    FindObjectOfType<Drone>().EnergyRecovery(_recoveryNum);
    //    gameObject.SetActive(false);
    //}
}
