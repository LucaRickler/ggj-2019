using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoFly : MonoBehaviour {
    [SerializeField]
    private bool _isOnField;
    public bool IsOnField {
        get { return _isOnField; }
    }
    [SerializeField]
    private Current _currentObj;
    public Current CurrentObj{
        get { return _currentObj; }

    }

    public float maxVar = 0.2f;
    public float var = 0.1f;
    // Doing stuff with cane


    // Use this for initialization
    void Start () {
        _currentObj = null;
        _isOnField = false;
	}

    public void SetField(Current ob) {
        if (ob != null){
            _isOnField = true;
            _currentObj = ob;
        }
    }
    public void ResetField()
    {
        if (_currentObj != null){
            _isOnField = false;
            _currentObj = null;

        }
    }

 

    bool flagMove = false;

    // Update is called once per frame
    void Update () {
        if (IsOnField) {            
            transform.position += CurrentObj.Direction* CurrentObj.Step * Time.deltaTime;

            if (transform.position.y <= 0.3f) flagMove = true;
            else if(transform.position.y >= maxVar) flagMove = false;

            Vector3 pos = transform.position;
            if (flagMove) pos.y += var*Time.deltaTime;
            else pos.y -= var*Time.deltaTime;
            transform.position = pos;
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(LayerMask.LayerToName(other.GetComponent<Collider>().gameObject.layer));
        if (other.tag == "current" && CurrentObj == null &&
            GameManager.Instance.Player.GetComponent<PlayerController>().Draggable == null){
            SetField(other.GetComponent<Collider>().gameObject.GetComponent<Current>());
            GetComponent<Rigidbody>().drag = 1000;
        }
        else if(other.tag == "current-obstacle" && CurrentObj != null){
            CurrentObj.SwapDirection();
            ResetField();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "current"){
            GetComponent<Rigidbody>().drag = 1;
            GetComponent<Rigidbody>().AddForce(CurrentObj.Direction*10);
            
        ResetField();

        }
    }
}
