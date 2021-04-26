using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxShape : Shape
{
    public override float Mass => (Size * Size) * Density;
    public override float Size { get => transform.localScale.x; set => transform.localScale = Vector2.one * value; }
    public override eType Type => eType.Box;
}
