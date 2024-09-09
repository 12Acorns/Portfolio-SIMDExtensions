using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System;
using SIMDExtensions_Generator.Generator.Types;
using SIMDExtensions_Generator.Generator.Types.Known;

namespace SIMDExtensions_Generator;

[Generator]
internal partial class VectorGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext _context)
	{
		var _provider = _context.SyntaxProvider.CreateSyntaxProvider(
			static (_node, _) => _node is ObjectCreationExpressionSyntax _invocation,
			static (_ctx, _) => AnalyzeCreation(_ctx))
			.Where(x => x is not null);

		var _compilation = _context.CompilationProvider
			.Combine(_provider.Collect());

		_context.RegisterSourceOutput(_compilation, Execute);
	}
	private void Execute(SourceProductionContext _prodContext,
		(Compilation _left, ImmutableArray<ObjectCreationExpressionSyntax> _right) _tup)
	{
//#if DEBUG
//		if(!Debugger.IsAttached)
//		{
//			Debugger.Launch();
//		}
//#endif

		var _code = new BaseClassGenerator().Generate();

		_prodContext.AddSource("ArchitectureType.g.cs", new ArchitectureTypeGenerator().Generate());
		_prodContext.AddSource("SIMDSupport.g.cs", new SIMDSupportGenerator().Generate());
		_prodContext.AddSource("BaseVector.g.cs", _code);
	}

	private static ObjectCreationExpressionSyntax? AnalyzeCreation(GeneratorSyntaxContext _context)
	{
		// Ensure the node is an ObjectCreationExpressionSyntax
		if(_context.Node is not ObjectCreationExpressionSyntax _creation)
			return null;

		var semanticModel = _context.SemanticModel;
		var typeInfo = semanticModel.GetTypeInfo(_creation);

		// Check if the type being instantiated is a generic struct named "BaseVector"
		if(typeInfo.Type is not INamedTypeSymbol _structSymbol || _structSymbol.TypeKind != TypeKind.Struct)
		{
			return null;
		}
		return _structSymbol.Name switch
		{
			"BaseVector" when _structSymbol.IsGenericType => _creation,
			_ => null
		};
	}
}