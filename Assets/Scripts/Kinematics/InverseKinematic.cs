using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematic : MonoBehaviour
{
    public InverseKinematicSegment original;
    public Transform target;
    public int count = 5;
    [Range(0.1f, 3.0f)] public float length = 1;
    [Range(0.1f, 3.0f)] public float size = 1;

    List<InverseKinematicSegment> segments = new List<InverseKinematicSegment>();

    void Start()
    {
        KinematicSegment parent = null;
        for(int i = 0; i < count; i++)
        {
            var segment = Instantiate(original, transform);
            segment.Initialize(parent, transform.position, 0, length, 1);

            segments.Add(segment);
            parent = segment;
        }
    }

    void Update()
    {
        foreach(InverseKinematicSegment segment in segments)
        {
            segment.length = length;
            segment.size = size;

            Vector2 position = (segment.parent) ? segment.parent.start : (Vector2)target.position;
            segment.Follow(position);
        }
    }
}
