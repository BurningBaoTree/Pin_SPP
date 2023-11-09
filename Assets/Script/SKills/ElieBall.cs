using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElieBall : SkillBase
{
    public override void Activeate(WhosActive whos)
    {
        if((int)whos == 0)
        {
            foreach (GameObject obj in PoolManager.Inst.pools[0].PooledObjectList)
            {
                if (obj.activeSelf)
                {
                    Ball ball = obj.GetComponent<Ball>();
                    ball.sp.color = ball.PlayerColor;
                    ball.BulletMod = true;
                    ball.PlayerBullet = true;
                }
            }
        }
        else
        {
            foreach (GameObject obj in PoolManager.Inst.pools[0].PooledObjectList)
            {
                if (obj.activeSelf)
                {
                    Ball ball = obj.GetComponent<Ball>();
                    ball.sp.color = ball.EnemyColor;
                    ball.BulletMod = true;
                }
            }
        }
    }
}
