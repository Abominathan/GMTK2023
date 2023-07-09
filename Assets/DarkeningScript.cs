using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkeningScript : MonoBehaviour
{
    public float decaytime = 1.1f;
    public float offtime = 0.4f;
    private float nextflip;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        nextflip = Time.time + decaytime + offtime;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (nextflip > Time.time)
        {
            float t = (nextflip - (Time.time + offtime)) / decaytime;
            if (t >= 0f)
            {
                Color lerpedColor = Color.Lerp(Color.black, Color.white, t);
                spriteRenderer.material.color = lerpedColor;
            }
        }
        else
        {
            nextflip = Time.time + decaytime + offtime;
            spriteRenderer.material.color = Color.white;
        }


    }
}
