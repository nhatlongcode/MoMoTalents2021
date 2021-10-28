using System;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
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

        if (this.gameObject.tag == "PlayerHead")
        {
            if (other.tag == "BotBody")
            {
                TOUCH_OBS_EVENT?.Invoke();
            }
        }

        if (this.gameObject.tag == "BotHead")
        {
            if (other.tag == "PlayerBody")
            {
                TOUCH_OBS_EVENT?.Invoke();
            }
        }
    }
}
