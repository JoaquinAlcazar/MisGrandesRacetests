using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseBehaviour : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // Escoger una direcci�n aleatoria normalizada
        moveDirection = Random.insideUnitCircle.normalized;
    }

    void FixedUpdate()
    {
        // Aplicar movimiento constante
        rb.velocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reproducir sonido de colisi�n
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Rebote simple: reflejar la direcci�n del movimiento
        Debug.Log("Colisi�n con: " + collision.gameObject.name);

        Vector2 normal = collision.contacts[0].normal;
        moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
    }
}
