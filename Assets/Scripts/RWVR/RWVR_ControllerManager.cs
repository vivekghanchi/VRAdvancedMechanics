using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_ControllerManager : MonoBehaviour {

	public static RWVR_ControllerManager Instance; // 1

	public RWVR_InteractionController leftController; // 2
	public RWVR_InteractionController rightController; // 3

	private void Awake()
	{
		Instance = this;
	}

	public bool AnyControllerIsInteractingWith<T>() // 1
	{
		if (leftController.InteractionObject && leftController.InteractionObject.GetComponent<T>() != null) // 2
		{
			return true;
		}

		if (rightController.InteractionObject && rightController.InteractionObject.GetComponent<T>() != null) // 3
		{
			return true;
		}

		return false; // 4
	}
}
