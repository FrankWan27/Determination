using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int numTracks;
	TrackManager tm;

	void Awake()
	{
        tm = GameObject.Find("TrackManager").GetComponent<TrackManager>();
        tm.AddTrack();
        tm.AddTrack();

    }


    public void changeNumTracks(int num)
	{
		



	}
}
