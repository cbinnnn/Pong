using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;//引入EasyTouch插件
//Player控制
public class PlayerController : MonoBehaviour {
    private Rigidbody2D r2d;
    public float speed;//Player控制速度
    private AudioSource audioSource;
	void Start () {
        r2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
    //向上移动
    public void moveUp()
    {
        r2d.velocity = new Vector2(0, speed);
    }
    //向下移动
    public void moveDown()
    {
        r2d.velocity = new Vector2(0, -speed);
    }
    //不移动
    public void moveStop()
    {
        r2d.velocity = Vector2.zero;
    }
}
