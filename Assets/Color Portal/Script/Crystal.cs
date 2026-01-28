using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] CrystalColor crystalColor;
    [SerializeField] GameObject destroyVfx;
    [SerializeField] GameObject parent;
    void OnDestroy()
    {

    }
    public void PlayVfx()
    {
        StartCoroutine(WaitToPlayVfx());
    }
    IEnumerator WaitToPlayVfx()
    {
        Instantiate(destroyVfx, transform);
        yield return new WaitForSeconds(1f);
        Destroy(parent);
    }
}
public enum CrystalColor
{
    RedCrystal,
    BlueCrystal
}
