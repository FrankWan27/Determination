using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NoteSpawner : MonoBehaviour
{
    float[,] map;

    /*    Beat   Track   NoteType
    Ex:    1       0        0

    NoteTypes: 0 = Tap
               1 = Slide
               -2 = Swipe Left
               2 = Swipe Right
               3... = Hold
               -3... = Hold Release
    */ 


    public int noteIterator = 0;
    public int totalNotes = 0;
    public float noteSpeed = 10;
    public float offset = 4;

    bool songFinished = false;

    public GameObject tapNotePrefab;
    public GameObject slideNotePrefab;
    public GameObject swipeNotePrefab;
    public GameObject holdNotePrefab;
    public GameObject holdMidPrefab;

    TrackManager tm;
    Conductor conductor;
    GameManager gm;

    List<int> holdNotes;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        conductor = gm.GetComponent<Conductor>();
        ReadSong();
        tm = GetComponentInParent<TrackManager>();
        //calculate offset
        //offset = noteSpeed;
        holdNotes = new List<int>();

    }

    // Update is called once per frame
    void Update()
    {
        if(checkBeat() && !songFinished)
        {
            SpawnNote(map[noteIterator, 1], map[noteIterator, 2]);
        }

        foreach(int holdNote in holdNotes)
        {
            GameObject newNote = Instantiate(holdMidPrefab, new Vector3(tm.GetTrackPos(holdNote), transform.position.y, transform.position.z + 1), Quaternion.identity);
            newNote.GetComponent<HoldNoteController>().section = 0;
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
        return map[noteIterator, 0] <= conductor.songPositionInBeats + offset;
       
    }

    void ReadSong()
    {

        string path = "Maps/" + gm.songName;

        TextAsset mapData = Resources.Load(path) as TextAsset;
        Debug.Log(mapData);
        var arrayString = mapData.text.Split('\n');

        //First line = BPM, Second line = First note offset
        
        totalNotes = arrayString.Length - 2;

        float bpm = float.Parse(arrayString[0]);
        float off = float.Parse(arrayString[1]);

        conductor.UpdateSong(bpm, off);

        map = new float[totalNotes, 3];

        for (int line = 0; line < totalNotes; line++)
        {
            string[] param = arrayString[line + 2].Split(' ');
            
            for (int i = 0; i < 3; i++)
                map[line, i] = float.Parse(param[i]);
        }

       // float[,] tempMap = { { 4, 1, 0}, { 8, 0, 0}, {12, 0, 0}, {12, 1, 0} };
       // map = tempMap;
    }

    void SpawnNote(float trackNum, float noteType)
    {
        int track = (int)trackNum;

        Vector3 destination = new Vector3(tm.GetTrackPos(track), transform.position.y, transform.position.z + 1);

        GameObject newNote;

        switch(map[noteIterator, 2])
        {
            case 0: //tapNote
                newNote = Instantiate(tapNotePrefab, destination, Quaternion.identity);
                break;
            case 1: //slideNote
                newNote = Instantiate(slideNotePrefab, destination, Quaternion.identity);
                break;
            case 2: //swipeRightNote
                newNote = Instantiate(swipeNotePrefab, destination, Quaternion.identity);
                newNote.GetComponent<SwipeNoteController>().direction = 1;
                break;
            case -2: //swipeLeftNote
                newNote = Instantiate(swipeNotePrefab, destination, Quaternion.Euler(180, 0, 0));
                newNote.GetComponent<SwipeNoteController>().direction = -1;
                break;
            default:
                if (map[noteIterator, 2] >= 3) //holdNoteStart
                {
                    newNote = Instantiate(holdNotePrefab, destination, Quaternion.identity);
                    newNote.GetComponent<HoldNoteController>().section = 1;
                    holdNotes.Add(track);
                }

                else if (map[noteIterator, 2] <= -3) //holdNoteEnd
                {
                    newNote = Instantiate(holdNotePrefab, destination, Quaternion.Euler(180, 0, 0));
                    newNote.GetComponent<HoldNoteController>().section = -1;
                    holdNotes.Remove(track);
                }

                else
                {
                    Debug.Log("Notetype Error");
                    return;
                }

                break;
        }


        //Init(Transform target, float beat, float noteSpeed, NoteSpawner ns)
        newNote.GetComponent<NoteContoller>().Init(tm.GetTrack(track).transform, map[noteIterator, 0], noteSpeed, this, conductor);

        noteIterator++;

        if (checkBeat()) //check for multiple notes on the same beat
        {
            SpawnNote(map[noteIterator, 1], map[noteIterator, 2]);
        }
    }
}
