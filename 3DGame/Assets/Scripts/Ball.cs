using UnityEngine;

public class Ball : MonoBehaviour 
{
    public float maxHeight;
    float timeCounter;
    float distance;
    Vector3 beginPos;
    bool isDrag;

    private void Start() 
    {
        beginPos = transform.position;
        distance =  Vector3.Distance(GameManager.Instance.GetCurrentCubePos(), beginPos);
        timeCounter = 0.0f;
    }

    public void InputProcess()
    {

    }

    public void MoveProcess() 
    {
        timeCounter += Time.deltaTime;
        float speed = GameManager.Instance.speed;
        
        float y = maxHeight * Mathf.Sin(Mathf.PI * speed * timeCounter / distance);
        Vector3 newPos = new Vector3(beginPos.x, beginPos.y + y, -beginPos.z);
        this.transform.position = newPos;
    }   

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Cube")
        {
            Debug.Log("cube");
            timeCounter = 0.0f;
            Vector3 resetPos = new Vector3(transform.position.x, beginPos.y, transform.position.z);
            this.transform.position = resetPos;
            GameManager.Instance.currentNote++;
            distance = Vector3.Distance(GameManager.Instance.GetCurrentCubePos(), beginPos);
        }    
    } 
}