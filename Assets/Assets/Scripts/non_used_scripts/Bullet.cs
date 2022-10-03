using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float life = 8;
    [SerializeField] GameObject bala;
    [SerializeField] GameObject balaexp;

    public int timerr;

    public Vector3 pos = new Vector3(0, 0, 0);
    private void Awake()
    {
        Destroy(bala, life);
       // Destroy(balaexp, life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        timerr = 0;

        if (timerr > 100)
        {
            // Destroy(collision.;
            Destroy(bala);
        }
    }
    // Update is called once per frame
    void Update()
    {

        timerr++;

    }
}
