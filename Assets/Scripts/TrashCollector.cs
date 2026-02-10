using System;
using UnityEngine;
using UnityEngine.Events;

public class TrashCollector : MonoBehaviour
{
    public UnityEvent OnTrashCollected;
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.CompareTag("trashbag"))
        {
            OnTrashCollected?.Invoke();
            
            TrashItem item = obj.GetComponent<TrashItem>();
            if (item != null)
            {
                item.ReturnToPool();
            }
            else
            {
                Destroy(obj.gameObject);
            }
        }
    }
}
