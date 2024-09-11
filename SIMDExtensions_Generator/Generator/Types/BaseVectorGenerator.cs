using SIMDExtensions_Generator.Generator.Types.Known;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text;
using System;

namespace SIMDExtensions_Generator.Generator.Types;

internal sealed partial class BaseVectorGenerator : IGeneratorProvider
{
	#region Struct construction
	private const string USINGNAMESPACES =
		"""
		using Vector512I = global::System.Runtime.Intrinsics.Vector512;
		using Vector256I = global::System.Runtime.Intrinsics.Vector256;
		using Vector128I = global::System.Runtime.Intrinsics.Vector128;
		using Vector64I = global::System.Runtime.Intrinsics.Vector64;
		using VectorI = global::System.Numerics.Vector;
		using global::System.Runtime.CompilerServices;
		using global::System.Runtime.InteropServices;
		using global::System.Runtime.Intrinsics.X86;
		using global::System.Runtime.Intrinsics.Arm;
		using global::System.Runtime.Intrinsics;
		using global::System.Numerics;
		using global::System;
		""";
	private const string NAMESPACE ="namespace SIMDExtensions.Core.Intrinsics;";
	private const string STATICINIT =
		"""
			#region Init static
			private static readonly ArchitectureType targetPlatform;
			private static readonly SIMDSupport targetBitWidth;
		
			static BaseVector()
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
		""";
	private const string VECTORINFO =
	"""
		/// <summary>
		/// Returns the Count (Size) of the supported vector type. -1 is returned is no vector is supported.
		/// </summary>
		public static int Count 
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return targetBitWidth switch
				{
					SIMDSupport.Vector512 => Vector512<T>.Count,
					SIMDSupport.Vector256 => Vector256<T>.Count,
					SIMDSupport.Vector128 => Vector128<T>.Count,
					SIMDSupport.Vector64 => Vector64<T>.Count,
					SIMDSupport.Vector => Vector<T>.Count,
					_ => -1
				};
			}
		}
		/// <summary>
		/// Returns whether <see cref="T"/> is a supported type for SIMD acceleration
		/// </summary>
		public static bool IsSupported
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return targetBitWidth switch
				{
					SIMDSupport.Vector512 => Vector512<T>.IsSupported,
					SIMDSupport.Vector256 => Vector256<T>.IsSupported,
					SIMDSupport.Vector128 => Vector128<T>.IsSupported,
					SIMDSupport.Vector64 => Vector64<T>.IsSupported,
					SIMDSupport.Vector => Vector<T>.IsSupported,
					_ => false
				};
			}
		}
	""";
	private const string STRUCTXMLDOCUMENTING =
		"""
		/// <summary>
		/// Below are members that are wrapped by <see cref="BaseVector{T}"/>
		/// <list type="bullet">
		///		<item>
		///			<term><see cref="Vector{T}"/></term>
		///		</item>
		///		<item>
		///			<term><see cref="Vector64{T}"/></term>
		///		</item>
		///		<item>
		/// 		<term><see cref="Vector128{T}"/></term>
		/// 	</item>
		/// 	<item>
		/// 		<term><see cref="Vector256{T}/></term>
		/// 	</item>
		/// 	<item>
		/// 		<term><see cref="Vector512{T}/></term>
		/// 	</item>
		/// </list>
		/// <para></para>
		/// <b>
		/// Do not recommend to use as is. Instead use: <see cref="VectorArray{T}"/> for handling unkown length elements,
		/// and or for easily handling data that may exceed a wrapped vectors <see cref="BaseVector{T}.Count"/>
		/// </b>
		/// </summary>
		""";
	private const string BASECLASS =
$@"
{USINGNAMESPACES}
		
{NAMESPACE}

[CompilerGenerated]
[StructLayout(LayoutKind.Sequential)]
{STRUCTXMLDOCUMENTING}
public readonly ref partial struct BaseVector<T> where T : struct, INumber<T>
{{
{VECTORINFO}
{STATICINIT}
	
}}";
	#endregion
	public BaseVectorGenerator()
	{
		Ctor = new CtorGenerator();
	}

	public int WriteIndex { get; } = GetWriteIndex(BASECLASS, GetTotalLines(BASECLASS.AsSpan()), '\t');

	private readonly CtorGenerator Ctor;

	public string Generate()
	{
		return 
			new StringBuilder(BASECLASS)
				.Insert(WriteIndex, Ctor.Generate())
			.ToString();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int GetWriteIndex(string _input, int _line, char _targetEndChar)
	{
		var _returnIndex = 0;
		foreach(var _lineInfo in GetLines(_input))
		{
			if(_lineInfo._line != _line)
			{
				continue;
			}
			int i = _lineInfo._index;
			if(i <= 0)
			{
				break;
			}
			var _previousChar = _input[i - 1];
			while(_previousChar != '\n')
			{
				_previousChar = _input[--i - 1];
			}
			var _currentChar = _input[i];
			while(_currentChar != '\n')
			{
				if(_currentChar == _targetEndChar)
				{
					_returnIndex = i;
				}
				_currentChar = _input[++i];
			}
			return _returnIndex;
		}
		return -1;
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int GetTotalLines(ReadOnlySpan<char> _input)
	{
		int _lines = 0;
		for(int i = 0; i < _input.Length; i++)
		{
			if(_input[i] is not '\n')
			{
				continue;
			}
			_lines++;
		}
		return _lines;
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static IEnumerable<(int _line, int _index)> GetLines(string _input)
	{
		int _lines = 0;
		for(int i = 0; i < _input.Length; i++)
		{
			if(_input[i] is not '\n')
			{
				continue;
			}
			yield return (++_lines, i);
		}
	}
}
