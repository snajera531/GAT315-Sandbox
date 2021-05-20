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

    public abstract float Mass { get; }
    public abstract float Size { get; set; }
    public abstract eType Type { get; }

    public AABB aabb { get => new AABB(transform.position, Vector2.one * Size); }
    public Color Color { set => spriteRenderer.material.color = value; }
    public float Density { get; set; } = 1;

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
