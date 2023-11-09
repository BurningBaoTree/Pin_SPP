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
            updater += Healing;
        }
        else
        {
            updater += EnemyHealing;
        }
    }
    void Healing()
    {
        player.HP += HPincrisPerFraim;
        incrisedPlayerHP += HPincrisPerFraim;
        if (incrisedPlayerHP > MaxHeal)
        {
            updater -= Healing;
        }
    }
    void EnemyHealing()
    {
        enemy.HP += HPincrisPerFraim;
        incrisedEnemyHP += HPincrisPerFraim;
        if (incrisedEnemyHP > MaxHeal)
        {
            updater -= EnemyHealing;
        }
    }
}
