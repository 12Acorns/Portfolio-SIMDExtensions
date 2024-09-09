using Vector512I = System.Runtime.Intrinsics.Vector512;
using Vector256I = System.Runtime.Intrinsics.Vector256;
using Vector128I = System.Runtime.Intrinsics.Vector128;
using Vector64I = System.Runtime.Intrinsics.Vector64;
using VectorI = System.Numerics.Vector;
using System.Runtime.Intrinsics.X86;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics;
using System.Numerics;

namespace SIMDExtensions_Core.Vectors;

[StructLayout(LayoutKind.Sequential)]
public readonly ref partial struct BaseVectorDepracated<T> where T : INumber<T>
{
	#region Init static
	private static readonly ArchitectureType targetPlatform;
	private static readonly SIMDSupport targetBitWidth;

	static BaseVectorDepracated()
	{
		targetPlatform = GetArchitectureType();
		targetBitWidth = GetSIMDWidth();
		if(targetBitWidth is not SIMDSupport.None && targetPlatform is ArchitectureType.None)
		{
			targetPlatform = ArchitectureType.Generic;
		}
	}
	private static ArchitectureType GetArchitectureType()
	{
		// 512 bit and below
		if(Avx512F.IsSupported)
		{
			return ArchitectureType.AVX512;
		}
		// 256 bit and below
		if(Avx2.IsSupported)
		{
			return ArchitectureType.AVX2;
		}
		if(Avx.IsSupported)
		{
			return ArchitectureType.AVX;
		}
		// 128 bit and below
		if(Sse42.IsSupported)
		{
			return ArchitectureType.SSE4_2;
		}
		if(Sse41.IsSupported)
		{
			return ArchitectureType.SSE4_1;
		}
		if(Sse3.IsSupported)
		{
			return ArchitectureType.SSE3;
		}
		if(Sse2.IsSupported)
		{
			return ArchitectureType.SSE2;
		}
		if(Sse.IsSupported)
		{
			return ArchitectureType.SSE;
		}
		//ARM-128/64 bit
		if(AdvSimd.IsSupported)
		{
			return ArchitectureType.NEON;
		}
		return ArchitectureType.None;
	}
	private static SIMDSupport GetSIMDWidth()
	{
		if(Vector512I.IsHardwareAccelerated)
		{
			return SIMDSupport.Vector512;
		}
		if(Vector256I.IsHardwareAccelerated)
		{
			return SIMDSupport.Vector256;
		}
		if(Vector128I.IsHardwareAccelerated)
		{
			return SIMDSupport.Vector128;
		}
		if(Vector64I.IsHardwareAccelerated)
		{
			return SIMDSupport.Vector64;
		}
		if(VectorI.IsHardwareAccelerated)
		{
			return SIMDSupport.Vector;
		}
		return SIMDSupport.None;
	}
	#endregion

	public static BaseVectorDepracated<T> operator +(BaseVectorDepracated<T> _lhs, BaseVectorDepracated<T> _rhs)
	{
		var _ret = default(BaseVectorDepracated<T>);
		Add(_lhs, _rhs, ref _ret);
		return _ret;
	}
	public static BaseVectorDepracated<T> operator -(BaseVectorDepracated<T> _lhs, BaseVectorDepracated<T> _rhs)
	{
		var _ret = default(BaseVectorDepracated<T>);
		Subtract(_lhs, _rhs, ref _ret);
		return _ret;
	}
	public static BaseVectorDepracated<T> operator *(BaseVectorDepracated<T> _lhs, BaseVectorDepracated<T> _rhs)
	{
		var _ret = default(BaseVectorDepracated<T>);
		Multiply(_lhs, _rhs, ref _ret);
		return _ret;
	}
	public static BaseVectorDepracated<T> operator /(BaseVectorDepracated<T> _lhs, BaseVectorDepracated<T> _rhs)
	{
		var _ret = default(BaseVectorDepracated<T>);
		Divide(_lhs, _rhs, ref _ret);
		return _ret;
	}

	static partial void Add(BaseVectorDepracated<T> _lhs, BaseVectorDepracated<T> _rhs, ref BaseVectorDepracated<T> _ret);
	static partial void Subtract(BaseVectorDepracated<T> _lhs, BaseVectorDepracated<T> _rhs, ref BaseVectorDepracated<T> _ret);
	static partial void Multiply(BaseVectorDepracated<T> _lhs, BaseVectorDepracated<T> _rhs, ref BaseVectorDepracated<T> _ret);
	static partial void Divide(BaseVectorDepracated<T> _lhs, BaseVectorDepracated<T> _rhs, ref BaseVectorDepracated<T> _ret);
}
/// <summary>
/// Allocating methods on the stack, provides a simple set of methods to perform arithmatic operations on generic type
/// <see cref="BaseVectorDepracated{T}"/>
/// </summary>
internal static class BaseVector
{
	public static BaseVectorDepracated<T> Add<T>(this BaseVectorDepracated<T> _this, BaseVectorDepracated<T> _other)
		where T : INumber<T>
	{
		return _this + _other;
	}
	public static BaseVectorDepracated<T> Subtract<T>(this BaseVectorDepracated<T> _this, BaseVectorDepracated<T> _other)
		where T : INumber<T>
	{
		return _this - _other;
	}
	public static BaseVectorDepracated<T> Multiply<T>(this BaseVectorDepracated<T> _this, BaseVectorDepracated<T> _other)
		where T : INumber<T>
	{
		return _this * _other;
	}
	public static BaseVectorDepracated<T> Divide<T>(this BaseVectorDepracated<T> _this, BaseVectorDepracated<T> _other)
		where T : INumber<T>
	{
		return _this / _other;
	}
}