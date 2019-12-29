using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNoteController : NoteContoller
{
  
    public int section = 1; //1 = start, 0 = mid, -1 = end

    TrackManager tm;

    private new void Start()
    {
        base.Start();
        tm = GameObject.Find("TrackManager").GetComponent<TrackManager>();
    }
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

                Vector3 wp;
                Vector2 touchPos;
                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        if (section == 1)
                        {
                            wp = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPos = new Vector2(wp.x, wp.y);
                            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                            {
                                OnTap();
                            }
                        }
                        break;
                    default:
                        if (section == 0 || section == -1)
                        {
                            wp = Camera.main.ScreenToWorldPoint(touch.position);
                            touchPos = new Vector2(wp.x, wp.y);
                            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                            {
                                OnTap();
                            }
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

            GameObject.Destroy(gameObject);

        }

    }
}