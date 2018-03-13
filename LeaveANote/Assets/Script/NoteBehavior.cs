using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBehavior : MonoBehaviour {
	public GameObject Notification;
	public string m_String;

	// Use this for initialization
	void Start () {
		Notification = GameObject.Find ("Notificaiton");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void beTouched(){
		if (!Notification.activeSelf)
			Notification.SetActive (true);
		Notification.GetComponent<Text> ().text = m_String;
	}

	public void setString(string stringToMe){
		m_String = stringToMe;
	}
}
