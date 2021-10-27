using System;
using UnityEngine;

public class HeadColliderInteract : MonoBehaviour
{
    public Action EAT_FRUIT_EVENT;
    public Action TOUCH_OBS_EVENT;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("obstacle");
            TOUCH_OBS_EVENT?.Invoke();
        }

        if (other.tag == "Fruit")
        {
            Debug.Log("fruit");
            EAT_FRUIT_EVENT?.Invoke();
            Destroy(other.gameObject);
        }
    }
}
