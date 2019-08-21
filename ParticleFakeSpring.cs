using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFakeSpring : ParticleForceGenerator
{
	/* anchored end of the spring */
	MyVector3 anchor;

	float springConstant;
	float damping;

	public ParticleFakeSpring(MyVector3 anchor, float springConstant, float damping)
	{
		this.anchor = anchor;
		this.springConstant = springConstant;
		this.damping = damping;
	}

	public void updateForce (MyParticle particle, float duration)
	{
		//checking if particle has infinite mass
		if (!particle.hasFiniteMass ())
			return;	

		//calculates position of the particle relative to the anchor
		MyVector3 position;
		position = particle.getPosition();
		position.SubtractVectorFromThisVector (this.anchor);

		//Calculate constants and check whether they are in bounds
		float gamma = 0.5f * Mathf.Sqrt(4*springConstant -damping*damping);
		if (gamma == 0.0f)
			return;
		
		MyVector3 c = position.multiplyVectorByScalarVR(damping/(2.0f * gamma)).returnAddVectorToThisVector(particle.getVelocity().multiplyVectorByScalarVR(1.0f/gamma));

		//Calculate the target position
		MyVector3 target = position.multiplyVectorByScalarVR(Mathf.Cos(gamma * duration)).returnAddVectorToThisVector(c.multiplyVectorByScalarVR(Mathf.Sin(gamma * duration)));
		target.multiplyVectorByScalar (Mathf.Exp (-0.5f * duration * damping));

		//Calculate resulting acceration and force
		MyVector3 accel =(target.returnSubtractVectorFromThisVector(position)).multiplyVectorByScalarVR(1.0f/(duration*duration)).returnSubtractVectorFromThisVector(particle.getVelocity().multiplyVectorByScalarVR(duration));
		particle.addForce (accel.multiplyVectorByScalarVR(particle.getMass ()));
	}
}
