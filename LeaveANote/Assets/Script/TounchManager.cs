using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TounchManager : MonoBehaviour {

	public GameObject notification2;
	bool isTouched = false;
	// Update is called once per frame
	void Update()
	{
		if (Input.GetTouch(0).phase == TouchPhase.Began)
		{
			isTouched = !isTouched;
			notification2.SetActive (isTouched);
			Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit raycastHit;
			if (Physics.Raycast(raycast, out raycastHit))
			{
				//Debug.Log("Something Hit");
				if (raycastHit.collider.CompareTag("Notes"))
				{
					raycastHit.collider.gameObject.GetComponent<NoteBehavior> ().beTouched ();
				}
			}
		}
	}
}
