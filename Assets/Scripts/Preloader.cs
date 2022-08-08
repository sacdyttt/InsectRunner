using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Tabtale.TTPlugins;

public class Preloader : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI m_Text;

    private void Awake()
    {
       TTPCore.Setup();
    }
    void Start()
    {
        
        StartCoroutine(LoadScene());
    }

   

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            int iprogress = Mathf.RoundToInt(progress);
            m_Text.text = "Loading: " + iprogress * 100 + "%";
            //print(progress);
            // Check if the load has finished
            if (progress >= 0.9f)
            {
                  asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
