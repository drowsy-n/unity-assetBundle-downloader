using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;


public class DownloadExample : MonoBehaviour
{
    // 번들 다운 받을 서버의 주소
    private string BundleURL;
    private string assetBundleName;

    void Start()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        // 에셋번들 이름
        assetBundleName = "assetbundletest";
        // 임시 로컬 경로
        BundleURL = "file:///C:/Ass/" + assetBundleName; 

        using (var uwr = UnityWebRequestAssetBundle.GetAssetBundle(BundleURL))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // 번들에서 에셋을 다운로드
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                // 첫번째 파라미터에 에셋이름
                AssetBundleRequest request = bundle.LoadAssetAsync("Player", typeof(GameObject)); 
                yield return request;
                
                // instantiate
                GameObject obj = Instantiate(request.asset) as GameObject;
                // 원하는 위치에 로드
                obj.transform.position = new Vector3(0.0f, 0.0f, 10.0f);
            }
        }

    }
}
