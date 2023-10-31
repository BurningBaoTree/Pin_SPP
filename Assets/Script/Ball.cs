using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Color PlayerColor;
    public Color EnemyColor;
    SpriteRenderer sp;

    Rigidbody2D rb;
    public float speed;


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
    private void Start()
    {
        BallSpeedSel();
        moveActive();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
                GameManager.Inst.Enemy.HP -= 10;
                GameManager.Inst.SerchTheBalls?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                GameManager.Inst.Player.HP -= 10;
                ReSetBall();
                moveActive();
            }
        }
    }
    private void OnDestroy()
    {
        GameManager.Inst.SerchTheBalls?.Invoke();
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
        speed = GameManager.Inst.GameBallSpeed;
    }
}
//���� ������ ���ϴµ�, �� ��� ���̳� �÷��̾ ġ�� HP�� �����Ѵ�.
