using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactSolver
{
    public static void Resolve(List<Contact> contacts)
    {
        foreach(Contact contact in contacts)
        {
            //separation
            float totalInverseMass = contact.bodyA.InverseMass + contact.bodyB.InverseMass;
            Vector2 separation = contact.normal * contact.depth / totalInverseMass;
            contact.bodyA.Position += separation * contact.bodyA.InverseMass;
            contact.bodyB.Position = contact.bodyB.Position - separation * contact.bodyB.InverseMass;

            //collision impulse
            Vector2 relativeVelocity = contact.bodyA.Velocity - contact.bodyB.Velocity;
            float normalVelocity = Vector2.Dot(relativeVelocity, contact.normal);

            if (normalVelocity > 0) continue;

            float restitution = (contact.bodyA.Restitution + contact.bodyB.Restitution) / 2;
            float impulseMagnitude = -(1.0f + restitution) * normalVelocity / totalInverseMass;

            Vector2 impulse = contact.normal * impulseMagnitude;
            contact.bodyA.AddForce(contact.bodyA.Velocity + (impulse * contact.bodyA.InverseMass), Body.eForceMode.Velocity);
            contact.bodyB.AddForce(contact.bodyB.Velocity - (impulse * contact.bodyB.InverseMass), Body.eForceMode.Velocity);
        }
    }
}
