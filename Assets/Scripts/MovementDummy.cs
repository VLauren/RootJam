using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDummy : MonoBehaviour
{
    public static MovementDummy Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}
