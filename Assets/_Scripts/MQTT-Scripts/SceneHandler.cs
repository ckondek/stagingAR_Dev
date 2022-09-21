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
    
    private Scene mainScene;

    private Scene[] allScenes;
    // Start is called before the first frame update
    void Start()
    {
        mainScene = SceneManager.GetActiveScene();
        //SceneManager.LoadSceneAsync(Scene1, LoadSceneMode.Additive);
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
    
    public void LoadScene4()
    {
        LoadScene(Scene4);
    }
    
    public void LoadScene5()
    {
        LoadScene(Scene5);
    }
    
    public void LoadScene6()
    {
        LoadScene(Scene6);
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
        if (SceneManager.GetActiveScene().name.Equals(sceneName))
        {
            return;
        }
        
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
