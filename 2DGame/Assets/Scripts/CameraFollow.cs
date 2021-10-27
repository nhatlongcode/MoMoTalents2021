using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 velocity;
    public float smoothTime;
    private void LateUpdate() 
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, target.transform.position, ref velocity, smoothTime) + offset;
    }
}
