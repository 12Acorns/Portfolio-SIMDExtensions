using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace SIMDExtensions_Generator;

internal partial class VectorGenerator
{
	private sealed record ModelData
	{
		public ModelData(INamedTypeSymbol _symbol, SemanticModel _model,
			ObjectCreationExpressionSyntax _vectorCreation)
		{
			Symbol = _symbol;
			Model = _model;
			VectorCreation = _vectorCreation;
		}

		/// <summary>
		/// The genetic T type, IE T is int
		/// </summary>
		public INamedTypeSymbol Symbol { get; }
		public SemanticModel Model { get; }
		public ObjectCreationExpressionSyntax VectorCreation { get; }

		internal void Deconstruct(out INamedTypeSymbol _structSymbol, out SemanticModel _model,
			out ObjectCreationExpressionSyntax _vectorCreation)
		{
			_structSymbol = Symbol;
			_model = Model;
			_vectorCreation = VectorCreation;
		}
	} 
}