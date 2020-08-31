using System;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

namespace PopovMaks.RAV3_Test
{
    public class GameClientApi
    {
        private readonly IAsyncProcessor _asyncProcessor;
        private readonly IJsonSerializer _json;

        public GameClientApi(IAsyncProcessor asyncProcessor, IJsonSerializer json)
        {
            _asyncProcessor = asyncProcessor;
            _json = json;
        }

        public void UpdateItemStatus(ItemStatusUpdateRequest request, Action onSuccess, Action<string> onError)
        {
            _asyncProcessor.StartRoutine(SendPostRequestRoutine(ProjectConstants.ServerUrl, request, onSuccess, onError));
        }

        private IEnumerator SendPostRequestRoutine<T>(string url, T payload, Action onSuccess, Action<string> onError)
        {
            using (var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
            {
                request.SetRequestHeader(ProjectConstants.AuthKey, ProjectConstants.AuthParam);

                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(_json.Serialize(payload)));
                request.downloadHandler = new DownloadHandlerBuffer();

                yield return request.SendWebRequest();

                if (request.isHttpError || request.isNetworkError)
                {
                    var message = $"Error: {request.error}; {Encoding.UTF8.GetString(request.downloadHandler.data)}";
                    onError?.Invoke(message);
                }
                else
                {
                    onSuccess?.Invoke();
                }
            }
        }
    }
}
