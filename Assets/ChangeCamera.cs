using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour {
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    GameObject emptyTarget;
    
    Vector3 poseBeforeChanges, poseAfterChanges;

    Quaternion rotBeforeChanges, rotAfterChanges;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            GameManager.Instance.ToggleCameraFollow();
            Debug.Log("OnTriggerEnter");
            poseBeforeChanges= mainCamera.transform.position;
            rotBeforeChanges = mainCamera.transform.rotation;
            poseAfterChanges = emptyTarget.transform.position;
            rotAfterChanges = emptyTarget.transform.rotation;
            StartCoroutine(ChangeCameraPosition());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("OnTriggerExit");
            StartCoroutine(ReverseCameraPosition());
        }
    }

    private IEnumerator ChangeCameraPosition() {
        Debug.Log(Vector3.Distance(mainCamera.transform.position, poseAfterChanges));

        while (!Mathf.Approximately(Vector3.Distance(mainCamera.transform.position, poseAfterChanges), 0.0f)) {
            Debug.Log(Vector3.Distance(mainCamera.transform.position, poseAfterChanges));
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, poseAfterChanges, 0.8f);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, rotAfterChanges, 0.2f);
            yield return null;
        }
    }

    private IEnumerator ReverseCameraPosition()
    {
        while (!Mathf.Approximately(Vector3.Distance(mainCamera.transform.position, poseBeforeChanges), 0.0f))
        {
            Debug.Log(Vector3.Distance(mainCamera.transform.position, poseAfterChanges));

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, poseBeforeChanges, 0.8f);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, rotBeforeChanges, 0.2f);
            yield return null;
        }
        GameManager.Instance.ToggleCameraFollow();
    }
}
