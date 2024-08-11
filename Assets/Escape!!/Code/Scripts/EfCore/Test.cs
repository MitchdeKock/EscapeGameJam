using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efcore : MonoBehaviour
{
    [SerializeField] private FloatReference souls;

    public delegate void AddSoul();
    public event OnAddSoul()

    void OnAddSoul()
    {
        souls.Value += 1;

    }
}
