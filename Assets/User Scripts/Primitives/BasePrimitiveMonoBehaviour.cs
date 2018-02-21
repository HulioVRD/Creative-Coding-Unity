using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public abstract class BasePrimitiveMonoBehaviour<T> : MonoBehaviour where T : IBuildable<T>,new()
{
    protected MeshRenderer meshRenderer;
    protected MeshFilter meshFilter;

    [SerializeField]
    protected T builder;
    public T Builder {
        get
        {
            if(builder == null)
            {
                builder = new T();
            }
            return builder;
        }
    }

    protected void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();           

        if (meshRenderer.sharedMaterial == null)
        {
            Shader shader = Shader.Find("Custom/UVTestShader");

            if(shader != null)
            {
                meshRenderer.sharedMaterial = new Material(shader);
            }
            else
            {
                meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
            }
        }

        meshFilter = GetComponent<MeshFilter>();
        RecreateMesh();
    }

    public void RecreateMesh()
    {
        if (Builder.Changed)
        {
            meshFilter.sharedMesh = Builder.Build();
            Builder.Changed = false;
        }
    }

}
