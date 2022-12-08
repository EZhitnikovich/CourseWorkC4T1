using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c4t1.Model
{
	public struct Vector3Int
	{
		public Vector3Int(int x, int y, int z)
		{
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public static bool operator ==(Vector3Int v1, Vector3Int v2) => v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;

        public static bool operator !=(Vector3Int v1, Vector3Int v2) => !(v1 == v2);
    }
}
