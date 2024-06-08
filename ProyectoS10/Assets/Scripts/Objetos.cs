using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos : MonoBehaviour
{
    public GameObject[] prefabs; // Array para contener múltiples prefabs

    // Start se llama antes de la primera actualización del frame
    void Start()
    {
        StartCoroutine(DropObjects());
    }

    IEnumerator DropObjects()
    {
        for (int contador = 1; contador <= 20; contador++)
        {
            // Elegir un prefab aleatorio del array
            int indiceAleatorio = Random.Range(0, prefabs.Length);
            GameObject prefabAleatorio = prefabs[indiceAleatorio];

            // Crear una instancia del prefab aleatorio
            GameObject instancia = Instantiate(prefabAleatorio);

            // Establecer un nombre único
            instancia.name = "Primitiva" + contador;

            // Agregar Rigidbody si es necesario
            if (!instancia.GetComponent<Rigidbody>())
            {
                instancia.AddComponent<Rigidbody>();
            }

            // Posicionar y rotar aleatoriamente
            instancia.transform.position = new Vector3(
                Random.Range(-30f, 30f),
                Random.Range(0f, 0.2f),
                Random.Range(-30f, 30f)
            );
            //instancia.transform.Rotate(new Vector3(Random.Range(0f, 360f), 0f, 0f));

            // Esperar un segundo antes de crear el siguiente objeto
            yield return new WaitForSeconds(1f);
        }
    }
}