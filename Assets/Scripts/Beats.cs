using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Beats : MonoBehaviour
{
    private bool isDestroyed;
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        isDestroyed = true;
        Debug.Log("Destroyed");
    }

    public bool GetIsDestroyed()
    {
        return isDestroyed;
    }
}
