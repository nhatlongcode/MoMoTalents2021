using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Cube cubePrefab;
    public Transform spawnPoint;
    public Transform touchPoint;
    public float spawnTime = 1.0f;
    public float spawnTimeCount;
    public float speed;
    public AudioSource audioSource;
    public AudioClip music;
    public string musicNotePath = "Assets/Resources/BeWithYou_Mondays_chart.bin";
    public int poolAmoumt;
    private Content.BeatData _beatData;
    private int _currentNote;
    private float _timeDelay;
    private Queue<Cube> _cubePool;
    private List<Cube> _listCube;

    private void Awake() {
        Application.targetFrameRate = 60;
        LoadData();
        //InitCubePool();
        spawnTimeCount = 0.0f;
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = music;
        _currentNote = 0;


    }

    private void Start()
    {
        audioSource.Play();
    }

    private void Update() 
    {
    
    }

    public void LoadData()
    {
        _beatData = ReadMusicData(musicNotePath);
        _listCube = new List<Cube>();
        for (int i=0; i< _beatData.noteDatas.Count; i++)
        {
            float s = speed * _beatData.noteDatas[i].timestamp * 60;
            Cube cube = Instantiate(cubePrefab, touchPoint.position + Vector3.forward * s, Quaternion.identity, this.transform);
            cube.SetSpeed(speed);
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

    public void InitCubePool()
    {
        _cubePool = new Queue<Cube>();
        for (int i=0; i<poolAmoumt; i++)
        {
            Cube cube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity, this.transform);
            cube.Init();
            _cubePool.Enqueue(cube);
        }
    }

    public void Spawn()
    {
        Cube cube = _cubePool.Dequeue();
        cube.transform.position = spawnPoint.position;
        cube.Spawn(speed);
        _cubePool.Enqueue(cube);
    }
}
