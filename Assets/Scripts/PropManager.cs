using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//道具管理类
public class PropManager : MonoBehaviour {
    //设置成单例模式
    private static PropManager _instance;
    public static PropManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject[] propArr;//道具数组
    private float posX;
    private float posY;
    int index;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        InitProp();//第一个道具
    }
    //道具生成
    public void InitProp()
    {
        index = Random.Range(0, propArr.Length);
        posX = Random.Range(-3.5f, 3.5f);
        posY = Random.Range(-4.75f, 4.75f);
        Vector2 propPos = new Vector2(posX, posY);
        GameObject.Instantiate(propArr[index], propPos, Quaternion.identity);
    }
}
