using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleWorld
{
	/*keep track of all set of particles and provide a means to update them all*/

	public List<MyParticle> particleReg = new List<MyParticle>();
	public MyParticle particle = new MyParticle();
	int partRegLen = 0;
	int maxContacts;
	public static int iterations;

	// holds a contact for the particle
	public ParticleContact thisContact = new ParticleContact();
	public List<ParticleContact> contacts = new List<ParticleContact>();

	//holds force generators for particles in this world
	public ParticleForceRegistry registry = new ParticleForceRegistry();

	//holds resolver for contacts
	public ParticleContactResolver resolver = new ParticleContactResolver(iterations);

	//holds list of particle contact generator
	//MyParticleContactGenerator contactGenReg;
	public MyParticleContactGenerator gen = new MyParticleContactGenerator();
	public int contactGenRegLen = 0;//you use this but don't do anything with it

	public void addToParitcleReg(MyParticle particle)
	{
		particleReg.Add(particle);
		partRegLen++;
		//MonoBehaviour.print("ParticleWorld: Particle added to registry");
	}


 
	/*
		Creates a new particle simulator that can handle up to a given number of contact per frame
	*/
	public ParticleWorld (int maxContacts, int iters)
	{
		this.maxContacts = maxContacts;
		iterations = iters;
		//MonoBehaviour.print("ParticleWorld: World Constructed");
	}

	public ParticleWorld (int maxContacts)
	{
		this.maxContacts = maxContacts;
		iterations = maxContacts * 2;
		//MonoBehaviour.print("ParticleWorld: World Constructed");
	}

	public void startFrame()
	{
		int i = 0;
		addToParitcleReg (particle);

		//removing all forces from the accumulator
		while (i < partRegLen) 
		{
			particleReg [i].clearAccumulator ();
			i++;
		}
		//MonoBehaviour.print("ParticleWorld: Frame Started");

	}

	//call all contact generators to report their contacts
	// returns number of contacts
	public int generateContacts()
	{
		int limit = maxContacts;
		int i = 0;
		ParticleContact nextContact = contacts[i];
		//nextContact = this.thisContact;

		while (i <= contactGenRegLen) 
		//while(i <= maxContacts)
		{
			int used = gen.addContact (nextContact, limit);
			limit -= used;
			i++;
			if (contacts [i] != null) 
			{
				nextContact = contacts [i];
			}


			//we've run out of contacts
			if (limit <= 0) break;

		}
		//MonoBehaviour.print("ParticleWorld: Contacts Generated");
		//return number of contacts used
		return maxContacts-limit;
	}

	/// <summary>
	/// There are issues with the particle integration.
	/// partRegLen is increasing and it shouldn't be
	/// </summary>

	//integrates all particles in theis world in time given by the duration 
	public void integrate()
	{
		int i = 0;
		MonoBehaviour.print ("ParticleWorld: contactGenRegLen = " + contactGenRegLen);
		MonoBehaviour.print ("ParticleWorld::Integrate: partRegLen = " + partRegLen);
		while(i<partRegLen)
		{
			//integrate all particles in the registry
			this.particleReg[i].integrate();

			// to next element of the list
			i++;
			MonoBehaviour.print("ParticleWorld: While loop in integrate function is totally working");
		} 
		MonoBehaviour.print("ParticleWorld: Particles integrated");
		
	}

	//runs all physics in the particle world
	public void runPhysics(float duration)
	{
		bool calculateIterations = true;
		//apply all force generators
		registry.updateForces(duration);

		//then integrate the objects
		integrate();

		//Generate contacts
		int usedContacts = generateContacts();

		//process them
		if (calculateIterations) { 
			resolver.setIterations (usedContacts * 2);
			calculateIterations = false;
		}
		resolver.resolveContacts (contacts,usedContacts,duration);
		//MonoBehaviour.print("ParticleWorld: Physics Ran");
	}

}


