using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ザコエネミーが死んだときのエフェクトと音
/// </summary>
public class EnemyExplosionAudio : MonoBehaviour
{
    ParticleSystem _particleSystem;
    AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    public void Action(Vector3 activePoint)
    {
        transform.position = activePoint;
        _particleSystem.Play();
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}
