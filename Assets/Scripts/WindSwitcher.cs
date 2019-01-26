using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSwitcher : MonoBehaviour {

    public void Switch() {
        StartCoroutine(Switcher());
    }

    private IEnumerator Switcher() {
        yield return new WaitForSeconds(1);
        Current[] list = GameManager.Instance.ListCurrents;
        foreach (Current o in list) {
            o.SwapDirection();
        }
    }
}
