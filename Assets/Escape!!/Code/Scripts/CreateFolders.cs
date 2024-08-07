using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;

public class CreateFolders : EditorWindow
{
    private static string projectName = string.Empty;
    [MenuItem("Assets/Create Default Folders")]
    private static void SetUpFolders()
    {
        CreateFolders window = ScriptableObject.CreateInstance<CreateFolders>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 100);
        window.ShowPopup();
    }

    private static void CreateAllFolfers()
    {
        List<string> folders = new List<string>
        {
            "Art,Materials,Models,Textures",
            "Audio,Music,Sound",
            "Code,Scripts,Shaders",
            "Level,Prefabs,Scenes,UI",
            "Documents"
        };

        foreach (string folder in folders)
        {
            string baseFolder = folder.Split(',').First();
            List<string> subFolders = folder.Split(',').ToList();
            subFolders.RemoveAt(0);

            if (!Directory.Exists("Assets/" + baseFolder))
            {
                Directory.CreateDirectory($"Assets/{projectName}/{baseFolder}");
            }

            foreach (string subfolder in subFolders)
            {
                if (!Directory.Exists("Assets/" + subfolder))
                {
                    Directory.CreateDirectory($"Assets/{projectName}/{baseFolder}/{subfolder}");
                }
            }
        }

        if (Directory.Exists("Assets/Scenes"))
        {
            Debug.Log("Moved Scenes");
            Directory.Move("Assets/Scenes", $"Assets/{projectName}/Level/Scenes");
        }

        Debug.Log("Moved CreateFolders");
        Directory.Move("Assets/CreateFolders.cs", $"Assets/{projectName}/Code/Scripts/CreateFolders.cs");

        AssetDatabase.Refresh();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Insert the Project name used as the root folder");
        projectName = EditorGUILayout.TextField("Project Name: ", projectName);
        this.Repaint();
        GUILayout.Space(10);
        if (GUILayout.Button("Generate!"))
        {
            CreateAllFolfers();
            this.Close();
        }
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }
    }
}
