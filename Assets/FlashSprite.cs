using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashSprite : MonoBehaviour
{
    public float timeon = 2f;
    public float timeoff = 1f;
    private float nextflip;
    void Start()
    {
        nextflip = Time.time + timeon;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextflip)
        {
            bool flip = !this.gameObject.GetComponent<Image>().enabled;
            if (flip) {nextflip = Time.time + timeon;}
            else {nextflip = Time.time + timeoff;}
            Debug.Log(nextflip);
            this.gameObject.GetComponent<Image>().enabled = flip;
        }


    }
}
