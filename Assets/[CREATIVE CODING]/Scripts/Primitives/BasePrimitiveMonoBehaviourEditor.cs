using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

public static class BasePrimitiveMonoBehaviourEditor <T> where T : BaseBuilder<T>
{
    public static void GenerateBaseFields(T baseBuilder)
    {
        // position and rotation
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Mesh Settings", EditorStyles.boldLabel);

        baseBuilder.SetFrontSided(EditorGUILayout.Toggle("Generate Front Side", baseBuilder.FrontSided));
        baseBuilder.SetBackSided(EditorGUILayout.Toggle("Generate Back Side", baseBuilder.BackSided));

        baseBuilder.SetPivot(EditorGUILayout.Vector3Field("Pivot", baseBuilder.Pivot));
        baseBuilder.SetRotation(EditorGUILayout.Vector3Field("Rotation", baseBuilder.Rotation));

        // UV
        EditorGUILayout.Space();
        baseBuilder.SetGenerateUvMap(EditorGUILayout.Toggle("Generate UV ", baseBuilder.GenerateUvMap));

        EditorGUI.BeginDisabledGroup(!baseBuilder.GenerateUvMap);
        baseBuilder.SetSizeRelative(EditorGUILayout.Toggle("Size Relative", baseBuilder.SizeRelative));

        baseBuilder.SetUvTiling(EditorGUILayout.Vector2Field("UV Tiling", baseBuilder.UvTiling));

        baseBuilder.SetUvOffset(EditorGUILayout.Vector2Field("UV Offset", baseBuilder.UvOffset));
        EditorGUILayout.Space();
        EditorGUI.EndDisabledGroup();
    }
}
