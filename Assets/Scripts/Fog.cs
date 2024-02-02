using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class Fog : MonoBehaviour
{
    private Material fogMaterial;
    [SerializeField] private Shader fogShader;
    [SerializeField] private Color fogColor;

    private readonly float densityScaleFactor = 0.2f;    // for convenience
    [Range(0.0f, 1.0f)][SerializeField] private float fogDensity;
    [Range(0.0f, 100.0f)][SerializeField] private float fogStart;


    void Awake()
    {
        Camera cam = GetComponent<Camera>();
        // to make the camera generate depth textures
        // (meaning a high-precision depth value is calculated for each pixel)
        cam.depthTextureMode = DepthTextureMode.Depth;
    }

    void Start()
    {
        if (fogShader == null) { Debug.LogError("Error: the camera has no fog shader assigned"); }

        // give the camera a material that uses the fog shader
        fogMaterial = new Material(fogShader);
    }

    // Called after the Camera has finished rendering and modifies the
    // Camera's final image. Used to create post-processing effects.
    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        // use the material to set the parameters of the fog shader
        fogMaterial.SetFloat("_FogDensity", fogDensity * densityScaleFactor);
        fogMaterial.SetFloat("_FogStart", fogStart);
        fogMaterial.SetVector("_FogColor", fogColor);
        // `Blit` is often used for post-processing effects
        Graphics.Blit(source, dest, fogMaterial);
    }
}
