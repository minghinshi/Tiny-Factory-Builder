using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButton : ItemLabel
{
    private Process process;

    public CraftingButton(Process process) {
        this.process = process;
        
    }
}
