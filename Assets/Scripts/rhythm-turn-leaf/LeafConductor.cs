using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FroggyNamespace;

namespace FroggyNamespace
{
    public enum GameState
    {
        Intro,
        Playing,
        Paused,
        Win,
        Lose
    }
}

public class LeafConductor : MonoBehaviour
{
    public static LeafConductor instance { get; private set; }

    [SerializeField]
    float songBpm;

    [SerializeField]
    float clipLength;

    [SerializeField]
    float firstBeatOffset;

    [SerializeField, Range(0f, 8f)]
    public float beatsShownInAdvance;

    [SerializeField]
    GameObject player;

    public GameState currentGameState = GameState.Intro;
    
    float secPerBeat;
    float songPosition;
    public float songPositionInBeats;
    float dspSongTime;
    float [] notes;
    public int nextIndex;
    // AudioSource musicSource;

    [SerializeField]
    TextMeshProUGUI countdownText;

    [SerializeField]
    TextMeshProUGUI gameStateText;

    Vector3 prevNotePosition;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    void Start()
    {
        // musicSource = GetComponent<AudioSource>();
        // clipLength = musicSource.clip.length;
        // musicSource.Play();
        nextIndex = 0;
        notes = new float[(int)Mathf.Floor(clipLength)];
        for(int i = 0; i < Mathf.Floor(clipLength); i++)
        {
            notes[i] = (float)i;
        }
    }

    void Update()
    {
        if(currentGameState == GameState.Playing)
        {   
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
            songPositionInBeats = songPosition / secPerBeat;
            
            if(nextIndex < notes.Length && notes[nextIndex] < songPositionInBeats)
            {
                player.GetComponent<MovePlayer>().Jump();
                nextIndex++;
            } 
            else if (songPositionInBeats < 0f)
            {
                countdownText.SetText("{0:0}", Mathf.Floor(-songPositionInBeats)+1);
            }
            else
            {
                countdownText.gameObject.SetActive(false);
            }
        }

        switch(currentGameState)
        {
            case GameState.Win:
                gameStateText.SetText("Froggy won!");
                gameStateText.gameObject.SetActive(true);
                break;
            
            case GameState.Lose:
                gameStateText.SetText("Froggy lost :(");
                gameStateText.gameObject.SetActive(true);
                break;
        }
    }

    public void StartGame()
    {
        currentGameState = GameState.Playing;
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
    }
}
