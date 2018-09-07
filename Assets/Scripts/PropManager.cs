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
    public int CloneCount=0;
    private int count = 4;
    private List<GameObject> downList = new List<GameObject>();
    private List<GameObject> upList = new List<GameObject>();
    private List<GameObject> plusList = new List<GameObject>();
    private List<GameObject> minusList = new List<GameObject>();
    private List<GameObject> lifeList = new List<GameObject>();
    GameObject down ;
    GameObject up ;
    GameObject plus ;
    GameObject minus ;
    GameObject life;
    private float posX;
    private float posY;
    int index;
    private void Awake()
    {
        _instance = this;
        
    }
    private void Start()
    {
        InitPool();
    }
    //道具生成
    public void InitProp()
    {
        index = Random.Range(1, 6);
        GameObject initProp=GetProp(index);
        posX = Random.Range(-3.5f, 3.5f);
        posY = Random.Range(-4.75f, 4.75f);
        Vector2 propPos = new Vector2(posX, posY);
        initProp.transform.position = propPos;
        CloneCount++;
    }
    //初始化资源池
    private void InitPool()
    {
        for (int i = 0; i < count; i++)
        {
            CreateProp(1);
            CreateProp(2);
            CreateProp(3);
            CreateProp(4);
            CreateProp(5);

        }
    }
    //实例化道具，加入到列表中，并隐藏
    private GameObject CreateProp(int index)
    {
        GameObject go;
        if (index == 1)
        {
            go = Instantiate(Resources.Load("Down") as GameObject);
            go.SetActive(false);
            downList.Add(go);
        }
        else if (index == 2)
        {
            go = Instantiate(Resources.Load("Up") as GameObject);
            go.SetActive(false);
            upList.Add(go);
        }
       else if (index == 3)
        {
            go = Instantiate(Resources.Load("Plus") as GameObject);
            go.SetActive(false);
            plusList.Add(go);
        }
       else  if (index == 4)
        {
            go = Instantiate(Resources.Load("Minus") as GameObject);
            go.SetActive(false);
            minusList.Add(go);
        }
        else
        {
            go = Instantiate(Resources.Load("Life") as GameObject);
            go.SetActive(false);
            lifeList.Add(go);
        }
        return go;
    }
    //返回道具列表中还没有使用的道具对象，如果没有的话，则实例化新的道具
    public GameObject GetProp(int index)
    {
        if (index == 1)
        {
            foreach (GameObject down in downList)
            {
                if (down.activeInHierarchy == false)
                {
                    down.SetActive(true);
                    return down;
                }
            }
            return CreateProp(index);
        }
        else if(index == 2)
        {
            foreach (GameObject up in upList)
            {
                if (up.activeInHierarchy == false)
                {
                    up.SetActive(true);
                    return up;
                }
            }
            return CreateProp(index);
        }
        else if (index == 3)
        {
            foreach (GameObject plus in plusList)
            {
                if (plus.activeInHierarchy == false)
                {
                    plus.SetActive(true);
                    return plus;
                }
            }
            return CreateProp(index);
        }
        else if (index == 4)
        {
            foreach (GameObject minus in minusList)
            {
                if (minus.activeInHierarchy == false)
                {
                    minus.SetActive(true);
                    return minus;
                }
            }
            return CreateProp(index);
        }
        else
        {
            foreach (GameObject life in lifeList)
            {
                if (life.activeInHierarchy == false)
                {
                    life.SetActive(true);
                    return life;
                }
            }
            return CreateProp(index);
        }
    }
    //回收子弹到资源池中
    //回收成功返回true，失败返回false
    public bool PutBack(GameObject go)
    {
        if (downList.Contains(go)|| upList.Contains(go)|| plusList.Contains(go)||minusList.Contains(go)|| lifeList.Contains(go))
        {
            go.SetActive(false);
            return true;
        }
        return false;
    }
}
