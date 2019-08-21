using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDrag : ParticleForceGenerator  {

	//Holds velocity coefficient
	float k1;

	//Holds velocity squared coefficient
	float k2;

	public ParticleDrag(float k1, float k2){
		this.k1 = k1;
		this.k2 = k2;
	}

	public void updateForce (MyParticle particle, float duration)
	{

		MyVector3 force;
		force = particle.getVelocity ();

		//Calculate Total Drag coefficient
		float dragCoeff = force.magnitude();
		dragCoeff = k1 * dragCoeff + k2 * dragCoeff * dragCoeff;

		//Calculate and apply  the final force
		force.normalize();
		force.multiplyVectorByScalar(-dragCoeff);
		particle.addForce (force);
	}
}
