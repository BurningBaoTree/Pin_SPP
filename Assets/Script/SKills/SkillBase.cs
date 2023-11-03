using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WhosActive
{
    Player = 0,
    Enemy
}

public class SkillBase : MonoBehaviour
{
    protected Action updater;

    public float coolTime = 0;

    protected Player player;
    protected Enemy enemy;

    private void Awake()
    {
        updater = () => {};
    }
    private void Start()
    {
        player = GameManager.Inst.Player;
        enemy = GameManager.Inst.Enemy;
    }
    protected virtual void Update()
    {
        updater();
    }
    public virtual void Activeate(WhosActive whos)
    {

    }
}
