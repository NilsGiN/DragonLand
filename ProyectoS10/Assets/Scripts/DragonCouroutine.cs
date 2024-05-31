using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCouroutine : MonoBehaviour
{
    [SerializeField] GameObject     Dragon;
    [SerializeField] Transform[]    puntosControl;
    [SerializeField] float          velocity = 10f;
    [SerializeField] float          rotationSpeed = 220f;

    void Start()
    {
        StartCoroutine(MoveDragon());
    }

    IEnumerator MoveDragon()
    {
        int i = 1;

        while (true)
        {
            Vector3 targetPosition = new Vector3(puntosControl[i].position.x, Dragon.transform.position.y, puntosControl[i].position.z);

            while (Dragon.transform.position != targetPosition)
            {
                // Move the Dragon smoothly towards the target position
                Dragon.transform.position = Vector3.MoveTowards(Dragon.transform.position, targetPosition, velocity * Time.deltaTime);

                // Rotate the Dragon to face the next point, keeping Y rotation constant
                Vector3 lookDirection = puntosControl[i].position - Dragon.transform.position;
                lookDirection.y = 0f; // Constrain Y component to zero               
                if (lookDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    Dragon.transform.rotation = Quaternion.RotateTowards(Dragon.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    yield return null;
                }
                else
                {
                    i = (i + 1) % puntosControl.Length;
                    yield return null; // Pause for a frame before moving to the next point
                }
            }     
        }
    }
}