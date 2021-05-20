using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BVH : BroadPhase
{
    BVHNode rootNode;

    public override void Build(AABB aabb, List<Body> bodies)
    {
        potentialColCount = 0;
        List<Body> sorted = new List<Body>(bodies);

        //sort along x axis
        sorted.Sort((x,y) => x.Position.x.CompareTo(y.Position.x));

        rootNode = new BVHNode(sorted);
    }

    public override void Draw()
    {
        rootNode?.Draw();
    }

    public override void Query(AABB aabb, List<Body> bodies)
    {
        rootNode.Query(aabb, bodies);
        potentialColCount += bodies.Count;
    }

    public override void Query(Body body, List<Body> bodies)
    {
        Query(body.shape.aabb, bodies);
    }
}
