using UnityEngine;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour 
{
    public float maxHeight;
    public float minDrag;
    public float dragModifier;
    float timeCounter;
    float distance;
    Vector3 beginPos;
    Vector3 startPos;
    Vector3 dist;
    bool isDraging;

    private void Start() 
    {
        beginPos = transform.position;
        distance =  Vector3.Distance(GameManager.Instance.GetCurrentCubePos(), beginPos);
        timeCounter = 0.0f;
    }

    public void InputProcess()
    {

    }

    float startMouseX;

    public void MoveProcess() 
    {
        timeCounter += Time.deltaTime;
        float speed = GameManager.Instance.speed;
        float y = maxHeight * Mathf.Sin(Mathf.PI * speed * timeCounter / distance);
        Vector3 newPos = new Vector3(this.transform.position.x, beginPos.y + y, beginPos.z);

        if (Input.GetMouseButtonDown(0)) 
        {
            startMouseX = Input.mousePosition.x;
            isDraging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
        }

        if (isDraging)
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
            timeCounter = 0.0f;
            Vector3 resetPos = new Vector3(transform.position.x, beginPos.y, transform.position.z);
            this.transform.position = resetPos;
            GameManager.Instance.currentNote++;
            distance = Vector3.Distance(GameManager.Instance.GetCurrentCubePos(), beginPos);
        }   

        if (other.tag == "Plane")
        {
            SceneManager.LoadScene("SampleScene");
        } 
    } 
}