using UnityEngine;
using UnityEngine.InputSystem;
public class MoveToMouse : MonoBehaviour
{
    [Range(0, 1f)]
    public float smoothTime;
    Vector2 velocity;
    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), ref velocity, smoothTime);
    }
}
