using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject BossEnemy;
    public GameObject[] powerupPrefabs;

    internal List<GameObject> Enemies = new ();
    internal List<GameObject> PowerUps = new();

    private bool isBossLevel = false;
    private int _waveNumber = 0;
    private float spawnRange = 9.0f;
    private float currentTime;

    public static SpawnManager instance;

    private void Awake()
    {
        if (instance != null) {
            Destroy(instance.gameObject);
        }
        instance = this;

    }

    void Start()
    {
    }

    void Update()
    {


        if (!gameObject)
        {
            return;
        }

        if (Enemies.Count == 0)
        {
            ChangeWave();
            SpawnPowerup();
        }

        if (PowerUps.Count == 0) {

        currentTime += Time.deltaTime;

        if ( currentTime > 5)
        {
            currentTime = 0;
            SpawnPowerup();
        }
        }
    }

    public static Vector3 GenerateSpawnPosition(float yHeight = 0)
    {
        if (instance == null) return Vector3.zero;

        float spawnPosX = UnityEngine.Random.Range(-instance.spawnRange, instance.spawnRange);
        float spawnPosZ = UnityEngine.Random.Range(-instance.spawnRange, instance.spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, yHeight, spawnPosZ);
        return randomPos;
    }

    void ChangeWave()
    {

        if (!GameManager.IsGamePlaying) return;

        _waveNumber++;
        LevelManager.instance.changeLevelText(_waveNumber);
        isBossLevel = (_waveNumber % 3 == 0);

        if (isBossLevel)
        {
            var enemy = Instantiate(BossEnemy, GenerateSpawnPosition(1), Quaternion.identity);
            Enemies.Add(enemy);
            LevelManager.instance.EnableBosslevelText(true);
        }
        else
        {
            for (int i = 0; i < _waveNumber; i++)
            {
                int EFrab = UnityEngine.Random.Range(0, enemyPrefab.Length);
                var enemy = Instantiate(enemyPrefab[EFrab], GenerateSpawnPosition(), Quaternion.identity);
                Enemies.Add(enemy);
            }
        }

    }


    void SpawnPowerup()
    {
        if (!GameManager.IsGamePlaying) return;

        if (powerupPrefabs != null && powerupPrefabs.Length > 0)
        {
            int RPower = UnityEngine.Random.Range(0, powerupPrefabs.Length);
            GameObject newPowerup = Instantiate(powerupPrefabs[RPower], GenerateSpawnPosition(), powerupPrefabs[RPower].transform.rotation);
            PowerUps.Add(newPowerup);
        }
    }


}
