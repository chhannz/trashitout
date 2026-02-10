using UnityEngine;
using UnityEngine.Pool; // Wajib

public class TrashSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TrashItem trashPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Vector2 spawnAreaMin; // Pojok kiri bawah area spawn
    [SerializeField] private Vector2 spawnAreaMax; // Pojok kanan atas area spawn

    // Variabel Pool Bawaan Unity
    private IObjectPool<TrashItem> trashPool;
    private float nextSpawnTime;

    private void Awake()
    {
        // Inisialisasi Pool
        trashPool = new ObjectPool<TrashItem>(
            createFunc: CreateTrash,        // Fungsi saat kolam kosong & butuh baru
            actionOnGet: OnTakeFromPool,    // Fungsi saat objek diambil dari kolam
            actionOnRelease: OnReturnToPool,// Fungsi saat objek dikembalikan
            actionOnDestroy: OnDestroyTrash,// Fungsi saat kolam dihancurkan (misal ganti scene)
            collectionCheck: true,          // Cek error (biar gak double return)
            defaultCapacity: 10,            // Kapasitas awal
            maxSize: 50                     // Batas maksimum memori
        );
    }

    private void Update()
    {
        // Timer sederhana untuk spawn
        if (Time.time >= nextSpawnTime)
        {
            SpawnTrash();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnTrash()
    {
        // Minta 1 sampah dari kolam
        // Ini otomatis memanggil createFunc (kalau kosong) atau actionOnGet (kalau ada stok)
        TrashItem trash = trashPool.Get(); 

        // Tentukan posisi acak
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        trash.transform.position = new Vector3(x, y, 0);
    }

    // --- FUNGSI-FUNGSI LOGIKA POOL ---

    private TrashItem CreateTrash()
    {
        // Bikin baru (Instantiate)
        TrashItem trash = Instantiate(trashPrefab);
        // Beri tahu sampah itu kolamnya siapa
        trash.SetPool(trashPool); 
        return trash;
    }

    private void OnTakeFromPool(TrashItem trash)
    {
        trash.gameObject.SetActive(true); // Nyalakan
    }

    private void OnReturnToPool(TrashItem trash)
    {
        trash.gameObject.SetActive(false); // Matikan (sembunyikan)
    }

    private void OnDestroyTrash(TrashItem trash)
    {
        Destroy(trash.gameObject); // Hancurkan beneran kalau game tutup
    }
}