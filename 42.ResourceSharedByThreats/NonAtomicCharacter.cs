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
