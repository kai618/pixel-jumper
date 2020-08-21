using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [Min(1)]
    public int level = 1;

    public Vector2 playerStartPos;

    public string nextScene
    {
        get { return "Level_" + (level + 1); }
        set
        {
            nextScene = value;
        }
    }
}
