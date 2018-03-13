using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour {

	public GameObject Notification;
	public GameObject Notes;
	public float disFromCam = 2.0f;
	public int noteCount = 0;
	// Use this for initialization
	void Start () {
		noteCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateNotes(){
		//Notification.GetComponent<ShowSText> ().show ();
		GameObject m_Notes = Instantiate (Notes, Camera.main.transform.position + Camera.main.transform.forward * disFromCam, Quaternion.identity);
		m_Notes.name = m_Notes.name + noteCount.ToString ();
		noteCount++;
	}
}
