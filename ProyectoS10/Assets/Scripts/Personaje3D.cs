using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje3D : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Animator anim;
    public Transform Eje;

    public bool inGround;
    private RaycastHit hit;
    public float distance;
    public Vector3 v3;

    VidaPlayer playerVida;
    public int cantidad;
    // Start is called before the first frame update
    void Start()
    {
        playerVida = GameObject.FindWithTag("Player").GetComponent<VidaPlayer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arma"))
        {
            playerVida.vida += cantidad;
        }
    }

    void Move()
    {
        Vector3 RotaTargetZ = Eje.transform.forward;
        RotaTargetZ.y = 0;

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RotaTargetZ), 0.3f);
            var dir = transform.forward * speed * Time.deltaTime;
            dir.y = rb.velocity.y;
            rb.velocity = dir;
            anim.SetBool("walk", true);
        }
        else
        {
            if (inGround)
            {
                rb.velocity = Vector3.zero;
            }
            anim.SetBool("walk", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RotaTargetZ * -1), 0.3f);
            var dir = transform.forward * speed * Time.deltaTime;
            dir.y = rb.velocity.y;
            rb.velocity = dir;
            anim.SetBool("walk", true);
        }

        Vector3 RotaTargetX = Eje.transform.right;
        RotaTargetX.y = 0;

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RotaTargetX), 0.3f);
            var dir = transform.forward * speed * Time.deltaTime;
            dir.y = rb.velocity.y;
            rb.velocity = dir;
            anim.SetBool("walk", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RotaTargetX * -1), 0.3f);
            var dir = transform.forward * speed * Time.deltaTime;
            dir.y = rb.velocity.y;
            rb.velocity = dir;
            anim.SetBool("walk", true);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position + v3, transform.up * -1, out hit, distance))
        {
            if (hit.collider.tag == "piso")
            {
                inGround = true;
            }
        }
        else
        {
            inGround = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + v3, Vector3.up * -1 * distance); 
    }
}
