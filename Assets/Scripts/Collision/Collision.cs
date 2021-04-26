using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Collision
{
    public static void CreateContacts(List<Body> bodies, out List<Contact> contacts)
    {
        contacts = new List<Contact>();

        for (int i = 0; i < bodies.Count; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                if (bodyA.Type == Body.eType.Static && bodyB.Type == Body.eType.Static) continue;

                Circle circleA = new Circle(bodyA.Position, ((CircleShape)bodyA.shape).Radius);
                Circle circleB = new Circle(bodyB.Position, ((CircleShape)bodyB.shape).Radius);

                if (circleA.Contains(circleB))
                {
                    Contact contact = new Contact() { bodyA = bodyA, bodyB = bodyB };

                    Vector2 direction = circleA.Center - circleB.Center;
                    float distance = direction.magnitude;
                    contact.depth = (circleA.Radius + circleB.Radius) - distance;
                    contact.normal = direction.normalized;

                    contacts.Add(contact);
                }
            }
        }
    }
}
