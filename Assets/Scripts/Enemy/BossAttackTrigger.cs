using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrigger : LaneObjectBase
{
    [SerializeField, Header("攻撃開始するかどうか")] bool _isBossAttack;
    public override void Action()
    {
        EnemyBoss enemyBoss = FindObjectOfType<EnemyBoss>();
        enemyBoss.PlayerDistanceSave();
        enemyBoss.SpotLightActive(true);
        enemyBoss.isAttack = _isBossAttack;
    }
}
