using UnityEngine;
using System.Collections;
using UnityEditor;

public class AssignMaterial : ScriptableWizard
{
    public Material theMaterial;

    void OnWizardUpdate()
    {
        helpString = "Select Game Obects";
        isValid = (theMaterial != null);
    }

    void OnWizardCreate()
    {
        GameObject[] gos = Selection.gameObjects;

        foreach(GameObject go in gos)
        {
            go.GetComponent<Renderer>().material = theMaterial;
        }
    }

    [MenuItem("Custom/Assign Material")]
    static void assignMaterial()
    {
        ScriptableWizard.DisplayWizard("Assign Material", typeof(AssignMaterial), "Assign");
    }
}
