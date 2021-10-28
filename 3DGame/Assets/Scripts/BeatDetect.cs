using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetect : MonoBehaviour
{
    public int bpm;
    public float beatTimer;
    public void Detect()
    {
        float beat = 60.0f / bpm;
        beatTimer += Time.deltaTime;
        if (beatTimer >= beat)
        {
            beatTimer -= beat;
        }
    }
}
