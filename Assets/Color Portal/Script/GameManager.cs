using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameObject camera_Outro;
    public List<CrystalColor> crystalList = new List<CrystalColor>();
    public Animator sceneTransitionAnimator;
    public GameObject sceneTransition;
    public TextMeshProUGUI crystalText;
    GameState currentState;
    [Header("Timelines")]
    public PlayableAsset outroTimeline;
    public PlayableDirector director;
    public bool introPlayed = false;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SwitchGameState(GameState.Loading);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only Scene 1
        if (scene.buildIndex == 0) // or scene.name == "Scene1"
        {
            // Find timeline again (scene reload destroys reference)
            director = FindAnyObjectByType<PlayableDirector>();
            camera_Outro = GameObject.FindGameObjectWithTag("Outro_Cam");


            if (!introPlayed)
            {
                director.Play();
                introPlayed = true;
            }
            else
            {
                // Skip intro
                director.time = director.duration;
                director.Evaluate();
            }
        }
    }

    public void CollectCrystal(GameObject crystal)
    {
        Crystal cry = crystal.GetComponent<Crystal>();
        if (!crystalList.Contains(cry.crystalColor))
        {
            crystalList.Add(cry.crystalColor);
            //crystalText.text = $"{crystalList.Count}";
        }
    }
    void Update()
    {
        while (currentState == GameState.GameFinished)
        {
            camera_Outro.SetActive(true);
        }
    }
    public void SwitchGameState(GameState newState)
    {
        currentState = newState;
    }
    public GameState CurrentGameState()
    {
        return currentState;
    }

    public void GameOverLogic()
    {
        if (crystalList.Count >= 3)
        {
            TriggerOutroCutscene();
        }
    }
    void TriggerOutroCutscene()
    {
        SwitchGameState(GameState.Exit);
        Time.timeScale = 1f;

        director.Stop();
        director.playableAsset = outroTimeline;
        director.time = 0;
        director.Play();
    }


}
public enum GameState
{
    Loading,
    MainMenu,
    Playing,
    Paused,
    Resumed,
    GameFinished,
    Exit
}
public enum PortalColor
{
    Gray,
    Red,
    Blue,
    Yellow
}