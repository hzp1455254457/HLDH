using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public static class EditorScreenshotExtension
{
    //ctrl + shift + y ??ͼ
    [MenuItem("Screenshot/Take Screenshot %#y")]
    private static async void Screenshot()
    {
        string folderPath = Directory.GetCurrentDirectory() + "\\Screenshots";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var timestamp = System.DateTime.Now;
        var stampString = string.Format("{0}-{1:00}-{2:00}_{3:00}-{4:00}-{5:00}", timestamp.Year, timestamp.Month, timestamp.Day, timestamp.Hour, timestamp.Minute, timestamp.Second);
        ScreenCapture.CaptureScreenshot(Path.Combine(folderPath, stampString + ".png"));

        Debug.Log("??ͼ??......");
        //?ȴ?5??
        await System.Threading.Tasks.Task.Delay(1000);
        //await Task.
        System.Diagnostics.Process.Start("explorer.exe", folderPath);
        Debug.Log("??ͼ" + stampString + ".png");
    }
}