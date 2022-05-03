using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    [SerializeField] Vector3 direction;

    void Update()
    {
        transform.Rotate(direction);
    }
}
