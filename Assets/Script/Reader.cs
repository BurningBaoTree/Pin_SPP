using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reader : MonoBehaviour
{
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
    float speed = 8f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.Inst.GameStart += () =>
        {
            this.gameObject.SetActive(true);
        };
        GameManager.Inst.GameOver += (bool trr) => { this.gameObject.SetActive(false); };
        GameManager.Inst.Player.PlayerHit += Calculate;
        this.gameObject.SetActive(false);
    }
    void Calculate()
    {
        Dir = GameManager.Inst.Player.ReadWayPoint;
        this.transform.position = GameManager.Inst.Player.hitpoint;
        rb.velocity = Dir * speed;
    }
    void calculateComplit(Vector2 dir)
    {
        GameManager.Inst.Enemy.CalResult = dir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Reader"))
        {
            Debug.Log("계산 완료");
            rb.velocity = Vector3.zero;
            calculateComplit(this.transform.position);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ///벽에 부딫힌 경우 반사
        if (collision.collider.CompareTag("Wall"))
        {
            Vector2 reflection = Vector2.Reflect(Dir, collision.contacts[0].normal);
            Dir = reflection;
        }
    }
}
