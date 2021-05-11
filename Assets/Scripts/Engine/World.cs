using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData collision;
    public BoolData simulate;
    public BoolData wrap;
    public FloatData gravitation;
    public FloatData gravity;
    public FloatData fixedFPS;
    public StringData fpsText;
    public VectorField vectorField;

    static World instance;
    static public World Instance { get { return instance; } }

    public AABB AABB { get => aabb; }
    float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }
    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }

    public List<Body> Bodies { get; set; } = new List<Body>();
    public List<Force> Forces { get; set; } = new List<Force>();
    public List<SpringForce> Springs { get; set; } = new List<SpringForce>();

    AABB aabb;
    float timeAccumulator = 0;
    float fpsAverage = 0;
    float smoothing = 0.975f;
    Vector2 size;

    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
        aabb = new AABB(Vector2.zero, size * 2);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        float fps = 1.0f / dt;

        fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));
        fpsText.value = "FPS: " + fpsAverage.ToString("F1");
        Springs.ForEach(spring => spring.Draw());

        if (!simulate.value) return;

        GravitationalForce.ApplyForce(Bodies, gravitation.value);
        Forces.ForEach(force => Bodies.ForEach(body => force.ApplyForce(body)));
        Springs.ForEach(spring => spring.ApplyForce());
        Bodies.ForEach(body => vectorField.ApplyForce(body));

        //Bodies.ForEach(body => body.shape.Color = Color.magenta);

        timeAccumulator += Time.deltaTime;
        while(timeAccumulator >= fixedDeltaTime)
        {
            Bodies.ForEach(body => body.Step(fixedDeltaTime));
            Bodies.ForEach(body => Integrator.SemiImplicitEuler(body, fixedDeltaTime));

            Bodies.ForEach(body => body.shape.Color = Color.magenta);
            if (collision)
            {
                Collision.CreateContacts(Bodies, out List<Contact> contacts);
                contacts.ForEach(contact => { contact.bodyA.shape.Color = Color.green; contact.bodyB.shape.Color = Color.green; });
                ContactSolver.Resolve(contacts);
            }


            timeAccumulator -= fixedDeltaTime;
        }

        if (wrap)
        {
            Bodies.ForEach(body => body.Position = Utilities.Wrap(body.Position, -size, size));
        }

        Bodies.ForEach(body => body.Force = Vector2.zero);
        Bodies.ForEach(body => body.Acceleration = Vector2.zero);
    }
}
