using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Security.Cryptography;

public class CaptureImage : MonoBehaviour
{
    public void TakePicture(int maxSize)
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                Material material = quad.GetComponent<Renderer>().material;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                material.mainTexture = texture;

                Destroy(quad, 5f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                Destroy(texture, 5f);
            }
        }, maxSize);

        Debug.Log("Permission result: " + permission);
    }

    //private void Upload()
    //{
    //    string SERVER_ACCESS_KEY = "3df981a384bf4bdb116353a9d5d783ca3b468c35";
    //    string SERVER_SECRET_KEY = "43e0cc31e878e9e062f3219c3d3952bccdcef2bf";
    //    string SIGNATURE;

    //    WWWForm form = new WWWForm();
    //    form.AddField("Authorization", "VWS :");

    //    var md5 = new MD5CryptoServiceProvider();
    //    byte[] bytes = Encoding.UTF8.GetBytes(form);
    //    byte[] hash = md5.ComputeHash(bytes);

    //    string StringToSign = "POST" + "\n" + hash.ToString + "\n" + "form-data" + "\n";
    //    HMACSHA1 hmac = new HMACSHA1(SERVER_SECRET_KEY, StringToSign);

    //    byte[] bytesToEncode = Encoding.UTF8.GetBytes(SERVER_SECRET_KEY);
    //    string encodedBase64 = Convert.ToBase64String(bytesToEncode);

    //    using (UnityWebRequest www = UnityWebRequest.Post("https://vws.vuforia.com/targets", form))
    //    {
    //        yield return www.SendWebRequest();

    //        if (www.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log(www.error);
    //        }
    //        else
    //        {
    //            Debug.Log("Form upload complete!");
    //            //Debug.Log(www.result);
    //        }
    //    }
    //}
}
