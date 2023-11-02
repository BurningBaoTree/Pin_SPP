using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum dificalty
{
    easy = 0,
    middle,
    hard
}

public class Enemy : MonoBehaviour
{
    public CoolTimeSys cooltime;
    public SkillBase[] skill;
    GameManager gameManager;
    Rigidbody2D rb;
    Action action;
    public Vector2 dir;
    public Vector2 Dir
    {
        get
        {
            return dir;
        }
        set
        {
            if (dir != value)
            {
                dir = value;
                ChangeDirecthion();
            }
        }
    }
    Vector2 targetPostion;
    public Vector2 calResult;
    public Vector2 CalResult
    {
        get
        {
            return calResult;
        }
        set
        {
            if (calResult != value)
            {
                calResult = value;
            }
        }
    }

    int ability1;
    int ability2;

    CircleCollider2D circleCollider;

    Vector2 difalutPos;

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
            patternSellect();
        }
    }
    public float hp;
    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            float copyhp = hp;
            if (hp != value)
            {
                hp = value;
                if (hp > 500)
                {
                    hp = 500;
                }
                if (hp < 0)
                {
                    GameManager.Inst.GameOver?.Invoke(true);
                }
                this.transform.localScale = this.transform.localScale + (Vector3.right * (hp - copyhp) * 0.01f);
            }
        }
    }

    public float speed;
    public float hitAngle;

    bool move;
    bool Move
    {
        get
        {
            return move;
        }
        set
        {
            if (move != value)
            {
                move = value;
                if (move)
                {
                    action += moveActive;
                }
                else
                {
                    action -= moveActive;
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        difalutPos = this.transform.position;
        cooltime = GetComponent<CoolTimeSys>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void OnEnable()
    {
        initialize();
    }
    private void OnDisable()
    {
        Dificalty = dificalty.easy;
    }
    private void Start()
    {
        gameManager = GameManager.Inst;
    }
    private void Update()
    {
        action();
    }
    void moveActive()
    {
        transform.transform.position = Vector2.MoveTowards(transform.position, targetPostion, Time.deltaTime * speed);
    }
    void ChangeDirecthion()
    {
        targetPostion = new Vector2(Dir.x, 4);
        Move = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Move = false;
        }
        if (collision.transform.CompareTag("Ball"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Ball ball = collision.gameObject.GetComponent<Ball>();
            float xGage = (contact.point.x - this.transform.position.x) * hitAngle;
            ball.Dir = new Vector2(xGage, -ball.Dir.y);
        }
    }
    /// <summary>
    /// 초기화용 함수
    /// </summary>
    void initialize()
    {
        HP = 100;
        this.transform.position = difalutPos;
        circleCollider.radius = 3;
        action = () => { };
    }

    void EasyMode()
    {
        speed = 7;
        action += ReadCorce;
    }
    void middelMode()
    {
        speed = 15;
        action += ReadCorce;
    }
    void HardMode()
    {
        speed = 18;
        action += ReadCorce;
    }
    void patternSellect()
    {
        GetAbility();
        switch (Dificalty)
        {
            case dificalty.easy:
                EasyMode();
                break;
            case dificalty.middle:
                middelMode();
                break;
            case dificalty.hard:
                HardMode();
                break;
            default:
                break;
        }
    }

    void GetAbility()
    {
        ability1 = UnityEngine.Random.Range(0, 5);
        ability1 = UnityEngine.Random.Range(0, 5);
    }

    //플레이어를 잠시 따라다니는 함수
    void PlayerFallow()
    {
         Dir = gameManager.Player.transform.position;
    }

    void ReadCorce()
    {
        Dir = CalResult;
    }

    void ZigZag()
    {
        Dir = Vector2.right * (Mathf.Cos(Time.deltaTime));
        if (cooltime.coolclocks[1].coolEnd)
        {
            action -= ZigZag;
        }
    }

    void AvoidBullet()
    {

    }

}

//플레이어가 공을 때리기 전까지는 가운데에서 대기하다가 플레이어가 공을 치는 순간 공의 움직임을 읽고 가서 대기한다.
//난이도 쉬움 : 처음에는 공의 움직임을 따라 방향을 움직이다가 마지막에 궤도를 읽고 간다.(속도가 느리다.)
//난이도 쉬움 : 처음에는 공의 움직임을 따라 방향을 움직이다가 마지막에 궤도를 읽고 간다.(속도가 보통.)
//난이도 쉬움 : 처음에는 공의 움직임을 따라 방향을 움직이다가 마지막에 궤도를 읽고 간다.(속도가 빠름.)
