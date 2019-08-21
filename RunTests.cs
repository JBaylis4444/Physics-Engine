using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ian Millington's Game Physics book was referenced a lot to write the classes for this code
public class RunTests : MonoBehaviour {

	static MyParticle part1 = new MyParticle ();
	static MyParticle part2 = new MyParticle ();
	static MyVector3 VectGrav = new MyVector3 (0f,-9f,0);
	static ParticleGravity grav = new ParticleGravity (VectGrav);
	static ParticleSpring spr = new ParticleSpring (part2, 0.2f, 0.5f);
	static ParticleDrag drag = new ParticleDrag (0.05f,0.05f);
	static ParticleBungie bung = new ParticleBungie (part2, 0.55f, 5f);
	static ParticleFakeSpring fake = new ParticleFakeSpring(part2.position,0.05f,0.05f);
	static ParticleAnchoredSpring anchor = new ParticleAnchoredSpring (part2.position, 0.95f, 1.5f);
	static ParticleBuoyancy buoy = new ParticleBuoyancy (1f, 0.55f, 5f, 100.0f);

	static float duration = 0;
	// Use this for initialization
	void Start () {
		
		part1.setPosition (transform.position.x, transform.position.y, transform.position.z);
		part2.setPosition (transform.position.x , transform.position.y + 1.5f , transform.position.z);//same position as green ball but not the positon of green ball
		//part1.setMass (25);
		part1.setInverseMassWithMass (0.5f);
		print ("Part1 inverse mass: " + part1.getInverseMass());
		part1.affectedByGravity (true);
		//setGravity (0, -1, 0);
		//part1.setVelocity (12, 10, -1);
		//part1.setAcceleration (0, 20, 0);
		part1.checkGravity ();
		//part1.setAcceleration ((float)gravity.getComponentX(), (float)gravity.getComponentY(), (float)gravity.getComponentZ());
		/*
			You could mess around with creating fireworks later page 84 in millington though not in the current form
		*/
	}

	// Update is called once per frame
	void FixedUpdate () {
		//print ("part 1 Z: " + part1.position.getComponentZ());
		/*
		    Millington's book doesn't touch much upon duration
			I know I didn't throw it in here but we can use loops and conditions to limit the movement of the springs. I did end up experimenting
			with this and then deleting it to test other things.

			To test seperate forces below comment and uncomment lines at will

		*/

		//to work nicely use drag force

		duration = Time.fixedUnscaledDeltaTime;

		grav.updateForce (part1, duration);
		spr.updateForce (part1, duration);
		//bung.updateForce (part1, duration);
		//fake.updateForce(part1,duration);
		//anchor.updateForce(part1, duration);
		//buoy.updateForce(part1, duration);

		drag.updateForce ( part1, duration);
		part1.integrate ();
		transform.position = part1.position.convertToVector3();

	}
}





