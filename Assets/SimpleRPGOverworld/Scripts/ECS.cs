using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ECS
{
    public static IEnumerable<Tuple<T>> GetEntities<T>() where T : MonoBehaviour, IComponentData
    {
        // A bad method for grabbing componentData.
        // It'd be better to store this in a Hashset or Dictionary and grab from there.
        // But for now...
        foreach (var componentData in Object.FindObjectsOfType<T>())
            yield return new Tuple<T>(componentData);
    }

    public static IEnumerable<Tuple<T0, T1>> GetEntities<T0, T1>() where T0 : MonoBehaviour, IComponentData where T1 : MonoBehaviour, IComponentData
    {
        // A bad method for grabbing componentData.
        // It'd be better to store this in a Hashset or Dictionary and grab from there.
        // But for now...
        foreach (var componentData1 in Object.FindObjectsOfType<T0>())
            foreach (var componentData2 in componentData1.GetComponents<T1>())
                yield return new Tuple<T0, T1>(componentData1, componentData2);
    }
}