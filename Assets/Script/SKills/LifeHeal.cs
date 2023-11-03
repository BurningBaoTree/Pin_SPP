using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHeal : SkillBase
{
    public float MaxHeal = 100;
    public float HPincrisPerFraim = 2;
    public float incrisedPlayerHP = 0;
    public float incrisedEnemyHP = 0;

    public override void Activeate(WhosActive whos)
    {
        incrisedPlayerHP = 0;
        incrisedEnemyHP = 0;
        if ((int)whos == 0)
        {
            Debug.Log("플레이어 힐 시작");
            updater += Healing;
        }
        else
        {
            Debug.Log("적 힐 시작");
            updater += EnemyHealing;
        }
    }
    void Healing()
    {
        Debug.Log("플레이어 힐하는중");
        player.HP += HPincrisPerFraim;
        incrisedPlayerHP += HPincrisPerFraim;
        if (incrisedPlayerHP > MaxHeal)
        {
            Debug.Log("플레이어 힐 끝");
            updater -= Healing;
        }
    }
    void EnemyHealing()
    {
        Debug.Log("적 힐하는중");
        enemy.HP += HPincrisPerFraim;
        incrisedEnemyHP += HPincrisPerFraim;
        if (incrisedEnemyHP > MaxHeal)
        {
            Debug.Log("적 힐 끝");
            updater -= EnemyHealing;
        }
    }
}
