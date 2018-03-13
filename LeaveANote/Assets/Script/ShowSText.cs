using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSText : MonoBehaviour {

	public GameObject m_TM;
	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void show(){
		this.gameObject.SetActive (true);
	}

	public void unShow(){
		this.gameObject.SetActive (false);
	}

}
