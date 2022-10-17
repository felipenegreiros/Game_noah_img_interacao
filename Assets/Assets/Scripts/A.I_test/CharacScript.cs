using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacScript : MonoBehaviour
{
    [SerializeField] Animator anim;
    private CharacterController _charController;

    public float inX;
    public float inZ;
    private Vector3 vmovement;
    private Vector3 vvelocity;
    public float moveSpeed;
    private float gravidade;
    private int tempo;
    bool kick;

    [SerializeField] private float forceMagnitude;

    //ragdollvaribles
    [SerializeField] public MeshCollider Maincollider;
    [SerializeField] public Collider pecollider;
    //anim
    [SerializeField] GameObject ThisGuyrig;
    [SerializeField] GameObject bala;
    [SerializeField] GameObject Sword;
    [SerializeField] GameObject ps;

    public Vector3 characPosition;
    public Quaternion characRotation;
    // Start is called before the first frame update
    void Start()
    {
        Getragdoolbits();
        RagdollOff();

        GameObject Playeri = GameObject.FindGameObjectWithTag("Player");
        _charController = Playeri.GetComponent<CharacterController>();
        anim = Playeri.GetComponent<Animator>();


        moveSpeed = 4f;
        gravidade = 0.5f;

        anim.SetBool("kick", false);
        anim.SetBool("kick2", true);
    }

    Rigidbody[] ragrigid;
    Collider[] ragcollider;
    void Getragdoolbits()
    {
        ragrigid = ThisGuyrig.GetComponentsInChildren<Rigidbody>();
        ragcollider = ThisGuyrig.GetComponentsInChildren<Collider>();
    }
    void RagdollOff()
    {
        foreach (Collider col in ragcollider)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rag in ragrigid)
        {
            rag.isKinematic = true;
        }

       // pecollider.enabled = true;
        anim.enabled = true;
        Maincollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
    void RagdollOn()
    {
        anim.enabled = false;

        foreach (Collider col in ragcollider)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rag in ragrigid)
        {
            rag.isKinematic = false;
        }

        Maincollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
       private void OnCollisionEnter(Collision collision)
    {
        //characPosition = transform.position;
        // characRotation = transform.rotation;
        if (collision.gameObject.name == "Rosto_AI")
        {
            RagdollOn();
            Instantiate(ps, characPosition, characRotation);
            Debug.Log("pow");
        }
        if (collision.gameObject.tag == "balah")
        {
            RagdollOn();
            Instantiate(ps, characPosition, characRotation);
            Debug.Log("pow");
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            RagdollOff();
        }
      //  else
      //  {
      //      RagdollOff();
      //  }

        inX = Input.GetAxis("Horizontal");
        inZ = Input.GetAxis("Vertical");
        

        Maist();
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("kick", true);
            kick = true;
            //Debug.Log("Key");
        }
        else
        {
           // anim.SetBool("kick", false);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            anim.SetBool("kick", false);
            kick = false;
         
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("bool1", true);
            anim.SetBool("bool2", true);
            inX = inX * 100;
           // tempo = 0;
        }
        if (Input.GetKey(KeyCode.M))
        {

            anim.SetBool("run", true);
            anim.SetBool("run2", false);
            inX = inX * 100;

           // Debug.Log("colon");
        }
         if(Input.GetKeyUp(KeyCode.M))
        {
            anim.SetBool("run", false);
            anim.SetBool("run2", true);

        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) 
        {
           // anim.SetBool("run", false);
           // anim.SetBool("run2", true);

            anim.SetBool("bool1", false);
            anim.SetBool("bool2", false);
            tempo = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("lbool", true);
            anim.SetBool("lbool2", false);
        }
        else 
        {
            anim.SetBool("lbool", false);
            anim.SetBool("lbool2", true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("rbool", true);
            anim.SetBool("rbool2", false);
        }
        else
        {
            anim.SetBool("rbool", false);
            anim.SetBool("rbool2", true);  
        }
        if (tempo > 160)
        {
           // anim.SetBool("run", true);
           // anim.SetBool("run2", false);
        }



    }
    private void FixedUpdate()
    {

        vmovement = _charController.transform.forward * inZ;
        
        _charController.transform.Rotate(Vector3.up * inX * (1f * Time.deltaTime));

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.M))
            {
                _charController.Move(vmovement * moveSpeed * Time.deltaTime);
            tempo = 0;

        }

    }
    private void Maist()
    {
        tempo++;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody !=null 
          //  & Input.GetKey(KeyCode.K)//pra so ativar se ele chutar
            
            )
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }

    }

}
