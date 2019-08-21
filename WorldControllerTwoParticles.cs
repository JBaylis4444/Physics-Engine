using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldControllerTwoParticles:MonoBehaviour
{
	
	static MyParticle part1 = new MyParticle();
	static MyParticle part2 = new MyParticle();
	static ParticleContact contact = new ParticleContact (part1, part2);
	static MyVector3 gravVect1 = new MyVector3 (0, 0.001f, 0);
	static MyVector3 gravVect2 = new MyVector3 (0, -0.001f, 0);
	static float tduration;

	static ParticleGravity gravity1 = new ParticleGravity(gravVect1);
	static ParticleGravity gravity2 = new ParticleGravity(gravVect2);
	static ParticleDrag drag = new ParticleDrag (0.05f,0.05f);
	static ParticleForceRegistry MyForceReg = new ParticleForceRegistry();

	//static ParticleDrag drag = new ParticleDrag (0.01f,0.01f);

	void Start()
	{
		part1.setPosition (GameObject.Find("Particle1").transform.position.x, GameObject.Find("Particle1").transform.position.y, GameObject.Find("Particle1").transform.position.z);
		part2.setPosition (GameObject.Find("Particle2").transform.position.x, GameObject.Find("Particle2").transform.position.y, GameObject.Find("Particle2").transform.position.z);
		//print ("Positions set");

		part1.setInverseMassWithMass (0.5f);
		part1.affectedByGravity (true);
		part1.checkGravity ();

		part2.setInverseMassWithMass (0.5f);
		part2.affectedByGravity (true);
		part2.checkGravity ();
		MyForceReg.add (part1, gravity1);
		MyForceReg.add (part2, gravity2);
		//MyForceReg.add (part1, drag);
		//MyForceReg.add (part2, drag);
	}

	void FixedUpdate () 
	{
		MyVector3 positionDifference = new MyVector3 ();
		float posVectDiffx, posVectDiffy, posVectDiffz;
		/*posVectDiffx = part1.position.getComponentX () - part2.position.getComponentX ();
		posVectDiffy = part1.position.getComponentY () - part2.position.getComponentY ();
		posVectDiffz = part1.position.getComponentZ () - part2.position.getComponentZ ();

		positionDifference.setVector (posVectDiffx, posVectDiffy, posVectDiffz);*/
		tduration = Time.fixedUnscaledDeltaTime;

		//gravity1.updateForce (part1, tduration);
		//gravity2.updateForce (part2, tduration);

		//both gravity vectors updated in line below.
		MyForceReg.updateForces(tduration);
		//drag.updateForce (part1, tduration);
		//drag.updateForce (part2, tduration);
		//print ("Forces updated");

		/*if(positionDifference.magnitude()<=1)
		{
			contact.resolve (tduration);
		}*/

		contact.resolve (tduration);

		part1.integrate ();
		part2.integrate ();
		print ("Particle1: " + part1.position.getComponentY());
		GameObject.Find("Particle1").transform.position = part1.position.convertToVector3();
		GameObject.Find("Particle2").transform.position = part2.position.convertToVector3();
	}
}


