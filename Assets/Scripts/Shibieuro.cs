using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shibieuro : MonoBehaviour
{
    AudioSource audioSource;
    Camera mainCamera;
    AudioSource music;

    private static bool triggered = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        triggered = true;

        HorseBehaviour winner = other.GetComponent<HorseBehaviour>();
        if (winner != null)
        {
            // Aumentar estadísticas del ganador
            winner.victorias++;
            winner.partidasJugadas++;
            winner.SaveStats();
            DontDestroyOnLoad(winner.gameObject);

            // Aumentar derrotas a los demás
            HorseBehaviour[] allHorses = FindObjectsOfType<HorseBehaviour>();
            foreach (HorseBehaviour horse in allHorses)
            {
                if (horse != winner)
                {
                    horse.derrotas++;
                    horse.partidasJugadas++;
                    horse.SaveStats();
                }
            }

            StartCoroutine(PlaySoundAndPause());
            StartCoroutine(MoveAndZoomCamera(winner.transform));
            music.Pause();
        }
    }

    IEnumerator PlaySoundAndPause()
    {
        Time.timeScale = 0f;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(audioSource.clip.length + 1f);
        SceneManager.LoadScene("VictoryScene");
    }

    IEnumerator MoveAndZoomCamera(Transform target)
    {
        float duration = 1f;
        float time = 0f;

        float targetSize = 2f;
        float startSize = mainCamera.orthographicSize;

        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, startPosition.z);

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, time / duration);
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            yield return null;
        }

        mainCamera.orthographicSize = targetSize;
        mainCamera.transform.position = targetPosition;
    }
}
