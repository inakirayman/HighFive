using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class GraphQlQuery : MonoBehaviour
{
    [SerializeField] private string endpointUrl = "https://your-graphql-endpoint.com/graphql";
    [SerializeField] private string apiToken = "YOUR_API_TOKEN";
    [SerializeField] private Text resultText;

    IEnumerator Start()
    {
        // Define your GraphQL query
        var query = @"
          query profile {
            profile {
              pupil {
                id
                username {
                  username
                }
                class {
                  title
                  school {
                    title
                  }
                }
                pupilTotal {
                  totalCredits
                  totalRegistrations
                }
              }
            }
          }
        ";

        // Serialize the query into JSON
        JObject requestBodyJson = new JObject();
        requestBodyJson["query"] = query;
        string requestBodyString = requestBodyJson.ToString();

        // Create a new UnityWebRequest object
        var request = new UnityWebRequest(endpointUrl, "POST");

        // Set the request headers
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiToken);

        // Build the request body
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestBodyString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request
        yield return request.SendWebRequest();

        // Handle the response
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            resultText.text = request.downloadHandler.text;
            Debug.Log(request.downloadHandler.text);
        }
    }
}

