using System;

namespace C_2_VS__Projekt
{
    public abstract class Item
    {
        public string Name { get; }
        protected Item(string name) => Name = name;
        public virtual int AttackBonus => 0;
        public virtual int DefenseBonus => 0;
    }

    public class Weapon : Item
    {
        public override int AttackBonus { get; }
        public Weapon(string name, int bonus) : base(name) => AttackBonus = bonus;
    }

    public class Armor : Item
    {
        public override int DefenseBonus { get; }
        public Armor(string name, int bonus) : base(name) => DefenseBonus = bonus;
    }

    public class Potion : Item
    {
        public Action<Entity> Effect { get; }
        public Potion(string name, Action<Entity> effect) : base(name) => Effect = effect;
    }
}