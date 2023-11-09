using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlow : SkillBase
{
    public override void Activeate(WhosActive whos)
    {
        if ((int)whos == 0)
        {
            GameManager.Inst.Enemy.SlowDebuff();
        }
        else
        {
            GameManager.Inst.Player.SlowDebuff();
        }
    }
}
