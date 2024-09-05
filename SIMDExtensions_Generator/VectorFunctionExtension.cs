using System;
using System.Collections.Generic;
using System.Text;

namespace SIMDExtensions_Generator;
internal static class VectorFunctionExtension
{
	private static readonly Dictionary<VectorFunction, string> mapping = new()
	{
		{ VectorFunction.Add, "Add" },
		{ VectorFunction.Subtract, "Subtract" },
		{ VectorFunction.Multiply, "Multiply" },
		{ VectorFunction.Divide, "Divide" },
	};

	public static string ToFuncName(this VectorFunction _func)
	{
		if(!mapping.TryGetValue(_func, out string _value))
		{
			throw new NotSupportedException($"{_func} is not yet a supported {typeof(VectorFunction)} function yet");
		}
		return _value;
	}
}
