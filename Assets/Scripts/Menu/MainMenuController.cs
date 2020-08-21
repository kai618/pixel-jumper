using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject levelPanel;

    private MenuBackground menuBackground;

    public GameObject selector;

    public GameObject NinjaFrog;
    public GameObject VirtualGuy;
    public GameObject PinkMan;

    void Awake()
    {
        menuBackground = GameObject.Find("Background").GetComponent<MenuBackground>();
    }

    void Start()
    {
        AwakeSelectedHero();
    }

    private void AwakeSelectedHero()
    {
        AwakeNinjaFrog();
        bool[] skins = GameController.instance.Data.BoughtSkins;
        if (skins[0]) AwakeVirtualGuy();
        if (skins[1]) AwakePinkMan();
    }

    private async void AwakeNinjaFrog()
    {
        try
        {
            await Task.Delay(500);
            GameObject frog = Instantiate(NinjaFrog, Vector2.zero, Quaternion.identity);
            frog.name = "Ninja Frog";

            await Task.Delay(2000);
            frog.GetComponent<Player>().SetRun(true);

            menuBackground.scrolling = true;
            StartCoroutine(MakePlayerJump(frog));
        }
        catch { }
    }
    private async void AwakeVirtualGuy()
    {
        try
        {
            await Task.Delay(700);

            GameObject guy = Instantiate(VirtualGuy, new Vector2(-1.9f, -1), Quaternion.identity);
            guy.name = "Virtual Guy";

            await Task.Delay(1800);
            guy.GetComponent<Player>().SetRun(true);

            StartCoroutine(MakePlayerJump(guy));
        }
        catch { }
    }

    private async void AwakePinkMan()
    {
        try
        {
            await Task.Delay(900);

            GameObject man = Instantiate(PinkMan, new Vector2(1.9f, -0.5f), Quaternion.identity);
            man.name = "Pink Man";

            await Task.Delay(1600);
            man.GetComponent<Player>().SetRun(true);

            StartCoroutine(MakePlayerJump(man));
        }
        catch { }
    }

    private IEnumerator MakePlayerJump(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        float duration = Random.Range(5, 8);
        yield return new WaitForSeconds(duration);

        float distanceY = Random.Range(1f, 1.7f);
        Jump jump = playerScript.GenerateJump(new Vector2(0, distanceY));
        playerScript.SetJump(jump);

        StartCoroutine(MakePlayerJump(player));
    }

    public void ToLevelPanel()
    {
        AudioController.instance.PlaySelectSFX();
        menuPanel.SetActive(false);
        levelPanel.SetActive(true);
        selector.SetActive(true);
    }

    public void ToMenuPanel()
    {
        AudioController.instance.PlaySelectSFX();
        menuPanel.SetActive(true);
        levelPanel.SetActive(false);
        selector.SetActive(false);
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
