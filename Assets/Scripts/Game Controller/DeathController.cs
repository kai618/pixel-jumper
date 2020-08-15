using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathController : MonoBehaviour
{
    public int playerDeathCount { get; private set; } = 0;

    private Text deathCountText;

    void Start()
    {
        deathCountText = GameObject.Find("Death Count").GetComponent<Text>();
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

    public void IncrementPlayerDeath()
    {
        playerDeathCount++;
        RenderPlayerDeathCount();
    }

    void RenderPlayerDeathCount()
    {
        deathCountText.text = playerDeathCount.ToString();
    }
}
