using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    int tempo = 0;
    [SerializeField] GameObject instance;
    [SerializeField] GameObject caps;
    bool temp = false;

    public float MoveSpeed = 18f;
    public float Drag = 0.95f;
    public float MaxSpeed = 8;
    public float SteerAngle = 15;
    public float Traction = 4;


    float rotSpeed = 25;
    Vector3 currentEulerAngles;
    float Zr;

    private Vector3 MoveForce;

    [SerializeField] public Transform bulletSpawnPoint;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject bulletexplode;
    [SerializeField] public float bulletSpeed = 120;

    Rigidbody[] filhos;

    void Start()
    {

    }
    public void Ttimer()
    {
        tempo++;
    }

    private void FixedUpdate()
    {
        //ativação do projétil
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("bom");


            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            var bulletex = Instantiate(bulletexplode, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            filhos = bullet.GetComponentsInChildren<Rigidbody>();
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            // bulletex.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            Destroy(bulletex, 1);

            //tentativa de aplicar aceleracao nos filhos
            foreach (Rigidbody rigid in filhos)
            {
                rigid.velocity = bulletSpawnPoint.forward * bulletSpeed * 2;
            }
        }
    }
    void Update()
    {




        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("VerticalKeys") * Time.deltaTime;
        //transform forward aplica a mudança de posicao somente ao eixo frente e traz, leva em conta
        transform.position += MoveForce * Time.deltaTime;

        float SteerInput = Input.GetAxis("HorizontalKeys");
        transform.Rotate(Vector3.up * SteerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);


        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);
        //essa multiplicaçao toda q ta sendo usada p calcular moveforce só pode ir ate 15
        //metodo define valor maximo permitido pro vetor no cas o valor de maxspeed
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
        //lerp = linear interpolation

        //tentativa de rodar
        if (Input.GetKey(KeyCode.N))
        {
            Zr++;
            // currentEulerAngles += new Vector3(0, Zr, Zr) * rotSpeed;
        }
        else
        {
            // currentEulerAngles += new Vector3(0, 0, 0);
            Zr = 0;
        }

        // currentEulerAngles += new Vector3(0, Yr, 0) * Time.deltaTime * rotSpeed;

        // transform.eulerAngles = currentEulerAngles;

        if (temp == true)
        {
            Ttimer();
        }
        if (tempo > 1)
        {
            // instance.SetActive(false);
            // Debug.Log("time_end");
            temp = false;
            // anim.SetBool("Bool1", true);
            // instance.SetActive(false);
            tempo = 0;
        }
        if (temp == true)
        {
            //particulas de colisao
            Vector3 pos = new Vector3(0, 10, 0);
            Instantiate(caps, pos, Quaternion.identity);
            Instantiate(instance, transform.position, Quaternion.identity);
            // Debug.Log("collide");

        }
    }
    public void OnCollisionEnter(Collision collision)
    {

        instance.SetActive(true);
        temp = true;
    }
}
