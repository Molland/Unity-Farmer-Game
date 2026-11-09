using NUnit.Framework;
using HappyHarvest;
using UnityEditor;
using UnityEngine;

// Тестовая сборка Tests (includePlatforms: Editor) — тесты EditMode
public class PlayerTests
{
    // В игре HappyHarvest игрок не лежит в сцене: он спавнится при старте
    // из префаба Resources/Character.prefab (см. GameManager.RegisterSpawn),
    // поэтому EditMode-тест проверяет конфигурацию этого префаба.
    private const string PlayerPrefabPath = "Assets/HappyHarvest/Resources/Character.prefab";

    // EDITMODE ТЕСТ: проверка правильности настройки префаба игрока
    [Test]
    public void EditMode_PlayerPrefab_IsConfiguredCorrectly()
    {
        // Загружаем содержимое префаба игрока
        GameObject playerPrefab = PrefabUtility.LoadPrefabContents(PlayerPrefabPath);
        Assert.IsNotNull(playerPrefab, "Ошибка: не удалось загрузить префаб Character.prefab!");

        try
        {
            // Проверяем, что у префаба игрока есть обязательный скрипт управления
            PlayerController controller = playerPrefab.GetComponent<PlayerController>();
            Assert.IsNotNull(controller, "Ошибка: на префабе игрока отсутствует скрипт PlayerController!");

            // Проверяем, что у префаба игрока есть скрипт здоровья
            Health health = playerPrefab.GetComponent<Health>();
            Assert.IsNotNull(health, "Ошибка: на префабе игрока отсутствует скрипт Health!");

            // Стартовое здоровье должно быть положительным
            Assert.Greater(health.maxHP, 0, "Ошибка: maxHP игрока должен быть больше нуля!");
        }
        finally
        {
            // Выгружаем префаб, чтобы ничего не изменить в ассете
            PrefabUtility.UnloadPrefabContents(playerPrefab);
        }
    }
}
