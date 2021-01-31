using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button bt_home;
    public GameObject go_Loading;

    IEnumerator Start()
    {
        bt_home.onClick.AddListener(OnClickHome);
        go_Loading.SetActive(true);

        var url = Path.Combine(Application.streamingAssetsPath, "pack", Goble.moudleName);

        print("[module]" + Goble.moudleName);
        print("[name]" + Goble.userName);

        //StartCoroutine(LoadAssetBundle(url + ".asset"));
        //yield return LoadAssetBundle(url + ".asset");
        yield return new WaitUntil(LoadProcess);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool LoadProcess()
    {
        go_Loading.SetActive(false);
        return true;
    }

    void OnClickHome()
    {
        Application.LoadLevel(0);
    }

    #region Private
    //private IEnumerator Load_gameObject(string url)
    //{
    //    print("[url]" + url);
    //    using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, 0))
    //    {
    //        yield return uwr.SendWebRequest();
    //        if (uwr.error != null)
    //        {
    //            throw new Exception("WWW download error: " + uwr.error);
    //        }
    //        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
    //        GameObject go = bundle.LoadAsset(bundle.name) as GameObject;
    //        Instantiate(go);
    //    }
    //}


    IEnumerator LoadAssetBundle(string url)
    {
        print("[url]" + url);
        var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return request.Send();
        if (request.isNetworkError)
        {
            // TODO: Handle error, request.error
            print("[error]" + request.isNetworkError);
            yield break;
        }
        var assetBundle = DownloadHandlerAssetBundle.GetContent(request);
        GameObject go = assetBundle.LoadAsset(assetBundle.name) as GameObject;
        Instantiate(go);
    }
    #endregion
}
