// source: https://www.linkedin.com/pulse/grabbing-screenshots-ar-foundation-simon-jackson/
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Screenshot : MonoBehaviour
{
    public TMP_Text hideButton;
    // Grab the camera's view when this variable is true.
    private bool grabScreenshot, hidden;
    // Cache variable for our unlit shader
    private Shader unlitTexture;
    [SerializeField]
    [Tooltip("Assign the camera that is taking the screenshot")]
    private CameraRenderEvent cam;
    private List<GameObject> screenshotList;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            // Not the most ideal search, Cameras should be tagged for search, or referenced.
            cam = GameObject.FindObjectOfType<CameraRenderEvent>();
        }
        if (cam != null)
        {
            //Subscribe to the Render event from the camera
            cam.OnPostRenderEvent += OnPostRender;
        }
        // cache a reference to the Unlit shader
        unlitTexture = Shader.Find("Unlit/Texture");
        screenshotList = new List<GameObject>();
        hidden = false;
    }

    public void TakeScreenshot()
    {
        Debug.Log("klickn grabbing Screenshot");
        grabScreenshot = true;
    }

    public void HideShowScreenshot()
    {
        Debug.Log("klick hideshow Screenshot");
        if (!hidden)
        {
            hidden = true;
            hideImages();
            hideButton.SetText("Show");
        }
        else
        {
            hidden = false;
            showImages();
            hideButton.SetText("Hide");
        }  
    }

    public void DeleteScreenshots()
    {
        foreach (GameObject obj in screenshotList)
        {
            Destroy(obj);
        }
        screenshotList = new List<GameObject>();
    }

    private void OnPostRender()
    {
        if (grabScreenshot)
        {
            Debug.Log("grabbing Screenshot");
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
            var spawnedObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
            spawnedObject.transform.SetParent(cam.transform.parent);
            spawnedObject.transform.LookAt(spawnedObject.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
            // Set a position Forward for the camera view
            Vector3 pos = cam.transform.position + cam.transform.forward * 0.75f;
            //Get the current scale of the Quad
            Vector3 scale = spawnedObject.transform.localScale;
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
            spawnedObject.transform.localScale = scale;
            spawnedObject.transform.position = pos;
            // Apply the grabbed screenshot texture to the Quad's material
            var renderer = spawnedObject.GetComponent<MeshRenderer>();
            renderer.material.shader = unlitTexture;
            renderer.material.mainTexture = screenShot;
            if (hidden) spawnedObject.SetActive(false);
            screenshotList.Add(spawnedObject);
            //Stop grabbing a screenshot
            grabScreenshot = false;
        }
    }

    private void hideImages() 
    {
        foreach (GameObject image in screenshotList)
        {
            image.SetActive(false);
        }
    }

    private void showImages() 
    {
        foreach (GameObject image in screenshotList)
        {
            image.SetActive(true);
        }
    }
}