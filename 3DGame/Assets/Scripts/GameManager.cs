using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Cube cubePrefab;
    public Vector2 randomCubeRange;
    public Transform touchPoint;
    public float spawnTimeCount;
    public float speed;
    public Ball ball;
    public AudioSource audioSource;
    public AudioClip music;
    public string musicNotePath = "Assets/Resources/BeWithYou_Mondays_chart.bin";
    public int currentNote;
    private Content.BeatData _beatData;
    private List<Cube> _listCube;

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
        LoadData();
        InitAllCube();
        //InitCubePool();
        spawnTimeCount = 0.0f;
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = music;
        currentNote = 0;
    }
    private void Start()
    {
        audioSource.Play();
    }

    private void Update() 
    {
        ball.MoveProcess();
        foreach(var cube in _listCube)
        {
            cube.MoveProcess();
        }
    }

    public void LoadData()
    {
        _beatData = ReadMusicData(musicNotePath);
    }

    public void InitAllCube()
    {
        _listCube = new List<Cube>();
        for (int i=0; i< _beatData.noteDatas.Count; i++)
        {
            float s = speed * _beatData.noteDatas[i].timestamp;
            Vector3 pos = touchPoint.position + Vector3.forward * s;
            pos.x += Random.Range(randomCubeRange.x, randomCubeRange.y);
            Cube cube = Instantiate(cubePrefab, pos, Quaternion.identity, this.transform);
            cube.Init(speed, touchPoint.transform.position);
            _listCube.Add(cube);
        }
    }

    public Content.BeatData ReadMusicData(string path)
    {
        if (!File.Exists(path)) return null;
        BinaryFormatter bf  = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        file.Position = 0;
        Content.BeatData data = (Content.BeatData)bf.Deserialize(file);
        bf.Serialize(file, data);
        file.Close();
        return data;
    }

    public Vector3 GetCurrentCubePos()
    {
        //currentNote++;
        return _listCube[currentNote].transform.position;
    }

}
