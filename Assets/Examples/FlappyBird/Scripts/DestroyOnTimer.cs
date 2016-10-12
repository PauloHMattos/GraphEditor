using UnityEngine;
using System.Collections;

public class DestroyOnTimer : MonoBehaviour
{
    private float _createTime;
    public float destroyDelay;

    void Start()
    {
        _createTime = Time.time;
    }

    void Update()
    {
        if(Time.time - _createTime > destroyDelay)
        {
            Destroy(gameObject);
        }
    }
}