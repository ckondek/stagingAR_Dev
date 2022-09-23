// based on source: https://www.linkedin.com/pulse/grabbing-screenshots-ar-foundation-simon-jackson/
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScreenshotManager : MonoBehaviour
{
    public TMP_Text hideButton;
    public GameObject timer;
    public Material screenshotMaterial, screenshotMaterial2, screenshotMaterial3;
    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    // Grab the camera's view when grabScreenshot variable is true.
    private bool grabScreenshot, hidden;
    // Cache variable for our unlit shader
    private Shader unlitTexture;
    [SerializeField]
    [Tooltip("Assign the camera that is taking the screenshot")]
    private CameraRenderEvent cam;
    private List<ScreenshotObject> screenshotList;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            // Not the most ideal search, Cameras should be tagged for search, or referenced.
            //cam = GameObject.FindObjectOfType<CameraRenderEvent>();
            cam = arCamera.GetComponent<CameraRenderEvent>();

        }
        if (cam != null)
        {
            //Subscribe to the Render event from the camera
            cam.OnPostRenderEvent += OnPostRender;
        }
        // cache a reference to the Unlit shader
        unlitTexture = Shader.Find("Unlit/Texture");
        screenshotList = new List<ScreenshotObject>();
        hidden = false;
    }

    void Update()
    {
        /*
        // do not capture events unless the welcome panel is hidden
        if (welcomePanel.activeSelf)
            return;
        */
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    GameObject selectedShot = hitObject.transform.parent.gameObject;
                    if (selectedShot != null)
                    {
                        ChangeSelectedObject(selectedShot);
                    }
                }
            }
        }
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
        foreach (ScreenshotObject obj in screenshotList)
        {
            Destroy(obj.screenshotGroup);
        }
        screenshotList = new List<ScreenshotObject>();
    }

    private void OnPostRender()
    {
        if (grabScreenshot)
        {
            Debug.Log("grabbing Screenshot");
            ScreenshotObject screenshotObject = new ScreenshotObject(cam, unlitTexture, timer, screenshotMaterial, screenshotMaterial2, screenshotMaterial3);


            if (hidden) screenshotObject.screenshotGroup.SetActive(false);
            screenshotList.Add(screenshotObject);
            //Stop grabbing a screenshot
            grabScreenshot = false;
        }
    }

    private void hideImages()
    {
        foreach (ScreenshotObject image in screenshotList)
        {
            image.screenshotGroup.SetActive(false);
        }
    }

    private void showImages()
    {
        foreach (ScreenshotObject image in screenshotList)
        {
            image.screenshotGroup.SetActive(true);
        }
    }

    void ChangeSelectedObject(GameObject selected)
    {
        foreach (ScreenshotObject current in screenshotList)
        {
            GameObject currentGroup = current.screenshotGroup;
            if (selected != currentGroup)
            {
                current.Unselect();
            }
            else
            {
                current.Select();
            }
        }
    }
}