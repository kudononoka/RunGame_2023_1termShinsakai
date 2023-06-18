using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossBulletEffect : MonoBehaviour
{
    ParticleSystem[] _particleSystems;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
    }

    public void Action(Vector3 pos)
    {
        transform.position = pos;
        foreach (var p in _particleSystems)
        {
            p.Play();
        }
    }
}
