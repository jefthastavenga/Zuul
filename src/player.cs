class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    // constructor
    public Player()
    {
        health = 100;
        CurrentRoom = null;
    }
    // fields
    public int health;
    // // constructor

    // methods
    public int Damage(int amount)
    {
        health -= amount;
        return health;
    }
    public int Heal(int amount)
    {
        health += amount;
        return health;
    }
    private bool IsAlive()
    {
        if (health <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}