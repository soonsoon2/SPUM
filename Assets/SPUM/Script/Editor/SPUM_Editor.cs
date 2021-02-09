using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(SPUM_Manager))]
[CanEditMultipleObjects]
public class SPUM_Editor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        
        SPUM_Manager SPB = (SPUM_Manager)target;

        bool dirUnitChk = Directory.Exists("Assets/Resources/SPUM/SPUM_Units");
        if(dirUnitChk)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(SPB.unitPath);
            FileInfo[] fileInfo = dirInfo.GetFiles("*.prefab");

            SPB._unitNumber.text = fileInfo.Length + " / 100";
            
        }

        if(SPB._mainBody==null)
        {
            string texPath = "Assets/SPUM/SPUM_Sprites/BodySource/Species/0_Human/Human_1.png";
            Texture2D t = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);
            if(t != null)
            {
                Debug.Log(t);
                SPB._mainBody = (Texture2D)(EditorGUILayout.ObjectField(t,typeof(Texture2D), true));
                SPB.SetBodySprite();
            }
            else
            {
                EditorGUILayout.HelpBox("There is no basic Body Sprite texutre. Please check your SPUM folder",MessageType.Error);
            }
        }
        else
        {
            bool dirChk = Directory.Exists("Assets/Resources/SPUM/SPUM_Sprites/Items");
            if(!dirChk)
            {
                EditorGUILayout.HelpBox("You need to install SPUM Sprite Data by below install buttons",MessageType.Error);
                if (GUILayout.Button("Install Resources Data",GUILayout.Height(50))) 
                {
                    SPB.InstallSpriteData();
                }
            }
            else
            {
                if (GUILayout.Button("Sync BodyData",GUILayout.Height(50))) 
                {
                    SPB.SetBodySprite();
                }

                base.OnInspectorGUI();
                if (GUILayout.Button("Reinstall Resources Data",GUILayout.Height(50))) 
                {
                    SPB.InstallSpriteData();
                }
            }
        }
       
    }
}
