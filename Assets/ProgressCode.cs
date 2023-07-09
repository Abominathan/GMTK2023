using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ProgressCode : MonoBehaviour
{
    public int next_scene_index;
    public GameObject panel;
    // Start is called before the first frame update

    public void RestartLevel()
    {
        panel.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    public void NextLevel()
    {
        panel.gameObject.SetActive(false);
        SceneManager.LoadScene(next_scene_index);
    }
}
