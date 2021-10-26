using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEffect : MonoBehaviour, IPoolObject
{
    public float jumpSpeed;
    public float randomSpeed;
    public float gravity;
    Vector3 velocity = Vector3.zero;
    public bool moveToOrigin;

    public void OnSpawn()
    {
        if(moveToOrigin)
        {
            transform.localPosition = Vector3.zero;
        }
        
        velocity = Vector3.zero;
        velocity = (Vector3.up * jumpSpeed) + Random.insideUnitSphere * randomSpeed;
    }

    public void OnDespawn()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity;
        velocity.y -= gravity * Time.deltaTime;
    }
}
