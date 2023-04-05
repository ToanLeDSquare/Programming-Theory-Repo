using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyWithDelayTime());
    }

    IEnumerator DestroyWithDelayTime()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
