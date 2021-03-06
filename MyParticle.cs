using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticle{

	public MyVector3 position = new MyVector3(0,0,0);
	public MyVector3 velocity = new MyVector3(0,0,0);
	public MyVector3 acceleration = new MyVector3(0,0,0);
	public MyVector3 gravity = new MyVector3 (0, 0f, 0);
	MyVector3 forceAccum = new MyVector3 (0, 0, 0);

	//Damping is required to remove energy added through iteration
	public float damping = 0.995f;

	public bool thisHasGravity = true;

	/* Inverse mass is a bit more useful to use
	 for immovable objects we could set a mass to be infinite making the inverse mass zero
	 inverse mass times the force gives acceleration
	 therefore zero times force gives zero acceleration and thus it gives zero change in velocity
	 an object with infinite mass, and zero velocity are immovable */
	protected float inverseMass;
	public float mass; //this is also useful for calculating forced


	public MyParticle(MyVector3 pos, MyVector3 vel, MyVector3 accel){
		this.position.setVector (pos.getComponentX(), pos.getComponentY(), pos.getComponentZ());
		this.velocity.setVector (vel.getComponentX(), vel.getComponentY(), vel.getComponentZ());
		this.acceleration.setVector (accel.getComponentX(), accel.getComponentY(), accel.getComponentZ());
	}

	public MyParticle(MyVector3 pos, MyVector3 vel){
		this.position.setVector (pos.getComponentX(), pos.getComponentY(), pos.getComponentZ());
		this.velocity.setVector (vel.getComponentX(), vel.getComponentY(), vel.getComponentZ());
	}

	public MyParticle(MyVector3 pos){
		this.position.setVector (pos.getComponentX(), pos.getComponentY(), pos.getComponentZ());
	}

	public MyParticle(){
	}

	//Copy constructor
	public MyParticle copy(){
		MyVector3 pos = new MyVector3 ();
		MyVector3 vel = new MyVector3 ();
		MyVector3 acc = new MyVector3 ();

		pos = position.copy ();
		vel = velocity.copy ();
		acc = acceleration.copy ();
		return new MyParticle (pos, vel, acc);
	}
	public void setMass(float m){
		this.mass = m;
	}

	/* input a mass and set the inverse mass
	   if you put in a mass of zero you wil get a zero inverse mass
	   I also allowed for negative masses even though they currently exist
	   this is for further experimentation with inverse masses with the current laws
	   of physics
	*/
	public void setInverseMassWithMass(float m){
		if (m != 0) {
			this.inverseMass = 1 / m;
		} else {
			this.inverseMass = 0;
		}
	}

	public float getMass(){
		return 1 / inverseMass;
	}

	/*
		Just input your desired inverse mass
	*/
	public void setInverseMass(float invMass){
		this.inverseMass = invMass;
	}

	public float getInverseMass(){
		return inverseMass;
	}

	// To change gravity depending on what you want to do with it
	public void setGravity(float x, float y, float z){
		if (thisHasGravity == false) {
			gravity.setVector (0, 0, 0);
		} else {
			gravity.setVector (x, y, z);
		}
	}

	// check to see if this particle has gravity
	public void checkGravity()
	{
		if (thisHasGravity == false) {
			gravity.setVector (0, 0, 0);
		}
	}

	public void affectedByGravity(bool x)
	{
		thisHasGravity = x;
	}

	public void addGravity(){

		acceleration.addVectorToThisVector (gravity);
	}

	public void  setPosition(float x, float y, float z){
		position.setVector (x, y, z);
	}

	public void  setPosition(MyVector3 pos){
		position= pos;
	}

	public void  setVelocity(float x, float y, float z){
		velocity.setVector (x, y, z);
	}

	public void  setVelocity(MyVector3 vel){
		velocity = vel;
	}

	public void  setAcceleration(float x, float y, float z){
		acceleration.setVector (x, y, z);
	}

	public void  setAcceleration(MyVector3 acc){
		acceleration= acc;
	}
	public void setDamping(float damping){
		this.damping = damping;
	}

	public bool hasFiniteMass(){
		if (inverseMass != 0) {
			return true;
		} else
			return false;
	}


	public MyVector3 getPosition(){
		return position;
	}

	public MyVector3 getVelocity(){
		return velocity;
	}

	public MyVector3 getAcceleration(){
		return acceleration;
	}

	//update position and velocity

	public void integrate()
	{
		//update position
		position.addScaledVectorToThisVector (velocity, Time.deltaTime);

		//work out acceleration from the force
		MyVector3 resultingAccel = acceleration;
		resultingAccel.addScaledVectorToThisVector (forceAccum, inverseMass);

		//update linear velocity from acceleration
		velocity.addScaledVectorToThisVector(resultingAccel,Time.deltaTime);

		//throw in drag
		velocity.multiplyVectorByScalar((float)(Mathf.Pow((float)damping, Time.deltaTime)));
	}

	//Clears all forces in the accumulator but gravity
	public void clearAccumulator(){
		forceAccum.setVector (gravity.getComponentX(), gravity.getComponentY(), gravity.getComponentZ());
	}

	//Clear all forces in accumulator including gravity
	public void clearAllAccumulator(){
		forceAccum.setVector (0, 0, 0);
	}

	public void addForce(MyVector3 force){
		this.forceAccum.addVectorToThisVector (force);
	}


}
