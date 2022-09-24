using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHandler : MonoBehaviour
{
    private HashSet<LockedStage> lockedStages;
    private HashSet<ItemType> unlockedItems;
    private HashSet<Recipe> unlockedRecipes;

    private void UnlockStage(Stage stage) {
        if (stage is LockedStage lockedStage) lockedStages.Remove(lockedStage);
        stage.GetUnlockedItems().ForEach(x => unlockedItems.Add(x));
    }

    private void UpdateUnlockedRecipes() {

    }
}
