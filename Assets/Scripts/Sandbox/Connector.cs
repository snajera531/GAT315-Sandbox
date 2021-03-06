using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : Action
{
	public BoolData distanceLength;
	public FloatData springK;
	public FloatData springLength;

	public override eActionType ActionType => eActionType.Connector;

	bool action { get; set; } = false;
	Body source { get; set; } = null;

	public override void StartAction()
	{
		Body body = Utilities.GetBodyFromPosition(Input.mousePosition);
		if (body != null)
		{
			source = body;
			action = true;
		}
	}

	public override void StopAction()
	{
		if (source != null)
		{
			Body destination = Utilities.GetBodyFromPosition(Input.mousePosition);
			if (destination != null && destination != source)
			{
				float restLength = distanceLength ? (source.Position - destination.Position).magnitude : springLength;
				Create(source, destination, restLength, springK);
			}
		}

		source = null;
		action = false;
	}

	void Update()
	{
		if (source != null)
		{
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Lines.Instance.AddLine(source.Position, position, Color.white, 0.1f);
		}
	}

	void Create(Body bodyA, Body bodyB, float restLength, float k)
	{
		SpringForce spring = new SpringForce();
		spring.bodyA = bodyA;
		spring.bodyB = bodyB;
		spring.restLength = restLength;
		spring.k = k;

		World.Instance.Springs.Add(spring);
	}
}
