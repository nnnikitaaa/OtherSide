using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public Sentence[] showText;

    [Serializable]
    public class Sentence
    {
        public string text;
        public float showTime;
    }
}
