using System.Collections.Generic;
using UnityEngine;

public class Recipe : ScriptableObject
{
    private Dictionary<string, int> inputs;
    private Dictionary<string, int> outputs;

    public Dictionary<string, int> GetInputs()
    {
        return inputs;
    }
}
