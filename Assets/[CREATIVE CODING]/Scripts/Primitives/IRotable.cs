using UnityEngine;

public interface IRotable <T>
{
    Vector3 Rotation { get;}

    T SetRotation(Vector3 rotation);
}

