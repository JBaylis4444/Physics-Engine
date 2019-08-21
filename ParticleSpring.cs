using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpring : ParticleForceGenerator {

	/*Particle at the other end of the spring*/
	MyParticle other;

	float springConstant;
	/* Uncompressed/unextended length */
	float restLength;

	public ParticleSpring (MyParticle other, float springConstant, float restLength)
	{
		this.other = other;
		this.springConstant = springConstant;
		this.restLength = restLength;
	}

	//finds absolute value of a number
	float absValue(float number){
		if (number >= 0)
			return number;
		else
			return -1 * number;
	}

	public void updateForce (MyParticle particle, float duration)
	{
		// Calculate spring vector
		MyVector3 force;

		force = particle.getPosition ();
		force.SubtractVectorFromThisVector (other.getPosition());

		//Calculate the magnitude of the force
		float magnitude = force.magnitude();
		magnitude = absValue (magnitude - this.restLength);
		magnitude *= springConstant;

		//Calculate the final force and apply it
		force.normalize();
		force.multiplyVectorByScalar (-1*magnitude);
		particle.addForce (force);
	}
}
