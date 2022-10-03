using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InacioRagdool : MonoBehaviour
{
    [SerializeField] public MeshCollider mainCollider2;
    [SerializeField] public GameObject thisGuysRig2;
    [SerializeField] public Animator thisguysanimator2;
    void Start()
    {
        GetRagdollBits();
        RagdollModeOff();
    }
    void Update()
    {
       if(Input.GetKey(KeyCode.Alpha1))
        {
            RagdollModeOn();
        }
        else
        {
            RagdollModeOff();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    Collider[] ragdollColliders2;
    Rigidbody[] limbsRigidbodies2;
    void GetRagdollBits()
    {
        ragdollColliders2 = thisGuysRig2.GetComponentsInChildren<Collider>();
        limbsRigidbodies2 = thisGuysRig2.GetComponentsInChildren<Rigidbody>();

    }
    void RagdollModeOn()
    {
        thisguysanimator2.enabled = false;

        foreach (Collider col in ragdollColliders2)
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
    void RagdollModeOff()
    {
     foreach(Collider col in ragdollColliders2)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rigid in limbsRigidbodies2)
        {
            rigid.isKinematic = true;
        }

        thisguysanimator2.enabled = true;
        mainCollider2.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
