using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteContoller : MonoBehaviour
{

    float noteSpeed = 0;
    float beat = 0;
    NoteSpawner ns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(Transform target, float beat, float noteSpeed, NoteSpawner ns)
    {
        transform.SetParent(target);
        this.beat = beat;
        this.noteSpeed = noteSpeed;
        this.ns = ns;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * noteSpeed * Time.deltaTime / 4);
        
        if(ns.beat > beat)
        {
            GameObject.Destroy(gameObject);
        }
    }

    
}
