using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Story = 0,
    Endless = 1,
}

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    void Awake()
    {
        current = this;
    }

    [Header("References")]
    public AudioSource source;

    [Header("Object References")]
    public PlayerScript player;
    public BaldiScript baldi;

    [Header("Game Data")]
    public int notebooks;
    public int maxNotebooks;
    public GameMode gamemode;
    public bool spoopMode;
    public int failed;

    public GameObject schoolMusic;
    public AudioClip hang;

    public void ActivateSpoopMode()
    {
        spoopMode = true;
        schoolMusic.SetActive(false);
        source.PlayOneShot(hang);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
}
