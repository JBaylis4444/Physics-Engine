using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This classs is meant to hold forces and the particles that
each force applies to
*/

//pdf pages 97-99 check and update
public class ParticleForceRegistry {

	protected struct ParticleForceRegistration
	{
		public MyParticle particle;
		public ParticleForceGenerator fg;

		public void setValues(MyParticle p, ParticleForceGenerator f){
			this.particle = p;
			this.fg = f;
		}

	}


	protected List<ParticleForceRegistration> Registry = new List<ParticleForceRegistration>();
	protected ParticleForceRegistration registration = new ParticleForceRegistration(); 
	protected int listLength = 0;


	/*Registers given force generator to apply to the particle*/
	public void add (MyParticle particle, ParticleForceGenerator fg)
	{
		registration.setValues (particle, fg);
		Registry.Add (registration);
		listLength++;

	}

	/*Removes given pair from Registry. If not in registry nothing happens*/
	public void remove(MyParticle particle, ParticleForceGenerator fg){
		registration.setValues (particle, fg);
		Registry.Remove (registration);
		listLength--;
	}

	public MyParticle getParticleAtIndex(int i){
		return Registry [i].particle;
	}

	public int getLength(){
		return listLength;
	}
	/*Clears the registry without deleting the force generators or the particles. Just removes the connection*/
	public void clear(){
		Registry.Clear ();
		listLength = 0;
	}

	/*Updates all force generators*/
	public void updateForces (float duration){
		for (int i = 0; i < listLength; i++) {
			Registry [i].fg.updateForce (Registry[i].particle, duration);
		}

	}
}
