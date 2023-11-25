
using UnityEditor;
using UnityEngine;

public class WizardSkin : ScriptableWizard
{
    [MenuItem("GameObject/Wizard Skin")]
    static void CreateWizardSkin()
    {
        ScriptableWizard.DisplayWizard<WizardSkin>("Create Skin", "Create", "Apply");
    }
}
