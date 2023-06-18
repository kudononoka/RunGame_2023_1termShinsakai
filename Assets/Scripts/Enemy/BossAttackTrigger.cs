using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrigger : LaneObjectBase
{
    [SerializeField, Header("�U���J�n���邩�ǂ���")] bool _isBossAttack;
    public override void Action()
    {
        EnemyBoss enemyBoss = FindObjectOfType<EnemyBoss>();
        enemyBoss.PlayerDistanceSave();
        enemyBoss.SpotLightActive(true);
        enemyBoss.isAttack = _isBossAttack;
    }
}
