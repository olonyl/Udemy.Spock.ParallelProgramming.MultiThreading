using System;

namespace _42.ResourceSharedByThreats
{
    class CharacterLock
    {
        private int _armor;
        private int _health = 100;

        public int Health { get => _health; private set => _health = value; }
        public int Armor { get => _armor; private set => _armor = value; }
        private readonly object _token = new object();
        public void Hit(int damage)
        {
            using (_token.Lock(TimeSpan.FromSeconds(3)))
            {
                Health -= damage - Armor;

            }
        }

        public void Heal(int health)
        {
            using (_token.Lock(TimeSpan.FromSeconds(3)))
            {
                Health += health;

            }
        }
    }
}
