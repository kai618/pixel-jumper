using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject pausePanel;
#pragma warning disable 0649

    private TouchDetector touchDetector;
    private GameObject pauseBtn;

    void Awake()
    {
        touchDetector = GameObject.Find("Touch Detector").GetComponent<TouchDetector>();
        pauseBtn = GameObject.Find("Pause Btn");
    }  

    public void PauseGame()
    {
        touchDetector.enabled = false;
        pauseBtn.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        pauseBtn.SetActive(true);
        touchDetector.enabled = true;
        Time.timeScale = 1f;
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
