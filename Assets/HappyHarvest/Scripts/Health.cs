using System;
using UnityEngine;

namespace HappyHarvest
{
    // Простая система здоровья игрока.
    // При достижении нулевого здоровья планируется событие PlayerDeath.
    public class Health : MonoBehaviour
    {
        // Стартовое (максимальное) количество хитпоинтов
        public int maxHP = 3;

        // Текущее здоровье (устанавливается в Awake равным maxHP)
        public int CurrentHP { get; private set; }

        // Жив ли персонаж
        public bool IsAlive => CurrentHP > 0;

        // Событие смерти игрока: планируется один раз, когда здоровье достигает нуля
        public static event Action PlayerDeath;

        void Awake()
        {
            CurrentHP = maxHP;
        }

        // Восстановление здоровья: не выше maxHP
        public void Increment()
        {
            CurrentHP = Mathf.Min(CurrentHP + 1, maxHP);
        }

        // Получение урона: уменьшает HP на 1.
        // Когда HP доходит до нуля — планируется событие PlayerDeath.
        public void Decrement()
        {
            if (!IsAlive)
            {
                return;
            }

            CurrentHP--;

            if (CurrentHP == 0)
            {
                PlayerDeath?.Invoke();
            }
        }
    }
}
