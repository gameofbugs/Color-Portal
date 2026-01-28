using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    List<GameObject> crystalList = new List<GameObject>();
    public Animator sceneTransitionAnimator;
    public GameObject vfx;
    public TextMeshProUGUI crystalText;
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
    }
    public void CollectCrystal(GameObject crystal)
    {
        if (!crystalList.Contains(crystal))
        {
            crystalList.Add(crystal);
            crystalText.text = $"{crystalList.Count}";
        }
    }
}
public enum PortalColor
{
    Gray,
    Red,
    Blue
}