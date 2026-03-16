using UnityEngine;

public class GentleFloatAndSway : MonoBehaviour
{
    public float floatAmount = 0.06f;
    public float floatSpeed = 0.18f;

    public float swayAmount = 0.5f;
    public float swaySpeed = 0.12f;

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * Mathf.PI * 2f * floatSpeed) * floatAmount;
        float angle = Mathf.Sin(Time.time * Mathf.PI * 2f * swaySpeed) * swayAmount;

        transform.position = startPos + new Vector3(0f, y, 0f);
        transform.rotation = startRot * Quaternion.Euler(0f, 0f, angle);

        float x = Mathf.Sin(Time.time * Mathf.PI * 2f * 0.09f) * 0.025f;
        transform.position = startPos + new Vector3(x, y, 0f);
    }
}