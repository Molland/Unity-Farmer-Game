using System.Collections;
using NUnit.Framework;
using HappyHarvest;
using UnityEngine;
using UnityEngine.TestTools;

// Вариант 19: тест на механику PlayerDeath
// (Assets/HappyHarvest/Scripts/Health.cs)
public class PlayerDeathTests
{
    // PLAYMODE ТЕСТ (ВАРИАНТ 19): при нулевом здоровье планируется событие PlayerDeath
    [UnityTest]
    public IEnumerator PlayMode_PlayerDeath_EventPlannedAtZeroHealth()
    {
        // Создаем временный объект с реальным скриптом здоровья из игры
        GameObject playerObj = new GameObject("TestPlayer");
        playerObj.SetActive(false);
        Health health = playerObj.AddComponent<Health>();

        health.maxHP = 2;
        playerObj.SetActive(true); // Awake выставит CurrentHP = maxHP = 2

        // Подписываемся на событие смерти игрока
        bool deathPlanned = false;
        void OnPlayerDeath() => deathPlanned = true;
        Health.PlayerDeath += OnPlayerDeath;

        // Пока здоровье больше нуля — событие смерти планироваться не должно
        health.Decrement();
        Assert.IsFalse(deathPlanned,
            "Ошибка: событие PlayerDeath не должно планироваться, пока здоровье больше нуля!");

        // При достижении нулевого здоровья должно планироваться событие PlayerDeath
        health.Decrement();
        Assert.IsTrue(deathPlanned,
            "Ошибка: при нулевом здоровье должно планироваться событие PlayerDeath!");

        // Отписываемся от события и удаляем объект после теста
        Health.PlayerDeath -= OnPlayerDeath;
        Object.Destroy(playerObj);

        // Пропускаем один кадр для симуляции игрового времени
        yield return null;
    }
}
