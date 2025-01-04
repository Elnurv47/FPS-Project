using System.Collections;
using UnityEngine;

public class DestroyAfterTimeParticle : MonoBehaviour
{
	[Tooltip("Time to destroy")]
	public float timeToDestroy = 0.02f;

	private void Awake () 
	{
		Destroy(gameObject, timeToDestroy);
	}

}
