using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShape : Shape
{
    public float Radius { get => transform.localScale.x * 0.5f; set => transform.localScale = Vector2.one * value; }

    public override eType Type => Shape.eType.Circle;
    public override float Mass => (Mathf.PI * Radius * Radius) * Density;
}
