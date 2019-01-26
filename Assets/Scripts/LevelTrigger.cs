using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelTrigger : MonoBehaviour {

    public GameObject destroied;
    void OnTriggerEnter(Collider other) {
        StartCoroutine(Fade());
    }

    private  IEnumerator Fade() {
        //TODO: fade here
        yield return null;
        Destroy(destroied);
    }
}