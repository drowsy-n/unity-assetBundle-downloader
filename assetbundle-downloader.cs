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
        // 지난 포스팅에서 만들었던 에셋번들 이름
        assetBundleName = "assetbundletest";
        // 저는 임시로 로컬 경로를 넣어주었어요. 이 경로에 생성해놨던 에셋번들 파일을 넣어놓으시면 됩니다.
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
                // 번들에서 에셋을 다운로드하고
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                // 첫번째 파라미에 에셋이름을 넣는데, 저는 Player라는 프리팹으로 테스트를 진행했기때문에 Player라고 적어줬어요 
                AssetBundleRequest request = bundle.LoadAssetAsync("Player", typeof(GameObject)); 
                yield return request;
                
                // instantiate하고
                GameObject obj = Instantiate(request.asset) as GameObject;
                // 원하는 위치에 로드
                obj.transform.position = new Vector3(0.0f, 0.0f, 10.0f);
            }
        }

    }
}