using System.Threading;

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