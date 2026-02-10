using UnityEngine;

public class SceneColorController : MonoBehaviour
{
    public GameObject redObj;
    public GameObject blueObj;
    public GameObject yellowObj;
    public GameObject redPortal;
    public GameObject bluePortal;
    public GameObject yellowPortal;

    void OnEnable()
    {
        UpdateColors();
    }

    public void UpdateColors()
    {

        if (GameManager.Instance != null && GameManager.Instance.crystalList.Count > 0)
        {
            if (GameManager.Instance.crystalList.Contains(CrystalColor.RedCrystal))
            {
                redObj.SetActive(true);
                redPortal.SetActive(false);
            }
            if (GameManager.Instance.crystalList.Contains(CrystalColor.BlueCrystal))
            {
                blueObj.SetActive(true);
                bluePortal.SetActive(false);
            }
            if (GameManager.Instance.crystalList.Contains(CrystalColor.Yellow))
            {
                yellowObj.SetActive(true);
                yellowPortal.SetActive(false);
            }
        }
    }
}
