using UnityEngine;
using UnityEngine.Pool;

public class TrashItem : MonoBehaviour
{
    private IObjectPool<TrashItem> _pool;
    
    public void SetPool(IObjectPool<TrashItem> pool)
    {
        _pool = pool;
    }

    public void ReturnToPool()
    {
        if (_pool != null)
        {
            _pool.Release(this);
        } else
        {
            Destroy(gameObject);
        }
    }
}
