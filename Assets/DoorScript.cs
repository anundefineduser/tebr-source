using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("References")]
    public MeshRenderer renderer;
    public Collider collider;
    public AudioSource source;

    [Header("Variables")]
    public float openTime; // Amount of time the door is open for.
    public float lockTime; // Amount of time the door is locked for.

    [Header("Feedback")]
    public Material closed;
    public Material open;
    public AudioClip openSound;
    public AudioClip closeSound;

    void Start()
    {
        if (!renderer)
            renderer = GetComponent<MeshRenderer>();
        if (!collider)
            collider = GetComponent<Collider>();
        if (!source)
            source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (openTime > 0f)
        {
            openTime -= lockTime > 0f ? openTime : Time.deltaTime;
            if (openTime <= 0f)
                PlaySound(closeSound);
        }

        collider.enabled = lockTime > 0f || openTime <= 0f;
        renderer.material = collider.enabled ? closed : open;
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
