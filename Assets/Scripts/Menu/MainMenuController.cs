using UnityEngine;
using System.Threading.Tasks;

public class MainMenuController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject player;
#pragma warning disable 0649

    private MenuBackground menuBackground;

    void Awake()
    {
        menuBackground = GameObject.Find("Background").GetComponent<MenuBackground>();
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
        player.GetComponent<Player>().SetRun(true);
        menuBackground.rotating = true;
    }
}
