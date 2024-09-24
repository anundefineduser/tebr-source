using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CreateItemScript : EditorWindow
{
    string name = "NewItemScript";

    [MenuItem("Assets/Create/Baldi/Items/New Item Script", false, 2)]
    static void OpenWindow()
    {
        CreateItemScript window = (CreateItemScript)EditorWindow.GetWindow(typeof(CreateItemScript));
        window.Show();
        window.titleContent = new GUIContent("Item Script Creator");
    }

    private void OnGUI()
    {
        name = EditorGUILayout.TextField("Script Name:", name);
        if (GUILayout.Button("Create Script"))
        {
            CreateScript(name);
            this.Close();
        }
    }

    void CreateScript(string _name)
    {
        ProjectWindowUtil.CreateAssetWithContent($"{_name}.cs",
$@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class {_name} : ItemBase
{{
    public override void Pickup()
    {{
        base.Pickup();
    }}

    public override void Use()
    {{
        base.Use();
    }}
}}");
    }
}
