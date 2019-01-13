using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlaneBuilder : BaseBuilder<PlaneBuilder>, IPlaneBased<PlaneBuilder>
{
    #region variables and fields

    [SerializeField]
    protected float _width = 1;
    public float Width
    {
        get
        {
            return _width;
        }
    }

    [SerializeField]
    protected float _height = 1;
    public float Height {
        get
        {
            return _height;
        }
    }

    [SerializeField]
    protected int _numberOfSegmentsWidth = 1;
    public int NumberOfSegmentsWidth
    {
        get
        {
            return _numberOfSegmentsWidth;
        }
    }

    [SerializeField]
    protected int _numberOfSegmentsHeight = 1;
    public int NumberOfSegmentsHeight
    {
        get
        {
            return _numberOfSegmentsHeight;
        }
    }

    #endregion

    #region builders methods
    public override Mesh Build()
    {      
        return PlanePrimitive.Create(this);
    }

    public PlaneBuilder SetWidth(float width)
    {
        SetValue(ref _width, width);
        return this;
    }

    public PlaneBuilder SetHeight(float height)
    {
        SetValue(ref _height, height);
        return this;
    }

    public PlaneBuilder SetDimensions(float width, float height)
    {
        return SetWidth(width).SetHeight(height);
    }

    public PlaneBuilder SetDimensions(Vector3 dimensions)
    {
        return SetWidth(dimensions.x).SetHeight(dimensions.y);
    }


    public PlaneBuilder SetNumberOfSegmentsWidth(int numberOfSegmentsWidth)
    {
        if (numberOfSegmentsWidth < 1)
            numberOfSegmentsWidth = 1;

        SetValue(ref _numberOfSegmentsWidth, numberOfSegmentsWidth);

        return this;
    }


    public PlaneBuilder SetNumberOfSegmentsHeight(int numberOfSegmentsHeight)
    {
        if (numberOfSegmentsHeight < 1)
            numberOfSegmentsHeight = 1;

        SetValue(ref _numberOfSegmentsHeight, numberOfSegmentsHeight);

        return this;
    }

    public PlaneBuilder SetNumberOfSegments(int numberOfSegmentsWidth, int numberOfSegmentsHeight)
    {
        return SetNumberOfSegmentsWidth(numberOfSegmentsWidth)
              .SetNumberOfSegmentsHeight(numberOfSegmentsHeight);
    }

    #endregion
}
