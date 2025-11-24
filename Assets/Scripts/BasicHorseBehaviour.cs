using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;


public class BasicHorseBehaviour : MonoBehaviour
{

    public float speed = 3f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    
    private string characterID;
    public string CharacterID => characterID;

    private string jsonPath;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        moveDirection = Random.insideUnitCircle.normalized;

        characterID = gameObject.name.Replace("(Clone)", "").Trim(); // Limpia "(Clone)"

    }


    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioSource != null)
            audioSource.Play();

        Vector2 normal = collision.contacts[0].normal;
        moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
    }
}
