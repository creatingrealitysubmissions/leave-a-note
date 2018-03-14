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
}

public delegate void NearbyNotesUpdatedDelegate(Note[] notes);

public class NoteStore : MonoBehaviour {
	public const double REQUEST_RESOLUTION_DEG = 200;
	public const float MAXIMUM_INACCURACY = 20;
	public NearbyNotesUpdatedDelegate onNotesUpdated;

	private bool hasLocation = false;
	private LocationInfo lastUpdate;

	public void Start() {
		Input.location.Start();
	}

	[System.Serializable]
	private struct NoteResult {
		public Note[] notes;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.location.status == LocationServiceStatus.Running) {
			LocationInfo location = Input.location.lastData;
			print("Location alive! Current accuracy: " + location.horizontalAccuracy);
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

	private IEnumerator RequestAtPos(LocationInfo location) {
		lastUpdate = location;
		hasLocation = true;
		print("Making web request at " + location.ToString());
		using (UnityWebRequest www = UnityWebRequest.Get("https://leave-a-note.herokuapp.com/notes?lat=" + location.latitude + "&long=" + location.longitude + "&resolution=" + REQUEST_RESOLUTION_DEG)) {
			yield return www.SendWebRequest();
			if (www.isNetworkError || www.isHttpError) {
				print("Failed to make request.");
				print(www.error);
				hasLocation = false;
			} else {
				print(www.downloadHandler.text);
				NoteResult result = JsonUtility.FromJson<NoteResult>(www.downloadHandler.text);
				print("Got " + result.notes.Length + " items.");
				onNotesUpdated.Invoke(result.notes);
			}
		}
	}
}
