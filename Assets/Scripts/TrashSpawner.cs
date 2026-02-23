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
        TrashItem trash = trashPool.Get();
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        trash.transform.position = new Vector2(x, y);
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
