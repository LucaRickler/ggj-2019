using UnityEngine;

public class Finale : MonoBehaviour {
    public string TargetName;

    public Animator MainCamera;
    public GameObject External;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == TargetName) {
            //Camera.SetupCurrent(MainCamera.GetComponent<Camera>());
            External.SetActive(true);
            //MainCamera.SetTrigger("Ending");
        }
    }
}