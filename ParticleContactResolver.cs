using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//alternative is a time division engine page 147 pdf millington
public class ParticleContactResolver
{
	//number of iterations allowed
	protected int iterations;

	//keep number of actual iterations used. Performance tracking value
	protected int iterationsUsed;

	public ParticleContactResolver (int iterations)
	{
		this.iterations = iterations;
	}

	//sets number of iterations that can be used
	public void setIterations(int iterations)
	{
		this.iterations = iterations;
	}

	//resolves particle contacts for both penetration and velocity
	public void resolveContacts(List<ParticleContact> contactArray, int numContacts, float duration)
	{
		iterationsUsed = 0;

		while (iterationsUsed < iterations) 
		{
			//finds number of contacts with the highest closing velocity
			float max = 0;
			int maxIndex = numContacts;
			for (int i = 0; i < numContacts; i++) 
			{
				float sepVel = contactArray [i].calculateSeparatingVelocity ();
				if (sepVel < max) 
				{
					max = sepVel;
					maxIndex = i;
				}
			}
			//resolve this contact
			contactArray[maxIndex].resolve(duration);
			iterationsUsed++;
		}
	}


}


