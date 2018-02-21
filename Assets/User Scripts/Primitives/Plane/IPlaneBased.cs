using UnityEngine;
/// <summary>
/// Interface of plane based builder
/// </summary>
/// <typeparam name="T">Builder class</typeparam>
interface IPlaneBased <T>
{
    float Width { get; }
    float Height { get;}

    int NumberOfSegmentsWidth { get; }
    int NumberOfSegmentsHeight { get; }

    T SetWidth(float width);
    T SetHeight(float height);
    T SetDimensions(float width, float height);
    T SetDimensions(Vector3 dimensions);

    T SetNumberOfSegmentsWidth(int numberOfSegmentsWidth);
    T SetNumberOfSegmentsHeight(int numberOfSegmentsHeight);
    T SetNumberOfSegments(int numberOfSegmentsWidth, int numberOfSegmentsHeight);
}
