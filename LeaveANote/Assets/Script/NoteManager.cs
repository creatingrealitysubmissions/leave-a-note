using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour {

	public GameObject Notification;
	public GameObject Notes;
	public float disFromCam = 2.0f;
	public int noteCount = 0;
	public InputField m_IF;
	public GameObject UINote;
	// Use this for initialization
	void Start () {
		noteCount = 0;
		m_IF = GameObject.Find ("UI/NotePage/Form/WriteNoteField").GetComponent<InputField> ();
		//m_IF = GameObject.FindGameObjectWithTag ("inputField").GetComponent<InputField> ();
		UINote = GameObject.Find("UI/NotePage");
		m_IF.transform.parent.parent.gameObject.GetComponent<NoteController> ().unShow ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateNotes(){
		//Notification.GetComponent<ShowSText> ().show ();
		GameObject m_Notes = Instantiate (Notes, Camera.main.transform.position + Camera.main.transform.forward * disFromCam, Quaternion.identity);
		m_Notes.name = m_Notes.name + noteCount.ToString ();
		m_Notes.GetComponent<NoteBehavior> ().setString (m_IF.text);
		m_IF.text = "";
		noteCount++;
	}

	public void setUpNote(string m_String){
		UINote.GetComponent<NoteController> ().showRead (m_String);
	}
}
