using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : Enemy
{
    public GameObject minionPrefab;
    public int currentMinions = 1;
    public float continuousMinionSpawnInterval = 5f;


    public void Start()
    {
        base.Start();
        LevelManager.instance.EnableBosslevelText(true);
        StartCoroutine(SpawnMinions());
    }

    IEnumerator SpawnMinions()
    {
           
       for (int i = 0; i < currentMinions; i++)
       {
            //float angle = i * (360f / currentMinions);
            //Vector3 spawnPosition = transform.position + Quaternion.Euler(0, angle, 0) * transform.forward * 3f;
            var enemy = Instantiate(minionPrefab, SpawnManager.GenerateSpawnPosition(), Quaternion.identity);
            SpawnManager.instance.Enemies.Add(enemy);
       }
       currentMinions += 1;
       yield return new WaitForSeconds(continuousMinionSpawnInterval);
       StartCoroutine(SpawnMinions());
    }

    private void OnDestroy()
    {
        StopCoroutine(SpawnMinions());
        LevelManager.instance.EnableBosslevelText(false);

    }

}