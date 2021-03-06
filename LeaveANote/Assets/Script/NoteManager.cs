﻿using System.Collections;
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
	private HashSet<int> displayedNotes = new HashSet<int>();

	// These represent the same position in two different coordinate systems,
	// which gives us a basis for conversion between them.
	private LocationInfo latLongOrigin;
	private Vector3 unityOrigin;
	private bool hasCoordinates = false;

	private NoteStore store;

	// Use this for initialization
	void Start () {
		noteCount = 0;
		m_IF = GameObject.Find ("UI/NotePage/Form/WriteNoteField").GetComponent<InputField> ();
		//m_IF = GameObject.FindGameObjectWithTag ("inputField").GetComponent<InputField> ();
		UINote = GameObject.Find("UI/NotePage");
		m_IF.transform.parent.parent.gameObject.GetComponent<NoteController> ().unShow ();

		store = GameObject.Find("NoteStore").GetComponent<NoteStore>();
		store.onNotesUpdated += notesUpdated;
		store.onNotePosted += noteAdded;
	}

	// Update is called once per frame
	void Update () {

	}

	public void CreateNotes(){
		int colour = UINote.GetComponent<NoteController>().returnPick();
		CreateNote(Camera.main.transform.position + Camera.main.transform.forward.normalized * disFromCam, m_IF.text, colour);
		// TODO: This shouldn't work if we don't have a good location...
		store.AddNote(Input.location.lastData, m_IF.text, colour);
		m_IF.Select ();
		m_IF.text = "";
		//UINote.GetComponent<NoteController> ().resetpick ();
	}

	public void setUpNote(string m_String, int mColor){
		//Debug.Log ("setting UP");
		//Debug.Log ("SetUPNOTE " + m_String);
		UINote.GetComponent<NoteController> ().showRead (m_String, mColor);
		//UINote.GetComponent<NoteController> ().changeNoteColor (mColor);
	}

	private void CreateNote(Vector3 position, string text, int colour) {
		GameObject m_Notes = Instantiate(Notes, position, Quaternion.identity);
		Texture tex = UINote.GetComponent<NoteController>().m_Textures[colour]; // HACK: this array is in the wrong place.
		m_Notes.GetComponent<NoteBehavior> ().NoteObjColor (tex, colour);
		m_Notes.name = m_Notes.name + noteCount.ToString();
		m_Notes.GetComponent<NoteBehavior>().setString(text);
		noteCount++;
	}

	private void notesUpdated(Note[] notes, LocationInfo location) {
		if (!hasCoordinates) {
			latLongOrigin = location;
			unityOrigin = Camera.main.transform.position;
			hasCoordinates = true;
		}
		foreach (Note note in notes) {
			if (displayedNotes.Contains(note.id)) {
				continue;
			}
			displayedNotes.Add(note.id);
			CreateNote(latLongToUnity(note.lat, note.lon), note.content, note.color);
		}
	}

	// This makes sure we don't end up trying to re-display a note we just added.
	private void noteAdded(Note note) {
		displayedNotes.Add(note.id);
	}

	private Vector3 latLongToUnity(double lat, double lon) {
		// Lazy approach: use the spherical equirectangular projection, which is probably okayish over short distances.
		// source: http://www.movable-type.co.uk/scripts/latlong.html
		const double DEG_TO_RAD = 0.017453292519943295769236907684886f;
		double lambda1 = latLongOrigin.longitude * DEG_TO_RAD;
		double lambda2 = lon * DEG_TO_RAD;
		double phi1 = latLongOrigin.latitude * DEG_TO_RAD;
		double phi2 = lat * DEG_TO_RAD;
		Vector3 delta = new Vector3((float)((lambda2 - lambda1) * System.Math.Cos(phi1 + phi2) / 2.0), 0, (float)(phi2 - phi1));
		delta *= 6371008f; // radius of earth
		return delta + unityOrigin;
	}
}