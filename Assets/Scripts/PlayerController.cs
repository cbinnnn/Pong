using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Player控制
public class PlayerController : MonoBehaviour {
    private Rigidbody2D r2d;
    public float speed;//Player控制速度
    private AudioSource audioSource;
    private int number;
    void Start () {
        r2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
	}
    private void FixedUpdate()
    {
        if (transform.CompareTag("Player1"))
        {
            Player1();
        }
        if (transform.CompareTag("Player2"))
        {
            Player2();
        }
        else if (transform.CompareTag("Com"))
        {
            Com();
        }
    }
    private void Player1()
    {
        if (ETCInput.GetButton("UpButton"))
        {
            r2d.velocity = new Vector2(0, speed);
        }
        else if (ETCInput.GetButton("DownButton"))
        {
            r2d.velocity = new Vector2(0, -speed);
        }
        else
        {
            r2d.velocity = Vector2.zero;
        }
    }
    private void Player2()
    {
        if (ETCInput.GetButton("UpButton1"))
        {
            r2d.velocity = new Vector2(0, speed);
        }
        else if (ETCInput.GetButton("DownButton1"))
        {
            r2d.velocity = new Vector2(0, -speed);
        }
        else
        {
            r2d.velocity = Vector2.zero;
        }
    }
    private void Com()
    {
        if (BallManager.Instance.r2d.velocity.x > 0)
        {
            //如果小球落点在电脑上方，电脑往上走
            if (BallManager.Instance.r2d.transform.position.y > r2d.transform.position.y && BallManager.Instance.r2d.transform.position.y - r2d.transform.position.y - r2d.transform.localScale.y / 2 >= 0.1f)
                r2d.velocity = new Vector2(0, 8);
            //如果小球落点在电脑下方，电脑往下走
            if (BallManager.Instance.r2d.transform.position.y < r2d.transform.position.y && r2d.transform.position.y - BallManager.Instance.r2d.transform.position.y - r2d.transform.localScale.y / 2 >= 0.1f)
                r2d.velocity = new Vector2(0, -8);
            //如果小球落点在电脑位置，电脑不动
            if (r2d.transform.position.y == BallManager.endPosY)
                r2d.velocity = Vector2.zero;
        }
        //如果小球卡在电脑后面，随机一个速度，移动电脑
        if (BallManager.Instance.r2d.transform.position.x > r2d.transform.position.x && BallManager.Instance.r2d.velocity.x < 0)
        {
            number = Random.Range(0, 2);
            if (number == 0)
                r2d.velocity = new Vector2(0, 10);
            if (number == 1)
                r2d.velocity = new Vector2(0, 0);
            if (number == 3)
                r2d.velocity = new Vector2(0, -10);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
}
