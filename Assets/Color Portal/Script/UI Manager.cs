using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject shortCuts, mainMenu, pauseMenu, keyBinding;

    [Header("Message UI")]
    public Image messageImage;
    public TMP_Text messageText;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.04f;
    public float messageHoldTime = 1.2f;

    private Coroutine messageRoutine;

    void Start()
    {
        // Initialize UI state
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.CurrentGameState() == GameState.MainMenu)
            {
                mainMenu.SetActive(true);
                pauseMenu.SetActive(false);
                shortCuts.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (GameManager.Instance == null)
            return;

        HandleGamePlayUI();
    }

    void HandleGamePlayUI()
    {
        switch (GameManager.Instance.CurrentGameState())
        {
            case GameState.MainMenu:
                mainMenu.SetActive(true);
                HandleMainMenuInput();
                break;

            case GameState.Playing:
                HandlePlayingInput();
                keyBinding.SetActive(true);
                break;

            case GameState.Paused:
                HandlePausedInput();
                break;
        }
    }

    // ---------------- STATES ----------------

    void HandleMainMenuInput()
    {
        // Only Enter or Space makes menu disappear and starts game
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            mainMenu.SetActive(false);  // Hide main menu
            ResumeGame();  // Start game
        }

        // Escape quits application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void HandlePlayingInput()
    {
        // Toggle shortcuts
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            ToggleShortcuts();
            return;
        }

        // Escape pauses game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (shortCuts.activeSelf)
            {
                shortCuts.SetActive(false);
            }
            else
            {
                PauseGame();
            }
        }
    }

    void HandlePausedInput()
    {
        // Only Enter or Space resumes game
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }

        // Escape also resumes
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    // ---------------- ACTIONS ----------------

    void ToggleShortcuts()
    {
        shortCuts.SetActive(!shortCuts.activeSelf);
    }

    void PauseGame()
    {
        GameManager.Instance.SwitchGameState(GameState.Paused);
        pauseMenu.SetActive(true);   // Show pause menu
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        GameManager.Instance.SwitchGameState(GameState.Playing);
        pauseMenu.SetActive(false);  // Hide pause menu
        mainMenu.SetActive(false);   // Hide main menu (if it was showing)
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    // ==================================================
    // 📢 MESSAGE SYSTEM (IMAGE + TMP)
    // ==================================================

    public void ShowMessageSequence(string[] messages)
    {
        if (messageRoutine != null)
            StopCoroutine(messageRoutine);

        messageRoutine = StartCoroutine(MessageSequenceRoutine(messages));
    }

    private IEnumerator MessageSequenceRoutine(string[] messages)
    {
        messageImage.gameObject.SetActive(true);

        foreach (string msg in messages)
        {
            yield return StartCoroutine(TypeText(msg));
            yield return new WaitForSecondsRealtime(messageHoldTime);
        }

        messageImage.gameObject.SetActive(false);
        messageRoutine = null;
    }

    // ---------------- TYPEWRITER ----------------

    private IEnumerator TypeText(string text)
    {
        messageText.text = text;
        messageText.maxVisibleCharacters = 0;

        for (int i = 0; i <= text.Length; i++)
        {
            messageText.maxVisibleCharacters = i;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    // ==================================================
    // 🎮 GAME MESSAGE PRESETS
    // ==================================================

    public void ShowStartingMessage()
    {
        ShowMessageSequence(new string[]
        {
            "WORLD FILLED WITH COLOR",
            "LIFE BREATHING FREELY",
            "EVERY SHADE IN BALANCE",
            "THE WORLD FEELS ALIVE",
            "PEACE HOLDS STRONG",
            "NOTHING SEEMS WRONG",
        });
    }

    public void ShowEarthquakeMessage(float magnitude)
    {
        ShowMessageSequence(new string[]
        {
            "EARTHQUAKE DETECTED",
            $"MAGNITUDE {magnitude:F1}",
            "TAKE COVER IMMEDIATELY",
            "THE GROUND BEGINS TO SHAKE",
            "THE WORLD LOSES CONTROL",
            "STRUCTURES FALL APART",
            "COLORS SCREAM AND FADE",
            "LIGHT IS DRAINED AWAY",
            "BLACK AND WHITE REMAINS",
        });
    }

    public void ShowInstructionMessage()
    {
        ShowMessageSequence(new string[]
        {
            "COLOR STILL EXISTS",
            "IT HAS NOT DISAPPEARED",
            "FRAGMENTS ARE SCATTERED",
            "PORTALS LEAD THE WAY",
            "COLLECT WHAT WAS LOST",
            "RESTORE THE WORLD",
        });
    }

    public void ShowEndingMessage()
    {
        ShowMessageSequence(new string[]
        {
            "THE WORLD BREATHES AGAIN",
            "COLORS RETURN SLOWLY",
            "THE SILENCE BREAKS",
            "BALANCE IS RESTORED",
            "THE WORLD REMEMBERS",
            "THANK YOU FOR STAYING",
        });
    }
}