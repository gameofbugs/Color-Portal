using UnityEngine;

public class Interactor : MonoBehaviour
{
    public enum DistortionState
    {
        Normal,        // 0
        ToBlackWhite,  // 1
        ToColor        // 2
    }

    [Header("State (Inspector Only)")]
    public DistortionState state;

    [Header("State (Terrain Settings)")]
    public Vector2 terrainSize = new Vector2(50f, 50f);
    public Vector2 grassTiling = new Vector2(0.13f, 0.11f);
    public Vector2 soilTiling = new Vector2(0.5f, 0.5f);
    public Vector2 stoneTiling = new Vector2(0.8f, 0.8f);

    [Header("Wave Settings")]
    public float speed = 3f;
    public float maxRadius = 50f;

    [Header("Shader Params (Global)")]
    public float ringThickness = 0.12f;
    public float rainbowIntensity = 5f;
    public float rainbowSpeed = 0.35f;
    public float glowStrength = 1f;

    float radius = 0f;
    DistortionState lastState;

    void Start()
    {
        // Wait for GameManager to be ready before checking state
        if (GameManager.Instance != null && GameManager.Instance.CurrentGameState() != GameState.Loading)
        {
            ForceBlackWhiteInstant();
        }
    }

    void Update()
    {
        // Set terrain shader globals
        Shader.SetGlobalVector("_TerrainSize", terrainSize);
        Shader.SetGlobalVector("_GrassTiling", grassTiling);
        Shader.SetGlobalVector("_SoilTiling", soilTiling);
        Shader.SetGlobalVector("_StoneTiling", stoneTiling);

        // Center always follows this object
        Shader.SetGlobalVector(
            "_Position",
            new Vector4(
                transform.position.x,
                transform.position.y,
                transform.position.z,
                1
            )
        );

        // Detect state change from Inspector
        if (state != lastState)
        {
            radius = 0f;
            lastState = state;
        }

        // Run wave only for active states
        if (state == DistortionState.ToBlackWhite ||
            state == DistortionState.ToColor)
        {
            radius += Time.deltaTime * speed;
            radius = Mathf.Min(radius, maxRadius);
        }

        // Send globals
        Shader.SetGlobalFloat("_Radius", radius);
        Shader.SetGlobalFloat("_DistortionMode", (int)state);
        Shader.SetGlobalFloat("_RingThickness", ringThickness);
        Shader.SetGlobalFloat("_RainbowIntensity", rainbowIntensity);
        Shader.SetGlobalFloat("_RainbowSpeed", rainbowSpeed);
        Shader.SetGlobalFloat("_GlowStrength", glowStrength);
    }

    // ===== TIMELINE / GAME CONTROL METHODS =====

    public void StartToBlackWhite()
    {
        state = DistortionState.ToBlackWhite;
        radius = 0f;
        lastState = state;

    }

    public void StartToColor()
    {
        state = DistortionState.ToColor;
        radius = 0f;
        lastState = state;
    }

    public void ForceBlackWhiteInstant()
    {
        state = DistortionState.ToBlackWhite;
        radius = maxRadius;
        lastState = state;
    }
    public void SetMainMenu()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SwitchGameState(GameState.MainMenu);
    }
}