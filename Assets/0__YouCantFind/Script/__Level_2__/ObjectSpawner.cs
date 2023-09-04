using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnInterval = 2f;
    public float spawnHeight = 10f;
    public float minHorizontalSpeed = -5f;
    public float maxHorizontalSpeed = 5f;

    private float spawnTimer = 0f;

    private void Update()
    {
        // 일정 간격으로 물체 생성
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0f;
        }
    }

    private void SpawnObject()
    {
        // 물체 생성
        GameObject spawnedObject = Instantiate(objectPrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // 초기 속도 설정
        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 initialVelocity = new Vector3(Random.Range(minHorizontalSpeed, maxHorizontalSpeed), 0f, 0f);
            rb.velocity = initialVelocity;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // 랜덤한 초기 위치 생성
        float x = Random.Range(-10f, 10f);
        Vector3 spawnPosition = new Vector3(x, spawnHeight, x);
        return spawnPosition;
    }
}

