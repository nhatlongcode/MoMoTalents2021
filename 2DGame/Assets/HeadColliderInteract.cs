using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColliderInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("obstacle");
        }

        if (other.tag == "Fruit")
        {
            Debug.Log("fruit");
        }
    }
}
