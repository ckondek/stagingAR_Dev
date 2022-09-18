// source: https://www.linkedin.com/pulse/grabbing-screenshots-ar-foundation-simon-jackson/
// and https://answers.unity.com/questions/1545858/onpostrender-is-not-called.html
using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class CameraRenderEvent : MonoBehaviour
{
    private Camera cam;
    public event Action OnPostRenderEvent;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }
    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }

    private void OnPostRender()
    {
        OnPostRenderEvent?.Invoke();
    }
}