using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteButton : MonoBehaviour {

	public Image m_Image;

	// Use this for initialization
	void Start () {
		m_Image = GetComponent<Image> ();
	}

	public void hideButton(){
		gameObject.SetActive (false);
		//m_Image.enabled = false;
	}

	public void showButton(){
		gameObject.SetActive (true);
		//m_Image.enabled = true;
	}
}
