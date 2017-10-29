using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

public class GameController : MonoBehaviour {

    #region Variables
    // FIELDS //
    static AudioSource[] AudioSources;
    static AudioVisualizer[] AudioVisualizers;
    static IEnumerable<SongsList.Song> NewSongs;
    static SongsList.Song[] SongsToRandom;
    static AudioClip NewSong;
    static SongsList AllSongsList;

    UIController UIcontroller;
    // PUBLIC PROPERTIES //


    // PRIVATE PROPERTIES //

    #endregion

    #region Unity Methods
    void Awake()
    {
        AllSongsList = FindObjectOfType<SongsList>();
        UIcontroller = FindObjectOfType<UIController>();

        LookForStoredSongsPaths();

        FindAudioVisualizers();
    }
    #endregion

    #region Public Methods
    // PUBLIC METHODS //
    public static void SetSong(AudioClip newSong)
    {
        for (int i = 0; i < AudioVisualizers.Length; i++)
        { 
            AudioVisualizers[i].SetSong(newSong);
            AudioSources[i].Play();
        }
    }

    public static void SetSong()
    {
        NewSong = AllSongsList.GetRandomSong().Clip;
        for (int i = 0; i < AudioVisualizers.Length; i++)
        {
            AudioVisualizers[i].SetSong(NewSong);
            AudioSources[i].Play();
        }
    }
    #endregion

    #region Private Methods
    // PRIVATE METHODS //

    void LookForStoredSongsPaths()
    {
        string StoredSongsPaths = PlayerPrefs.GetString("SongsPaths");
        if (!String.IsNullOrEmpty(StoredSongsPaths))
        {
            AllSongsList.FindAllSongs();
        }
        else
        {
            AllSongsList.SongsPaths = StoredSongsPaths.Split(';');
        }
    }

    void FindAudioVisualizers()
    {
        AudioVisualizers = FindObjectsOfType<AudioVisualizer>();
        AudioSources = new AudioSource[AudioVisualizers.Length];

        for (int i = 0; i < AudioVisualizers.Length; i++)
        {
            AudioSources[i] = AudioVisualizers[i].MyAudioSource;
        }
    }
    #endregion
}
