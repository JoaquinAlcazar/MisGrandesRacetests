using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shibieuro : MonoBehaviour
{
    AudioSource audioSource;
    Camera mainCamera;
    AudioSource music;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(PlaySoundAndPause());
        StartCoroutine(MoveAndZoomCamera());
        music.Pause();
    }

    IEnumerator PlaySoundAndPause()
    {
        Time.timeScale = 0f;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(audioSource.clip.length);
        SceneManager.LoadScene("VictoryScene");
    }

    IEnumerator MoveAndZoomCamera()
    {
        float duration = 1f; 
        float time = 0f;

        float targetSize = 2f; 
        float startSize = mainCamera.orthographicSize;

        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, startPosition.z);

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
