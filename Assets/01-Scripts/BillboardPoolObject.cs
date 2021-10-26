using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardPoolObject : MonoBehaviour, IPoolObject
{
    public void OnDespawn()
    {
        
    }

    public void OnSpawn()
    {
        transform.localPosition = Vector3.zero;
        if(TryGetComponent<Billboard>(out var billboard))
        {
            billboard.Set();
        }
    }
}
