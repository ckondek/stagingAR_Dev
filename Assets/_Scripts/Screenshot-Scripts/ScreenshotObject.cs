using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotObject
{
    private CameraRenderEvent cam;
    public GameObject screenshotGroup;
    private GameObject colorPic, grayPic, backPic, timerPrefab;
    private Shader unlitTexture;
    private Texture2D screenShot, flippedTexture, grayTexture;
    private int screenShotWidth, screenShotHeight;
    private Material screenshotMaterial, screenshotMaterial2;

    public ScreenshotObject(CameraRenderEvent camera, Shader textureShader, GameObject timer, Material material, Material material2)
    {
        cam = camera;
        unlitTexture = textureShader;
        timerPrefab = timer;
        screenshotMaterial = material;
        screenshotMaterial2 = material2;
        screenshotGroup = new GameObject();
        screenshotGroup.transform.SetParent(cam.transform.parent);
        TakeColorfulScreenshot();
        GrayTextures();
        CreateQuads();
        SpawnTimer();
    }

    private void TakeColorfulScreenshot()
    {
        //Set the screen/image width and height parameters
        screenShotWidth = Screen.width;
        screenShotHeight = Screen.height;
        // store in image
        screenShot = new Texture2D(screenShotWidth, screenShotHeight, TextureFormat.RGB24, false);
        // Use the ‘ReadPixels’ function to grab the pixels from the “Main” camera
        screenShot.ReadPixels(new Rect(0, 0, screenShotWidth, screenShotHeight), 0, 0);
        // ‘Apply()’ then commits these changes to the texture, ready for use
        screenShot.Apply();

        /*
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
        */
    }

    private void SpawnTimer()
    {
        GameObject timer = GameObject.Instantiate(timerPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        timer.transform.SetParent(screenshotGroup.transform);
        timer.transform.LookAt(colorPic.transform.position + cam.transform.rotation * Vector3.forward * (-1.0f), cam.transform.rotation * Vector3.up);
        /*
        float rightOffset = 0.25f;
        float downOffset = 0.45f;
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                rightOffset = screenShotWidth / (float)screenShotHeight / 2.0f - 0.25f;
                break;
            default:
                downOffset = screenShotHeight / (float)screenShotWidth / 2.0f - 0.05f;
                break;
        }
        //float rightOffset = colorPic.transform.localScale.x * (float)screenShotWidth / 2.0f;
        //float downOffset = colorPic.transform.localScale.y * (float)screenShotHeight / 2.0f;
        Vector3 perpDown = new Vector3(0.0f, -cam.transform.forward.z, cam.transform.forward.y);
        Vector3 posOffset = new Vector3(rightOffset, 0.0f, 0.0f);
        Vector3 pos = cam.transform.position + (cam.transform.forward + perpDown * downOffset) * 0.745f - posOffset ;
        */
        Vector3 pos = cam.transform.position + cam.transform.forward * 0.752f;
        timer.transform.position = pos;
    }

    private void GrayTextures()
    {
        //flippedTexture = new Texture2D(screenShot.width, screenShot.height);
        grayTexture = new Texture2D(screenShot.width, screenShot.height);

        int xN = screenShotWidth;
        int yN = screenShotHeight;

        Color bufferColor;
        float bufferGrayscale;
        for (int i = 0; i < xN; i++)
        {
            for (int j = 0; j < yN; j++)
            {
                bufferGrayscale = screenShot.GetPixel(i, j).grayscale;
                bufferColor = new Color(bufferGrayscale, bufferGrayscale, bufferGrayscale);
                //flippedTexture.SetPixel(xN - i - 1, j, bufferColor);
                grayTexture.SetPixel(i, j, bufferColor);
            }
        }
        //flippedTexture.Apply();
        grayTexture.Apply();
    }

    private void CreateQuads()
    {
        // Spawn a Quad Primitive
        colorPic = GameObject.CreatePrimitive(PrimitiveType.Quad);
        grayPic = GameObject.CreatePrimitive(PrimitiveType.Quad);
        //backPic = GameObject.CreatePrimitive(PrimitiveType.Quad);

        colorPic.transform.SetParent(screenshotGroup.transform);
        grayPic.transform.SetParent(screenshotGroup.transform);
        //backPic.transform.SetParent(screenshotGroup.transform);

        colorPic.transform.LookAt(colorPic.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        grayPic.transform.LookAt(grayPic.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        //backPic.transform.LookAt(backPic.transform.position + cam.transform.rotation * Vector3.forward * (-1.0f), cam.transform.rotation * Vector3.up);

        // Set a position Forward for the camera view
        Vector3 pos1 = cam.transform.position + cam.transform.forward * 0.75f;
        Vector3 pos2 = cam.transform.position + cam.transform.forward * 0.751f;
        //Vector3 pos3 = cam.transform.position + cam.transform.forward * 0.752f;

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
        grayPic.transform.localScale = scale;
        //backPic.transform.localScale = scale;

        colorPic.transform.position = pos1;
        grayPic.transform.position = pos2;
        //backPic.transform.position = pos3;

        // Apply the grabbed screenshot texture to the Quad's material
        var renderer1 = colorPic.GetComponent<MeshRenderer>();
        var renderer2 = grayPic.GetComponent<MeshRenderer>();
        //var renderer3 = backPic.GetComponent<MeshRenderer>();

        renderer1.material = screenshotMaterial;
        renderer1.material.mainTexture = screenShot;
        renderer2.material = screenshotMaterial2;
        //renderer2.material.shader = unlitTexture;
        renderer2.material.mainTexture = grayTexture;
        //renderer3.material.shader = unlitTexture;
        //renderer3.material.mainTexture = flippedTexture;
    }
}
