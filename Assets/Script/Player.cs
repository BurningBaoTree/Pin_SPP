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
    /// �����ϼ� �ִ��� ������ üũ�ϴ� bool ����(������Ƽ�� �����ϼ� ������ ������ �ʱ�ȭ)
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
        //���� �΋H���� �����ϼ� ����.
        if (collision.transform.CompareTag("Wall"))
        {
            Move = false;
        }

        //���� �΋H�� ��� �΋H�� ��ġ�� ���� ���� �����̰� �ȴ�.�ٵ� ���� ���� �����̷� ġ�� ĥ���� �ӵ��� ��������.(���� �� ����)
        if (collision.transform.CompareTag("Ball"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Ball ball = collision.gameObject.GetComponent<Ball>();
            float xGage = (contact.point.x - this.transform.position.x)* hitAngle;
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
        this.skill[0] = gameManager.Skill[0];
        this.skill[1] = gameManager.Skill[1];
    }

    void resetPos()
    {

    }

}
