using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject perfectPrefab;
    public GameObject latePrefab;
    public GameObject earlyPrefab;
    public GameObject missPrefab;

    public Text scoreText;
    public Text comboText;

    public float score = 0;
    public float combo = 0;

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        comboText.text = "Combo: " + combo.ToString() + "x";
    }


    public void TapNote(float accuracy, Transform target)
    {
        if (accuracy < -0.5f || accuracy > 0.5f)
        {
            Debug.Log("Miss");
            Instantiate(missPrefab, new Vector3(target.position.x, -6, 0), Quaternion.identity);
            combo = 0;

        }
        else if (accuracy < -0.2f)
        {
            Debug.Log("Late");
            Instantiate(latePrefab, target.position, Quaternion.identity);
            combo++;
            score += combo * 150;

        }
        else if (accuracy > 0.2f)
        {
            Debug.Log("Early");
            Instantiate(earlyPrefab, target.position, Quaternion.identity);
            combo++;
            score += combo * 150;

        }
        else
        {
            Debug.Log("Perfect");
            Instantiate(perfectPrefab, target.position, Quaternion.identity);
            combo++;
            score += combo * 300;
        }
    }
}
