using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    #region Variables
    // FIELDS //
    [SerializeField]
    GameObject Mark;
    [SerializeField]
    float Offset = -8.5f;
    [SerializeField]
    int Reverse = 1;

    LineRenderer MyLineRenderer;
    Vector3 MarkPosition;

    float[] Samples = new float[512];
    float[] MarkPositions;

    // PUBLIC PROPERTIES //
    public AudioSource MyAudioSource { get; private set; }
    #endregion

    #region Unity Methods
    void Awake()
    {
        MyAudioSource = GetComponent<AudioSource>();
        MyLineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        MyLineRenderer.positionCount = Samples.Length/2;
        MarkPositions = new float[Samples.Length/2];

        transform.position = new Vector3(-Samples.Length / 4 + transform.position.x, transform.position.y, transform.position.z);

        for (int i = 0; i < Samples.Length/2; i++)
        {
            MarkPositions[i] = transform.position.x + i/5;
        }
    }

    void Update()
    {
        MyAudioSource.GetSpectrumData(Samples, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < Samples.Length/2; i++)
        {
            MarkPosition.Set(MarkPositions[i], Reverse*Mathf.Clamp(Samples[i] * (50*i/16), 0, 50) + Offset, 0);
            MyLineRenderer.SetPosition(i, MarkPosition - transform.position);
        }
    }
    #endregion

    #region Public Methods
    public void SetSong(AudioClip newSong)
    {
        MyAudioSource.clip = newSong;
    }
    #endregion
}