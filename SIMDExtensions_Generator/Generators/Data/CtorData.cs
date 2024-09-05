using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace SIMDExtensions_Generator.Generators.Data;

internal sealed class CtorData
{
	public CtorData(string _genericType, SemanticModel _model, ObjectCreationExpressionSyntax _vectorCreation)
	{
		Model = _model;
		GenericType = _genericType;
		VectorCreation = _vectorCreation;
	}

	public string GenericType { get; }
	public SemanticModel Model { get; }
	public ObjectCreationExpressionSyntax VectorCreation { get; }

	public void Deconstruct(out string _genericType, out SemanticModel _model,
		out ObjectCreationExpressionSyntax _vectorCreation)
	{
		_model = Model;
		_genericType = GenericType;
		_vectorCreation = VectorCreation;
	}
}
