using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData fixedFPS;
    public StringData fpsText;

    static World instance;
    static public World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> Bodies { get; set; } = new List<Body>();

    float timeAccumulator = 0;
    float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }
    float fpsAverage = 0;
    float smoothing = 0.975f;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        float fps = 1.0f / dt;
        fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));
        fpsText.value = "FPS: " + fpsAverage.ToString("F1");

        if (!simulate.value) return;

        timeAccumulator += Time.deltaTime;
        while(timeAccumulator >= fixedDeltaTime)
        {
            Bodies.ForEach(body => body.Step(dt));
            Bodies.ForEach(body => Integrator.SemiImplicitEuler(body, dt));

            timeAccumulator -= fixedDeltaTime;
        }


        Bodies.ForEach(body => body.Force = Vector2.zero);
        Bodies.ForEach(body => body.Acceleration = Vector2.zero);
    }
}
