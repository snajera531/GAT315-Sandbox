using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public enum eForceMode
    {
        Force,
        Acceleration,
        Velocity
    }

    public float Damping { get; set; } = 0;
    public float Mass { get; set; } = 1;

    public Shape shape;

    public Vector2 Acceleration { get; set; } = Vector2.zero;
    public Vector2 Force { get; set; } = Vector2.zero;
    public Vector2 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector2 Velocity { get; set; } = Vector2.zero;

    public void AddForce(Vector2 force, eForceMode forceMode = eForceMode.Force)
    {
        switch (forceMode)
        {
            case eForceMode.Force:
                this.Force += force;
                break;
            case eForceMode.Acceleration:
                Acceleration = force;
                break;
            case eForceMode.Velocity:
                Velocity = force;
                break;
            default:
                break;
        }


    }

    public void Step(float dt)
    {
        Acceleration = new Vector2(0, -9.81f) + (Force / Mass);
    }
}
