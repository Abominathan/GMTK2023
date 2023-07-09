using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowPath : MonoBehaviour
{
    // Speed is how many seconds to get to the next knot point
    public float speed = 60f;
    public float angle_speed = 60f;
    public Transform obj;
    public Transform[] bz_points;

    public TMP_Text finish_text;
    public TextMeshProUGUI b2;
    public string b2_text;
    public GameObject end_panel;
    public GameObject move_obj;

    private float repeat_time = 0f;


    private void Awake()
    {
        repeat_time = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            Debug.LogFormat("REP "+repeat_time.ToString());
            float run_time = Time.time - repeat_time;
            Debug.LogFormat("RUN " + run_time.ToString());
            int i = (int)Mathf.Floor(run_time / speed);
            float t_value = (run_time % speed) / speed;
            float singleStep = angle_speed * Time.deltaTime;
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
                Vector3 targetDirection = first_dir_bernstien_bezeir(
                    bz_points[i].Find("Knot").position,
                     bz_points[i].Find("Control").position,
                      bz_points[i + 1].Find("Knot").position,
                       mirror_control,
                       t_value);

                // Normal to direction of motion
                targetDirection = Vector3.Cross(targetDirection, new Vector3(0, 0, -1));

                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, singleStep);
            }
            else
            {
                float score_value = move_obj.GetComponent<move>().get_score();
                finish_text.SetText("Skateboard Made it to the end.\n" + Mathf.RoundToInt(score_value).ToString());
                b2.SetText(b2_text);
                end_panel.SetActive(true);
                Time.timeScale = 0;
            }
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
    Vector3 first_dir_bernstien_bezeir(Vector3 bz_k0, Vector3 bz_c0, Vector3 bz_k1, Vector3 bz_c1, float t)
    {
        float c0 = -3f * Mathf.Pow(t, 2f) + 6f * t - 3f;
        float c1 = 9f * Mathf.Pow(t, 2f) - 12f * t + 3f;
        float c2 = -9f * Mathf.Pow(t, 2f) + 6f * t;
        float c3 = 3f * Mathf.Pow(t, 2f);

        Vector3 p = c0 * bz_k0;
        p += c1 * bz_c0;
        p += c2 * bz_c1;
        p += c3 * bz_k1;

        return p;
    }
}

