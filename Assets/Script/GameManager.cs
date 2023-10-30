using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player;
    public Enemy Enemy;

    public Action GameStart;
    public Action<bool> GameOver;


    private static GameManager instance;
    public static GameManager Inst => instance;

    List<Ball> balls = new List<Ball>();

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
}
