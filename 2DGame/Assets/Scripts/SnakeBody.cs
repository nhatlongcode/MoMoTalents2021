using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public Transform PreviousNode;
    public float smoothTime;
    public SpriteRenderer spriteRenderer;
    private Vector3 velocity;


    public void ProcessUpdate()
    {
        Move();
    }

    public void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, PreviousNode.position, ref velocity, smoothTime);
        transform.up = PreviousNode.position - transform.position;
    }
}
