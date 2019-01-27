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

    Coroutine coroutine = null;

    public void ShowCredits() {
        Debug.Log("ON show credits");
        coroutine = StartCoroutine(CreditsScreen());
    }

    private IEnumerator CreditsScreen() {
        Credits.SetActive(true);

        //Fade in
        while (!Mathf.Approximately(FadeImage.alpha, 1.0f) && !exitCredits)
        {
           FadeImage.alpha += FadeSpeed * Time.deltaTime;
           yield return null;
        }
        exitCredits = false;
        Text txt = Credits.GetComponentInChildren<Text>();
        Debug.Log(txt.rectTransform.localPosition);
        while (txt.rectTransform.localPosition.y <= 650 && !exitCredits)
        {
            Vector3 tmp = txt.rectTransform.localPosition;
            tmp.y += FadeSpeed * 20 * Time.deltaTime;
            txt.rectTransform.localPosition = tmp;
            yield return null;
        }
        while (!Mathf.Approximately(FadeImage.alpha, 0.0f) && !exitCredits)
        {
            FadeImage.alpha -= FadeSpeed * Time.deltaTime;
            yield return null;
        }
        if (exitCredits) {
            FadeImage.alpha = 0;
            Vector3 tmp = txt.rectTransform.localPosition;
            tmp.y = -580;
            txt.rectTransform.localPosition = tmp;
        }
        //1coroutine = null;
        Credits.SetActive(false);
        exitCredits = false;
        //yield return new WaitForSeconds(1);

       /* while (!Mathf.Approximately(cg.color.a, 0.0f))
        {
            Color c = cg.color;
            c.a -= FadeSpeed * Time.deltaTime;
            cg.color = c;
            yield return null;
        }
        coroutine = null;
        Credits.SetActive(false);*/
        //yield return null;
    }

    void Update() {
        if (!Credits.activeSelf && InputManager.Up() && !fading)
        {
            StartCoroutine(Fade());
        }
        else if (Credits.activeSelf && !exitCredits && InputManager.Exit()) {
            Debug.Log("ON exit");
            exitCredits = true;
            //StopCoroutine(coroutine);
            //StopCoroutine("CreditsScreen");
            /*Debug.Log("Esc clicked");
            StopCoroutine(coroutine);
            Image cg = Credits.GetComponent<Image>();
            Color c = cg.color;
            c.a = 0.0f;
            cg.color = c;
            Text txt = Credits.GetComponentInChildren<Text>();
            Vector3 v = txt.rectTransform.localPosition;
            v.y = -580;
            txt.rectTransform.localPosition = v;
            coroutine = null;
            Credits.SetActive(false);*/
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