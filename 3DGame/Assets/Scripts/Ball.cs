using UnityEngine;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour 
{
    public float maxHeight;
    public float minDrag;
    public float dragModifier;
    private float _timeCounter;
    private float _distance;
    private Vector3 _beginPos;
    private bool _isDraging;

    private void Start() 
    {
        _beginPos = transform.position;
        _distance =  Vector3.Distance(GameManager.Instance.GetCurrentCubePos(), _beginPos);
        _timeCounter = 0.0f;
    }

    public void InputProcess()
    {

    }

    float startMouseX;

    public void MoveProcess() 
    {
        _timeCounter += Time.deltaTime;
        float speed = GameManager.Instance.speed;
        float y = maxHeight * Mathf.Sin(Mathf.PI * speed * _timeCounter / _distance);
        Vector3 newPos = new Vector3(this.transform.position.x, _beginPos.y + y, _beginPos.z);

        if (Input.GetMouseButtonDown(0)) 
        {
            startMouseX = Input.mousePosition.x;
            _isDraging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDraging = false;
        }

        if (_isDraging)
        {
            float x = Input.mousePosition.x;
            if (Mathf.Abs(x - startMouseX) > minDrag)
            {
                newPos.x += (x - startMouseX)/dragModifier;
            }
        }


        this.transform.position = newPos;

    }   


    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Cube")
        {
            other.GetComponent<Cube>().Touch();
            _timeCounter = 0.0f;
            Vector3 resetPos = new Vector3(transform.position.x, _beginPos.y, transform.position.z);
            this.transform.position = resetPos;
            GameManager.Instance.currentNote++;
            _distance = Vector3.Distance(GameManager.Instance.GetCurrentCubePos(), _beginPos);
        }   

        if (other.tag == "Plane")
        {
            SceneManager.LoadScene("SampleScene");
        } 
    } 
}