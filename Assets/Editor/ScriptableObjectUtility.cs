using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class ScriptableObjectUtility
    {
        public static void CreateAsset<T>() where T : ScriptableObject
        {

            T asset = ScriptableObject.CreateInstance<T>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

            Create(asset, assetPathAndName);
        }
        
        [MenuItem("Assets/Create/This ScriptableObject", false, 1)]
        public static void CreateManager()
        {
            var selected = Selection.activeObject;

            if (IsScriptableObjectScriptSelected(selected))
            {
                ScriptableObject asset = ScriptableObject.CreateInstance(selected.name);
                string path = AssetDatabase.GetAssetPath(Selection.activeObject).Replace(".cs", ".asset");

                Create(asset, path);
            }
        }
        
        private static void Create(Object asset, string path)
        {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
        
        private static bool IsScriptableObjectScriptSelected(Object selected)
        {
            if (selected is MonoScript script)
            {
                var scriptClass = script.GetClass();
                if (!scriptClass.IsSubclassOf(typeof(ScriptableObject)))
                {
                    EditorUtility.DisplayDialog("���!", "������ ������ �� ����������� �� ScriptableObject.", "�����.");
                    return false;
                }
            }
            else
            {
                EditorUtility.DisplayDialog("���!", "��� �������� ������ ��� ��������.", "�����.");
                return false;
            }

            return true;
        }
    }
}