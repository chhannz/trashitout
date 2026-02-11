using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.CompareTag("trash"))
        {
            TrashItem item = obj.GetComponent<TrashItem>();
            if (item != null)
            {
                item.ReturnToPool();
            } else
            {
                Destroy(obj.gameObject);
            }
        }
    }
}
