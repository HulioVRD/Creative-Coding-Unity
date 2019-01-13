using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlaneMonoBehaviour))]
public class PlaneMonoBehaviourEditor : Editor
{
    PlaneMonoBehaviour planeMonoBehaviour;
    PlaneBuilder planeBuilder;

    private void Awake()
    {
        planeMonoBehaviour = (PlaneMonoBehaviour)target;
        planeBuilder = planeMonoBehaviour.Builder;
    }

    public override void OnInspectorGUI()
    {
        planeMonoBehaviour = (PlaneMonoBehaviour)target;
        planeBuilder = planeMonoBehaviour.Builder;

        BasePrimitiveMonoBehaviourEditor<PlaneBuilder>.GenerateBaseFields(planeBuilder);

        EditorGUI.BeginDisabledGroup(!planeBuilder.FrontSided && !planeBuilder.BackSided);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Plane Settings", EditorStyles.boldLabel);
        planeBuilder.SetWidth(EditorGUILayout.FloatField("Width", planeBuilder.Width));
        planeBuilder.SetHeight(EditorGUILayout.FloatField("Height", planeBuilder.Height));

        planeBuilder.SetNumberOfSegmentsWidth(EditorGUILayout.IntField("Number Of Segments Width", planeBuilder.NumberOfSegmentsWidth));
        planeBuilder.SetNumberOfSegmentsHeight(EditorGUILayout.IntField("Number Of Segments Height", planeBuilder.NumberOfSegmentsHeight));

        EditorGUI.EndDisabledGroup();

        planeMonoBehaviour.RecreateMesh();
    }


    private void OnSceneGUI()
    {
        Vector3 pos = planeMonoBehaviour.gameObject.transform.position;
        float size = planeBuilder.Width;
        Handles.Label(pos, "asdasda");
        float snap = 0.5f;
        
        Quaternion rot = Quaternion.Euler(planeBuilder.Rotation);
        planeBuilder.SetWidth(Handles.ScaleValueHandle
        (
            planeBuilder.Width,
            pos,
            Quaternion.identity,
            planeBuilder.Width,
            Handles.ArrowHandleCap,
            snap

        ));
    }


    //planeObject.Pivot = EditorGUILayout.Vector2Field("Pivot", planeObject.Pivot);

    //planeObject.Rotation = EditorGUILayout.Vector3Field("Rotation", planeObject.Rotation);

    //planeObject.NumberOfSegmentsWidth = EditorGUILayout.IntField("Number Of Segments - Width", planeObject.NumberOfSegmentsWidth);
    //planeObject.NumberOfSegmentsHeight = EditorGUILayout.IntField("Number Of Segments - Height", planeObject.NumberOfSegmentsHeight);

    //planeObject.GenerateUv = EditorGUILayout.Toggle("Generate UV Map", planeObject.GenerateUv);

    //EditorGUI.BeginDisabledGroup(!planeObject.GenerateUv);
    //planeObject.SizeRelative = EditorGUILayout.Toggle("Size Relative", planeObject.SizeRelative);

    //planeObject.UvTiling = EditorGUILayout.Vector2Field("UV Map Tiling", planeObject.UvTiling);
    //planeObject.UvOffset = EditorGUILayout.Vector2Field("UV Map Offset", planeObject.UvOffset);
    //EditorGUI.EndDisabledGroup();

    //planeObject.PivotX = EditorGUILayout.FloatField("PivotX", planeObject.PivotX);
    //planeObject.PivotY = EditorGUILayout.FloatField("PivotY", planeObject.PivotY);

    //planeObject.Build();
}