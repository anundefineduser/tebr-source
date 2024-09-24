using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

public class CameraScript : MonoBehaviour
{
    [Header("References")]
    public Camera camera;
    public PlayerScript player;

    [Header("Camera Variables")]
    public float sensitivity; //TODO: Have it load saved preference.
    [NaughtyAttributes.ReadOnly, NaughtyAttributes.Label("Camera Turned")] public bool turned;
    [NaughtyAttributes.Label("Lock Camera Y")] public bool lockY = true;
    public float walkingFOV;
    public float runningFOV;
    [NaughtyAttributes.Label("FOV Lerp Speed")] public float lerpFOV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.current.isPaused) return;

        turned = Input.GetKey(KeyCode.Space); //TODO: Replace GetKey with new Unity Input System code. Checks if space/turn button is held.
        float xDelta = Input.GetAxis("Mouse X") * sensitivity * Time.timeScale; //TODO: Replace GetAxis with new Unity Input System code. 
        float yDelta = Input.GetAxis("Mouse Y") * sensitivity * Time.timeScale; //TODO: Replace GetAxis with new Unity Input System code.
        player.transform.rotation = player.transform.rotation * Quaternion.Euler(Vector3.up * xDelta); // Rotates the Player by the mouse x delta.
        camera.transform.localRotation = Quaternion.Euler(camera.transform.localRotation.eulerAngles.x - (lockY ? 0f : yDelta), turned ? 180f : 0f, camera.transform.localRotation.eulerAngles.z); // Rotates the Camera by the mouse y delta, as well as turning it 180 if space/turn button is held.

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, player.isRunning ? runningFOV : walkingFOV, lerpFOV); // Lerps camera FOV to either the walking, or running FOV depending on Player, speed is `lerpFOV`.
    }
}
