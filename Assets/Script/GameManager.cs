using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    dificalty dificalty = dificalty.easy;
    public dificalty Dificalty
    {
        get
        {
            return dificalty;
        }
        set
        {
            dificalty = value;
            switch (dificalty)
            {
                case dificalty.easy:
                    GameBallSpeed = 5;
                    break;
                case dificalty.middle:
                    GameBallSpeed = 8;
                    break;
                case dificalty.hard:
                    GameBallSpeed = 15;
                    break;
                default:
                    GameBallSpeed = 5;
                    break;
            }
        }
    }


    public Player Player;
    public Enemy Enemy;
    public GameObject Ball;

    public Action GameStart;
    public Action<bool> GameOver;
    public Action StageSet;
    public Action ResetTheGame;
    public Action SerchTheBalls;

    Action timecountAction;

    public float timeCount = 0;
    public float GameBallSpeed = 5;
    public bool GameHasBeenStarted = false;

    private static GameManager instance;
    public static GameManager Inst => instance;

    public Sprite[] sprites = new Sprite[2];
    public SkillBase[] Skill = new SkillBase[2];

    private void Awake()
    {
        instance = this;
        timecountAction = () => { };
        GameStart += StartCount;
        GameStart += PlayActivate;
        GameOver += EndActivate;
        ResetTheGame += ResetTime;
        SerchTheBalls += ballSerch;

    }
    private void Update()
    {
        timecountAction();
    }

    void StartCount()
    {
        timecountAction += countTime;
    }

    void countTime()
    {
        timeCount += Time.deltaTime;
    }

    void ResetTime()
    {
        GameStart -= StartCount;
        timeCount = 0;
        GameBallSpeed = 5;
    }
    void ballSerch()
    {
        if(GameHasBeenStarted)
        {
            //배열로 공들을 찾고
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Ball");
            // 공이 없으면 실행
            if (objects.Length == 0)
            {
                Instantiate(Ball, Vector3.zero, Quaternion.identity);
            }
        }
    }
    void PlayActivate()
    {
        GameHasBeenStarted = true;
        Player.gameObject.SetActive(true);
        Enemy.gameObject.SetActive(true);
        Enemy.Dificalty = this.Dificalty;
        ballSerch();
    }
    void EndActivate(bool type)
    {
        GameHasBeenStarted = false;
        Player.gameObject.SetActive(false);
        Enemy.gameObject.SetActive(false);
    }
}
