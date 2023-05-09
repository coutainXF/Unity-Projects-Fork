using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    [SerializeField] Transform target;//跟随目标
    [SerializeField] Vector2 minCameraPosition;
    [SerializeField] Vector2 maxCameraPosition;

    Vector3 offset;//与目标的偏移值
    Vector3 origin;//初始位置

    public Vector3 minviewport;
    public Vector3 maxviewport;

    protected override void Awake()
    {
        base.Awake();
        origin = transform.position;
    }

    void Start()
    {
        StartCoroutine(nameof(GetCameraPos));
    }

    void FixedUpdate()
    {
        Vector3 targetPos = target.position;
        targetPos.x = Mathf.Clamp(targetPos.x, minCameraPosition.x, maxCameraPosition.x);
        transform.position = new Vector3(targetPos.x,origin.y,origin.z);
    }

    IEnumerator GetCameraPos()
    {
        while (gameObject.activeSelf)
        {
            minviewport = Camera.main.ViewportToWorldPoint(Vector3.zero); 
            maxviewport = Camera.main.ViewportToWorldPoint(Vector3.one);
            yield return null;
        }
    }
}