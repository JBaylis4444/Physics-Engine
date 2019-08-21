using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Basic interface for contact generators applying to particles
	Contact generator also holds a registry for all contacts
*/

public class MyParticleContactGenerator
{

	public List<ParticleContact> contactReg = new List<ParticleContact>();

	/*
		**Maybe** 
		
		Add a particle contact list in here and add to that list with addContact.
		Create a particle contact generator and use the list created within that particle contact generator 


		**Alternative**
		add ParticleContact List as a parameter use .Add to add to it. TRY THIS FIRST!!!!!
	*/
	public int addContact(ParticleContact contact, int limit)
	{
		if (limit > 0) 
		{
			contactReg.Add (contact);
			return 1;
		}

		return 0;
	}



}


