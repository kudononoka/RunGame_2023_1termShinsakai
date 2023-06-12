using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneObjectPsrticle : MonoBehaviour
{
    Transform _player;
    ParticleSystem _system;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _system = GetComponent<ParticleSystem>();
        //_system.trigger.SetCollider(0, _player.transform);
        ParticleSystem.TriggerModule _systemTrigger = _system.trigger;
        ParticleSystemOverlapAction triggerState = _systemTrigger.enter;
        triggerState = ParticleSystemOverlapAction.Callback;
        _systemTrigger.enter = triggerState;
        //_system.trigger = triggerState;
    }
    
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GamedState = GameManager.GameState.GameOver;
        }
    }
}
