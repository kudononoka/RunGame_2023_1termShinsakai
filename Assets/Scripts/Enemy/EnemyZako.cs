using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZako : EnemyBase
{
    [SerializeField, Header("angleÇÃîÕàÕ")] float _angleRange;
    [SerializeField, Header("angleSpeed")] float _angleSpeed;
    [SerializeField, Header("ê‘äOê¸Ç∆Ç»ÇÈGameObject")] GameObject _radline;
    Vector3 _startFoward;
    float _dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        _startFoward = transform.forward;
        _radline = transform.GetChild(0).gameObject;
        _radline.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Vector3.Angle(_startFoward, transform.forward);
        if (angle < -_angleRange || angle > _angleRange)
        {
            _dir *= -1;
        }

        transform.Rotate(new Vector3(0, _dir * _angleSpeed * Time.deltaTime, 0));

        if(_isDeath)
        {
            Vector3 pos = transform.position;
            pos.y += 1.5f;
            FindObjectOfType<EnemyExplosionAudio>().Action(pos);
            _radline.SetActive(false);
            _isDeath = false;
        }
    }

    
}
