using UnityEngine;

public class MoveToTransform : MonoBehaviour
{
    public Transform moveTo;
    [Range(0, 1f)]
    public float smoothTime;

    Vector2 velocity;
    private void Update()
    {

        if (moveTo)
        {
            transform.position = Vector2.SmoothDamp(transform.position, moveTo.position, ref velocity, smoothTime);
        }
    }
}
