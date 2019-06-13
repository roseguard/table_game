using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu menu;

    public GameObject MainMenuCanvas;
    public GameObject GamePlayCanvas;
    public GameObject RestartCanvas;
    public GameObject PauseCanvas;
    public GameObject GamePlayElements;
    public GameObject GamePlayElementsOther;
    public GameObject Fade;
    public string mainMenu;

    void Start()
    {
        menu = this;

        MainMenuCanvas.SetActive(true);
        GamePlayCanvas.SetActive(false);
        GamePlayElements.SetActive(false);
        GamePlayElementsOther.SetActive(false);
        RestartCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        Fade.SetActive(false);
    }
    public void StartAnimation()
    {
        Fade.SetActive(true);
        Invoke("StartGame", 1f);
    }

    public void StartGame()
    {
        MainMenuCanvas.SetActive(false);
        GamePlayCanvas.SetActive(true);
        GamePlayElements.SetActive(true);
        GamePlayElementsOther.SetActive(true);
        //Invoke("FadeOnAnimation", 1f);
        FadeOffAnimation();
        Debug.Log("Show game elements");
    }

    public void FadeOnAnimation()
    {
        Fade.SetActive(true);
        Invoke("FadeOffAnimation", 1f);
    }

    public void FadeOffAnimation()
    {
        Fade.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        //MainMenuCanvas.SetActive(false);
        //GamePlayCanvas.SetActive(true);
        //GamePlayElements.SetActive(true);
        //Invoke("FadeInOutAnimation", 1f);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void restartCanvasOn()
    {
        RestartCanvas.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseCanvas.SetActive(false);
    }

    public void Restart()
    {
        GamePlayCanvas.SetActive(false);
        GamePlayElements.SetActive(false);
        GamePlayElementsOther.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
        PauseCanvas.SetActive(false);
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
        PauseCanvas.SetActive(false);
    }
}
