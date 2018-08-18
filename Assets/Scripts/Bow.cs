using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Bow : MonoBehaviour {


	public Transform attachedArrow; // 1
	public SkinnedMeshRenderer BowSkinnedMesh; // 2

	public float blendMultiplier = 255f; // 3
	public GameObject realArrowPrefab; // 4

	public float maxShootSpeed = 50; // 5

	public AudioClip fireSound; // 6

	bool IsArmed()
	{
		return attachedArrow.gameObject.activeSelf;
	}

	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(transform.position, attachedArrow.position); // 1
		BowSkinnedMesh.SetBlendShapeWeight(0, Mathf.Max(0, distance * blendMultiplier)); // 2	
	}

	private void Arm() // 1
	{
		attachedArrow.gameObject.SetActive(true);
	}

	private void Disarm() 
	{
		BowSkinnedMesh.SetBlendShapeWeight(0, 0); // 2
		attachedArrow.position = transform.position; // 3
		attachedArrow.gameObject.SetActive(false); // 4
	}

	private void OnTriggerEnter(Collider other) // 1
	{
		if (
			!IsArmed() 
			&& other.CompareTag("InteractionObject") 
			&& other.GetComponent<RealArrow>() 
			&& !other.GetComponent<RWVR_InteractionObject>().IsFree() // 2
		) {
			Destroy(other.gameObject); // 3
			Arm(); // 4
		}
	}

	public void ShootArrow()
	{
		GameObject arrow = Instantiate(realArrowPrefab, transform.position, transform.rotation); // 1
		float distance = Vector3.Distance(transform.position, attachedArrow.position); // 2

		arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * distance * maxShootSpeed; // 3
		AudioSource.PlayClipAtPoint(fireSound, transform.position); // 4
		GetComponent<RWVR_InteractionObject>().currentController.Vibrate(3500); // 5
		arrow.GetComponent<RealArrow>().Launch(); // 6

		Disarm(); // 7
	}
}
