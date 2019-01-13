using UnityEngine;

public interface IPivotable<T>
{
    Vector3 Pivot { get; }

    T SetPivot(Vector3 pivot);
}

