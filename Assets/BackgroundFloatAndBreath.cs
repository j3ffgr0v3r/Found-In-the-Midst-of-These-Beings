using UnityEngine;

public class BackgroundFloatAndBreath : MonoBehaviour
{
    [Header("Drift")]
    [SerializeField] private float xAmplitude = 0.05f;
    [SerializeField] private float yAmplitude = 0.03f;
    [SerializeField] private float xSpeed = 0.02f;
    [SerializeField] private float ySpeed = 0.015f;

    [Header("Breathing Scale")]
    [SerializeField] private float scaleAmplitude = 0.02f;
    [SerializeField] private float scaleSpeed = 0.01f;

    private Vector3 startPosition;
    private Vector3 startScale;

    private void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    private void Update()
    {
        float x = Mathf.Sin(Time.time * xSpeed * Mathf.PI * 2f) * xAmplitude;
        float y = Mathf.Cos(Time.time * ySpeed * Mathf.PI * 2f) * yAmplitude;

        transform.position = startPosition + new Vector3(x, y, 0f);

        float scaleOffset = 1f + Mathf.Sin(Time.time * scaleSpeed * Mathf.PI * 2f) * scaleAmplitude;
        transform.localScale = startScale * scaleOffset;
    }
}