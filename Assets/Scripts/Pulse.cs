using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float pulseFactor = 1.1f;
    Vector3 originalSize;
    Conductor conductor;
    int currentBeat = -20;

    void Start()
    {
        conductor = GetComponent<NoteContoller>().conductor;
        originalSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float smoothSpeed = 0.1f;
        transform.localScale = Vector3.Lerp(transform.localScale, originalSize, smoothSpeed);

        if((int)conductor.songPositionInBeats - currentBeat >= 1)
        {
            currentBeat = (int)conductor.songPositionInBeats;
            transform.localScale = originalSize * pulseFactor;
        }
    }
}
