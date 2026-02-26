using UnityEngine;
using UnityEngine.Pool;

public class TrashSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TrashItem trashPrefab;
    [SerializeField] private float spawnInterval = 2f;
    //[SerializeField] private Vector2 spawnAreaMin;
    //[SerializeField] private Vector2 spawnAreaMax;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Sensor Settings")] 
    [SerializeField] private float sensorRadius = 0.5f;
    [SerializeField] private LayerMask trashLayer;
    [SerializeField] private int maxSpawnAttempts = 10;
    
    private IObjectPool<TrashItem> trashPool;
    private float nextSpawnTime = 0;

    private void Awake()
    {
        trashPool = new ObjectPool<TrashItem>(
            createFunc: CreateTrash,
            actionOnGet: TakePool,
            actionOnRelease: ReturnPool,
            actionOnDestroy: DestroyTrash,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 50);
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnTrash();
            nextSpawnTime = Time.time + spawnInterval;
            Debug.Log(nextSpawnTime);
        }
    }

    private void SpawnTrash()
    {
        Vector3 finalpos = Vector3.zero;
        bool emptyPos = false;
        
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points specified");
            return;
        }

        for (int i = 0; i < maxSpawnAttempts; ++i)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPos = spawnPoints[randomIndex].position;
            Collider2D hit = Physics2D.OverlapCircle(spawnPos, sensorRadius, trashLayer);
            if (hit == null)
            {
                finalpos = spawnPos;
                emptyPos = true;
                break;
            }
        }

        if (!emptyPos)
        {
            Debug.Log("Full");
            return;
        }
        TrashItem trash = trashPool.Get();
        //float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        //float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        trash.transform.position = finalpos;
    }

    private TrashItem CreateTrash()
    {
        TrashItem trash = Instantiate(trashPrefab);
        trash.SetPool(trashPool);
        return trash;
    }

    private void TakePool(TrashItem trash)
    {
        trash.gameObject.SetActive(true);
    }

    private void ReturnPool(TrashItem trash)
    {
        trash.gameObject.SetActive(false);
    }

    private void DestroyTrash(TrashItem trash) 
    {
       Destroy(trash.gameObject);
    }

}
