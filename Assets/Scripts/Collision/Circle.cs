using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Circle
{
    public Vector2 Center { get; set; }
    public float Radius { get; set; }

    public Circle(Vector2 center, float radius)
    {
        this.Center = center;
        this.Radius = radius;
    }

    public bool Contains(Circle c)
    {
        Vector2 direction = Center - c.Center;
        float sqrDistance = direction.magnitude;
        float sqrRadius = (Radius + c.Radius) * (Radius + c.Radius);

        return (sqrDistance <= sqrRadius);
    }
}
