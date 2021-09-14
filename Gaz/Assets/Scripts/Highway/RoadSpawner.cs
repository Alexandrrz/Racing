using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RoadSpawner : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject City, Fog;
    public GameObject[] _playerCars = new GameObject[3];
    public GameObject _rccCamera;
    //таймер начала и то что он скрывает

    public Chunk RoadPrefab;
    public Chunk[] WolrdPrefab = new Chunk[2];
    public Chunk[] CarsPrefabs = new Chunk[3];
    public Chunk FirstRoad, FirstWorld, FirstCars;

    private List<Chunk> SpawnedChunks = new List<Chunk>();
    private List<Chunk> WorldSpawnedChunks = new List<Chunk>();
    private List<Chunk> CarsSpawned = new List<Chunk>();

    private void Start()
    {
        SpawnedChunks.Add(FirstRoad);
        WorldSpawnedChunks.Add(FirstWorld);
        CarsSpawned.Add(FirstCars);
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 1;

        if(PlayerPrefs.GetInt("Player_car") == 0)
        {
            _playerCars[0].SetActive(true);
            _playerCars[1].SetActive(false);
            _playerCars[2].SetActive(false);
            _rccCamera.GetComponent<RCC_Camera>().TPSOffsetY = 0.5f;
            PlayerTransform = _playerCars[0].transform;
        }
        if (PlayerPrefs.GetInt("Player_car") == 1)
        {
            _playerCars[0].SetActive(false);
            _playerCars[1].SetActive(true);
            _playerCars[2].SetActive(false);
            _rccCamera.GetComponent<RCC_Camera>().TPSOffsetY = 0.5f;
            PlayerTransform = _playerCars[1].transform;
        }
        if (PlayerPrefs.GetInt("Player_car") == 2)
        {
            _playerCars[0].SetActive(false);
            _playerCars[1].SetActive(false);
            _playerCars[2].SetActive(true);
            _rccCamera.GetComponent<RCC_Camera>().TPSOffsetY = 1.5f;
            PlayerTransform = _playerCars[2].transform;
        }
    }
    private void FixedUpdate()
    {
        //СПАВН ДОРОГИ

        if (PlayerTransform.position.z > SpawnedChunks[SpawnedChunks.Count - 1].Middle.position.z)
        {
            SpawnChunk();
        }

        //СПАВН МИРА
        if (PlayerTransform.position.z > WorldSpawnedChunks[WorldSpawnedChunks.Count - 1].Middle.position.z)
        {
            SpawnWorldChunk();
        }

        //СПАВН МАШИН
        if (PlayerTransform.position.z > CarsSpawned[CarsSpawned.Count - 1].Middle.position.z)
        {
            SpawnCarsChunk();
        }

        

        //ГОРОД ВДАЛИ
        City.transform.position = new Vector3(City.transform.position.x, City.transform.position.y, (PlayerTransform.position.z + 560f));
        Fog.transform.position = new Vector3(Fog.transform.position.x, Fog.transform.position.y, (PlayerTransform.position.z + 160f));
    }
    private void Update()
    {
        //Задаем движение всем машинам
        foreach (var child in CarsSpawned)
        {
            child.transform.Translate(0, 0, 12 * Time.deltaTime);
        }
    }

    private void SpawnChunk()
    {
        Chunk newRoad = Instantiate(RoadPrefab);
        newRoad.transform.position = SpawnedChunks[SpawnedChunks.Count - 1].End.position - newRoad.Begin.localPosition;
        SpawnedChunks.Add(newRoad);

        if (SpawnedChunks.Count > 2)
        {
            Destroy(SpawnedChunks[0].gameObject);
            SpawnedChunks.RemoveAt(0);
        }
    }
    private void SpawnWorldChunk()
    {
        Chunk newWorld = Instantiate(WolrdPrefab[UnityEngine.Random.Range(0, WolrdPrefab.Length)]);
        newWorld.transform.position = WorldSpawnedChunks[WorldSpawnedChunks.Count - 1].End.position - newWorld.Begin.localPosition;
        WorldSpawnedChunks.Add(newWorld);

        if (WorldSpawnedChunks.Count > 2)
        {
            Destroy(WorldSpawnedChunks[0].gameObject);
            WorldSpawnedChunks.RemoveAt(0);
        }
    }
    private void SpawnCarsChunk()
    {
        Chunk newCars = Instantiate(CarsPrefabs[UnityEngine.Random.Range(0, CarsPrefabs.Length)]);
        newCars.transform.position = CarsSpawned[CarsSpawned.Count - 1].End.position - newCars.Begin.localPosition;
        CarsSpawned.Add(newCars);

        if (CarsSpawned.Count > 2)
        {
            Destroy(CarsSpawned[0].gameObject);
            CarsSpawned.RemoveAt(0);
        }
    }
}
