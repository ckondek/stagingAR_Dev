using System.Collections;
using System.Collections.Generic;
using M2MqttUnity.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [Scene] 
    public string Scene1;
    
    [Scene] 
    public string Scene2;
    
    [Scene] 
    public string Scene3;

    [Scene] 
    public string Scene4;
    
    [Scene] 
    public string Scene5;
    
    [Scene] 
    public string Scene6;

    [Scene]
    public string Scene7;

    [Scene]
    public string Scene8;

    [Scene]
    public string Scene9;

    private Scene mainScene;

    private Scene[] allScenes;
    // Start is called before the first frame update
    void Start()
    {
        mainScene = SceneManager.GetActiveScene();
        M2MqttUnityStagingAR._mqttEvent.AddListener(MQTT_Load);
        //SceneManager.LoadSceneAsync(Scene1, LoadSceneMode.Additive);
    }

    public void LoadScene1()
    {
        StartCoroutine(LoadScene(Scene1));
    }
    
    public void LoadScene2()
    {
        StartCoroutine(LoadScene(Scene2));
    }
    
    public void LoadScene3()
    {
        StartCoroutine(LoadScene(Scene3));
    }
    
    public void LoadScene4()
    {
        StartCoroutine(LoadScene(Scene4));
    }
    
    public void LoadScene5()
    {
        StartCoroutine(LoadScene(Scene5));
    }
    
    public void LoadScene6()
    {
        StartCoroutine(LoadScene(Scene6));
    }

    public void LoadScene7()
    {
        StartCoroutine(LoadScene(Scene7));
    }

    public void LoadScene8()
    {
        StartCoroutine(LoadScene(Scene8));
    }

    public void LoadScene9()
    {
        StartCoroutine(LoadScene(Scene9));
    }

    public void MQTT_Load(MQTTMsg msg)
    {
        if (msg.topic.Equals("StagingAR/SceneHandler"))
        {
            StartCoroutine(LoadScene(msg.msg));
        }
    }
    
  private IEnumerator LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name.Equals(sceneName))
        {
            yield break;
        }

        if (SceneManager.sceneCount > 0)
        {
            for(int i=1; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (SceneManager.GetSceneByBuildIndex(i).isLoaded)
                {
                    yield return SceneManager.UnloadSceneAsync(i);
                }
            }
        }

        SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
    }
}
