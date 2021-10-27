using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float defaultSpeed;
    public float extraSpeed;
    public float currentSpeed;
    public Transform head;
    public float turnSpeed;
    public float bodySmoothTime;
    public SnakeBody bodyPrefab;
    public int minBodyCount;
    private Transform _lastNode;
    private List<SnakeBody> _bodyParts;
    private Vector2 _mousePos;
    private int _currentOrder;
    void Awake()
    {
        ResetParameters();
        Reset();
    }   

    void Update()
    {
        HeadFollowMouse();
        SpeedUp();
        MoveForward();


        if (Input.GetKeyDown(KeyCode.P))
        {
            AddNewSnakeBody();
        }

    }

    public void Reset()
    {
        ClearSnakeBody();
        AddNewSnakeBodies(minBodyCount);
    }

    public void ResetParameters()
    {
        currentSpeed = defaultSpeed;
        _lastNode = head;
        _currentOrder = -1;
    }

    public void AddNewSnakeBody()
    {
        SnakeBody body = Instantiate(bodyPrefab, _lastNode.position, Quaternion.identity, this.transform);
        body.PreviousNode = _lastNode;
        body.spriteRenderer.sortingOrder = _currentOrder;;
        _currentOrder--;
        _lastNode = body.transform;
        _bodyParts.Add(body);
    }

    public void AddNewSnakeBodies(int amout)
    {
        for (int i=0; i<amout; i++)
        {
            AddNewSnakeBody();
        }
    }

    public void ClearSnakeBody()
    {
        foreach (var body in _bodyParts)
        {
            Destroy(body.gameObject);
        }
    }

    private void MoveForward()
    {
        head.transform.position += head.transform.up * currentSpeed * Time.deltaTime;

        foreach(var body in _bodyParts)
        {
            body.smoothTime = bodySmoothTime;
            body.ProcessUpdate();
        }
    }

    private void HeadFollowMouse()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (_mousePos - (Vector2) head.transform.position).normalized;
        head.up = Vector2.Lerp(head.up, direction, Time.deltaTime * turnSpeed);
    }

    private void SpeedUp()
    {
        if (Input.GetMouseButton(0))
        {
            currentSpeed = defaultSpeed + extraSpeed;
        }
        else currentSpeed = defaultSpeed;
    }



}
