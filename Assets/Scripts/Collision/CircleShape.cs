using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShape : Shape
{
    public float Radius { get => Size * 0.5f; }

    public override float Mass => (Mathf.PI * Radius * Radius) * Density;
    public override float Size { get => transform.localScale.x; set => transform.localScale = Vector2.one * value; }
    public override eType Type => eType.Circle;
}
