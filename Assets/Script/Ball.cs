using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager gamemana;

    public Color PlayerColor;
    public Color EnemyColor;
    SpriteRenderer sp;

    Rigidbody2D rb;
    public float speed;

    public bool BulletMod = false;
    public bool PlayerBullet = false;

    int notFirstTime = 0;

    /// <summary>
    /// ���� ����(������Ƽ�� ������ �ٲ� ������ �ٵ� ������ �Է½��Ѽ� �����̰� �Ѵ�.)
    /// </summary>
    Vector2 dir;
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
                rb.velocity = dir;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        if (gamemana == null && notFirstTime != 0)
        {
            gamemana = GameManager.Inst;
            BallSpeedSel();
            moveActive();
        }
        else if (gamemana != null)
        {
            BallSpeedSel();
            moveActive();
        }
        notFirstTime++;
    }
    private void Start()
    {
        gamemana = GameManager.Inst;
    }
    void OnDisable()
    {
        if (gamemana != null && gamemana.GameHasBeenStarted)
        {
            gamemana.SerchTheBalls?.Invoke();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ///�Ѿ� �����϶� �������� 3��!
        if (BulletMod)
        {
            if (collision.collider.CompareTag("Player") && !PlayerBullet)
            {
                gamemana.Player.HP -= 15;
                this.gameObject.SetActive(false);
            }
            if (collision.collider.CompareTag("Enemy") && PlayerBullet)
            {
                gamemana.Enemy.HP -= 15;
                this.gameObject.SetActive(false);
            }
        }

        ///���� �΋H�� ��� �ݻ�
        if (collision.collider.CompareTag("Wall"))
        {
            Vector2 reflection = Vector2.Reflect(Dir, collision.contacts[0].normal);
            Dir = reflection;
        }

        ///�������� �΋H�� ��� ���� ��ġ�� �����ϰ�, �浹 ��ġ�� ���� ����� �Ǹ� ���.
        if (collision.collider.CompareTag("DeadZone"))
        {
            ContactPoint2D contact = collision.contacts[0];
            if (contact.point.y > 0)
            {
                gamemana.Enemy.HP -= 5;
                this.gameObject.SetActive(false);
            }
            else
            {
                gamemana.Player.HP -= 5;
                this.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// �����Ҷ� ���� �Ʒ��� �����̴� �Լ�
    /// </summary>
    void moveActive()
    {
        rb.velocity = Vector2.down * speed;
        Dir = Vector2.down * speed;
    }

    /// <summary>
    /// ���� ��� ��ġ�� �ű�� �Ʒ��� �����̴� �Լ�
    /// </summary>
    void ReSetBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
        sp.color = Color.white;
    }
    /// <summary>
    /// ���� ���������� ���������� ���� �ӵ��� GameManager���� �������� �Լ�
    /// </summary>
    void BallSpeedSel()
    {
        speed = gamemana.GameBallSpeed;
    }
}
//���� ������ ���ϴµ�, �� ��� ���̳� �÷��̾ ġ�� HP�� �����Ѵ�.
