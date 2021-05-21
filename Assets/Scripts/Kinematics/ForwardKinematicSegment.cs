using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardKinematicSegment : KinematicSegment
{
    [Range(-90, 90)] public float inputAngle;
    float baseAngle;

    void Update()
    {
        angle = (parent != null) ? (inputAngle + parent.angle) : (inputAngle + baseAngle);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void Initialize(KinematicSegment parent, Vector2 position, float angle, float length, float width)
    {
        this.parent = parent;
        this.width = width;

        this.angle = angle;
        this.length = length;

        start = position;
        baseAngle = angle;
    }
}
