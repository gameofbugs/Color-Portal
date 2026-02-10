using System;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    [SerializeField] PortalColor worldColor;
    public GameObject portalPrefab;
    public GameObject crystalPrefab;
    public Transform spawnPoint;
    public Crystal crystal;

    void Start()
    {
        crystalPrefab.SetActive(true);
        crystal.OnCrystalCollected += OnCrystalColledted;
    }

    private void OnCrystalColledted()
    {
        portalPrefab.SetActive(true);
    }
}