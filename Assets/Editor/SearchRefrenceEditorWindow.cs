using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SearchRefrenceEditorWindow : EditorWindow
{
    /// <summary>
    /// ��������
    /// </summary>
    [MenuItem("Assets/SearchRefrence")]
    static void SearchRefrence()
    {
        SearchRefrenceEditorWindow window = (SearchRefrenceEditorWindow)EditorWindow.GetWindow(typeof(SearchRefrenceEditorWindow), false, "Searching", true);
        window.Show();
    }

    private static Object searchObject;
    private List<Object> result = new List<Object>();
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        searchObject = EditorGUILayout.ObjectField(searchObject, typeof(Object), true, GUILayout.Width(200));
        if (GUILayout.Button("Search", GUILayout.Width(100)))
        {
            result.Clear();

            if (searchObject == null)
                return;

            string assetPath = AssetDatabase.GetAssetPath(searchObject);
            string assetGuid = AssetDatabase.AssetPathToGUID(assetPath);
            //ֻ���prefab
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });

            int length = guids.Length;
            for (int i = 0; i < length; i++)
            {
                string filePath = AssetDatabase.GUIDToAssetPath(guids[i]);
                EditorUtility.DisplayCancelableProgressBar("Checking", filePath, i / length * 1.0f);

                //����Ƿ����guid
                string content = File.ReadAllText(filePath);
                if (content.Contains(assetGuid))
                {
                    Object fileObject = AssetDatabase.LoadAssetAtPath(filePath, typeof(Object));
                    result.Add(fileObject);
                }
            }
            EditorUtility.ClearProgressBar();
        }
        EditorGUILayout.EndHorizontal();

        //��ʾ���
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < result.Count; i++)
        {
            EditorGUILayout.ObjectField(result[i], typeof(Object), true, GUILayout.Width(300));
        }
        EditorGUILayout.EndHorizontal();
    }

}
