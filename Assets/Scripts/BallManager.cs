using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//小球管理类
public class BallManager : MonoBehaviour {
    private Rigidbody2D r2d;
    public static float startPosY;//小球碰到Player的位置
    public static float endPosY;//小球将要打到的位置
    private static Vector2 speed;
    private static Vector2 player1Scale;
    private static Vector2 player2Scale;
    public float maxSpeed = 12f;//设置最大速度
    // Use this for initialization
    void Start()
    {
        GoBall();//小球初始化
    }
    void Update()
    {
        Vector2 velocity = r2d.velocity;
        if (velocity.x < 14 && velocity.x > -14 && velocity.x != 0)
        {
            if (velocity.x > 0)
                velocity.x = 13;
            else
                velocity.x = -13;
        }
        r2d.velocity = velocity;
    }
    //碰撞检测
    private void OnCollisionEnter2D(Collision2D col)
    {
        //碰到Player
        if (col.collider.tag == "Player")
        {
            Vector2 velocity = r2d.velocity;
            //小球的y方向速度等于本身y方向速度的一半+Player速度的一半
            velocity.y = velocity.y / 2 + col.rigidbody.velocity.y / 2;
            r2d.velocity = velocity;
            //计算小球的碰到Player的位置和即将打到的位置
            startPosY = col.collider.transform.position.y;
            endPosY = startPosY + r2d.velocity.y * ((GameManager.Instance.Player2.position.x - GameManager.Instance.Player1.position.x) / r2d.velocity.x);
        }
        //如果碰到墙
        if (col.gameObject.name == "leftWall" || col.gameObject.name == "rightWall")
        {
            //调用GameManager的ChangeScore函数
            GameManager.Instance.ChangeScore(col.gameObject.name);
        }
    }
    //小球的运动初始化函数
    void GoBall()
    {
        r2d = GetComponent<Rigidbody2D>();
        int number = Random.Range(0, 2);
        //随机小球运动方向
        if (number == 1)
        {
            r2d.AddForce(new Vector2(120, 0));
        }
        else
        {
            r2d.AddForce(new Vector2(-120, 0));
        }
    }
    //道具触发检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        speed = r2d.velocity;//获得触发道具前小球的速度
        //小球加速
        if (collision.CompareTag("Up"))
        {
            r2d.velocity *= 1.5f;
            if (r2d.velocity.x > maxSpeed)
                r2d.velocity = new Vector2(maxSpeed, r2d.velocity.y);
            //5秒后重置速度
            Invoke("reSpeed", 5);
        }
        //小球减速
        if (collision.CompareTag("Down"))
        {
            r2d.velocity *= 0.3f;
            //5秒后重置速度
            Invoke("reSpeed", 5);
        }
        //Player长度增加
        if (collision.CompareTag("Plus"))
        {
            if (r2d.velocity.x > 0)
            {
                player1Scale = GameManager.Instance.Player1.localScale;
                GameManager.Instance.Player1.localScale = new Vector2(GameManager.Instance.Player1.localScale.x, GameManager.Instance.Player1.localScale.y * 1.25f);
                //8秒后重置Player1长度
                Invoke("rePlayer1", 8);
            }
            else
            {
                player2Scale = GameManager.Instance.Player2.localScale;
                GameManager.Instance.Player2.localScale = new Vector2(GameManager.Instance.Player2.localScale.x, GameManager.Instance.Player2.localScale.y * 1.25f);
                //8秒后重置Player2长度
                Invoke("rePlayer2", 8);
            }
        }
        //Player长度缩小
        if (collision.CompareTag("Minus"))
        {
            if (r2d.velocity.x > 0)
            {
                player1Scale = GameManager.Instance.Player1.localScale;
                GameManager.Instance.Player1.localScale = new Vector2(GameManager.Instance.Player1.localScale.x, GameManager.Instance.Player1.localScale.y * 0.75f);
                //8秒后重置Player1长度
                Invoke("rePlayer1", 8);
            }
            else
            {
                player2Scale = GameManager.Instance.Player2.localScale;
                GameManager.Instance.Player2.localScale = new Vector2(GameManager.Instance.Player2.localScale.x, GameManager.Instance.Player2.localScale.y * 0.75f);
                //8秒后重置Player2长度
                Invoke("rePlayer2", 8);
            }
        }
        if (collision.CompareTag("Life"))
        {
            if (r2d.velocity.x > 0)
            {
                if(GameManager.Instance.score1>0)
                    GameManager.Instance.score1--;
            }
            else
            {
                if (GameManager.Instance.score2 > 0)
                    GameManager.Instance.score2--;
            }
            GameManager.Instance.Score1Text.text = GameManager.Instance.score1.ToString();
            GameManager.Instance.Score2Text.text = GameManager.Instance.score2.ToString();
            PropManager.Instance.PutBack(collision.gameObject);
            return;
        }
        //销毁道具
        PropManager.Instance.PutBack(collision.gameObject);
        PropManager.Instance.CloneCount--;
    }
    //重置速度
    void reSpeed()
    {
        if (r2d.velocity.x > 0)
            r2d.velocity = new Vector2(10, r2d.velocity.y);
        else
            r2d.velocity = new Vector2(-10, r2d.velocity.y);
    }
    //重置Player1的长度
    void rePlayer1()
    {
        GameManager.Instance.Player1.localScale = player1Scale;
    }
    //重置Player2的长度
    void rePlayer2()
    {
        GameManager.Instance.Player2.localScale = player2Scale;
    }
}
