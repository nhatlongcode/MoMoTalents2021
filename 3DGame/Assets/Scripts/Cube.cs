using UnityEngine;

public class Cube : MonoBehaviour 
{
    private float _speed;
    public void Init()
    {
       // this.gameObject.SetActive(false);
    }
    public void Spawn(float speed)
    {
        this.gameObject.SetActive(true);
        _speed = speed;
    }    

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Update() 
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.back * 10, _speed);
    }
}