using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUI : MonoBehaviour {
    public GameObject Credits;

    public GameObject Menu;

    public CanvasGroup FadeImage;

    private bool fading = false;
    private bool exitCredits = false;

    public float FadeSpeed = 2.0f;


    public void ShowCredits() {
        StartCoroutine(CreditsScreen());
    }

    private IEnumerator CreditsScreen() {
        Credits.SetActive(true);

        while (!Mathf.Approximately(FadeImage.alpha, 1.0f) && !exitCredits)
        {
           FadeImage.alpha += FadeSpeed * Time.deltaTime;
           yield return null;
        }
        exitCredits = false;
        RectTransform txt = Credits.GetComponentInChildren<RectTransform>();

        while (txt.localPosition.y <= 900 && !exitCredits)
        {
            Vector3 tmp = txt.localPosition;
            tmp.y += FadeSpeed * 20 * Time.deltaTime;
            txt.localPosition = tmp;
            yield return null;
        }

        while (!Mathf.Approximately(FadeImage.alpha, 0.0f) && !exitCredits)
        {
            FadeImage.alpha -= FadeSpeed * Time.deltaTime;
            yield return null;
        }

        if (exitCredits) {
            FadeImage.alpha = 0;
            Vector3 tmp = txt.localPosition;
            tmp.y = -680;
            txt.localPosition = tmp;
        }
        Credits.SetActive(false);
        exitCredits = false;
    }

    void Update() {
        if (!Credits.activeSelf && InputManager.Up() && !fading)
        {
            StartCoroutine(Fade());
        }
        else if (Credits.activeSelf && !exitCredits && InputManager.Exit()) {
            exitCredits = true;
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