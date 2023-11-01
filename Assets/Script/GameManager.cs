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
    /// �ð� ī��Ʈ�� ��������Ʈ
    /// </summary>
    private void Update()
    {
        timecountAction();
        updater();
    }

    /// <summary>
    /// ������ ���۵Ǵ� Ÿ�ֿ̹� ī��Ʈ �Լ��� ��������Ʈ�� ������ �Լ�
    /// </summary>
    void StartCount()
    {
        timecountAction += countTime;
    }

    /// <summary>
    /// update��������Ʈ���� ������ �ð� ī����
    /// </summary>
    void countTime()
    {
        timeCount += Time.deltaTime;
    }

    /// <summary>
    /// �ð� ī��Ʈ �ʱ�ȭ
    /// </summary>
    void ResetTime()
    {
        GameStart -= StartCount;
        timeCount = 0;
        GameBallSpeed = 5;
    }

    /// <summary>
    /// ���� ã�� ���� �ϳ��� ������ �߾ӿ��� ���� �����Ѵ�.
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
                    //���� �ִ�.
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
    /// ���� �÷��̸� �����Ҷ� �ʿ��� �ʱ�ȭ
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
    /// ���� �÷��̸� ������ �ʿ��� �ʱ�ȭ
    /// </summary>
    /// <param name="type"></param>
    void EndActivate(bool type)
    {
        GameHasBeenStarted = false;
        Player.gameObject.SetActive(false);
        Enemy.gameObject.SetActive(false);
    }
}
