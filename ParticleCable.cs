using System;


public class ParticleCable: ParticleLink
{

	// to link two particles
	MyParticle[] particle;



	//max length particles can separate
	public float maxLength;

	//hold bounciness of the cable
	public float restitution;


	//returns current length of the cable
	public float currentLength()
	{
		MyVector3 relativePos = particle [0].getPosition ().returnSubtractVectorFromThisVector (particle[1].getPosition());
		return relativePos.magnitude ();
	}

	public int fillContact(ParticleContact contact, int limit)
	{
		//initializing Particle here because I can't do it above
		particle = new MyParticle[2];

		//find current length of the cable
		float length = currentLength();

		//check if over extended
		if (length<maxLength)
		{
			return 0;
		}

		//otherwise return the contact
		contact.particle [0] = particle [0];
		contact.particle [1] = particle [1];

		//calculate normal
		MyVector3 normal = particle[1].getPosition().returnSubtractVectorFromThisVector(particle[0].getPosition());
		normal.normalize ();

		contact.penetration = length - maxLength;
		contact.restitution = restitution;

		return 1;
	}
}


