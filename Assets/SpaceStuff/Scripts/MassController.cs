using UnityEngine;
using BaseFramework;
using System.Collections.Generic;

[RequireComponent (typeof( Rigidbody ) )]
public class MassController : MonoBehaviour
{
	public Color raycolor;
	private void OnCollisionEnter( Collision collisionInfo )
	{
		// f=ma
		//rigidbody.AddForce ( collisionInfo.relativeVelocity * collisionInfo.rigidbody.mass, ForceMode.Impulse );
		
		// todo: turn this into a variable-mass system.
		// to achieve this the following things must be determined:
		// 1. When does an object lose mass and how do we calculate the amount that is lost?
		// 2. When does an object gain mass and how do we calculate the amount that is gained?
		// 3. What do we do with objects with a very high velocity and/or very low mass?
	}
}
