using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //Hard code for speed
    public SnakeData data;
    public List<SnakeThemeData> skins;
    public GameObject playerSnake;
    public int botAmount;

    public Snake snakePrefab;
    private Snake _currentSnake;

    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else 
        {
            Instance = this;
        }
        InitSnakeBot();
    }

    private void Update() 
    {
        
    }

    public void InitSnakeBot()
    {

    }

    public SnakeThemeData ChooseRandomSkin()
    {
        return skins[Random.Range(0, skins.Count)];
    }
}
