using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatFade : MonoBehaviour
{
    public float lifespan = 1f;
    public float floatSpeed = 0.5f;
    float currentLife = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * floatSpeed * Time.deltaTime);

        currentLife += Time.deltaTime;

        if(currentLife > lifespan)
        {
            GameObject.Destroy(gameObject);
        }

    }
}
