using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBungie: ParticleForceGenerator
{
	/*Particle at the other end of the spring*/
	MyParticle other;


	float springConstant;
	/* Uncompressed/unextended length */
	float restLength;

	public ParticleBungie (MyParticle other, float springConstant, float restLength)
	{
		this.other = other;
		this.springConstant = springConstant;
		this.restLength = restLength;
	}

	public void updateForce (MyParticle particle, float duration)
	{
		// Calculate spring vector
		MyVector3 force;

		force = particle.getPosition ();
		force.SubtractVectorFromThisVector (other.getPosition());

		//Check is the bungie is compressed
		// if bungie is compressed break out of the method without adding a force
		float magnitude = force.magnitude();


		if (magnitude < restLength)
			return;
		else if (magnitude > restLength) {
			//Everything after this comment will happen if the if condition isn't true
			//Calculate the magnitude of the force
			magnitude = springConstant * (restLength - magnitude);
			//magnitude = springConstant * (magnitude-restLength);

			//Calculate the final force and apply it
			force.normalize ();
			force.multiplyVectorByScalar (magnitude);
			particle.addForce (force);
		} else
			return;

	}
}


