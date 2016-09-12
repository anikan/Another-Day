using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    public AudioClip[] songs;

    public float timeToTransitionSong = 2f;
    private float targetVolume;

    private AudioSource source;
    int currentSongIndex = 0;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        targetVolume = source.volume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void stopSong()
    {
        StartCoroutine(stopSongCoroutine());
    }

    public void startFirstSong()
    {
        currentSongIndex = 0;
        source.clip = songs[currentSongIndex];
        StartCoroutine(startSongCoroutine());

    }

    public void startNextSong()
    {
        currentSongIndex++;

        source.clip = songs[currentSongIndex];

        StartCoroutine(startSongCoroutine());
    }

    /// <summary>
    /// A coroutine to fade out a song.
    /// </summary>
    /// <returns></returns>
    IEnumerator stopSongCoroutine()
    {
        for (float timeLeft = timeToTransitionSong; timeLeft > 0; timeLeft -= Time.deltaTime)
        {
            GetComponent<AudioSource>().volume = timeLeft / timeToTransitionSong * targetVolume;
            yield return null;
        }

        GetComponent<AudioSource>().Stop();
    }


    /// <summary>
    /// A coroutine to fade in a song.
    /// </summary>
    /// <returns></returns>
    IEnumerator startSongCoroutine()
    {
        GetComponent<AudioSource>().Play();

        for (float timeSinceStart = 0; timeSinceStart < timeToTransitionSong; timeSinceStart += Time.deltaTime)

        {
            Debug.Log(timeSinceStart / timeToTransitionSong * targetVolume);
            GetComponent<AudioSource>().volume = timeSinceStart / timeToTransitionSong * targetVolume;
            yield return null;
            timeSinceStart += Time.deltaTime;
        }
    }

}
