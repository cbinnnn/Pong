using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public AssetBundle wallAB;
    public AssetBundle audiosAB;
    public AssetBundle maingoAB;
    public AssetBundle maingouiAB;
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

    public GameObject overText;
    public int PropCount = 1;
    private void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {
        UIManager.Instance.isClonePlayerGame = false;
        UIManager.Instance.isCloneComGame = false;
        wallAB = AssetBundle.LoadFromFile("AssetBundles/Android/wall");
        maingouiAB = AssetBundle.LoadFromFile("AssetBundles/Android/maingoui");
        maingoAB = AssetBundle.LoadFromFile("AssetBundles/Android/maingo");
        audiosAB = AssetBundle.LoadFromFile("AssetBundles/Android/audios");
        ResetWall();
        ResetPlayer();
        ResetBall();
        maingoAB.Unload(false);
        audiosAB.Unload(false);
    }
    private void Update()
    {
        FillProp();
        PropManager.Instance.EndLife();
    }
    //初始化墙体
    void ResetWall()
    {
        GameObject upGo = Instantiate(wallAB.LoadAsset<GameObject>("upWall"));
        GameObject rightGo = Instantiate(wallAB.LoadAsset<GameObject>("rightWall"));
        GameObject downGo = Instantiate(wallAB.LoadAsset<GameObject>("downWall"));
        GameObject leftGo = Instantiate(wallAB.LoadAsset<GameObject>("leftWall"));
        upwall = upGo.transform.GetComponent<BoxCollider2D>();
        rightwall = rightGo.transform.GetComponent<BoxCollider2D>();
        downwall = downGo.transform.GetComponent<BoxCollider2D>();
        leftwall = leftGo.transform.GetComponent<BoxCollider2D>();

        Vector3 tempPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        upwall.transform.position = new Vector3(0, tempPos.y  + 0.5f, 0);
        upwall.size = new Vector2(tempPos.x * 2, 1);

        downwall.transform.position = new Vector3(0, -tempPos.y - 0.5f, 0);
        downwall.size = new Vector2(tempPos.x * 2, 1);

        rightwall.transform.position = new Vector3(tempPos.x + 0.5f, 0,0);
        rightwall.size = new Vector2(1,tempPos.y * 2);

        leftwall.transform.position = new Vector3(-tempPos.x - 0.5f, 0, 0);
        leftwall.size = new Vector2(1, tempPos.y * 2);
        wallAB.Unload(false);
    }
    //初始化Player
    void ResetPlayer()
    {
        GameObject player1Go;
        GameObject player2Go;
        if (SceneManager.GetActiveScene().name == "Player")
        {
            player1Go= Instantiate(maingoAB.LoadAsset<GameObject>("Player1"));
            player2Go= Instantiate(maingoAB.LoadAsset<GameObject>("Player2"));
            Player1 = player1Go.GetComponent<Transform>();
            Player2 = player2Go.GetComponent<Transform>();
        }
        else if (SceneManager.GetActiveScene().name == "Com")
        {
            player1Go = Instantiate(maingoAB.LoadAsset<GameObject>("Player1"));
            player2Go = Instantiate(maingoAB.LoadAsset<GameObject>("Com"));
            Player1 = player1Go.GetComponent<Transform>();
            Player2 = player2Go.GetComponent<Transform>();
        }                
        Vector2 Player1Pos = Camera.main.ScreenToWorldPoint(new Vector2(300, Screen.height/2));
        Vector2 Player2Pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width-300, Screen.height / 2));
        Player1.position = new Vector3(Player1Pos.x, Player1Pos.y, 0);
        Player2.position = new Vector3(Player2Pos.x, Player2Pos.y, 0);
    }
    void ResetBall()
    {
        GameObject ballGo = Instantiate(maingoAB.LoadAsset<GameObject>("Ball"));
    }
    public void FillProp()
    {
        for(int i = 0; i < PropCount - PropManager.Instance.CloneCount; i++)
        {
            PropManager.Instance.InitProp();
        }
    }
    //分数处理函数
    public void ChangeScore(GameObject wall)
    {
        if (wall.CompareTag("leftWall"))
            score1++;
        else if (wall.CompareTag("rightWall"))
            score2++;
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
            GameOver();
        }
    }
    //游戏结束
    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
