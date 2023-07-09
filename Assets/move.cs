using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class move : MonoBehaviour
{
    
    public Transform obj;
    public Transform character;
    public GameObject score;

    private TMP_Text score_text;
    private float score_value = 0;

    public float speed = 100;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;
    private Vector3 current_input = Vector3.zero;

    public void Start()
    {
        score_text = score.GetComponent<TMP_Text>();
    }

    public void Update()
    {
        Vector3 distanceBetweenObjects = transform.position - character.transform.position;
        // Seperates scoring portion in the x and y direction. 4f/2f are the width of camera
        float x_score = Mathf.Max(0f, 3.3f - Mathf.Abs(distanceBetweenObjects[0]));
        float y_score = Mathf.Max(0f, 1.4f - Mathf.Abs(distanceBetweenObjects[1]));
        // Squared it so that being closer gives a bigger benefit
        score_value += Mathf.Pow(x_score*y_score, 2)*Time.deltaTime*5f;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 temp_input = new Vector3(h, v, 0).normalized;
        current_input = Vector3.SmoothDamp(current_input, temp_input, ref velocity, smoothTime);


        Vector3 tempspeed = current_input * speed * Time.deltaTime;

        obj.transform.position += tempspeed;
        // Bad fix to the score going to 2 at first
        if (score_value < 4)
        {
            score_text.SetText("Score: 0");
        }
        else
        {
            score_text.SetText("Score: " + Mathf.RoundToInt(score_value).ToString());
        }
    }

    public float get_score()
    {
        return score_value;
    }
}