using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dificalty
{
    easy = 0,
    middle,
    hard
}

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;
    Action action;
    Vector2 dir;

    Vector2 difalutPos;

    public dificalty dificalty = dificalty.easy;

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
                    Time.timeScale = 0;
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
    }
    private void OnEnable()
    {
        action += () => { };
        initialize();
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
    /// 초기화용 함수
    /// </summary>
    void initialize()
    {
        HP = 100;
        this.transform.position = difalutPos;
    }
}

//플레이어가 공을 때리기 전까지는 가운데에서 대기하다가 플레이어가 공을 치는 순간 공의 움직임을 읽고 가서 대기한다.
//난이도 쉬움 : 처음에는 공의 움직임을 따라 방향을 움직이다가 마지막에 궤도를 읽고 간다.(속도가 느리다.)
//난이도 쉬움 : 처음에는 공의 움직임을 따라 방향을 움직이다가 마지막에 궤도를 읽고 간다.(속도가 보통.)
//난이도 쉬움 : 처음에는 공의 움직임을 따라 방향을 움직이다가 마지막에 궤도를 읽고 간다.(속도가 빠름.)
