using UnityEngine;

public class Cube : MonoBehaviour 
{
    private float _speed;
    private Vector3 _des;
    public void Init(float speed, Vector3 des)
    {
        _speed = speed;
        _des = des;
    }

    public void MoveProcess() 
    {
        Vector3 pos = this.transform.position;
        pos.z -= _speed * Time.deltaTime;
        this.transform.position = pos;
        //hard code
        if (this.transform.position.z < -5.0f) this.gameObject.SetActive(false);
    }
}