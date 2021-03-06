﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapNoteController : NoteContoller
{


    private new void Update()
    {
        base.Update();

        //probably need to change to account for closeby notes
        if (Input.touchCount > 0)
        {

            //https://docs.unity3d.com/ScriptReference/TouchPhase.Began.html
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
                        Vector2 touchPos = new Vector2(wp.x, wp.y);
                        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                        {
                            OnTap();
                        }
                        break;
                }
            }
        }
    }

    private void OnTap()
    {
        Debug.Log(targetBeat - conductor.songPositionInBeats);

        accuracy = targetBeat - conductor.songPositionInBeats;
        if(accuracy < -1 || accuracy > 1)
        {
            return;
        }
        else
        {
            sm.TapNote(accuracy, transform);



            GameObject.Destroy(gameObject);

        }

    }
}