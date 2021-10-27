using UnityEngine;

[CreateAssetMenu(fileName = "SnakeData", menuName = "2DGame/Data/SnakeData", order = 0)]
public class SnakeData : ScriptableObject 
{
    public string ID;
    [Header("Speed")]
    public float defaultSpeed;
    public float extraSpeed; 
    public float turnSpeed; 
    [Header("Size")]
    public float defaultSize;
    public float growthSize; 
    [Header("Body")]
    public int bodyMinCount;
    public int bodyStartCount;
    public float bodyFollowTime;
    public float bodyFollowGrowth; 
}