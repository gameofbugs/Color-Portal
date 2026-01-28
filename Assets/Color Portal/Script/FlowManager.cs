using UnityEngine;

public class FlowManager : MonoBehaviour
{
    [SerializeField] PortalColor worldColor;
    public GameObject portalPrefab;
    public GameObject crystalPrefab;
    public Transform[] spawnPoint;

    void Start()
    {
        int index = Random.Range(0, spawnPoint.Length);
        //GameObject portal = Instantiate(portalPrefab);
        //portal.GetComponent<Portal>().SetPortalColor(PortalColor.Gray);
        Instantiate(crystalPrefab, spawnPoint[index]);
    }
}
