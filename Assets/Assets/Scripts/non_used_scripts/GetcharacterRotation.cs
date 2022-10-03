using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetcharacterRotation : MonoBehaviour
{
    [SerializeField] private Transform targetperso;
    private Transform m_configurableJoint;
    Vector3 TargetInitialPos;
    //GET CHARACTER POSITION
    void Start()
    {
        m_configurableJoint = this.GetComponent<Transform>();
        TargetInitialPos = targetperso.transform.localPosition;
    }
    private void FixedUpdate()
    {

        m_configurableJoint.localPosition = copyPos();
    }
    private Vector3 copyPos()
    {
        return new Vector3(targetperso.localPosition.x, targetperso.localPosition.y, targetperso.localPosition.z) ;
    }

}
