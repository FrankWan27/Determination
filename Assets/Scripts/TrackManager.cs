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
            tracks[i] = Instantiate(trackPrefab, transform.position, Quaternion.identity);
            tracks[i].transform.SetParent(transform);
        }
    }

	public void AddTrack()
	{
        numTracks++;
        //Vector3 startLoc = transform.position;
        //if(numTracks > 1)
       //     startLoc = new Vector3(dest[numTracks - 2], transform.position.y, transform.position.z);

        //tracks[numTracks - 1].transform.position = startLoc;//start at right most

        AdjustTracks();
	}

	public void RemoveTrack()
	{
        //tracks[numTracks - 1].transform.position = transform.position; //hide
		numTracks--;
		AdjustTracks();
	}


	public void LERPTracks()
	{
        float smoothSpeed = 0.1f;

		for(int i = 0; i < maxTracks; i++)
		{
            GameObject track = tracks[i];
            Vector3 destination;
            if(i < numTracks)                
               destination = new Vector3(dest[i], track.transform.position.y, track.transform.position.z);
            else
               destination = new Vector3(dest[numTracks - 1], track.transform.position.y, track.transform.position.z);
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
