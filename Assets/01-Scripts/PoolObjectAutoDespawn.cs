using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectAutoDespawn : MonoBehaviour, IPoolObject
{
    public float time;
    public void OnSpawn()
    {
        StartCoroutine(DespawnAfterTimeCoroutine(time));
    }

    public void OnDespawn()
    {

    }

    public IEnumerator DespawnAfterTimeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.Instance.Despawn(gameObject);
    }
}
