using UnityEditor;
using UnityEngine;

namespace Catoff
{
    public class CatoffAPIKeyEditor : EditorWindow
    {
        private string apiKey = "";
        private const string ApiKeyPref = "CatoffAPIKey";

        [MenuItem("Catoff SDK/Settings")]
        public static void ShowWindow()
        {
            GetWindow<CatoffAPIKeyEditor>("Catoff SDK Settings");
        }

        private void OnEnable()
        {
            apiKey = EditorPrefs.GetString(ApiKeyPref, "");
        }

        private void OnGUI()
        {
            GUILayout.Label("Catoff API Key", EditorStyles.boldLabel);
            apiKey = EditorGUILayout.TextField("API Key:", apiKey);

            if (GUILayout.Button("Save API Key"))
            {
                EditorPrefs.SetString(ApiKeyPref, apiKey);
                Debug.Log("Catoff API Key saved successfully.");
            }
        }

        public static string GetApiKey()
        {
            return EditorPrefs.GetString(ApiKeyPref, "");
        }
    }
}