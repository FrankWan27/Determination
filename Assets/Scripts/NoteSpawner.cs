using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    float[,] map;

    /*    Beat   Track   NoteType
    Ex:    1       0        0

    NoteTypes: 0 = Tap
               -1 = Swipe Left
               1 = Swipe Right
               2... = Hold
               -2... = Hold Release
    */ 


    public float bpm = 120;
    public float beat = 0;
    public int noteIterator = 0;
    public int totalNotes = 0;

    bool songFinished = false;

    public GameObject tapNotePrefab;
    TrackManager tm;

    // Start is called before the first frame update
    void Start()
    {
        ReadSong();
        tm = GetComponentInParent<TrackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        beat += Time.deltaTime * bpm / 60;

        if(checkBeat() && !songFinished)
        {
            SpawnNote(map[noteIterator, 1], map[noteIterator, 2]);
        }


    }

    bool checkBeat()
    {
        if (noteIterator >= totalNotes)
        {
            songFinished = true;
            return false;
        }

        //maybe use 1/bpm? 
        return map[noteIterator, 0] <= beat;
       
    }

    void ReadSong()
    {
        totalNotes = 4;
        map = new float[totalNotes, 3];

        float[,] tempMap = { { 4, 1, 0}, { 8, 0, 0}, {12, 0, 0}, {12, 1, 0} };
        map = tempMap;
    }

    void SpawnNote(float trackNum, float noteType)
    {
        int track = (int)trackNum;

        Vector3 destination = new Vector3(tm.GetTrackPos(track), transform.position.y, transform.position.z);

        GameObject newTrack = Instantiate(tapNotePrefab, destination, Quaternion.identity);

        noteIterator++;

        if (checkBeat()) //check for multiple notes on the same beat
        {
            SpawnNote(map[noteIterator, 1], map[noteIterator, 2]);
        }
    }
}
