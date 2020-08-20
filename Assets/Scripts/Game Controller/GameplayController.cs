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

    [SerializeField] private GameObject HudCanvas;
    [SerializeField] private GameObject resultCanvas;
#pragma warning disable 0649

    private TouchDetector touchDetector;
    private Camera levelCamera;
    private readonly float minCameraSize = 5;
    private readonly float maxCameraSize = 7;
    private GameObject pauseBtn;

    private Player selecteddPlayer;

    public GameObject NinjaFrog;

    void Awake()
    {
        CreatePlayer();
        touchDetector = GameObject.Find("Touch Detector").GetComponent<TouchDetector>();
        levelCamera = GameObject.Find("Level Camera").GetComponent<Camera>();
        pauseBtn = GameObject.Find("Pause Btn");

    }

    private void CreatePlayer()
    {
        if (GameController.instance.Data.SelectedPlayer == 0)
        {
            GameObject gameObject = Instantiate(NinjaFrog, GetStartingPlayerPosition(), Quaternion.identity);
            gameObject.name = "Player";

            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Foreground";
            sr.sortingOrder = 3;

            selecteddPlayer = gameObject.GetComponent<Player>();
        }
    }

    private Vector3 GetStartingPlayerPosition()
    {
        if (SceneManager.GetActiveScene().name == "Level_01")
        {

        }
        else if (SceneManager.GetActiveScene().name == "Level_02")
        {

        }
        return new Vector3(5, 14, 0);
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

    public void FinishLevel()
    {
        selecteddPlayer.EndLevel();
        HudCanvas.SetActive(false);
        resultCanvas.SetActive(true);
    }
}
