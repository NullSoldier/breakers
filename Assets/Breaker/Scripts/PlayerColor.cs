using UnityEngine;
using System.Collections;

public class ColorState : MonoBehaviour
{
    public void Awake()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();

        renderer.material.color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            1f);
    }
}