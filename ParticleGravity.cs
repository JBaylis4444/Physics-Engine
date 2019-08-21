using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGravity: ParticleForceGenerator {

	/*acceleration due to gravity*/
	public MyVector3 gravity;

	/*Creates generator with given acceleration*/
	public ParticleGravity(MyVector3 grav){
		this.gravity = grav;
	}

	/*Updates force*/
	/*
	void ParticleForceGenerator.updateForce (MyParticle particle, float duration){

		//check we don't have infinite mass
		if (!particle.hasFiniteMass())
			return;
		//app mass-scaled force to the particle
		particle.addForce(gravity.multiplyVectorByScalarVR(particle.getMass ()));
	}
	*/


	public void updateForce (MyParticle particle, float duration)
	{
		//ParticleForceGenerator.updateForce (particle, duration);
		//check we don't have infinite mass
		if (!particle.hasFiniteMass())
			return;
		//app mass-scaled force to the particle
		particle.addForce(gravity.multiplyVectorByScalarVR(particle.getMass ()));

	}


}

