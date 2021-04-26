using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectEnum", menuName = "Data/Enum/Object")]
public class ObjectEnumData : EnumData
{
    public enum eType
    {
        Static,
        Kinematic,
        Dynamic
    }

    public eType value;

    public override int index { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override string[] names => throw new System.NotImplementedException();
}
