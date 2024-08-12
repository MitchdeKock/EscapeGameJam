using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HealthBar))]
public class HealthBarUnityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HealthBar healthBar = (HealthBar)target;

        if (GUILayout.Button("Change Health"))
        {
            healthBar.TestHealth();
        }
    }
}
