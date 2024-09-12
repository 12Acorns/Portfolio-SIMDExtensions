namespace SIMDExtensions_Generator.Generator.Types.BaseVector;

internal sealed partial class BaseVectorGenerator
{
	private sealed class VectorOperationGenerator : IGeneratorProvider
	{
		public int WriteIndex { get; } = -1;

		public string Generate()
		{
			return "return default;";
		}
	}
}