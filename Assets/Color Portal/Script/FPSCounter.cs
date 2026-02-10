using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [Header("Settings")]
    public bool showFPS = true;
    public float updateInterval = 0.5f;

    [Header("UI Reference (Optional)")]
    public TextMeshProUGUI fpsText; // For UI Text display

    private float accum = 0f;
    private int frames = 0;
    private float timeleft;
    private float fps;

    void Start()
    {
        timeleft = updateInterval;
    }

    void Update()
    {
        if (!showFPS) return;

        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        if (timeleft <= 0f)
        {
            fps = accum / frames;
            timeleft = updateInterval;
            accum = 0f;
            frames = 0;

            // Update UI Text if assigned
            if (fpsText != null)
            {
                fpsText.text = $"FPS: {fps:F0}";
            }
        }
    }

    void OnGUI()
    {
        if (!showFPS || fpsText != null) return;

        // Fallback to OnGUI if no UI Text assigned
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(10, 10, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = GetFPSColor(fps);

        string text = $"FPS: {fps:F0}";
        GUI.Label(rect, text, style);
    }

    Color GetFPSColor(float currentFPS)
    {
        if (currentFPS >= 60f) return Color.green;
        if (currentFPS >= 30f) return Color.yellow;
        return Color.red;
    }
}