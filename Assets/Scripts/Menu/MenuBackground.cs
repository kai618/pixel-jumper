﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour
{
    private Renderer quad1, quad2, quad3;

    public bool scrolling { get; set; } = false;

    void Start()
    {
        quad1 = GameObject.Find("Quad 1").GetComponent<Renderer>();
        quad2 = GameObject.Find("Quad 2").GetComponent<Renderer>();
        quad3 = GameObject.Find("Quad 3").GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        if (scrolling) MoveRightToLeft();
    }

    private void MoveRightToLeft()
    {
        float time = Time.deltaTime;

        quad1.material.mainTextureOffset += new Vector2(time * 0.03f, 0);
        quad2.material.mainTextureOffset += new Vector2(time * 0.01f, 0);
        quad3.material.mainTextureOffset += new Vector2(time * 0.006f, 0);
    }
}
