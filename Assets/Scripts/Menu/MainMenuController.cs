using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject player;
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
        await Task.Delay(2000);
        player.SetActive(true);

        await Task.Delay(2000);
        playerScript.SetRun(true);
        menuBackground.rotating = true;

        StartCoroutine(MakePlayerJump());
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

    public void ToLevelChooser() {
        SceneManager.LoadScene("HieuTestScene");
    }
}
