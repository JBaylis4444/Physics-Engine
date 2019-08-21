using System;


public interface ParticleLink
{
	// interfaces can not contain fields or restraints so remember to put a particle array of length 2
	// in whatever is implimenting this interface. like down beflore

	/*
	 * // to link two particles
	MyParticle[] particle = new MyParticle[2];
	*/

	//returns current length of the cable
	float currentLength();

	//page 149 millington pdf
	//fils structure with given contact needed to prevent it from violating its constraint
	int fillContact(ParticleContact contact, int limit);

}

