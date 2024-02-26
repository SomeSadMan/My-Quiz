using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource mainMusic;
    [SerializeField] private GameObject buttonOff;
    [SerializeField] private GameObject buttonOn;
    private static MusicController instance;


    private void Awake()
    {
        mainMusic.Play();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    public void TurnMusicOff()
    {
        mainMusic.volume = 0;
        buttonOff.SetActive(false);
        buttonOn.SetActive(true);


    }
    public void TurnMusicOn()
    {
        mainMusic.volume = 0.3f;
        buttonOn.SetActive(false);
        buttonOff.SetActive(true);
    }
}
