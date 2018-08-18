using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_ArrowInBow : RWVR_InteractionObject {

	public float minimumPosition; // 1
	public float maximumPosition; // 2

	private Transform attachedBow; // 3
	private const float arrowCorrection = 0.3f; // 4

	public override void Awake()
	{
		base.Awake();
		attachedBow = transform.parent;
	}

	public override void OnTriggerIsBeingPressed(RWVR_InteractionController controller) // 1
	{
		base.OnTriggerIsBeingPressed(controller); // 2

		Vector3 arrowInBowSpace = attachedBow.InverseTransformPoint(controller.transform.position); // 3
		cachedTransform.localPosition = new Vector3(0, 0, arrowInBowSpace.z + arrowCorrection); // 4
	}

	public override void OnTriggerWasReleased(RWVR_InteractionController controller) // 1
	{
		attachedBow.GetComponent<Bow>().ShootArrow(); // 2
		currentController.Vibrate(3500); // 3
		base.OnTriggerWasReleased(controller); // 4
	}

	void LateUpdate()
	{
		// Limit position
		float zPos = cachedTransform.localPosition.z; // 1
		zPos = Mathf.Clamp(zPos, minimumPosition, maximumPosition); // 2
		cachedTransform.localPosition = new Vector3(0, 0, zPos); // 3

		//Limit rotation
		cachedTransform.localRotation = Quaternion.Euler(Vector3.zero); // 4

		if (currentController)
		{
			currentController.Vibrate(System.Convert.ToUInt16(500 * -zPos)); // 5
		}
	}
}
