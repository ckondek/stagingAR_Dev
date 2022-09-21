using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotObject
{
    private CameraRenderEvent cam;
    public GameObject screenshotGroup;
    private GameObject colorPic, grayPic, timerPrefab;
    private Shader unlitTexture;

    public ScreenshotObject(CameraRenderEvent camera, Shader textureShader, GameObject timer)
    {
        cam = camera;
        unlitTexture = textureShader;
        timerPrefab = timer;
        screenshotGroup = new GameObject();
        screenshotGroup.transform.SetParent(cam.transform.parent);
        TakeColorfulScreenshot();
        SpawnTimer();
    }

    private void TakeColorfulScreenshot()
    {
        //Set the screen/image width and height parameters
        int screenShotWidth = Screen.width;
        int screenShotHeight = Screen.height;
        // store in image
        var screenShot = new Texture2D(screenShotWidth, screenShotHeight, TextureFormat.RGB24, false);
        // Use the ‘ReadPixels’ function to grab the pixels from the “Main” camera
        screenShot.ReadPixels(new Rect(0, 0, screenShotWidth, screenShotHeight), 0, 0);
        // ‘Apply()’ then commits these changes to the texture, ready for use
        screenShot.Apply();
        // Spawn a Quad Primitive
        colorPic = GameObject.CreatePrimitive(PrimitiveType.Quad);
        colorPic.transform.SetParent(screenshotGroup.transform);
        colorPic.transform.LookAt(colorPic.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        // Set a position Forward for the camera view
        Vector3 pos = cam.transform.position + cam.transform.forward * 0.75f;
        //Get the current scale of the Quad
        Vector3 scale = colorPic.transform.localScale;
        float screenAspectRatio;
        //Calculate the aspect ratio and set the scale based upon the orientation of the device
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                screenAspectRatio = screenShotWidth / (float)screenShotHeight;
                scale.x *= screenAspectRatio;
                break;
            default:
                screenAspectRatio = screenShotHeight / (float)screenShotWidth;
                scale.y *= screenAspectRatio;
                break;
        }
        //Update the position and scale of the Quad
        colorPic.transform.localScale = scale;
        colorPic.transform.position = pos;
        // Apply the grabbed screenshot texture to the Quad's material
        var renderer = colorPic.GetComponent<MeshRenderer>();
        renderer.material.shader = unlitTexture;
        renderer.material.mainTexture = screenShot;
    }

    private void SpawnTimer()
    {
        GameObject timer = GameObject.Instantiate(timerPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        timer.transform.SetParent(screenshotGroup.transform);
        timer.transform.LookAt(colorPic.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        Vector3 pos = cam.transform.position + cam.transform.forward * 0.7f;
        //Vector3 scale = timer.transform.localScale;
        timer.transform.position = pos;
    }
}
