using SIMDExtensions_Generator.Generators.Data;
using SIMDExtensions_Generator.Extensions;
using System.Collections.Generic;
using System.Text;

namespace SIMDExtensions_Generator.Generators.Types;

internal sealed class CtorGenerator : IVectorGenerator
{
	private static readonly (string _type, string _name)[] vectorTypeAndName =
	[
		("Vector512<T>", "vector512"),
		("Vector256<T>", "vector256"),
		("Vector128<T>", "vector128"),
		("Vector64<T>", "vector64"),
		("Vector<T>", "vector"),
	];

	public CtorGenerator(IEnumerable<CtorData> _ctorData) =>
		ctorData = _ctorData;

	private readonly IEnumerable<CtorData> ctorData;

    public string Generate()
	{
		StringBuilder _ctorBuilder = new();
        return _ctorBuilder.AppendIEnumerable(ctorData, BuildCtor).ToString();
	}

	private string BuildCtor()
	{
		StringBuilder _ctorBuilder = new();
		foreach(var (_type, _name) in vectorTypeAndName)
		{
			_ctorBuilder.AppendLine()
				.AppendFormat("""
						internal readonly {0}? {1};
						public BaseVector({0} _vector)
						{{
					""", _type, _name);

			_ctorBuilder.AppendLine("	}");
		}
		_ctorBuilder
			.AppendLine()
			.Append("""
				public BaseVector(global::SIMDExtensions_Core.Vectors.BaseVector<T> _vector)
				{
			""");
		foreach(var (_type, _name) in vectorTypeAndName)
		{
			_ctorBuilder
				.AppendLine()
				.AppendFormat("""
						if(_vector.vectorType == typeof({0}))
						{{
							{1} = _vector.{1};
						}}
				""", _type, _name);

			_ctorBuilder = VectorMapping(_ctorBuilder, (_type, _name), ctorData);
		}
		_ctorBuilder.Append("\n    }");

		return _ctorBuilder.ToString();
	}
	private static StringBuilder VectorMapping(StringBuilder _builder, (string _type, string _name) _vectorMapping, 
		IEnumerable<CtorData> _data)
	{
		foreach(var (_generic, _, _) in _data)
		{
			_builder
				.AppendLine()
				.AppendFormat(
					"""
							if(typeof(T) is INumber<{0}>)
							{{
								vectorType = typeof({1});
								{2} = _vector;
							}}
					""", _generic, _vectorMapping._type.Replace("<T>", $"<{_generic}>"), _vectorMapping._name)
				.AppendLine();
		}
		return _builder;
	}
}
