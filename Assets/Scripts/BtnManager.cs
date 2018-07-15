using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//按钮管理类
public class BtnManager : MonoBehaviour {
    //加载Player场景
	public void playerBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Player");
    }
    //加载Com场景
    public void comBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Com");
    }
    //加载Menu场景
    public void menuBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
