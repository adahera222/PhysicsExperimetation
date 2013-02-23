using UnityEngine;
using BaseFramework;
using System.Collections.Generic;

[RequireComponent (typeof( Rigidbody ) )]
public class MassController : MonoBehaviour
{
	public Color m_color;
	
	private const float MINIMUM_MASS_THRESHOLD = 1f;
	
	private Vector3 m_acceleration;
	private Vector3 m_lastVelocity;
	
	public Vector3 Acceleration
	{
		get
		{
			return m_acceleration;
		}
	}
	
	/// <summary>
	/// Calculates and applies a force when the object accretes (gains) or ablates (loses) mass.
	/// Uses the formula F=m*a-rv*dm, where rv is the relative velocity and dm is the difference in mass.
	/// </summary>
	/// <param name='body'>
	/// The MassController that is gaining/losing mass.
	/// </param>
	/// <param name='relativeVelocity'>
	/// The relative velocity of the body to the second body it is interacting with.
	/// </param>
	/// <param name='deltaMass'>
	/// The mass difference gained/lost.
	/// </param>
	private static void AccreteOrAblateMass( MassController body, Vector3 relativeVelocity, float deltaMass )
	{
		Vector3 externalForce = body.rigidbody.mass * body.Acceleration;
		externalForce -= relativeVelocity * deltaMass;
		
		body.rigidbody.AddForce( externalForce, ForceMode.Impulse );
	}
	
	private void  Awake()
	{
		m_acceleration = m_lastVelocity = Vector3.zero;
	}
	
	private void OnCollisionEnter( Collision collisionInfo )
	{
		// todo: turn this into a variable-mass system.
		
		// Questions:
		// 1a. How much mass is gained or lost during a collsion?
		// a: It should be proportional to the impact force and the difference of mass between the bodies.
		//    A body of high mass should gain mass
		//    A body of low mass should lose mass
		//    A high impact force should yield less mass lost
		//    A low impact force should yield more mass gained
		
		// 1b. Should this de different depending on the angle of the collision?
		// a: Most likely- but I am unsure how so.
		
		// 2. How do we deal with bodies with a very high velocity and/or very low mass?
		// a: Destroy them for now, but we may want to have a background "dust" level that slowly accretes into a body OR forms some kind of visual accretion disk.
		
		//Debug.DrawRay( transform.position, collisionInfo.impactForceSum*500, m_color, 6000 );
		//Debug.Log( m_color+" "+collisionInfo.impactForceSum );
		
		float deltaMass = rigidbody.mass - collisionInfo.rigidbody.mass;
		deltaMass *= 1 / collisionInfo.impactForceSum.magnitude; // todo!!
		
		AccreteOrAblateMass( this, collisionInfo.relativeVelocity, deltaMass );
		rigidbody.mass += deltaMass;
	}
	
	private void FixedUpdate()
	{
		m_acceleration = (rigidbody.velocity - m_lastVelocity) / Time.fixedDeltaTime;
		m_lastVelocity = rigidbody.velocity;
	}
}
