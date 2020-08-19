using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button zoomInBtn;
    [SerializeField] private Button zoomOutBtn;
#pragma warning disable 0649

    private TouchDetector touchDetector;
    private Camera levelCamera;
    private readonly float minCameraSize = 5;
    private readonly float maxCameraSize = 7;
    private GameObject pauseBtn;


    void Awake()
    {
        touchDetector = GameObject.Find("Touch Detector").GetComponent<TouchDetector>();
        levelCamera = GameObject.Find("Level Camera").GetComponent<Camera>();

        pauseBtn = GameObject.Find("Pause Btn");
    }

    public void PauseGame()
    {
        AudioController.instance.PlayPauseSFX();
        touchDetector.enabled = false;
        pauseBtn.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        AudioController.instance.PlaySelectSFX();
        pausePanel.SetActive(false);
        pauseBtn.SetActive(true);
        touchDetector.enabled = true;
        Time.timeScale = 1f;
    }

    public void ToMainMenu()
    {
        AudioController.instance.PlaySelectSFX();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void ZoomIn()
    {
        AudioController.instance.PlaySelectSFX();
        float size = levelCamera.orthographicSize;
        if (size > minCameraSize) size--;
        levelCamera.orthographicSize = size;
        ChangeZoomBtnState(size);
    }

    public void ZoomOut()
    {
        AudioController.instance.PlaySelectSFX();
        float size = levelCamera.orthographicSize;
        if (size < maxCameraSize) size++;
        levelCamera.orthographicSize = size;
        ChangeZoomBtnState(size);
    }

    private void ChangeZoomBtnState(float cameraSize)
    {
        if (cameraSize == minCameraSize)
        {
            zoomInBtn.interactable = false;
        }
        else if (cameraSize == maxCameraSize)
        {
            zoomOutBtn.interactable = false;
        }
        else
        {
            zoomInBtn.interactable = true;
            zoomOutBtn.interactable = true;
        }
    }
}
