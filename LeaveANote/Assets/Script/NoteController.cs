using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour {

	public GameObject m_TM;
	public GameObject m_Form;
	public GameObject m_Read;

	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
		m_TM = GameObject.Find("TouchManager");
		m_Form = GameObject.Find ("Form");
		m_Read = GameObject.Find ("Read");
	}

	// Update is called once per frame
	void Update () {

	}

	public void show(){
		this.gameObject.SetActive (true);
		m_TM.GetComponent<TounchManager> ().nowEdit ();
	}

	public void unShow(){
		this.gameObject.SetActive (false);
		m_Form.SetActive (false);
		m_Read.SetActive (false);
		m_TM.GetComponent<TounchManager> ().notEdit ();
	}

	public void showForm(){
		show ();
		m_Read.SetActive (false);
		m_Form.SetActive (true);
	}

	public void showRead(){
		show ();
		m_Form.SetActive (false);
		m_Read.SetActive (true);
	}
}
