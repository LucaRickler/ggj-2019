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

    private bool player_inside;

    [SerializeField]    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !player_inside) {
            player_inside = true;
            GameManager.Instance.Player.GetComponent<PlayerController>().SetUnmovable(true);
            GameManager.Instance.ToggleCameraFollow();
            //Debug.Log("OnTriggerEnter");
            poseBeforeChanges= mainCamera.transform.position;
            rotBeforeChanges = mainCamera.transform.rotation;
            poseAfterChanges = emptyTarget.transform.position;
            rotAfterChanges = emptyTarget.transform.rotation;
            StartCoroutine(ChangeCameraPosition());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && player_inside)
        {  
            player_inside = false;
           // Debug.Log("OnTriggerExit");
            GameManager.Instance.Player.GetComponent<PlayerController>().SetUnmovable(true);
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
        GameManager.Instance.Player.GetComponent<PlayerController>().SetUnmovable(false);
    }

    private IEnumerator ReverseCameraPosition()
    {
        yield return new WaitForSeconds(1);

        while (!Mathf.Approximately(Vector3.Distance(mainCamera.transform.position, poseBeforeChanges), 0.0f))
        {
            Debug.Log(Vector3.Distance(mainCamera.transform.position, poseAfterChanges));

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, poseBeforeChanges, 0.8f);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, rotBeforeChanges, 0.2f);
            yield return null;
        }
        GameManager.Instance.Player.GetComponent<PlayerController>().SetUnmovable(false);
        GameManager.Instance.ToggleCameraFollow();
    }
}
