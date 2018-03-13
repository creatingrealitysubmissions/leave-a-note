using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSText : MonoBehaviour {

	public GameObject m_TM;
	// Use this for initialization
	void Start () {
		//this.gameObject.SetActive (false);
		m_TM = GameObject.Find("TouchManager");
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
		m_TM.GetComponent<TounchManager> ().notEdit ();
	}

}
