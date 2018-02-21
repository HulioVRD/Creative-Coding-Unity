using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITexturable<T>{
    bool GenerateUvMap { get;}
    bool SizeRelative { get;}
    Vector2 UvTiling { get;}
    Vector2 UvOffset { get;}

    T SetSizeRelative(bool sizeRelative);
    T SetGenerateUvMap(bool generateTexture);

    T SetUvTiling(Vector2 uvTiling);
    T SetUvTiling(float uvTilingX, float uvTilingY);

    T SetUvOffset(Vector2 uvOffset);
    T SetUvOffset(float uvOffsetX, float uvOffsetY);

}
