using UnityEngine;
using UnityEngine.Pool;

public class TrashSpawner : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private TrashItem trashPrefab;

    [SerializeField] private float spawnInterval = 2f;

    [SerializeField] private Vector2 spawnAreaMin; 
    [SerializeField] private Vector2 spawnAreaMax;

    private IObjectPool<TrashItem> trashPool;
    private float nextSpawnTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTrash()
    {
        
    }
}
