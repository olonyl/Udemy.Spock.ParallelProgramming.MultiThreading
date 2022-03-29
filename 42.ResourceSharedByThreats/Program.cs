using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _42.ResourceSharedByThreats
{

    class Character
    {
        private int _armor;
        private int _health = 100;

        public int Health { get => _health; private set => _health = value; }
        public int Armor { get => _armor; private set => _armor = value; }
        public void Hit(int damage)
        {
            //Health -= damage - Armor;
            int actualDamage = Interlocked.Add(ref damage, -Armor);
            Interlocked.Add(ref _health, -actualDamage);
        }

        public void Heal(int health)
        {
            //Health += health;
            Interlocked.Add(ref _health, health);
        }

        public void CastArmorSpell(bool isPositive)
        {
            if (isPositive)
            {
                Interlocked.Increment(ref _armor);
                //                Armor++;
            }
            else
            {
                //Armor--;
                Interlocked.Decrement(ref _armor);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            AtomicCharacter c = new AtomicCharacter();

            var tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                Task t1 = Task.Factory.StartNew(() =>
                 {
                     for (int j = 0; j < 10; j++)
                     {
                         c.Hit(10);
                     }
                 });
                tasks.Add(t1);

                Task t2 = Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        c.Heal(10);
                    }
                });
                tasks.Add(t2);
            }
            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Resulting Health is this={c.Health}");

            Console.Read();
        }
    }
}

class NonAtomicCharacter
{

    public int Health { get; private set; }
    public void Hit(int damage)
    {
        Health -= damage;
    }

    public void Heal(int health)
    {
        Health += health;
    }

}


class AtomicCharacter
{
    private int _health;

    public int Health { get => _health; private set => _health = value; }
    public void Hit(int damage)
    {
        Interlocked.Add(ref _health, -damage);
    }

    public void Heal(int health)
    {
        Interlocked.Add(ref _health, health);
    }

}