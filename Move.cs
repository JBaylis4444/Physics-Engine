using System;


public class Move:ParticleForceGenerator
{
	MyVector3 force;
	float x;
	float y;
	float z;

	public Move (float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
		force = new MyVector3 (x,y,z);
	}

	/*
	public Move(MyVector3 force)
	{
		this.force = force; 
	}
	*/

	public void updateForce (MyParticle particle, float duration)
	{
		particle.addForce(this.force.multiplyVectorByScalarVR(particle.getMass ()));		
	}

}


