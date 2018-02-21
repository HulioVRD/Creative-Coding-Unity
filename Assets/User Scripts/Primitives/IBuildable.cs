using UnityEngine;

public interface IBuildable<T> {
    bool Changed { get; set;}
    bool FrontSided { get;}
    bool BackSided { get;}

    T SetFrontSided(bool frontSided);
    T SetBackSided(bool backSided);
    T SetSides(bool frontSided, bool backSided);
    Mesh Build();
}
