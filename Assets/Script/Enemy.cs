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
    GameManager gameManager;
    Rigidbody2D rb;
    Action action;
    Vector2 dir;
    public Vector2 CalResult;

    int ability1;
    int ability2;

    CircleCollider2D circleCollider;

    Vector2 difalutPos;

    float Blocklngth = 3;


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
        action += () => { };
    }
    private void OnEnable()
    {
        initialize();
    }
    private void OnDisable()
    {
        Dificalty = dificalty.easy;
        action = () => { };
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
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
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
    /// �ʱ�ȭ�� �Լ�
    /// </summary>
    void initialize()
    {
        HP = 100;
        this.transform.position = difalutPos;
        patternSellect();
    }

    void EasyMode()
    {
        speed = 5;
    }
    void middelMode()
    {
        speed = 10;
    }
    void HardMode()
    {
        speed = 15;
        action += ReadCorce;
    }
    void patternSellect()
    {
        GetAbility();
        switch (dificalty)
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

    //�÷��̾ ��� ����ٴϴ� �Լ�
    void PlayerFallow()
    {
        dir = gameManager.Player.transform.position.normalized;
    }

    void ReadCorce()
    {
        dir = CalResult;
        Move = true;
    }

    void ZigZag()
    {
        dir.x = Mathf.Cos(Time.deltaTime);
    }

    void AvoidBullet()
    {

    }

}

//�÷��̾ ���� ������ �������� ������� ����ϴٰ� �÷��̾ ���� ġ�� ���� ���� �������� �а� ���� ����Ѵ�.
//���̵� ���� : ó������ ���� �������� ���� ������ �����̴ٰ� �������� �˵��� �а� ����.(�ӵ��� ������.)
//���̵� ���� : ó������ ���� �������� ���� ������ �����̴ٰ� �������� �˵��� �а� ����.(�ӵ��� ����.)
//���̵� ���� : ó������ ���� �������� ���� ������ �����̴ٰ� �������� �˵��� �а� ����.(�ӵ��� ����.)
