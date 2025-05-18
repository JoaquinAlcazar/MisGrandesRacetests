using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;

public class CharacterSpawner : MonoBehaviour
{
    public List<GameObject> charactersToSpawn; // Lista completa desde Inspector
    public float spawnRadius = 2f;
    public Vector3 spawnScale = Vector3.one; // << Escala editable desde el Inspector

    private string jsonPath;

    private void Start()
    {
        jsonPath = Path.Combine(Application.persistentDataPath, "characterData.json");

        List<GameObject> selectedCharacters = SelectCharacters();
        Shuffle(selectedCharacters);
        SpawnCharacters(selectedCharacters);
        Debug.Log("Datos en: " + jsonPath);
    }

    List<GameObject> SelectCharacters()
    {
        Dictionary<string, int> partidasPorNombre = new Dictionary<string, int>();

        CharacterStatsDatabase db = new CharacterStatsDatabase();
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            db = JsonUtility.FromJson<CharacterStatsDatabase>(json);
        }

        foreach (var go in charactersToSpawn)
        {
            string name = go.name;
            var stats = db.characters.Find(c => c.characterName == name);
            partidasPorNombre[name] = stats != null ? stats.gamesPlayed : 0;
        }

        int minGames = partidasPorNombre.Values.Min();

        List<GameObject> candidatos = charactersToSpawn
            .Where(go => partidasPorNombre[go.name] == minGames)
            .ToList();

        if (candidatos.Count > 6)
        {
            Shuffle(candidatos);
            candidatos = candidatos.Take(6).ToList();
        }
        else if (candidatos.Count < 6)
        {
            var adicionales = charactersToSpawn
                .OrderBy(go => partidasPorNombre[go.name])
                .Where(go => !candidatos.Contains(go))
                .Take(6 - candidatos.Count);
            candidatos.AddRange(adicionales);
        }

        foreach (var go in candidatos)
        {
            var stats = db.characters.Find(c => c.characterName == go.name);
            if (stats == null)
            {
                db.characters.Add(new CharacterStats
                {
                    characterName = go.name,
                    gamesPlayed = 1,
                    wins = 0,
                    losses = 0
                });
            }
            else
            {
                stats.gamesPlayed += 1;
            }
        }

        string updatedJson = JsonUtility.ToJson(db, true);
        File.WriteAllText(jsonPath, updatedJson);

        return candidatos;
    }

    void SpawnCharacters(List<GameObject> selected)
{
    int count = selected.Count;
    float angleStep = 360f / count;

    for (int i = 0; i < count; i++)
    {
        float angle = i * angleStep * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
        Vector3 spawnPosition = transform.position + (Vector3)offset;

        GameObject spawned = Instantiate(selected[i], spawnPosition, Quaternion.identity);
        spawned.name = selected[i].name; // <- Esto elimina el "(Clone)"
            Vector3 currentScale = spawned.transform.localScale;
            spawned.transform.localScale = new Vector3(
                currentScale.x * spawnScale.x,
                currentScale.y * spawnScale.y,
                currentScale.z * spawnScale.z
            );


        }
    }

    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            GameObject temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
