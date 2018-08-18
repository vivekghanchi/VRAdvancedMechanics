using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_SpecialObjectSpawner : RWVR_InteractionObject {

	public GameObject arrowPrefab; // 1
	public List<GameObject> randomPrefabs = new List<GameObject>(); // 2


	private void SpawnObjectInHand(GameObject prefab, RWVR_InteractionController controller) // 1
	{
		GameObject spawnedObject = Instantiate(prefab, controller.snapColliderOrigin.position, controller.transform.rotation); // 2
		controller.SwitchInteractionObjectTo(spawnedObject.GetComponent<RWVR_InteractionObject>()); // 3
		OnTriggerWasReleased(controller); // 4
	}

	public override void OnTriggerWasPressed(RWVR_InteractionController controller) // 1
	{
		base.OnTriggerWasPressed(controller); // 2

		if (RWVR_ControllerManager.Instance.AnyControllerIsInteractingWith<Bow>()) // 3
		{
			SpawnObjectInHand(arrowPrefab, controller);
		}
		else // 4
		{
			SpawnObjectInHand(randomPrefabs[UnityEngine.Random.Range(0, randomPrefabs.Count)], controller);
		}
	}
}
