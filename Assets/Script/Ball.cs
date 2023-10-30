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
        moveActive();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Vector2 reflection = Vector2.Reflect(Dir, collision.contacts[0].normal);
            Dir = reflection;
        }
        if (collision.collider.CompareTag("DeadZone"))
        {
            ContactPoint2D contact = collision.contacts[0];
            if (contact.point.y > 0)
            {
                GameManager.Inst.Enemy.HP -= 10;
                ReSetBall();
            }
            else
            {
                GameManager.Inst.Player.HP -= 10;
                ReSetBall();
            }
        }
    }
    void moveActive()
    {
        rb.velocity = Vector2.down * speed;
        Dir = Vector2.down * speed;
    }
    void ReSetBall()
    {
        transform.position = Vector3.zero;
        sp.color = Color.white;
        moveActive();
    }
}
//공이 색갈이 변하는데, 그 경우 적이나 플레이어가 치면 HP가 감소한다.
