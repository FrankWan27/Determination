using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{

    //https://www.gamasutra.com/blogs/GrahamTattersall/20190515/342454/Coding_to_the_Beat__Under_the_Hood_of_a_Rhythm_Game_in_Unity.php

    public float songBpm = 174f;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    public float secPerBeat;

    public float songPosition = -10f;

    public float songPositionInBeats = -10f;

    public float dspSongTime;

    public AudioSource audioSource;

    public AudioClip song;

    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = song;

        UpdateSong(songBpm, firstBeatOffset);

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        audioSource.Play();
    }

    public void UpdateSong(float bpm, float offset)
    {
        songBpm = bpm;
        firstBeatOffset = offset;
        
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;


    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
    }
}
