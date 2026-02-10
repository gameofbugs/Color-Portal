using System;
using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public CrystalColor crystalColor;
    [SerializeField] GameObject destroyVfx;
    [SerializeField] GameObject parent;
    public void PlayVfx()
    {
        AudioManager.Instance.PlayCrystalCollected();
        OnCrystalCollected?.Invoke();
        StartCoroutine(WaitToPlayVfx());
    }

    IEnumerator WaitToPlayVfx()
    {
        Instantiate(destroyVfx, transform);
        yield return new WaitForSeconds(1f);
        Destroy(parent);
    }
    public event Action OnCrystalCollected;
}

public enum CrystalColor
{
    RedCrystal,
    BlueCrystal,
    Yellow
}