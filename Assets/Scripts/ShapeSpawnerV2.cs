using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawnerV2 : MonoBehaviour
{
    public GameObject[] shapes;
    public Transform player;
    public float spawnDistance = 10f;
    public int maxShapesInView = 4;
    public float minShapeSize = 0.5f;
    public float maxShapeSize = 1.5f;
    public float minShapeDistance = 1f;
    public float maxShapeDistance = 2f;

    private List<GameObject> spawnedShapes = new List<GameObject>();
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        CheckSpawnShapes();
        DespawnShapesAbovePlayer();
        DespawnShapesBelowPlayer();
    }

    void CheckSpawnShapes()
    {
        if (player == null)
            return;

        if (CountShapesInView() < maxShapesInView)
        {
            SpawnShape();
        }
    }

    int CountShapesInView()
    {
        int count = 0;
        foreach (GameObject shape in spawnedShapes)
        {
            if (shape.activeSelf && IsShapeInView(shape))
            {
                count++;
            }
        }
        return count;
    }

    void SpawnShape()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        if (spawnPosition != Vector3.zero)
        {
            Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f));
            GameObject shape = Instantiate(shapes[Random.Range(0, shapes.Length)], spawnPosition, spawnRotation);
            float scale = Random.Range(minShapeSize, maxShapeSize);
            shape.transform.localScale = new Vector3(scale, scale, scale);
            spawnedShapes.Add(shape);
        }
    }

    void DespawnShapesAbovePlayer()
    {
        for (int i = spawnedShapes.Count - 1; i >= 0; i--)
        {
            if (spawnedShapes[i].transform.position.y > player.position.y + spawnDistance)
            {
                Destroy(spawnedShapes[i]);
                spawnedShapes.RemoveAt(i);
            }
        }
    }

    void DespawnShapesBelowPlayer()
    {
        for (int i = spawnedShapes.Count - 1; i >= 0; i--)
        {
            if (spawnedShapes[i].transform.position.y < player.position.y - spawnDistance)
            {
                Destroy(spawnedShapes[i]);
                spawnedShapes.RemoveAt(i);
            }
        }
    }

    bool IsShapeInView(GameObject shape)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(shape.transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 playerPosition = player.position;

        int maxAttempts = 10;
        int currentAttempt = 0;

        while (currentAttempt < maxAttempts)
        {
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float distance = Random.Range(minShapeDistance, maxShapeDistance);

            Vector3 spawnDirection = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
            Vector3 spawnPosition = playerPosition + spawnDirection * distance;
            spawnPosition += spawnDirection * (maxShapeSize / 2);

            bool isValidPosition = true;
            foreach (GameObject shape in spawnedShapes)
            {
                if (Vector3.Distance(spawnPosition, shape.transform.position) < minShapeDistance ||
                    Vector3.Distance(spawnPosition, playerPosition) < minShapeDistance)
                {
                    isValidPosition = false;
                    break;
                }
            }

            if (isValidPosition)
            {
                return spawnPosition;
            }

            currentAttempt++;
        }

        return Vector3.zero;
    }
}
