using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int numTracks;
	TrackManager tm;
    public string songName = "crystalized";

	void Awake()
	{
        tm = GameObject.Find("TrackManager").GetComponent<TrackManager>();
        tm.AddTrack();
        tm.AddTrack();

    }

    private void Update()
    {

    }

    public void changeNumTracks(int num)
	{
		



	}
}
