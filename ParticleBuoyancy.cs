using System;

/*Generates buoyancy force a plane of liquid that is parrallel to the XZ plane*/
public class ParticleBuoyancy: ParticleForceGenerator
{
	//holds the max depth for an object before it generates max buoyancy on the object 
	float maxDepth;

	//Volume of the object
	float volume;

	//holds height of water above y=0 and is parallel to the XZ
	float waterHeight;

	//holds the density of a liquid
	//the density of wate ris 100kg per meter
	float liquidDensity;

	public ParticleBuoyancy (float maxDepth, float volume, float waterHeight, float liquidDensity)
	{
		this.maxDepth = maxDepth;
		this.volume = volume;
		this.waterHeight = waterHeight;
		this.liquidDensity = liquidDensity;
	}

	//default density set to water
	public ParticleBuoyancy (float maxDepth, float volume, float waterHeight)
	{
		this.maxDepth = maxDepth;
		this.volume = volume;
		this.waterHeight = waterHeight;
		this.liquidDensity = 1000.0f;
	}

	//I might come back and add some get and set methods to modify volume and waterHeight 

	public void updateForce (MyParticle particle, float duration)
	{
		float depth = particle.getPosition ().getComponentY ();

		//check is object is out of water
		//
		if (depth >= waterHeight+maxDepth) return;
		MyVector3 force = new MyVector3(0f, 0f, 0f);

		//check if we're at max depth
		if (depth  <= waterHeight - maxDepth)
		{
			force.y = liquidDensity * volume;
			particle.addForce(force);
			return;
		}

		//if the other 2 aren't true we're partially submerged
		force.y = liquidDensity * volume * (depth- maxDepth - waterHeight);
		particle.addForce(force);

	}

}


