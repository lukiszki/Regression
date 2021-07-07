
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Network))]
public class PrintCostB : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Network net = (Network)target;
        if(GUILayout.Button("Print cost"))
        {
            net.printCost(net.TrainData);
        }
        if(GUILayout.Button("Train"))
        {
            net.Train(net.TrainData);
        }
    }
}
