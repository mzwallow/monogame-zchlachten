using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class Tag
    {
        public readonly Player Player, Enemy;
        public readonly StatusEffect StatusEffect;

        public TagType Type;

        public Tag(TagType type)
        {
            Type = type;
        }

        public Tag(Player player, Player enemy, TagType type)
        {
            Player = player;
            Enemy = enemy;

            Type = type;
        }

        public Tag(TagType type, StatusEffect statusEffect)
        {
            StatusEffect = statusEffect;

            Type = type;
        }
    }
}