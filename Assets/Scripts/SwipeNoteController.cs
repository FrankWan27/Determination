using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeNoteController : NoteContoller
{
    float startPos = -50;
    float endPos = 0;
    public float direction = 1; //1 = right, -1 = left

    TrackManager tm;

    private new void Start()
    {
        base.Start();
        tm = GameObject.Find("TrackManager").GetComponent<TrackManager>();
    }
    private new void Update()
    {
        transform.Translate(Vector3.down * noteSpeed * Time.deltaTime);

        if (conductor.songPositionInBeats > targetBeat + 1f)
        {
            sm.TapNote(20, transform);
            if (direction > 0)
                tm.AddTrack();
            else
                tm.RemoveTrack();
            GameObject.Destroy(gameObject);
        }

        //probably need to change to account for closeby notes
        if (Input.touchCount > 0)
        {

            //https://docs.unity3d.com/ScriptReference/TouchPhase.Began.html
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                Vector3 wp;
                Vector2 touchPos;
                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        wp = Camera.main.ScreenToWorldPoint(touch.position);
                        touchPos = new Vector2(wp.x, wp.y);
                        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                        {
                            startPos = touchPos.x;
                        }
                        break;

                    case TouchPhase.Ended:
                        wp = Camera.main.ScreenToWorldPoint(touch.position);
                        touchPos = new Vector2(wp.x, wp.y);
                        if(direction * (touchPos.x - startPos) > 0)
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


        if (accuracy < -1 || accuracy > 1)
        {
            return;
        }
        else
        {
            sm.TapNote(accuracy, transform);

            accuracy = targetBeat - conductor.songPositionInBeats;
            if (direction > 0)
                tm.AddTrack();
            else
                tm.RemoveTrack();

            GameObject.Destroy(gameObject);

        }

    }
}