using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    [Header("Cursor Variables")]
    public bool cursorLocked;
    public bool cursorVisible;

    [Header("Reticle Variables")]
    public GameObject reticle;
    public bool reticleSelect;
    public GameObject select;

    public static ReticleManager current;
    void Awake()
    {
        current = this;
    }

    void Update()
    {
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = cursorVisible;

        reticle.SetActive(!Cursor.visible);
        select.SetActive(reticleSelect);
    }
}
