using System;



/// <summary>
/// turn force to zero
/// delete force registry entry from registry
/// 
/// </summary>
public class ParticleContact
{
	/*
		Holds two particles that are coming in contact with each other.
		Second can be NULL for coming in contact with scenery
	*/
	public MyParticle[] particle = new MyParticle[2];

	/*
		Holds normal resititution at the contact
	*/
	public float restitution;

	//holds direction of contact in world coordinates

	public MyVector3 contactNormal = new MyVector3();

	//holds the depth of penetration at the contact
	//faking a rigid collision and letting the objects slightly pass through each other
	//when this happens the particle contact will push them apart by the amount they've penetrated each other
	public float penetration;

	public ParticleContact (MyParticle particle1, MyParticle particle2)
	{
		particle [0] = particle1;
		particle [1] = particle2;
	}

	public ParticleContact (MyParticle particle1)
	{
		particle [0] = particle1;
	}

	public ParticleContact ()
	{
		
	}

	public void addParticle(MyParticle particle1,MyParticle particle2)
	{
		particle [0] = particle1;
		particle [1] = particle2;
	}

	public void addParticle(MyParticle particle1)
	{
		particle [1] = particle1;
	}

	//resolves this contact for both volcity and interpentration
	public void resolve(float duration)
	{
		resolveVelocity (duration);
		resolveInterpenetration (duration);
	}




	//Calculates separating velocity from this contact
	public float calculateSeparatingVelocity()
	{
		MyVector3 relativeVelocity = particle [0].getVelocity ();
		if (particle [1] != null) //check if not null
			relativeVelocity.SubtractVectorFromThisVector (particle [1].getVelocity ());
		return relativeVelocity.scalarProduct(contactNormal);
	}



	//handles interpenetration resolution for this contact
	public void resolveInterpenetration(float duration)
	{
		//if no penetration do nothing
		if(penetration<=0) return;

		//movement of each object is dependant on ts inverse mass
		float totalInverseMass = particle [0].getInverseMass ();
		if (particle [1] != null)
			totalInverseMass += particle [1].getInverseMass ();

		//if all particles have infinite mass no impulses have effect
		if (totalInverseMass <= 0) return;

		//find amount of penetration resolution per inverse mass
		MyVector3 movePerIMass = contactNormal.multiplyVectorByScalarVR(-penetration/totalInverseMass);

		//Apply the penetration resolution
		particle[0].setPosition(particle[0].getPosition().returnAddVectorToThisVector(movePerIMass.multiplyVectorByScalarVR(particle[0].getInverseMass())));

		if (particle [1] != null)
		{
			particle[1].setPosition(particle[1].getPosition().returnAddVectorToThisVector(movePerIMass.multiplyVectorByScalarVR(particle[1].getInverseMass())));

		}

	}



	private void resolveVelocity(float duration)
	{
		//find velocity in the direction of contact
		float separationVelocity = calculateSeparatingVelocity();

		if (separationVelocity > 0) 
		{
			//The contact is either separating or stationary - no impulse required
			return;
		}

		//calculate separating velocity
		float newSepVelocity = -separationVelocity * restitution;


		//velocity build up due to acceleration only
		MyVector3 accCausedVelocity = particle[0].getAcceleration();
		if (particle [1] != null) {
			accCausedVelocity.SubtractVectorFromThisVector(particle [1].getAcceleration());
		}

		float accCausedSepVelocity = accCausedVelocity.scalarProduct(contactNormal.multiplyVectorByScalarVR(duration));

		//if we've got a velocity due to accelration build up remove it from the new separating velocity
		if(accCausedSepVelocity < 0)
		{
			newSepVelocity += restitution * accCausedSepVelocity;

			//make sure we remove no more than needed
			if (newSepVelocity < 0) newSepVelocity = 0;
		}


		float deltaVelocity = newSepVelocity - separationVelocity;

		/*
			A change in velocity is applied to each object in proportion to its inverse mass.
			[the smaller the inverse mass the less the change in overall velocity]
		*/

		float totalInverseMass = particle [0].getInverseMass ();
		if (particle [1] != null)
			totalInverseMass += particle [1].getInverseMass ();


		//if all particles have infinite mass no impulses have effect
		if (totalInverseMass <= 0) return;


		//Calculate impulses to apply
		float impulse = deltaVelocity / totalInverseMass;


		//Find amount of impulse per unit of inverse mass


		MyVector3 impulsePerIMass = contactNormal.multiplyVectorByScalarVR(impulse);

		//apply impulses in the direction of contact proportional to the inverse mass
		particle[0].setVelocity(particle[0].getVelocity().returnAddVectorToThisVector(impulsePerIMass.multiplyVectorByScalarVR(particle[0].getInverseMass())));

		if (particle [1] != null) 
		{
			particle[1].setVelocity(particle[1].getVelocity().returnAddVectorToThisVector(impulsePerIMass.multiplyVectorByScalarVR(-1 * particle[1].getInverseMass())));
		}
	}

}

