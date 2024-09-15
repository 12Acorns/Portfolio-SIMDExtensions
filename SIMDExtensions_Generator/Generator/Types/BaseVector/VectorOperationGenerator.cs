using SIMDExtensions_Generator.Generator.Types.Data;

namespace SIMDExtensions_Generator.Generator.Types.BaseVector;

internal sealed partial class BaseVectorGenerator
{
	private sealed class VectorOperationGenerator : IGeneratorProvider
	{
		public VectorOperationGenerator(VectorMethod _method)
		{
			method = _method;
		}
		private readonly VectorMethod method;

		public int WriteIndex { get; } = -1;

		public string Generate()
		{
			return "return default;";
		}
	}
}