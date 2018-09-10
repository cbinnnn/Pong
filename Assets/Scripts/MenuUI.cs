using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {
    private void Start()
    {
        UIManager.Instance.isCloneComGame = false;
        UIManager.Instance.isClonePlayerGame = false;
    }
    //加载Player场景
    public void PlayerBtn()
    {        
        SceneManager.LoadScene("Player");
    }
    //加载Com场景
    public void ComBtn()
    {
        SceneManager.LoadScene("Com");
    }
}
