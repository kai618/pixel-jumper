using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public GameObject pausePanel;
    public Button zoomInBtn;
    public Button zoomOutBtn;

    public GameObject HudCanvas;
    public GameObject resultCanvas;

    private TouchDetector touchDetector;
    private Camera levelCamera;
    private readonly float minCameraSize = 5;
    private readonly float maxCameraSize = 7;
    private GameObject pauseBtn;

    private Player selecteddPlayer;

    public GameObject NinjaFrog;
    public GameObject VirtualGuy;
    public GameObject PinkMan;

    private LevelInfo levelInfo;

    void Awake()
    {
        touchDetector = GameObject.Find("Touch Detector").GetComponent<TouchDetector>();
        levelCamera = GameObject.Find("Level Camera").GetComponent<Camera>();
        pauseBtn = GameObject.Find("Pause Btn");
        levelInfo = GetComponent<LevelInfo>();

        CreatePlayer();
    }

    private void CreatePlayer()
    {
        // Debug.Log(GameController.instance.Data.SelectedPlayer);
        // if (GameController.instance.Data.SelectedPlayer == 0)
        // {
        GameObject gameObject = Instantiate(NinjaFrog, levelInfo.playerStartPos, Quaternion.identity);
        gameObject.name = "Player";

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "Foreground";
        sr.sortingOrder = 3;

        selecteddPlayer = gameObject.GetComponent<Player>();
        // }
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

    public void ToNextLevel()
    {
        SceneManager.LoadScene(levelInfo.nextScene);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        CollectibleController cc = GetComponent<CollectibleController>();
        DeathController dc = GetComponent<DeathController>();
        GameData data = GameController.instance.Data;

        selecteddPlayer.FinishLevel();
        HudCanvas.SetActive(false);
        resultCanvas.SetActive(true);

        if (data.ReachedLevel <= levelInfo.level) resultCanvas.GetComponent<ResultCanvas>().ShowFirstRunText();
        else resultCanvas.GetComponent<ResultCanvas>().ShowHNotFirstRunText();

        data.MoneyTotal += cc.levelMoneySum;
        data.DeathCount += dc.levelDeathCount;
        data.ReachedLevel = levelInfo.level + 1;
        GameController.instance.PersistData();
    }
}
