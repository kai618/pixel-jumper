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
        Vector2 startPos = Vector2.zero;

        int skinCount = 1;
        bool[] skins = GameController.instance.Data.BoughtSkins;
        foreach (bool s in skins)
        {
            if (s) skinCount++;
        }

        // make the skin row look symmetric
        // if (skinCount % 2 == 0) startPos.x += -0.95f;

        AwakeNinjaFrog(startPos);
        if (skins[0]) AwakeVirtualGuy(startPos + new Vector2(-1.9f, -1));
        if (skins[1]) AwakePinkMan(startPos + new Vector2(1.9f, -0.5f));

    }

    private async void AwakeNinjaFrog(Vector2 pos)
    {

        await Task.Delay(500);

        // avoid errors when switching to other scene too fast
        if (this == null) return;

        GameObject frog = Instantiate(NinjaFrog, pos, Quaternion.identity);
        frog.name = "Ninja Frog";

        await Task.Delay(2000);
        if (this == null) return;

        frog.GetComponent<Player>().SetRun(true);

        menuBackground.scrolling = true;
        StartCoroutine(MakePlayerJump(frog));
    }
    private async void AwakeVirtualGuy(Vector2 pos)
    {
        await Task.Delay(700);
        if (this == null) return;

        GameObject guy = Instantiate(VirtualGuy, pos, Quaternion.identity);
        guy.name = "Virtual Guy";

        await Task.Delay(1800);
        if (this == null) return;

        guy.GetComponent<Player>().SetRun(true);

        StartCoroutine(MakePlayerJump(guy));
    }

    private async void AwakePinkMan(Vector2 pos)
    {
        await Task.Delay(900);
        if (this == null) return;

        GameObject man = Instantiate(PinkMan, pos, Quaternion.identity);
        man.name = "Pink Man";

        await Task.Delay(1600);
        if (this == null) return;

        man.GetComponent<Player>().SetRun(true);

        StartCoroutine(MakePlayerJump(man));
    }

    private IEnumerator MakePlayerJump(GameObject player)
    {
        Player playerScript = player.GetComponent<Player>();
        float duration = Random.Range(4, 8);
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
        SceneManager.LoadScene("ShopScene");
    }
}
