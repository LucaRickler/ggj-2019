using UnityEngine;

public class Finale : MonoBehaviour {
    public string TargetName;

    public Animator MainCamera;
    public GameObject External;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == TargetName) {
            External.SetActive(true);
            MainCamera.SetTrigger("Ending");
        }
    }
}