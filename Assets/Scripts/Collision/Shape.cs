using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : MonoBehaviour
{
    public enum eType
    {
        Circle,
        Box
    }

    public abstract eType Type { get; }
    public abstract float Mass { get; }

    public float Density { get; set; } = 1;
}
