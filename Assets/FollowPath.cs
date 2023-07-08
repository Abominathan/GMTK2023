using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // Speed is how many seconds to get to the next knot point
    public float speed = 60f;
    public Transform obj;
    public Transform[] bz_points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = (int)Mathf.Floor(Time.time / speed);
        float t_value = (Time.time % speed) / speed;

        if (i < (bz_points.Length - 1))
        { 
            Vector3 lbz1c = 2f * bz_points[i + 1].Find("Knot").localPosition;
            Vector3 mirror_control = lbz1c - bz_points[i + 1].Find("Control").localPosition;

            Vector3 temp = bernstien_bezeir(
                bz_points[i].Find("Knot").position,
                 bz_points[i].Find("Control").position,
                  bz_points[i + 1].Find("Knot").position,
                   mirror_control,
                   t_value);

            obj.transform.position = temp;
        }
    }

    Vector3 bernstien_bezeir(Vector3 bz_k0, Vector3 bz_c0, Vector3 bz_k1, Vector3 bz_c1, float t)
    {
        float c0 = -1f * Mathf.Pow(t, 3f) + 3f * Mathf.Pow(t, 2f) - 3f * t + 1f;
        float c1 = 3f * Mathf.Pow(t, 3f) - 6f * Mathf.Pow(t, 2f) + 3f * t;
        float c2 = -3f * Mathf.Pow(t, 3f) + 3f * Mathf.Pow(t, 2f);
        float c3 = Mathf.Pow(t, 3f);

        Vector3 p = c0 * bz_k0;
        p += c1 * bz_c0;
        p += c2 * bz_c1;
        p += c3 * bz_k1;

        return p;
    }
}

