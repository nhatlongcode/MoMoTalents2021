using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float defaultSpeed;
    public float extraSpeed;
    public float currentSpeed;
    public Transform head;
    public float turnSpeed;
    public Transform lastNode;
    public float bodySmoothTime;
    public SnakeBody bodyPrefab;
    public float turnrate;
    public List<SnakeBody> bodyParts;
    private Vector2 mousePos;
    public Vector3 pointInWorld, direction;
    void Awake()
    {
        currentSpeed = defaultSpeed;
        lastNode = head;
    }   

    void Update()
    {
        Debug.Log(mousePos);
        MoveForward();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2) head.transform.position).normalized;
        head.up = Vector2.Lerp(head.up, direction, Time.deltaTime * turnSpeed);


        if (Input.GetKeyDown(KeyCode.P))
        {
            AddNewSnakeBody();
        }
        SpeedUp();

    }

    public void AddNewSnakeBody()
    {
        SnakeBody body = Instantiate(bodyPrefab, lastNode.position, Quaternion.identity, this.transform);
        body.PreviousNode = lastNode;
        lastNode = body.transform;
        bodyParts.Add(body);
    }

    private void MoveForward()
    {
        head.transform.position += head.transform.up * currentSpeed * Time.deltaTime;

        foreach(var body in bodyParts)
        {
            body.smoothTime = bodySmoothTime;
            body.ProcessUpdate();
        }
    }

    private void SpeedUp()
    {
        if (Input.GetMouseButton(0))
        {
            currentSpeed = defaultSpeed + extraSpeed;
        }
        else currentSpeed = defaultSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Fruit")
        {
            Destroy(other.gameObject);
            Debug.Log("collision");
        }    
    }

}
