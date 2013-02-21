using UnityEngine;
using BaseFramework;
using System.Collections;
using System.Collections.Generic;

public class SpacePhysics : MonoSingleton<SpacePhysics>
{
	private const float GRAVITATION = 9.81f;
	private const float FALLOFF = 2f;
	
	private List<MassController> m_objects;
	
	private void Awake()
	{
		MassController[] bodies = (MassController[])GameObject.FindObjectsOfType( typeof( MassController ) );
		m_objects = new List<MassController>( bodies );
	}
	
	private void Update()
	{
		for (int i=0; i<m_objects.Count; i++)
		{
			for (int j=i+1; j<m_objects.Count; j++)
			{
				Rigidbody a = m_objects[i].GetComponent<Rigidbody>();
				Rigidbody b = m_objects[j].GetComponent<Rigidbody>();
				
				ApplyGravitation (a, b);
			}
		}
	}
	
	/// <summary>
	/// Applies the gravitation to both objects using Newton's law of universal gravitation
	/// </summary>
	/// <param name='a'>
	/// The first rigidbody, a.
	/// </param>
	/// <param name='b'>
	/// The second rigidbody, b.
	/// </param>
	private static void ApplyGravitation( Rigidbody a, Rigidbody b )
	{
		float force = GRAVITATION * (a.mass * b.mass) / Mathf.Pow( Vector3.Distance( a.transform.position, b.transform.position ), FALLOFF );
		Vector3 direction = b.transform.position - a.transform.position;
		
		a.AddForce (force *  direction);
		b.AddForce (force * -direction);
	}
}
