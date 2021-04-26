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

    public enum eType
    {
        Dynamic,
        Kinematic, 
        Static
    }

    public Shape shape;

    public eType Type;

    public float Damping { get; set; } = 0;
    public float InverseMass { get => (Mass == 0) ? 0 : 1 / Mass; }
    public float Mass { get => shape.Mass; }
    public float Restitution { get; set; } = 0.5f;

    public Vector2 Acceleration { get; set; } = Vector2.zero;
    public Vector2 Force { get; set; } = Vector2.zero;
    public Vector2 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector2 Velocity { get; set; } = Vector2.zero;

    public void AddForce(Vector2 force, eForceMode forceMode = eForceMode.Force)
    {
        if (Type != eType.Static) return;
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
        if (Type != eType.Dynamic) return;
        Acceleration = World.Instance.Gravity + (Force / Mass);
    }
}
