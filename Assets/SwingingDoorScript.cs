using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;
using static Unity.VisualScripting.Member;

public class SwingingDoorScript : MonoBehaviour
{
    [Header("References")]
    public NavMeshObstacle obstacle; // Used for NPCs traversing around the door when locked/not enough notebooks are collected.
    public MeshRenderer renderer;
    public Collider collider;
    public AudioSource source;

    [Header("Variables")]
    public int notebooksNeeded;
    public float openTime; // Amount of time the door is open for.
    public float lockTime; // Amount of time the door is locked for.

    [Header("Tags")]
    public string playerTag;
    [NaughtyAttributes.Label("NPC Tag")] public string npcTag;

    [Header("Feedback")]
    public Material closed;
    public Material open;
    public Material locked;
    public AudioClip openSound;
    [NaughtyAttributes.Label("Notebooks Collection Sound")] public AudioClip nbSound; // Sound for interacting with not enough notebooks.

    void Start()
    {
        if (!obstacle)
            obstacle = GetComponent<NavMeshObstacle>();
        if (!renderer)
            renderer = GetComponent<MeshRenderer>();
        if (!collider)
            collider = GetComponent<Collider>();
        if (!source)
            source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openTime > 0f)
            openTime -= lockTime > 0f ? openTime : Time.deltaTime;
        if (lockTime > 0f)
            lockTime -= Time.deltaTime;

        obstacle.enabled = lockTime > 0f || GameManager.current.notebooks < notebooksNeeded;
        collider.enabled = lockTime > 0f || openTime <= 0f;

        Material material = openTime > 0f ? open : closed;
        renderer.material = lockTime > 0f ? locked : material;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) || other.CompareTag(npcTag))
        {
            if (GameManager.current.notebooks < notebooksNeeded)
            {
                Debug.Log("3");
                //TODO: Notebook sound.
                return;
            }

            if (openTime <= 0f)
                source.PlayOneShot(openSound);
            openTime = 2f;
            //TODO: Open sound. 
        };
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(playerTag) || other.CompareTag(npcTag))
        {
            if (GameManager.current.notebooks >= notebooksNeeded)
                openTime = 2f;
        };
    }
}
