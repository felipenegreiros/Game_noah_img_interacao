using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] GameObject camera;
    public Vector3 chpos;
    public Vector3 chpos2;
    public Quaternion chrot;
    void Start()
    {   
    }
    void Update()
    {
        chpos = character.transform.position;
        chrot = character.transform.rotation;

        chpos2 = new Vector3(character.transform.position.x, character.transform.position.y+1, character.transform.position.z-2);
        //chpos2 = new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z);

        camera.transform.position = chpos2;
        camera.transform.rotation = chrot;
    }
}
