using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI : MonoBehaviour {
    public GameObject Credits;

    public GameObject Menu;

    private bool fading = false;

    public float FadeSpeed = 2.0f;

    public void ShowCredits() {
        StartCoroutine(CreditsScreen());
    }

    private IEnumerator CreditsScreen() {
        yield return null;
    }

    void Update() {
        if(!Credits.activeSelf && InputManager.Up() && !fading) {
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade() {
        fading = true;
        CanvasGroup cg = Menu.GetComponent<CanvasGroup>();
        while(!Mathf.Approximately(cg.alpha, 0.0f)) {
            cg.alpha -= FadeSpeed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}