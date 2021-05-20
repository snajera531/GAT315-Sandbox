using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Collision
{
    public static bool TestOverlap(Body bodyA, Body bodyB)
    {
        if (bodyA.Type == Body.eType.Static && bodyB.Type == Body.eType.Static) return false;

        Circle circleA = new Circle(bodyA.Position, ((CircleShape)bodyA.shape).Radius);
        Circle circleB = new Circle(bodyB.Position, ((CircleShape)bodyB.shape).Radius);

        return circleA.Contains(circleB);
    }

    /*public static void CreateContacts(List<Body> bodies, out List<Contact> contacts)
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
    }*/

    public static void CreateNarrowPhaseContacts(ref List<Contact> contacts)
    {
        contacts.RemoveAll(contact => (TestOverlap(contact.bodyA, contact.bodyB) == false));
    }

    public static void UpdateContactInfo(ref Contact contact)
    {
        if (contact.bodyA.shape.Type == Shape.eType.Circle && contact.bodyB.shape.Type == Shape.eType.Circle)
        {
            Circle circleA = new Circle(contact.bodyA.Position, ((CircleShape)contact.bodyA.shape).Radius);
            Circle circleB = new Circle(contact.bodyB.Position, ((CircleShape)contact.bodyB.shape).Radius);

            Vector2 direction = circleA.Center - circleB.Center;
            float distance = direction.magnitude;
            contact.depth = (circleA.Radius + circleB.Radius) - distance;
            contact.normal = direction.normalized;
        }
    }

    public static void CreateBroadPhaseContacts(BroadPhase broadPhase, List<Body> bodies, out List<Contact> contacts)
    {
        contacts = new List<Contact>();

        List<Body> queryBodies = new List<Body>();
        foreach (Body body in bodies)
        {
            queryBodies.Clear();
            broadPhase.Query(body, queryBodies);
            foreach (Body queryBody in queryBodies)
            {
                if (queryBody == body) continue;

                Contact contact = new Contact() { bodyA = body, bodyB = queryBody };
                contacts.Add(contact);
            }
        }
    }
}