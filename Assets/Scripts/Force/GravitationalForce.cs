using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForce : MonoBehaviour
{
    public static void ApplyForce(List<Body> bodies, float G)
    {
        for(int i = 0; i < bodies.Count; i++)
        {
            for(int j = i + 1; j < bodies.Count; j++)
            {
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                Vector2 direction = bodyA.Position - bodyB.Position;
                float sqrDistance = Mathf.Max(direction.magnitude * direction.magnitude, 1);
                float force = G * (bodyA.Mass * bodyB.Mass) / sqrDistance;

                bodyA.AddForce(-direction.normalized, Body.eForceMode.Force);
                bodyA.AddForce(direction.normalized, Body.eForceMode.Force);
            }
        }
    }
}
