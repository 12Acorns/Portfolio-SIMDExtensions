using System.Collections.Generic;

namespace SIMDExtensions_Generator.Generator.Types.Data;

internal static class SIMDDataHolder
{
	private static readonly Dictionary<string, string> widthToNameMapper = new()
	{
		{ "Vector512", "vector512" },
		{ "Vector256", "vector256" },
		{ "Vector128", "vector128" },
		{ "Vector64", "vector64" },
		{ "Vector", "vector" },
	};

	public static readonly IEnumerable<(string _type, string _name)> VectorTypeAndNames =
	[
		("Vector512<T>", "vector512"),
		("Vector256<T>", "vector256"),
		("Vector128<T>", "vector128"),
		("Vector64<T>", "vector64"),
		("Vector<T>", "vector"),
	];
	public static readonly IEnumerable<string> SupportedArchitectures =
	[
		"None",
		"Generic",
		"SSE",
		"SSE2",
		"SSE3",
		"SSSE3",
		"SSE4_1",
		"SSE4_2",
		"AVX",
		"AVX2",
		"AVX512",
		"NEON"
	];
	public static readonly IEnumerable<string> SIMDWidths =
	[
		"None",
		"Vector",
		"Vector64",
		"Vector128",
		"Vector256",
		"Vector512"
	];

	public const string SIMDWidthsEnumName = "SIMDSupport";
	public const string SIMDArchitectureEnumName = "ArchitectureType";

	public static bool TryEnumToMember(string _enumMember, out string _name)
	{
		return widthToNameMapper.TryGetValue(_enumMember, out _name);
	}
}
