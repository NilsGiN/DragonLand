using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicPerson : MonoBehaviour
{
    public float velocMov = 5f;
    public float velocRot = 200f;
    private Animator anim;
    public float x, y;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(0, 0, y); // Movimiento basado en el eje Vertical
        transform.Translate(movementDirection * Time.deltaTime * velocMov);

        float rotationAngle = x * Time.deltaTime * velocRot; // Rotación basada en el eje Horizontal
        transform.Rotate(0, rotationAngle, 0);

        if (movementDirection == Vector3.zero)
        {
            anim.SetFloat("speed", 0);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("speed", 0.5f);
        }
        else
        {
            anim.SetFloat("speed", 1);
        }
    }
}
