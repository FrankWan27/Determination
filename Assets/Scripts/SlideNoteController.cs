using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideNoteController : NoteContoller
{


    private new void Update()
    {
        base.Update();

        //probably need to change to account for closeby notes
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    OnTap();
                }


            }
        }
    }

    private void OnTap()
    {
        Debug.Log(targetBeat - conductor.songPositionInBeats);

        accuracy = targetBeat - conductor.songPositionInBeats;
        if (accuracy < -0.2f || accuracy > 0.2f)
        {
            return;
        }
        else
        {
            sm.TapNote(0, transform);



            GameObject.Destroy(gameObject);

        }

    }
}