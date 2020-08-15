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

    public void ReviveMe(GameObject me, Vector2 pos, float time = 0)
    {
        if (time > 0) StartCoroutine(ReviveLater(me, pos, time));
        else ReviveNow(me, pos);
    }


    private IEnumerator ReviveLater(GameObject me, Vector2 pos, float time)
    {
        yield return new WaitForSeconds(time);
        ReviveNow(me, pos);
    }

    private void ReviveNow(GameObject me, Vector2 pos)
    {
        me.transform.position = pos;
        me.SetActive(true);
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
