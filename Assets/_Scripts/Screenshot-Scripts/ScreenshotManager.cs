// based on source: https://www.linkedin.com/pulse/grabbing-screenshots-ar-foundation-simon-jackson/
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScreenshotManager : MonoBehaviour
{
    public TMP_Text hideButton;
    public GameObject timer;
    public Material screenshotMaterial, screenshotMaterial2;
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
            ScreenshotObject screenshotObject = new ScreenshotObject(cam, unlitTexture, timer, screenshotMaterial, screenshotMaterial2);


            if (hidden) screenshotObject.screenshotGroup.SetActive(false);
            screenshotList.Add(screenshotObject.screenshotGroup);
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