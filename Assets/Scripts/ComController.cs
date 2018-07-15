using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//电脑控制
public class ComController : MonoBehaviour {
    private Rigidbody2D r2d;
    private AudioSource audioSource;
    private Rigidbody2D ballR2d;
    private Transform ballTrans;//小球位置
    private Transform comTrans;//电脑位置
    private int number;
	// Use this for initialization
	void Start () {
        r2d = GetComponent<Rigidbody2D>();
        ballR2d = GameObject.Find("Ball").GetComponent<Rigidbody2D>();
        ballTrans= GameObject.Find("Ball").GetComponent<Transform>();
        comTrans = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }
	void Update () {
        //如果小球打过来
        if (ballR2d.velocity.x > 0)
        {
            //如果小球落点在电脑上方，电脑往上走
            if(ballTrans.position.y>comTrans.position.y&&ballTrans.position.y-comTrans.position.y-comTrans.localScale.y/2>=0.1f)
                r2d.velocity = new Vector2(0, 8);
            //如果小球落点在电脑下方，电脑往下走
            if (ballTrans.position.y < comTrans.position.y && comTrans.position.y - ballTrans.position.y - comTrans.localScale.y / 2 >= 0.1f)
                r2d.velocity = new Vector2(0, -8);
            //如果小球落点在电脑位置，电脑不动
            if (comTrans.position.y == BallManager.endPosY)
                r2d.velocity = Vector2.zero;
        }
        //如果小球卡在电脑后面，随机一个速度，移动电脑
        if (ballTrans.position.x > comTrans.position.x && ballR2d.velocity.x < 0)
        {
            number = Random.Range(0, 2);
            if(number==0)
                r2d.velocity = new Vector2(0, 10);
            if(number==1)
                r2d.velocity = new Vector2(0, 0);
            if(number==3)
                r2d.velocity = new Vector2(0, -10);
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
}
