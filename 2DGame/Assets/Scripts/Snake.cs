using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public bool isBot;
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
    public int startBodyCount;
    private Transform _lastNode;
    private List<SnakeBody> _bodyParts;
    private Vector2 _mousePos;
    private int _currentOrder;
    private bool _isDead;
    void Awake()
    {
        Init();
        Reset();
    }   

    private void Update()
    {
        HeadFollowMouse();
        SpeedUp();
        MoveForward();
        CheatKey();
        CheckLife();
    }

    public void Init()
    {
        _bodyParts = new List<SnakeBody>();
        LinkHeadEvent();
    }

    public void Reset()
    {
        ResetParameters();
        ResetPosition();
        ClearSnakeBody();
        AddNewSnakeBodies(startBodyCount);
        SetDefaultSize();
        UpdateSize();
        _isDead = false;
    }

    public void AssignData(SnakeData data)
    {
        this.defaultSpeed = data.defaultSpeed;
        this.extraSpeed = data.extraSpeed;
        this.turnSpeed = data.turnSpeed;
        this.defaultSize = data.defaultSize;
        this.growthSize = data.growthSize;
        this.minBodyCount = data.bodyMinCount;
        this.startBodyCount = data.bodyStartCount;
        this.bodyFollowTime = data.bodyFollowTime;
        this.bodyFollowGrowth = data.bodyFollowGrowth;
    }

    public void ResetParameters()
    {
        currentSpeed = defaultSpeed;
        _lastNode = head.transform;
        _currentOrder = -1;
    }

    public void ResetPosition()
    {
        head.transform.position =  Vector3.zero;
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
        _bodyParts.Clear();
    }

    public void UpdateCameraZoom()
    { 
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
        _isDead = true;
    }

    private void CheckLife()
    {
        if (_isDead) Reset();
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
