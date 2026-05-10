using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderHandler : MonoBehaviour
{
    private float NextSceneLoadTime = 1f;
    private void Start() =>
        StartCoroutine(LoadTargetScene());

    private IEnumerator LoadTargetScene()
    {   
        yield return new WaitForSecondsRealtime(NextSceneLoadTime);
        //In Game Scene Pause State Maked a Issuse So Thay why I Use WaitForSecondsRealtime 
        SceneManager.LoadScene(MainSceneLoader.GetTargetScene().ToString());
    }
}
