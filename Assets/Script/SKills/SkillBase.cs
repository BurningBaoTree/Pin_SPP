using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public Action Active;
    protected Action updater;

    private void Awake()
    {
        updater = () => { };
        Active += Activeate;
    }
    private void Update()
    {
        updater();
    }
    protected virtual void Activeate()
    {

    }
}
