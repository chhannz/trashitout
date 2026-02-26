using UnityEngine;
using UnityEngine.Pool;

public class TrashSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]private TrashItem trashPrefab;
    [SerializeField]private float spawnInterval = 2f;
    //[SerializeField] private Vector2 spawnPosMin;
    //[SerializeField] private Vector2 spawnPosMax;
    //spawn location 
    [SerializeField] private Transform[] spawnPoints;

    [Header("Sensor Settings")]
    [SerializeField]private float checkRadius = 0.5f;
    [SerializeField] private LayerMask trashLayer; 
    [SerializeField] private int maxSpawnAttempts = 10;
    
    private IObjectPool<TrashItem> trashPool;
    private float nextSpawnTime = 0f;
    void Awake()
    {
        trashPool = new ObjectPool<TrashItem> (
            createFunc: CreateTrash,
            actionOnGet: TakePool,
            actionOnRelease: ReturnPool,
            actionOnDestroy: DestroyTrash,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 50);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnTrash();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnTrash()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Spawn Points empty !!");
            return;
        }
        
        Vector3 finalSpawnPos = Vector3.zero;
        bool foundEmptyPoint = false;

        for (int i = 0; i < maxSpawnAttempts; ++i)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 testPos = spawnPoints[randomIndex].position;
            
            Collider2D hit = Physics2D.OverlapCircle(testPos, checkRadius, trashLayer);
            if (hit == null)
            {
                finalSpawnPos = testPos;
                foundEmptyPoint = true;
                break;
            }
        }

        if (!foundEmptyPoint)
        {
            Debug.Log("Full");
            return;
        }
        
        TrashItem trash = trashPool.Get();
        trash.transform.position = finalSpawnPos;

        //float x = Random.Range(spawnPosMin.x, spawnPosMax.x);
        //float y = Random.Range(spawnPosMin.y, spawnPosMax.y);
        //trash.transform.position = new Vector2(x, y);
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
