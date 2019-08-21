using System;
using System.Collections.Generic;
using UnityEngine;

/*Spring force with a fixed end*/
public class ParticleAnchoredSpring : ParticleForceGenerator
{
	MyVector3 anchor;
	float springConstant;
	float restLength;

	public ParticleAnchoredSpring (MyVector3 anchor, float springConstant, float restLength)
	{
		this.anchor = anchor;
		this.springConstant = springConstant;
		this.restLength = restLength;
	}

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
		force.SubtractVectorFromThisVector (anchor);

		//Calculate the magnitude of the force
		float magnitude = force.magnitude();
		magnitude = absValue(magnitude - this.restLength);
		magnitude *= springConstant;

		//Calculate the final force and apply it
		force.normalize();
		force.multiplyVectorByScalar (-1*magnitude);
		particle.addForce (force);

	}
}


