using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oprimido_animacao : MonoBehaviour
{
    public Collider mainCollider2;
    public GameObject ThisGuysRig2;
    public Animator ThisGuysAnimator2;
    [SerializeField] GameObject rosto;
    void Start()
    {
        GetRagdollBits2();
        RagdollModeOff2();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            RagdollModeOn2();
            Debug.Log("rag");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
      if(collision.gameObject.tag == "balah")
        {
            Debug.Log("collide");
            RagdollModeOn2();
        }
    }
    Collider[] ragDollColliders2;
    Rigidbody[] limbsRigidbodies2;
    void GetRagdollBits2()
    {
        ragDollColliders2 = ThisGuysRig2.GetComponentsInChildren<Collider>();
        limbsRigidbodies2 = ThisGuysRig2.GetComponents<Rigidbody>();
    }
    void RagdollModeOn2()
    {
        ThisGuysAnimator2.enabled = false;

        foreach (Collider col in ragDollColliders2)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rigid in limbsRigidbodies2)
        {
            rigid.isKinematic = false;
        }

        mainCollider2.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    void RagdollModeOff2()
    {
    foreach(Collider col in ragDollColliders2)
        {
            col.enabled = false;
        }

    foreach (Rigidbody rigid in limbsRigidbodies2)
        {
            rigid.isKinematic = true;
        }

        ThisGuysAnimator2.enabled = true;
        mainCollider2.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

}
