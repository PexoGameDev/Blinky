using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    #region Variables
    // FIELDS //
    [SerializeField] 
    Button[] Buttons; // 0-Next Song | 1 - 
    [SerializeField]
    Text[] Texts; // 0-SongTitle | 1-SongList
	// PUBLIC PROPERTIES //


	// PRIVATE PROPERTIES //
	
	#endregion
	
	#region Unity Methods
	void Awake () 
	{
        AddButtonsListeners();
    }

    void Update () 
	{
		
	}
	#endregion

	#region Public Methods
	// PUBLIC METHODS //
    public void SetSongTitleText(string songTitle)
    {
        Texts[0].text = songTitle;
    }
    public void SetSongsListText(string songsList)
    {
        Texts[1].text = songsList;
    }
    #endregion

    #region Private Methods
    // PRIVATE METHODS //
    void AddButtonsListeners()
    {
        Buttons[0].onClick.AddListener(delegate
        {
            GameController.SetSong();
        });
            //SongsList.Songs[(int)Random.Range(0f, SongsList.Songs.Length)].Clip); });

    }
	#endregion
}
