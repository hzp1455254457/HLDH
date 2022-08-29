using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ChangeFontWindow : EditorWindow
{
    [MenuItem("Tools/��������")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(ChangeFontWindow));
    }

    Font toChange;
    static Font toChangeFont;
    FontStyle toFontStyle;
    static FontStyle toChangeFontStyle;

    void OnGUI()
    {
        toChange = (Font)EditorGUILayout.ObjectField(toChange, typeof(Font), true, GUILayout.MinWidth(100f));
        toChangeFont = toChange;
        toFontStyle = (FontStyle)EditorGUILayout.EnumPopup(toFontStyle, GUILayout.MinWidth(100f));
        toChangeFontStyle = toFontStyle;
        if (GUILayout.Button("����"))
        {
            Change();
        }
    }

    public static void Change()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        if (!canvas)
        {
            Debug.Log("NO Canvas");
            return;
        }
        Transform[] tArray = canvas.GetComponentsInChildren<Transform>();
        for (int i = 0; i < tArray.Length; i++)
        {
            Text t = tArray[i].GetComponent<Text>();
            if (t)
            {
                //��������������  ��������ĺ� �Լ�����޸��³����������״̬ �ڱ���ͺ��� ����Ȼ�������û�� �Ͳ��ᱣ�� ��ʧ����
                Undo.RecordObject(t, t.gameObject.name);
                t.font = toChangeFont;
                t.fontStyle = toChangeFontStyle;
                //ˢ����
                EditorUtility.SetDirty(t);
            }
        }
        Debug.Log("Succed");
    }

}