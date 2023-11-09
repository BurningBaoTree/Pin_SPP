using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Shootball : SkillBase
{
    public float BulletSpeed;
    public override void Activeate(WhosActive whos)
    {
        if ((int)whos == 0)
        {
            GameObject ball = PoolManager.Inst.SpawnObjectinject(0, GameManager.Inst.Player.transform.position + Vector3.up);
            Ball b = ball.GetComponent<Ball>();
            b.moveActive();
            b.Dir = Vector2.up* BulletSpeed;
            b.sp.color = b.PlayerColor;
            b.BulletMod = true;
            b.PlayerBullet = true;
        }
        else
        {
            GameObject ball = PoolManager.Inst.SpawnObjectinject(0, GameManager.Inst.Enemy.transform.position + Vector3.down);
            Ball b = ball.GetComponent<Ball>();
            b.moveActive();
            b.Dir = Vector2.down* BulletSpeed;
            b.sp.color = b.EnemyColor;
            b.BulletMod = true;
        }
    }
}
