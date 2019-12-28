using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
	List<GameObject> tracks;
	public GameObject trackPrefab;

	public int numTracks;

    float[] dest;


    void Update()
	{
        LERPTracks();
	}

	void Awake()
	{
		tracks = new List<GameObject>();
	}

	public void AddTrack()
	{
		GameObject newTrack = Instantiate(trackPrefab, transform.position, Quaternion.identity);
		tracks.Add(newTrack);
		numTracks++;
		AdjustTracks();
	}

	public void RemoveTrack()
	{
        GameObject.Destroy(tracks[numTracks - 1]);
        tracks.RemoveAt(numTracks - 1);
		numTracks--;
		AdjustTracks();
	}


	public void LERPTracks()
	{
        float smoothSpeed = 0.4f;

		for(int i = 0; i < numTracks; i++)
		{
            GameObject track = tracks[i];
            Vector3 destination = new Vector3(dest[i], track.transform.position.y, track.transform.position.z);
            track.transform.position = Vector3.Lerp(track.transform.position, destination, smoothSpeed);
        }

	}

    public void AdjustTracks()
    {
        dest = Tools.SetDest(numTracks);
	}

    public float GetTrackPos(int trackPos)
    {
        return dest[trackPos];
    }

}
