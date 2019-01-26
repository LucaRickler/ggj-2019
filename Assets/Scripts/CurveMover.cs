using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class CurveMover : MonoBehaviour {

    [SerializeField]
    private GameObject load;

    public bool IsCharged {
        get {
            return load != null && !animator.GetBool("Moving");
        }
    }

    public Transform Cursor;

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public virtual void Move() {
        if (IsCharged) {
            StartCoroutine(MoveCicle());
        }
    }

    protected IEnumerator MoveCicle() {
        animator.SetTrigger("Move");
        yield return new WaitUntil(()=>!animator.GetBool("Moving"));
        Empty();
    }

    public void Charge(GameObject content) {
        load = content;
        load.transform.parent = Cursor;
    }

    public void Empty() {
        load.transform.parent = null;
        load = null;
    }
    

}