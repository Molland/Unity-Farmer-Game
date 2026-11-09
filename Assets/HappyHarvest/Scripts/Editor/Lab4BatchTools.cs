using HappyHarvest;
using UnityEditor;
using UnityEngine;

// Вспомогательный скрипт для автоматизации ЛР №4 из командной строки
public static class Lab4BatchTools
{
    // Добавляет компонент Health на префаб игрока Character
    public static void AddHealthToPlayerPrefab()
    {
        const string path = "Assets/HappyHarvest/Resources/Character.prefab";
        GameObject root = PrefabUtility.LoadPrefabContents(path);
        try
        {
            if (root.GetComponent<Health>() == null)
            {
                root.AddComponent<Health>();
                PrefabUtility.SaveAsPrefabAsset(root, path);
                Debug.Log("Lab4: компонент Health добавлен на префаб Character.");
            }
            else
            {
                Debug.Log("Lab4: компонент Health уже есть на префабе Character.");
            }
        }
        finally
        {
            PrefabUtility.UnloadPrefabContents(root);
        }
    }
}
