using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public bool isCloneMenu;
    public bool isCloneComGame;
    public bool isClonePlayerGame;
    private AssetBundle shareAB;
    private AssetBundle menuuiAB;
    private AssetBundle gameuiAB;
    private AssetBundle playergamecanvasAB;
    private AssetBundle comgamecanvasAB;
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);        
    }
    private void Start()
    {
        shareAB = AssetBundle.LoadFromFile("AssetBundles/Android/share");
    }
    // Update is called once per frame
    void Update () {
        if (SceneManager.GetActiveScene().name == "Menu" && !isCloneMenu)
        {           
            AssetBundle menuuiAB = AssetBundle.LoadFromFile("AssetBundles/Android/menuui");
            AssetBundle menucanvasAB = AssetBundle.LoadFromFile("AssetBundles/Android/menucanvas");
            Instantiate(menucanvasAB.LoadAsset<GameObject>("MenuCanvas"));
            isCloneMenu = true;
            menucanvasAB.Unload(false);
            menuuiAB.Unload(false);
            if (gameuiAB != null)
            {
                gameuiAB.Unload(true);
            }            
        }
        if (SceneManager.GetActiveScene().name == "Player" && !isClonePlayerGame)
        {
            if (menuuiAB != null)
            {
                menuuiAB.Unload(true);
            }
            if (gameuiAB == null)
            {
                gameuiAB = AssetBundle.LoadFromFile("AssetBundles/Android/gameui");
            }
            if (playergamecanvasAB == null)
            {
                playergamecanvasAB = AssetBundle.LoadFromFile("AssetBundles/Android/playergamecanvas");
            }               
                Instantiate(playergamecanvasAB.LoadAsset<GameObject>("PlayerGameCanvas"));
                isClonePlayerGame = true;
                playergamecanvasAB.Unload(false);
        }
        if (SceneManager.GetActiveScene().name == "Com" && !isCloneComGame)
        {
            if (menuuiAB != null)
            {
                menuuiAB.Unload(true);
            }
            if (gameuiAB == null)
            {
                gameuiAB = AssetBundle.LoadFromFile("AssetBundles/Android/gameui");
            }
            if (comgamecanvasAB == null)
            {
                comgamecanvasAB = AssetBundle.LoadFromFile("AssetBundles/Android/comgamecanvas");
            }           
            Instantiate(comgamecanvasAB.LoadAsset<GameObject>("ComGameCanvas"));
            isCloneComGame = true;
            comgamecanvasAB.Unload(true);
        }           
    }
}
