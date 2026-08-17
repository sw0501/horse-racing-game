using System.Collections;
using UnityEngine;

public class PhotoFinishController : MonoBehaviour
{
    public Camera raceCamera;
    public float slowMotionScale = 0.2f;
    public float slowMotionDurationSeconds = 1.5f;
    public float zoomOrthographicSize = 3f;
    public float zoomDurationSeconds = 0.5f;

    private bool isPlaying;

    public void Trigger()
    {
        if (isPlaying) return;
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        isPlaying = true;

        if (raceCamera == null)
        {
            raceCamera = Camera.main;
        }

        float originalOrthographicSize = raceCamera.orthographicSize;

        yield return ZoomTo(zoomOrthographicSize, originalOrthographicSize);

        Time.timeScale = slowMotionScale;
        yield return new WaitForSecondsRealtime(slowMotionDurationSeconds);
        Time.timeScale = 1f;

        yield return ZoomTo(originalOrthographicSize, zoomOrthographicSize);

        isPlaying = false;
    }

    private IEnumerator ZoomTo(float targetSize, float fromSize)
    {
        float elapsed = 0f;
        while (elapsed < zoomDurationSeconds)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / zoomDurationSeconds);
            raceCamera.orthographicSize = Mathf.Lerp(fromSize, targetSize, t);
            yield return null;
        }

        raceCamera.orthographicSize = targetSize;
    }
}
