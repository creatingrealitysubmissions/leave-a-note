using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBehavior : MonoBehaviour {
	public GameObject Notification;
	public string m_String = "Nothing";
	//public GameObject NoteField;
	public GameObject m_NM;
	public int m_Color = 0;

	// Use this for initialization
	void Start () {
		Notification = GameObject.Find ("Notificaiton");
		m_NM = GameObject.Find ("NoteParent");
		//NoteField = GameObject.Find ("UI/NoteToShow/testNote");
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		this.transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
	}

	public void beTouched(){
		if (!Notification.activeSelf)
			Notification.SetActive (true);
		Notification.GetComponent<Text> ().text = m_String;
		//NoteField.GetComponent<Text> ().text = m_String;
		m_NM.GetComponent<NoteManager> ().setUpNote (m_String, m_Color);
	}

	public void setString(string stringToMe){
		m_String = stringToMe;
	}

	public void NoteObjColor(Texture newText, int tarColor){
		this.GetComponent<Renderer> ().material.mainTexture = newText;
		this.transform.GetChild (0).GetComponent<Renderer> ().material.mainTexture = newText;
		m_Color = tarColor;
	}
}
