using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyVector3{


	//three coordinates that only take in real values
	public float x = 0;
	public float y = 0;
	public float z = 0;
	//private double padding = 0;

	public MyVector3(float x, float y, float z){
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public MyVector3(float x, float y){
		this.x = x;
		this.y = y;

	}

	public MyVector3(float x){
		this.x = x;
	}

	public MyVector3(){
	}

	//copy constructor.
	public MyVector3 copy(){
		float copy_x = this.x;
		float copy_y = this.y;
		float copy_z = this.z;
		MyVector3 copyVector = new MyVector3(copy_x,copy_y,copy_z)
			return copyVector;

	}
	//I used this as a constructor class
	public void setVector(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public float getComponentX()
	{
		return this.x;
	}

	public float getComponentY()
	{
		return this.y;
	}

	public float getComponentZ()
	{
		return this.z;
	}

	public void invert()
	{
		this.x = -x;
		this.y = -y;
		this.z = -z;
	}

	public float magnitude(){
		return Mathf.Sqrt((float)(x*x + y*y + z*z));
	}

	public float squareMagnitude(){
		return x * x + y * y + z * z;
	}

	public void normalize(){
		float num = this.magnitude ();
		if (num > 0) {
			this.x *= 1 / num;
			this.y *= 1 / num;
			this.z *= 1 / num;
		}
	}

	//Duplicated this vector and returns its normalized self
	public MyVector3 returnNormalized(){
		MyVector3 vector = new MyVector3 ();
		vector.setVector (this.x,this.y,this.z);
		float num = vector.magnitude ();
		if (num > 0) {
			vector.x *= 1 / num;
			vector.y *= 1 / num;
			vector.z *= 1 / num;
		}
		return vector;
	}


	//Multiplies current vector by a scalar
	public void multiplyVectorByScalar(float num){
		x *= num;
		y *= num;
		z *= num;
	}

	//Multiplies current vector by a scalar and returns the vector
	public MyVector3 multiplyVectorByScalarVR(float num){

		float x = this.x * num;
		float y = this.y * num;
		float z = this.z * num;
		MyVector3 vector = new MyVector3 ();
		vector.setVector (x, y, z);
		return vector;
	}

	/*input is a MyVector3
	  it adds the inputted Vector to this vector
		*/
	public void addVectorToThisVector(MyVector3 vector){

		this.x += vector.x;
		this.y += vector.y;
		this.z += vector.z;
	}

	// returns the sum of this vector plus an inputted vector
	public MyVector3 returnAddVectorToThisVector(MyVector3 vector){

		vector.x += this.x;
		vector.y += this.y;
		vector.z += this.z;
		return vector;
	}

	public void SubtractVectorFromThisVector(MyVector3 vector){

		this.x -= vector.x;
		this.y -= vector.y;
		this.z -= vector.z;
	}

	// returns the difference of this vector subtracted from an inputted vector
	public MyVector3 returnSubtractVectorFromThisVector(MyVector3 vector){

		vector.x -= this.x;
		vector.y -= this.y;
		vector.z -= this.z;
		return vector;
	}


	/*input is a MyVector3
	  it adds the inputted Vector by a scaled vector to this vector
		*/
	public void addScaledVectorToThisVector(MyVector3 vector, float scale){

		this.x += scale * vector.x;
		this.y += scale * vector.y;
		this.z += scale * vector.z;
	}

	/*input is a MyVector3
	  it returns the scaled vector of this added to the inputted vector
		*/
	public MyVector3 returnAddThisVectorScaledToVector(MyVector3 vector, float scale){

		vector.x += this.x * scale;
		vector.y += this.y * scale;
		vector.z += this.z * scale;
		return vector;
	}

	public void returnComponentProduct(MyVector3 vector){
		this.x *= vector.x;
		this.y *= vector.y;
		this.z *= vector.z;
	}


	public MyVector3 componentProduct(MyVector3 vector){
		vector.x *= this.x;
		vector.y *= this.y;
		vector.z *= this.z;
		return vector;
	}

	//returns a scalar float of two vetors. Also called the dot product
	public float scalarProduct(MyVector3 vector){
		return (vector.x * this.x + vector.y * this.y + vector.z * this.z);
	}

	//outputs the angle between this vector and the inputted vector
	public float trigScalarProduct(MyVector3 vector){

		MyVector3 a = new MyVector3 (this.x, this.y, this.z);
		MyVector3 b = new MyVector3 (vector.x, vector.y, vector.z);

		float mag_a = a.magnitude ();
		float mag_b = b.magnitude ();

		a.normalize ();
		b.normalize ();

		float angle = Mathf.Acos ((float)((a.scalarProduct (b)) / (mag_a * mag_b)));
		return angle;
	}

	//Outputs the vector product of this vector and another vector (cross product)
	public MyVector3 vectorProduct(MyVector3 vector){
		float prodx = this.y * vector.z - this.z * vector.y;
		float prody = this.z * vector.x - this.x * vector.z;
		float prodz = this.x * vector.y - this.y * vector.x;

		MyVector3 ret_vector = new MyVector3 (prodx,prody,prodz);
		return ret_vector;
	}

	public Vector3 convertToVector3 (){
		return new Vector3 ((float)this.x, (float)this.y, (float)this.z);
	}
}




