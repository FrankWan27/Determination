using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapNoteController : NoteContoller
{
    private void OnMouseDown()
    {
        Debug.Log(beat - ns.beat);
        GameObject.Destroy(gameObject);
    }
}
