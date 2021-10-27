using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float defaultSpeed;
    public float extraSpeed;
    public float currentSpeed;
    public float currentRotaion;
    public float rorationSensivity;
    public Transform head;
    public Transform lastNode;
    public float bodySmoothTime;
    public SnakeBody bodyPrefab;
    public List<SnakeBody> bodyParts;
    private Vector3 movementVelocity;
    void Awake()
    {
        currentSpeed = defaultSpeed;
        lastNode = head;
    }   

    void Update()
    {
        //Debug.Log(Input.mousePosition);
        // mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// direction = Vector3.Slerp(direction, mousePos-(Vector2)transform.position, Time.deltaTime * 1);
		// transform.up = direction;

        // Vector2 deltaMove = (mousePos - (Vector2)this.transform.position).normalized * speed * Time.deltaTime;
        // transform.position += new Vector3(deltaMove.x, deltaMove.y,0);
        if (Input.GetKey(KeyCode.A))
        {
            currentRotaion +=  rorationSensivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentRotaion -=  rorationSensivity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddNewSnakeBody();
        }
        SpeedUp();
        MoveForward();
        //MoveBody();
        Rotation();

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
        //this.transform.position = head.transform.position;

        foreach(var body in bodyParts)
        {
            body.smoothTime = bodySmoothTime;
            body.ProcessUpdate();
        }
    }

    private void Rotation()
    {
        head.transform.rotation = Quaternion.Euler(Vector3.forward*currentRotaion);
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
