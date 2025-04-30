using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public List<GameObject> charactersToSpawn;
    public float spawnRadius = 2f;

    private void Start()
    {
        Shuffle(charactersToSpawn);
        SpawnCharacters();
    }

    void SpawnCharacters()
    {
        int count = charactersToSpawn.Count;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
            Vector3 spawnPosition = transform.position + (Vector3)offset;

            Instantiate(charactersToSpawn[i], spawnPosition, Quaternion.identity);
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
