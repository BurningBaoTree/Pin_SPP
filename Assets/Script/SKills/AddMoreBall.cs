using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEditor.PlayerSettings;

public class AddMoreBall : SkillBase
{
    public int extraBall;
    public override void Activeate(WhosActive whos)
    {
        Vector3 pos;
        if ((int)whos == 0)
        {
            pos = GameManager.Inst.Player.transform.position;
            pos += Vector3.up * 0.5f;
        }
        else
        {
            pos = GameManager.Inst.Player.transform.position;
            pos += Vector3.down * 0.5f;
        }
        for (int i = 0; i < extraBall; i++)
        {
            GameObject ball = PoolManager.Inst.SpawnObjectinject(0, pos);
            Ball ballcomp = ball.GetComponent<Ball>();
            ballcomp.RanddomwMove();
        }
    }
}
