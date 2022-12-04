using Newtonsoft.Json;
using System;

namespace BattlesnakeAzureFunction.Model
{
    public struct Coord : IEquatable<Coord>
    {
        [JsonProperty("x")]
        public readonly int X;

        [JsonProperty("y")]
        public readonly int Y;

        public Coord(int x, int y)
        {
            X = x; Y = y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

        public static Coord operator +(Coord a, Coord b)
        {
            return new Coord(a.X + b.X, a.Y + b.Y);
        }

        public Coord Move(Direction direction)
        {
            return new Coord(
                direction switch
                {
                    Direction.left => X - 1,
                    Direction.right => X + 1,
                    _ => X
                },
                direction switch
                {
                    Direction.up => Y + 1,
                    Direction.down => Y - 1,
                    _ => Y
                });
        }

        #region IEquatable

        public override bool Equals(object other)
        {
            if (other is Coord p)
            {
                return Equals(p);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Coord other)
        {
            return X == other.X && Y == other.Y;
        }

        public static bool operator ==(Coord a, Coord b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Coord a, Coord b)
        {
            return !a.Equals(b);
        }

        // Also always implement new hashcode if equals updated
        public override int GetHashCode()
        {
            return X * 113 + Y;
        }

        #endregion IEquatable
    }
}