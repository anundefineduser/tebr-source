using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager current;
    private void Awake()
    {
        current = this;
    }

    public bool isPaused;
    public float pauseTime => isPaused ? 0 : 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = pauseTime;
        }
    }
}
