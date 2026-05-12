using UnityEngine;
using System.Collections;

// ─────────────────────────────────────────────
//  CoroutineRunner.cs
//  Tiny persistent helper so destroyed objects
//  can still run fade-out coroutines.
//  Place anywhere in your project — one instance
//  is created automatically, no scene setup needed.
// ─────────────────────────────────────────────
public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    private static CoroutineRunner Instance
    {
        get
        {
            if (_instance != null) return _instance;

            var go = new GameObject("_CoroutineRunner");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<CoroutineRunner>();
            return _instance;
        }
    }

    public static void Run(IEnumerator routine)
        => Instance.StartCoroutine(routine);

}