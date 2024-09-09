namespace SIMDExtensions_Generator.Generator.Types.Known;

internal sealed class SIMDSupportGenerator : IGeneratorProvider
{
	public int WriteIndex { get; } = -1;

	public string Generate()
	{
		return
			"""
			namespace SIMDExtensions.Core.Intrinsics;

			internal enum SIMDSupport
			{
				None,
				Vector,
				Vector64,
				Vector128,
				Vector256,
				Vector512,
			}
			""";
	}
}
