using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex_human : MonoBehaviour
{
    public float displacementAmount;
    SkinnedMeshRenderer meshRender;

    // Start is called before the first frame update
    void Start()
    {
        meshRender = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0.01f, Time.deltaTime);
        meshRender.material.SetFloat("_Amountt", displacementAmount);

       // displacementAmount = 1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            displacementAmount = +2f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            displacementAmount = +15f;
        }
    }
}
