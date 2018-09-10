using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    public Text Score1Text;
    public Text Score2Text;
    public Text OverText;
    private void Start()
    {
        UIManager.Instance.isCloneMenu = false;
    }
    private void Update()
    {
        Score1Text.text = GameManager.Instance.score1.ToString();
        Score2Text.text = GameManager.Instance.score2.ToString();
        if (GameManager.Instance.score1 == 15)
        {
            OverText.text = "Com Win!!!";
            OverText.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.score2 == 15)
        {
            OverText.text = "Player1 Win!!!";
            OverText.gameObject.SetActive(true);
        }
    }
    public void ResetBtn()
    {
        if (SceneManager.GetActiveScene().name == "Player")
        {
            UIManager.Instance.isClonePlayerGame = false;
        }
        else if (SceneManager.GetActiveScene().name == "Com")
        {
            UIManager.Instance.isCloneComGame = false;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void AgainBtn()
    {
        if (SceneManager.GetActiveScene().name == "Player")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Player");
        }
        else if (SceneManager.GetActiveScene().name == "Com")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Com");
        }        
    }
}
