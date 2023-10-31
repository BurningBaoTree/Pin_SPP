using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;

    public SkillBase[] skill = new SkillBase[2];

    PlayerInput input;
    Rigidbody2D rb;
    Action action;
    Vector2 dir;

    Vector2 difalutPos;

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
                this.transform.localScale = this.transform.localScale + (Vector3.right * (hp - copyhp) * 0.01f);
            }
        }
    }

    public float speed;
    public float hitAngle;

    /// <summary>
    /// 움직일수 있는지 없는지 체크하는 bool 변수(프로퍼티로 움직일수 없을때 물리값 초기화)
    /// </summary>
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
        input = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        difalutPos = this.transform.position;
        action += () => { };
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.moveBar.performed += MoveBar;
        input.Player.moveBar.canceled += MoveBar;
        initialize();
    }
    private void OnDisable()
    {
        input.Player.moveBar.canceled -= MoveBar;
        input.Player.moveBar.performed -= MoveBar;
        input.Disable();
    }
    private void Start()
    {
        gameManager = GameManager.Inst;
    }
    private void Update()
    {
        action();
    }

    private void MoveBar(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        dir = Vector2.right * context.ReadValue<Vector2>();
        if (dir.sqrMagnitude > 0.5f)
        {
            Move = true;
        }
        else
        {
            Move = false;
        }
    }
    void moveActive()
    {
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //벽에 부딫히면 움직일수 없다.
        if (collision.transform.CompareTag("Wall"))
        {
            Move = false;
        }

        //공에 부딫힐 경우 부딫힌 위치에 따라서 공이 움직이게 된다.근데 지금 왼쪽 가셍이로 치면 칠수록 속도가 빨라진다.(내비 둘 예정)
        if (collision.transform.CompareTag("Ball"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Ball ball = collision.gameObject.GetComponent<Ball>();
            float xGage = (contact.point.x - this.transform.position.x)* hitAngle;
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
        this.skill[0] = gameManager.Skill[0];
        this.skill[1] = gameManager.Skill[1];
    }

    void resetPos()
    {

    }

}
