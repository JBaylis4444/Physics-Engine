using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class worldForceRegTest:MonoBehaviour
{

	static ParticleForceRegistry testReg = new ParticleForceRegistry();
	static MyVector3 gravVect = new MyVector3 (0f, -0.9f,0f);
	static MyVector3 gravVect2 = new MyVector3 (0f, 0.9f,0f);
	static MyVector3 pos = new MyVector3 (0,1,0);
	static MyVector3 dummyVect = new MyVector3 (0,0,0);
	static ParticleGravity grav = new ParticleGravity(gravVect);
	static ParticleGravity grav2 = new ParticleGravity(gravVect2);
	static MyParticle part = new MyParticle(pos,dummyVect, dummyVect);
	//part.position.setVector(dummyVect);
	static MyParticle part2 = new MyParticle(pos,dummyVect, dummyVect);
	//List<GameObject> gameParts = new List<GameObject>();
	public GameObject gobject;
	private GameObject _gobject;
	private List<GameObject> _gobjectList = new List<GameObject>();



	void Start()
	{
		part.setInverseMassWithMass (0.5f);
		part.affectedByGravity (true);
		part.checkGravity();

		testReg.add (part, grav);
		_gobject = Instantiate(gobject, pos.convertToVector3(), Quaternion.identity, gameObject.transform) as GameObject;
	}

	void FixedUpdate()
	{
		float duration = Time.fixedUnscaledDeltaTime;
		testReg.updateForces (duration);
		//grav2.updateForce (part2, duration);
		part.integrate();
		part2.integrate ();
		Debug.Log (_gobject.transform.position);

		_gobject.transform.position = part2.position.convertToVector3();
		GameObject.Find("Green Sphere(Clone)").transform.position = part.position.convertToVector3();
		Debug.Log (_gobject.transform.position);
		print("registry particle position: " + testReg.getParticleAtIndex(0).getPosition().getComponentY());
		print("particle position: " + part2.getPosition().getComponentY());
	}

}

