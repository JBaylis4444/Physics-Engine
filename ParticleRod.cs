using System;


public class ParticleRod
{
	// to link two particles
	MyParticle[] particle;

	//lenght of rod
	float length;



	//returns current length of the cable
	public float currentLength()
	{
		return length;
	}

	//page 149 millington pdf
	//fils structure with given contact needed to prevent it from violating its constraint
	public int fillContact(ParticleContact contact, int limit)
	{
		particle = new MyParticle[2];

		//find current length of the rod
		float currentLen = currentLength();

		//check whether we're overextended
		if (currentLen == length)
		{
			return 0;
		}

		//otherwise return the contact
		contact.particle [0] = particle [0];
		contact.particle [1] = particle [1];

		//calculate normal
		MyVector3 normal = particle[1].getPosition().returnSubtractVectorFromThisVector(particle[0].getPosition());
		normal.normalize ();

		//contact normal is depended on whether or not we're extending or compressing
		if (currentLen > length) {
			contact.contactNormal = normal;
			contact.penetration = currentLen - length;
		} else {
			contact.contactNormal = normal.multiplyVectorByScalarVR(-1);
			contact.penetration = length - currentLen;
		}

		//no bounciness so restitution is always zero
		contact.restitution=0;

		return 1;
	}

}


