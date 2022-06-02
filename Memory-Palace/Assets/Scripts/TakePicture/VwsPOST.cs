using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class VwsPOST : MonoBehaviour
{
    string SERVER_ACCESS_KEY = "3df981a384bf4bdb116353a9d5d783ca3b468c35";
    string SERVER_SECRET_KEY = "43e0cc31e878e9e062f3219c3d3952bccdcef2bf";
    string URL = "https://vws.vuforia.com/targets";
    string targetName = "";
    string imageLocation = "";

    public void postTarget ()
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        JSONObj requestBody = new JSONObj();
        Dictionary<string, string> headers = new Dictionary<string,string>();

        string jsonBody = setRequestBody(requestBody);
        form.Add(new MultipartFormDataSection("body", jsonBody));
        setHeaders(ref headers, ref form, jsonBody);
        byte[] byteHeader = Encoding.UTF8.GetBytes(headers.ToString());

        StartCoroutine(Upload(form, headers));
    }

    private string setRequestBody (JSONObj requestBody)
    {
        requestBody.name = "TestVWS";
        requestBody.width = 1.0F;
        requestBody.image = System.Convert.ToBase64String(File.ReadAllBytes("C:/Users/seape/Documents/Uni Work/20220311_113631.png"));
        // requestBody.active_flag = 1;
        // requestBody.application_metadata = "";

        string jsonBody = JsonUtility.ToJson(requestBody);
        // Debug.Log("JSON Body: " + jsonBody);

        return jsonBody;
    }

    private void setHeaders (ref Dictionary<string, string> headers, ref List<IMultipartFormSection> form, string json)
    {
        //Signature = Base64(HMAC-SHA1(server_secret_key, StringToSign));
        //StringToSign = HTTP-Verb + "\n" + 
        // Content - MD5 + "\n" +
        //Content - Type + "\n" +
        //Date + "\n" +
        //Request - Path;

        System.DateTime dt = System.DateTime.Now;
        string dateString = dt.ToUniversalTime().ToString("R");
        // Debug.Log(dateString);
        
        byte[] bytes = Encoding.ASCII.GetBytes(json);

        var md5 = new MD5CryptoServiceProvider();
        byte[] MD5bytes = md5.ComputeHash(bytes);
        string MD5hash = System.Text.Encoding.ASCII.GetString(MD5bytes,0,MD5bytes.Length);

        string httpVerb = "POST";
        string contentType = "application/json";
        string requestPath = URL;

        string stringToSign = httpVerb + "\n" + MD5hash + "\n" + contentType + "\n" + dateString + "\n" + requestPath;

        byte[] toHash = Encoding.ASCII.GetBytes(stringToSign);
        HMACSHA1 hmac = new HMACSHA1(Encoding.ASCII.GetBytes(SERVER_SECRET_KEY));
        byte[] hashValue = hmac.ComputeHash(toHash);

        string SIGNATURE = System.Convert.ToBase64String(hashValue);
        // Debug.Log("Signature: " + SIGNATURE);

        headers.Add("Date", dateString);
        
        headers.Add("Authorization", "VWS " + SERVER_ACCESS_KEY + ":" + SIGNATURE);
        headers["Content-Type"] = contentType;
        
        // form.Add(new MultipartFormDataSection("Authorization", "VWS " + SERVER_ACCESS_KEY + ":" + SIGNATURE));
        // form.Add(new MultipartFormDataSection("Content-Type", contentType));

        // Debug.Log("String to sign: " + stringToSign);
        // Debug.Log("Headers: " + headers);
    }

    private string getHash (byte[] toHash)
    {
        var md5 = new MD5CryptoServiceProvider();
        byte[] hash = md5.ComputeHash(toHash);
        // Debug.Log("MD5 Hash: " + hash.ToString());

        return hash.ToString();
    }
     
    IEnumerator Upload(List<IMultipartFormSection> form,  Dictionary<string, string> headers)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            foreach (KeyValuePair<string,string> header in headers) {
                Debug.Log(header.Key + " " + header.Value);
                www.SetRequestHeader(header.Key, header.Value);
            }

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}

[System.Serializable]
public class JSONObj : MonoBehaviour
{
    public string name;
    public float width;
    public string image;
    public int active_flag;
    public string application_metadata;

}
