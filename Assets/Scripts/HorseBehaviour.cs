using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class CharacterStats
{
    public string characterName;
    public int gamesPlayed;
    public int wins;
    public int losses;
}

[System.Serializable]
public class CharacterStatsDatabase
{
    public List<CharacterStats> characters = new List<CharacterStats>();
}

public class HorseBehaviour : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    // Estadísticas del personaje
    public int partidasJugadas;
    public int victorias;
    public int derrotas;
    private string characterID;

    private string jsonPath;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        moveDirection = Random.insideUnitCircle.normalized;

        characterID = gameObject.name; // Usa el nombre como identificador
        jsonPath = Path.Combine(Application.persistentDataPath, "characterData.json");

        LoadStats();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (audioSource != null)
            audioSource.Play();

        Debug.Log("Colisión con: " + collision.gameObject.name);

        Vector2 normal = collision.contacts[0].normal;
        moveDirection = Vector2.Reflect(moveDirection, normal).normalized;
    }

    void LoadStats()
    {
        if (!File.Exists(jsonPath)) return;

        string json = File.ReadAllText(jsonPath);
        CharacterStatsDatabase db = JsonUtility.FromJson<CharacterStatsDatabase>(json);

        foreach (var stats in db.characters)
        {
            if (stats.characterName == characterID)
            {
                partidasJugadas = stats.gamesPlayed;
                victorias = stats.wins;
                derrotas = stats.losses;
                break;
            }
        }
    }

    public void SaveStats()
    {
        CharacterStatsDatabase db = new CharacterStatsDatabase();

        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            db = JsonUtility.FromJson<CharacterStatsDatabase>(json);
        }

        bool found = false;
        foreach (var stats in db.characters)
        {
            if (stats.characterName == characterID)
            {
                stats.gamesPlayed = partidasJugadas;
                stats.wins = victorias;
                stats.losses = derrotas;
                found = true;
                break;
            }
        }

        if (!found)
        {
            db.characters.Add(new CharacterStats
            {
                characterName = characterID,
                gamesPlayed = partidasJugadas,
                wins = victorias,
                losses = derrotas
            });
        }

        string updatedJson = JsonUtility.ToJson(db, true);
        File.WriteAllText(jsonPath, updatedJson);
    }
}
