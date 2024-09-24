using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private new Transform transform;
    private void Awake()
    {
        transform = base.transform;
    }

    void Update()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(euler.x, Camera.main.transform.rotation.eulerAngles.y, euler.z);
    }
}
