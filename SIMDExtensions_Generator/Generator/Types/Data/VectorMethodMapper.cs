using System.Collections.Generic;

namespace SIMDExtensions_Generator.Generator.Types.Data;

internal static class VectorMethodMapper
{
	private static readonly Dictionary<VectorMethod, string> nameToMethodNameMapper = new()
	{
		{ VectorMethod.Add, "Add" },
		{ VectorMethod.Subtract, "Subtract" },
		{ VectorMethod.Multiply, "Multiply" },
		{ VectorMethod.Divide, "Divide" },
	};

	public static bool TryGetMethodName(VectorMethod _method, out string _name)
	{
		return nameToMethodNameMapper.TryGetValue(_method, out _name);
	}
}
