using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float defaultSpeed;
    public float extraSpeed;
    public float currentSpeed;
    public float defaultSize;
    public float growthSize;
    public float currentSize;
    public HeadColliderInteract head;
    public float turnSpeed;
    public float bodyFollowTime;
    public float bodyFollowGrowth;
    public SnakeBody bodyPrefab;
    public int minBodyCount;
    private Transform _lastNode;
    private List<SnakeBody> _bodyParts;
    private Vector2 _mousePos;
    private int _currentOrder;
    void Awake()
    {
        _bodyParts = new List<SnakeBody>();
        Init();
        Reset();
    }   

    void Update()
    {
        HeadFollowMouse();
        SpeedUp();
        MoveForward();

        CheatKey();
    }

    public void Init()
    {
        LinkHeadEvent();
    }

    public void Reset()
    {
        ResetParameters();
        ClearSnakeBody();
        AddNewSnakeBodies(minBodyCount);
        SetDefaultSize();
        UpdateSize();
    }

    public void ResetParameters()
    {
        currentSpeed = defaultSpeed;
        _lastNode = head.transform;
        _currentOrder = -1;
    }

    public void AddNewSnakeBody()
    {
        SnakeBody body = Instantiate(bodyPrefab, _lastNode.position, Quaternion.identity, this.transform);
        body.PreviousNode = _lastNode;
        body.spriteRenderer.sortingOrder = _currentOrder;;
        body.transform.localScale = Vector3.one * currentSize;
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

    private void LinkHeadEvent()
    {
        head.EAT_FRUIT_EVENT += EatFruit;
        head.TOUCH_OBS_EVENT += TouchObs;
    }

    private void UnlinkHeadEvent()
    {
        head.EAT_FRUIT_EVENT -= EatFruit;
        head.TOUCH_OBS_EVENT -= TouchObs;
    }

    private void EatFruit()
    {
        Growth();
        AddNewSnakeBody();
    }

    private void TouchObs()
    {

    }

    private void MoveForward()
    {
        head.transform.position += head.transform.up * currentSpeed * Time.deltaTime;

        foreach(var body in _bodyParts)
        {
            body.smoothTime = bodyFollowTime;
            body.ProcessUpdate();
        }
    }

    private void UpdateSize()
    {
        //this.transform.localScale = Vector3.one * currentSize;
        head.transform.localScale = Vector3.one * currentSize;
        foreach (var body in _bodyParts)
        {
            body.transform.localScale = Vector3.one * currentSize;
        }
    }

    private void HeadFollowMouse()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (_mousePos - (Vector2) head.transform.position).normalized;
        head.transform.up = Vector2.Lerp(head.transform.up, direction, Time.deltaTime * turnSpeed);
    }

    private void SpeedUp()
    {
        if (Input.GetMouseButton(0))
        {
            currentSpeed = defaultSpeed + extraSpeed;
        }
        else currentSpeed = defaultSpeed;
    }

    private void SetDefaultSize()
    {
        currentSize = defaultSize;
    }

    private void Growth()
    {
        currentSize += growthSize;
        UpdateSize();
    }

    private void CheatKey()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddNewSnakeBody();
        }
    }

}
