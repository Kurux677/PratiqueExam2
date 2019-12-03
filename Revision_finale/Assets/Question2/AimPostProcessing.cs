using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class AimPostProcessing : MonoBehaviour
{
    private Material material;
    // Use this for initialization
    void Awake()
    {
        material = new Material(Shader.Find("Custom/ImageEffect"));
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
