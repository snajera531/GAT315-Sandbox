using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : Action
{    
    public GameObject original;
    public BodyEnumData bodyType;
    public FloatData damping;
    public FloatData density;
    public FloatData restitution;
    public FloatData size;
    public FloatData speed;

    bool action { get; set; } = false;
    bool oneTime { get; set; } = false;

    public override void StartAction()
    {
        action = true;
        oneTime = true;
    }

    public override void StopAction()
    {
        action = false;
    }

    void Update()
    {
        if (action && (oneTime || Input.GetKey(KeyCode.LeftControl)))
        {
            oneTime = false;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameObject = Instantiate(original, position, Quaternion.identity);
            if(gameObject.TryGetComponent<Body>(out Body body))
            {
                body.Damping = damping;
                body.shape.Size = size;
                body.shape.Density = density;
                body.Restitution = restitution;
                body.Type = (Body.eType)bodyType.value;

                Vector2 force = Random.insideUnitSphere.normalized * speed.value;
                body.AddForce(force, Body.eForceMode.Velocity);

                World.Instance.Bodies.Add(body);
            }
        }
    }
}
