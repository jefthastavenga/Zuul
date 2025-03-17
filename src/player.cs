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
    private int health = 100;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
   public bool IsHurt()
   {
   return true;     
   }

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
    public bool IsAlive()
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