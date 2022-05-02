using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {Health, Scores}; 

	public CollectibleTypes CollectibleType; 

	public bool rotate; 

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;
	
	private void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other != null) {
			Collect (other);
		}
	}

	private void Collect(Collider other)
	{
		bool result = false;
		
		switch (CollectibleType)
		{
			case CollectibleTypes.Health:
			{
				var healGameObject = other.GetComponent<IHeal>();
			
				if (healGameObject == null)
				{
					return;
				}
			
				result = healGameObject.OnHeal(1);
				break;
			}
			case CollectibleTypes.Scores:
			{
				var scorableGameObject = other.GetComponent<IScorable>();
			
				if (scorableGameObject == null)
				{
					return;
				}
			
				scorableGameObject.AddSores(1);
				result = true;
				break;
			}
		}

		if (result)
		{
			if(collectSound)
				AudioSource.PlayClipAtPoint(collectSound, transform.position);
			if(collectEffect)
				Instantiate(collectEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
