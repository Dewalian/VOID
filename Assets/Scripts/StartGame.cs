using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public AudioSource src;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Gameplay");
            src.Play();
        }
    }
}
