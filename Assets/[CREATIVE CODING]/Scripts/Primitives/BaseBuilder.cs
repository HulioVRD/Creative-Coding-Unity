using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public abstract class BaseBuilder<T> : IBuildable<T>, IPivotable<T>, IRotable<T>, ITexturable<T> where T: BaseBuilder<T>
{
    #region variables and fields
    [SerializeField]
    protected bool _changed = true;
    public bool Changed
    {
        get
        {
            return _changed;
        }
        set
        {
            _changed = value;
        }
    }

    [SerializeField]
    protected bool _frontSided = true;
    public bool FrontSided
    {
        get
        {
            return _frontSided;
        }
    }

    [SerializeField]
    protected bool _backSided = true;
    public bool BackSided
    {
        get
        {
            return _backSided;
        }
    }

    [SerializeField]
    protected Vector3 _pivot = new Vector3(0.5f,0.5f,0.5f);
    public Vector3 Pivot
    {
        get
        {
            return _pivot;
        }
    }

    [SerializeField]
    protected Vector3 _rotation = new Vector3(0, 0, 0);
    public Vector3 Rotation
    {
        get
        {
            return _rotation;
        }
    }

    [SerializeField]
    protected bool _generateTexture = true;
    public bool GenerateUvMap
    {
        get
        {
            return _generateTexture;
        }
    }

    [SerializeField]
    protected bool _sizeRelative = false;
    public bool SizeRelative
    {
        get
        {
            return _sizeRelative;
        }
    }

    [SerializeField]
    protected Vector2 _uvTiling = new Vector2(1,1);
    public Vector2 UvTiling
    {
        get
        {
            return _uvTiling;
        }
    }

    [SerializeField]
    protected Vector2 _uvOffset = new Vector2(0, 0);
    public Vector2 UvOffset
    {
        get
        {
            return _uvOffset;
        }
    }
    #endregion

    public abstract Mesh Build();

    public void SetValue<R>(ref R currentValue, R nextValue)
    {

        if(!EqualityComparer<R>.Default.Equals(currentValue, nextValue))
        {
            currentValue = nextValue;
            _changed = true;
        }
    }

    #region builders methods
    public T SetPivot(Vector3 pivot)
    {
        SetValue(ref _pivot, pivot);
        return (T)this;
    }

    public T SetRotation(Vector3 rotation)
    {
        SetValue(ref _rotation, rotation);
        return (T)this;
    }

    public T SetSizeRelative(bool sizeRelative)
    {
        SetValue(ref _sizeRelative, sizeRelative);
        return (T)this;
    }

    public T SetGenerateUvMap(bool generateTexture)
    {
        SetValue(ref _generateTexture, generateTexture);
        return (T)this;
    }

    public T SetUvTiling(Vector2 uvTiling)
    {
        SetValue(ref _uvTiling, uvTiling);
        return (T)this;
    }

    public T SetUvTiling(float uvTilingX, float uvTilingY)
    {
        SetValue(ref _uvTiling, new Vector2(uvTilingX, uvTilingY));
        return (T)this;
    }

    public T SetUvOffset(Vector2 uvOffset)
    {
        SetValue(ref _uvOffset, uvOffset);
        return (T)this;
    }

    public T SetUvOffset(float uvOffsetX, float uvOffsetY)
    {
        SetValue(ref _uvOffset, new Vector2(uvOffsetX, uvOffsetY));
        return (T)this;
    }

    public T SetFrontSided(bool frontSided)
    {
        SetValue(ref _frontSided, frontSided);
        return (T)this;
    }

    public T SetBackSided(bool backSided)
    {
        SetValue(ref _backSided, backSided);
        return (T)this;
    }

    public T SetSides(bool frontSided, bool backSided)
    {
        return SetFrontSided(frontSided).SetBackSided(backSided);
    }



    #endregion
}

