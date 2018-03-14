using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeoTracker : MonoBehaviour {

	public GameObject LocationSign;
	public GameObject FacingSign;
	public float m_Lat, m_Lon;

	// Use this for initialization
	void Start () {
		LocationSign = GameObject.Find ("UI/Location");
		FacingSign = GameObject.Find ("UI/Facing");
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		LocationSign.GetComponent<Text> ().text = "Camera Location: " + Camera.main.transform.position.x + ", " + Camera.main.transform.position.y + ", " + Camera.main.transform.position.z;
		FacingSign.GetComponent<Text> ().text = "Facing: " + Input.compass.magneticHeading;
	}

	public void GeneratePoint(){
		
	}
}
