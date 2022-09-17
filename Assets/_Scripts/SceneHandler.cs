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

    private Scene mainScene;

    private Scene[] allScenes;
    // Start is called before the first frame update
    void Start()
    {
        mainScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(Scene1, LoadSceneMode.Additive);
    }

    public void LoadScene1()
    {
        LoadScene(Scene1);
    }
    
    public void LoadScene2()
    {
        LoadScene(Scene2);
    }
    
    public void LoadScene3()
    {
        LoadScene(Scene3);
    }

    public void MQTT_Load(MQTTMsg msg)
    {
        if (msg.topic.Equals("StagingAR/SceneHandler"))
        {
            LoadScene(msg.msg);
        }
    }
    
  private void LoadScene(string sceneName)
    {
        allScenes = SceneManager.GetAllScenes();
        
        foreach (Scene s in allScenes)
        {
            if (s.buildIndex != mainScene.buildIndex)
            {
                SceneManager.UnloadSceneAsync(s);
            }
        }

        SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
    }
}
