using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealArrow : MonoBehaviour {

	public BoxCollider pickupCollider; // 1
	private Rigidbody rb; // 2
	private bool launched; // 3
	private bool stuckInWall; // 4

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (launched && !stuckInWall && rb.velocity != Vector3.zero) // 1
		{
			rb.rotation = Quaternion.LookRotation(rb.velocity); // 2
		}
	}

	public void SetAllowPickup(bool allow) // 1
	{
		pickupCollider.enabled = allow;
	}

	public void Launch() // 2
	{
		launched = true;
		SetAllowPickup(false);
	}

	private void GetStuck(Collider other) // 1
	{
		launched = false; // 2
		rb.isKinematic = true; // 3
		stuckInWall = true; // 4
		SetAllowPickup(true); // 5
		transform.SetParent(other.transform); // 6
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Controller") || other.GetComponent<Bow>()) // 1
		{
			return;
		}

		if (launched && !stuckInWall) // 2
		{
			GetStuck(other);
		}
	}

}
