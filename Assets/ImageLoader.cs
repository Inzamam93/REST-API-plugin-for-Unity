using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ImageLoader : MonoBehaviour
{
    //public string url = "https://i.pinimg.com/originals/9e/1d/d6/9e1dd6458c89b03c506b384f537423d9.jpg";
    //public string url = "https://randomuser.me/api/?gender=female";
    public string url = "https://pokeapi.co/api/v2/pokemon/1";
    public Renderer ImageRenderer;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadFromPokeApi());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void processJsonData( string url_)
    {
        JsonData jsonData_ = JsonUtility.FromJson<JsonData>(url_);
        Debug.Log(jsonData_.info);
        
    }

    IEnumerator LoadFromRestEndPoint()
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                //var texture = DownloadHandlerTexture.GetContent(uwr);
                ImageRenderer.material.mainTexture = DownloadHandlerTexture.GetContent(uwr);
                processJsonData(url);
            }
        }
    }

    IEnumerator LoadFromPokeApi()
    {
        UnityWebRequest pokeContentRequest_ = UnityWebRequest.Get(url);
        yield return pokeContentRequest_.SendWebRequest();

        if (pokeContentRequest_.isNetworkError || pokeContentRequest_.isHttpError)
        {
            Debug.LogError(pokeContentRequest_.error);
            yield break;
        }

        JSONNode pokeImageJsonData_ = JSON.Parse(pokeContentRequest_.downloadHandler.text);
        string pokeSpriteURL_ = pokeImageJsonData_["sprites"]["front_default"];

        // Get Pokemon Sprite
        UnityWebRequest pokeSpriteRequest_ = UnityWebRequestTexture.GetTexture(pokeSpriteURL_);

        yield return pokeSpriteRequest_.SendWebRequest();

        if (pokeSpriteRequest_.isNetworkError || pokeSpriteRequest_.isHttpError)
        {
            Debug.LogError(pokeSpriteRequest_.error);
            yield break;
        }

        ImageRenderer.material.mainTexture = DownloadHandlerTexture.GetContent(pokeSpriteRequest_);
    }
}

