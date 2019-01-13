using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class PlanePrimitive
{
    #region variables

    #region front only
    public static Vector3[] verticesFront = new Vector3[]
    {
          new Vector3(0, 0, 0),
          new Vector3(1, 0, 0),
          new Vector3(0, 1, 0),
          new Vector3(1, 1, 0)
    };

    public static int[] trisFront = 
    {
        0, 2, 1,
        1, 2, 3
    };

    public static Vector2[] uvsFront = new Vector2[]
    {
        new Vector2(0,0),
        new Vector2(1,0),
        new Vector2(0,1),
        new Vector2(1,1)
    };
    #endregion

    #region back only
    public static Vector3[] verticesBack = new Vector3[]
    {
          new Vector3(0, 0, 0),
          new Vector3(1, 0, 0),
          new Vector3(0, 1, 0),
          new Vector3(1, 1, 0)
    };

    public static int[] trisBack =
    {
        0, 1, 2,
        1, 3, 2
    };

    public static Vector2[] uvsBack = new Vector2[]
    {
        new Vector2(0,0),
        new Vector2(1,0),
        new Vector2(0,1),
        new Vector2(1,1)
    };
    #endregion

    #region double sided
    public static Vector3[] verticesDoubleSided = new Vector3[]
    {
          new Vector3(0, 0, 0),
          new Vector3(1, 0, 0),
          new Vector3(0, 1, 0),
          new Vector3(1, 1, 0),
          new Vector3(0, 0, 0),
          new Vector3(1, 0, 0),
          new Vector3(0, 1, 0),
          new Vector3(1, 1, 0)
    };

    public static int[] trisDoubleSided = 
    {
        0, 2, 1,
        1, 2, 3,
        0, 1, 2,
        1, 3, 2
    };

    #endregion

    #endregion

    #region helper methods
    public static Vector2 CalculatePivotShift(float pivotXRelative, float pivotYRelative, float width, float height)
    {
        return new Vector2(pivotXRelative * width, pivotYRelative * height);
    }

    public static Vector2 CalculatePivotShift(Vector2 pivot, float width, float height)
    {
        return new Vector2(pivot.x * width, pivot.y * height);
    }

    #endregion

    #region main creation method
    public static Mesh Create(PlaneBuilder builder)
    {

        if (!builder.FrontSided && !builder.BackSided)
            return null;

        // Generate only front side
        if (builder.FrontSided && !builder.BackSided)
        {
            // with UV
            if (builder.GenerateUvMap)
            {
                return CreateOnlyFrontTextured(builder);
            }
            else
            {
                return CreateOnlyFrontUntextured(builder);
            }
        }
        // Generate only back side
        else if (!builder.FrontSided && builder.BackSided)
        {
            // with UV
            if (builder.GenerateUvMap)
            {
                return CreateOnlyBackTextured(builder);
            }
            else
            {
                return CreateOnlyBackUntextured(builder);
            }
        }
        // Generate double sided
        else
        {
            // with UV
            if (builder.GenerateUvMap)
            {
                return CreateDoubleSidedTextured(builder);
            }
            else
            {
                return CreateDoubleSidedUntextured(builder);
            }
        }
    }
    #endregion

    #region one sided (only front) untextured creation methods

    public static Mesh CreateOnlyFrontUntextured()
    {
        return new Mesh
        {
            vertices = verticesFront,
            triangles = trisFront,
            uv = null
        };
    }

    public static Mesh CreateOnlyFrontUntextured(float width, float height)
    {
        return new Mesh
        {
            vertices = new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(width,0,0),
                new Vector3(0,height,0),
                new Vector3(width,height,0)
            },
            triangles = trisFront,
            uv = null
        };
    }

    public static Mesh CreateOnlyFrontUntextured(PlaneBuilder builder)
    {
        int numberOfVerticesWidth = builder.NumberOfSegmentsWidth + 1;
        int numberOfVerticesHeight = builder.NumberOfSegmentsHeight + 1;
        int numberOfVertices = numberOfVerticesWidth * numberOfVerticesHeight;

        float segmentSizeWidth = builder.Width / builder.NumberOfSegmentsWidth;
        float segmentSizeHeight = builder.Height / builder.NumberOfSegmentsHeight;
       
        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uvs = null;
        int[] triangles = new int[builder.NumberOfSegmentsWidth * builder.NumberOfSegmentsHeight * 6];

        Vector2 shifts = CalculatePivotShift(builder.Pivot.x, builder.Pivot.y, builder.Width, builder.Height);

        Quaternion rotation = Quaternion.Euler(builder.Rotation);

        for (int y = 0; y < numberOfVerticesHeight; y++)
        {
            for (int x = 0; x < numberOfVerticesWidth; x++)
            {
                int n = y * numberOfVerticesWidth + x;

                vertices[n] = rotation * new Vector3(x * segmentSizeWidth - shifts[0], y * segmentSizeHeight - shifts[1], 0);

            }
        }

        int index = 0;
        for (int y = 0; y < builder.NumberOfSegmentsHeight; y++)
        {
            for (int x = 0; x < builder.NumberOfSegmentsWidth; x++)
            {
                triangles[index] = y * numberOfVerticesWidth + x;
                triangles[index + 1] = (y + 1) * numberOfVerticesWidth + x;
                triangles[index + 2] = y * numberOfVerticesWidth + (x + 1);

                triangles[index + 3] = y * numberOfVerticesWidth + (x + 1);
                triangles[index + 4] = (y + 1) * numberOfVerticesWidth + x;
                triangles[index + 5] = (y + 1) * numberOfVerticesWidth + (x + 1);
                index += 6;
            }
        }

        Mesh mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles,
            uv = uvs,
        };

        mesh.RecalculateNormals();

        return mesh;
    }

    #endregion

    #region one sided (only back) untextured creation methods

    public static Mesh CreateOnlyBackUntextured(PlaneBuilder builder)
    {
        int numberOfVerticesWidth = builder.NumberOfSegmentsWidth + 1;
        int numberOfVerticesHeight = builder.NumberOfSegmentsHeight + 1;
        int numberOfVertices = numberOfVerticesWidth * numberOfVerticesHeight;

        float segmentSizeWidth = builder.Width / builder.NumberOfSegmentsWidth;
        float segmentSizeHeight = builder.Height / builder.NumberOfSegmentsHeight;

        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uvs = null;
        int[] triangles = new int[builder.NumberOfSegmentsWidth * builder.NumberOfSegmentsHeight * 6];

        Vector2 shifts = CalculatePivotShift(builder.Pivot, builder.Width, builder.Height);

        Quaternion rotation = Quaternion.Euler(builder.Rotation);

        for (int y = 0; y < numberOfVerticesHeight; y++)
        {
            for (int x = 0; x < numberOfVerticesWidth; x++)
            {
                int n = y * numberOfVerticesWidth + x;

                vertices[n] = rotation * new Vector3(x * segmentSizeWidth - shifts[0], y * segmentSizeHeight - shifts[1], 0);

            }
        }

        int index = 0;
        for (int y = 0; y < builder.NumberOfSegmentsHeight; y++)
        {
            for (int x = 0; x < builder.NumberOfSegmentsWidth; x++)
            {
                triangles[index] = y * numberOfVerticesWidth + x;
                triangles[index + 1] = y * numberOfVerticesWidth + (x + 1);
                triangles[index + 2] = (y + 1) * numberOfVerticesWidth + x;

                triangles[index + 3] = y * numberOfVerticesWidth + (x + 1);
                triangles[index + 4] = (y + 1) * numberOfVerticesWidth + (x + 1);
                triangles[index + 5] = (y + 1) * numberOfVerticesWidth + x;
                index += 6;
            }
        }

        Mesh mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles,
            uv = uvs,
        };

        mesh.RecalculateNormals();

        return mesh;
    }

    #endregion

    #region one sided (only front) textured creation methods

    public static Mesh CreateOnlyFrontTextured(PlaneBuilder builder)
    {
        int numberOfVerticesWidth = builder.NumberOfSegmentsWidth + 1;
        int numberOfVerticesHeight = builder.NumberOfSegmentsHeight + 1;
        int numberOfVertices = numberOfVerticesWidth * numberOfVerticesHeight;

        float segmentSizeWidth = builder.Width / builder.NumberOfSegmentsWidth;
        float segmentSizeHeight = builder.Height / builder.NumberOfSegmentsHeight;

        float uvSegmentSizeWidth;
        float uvSegmentSizeHeight;

        if (builder.SizeRelative)
        {
            uvSegmentSizeWidth = 1 / builder.NumberOfSegmentsWidth;
            uvSegmentSizeHeight = 1 / builder.NumberOfSegmentsHeight;
        }
        else
        {
            uvSegmentSizeWidth = segmentSizeWidth;
            uvSegmentSizeHeight = segmentSizeHeight;
        }


        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uvs = new Vector2[numberOfVertices];

        int[] triangles = new int[builder.NumberOfSegmentsWidth * builder.NumberOfSegmentsHeight * 6];

        Vector2 shifts = CalculatePivotShift(builder.Pivot, builder.Width, builder.Height);

        Quaternion rotation = Quaternion.Euler(builder.Rotation);

        for (int y = 0; y < numberOfVerticesHeight; y++)
        {
            for (int x = 0; x < numberOfVerticesWidth; x++)
            {
                int n = y * numberOfVerticesWidth + x;

                vertices[n] = rotation * new Vector3(x * segmentSizeWidth - shifts[0], y * segmentSizeHeight - shifts[1], 0);

                uvs[n] = new Vector2
                (
                    (x * uvSegmentSizeWidth + builder.UvOffset.x) * builder.UvTiling.x,
                    (y * uvSegmentSizeHeight + builder.UvOffset.y) * builder.UvTiling.y
                );

            }
        }

        int index = 0;
        for (int y = 0; y < builder.NumberOfSegmentsHeight; y++)
        {
            for (int x = 0; x < builder.NumberOfSegmentsWidth; x++)
            {
                triangles[index] = y * numberOfVerticesWidth + x;
                triangles[index + 1] = (y + 1) * numberOfVerticesWidth + x;
                triangles[index + 2] = y * numberOfVerticesWidth + (x + 1);

                triangles[index + 3] = y * numberOfVerticesWidth + (x + 1);
                triangles[index + 4] = (y + 1) * numberOfVerticesWidth + x;
                triangles[index + 5] = (y + 1) * numberOfVerticesWidth + (x + 1);
                index += 6;
            }
        }

        Mesh mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles,
            uv = uvs,
        };

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }

    #endregion

    #region one sided (only back) textured creation methods

    public static Mesh CreateOnlyBackTextured(PlaneBuilder builder)
    {
        int numberOfVerticesWidth = builder.NumberOfSegmentsWidth + 1;
        int numberOfVerticesHeight = builder.NumberOfSegmentsHeight + 1;
        int numberOfVertices = numberOfVerticesWidth * numberOfVerticesHeight;

        float segmentSizeWidth = builder.Width / builder.NumberOfSegmentsWidth;
        float segmentSizeHeight = builder.Height / builder.NumberOfSegmentsHeight;

        float uvSegmentSizeWidth;
        float uvSegmentSizeHeight;

        if (builder.SizeRelative)
        {
            uvSegmentSizeWidth = 1 / builder.NumberOfSegmentsWidth;
            uvSegmentSizeHeight = 1 / builder.NumberOfSegmentsHeight;
        }
        else
        {
            uvSegmentSizeWidth = segmentSizeWidth;
            uvSegmentSizeHeight = segmentSizeHeight;
        }


        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uvs = new Vector2[numberOfVertices];

        int[] triangles = new int[builder.NumberOfSegmentsWidth * builder.NumberOfSegmentsHeight * 6];

        Vector2 shifts = CalculatePivotShift(builder.Pivot, builder.Width, builder.Height);

        Quaternion rotation = Quaternion.Euler(builder.Rotation);

        for (int y = 0; y < numberOfVerticesHeight; y++)
        {
            for (int x = 0; x < numberOfVerticesWidth; x++)
            {
                int n = y * numberOfVerticesWidth + x;

                vertices[n] = rotation * new Vector3(x * segmentSizeWidth - shifts[0], y * segmentSizeHeight - shifts[1], 0);

                uvs[n] = new Vector2
                (
                    (x * uvSegmentSizeWidth + builder.UvOffset.x) * builder.UvTiling.x,
                    (y * uvSegmentSizeHeight + builder.UvOffset.y) * builder.UvTiling.y
                );

            }
        }

        int index = 0;
        for (int y = 0; y < builder.NumberOfSegmentsHeight; y++)
        {
            for (int x = 0; x < builder.NumberOfSegmentsWidth; x++)
            {
                triangles[index] = y * numberOfVerticesWidth + x;
                triangles[index + 1] = y * numberOfVerticesWidth + (x + 1);
                triangles[index + 2] = (y + 1) * numberOfVerticesWidth + x;

                triangles[index + 3] = y * numberOfVerticesWidth + (x + 1);
                triangles[index + 4] = (y + 1) * numberOfVerticesWidth + (x + 1);
                triangles[index + 5] = (y + 1) * numberOfVerticesWidth + x;
                index += 6;
            }
        }

        Mesh mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles,
            uv = uvs,
        };

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }

    #endregion

    #region two sided untextured creation methods

    public static Mesh CreateDoubleSidedUntextured(PlaneBuilder builder)
    {
        int numberOfVerticesWidth = builder.NumberOfSegmentsWidth + 1;
        int numberOfVerticesHeight = builder.NumberOfSegmentsHeight + 1;
        int numberOfVerticesOneSide = numberOfVerticesWidth * numberOfVerticesHeight;
        int numberOfVertices = numberOfVerticesOneSide * 2;

        float segmentSizeWidth = builder.Width / builder.NumberOfSegmentsWidth;
        float segmentSizeHeight = builder.Height / builder.NumberOfSegmentsHeight;

        Vector3[] vertices = new Vector3[numberOfVertices];

        int[] triangles = new int[builder.NumberOfSegmentsWidth * builder.NumberOfSegmentsHeight * 6 * 2];
        Vector2[] uvs = null;

        Vector2 shifts = CalculatePivotShift(builder.Pivot, builder.Width, builder.Height);

        Quaternion rotation = Quaternion.Euler(builder.Rotation);

        for (int y = 0; y < numberOfVerticesHeight; y++)
        {
            for (int x = 0; x < numberOfVerticesWidth; x++)
            {
                int n = y * numberOfVerticesWidth + x;
                int backN = numberOfVerticesOneSide + n;

                // Generate for front vertice
                vertices[n] = rotation * new Vector3(x * segmentSizeWidth - shifts[0], y * segmentSizeHeight - shifts[1], 0);

                // Generate for back vertice
                vertices[backN] = vertices[n];
            }
        }

        int index = 0;
        for (int y = 0; y < builder.NumberOfSegmentsHeight; y++)
        {
            for (int x = 0; x < builder.NumberOfSegmentsWidth; x++)
            {
                // Triangles for front segment
                triangles[index] = y * numberOfVerticesWidth + x;
                triangles[index + 1] = (y + 1) * numberOfVerticesWidth + x;
                triangles[index + 2] = y * numberOfVerticesWidth + (x + 1);

                triangles[index + 3] = y * numberOfVerticesWidth + (x + 1);
                triangles[index + 4] = (y + 1) * numberOfVerticesWidth + x;
                triangles[index + 5] = (y + 1) * numberOfVerticesWidth + (x + 1);

                // Triangles for back segment
                triangles[index + 6] = numberOfVerticesOneSide + y * numberOfVerticesWidth + x;
                triangles[index + 7] = numberOfVerticesOneSide + y * numberOfVerticesWidth + (x + 1);
                triangles[index + 8] = numberOfVerticesOneSide + (y + 1) * numberOfVerticesWidth + x;

                triangles[index + 9] = numberOfVerticesOneSide + y * numberOfVerticesWidth + (x + 1);
                triangles[index + 10] = numberOfVerticesOneSide + (y + 1) * numberOfVerticesWidth + (x + 1);
                triangles[index + 11] = numberOfVerticesOneSide + (y + 1) * numberOfVerticesWidth + x;
                index += 12;
            }
        }

        Mesh mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles,
            uv = uvs,
        };

        mesh.RecalculateNormals();

        return mesh;
    }

    #endregion

    #region two sided textured creation methods

    public static Mesh CreateDoubleSidedTextured(PlaneBuilder builder)
    {
        int numberOfVerticesWidth = builder.NumberOfSegmentsWidth + 1;
        int numberOfVerticesHeight = builder.NumberOfSegmentsHeight + 1;
        int numberOfVerticesOneSide = numberOfVerticesWidth * numberOfVerticesHeight;
        int numberOfVertices = numberOfVerticesOneSide * 2;

        float segmentSizeWidth = builder.Width / builder.NumberOfSegmentsWidth;
        float segmentSizeHeight = builder.Height / builder.NumberOfSegmentsHeight;

        float uvSegmentSizeWidth;
        float uvSegmentSizeHeight;

        if (builder.SizeRelative)
        {
            uvSegmentSizeWidth = 1 / builder.NumberOfSegmentsWidth;
            uvSegmentSizeHeight = 1 / builder.NumberOfSegmentsHeight;
        }
        else
        {
            uvSegmentSizeWidth = segmentSizeWidth;
            uvSegmentSizeHeight = segmentSizeHeight;
        }


        Vector3[] vertices = new Vector3[numberOfVertices];
        Vector2[] uvs = new Vector2[numberOfVertices];

        int[] triangles = new int[builder.NumberOfSegmentsWidth * builder.NumberOfSegmentsHeight * 6 * 2];

        Vector2 shifts = CalculatePivotShift(builder.Pivot, builder.Width, builder.Height);

        Quaternion rotation = Quaternion.Euler(builder.Rotation);

        for (int y = 0; y < numberOfVerticesHeight; y++)
        {
            for (int x = 0; x < numberOfVerticesWidth; x++)
            {
                int n = y * numberOfVerticesWidth + x;
                int backN = numberOfVerticesOneSide + n;

                // Generate for front vertice
                vertices[n] = rotation * new Vector3(x * segmentSizeWidth - shifts[0], y * segmentSizeHeight - shifts[1], 0);
                uvs[n] = new Vector2
                (
                    (x * uvSegmentSizeWidth + builder.UvOffset.x) * builder.UvTiling.x,
                    (y * uvSegmentSizeHeight + builder.UvOffset.y) * builder.UvTiling.y
                );

                // Generate for back vertice
                vertices[backN] = vertices[n];
                uvs[backN] = uvs[n];
            }
        }

        int index = 0;
        for (int y = 0; y < builder.NumberOfSegmentsHeight; y++)
        {
            for (int x = 0; x < builder.NumberOfSegmentsWidth; x++)
            {
                // Triangles for front segment
                triangles[index] = y * numberOfVerticesWidth + x;
                triangles[index + 1] = (y + 1) * numberOfVerticesWidth + x;
                triangles[index + 2] = y * numberOfVerticesWidth + (x + 1);

                triangles[index + 3] = y * numberOfVerticesWidth + (x + 1);
                triangles[index + 4] = (y + 1) * numberOfVerticesWidth + x;
                triangles[index + 5] = (y + 1) * numberOfVerticesWidth + (x + 1);

                // Triangles for back segment
                triangles[index + 6] = numberOfVerticesOneSide + y * numberOfVerticesWidth + x;
                triangles[index + 7] = numberOfVerticesOneSide + y * numberOfVerticesWidth + (x + 1);
                triangles[index + 8] = numberOfVerticesOneSide + (y + 1) * numberOfVerticesWidth + x;

                triangles[index + 9] = numberOfVerticesOneSide + y * numberOfVerticesWidth + (x + 1);
                triangles[index + 10] = numberOfVerticesOneSide + (y + 1) * numberOfVerticesWidth + (x + 1);
                triangles[index + 11] = numberOfVerticesOneSide + (y + 1) * numberOfVerticesWidth + x;
                index += 12;
            }
        }

        Mesh mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles,
            uv = uvs,
        };

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }

    #endregion
}
