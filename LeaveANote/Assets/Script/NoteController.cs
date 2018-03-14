using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour {

	public GameObject m_TM;
	public GameObject m_Form;
	public GameObject m_Read;
	public Image m_Image;
	public GameObject noteButton;

	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
		m_TM = GameObject.Find("TouchManager");
		m_Form = GameObject.Find ("UI/NotePage/Form");
		m_Read = GameObject.Find ("UI/NotePage/Read");
		noteButton = GameObject.Find ("UI/NoteButton");
		m_Image = GetComponent<Image> ();
	}
		

	public void show(){
		this.gameObject.SetActive (true);
		m_TM.GetComponent<TounchManager> ().nowEdit ();
		noteButton.GetComponent<NoteButton> ().hideButton ();
	}

	public void unShow(){
		this.gameObject.SetActive (false);
		m_Form.SetActive (false);
		m_Read.SetActive (false);
		m_TM.GetComponent<TounchManager> ().notEdit ();
		noteButton.GetComponent<NoteButton> ().showButton ();
	}

	public void showForm(){
		show ();
		m_Read.SetActive (false);
		m_Form.SetActive (true);
	}

	public void showRead(string m_String){
		show ();
		m_Form.SetActive (false);
		m_Read.SetActive (true);
		m_Read.transform.GetChild (0).GetComponent<Text> ().text = m_String;
	}

	public void changeNoteColor(){
		//m_Image.sprite;
	}
}
