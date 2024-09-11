using Vector512I = System.Runtime.Intrinsics.Vector512;
using Vector256I = System.Runtime.Intrinsics.Vector256;
using Vector128I = System.Runtime.Intrinsics.Vector128;
using Vector64I = System.Runtime.Intrinsics.Vector64;
using VectorI = System.Numerics.Vector;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics;
using System.Numerics;

namespace SIMDExtensions_Core.Vectors;

public readonly ref partial struct BaseVectorDepracated<T> where T : struct, INumber<T>
{
	public void AA()
	{
		var _arr = new T[100];

		var _vec = MemoryMarshal.Cast<T, Vector256<T>>(_arr.AsSpan());
	}
}