using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Shape : MonoBehaviour
{
    public enum eType
    {
        Circle,
        Box
    }

    public abstract eType Type { get; }
    public abstract float Mass { get; }
    public abstract float Size { get; set; }

    public float Density { get; set; } = 1;

    public Color Color { set => spriteRenderer.material.color = value; }

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
