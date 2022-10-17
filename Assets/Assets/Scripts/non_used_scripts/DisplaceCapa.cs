using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaceCapa : MonoBehaviour
{
    public float displacementAmount;
    MeshRenderer meshRender;

    // Start is called before the first frame update
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0.32f, Time.deltaTime);
        meshRender.material.SetFloat("_Amountt", displacementAmount);

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            displacementAmount = +10f;
        }
    }
}
