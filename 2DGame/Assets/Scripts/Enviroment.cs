using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public float spawnDuration;
    public Vector2 spawnRange;
    public GameObject fruitPrefab;
    private float _time = 0.0f;
    private void Update() 
    {
        _time += Time.deltaTime;
        if (_time >= spawnDuration)
        {
            SpawnFruit();
            _time = 0.0f;
        }
    }

    public void SpawnFruit()
    {
        Vector2 camPos = Camera.main.transform.position;
        Vector2 pos = new Vector2(
            Random.Range(camPos.x - spawnRange.x, camPos.x + spawnRange.x),
            Random.Range(camPos.y - spawnRange.y, camPos.y + spawnRange.y));
        Instantiate(fruitPrefab,pos, Quaternion.identity, this.transform);
    }
}
