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
    public DistortionState state = DistortionState.Normal;

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

    void Update()
    {
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
            radius = 0f;          // restart wave
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
}
