using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionHandler : Singleton<FunctionHandler>
{

    public Transform menuCanvas;
    public Transform winCanvas;
    public Transform uiCanvas;


    public Text menuText;

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }


    public void LevelComplete()
    {
        menuText.text = "YOU WIN";
        winCanvas.gameObject.SetActive(true);
        Time.timeScale =0f;
    }

    public void Pause()
    {
        //menuText.text = GameManager.Instance.Score.ToString();
        uiCanvas.gameObject.SetActive(!uiCanvas.gameObject.activeSelf);
        menuCanvas.gameObject.SetActive(!menuCanvas.gameObject.activeSelf);


        if(Time.timeScale == 1f)
            Time.timeScale = 0f;
        else if(Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    public void GameOver() 
    {
        menuText.text = GameManager.Instance.Score.ToString();
        uiCanvas.gameObject.SetActive(!uiCanvas.gameObject.activeSelf);
        winCanvas.gameObject.SetActive(!winCanvas.gameObject.activeSelf);

        if (Time.timeScale == 1f)
            Time.timeScale = 0f;
        else if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }


}
