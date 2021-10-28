using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Hard code for speed
    public SnakeData data;
    public List<SnakeThemeData> skins;
    public CameraFollow cameraFollow;

    public Snake snakePrefab;
    private Snake _currentSnake;

    private void Awake() 
    {
        //_currentSnake = Instantiate(snakePrefab, Vector3.zero, Quaternion.identity);
        //cameraFollow.target = _currentSnake.head.transform;
    }

    private void Update() 
    {
        
    }

    public void InitSnake()
    {

    }

    public SnakeThemeData ChooseRandomSkin()
    {
        return skins[Random.Range(0, skins.Count)];
    }
}
