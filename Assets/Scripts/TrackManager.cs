using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
	GameObject[] tracks;
	public GameObject trackPrefab;

	public int numTracks;
    public int maxTracks = 6;

    float[] dest;


    void Update()
	{
        LERPTracks();
	}

	void Awake()
	{
		tracks = new GameObject[maxTracks];
        for(int i = 0; i < maxTracks; i++)
        {
            tracks[i] = Instantiate(trackPrefab, transform.position + Vector3.left * 100, Quaternion.identity);
            tracks[i].transform.SetParent(transform);
        }
    }

	public void AddTrack()
	{
        numTracks++;
		AdjustTracks();
	}

	public void RemoveTrack()
	{
        tracks[numTracks - 1].transform.position = transform.position + Vector3.left * 100; //hide
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
        return GetTrack(trackPos).transform.position.x;
    }

    public GameObject GetTrack(int trackPos)
    {
        return tracks[trackPos];
    }
}
