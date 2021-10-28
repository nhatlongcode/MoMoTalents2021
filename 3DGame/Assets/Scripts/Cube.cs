using UnityEngine;

public class Cube : MonoBehaviour 
{
    public Color touchColor;
    private float _speed;
    private Vector3 _des;
    private Material _material;
    public void Init(float speed, Vector3 des)
    {
        _speed = speed;
        _des = des;
        _material = this.GetComponent<MeshRenderer>().material;
    }

    public void Touch()
    {
        _material.color = touchColor;
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