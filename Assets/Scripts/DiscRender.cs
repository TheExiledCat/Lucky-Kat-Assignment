﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscRender : MonoBehaviour
{
    MaterialPropertyBlock m;
    Renderer r;
    [SerializeField]Color c;
    private void Start()
    {
        m = new MaterialPropertyBlock();
        r = GetComponent<MeshRenderer>();
        m.SetColor("Color", c);
        r.SetPropertyBlock(m);
    }
    public void SetColor(Color _c)
    {
        c = _c;
    }
}
