namespace SIMDExtensions_Generator.Generator.Types.Known;

internal sealed class ArchitectureTypeGenerator : IGeneratorProvider
{
	public int WriteIndex { get; } = -1;

	public string Generate()
	{
		return
			"""
			namespace SIMDExtensions.Core.Intrinsics;

			internal enum ArchitectureType
			{
				None,
				Generic,
				SSE,
				SSE2,
				SSE3,
				SSSE3,
				SSE4_1,
				SSE4_2,
				AVX,
				AVX2,
				AVX512,
				NEON
			}
			""";
	}
}
