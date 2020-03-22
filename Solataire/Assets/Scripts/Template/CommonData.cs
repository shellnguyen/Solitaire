using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CommonData", menuName ="Template/CommonData")]
public class CommonData : ScriptableObject
{
    public ushort[] cardValues = { 0, 1, 1 << 1, 1 << 2, 1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7, 1 << 8,1 << 9, 1 << 10, 1 << 11 };
    public ushort[] suits = { 1 << 12, 1 << 13, 1 << 14, 1 << 15};
}
