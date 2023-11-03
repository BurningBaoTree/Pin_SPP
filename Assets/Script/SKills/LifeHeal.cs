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
            Debug.Log("�÷��̾� �� ����");
            updater += Healing;
        }
        else
        {
            Debug.Log("�� �� ����");
            updater += EnemyHealing;
        }
    }
    void Healing()
    {
        Debug.Log("�÷��̾� ���ϴ���");
        player.HP += HPincrisPerFraim;
        incrisedPlayerHP += HPincrisPerFraim;
        if (incrisedPlayerHP > MaxHeal)
        {
            Debug.Log("�÷��̾� �� ��");
            updater -= Healing;
        }
    }
    void EnemyHealing()
    {
        Debug.Log("�� ���ϴ���");
        enemy.HP += HPincrisPerFraim;
        incrisedEnemyHP += HPincrisPerFraim;
        if (incrisedEnemyHP > MaxHeal)
        {
            Debug.Log("�� �� ��");
            updater -= EnemyHealing;
        }
    }
}
