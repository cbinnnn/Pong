using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//游戏逻辑管理类
public class GameManager : MonoBehaviour
{
    //设置单例模式
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    //墙体碰撞器
    private BoxCollider2D upwall;
    private BoxCollider2D rightwall;
    private BoxCollider2D downwall;
    private BoxCollider2D leftwall;
    //Player位置
    public Transform Player1;
    public Transform Player2;
    //分数
    public  int score1;
    public  int score2;
    //Text UI
    public Text Score1Text;
    public Text Score2Text;
    public GameObject overText;
    public int PropCount = 1;
    private void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {
        ResetWall();
        ResetPlayer();
    }
    private void Update()
    {
        FillProp();
        PropManager.Instance.EndLife();
    }
    //初始化墙体
    void ResetWall()
    {
        upwall = transform.Find("upWall").GetComponent<BoxCollider2D>();
        rightwall = transform.Find("rightWall").GetComponent<BoxCollider2D>();
        downwall = transform.Find("downWall").GetComponent<BoxCollider2D>();
        leftwall = transform.Find("leftWall").GetComponent<BoxCollider2D>();

        Vector3 tempPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        upwall.transform.position = new Vector3(0, tempPos.y  + 0.5f, 0);
        upwall.size = new Vector2(tempPos.x * 2, 1);

        downwall.transform.position = new Vector3(0, -tempPos.y - 0.5f, 0);
        downwall.size = new Vector2(tempPos.x * 2, 1);

        rightwall.transform.position = new Vector3(tempPos.x + 0.5f, 0,0);
        rightwall.size = new Vector2(1,tempPos.y * 2);

        leftwall.transform.position = new Vector3(-tempPos.x - 0.5f, 0, 0);
        leftwall.size = new Vector2(1, tempPos.y * 2);
    }
    //初始化Player
    void ResetPlayer()
    {
        Vector2 Player1Pos = Camera.main.ScreenToWorldPoint(new Vector2(300, Screen.height/2));
        Vector2 Player2Pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width-300, Screen.height / 2));
        Player1.position = new Vector3(Player1Pos.x, Player1Pos.y, 0);
        Player2.position = new Vector3(Player2Pos.x, Player2Pos.y, 0);
    }
    public void FillProp()
    {
        for(int i = 0; i < PropCount - PropManager.Instance.CloneCount; i++)
        {
            PropManager.Instance.InitProp();
        }
    }
    //分数处理函数
    public void ChangeScore(string wallName)
    {
        if (wallName == "leftWall")
            score1++;
        else if (wallName == "rightWall")
            score2++;

        Score1Text.text = score1.ToString();
        Score2Text.text = score2.ToString();
        if (score1 == 3 || score2 == 3)
        {
            PropCount = 2;
            StartCoroutine(PropManager.Instance.InitLife());
        }
        if (score1 == 6 || score2 == 6)
        {
            PropCount = 3;
            StartCoroutine(PropManager.Instance.InitLife());
        }
        if (score1 == 9 || score2 == 9)
        {
            PropCount = 3;
            StartCoroutine(PropManager.Instance.InitLife());
        }
        if (score1 == 12 || score2 == 12)
        {
            PropCount = 4;
            StartCoroutine(PropManager.Instance.InitLife());
        }
        if (score1==15||score2==15)
        {
            if (score1 == 15)
                overText.GetComponent<Text>().text = "Com Win!!!";
            else
                overText.GetComponent<Text>().text = "Player1 Win!!!";
            GameOver();
        }
    }
    //游戏结束
    public void GameOver()
    {
        Time.timeScale = 0;
        overText.SetActive(true);
    }
}
