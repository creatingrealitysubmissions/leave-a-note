using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public struct GeoLocation {
	public double lat;
	public double lon;
	public float altitude;
}

[Serializable]
public struct Note {
	public double lat;
	public double lon;
	public float altitude;
	public int id;
	public string content;
	public int color;
}

public delegate void NearbyNotesUpdatedDelegate(Note[] notes, LocationInfo location);
public delegate void FinishedPostingNoteDelegate(Note note);

public class NoteStore : MonoBehaviour {
	public const double REQUEST_RESOLUTION_DEG = 0.0001;
	public const float MAXIMUM_INACCURACY = 40;
	public const string API_ROOT = "https://leave-a-note.herokuapp.com";
	public NearbyNotesUpdatedDelegate onNotesUpdated;
	public FinishedPostingNoteDelegate onNotePosted;

	private bool hasLocation = false;
	private LocationInfo lastUpdate;

	public void Start() {
		Input.location.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.location.status == LocationServiceStatus.Running) {
			LocationInfo location = Input.location.lastData;
			if (location.horizontalAccuracy > MAXIMUM_INACCURACY) {
				return;
			}
			const double mindelta = REQUEST_RESOLUTION_DEG / 2.0;
			if (!hasLocation
			    || location.latitude < lastUpdate.latitude - mindelta
			    || location.latitude > lastUpdate.latitude + mindelta
			    || location.longitude < lastUpdate.longitude - mindelta
			    || location.longitude > lastUpdate.longitude + mindelta) {
				StartCoroutine(RequestAtPos(location));
			}
		}
	}

	public void AddNote(LocationInfo location, string content, int colour) {
		StartCoroutine(RequestAddNote(location, content, colour));
	}

	[Serializable]
	private struct NoteResult {
		public Note[] notes;
	}

	private IEnumerator RequestAtPos(LocationInfo location) {
		lastUpdate = location;
		hasLocation = true;
		print("Making web request at " + location.ToString());
		using (UnityWebRequest www = UnityWebRequest.Get(API_ROOT + "/notes?lat=" + location.latitude + "&long=" 
														+ location.longitude + "&resolution=" + REQUEST_RESOLUTION_DEG)) {
			yield return www.SendWebRequest();
			if (www.isNetworkError || www.isHttpError) {
				print("Failed to make request.");
				print(www.error);
				hasLocation = false;
			} else {
				print(www.downloadHandler.text);
				NoteResult result = JsonUtility.FromJson<NoteResult>(www.downloadHandler.text);
				print("Got " + result.notes.Length + " items.");
				onNotesUpdated.Invoke(result.notes, location);
			}
		}
	}

	[Serializable]
	private struct SubmitResult {
		public Note note;
	}

	private IEnumerator RequestAddNote(LocationInfo location, string content, int colour) {
		print("Submitting note at " + location.longitude + ", " + location.latitude);
		WWWForm form = new WWWForm();
		form.AddField("long", location.longitude.ToString());
		form.AddField("lat", location.latitude.ToString());
		form.AddField("altitude", location.altitude.ToString());
		form.AddField("content", content);
		form.AddField("duration", (86400 * 5).ToString()); // 5 days for now (UI later?)
		form.AddField("color", colour.ToString());

		using (UnityWebRequest www = UnityWebRequest.Post(API_ROOT + "/notes", form)) {
			www.chunkedTransfer = false; // chunked transfers appear to break gunicorn.
			print("sending...");
			yield return www.SendWebRequest();
			print("sent!");
			if (www.isNetworkError || www.isHttpError) {
				// TODO: Error handling.
				print("Failed to actually submit the note.");
			} else {
				print("Submitted note!");
				SubmitResult result = JsonUtility.FromJson<SubmitResult>(www.downloadHandler.text);
				onNotePosted.Invoke(result.note);
			}
		}
	}
}
