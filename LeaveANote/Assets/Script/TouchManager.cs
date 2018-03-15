using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

	public GameObject notification2;
	bool isTouched = false;
	public bool isEditing;
	// Update is called once per frame
	void Start(){
		isEditing = false;
	}

	void Update()
	{
		if (Input.touchCount > 0) {
			if ((Input.GetTouch (0).phase == TouchPhase.Began) && !isEditing) {
				isTouched = !isTouched;
				notification2.SetActive (isTouched);
				Ray raycast = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
				RaycastHit raycastHit;
				if (Physics.Raycast (raycast, out raycastHit)) {
					//Debug.Log("Something Hit");
					if (raycastHit.collider.CompareTag ("Notes")) {
						raycastHit.collider.gameObject.GetComponent<NoteBehavior> ().beTouched ();
					}
				}
			}
		}
			
	}

	public void nowEdit(){
		isEditing = true;
	}

	public void notEdit(){
		isEditing = false;
	}
}
