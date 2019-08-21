using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunWorld: MonoBehaviour
{
	
	//set number of particles
	static int maxNumParticles = 10;
	static int numContacts;

	//thrown in for compatibility reasons
	//static List<Particle> particleList = new List<Particle> ();

	static List<GameObject> particleList = new List<GameObject>();
	static List<GameObject> _particleList = new List<GameObject>();
	public GameObject part;
	//static Particle part;
	static int numParticles = 0;

	// used a bit of informal theory to get the max number of contacts. (easier to just ask)
	static int maxNumContacts = (maxNumParticles + 5) * maxNumParticles;


	 


	static ParticleWorld world = new ParticleWorld(maxNumContacts, maxNumContacts*2);

	//set boundaries for the particles 
	//or int other words where the walls floor and ceiling will be
	//these will act like collisions
	static int bound = 10;
	static int xmax = bound;
	static int xmin = -1*bound;

	static int zmax = bound;
	static int zmin = -1*bound;

	static int ymax = bound;
	static int ymin = -1*bound;

	public MyVector3 createRandomVector(){
		
		float x = (float)Random.Range (xmin + 1, xmax - 1);
		float y = (float)Random.Range (ymin + 1, ymax - 1);
		float z = (float)Random.Range (zmin + 1, zmax - 1);
		MyVector3 vect = new MyVector3 (x, y, z);
		return vect;
	}
		
	//pretty sure this calls at the start of each frame so error with increaing partRegLen might be in here
	void Start()
	{
		float x;
		float y;
		float z;
		GameObject prefab = new GameObject();
		prefab = this.part;
		MyVector3 myVector = new MyVector3();
		MyVector3 VectGrav = new MyVector3 (0, 1f, 0);
		ParticleGravity grav = new ParticleGravity (VectGrav);
		Vector3 vector;

		//world.startFrame ();
		//creates particles and keeps track of them
		while (numParticles < maxNumParticles) 
		{
			///create particle with random position

			myVector = createRandomVector ();

			//add to particle list in this classs for display compatibility reasons
			vector = myVector.convertToVector3 ();
			particleList.Add (prefab);
			_particleList.Add (prefab);

			/*
			particleList[numParticles].setInverseMassWithMass (0.5f);
			particleList[numParticles].affectedByGravity (true);
			particleList[numParticles].checkGravity();
			*/

			particleList[numParticles]=Instantiate(_particleList[numParticles], vector, Quaternion.identity, gameObject.transform);

			//create particle and add it to the registry in the world
			world.particle.setPosition (myVector);
			world.particle.setInverseMass (0.2f);
			world.addToParitcleReg (world.particle);

			myVector = createRandomVector ();
			Move force = new Move (myVector.getComponentX(),myVector.getComponentY(),myVector.getComponentZ());
			world.registry.add (world.particleReg[numParticles], force);
			world.registry.add (world.particleReg[numParticles], grav);
			//print ("Force added to particle");

			numParticles++;
		}
		print ("The length of the world registry is " + (world.registry.getLength()));
		//Write function and throw this into Particle World Class!!!! possibly
		for(int i=0; i<numParticles-1;i++)
		{
			for (int j = i + 1; j < numParticles; j++) 
			{
				world.thisContact.addParticle (world.particleReg[i],world.particleReg[j]);
				world.contacts.Add (world.thisContact);
			}

		}
		
	}

	void FixedUpdate()
	{
		
		float duration = Time.fixedUnscaledDeltaTime;
		world.generateContacts ();
		world.startFrame ();
		world.runPhysics (duration);

		//particleList = GameObject.FindGameObjectWithTag ("Particle");
		/*
			It's to move the meshes along with the particles
		*/
		int i = 0;
		while (i < maxNumParticles) 
		{
			//particleList [i].transform.position = world.particleReg [i].getPosition ().convertToVector3 ();
			particleList [i].transform.position = world.registry.getParticleAtIndex(i).getPosition().convertToVector3();
			//print ("Particle position moved");
			i++;
		}

	}
		
}


