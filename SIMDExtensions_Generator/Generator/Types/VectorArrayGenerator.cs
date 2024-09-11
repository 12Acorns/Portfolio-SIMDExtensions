namespace SIMDExtensions_Generator.Generator.Types;

internal class VectorArrayGenerator : IGeneratorProvider
{
	private const string USINGNAMESPACES =
		"""
		using global::System.Numerics;
		using global::System;
		""";
	private const string NAMESPACE = "namespace SIMDExtensions.Core.Intrinsics;";
	private const string CLASS =
@$"
{USINGNAMESPACES}
{NAMESPACE}

public readonly ref struct VectorArray<T> where T : struct, INumber<T>
{{

}}";

	public int WriteIndex { get; } = -1;

	public string Generate()
	{
		return CLASS;
	}
}
