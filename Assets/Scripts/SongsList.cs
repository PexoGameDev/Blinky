using UnityEngine;
using System.IO;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class SongsList : MonoBehaviour {

    #region Variables
    // FIELDS //
    UIController UIcontroller;
    Song ActualSong;

    public string[] SongsPaths;

#if UNITY_EDITOR
    string MusicFolderURL = "D:/Muzyka/UnityTest";
#elif UNITY_ANDROID
    string MusicFolderURL = "/sdcard/Music";
#endif
#endregion

#region Data Structures
    public struct Song
    {
        public Song(string name, AudioClip clip)
        {
            Name = name;
            Clip = clip;
        }
        public string Name { get; }
        public AudioClip Clip { get; }
    }
#endregion

#region Unity Methods
    void Awake () 
	{
        UIcontroller = FindObjectOfType<UIController>();
        FindAllSongs();
    }
#endregion

#region Public Methods
    // PUBLIC METHODS //

#endregion

#region Private Methods
    // PRIVATE METHODS //

    public void FindAllSongs()
    {
        FileInfo[] SoundFiles;
        string[] SongsPathsArray;
        string tmpAllPaths = "";

        var DirInfo = new DirectoryInfo(MusicFolderURL);

        if (DirInfo.Exists)
        {
            SoundFiles = DirInfo.GetFiles("*.mp3", SearchOption.AllDirectories);


            SongsPathsArray = new string[SoundFiles.Length];
            for (int i = 0; i < SoundFiles.Length; i++)
            {
                SongsPathsArray[i] = SoundFiles[i].FullName;
                tmpAllPaths += SongsPathsArray[i] + ";";
            }

            tmpAllPaths = tmpAllPaths.Substring(0, tmpAllPaths.Length - 1);

            PlayerPrefs.SetString("SongsPaths", tmpAllPaths);

            SongsPaths = SongsPathsArray;
        }
    }

    public Song GetRandomSong()
    {
        StartCoroutine(GetSong(SongsPaths[(int)UnityEngine.Random.Range(0f,SongsPaths.Length)]));
        return ActualSong;
    }

    IEnumerator GetSong(string PathToSong)
    {
        WWW www;
        AudioClip TmpClip;
        FileInfo SoundFile = new FileInfo(PathToSong);
        string SongName = "";

        if (SoundFile.Exists)
        {
            www = new WWW("file://" + SoundFile.FullName);

            while (www.keepWaiting)
                yield return null;

            TmpClip = www.GetAudioClipCompressed(false, AudioType.MPEG);

            while (www.keepWaiting)
                yield return null;

            SongName = SoundFile.FullName.Split('\\')[SoundFile.FullName.Split('\\').Length - 1];

            SongName = SongName.Substring(0, SongName.Length - 4);

            ActualSong = new Song(SongName, TmpClip);
        }
        else
        {
            UIcontroller.SetSongsListText("DIRECTORY DOESN'T EXIST");
        }

        yield return null;
    }
#endregion
}