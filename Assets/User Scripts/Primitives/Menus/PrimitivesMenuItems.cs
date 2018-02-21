using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Unity Editor Primitives creation menu items
/// </summary>
public class PrimitivesMenuItems : MonoBehaviour {

    #region menu items methods

    /// <summary>
    /// Create Plane Primitive in Unity Editor
    /// </summary>
    [MenuItem("GameObject/Primivites/2D/Plane",false,-10)]
    private static void AddPlane()
    {
        GameObject gameObject = new GameObject()
        {
            name = "Plane"
        };

        gameObject.AddComponent<PlaneMonoBehaviour>();

        CenterAndSelectObject(gameObject);     
    }

    #endregion

    #region helper methods

    /// <summary>
    /// Centers camera on created GameObject, and selects it
    /// </summary>
    /// <param name="go">GameObject to center on and select</param>
    private static void CenterAndSelectObject(GameObject go)
    {
        Selection.activeObject = SceneView.lastActiveSceneView;
        Vector3 position = SceneView.lastActiveSceneView.pivot;
        go.transform.position = position;
        Selection.activeGameObject = go;
    }

    #endregion
}
