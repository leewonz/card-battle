using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformController : MonoBehaviour
{
    public static readonly float slowRate = 0.08f;
    public static readonly float normalRate = 0.15f;

    public CameraTransformHolder transformHolder;
    public Transform target;
    public float rate;

    // Start is called before the first frame update
    void Start()
    {
        SetTransform(CameraTransformHolder.CameraTransformType.Normal, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, rate);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rate);
    }

    public void SetTransform(CameraTransformHolder.CameraTransformType type, int num, bool immediate = false)
    {
        target = transformHolder.GetTransform(type, num);
        if (immediate)
        {
            SetTransformImmediate();
        }
    }

    public void SetTransform(Team team, bool immediate = false)
    {
        target = transformHolder.GetTransform(team);
        if (immediate)
        {
            SetTransformImmediate();
        }
    }

    public void SetTransform(Team team, int num, bool immediate = false)
    {
        target = transformHolder.GetTransform(team, num);
        if (immediate)
        {
            SetTransformImmediate();
        }
    }

    public ICommand GetTransformCommand(CameraTransformHolder.CameraTransformType type, int num)
    {
        Transform targetTransform = transformHolder.GetTransform(type, num);
        return GetTransformCommand(targetTransform);
    }

    public ICommand GetTransformCommand(Team team)
    {
        Transform targetTransform = transformHolder.GetTransform(team);
        return GetTransformCommand(targetTransform);
    }

    public ICommand GetTransformCommand(Team team, int num)
    {
        Transform targetTransform = transformHolder.GetTransform(team, num);
        return GetTransformCommand(targetTransform);
    }

    public ICommand GetTransformCommand(Transform transform)
    {
        return new Command<Transform>(SetTransformAction, transform)
            .SetName("Set Camera");
    }

    public void SetTransformImmediate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    public float SetTransformAction(Transform transform)
    {
        target = transform;
        return 0.0f;
    }
}
