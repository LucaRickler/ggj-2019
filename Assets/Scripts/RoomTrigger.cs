using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour {
    [SerializeField]
    private GameObject hallRoom, catapultRoom;
    public GameObject HallRoom {
        get { return hallRoom; }
    }

    public GameObject CatapultRoom
    {
        get { return catapultRoom; }
    }

    public float FadeSpeed = 0.6f;


    MeshRenderer[] hallMeshes, catapultMeshes;

    // Use this for initialization
    void Awake() {
        if (HallRoom != null) {
            hallMeshes = HallRoom.GetComponentsInChildren<MeshRenderer>();
        }
        if (CatapultRoom != null)
        {
            catapultMeshes = CatapultRoom.GetComponentsInChildren<MeshRenderer>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { // Enter the catapult room
            Debug.Log("On enter");

            StartCoroutine(HideRoom(hallMeshes));
            StartCoroutine(ShowRoom(catapultMeshes));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        { // Enter the catapult room
            Debug.Log("On exit");
            StartCoroutine(HideRoom(catapultMeshes));
            StartCoroutine(ShowRoom(hallMeshes));
        }
    }
    private IEnumerator HideRoom(MeshRenderer[] room)
    {
        foreach (MeshRenderer m in room)
        {
            Debug.Log(m.material.color);
                while (!Mathf.Approximately(m.material.GetFloat("_Transparent"), 0.0f))
                {
                    //Color c = m.material.color;
                    float tmp = m.material.GetFloat("_Transparent") - FadeSpeed * Time.deltaTime;
                    if (tmp < 0) tmp = 0.0f;
                    m.material.SetFloat("_Transparent", tmp);//-= FadeSpeed * Time.deltaTime;
                   // m.material.color = c;
                    yield return null;
                }
        }
    }
    private IEnumerator ShowRoom(MeshRenderer[] room)
    {
        foreach (MeshRenderer m in room)
        {
            while (!Mathf.Approximately(m.material.GetFloat("_Transparent"), 1.0f))
            {
                //Color c = mat.color;
                float tmp = m.material.GetFloat("_Transparent") + FadeSpeed * Time.deltaTime;
                if (tmp > 1) tmp = 1.0f;
                m.material.SetFloat("_Transparent", tmp);//+= FadeSpeed * Time.deltaTime;
                //mat.color = c;
                yield return null;
            }
        }
    }
}
