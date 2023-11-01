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

    CoolTimeSys timeSys;

    public Player Player;
    public Enemy Enemy;
    public GameObject Ball;

    public Action GameStart;
    public Action<bool> GameOver;
    public Action StageSet;
    public Action ResetTheGame;
    public Action SerchTheBalls;

    Action timecountAction;
    Action updater;

    public float timeCount = 0;
    public float GameBallSpeed = 5;
    public bool GameHasBeenStarted = false;

    private static GameManager instance;
    public static GameManager Inst => instance;

    public Sprite[] sprites = new Sprite[2];
    public SkillBase[] Skill = new SkillBase[2];

    PoolManager pooler;

    private void Awake()
    {
        instance = this;
        timecountAction = () => { };
        updater = () => { };    
        GameStart += StartCount;
        GameStart += PlayActivate;
        GameOver += EndActivate;
        ResetTheGame += ResetTime;
        SerchTheBalls += ballSerch;
        timeSys = GetComponent<CoolTimeSys>();
    }
    private void Start()
    {
        pooler = PoolManager.Inst;
    }

    /// <summary>
    /// 시간 카운트용 델리게이트
    /// </summary>
    private void Update()
    {
        timecountAction();
        updater();
    }

    /// <summary>
    /// 게임이 시작되는 타이밍에 카운트 함수를 델리게이트에 연결할 함수
    /// </summary>
    void StartCount()
    {
        timecountAction += countTime;
    }

    /// <summary>
    /// update델리게이트에서 구동될 시간 카운터
    /// </summary>
    void countTime()
    {
        timeCount += Time.deltaTime;
    }

    /// <summary>
    /// 시간 카운트 초기화
    /// </summary>
    void ResetTime()
    {
        GameStart -= StartCount;
        timeCount = 0;
        GameBallSpeed = 5;
    }

    /// <summary>
    /// 공을 찾고 공이 하나도 없으면 중앙에서 새로 생성한다.
    /// </summary>
    void ballSerch()
    {
        bool ballcheck = false;
        if (GameHasBeenStarted)
        {
            foreach (GameObject obj in pooler.pools[0].PooledObjectList)
            {
                if (obj.activeSelf)
                {
                    //공이 있다.
                    ballcheck = true;
                    break;
                }
            }
            if (!ballcheck)
            {
                timeSys.CoolTimeStart(0, 0.5f);
                updater += WaitForTheBall;
            }
        }
    }
    void WaitForTheBall()
    {
        if (timeSys.coolclocks[0].coolEnd)
        {
            pooler.SpawnObject(0, Vector3.zero);
            updater -= WaitForTheBall;
        }
    }

    /// <summary>
    /// 게임 플레이를 시작할때 필요한 초기화
    /// </summary>
    void PlayActivate()
    {
        GameHasBeenStarted = true;
        Player.gameObject.SetActive(true);
        Enemy.gameObject.SetActive(true);
        Enemy.Dificalty = this.Dificalty;
        ballSerch();
    }
    /// <summary>
    /// 게임 플레이를 끝낼때 필요한 초기화
    /// </summary>
    /// <param name="type"></param>
    void EndActivate(bool type)
    {
        GameHasBeenStarted = false;
        Player.gameObject.SetActive(false);
        Enemy.gameObject.SetActive(false);
    }
}
