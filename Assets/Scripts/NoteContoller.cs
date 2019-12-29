using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteContoller : MonoBehaviour
{

    public float noteSpeed = 0;
    public float targetBeat = 0;
    public NoteSpawner ns;
    public Conductor conductor;
    public float accuracy;

    protected ScoreManager sm;

    public void Start()
    {
        sm = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    public void Init(Transform target, float beat, float noteSpeed, NoteSpawner ns, Conductor conductor)
    {
        transform.SetParent(target);
        targetBeat = beat;
        this.noteSpeed = noteSpeed;
        this.ns = ns;
        this.conductor = conductor;
    }

    // Update is called once per frame
    protected void Update()
    {
        //move forward so lower notes have higher tap priority
        transform.Translate(Vector3.down * noteSpeed * Time.deltaTime + Vector3.forward * Time.deltaTime * 0.01f);
        
        if(conductor.songPositionInBeats > targetBeat + 1f)
        {
            sm.TapNote(20, transform);
            GameObject.Destroy(gameObject);
        }

    }

}
