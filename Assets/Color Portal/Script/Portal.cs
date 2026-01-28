using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] PortalColor portalColor;

    public void Enter()
    {
        StartCoroutine(SceneTransition(portalColor.ToString()));
    }
    public void SetPortalColor(PortalColor color)
    {
        portalColor = color;
    }
    IEnumerator SceneTransition(String sceneName)
    {
        GameManager.Instance.vfx.SetActive(true);
        GameManager.Instance.sceneTransitionAnimator.ResetTrigger("Start");
        GameManager.Instance.sceneTransitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.sceneTransitionAnimator.ResetTrigger("End");
        GameManager.Instance.sceneTransitionAnimator.SetTrigger("Start");
        GameManager.Instance.vfx.SetActive(false);
    }
}
