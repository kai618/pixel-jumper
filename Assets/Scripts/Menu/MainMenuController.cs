using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
#pragma warning disable 0649
    public GameObject player;
    public GameObject menuPanel;
    public GameObject levelPanel;
#pragma warning disable 0649

    private MenuBackground menuBackground;
    private Player playerScript;

    void Awake()
    {
        menuBackground = GameObject.Find("Background").GetComponent<MenuBackground>();
        playerScript = player.GetComponent<Player>();
    }

    void Start()
    {
        AwakePlayer();
    }

    private async void AwakePlayer()
    {
        try
        {
            await Task.Delay(500);
            player.SetActive(true);

            await Task.Delay(2000);
            playerScript.SetRun(true);

            menuBackground.rotating = true;
            StartCoroutine(MakePlayerJump());
        }
        catch { }
    }

    private IEnumerator MakePlayerJump()
    {
        float duration = Random.Range(6, 8);
        yield return new WaitForSeconds(duration);

        float distanceY = Random.Range(1f, 1.5f);
        Jump jump = playerScript.GenerateJump(new Vector2(0, distanceY));
        playerScript.SetJump(jump);

        StartCoroutine(MakePlayerJump());
    }



    public void ToLevelPanel()
    {
        AudioController.instance.PlaySelectSFX();
        menuPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void ToMenuPanel()
    {
        AudioController.instance.PlaySelectSFX();
        menuPanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    public void ToLevel01()
    {
        AudioController.instance.PlaySelectSFX();
        SceneManager.LoadScene("HieuTestScene");
    }
    public void ToLevel02()
    {
        AudioController.instance.PlaySelectSFX();
        SceneManager.LoadScene("Level_2");
    }

    public void ToLevel03()
    {

    }

    public void ToShop()
    {
        AudioController.instance.PlaySelectSFX();
        Debug.Log("Shop");
    }
}
