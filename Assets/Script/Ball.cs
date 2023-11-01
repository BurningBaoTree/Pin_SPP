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
    /// 방향 변수(프로퍼티로 방향이 바뀔때 리지디 바디에 방향을 입력시켜서 움직이게 한다.)
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
        ///총알 상태일때 데미지가 3배!
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

        ///벽에 부딫힌 경우 반사
        if (collision.collider.CompareTag("Wall"))
        {
            Vector2 reflection = Vector2.Reflect(Dir, collision.contacts[0].normal);
            Dir = reflection;
        }

        ///데드존에 부딫힐 경우 공의 위치를 리셋하고, 충돌 위치에 따라 대상의 피를 깐다.
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
    /// 시작할때 공을 아래로 움직이는 함수
    /// </summary>
    void moveActive()
    {
        rb.velocity = Vector2.down * speed;
        Dir = Vector2.down * speed;
    }

    /// <summary>
    /// 공을 가운데 위치로 옮기고 아래로 움직이는 함수
    /// </summary>
    void ReSetBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
        sp.color = Color.white;
    }
    /// <summary>
    /// 게임 스테이지가 정해졌을때 공의 속도를 GameManager에서 가져오는 함수
    /// </summary>
    void BallSpeedSel()
    {
        speed = gamemana.GameBallSpeed;
    }
}
//공이 색갈이 변하는데, 그 경우 적이나 플레이어가 치면 HP가 감소한다.
