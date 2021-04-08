using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : Action
{
    public GameObject original;
    public FloatData damping;
    public FloatData speed;

    bool action { get; set; } = false;

    public override void StartAction()
    {
        action = true;
    }

    public override void StopAction()
    {
        action = false;
    }

    void Update()
    {
        if (action)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameObject = Instantiate(original, position, Quaternion.identity);
            if(gameObject.TryGetComponent<Body>(out Body body))
            {
                Vector2 force = Random.insideUnitSphere.normalized * speed.value;
                body.AddForce(force, Body.eForceMode.Velocity);
                body.Damping = damping.value;
                World.Instance.Bodies.Add(body);
            }
        }
    }
}
