using SIMDExtensions_Generator.Generators.Data;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SIMDExtensions_Generator.Generators.Types.Meta;

internal sealed partial class MetaGenerator
{
	public sealed class MetaCallerGenerator : IVectorGenerator
    {
		public MetaCallerGenerator(IEnumerable<CtorData> _ctorData) =>
            ctorData = _ctorData;
        private IEnumerable<CtorData> ctorData { get; }

        public string Generate() => string.Concat(BuildCallerMeta(ctorData));

        private static IEnumerable<string> BuildCallerMeta(IEnumerable<CtorData> _ctorData)
        {
			const string _METAINFOTEMPLATE = """
		        // Auto-generated code
		        // The struct 'BaseVector' was instantiated by '{0}' in '{1}'
		        // with the generic type argument: '{2}'

		        """;

            foreach (var (_genericType, _model, _vectorCreation) in _ctorData)
            {
                var containingSymbol = _model.GetEnclosingSymbol(_vectorCreation.SpanStart);
                var containingMethod = containingSymbol as IMethodSymbol;
                var containingType = containingSymbol as ITypeSymbol;

                string _callerName = containingMethod?.Name ?? "<unknown method>";
                string _callerContainingType = containingType?.Name ?? "<unknown type>";

                yield return string.Format(_METAINFOTEMPLATE,
                    _callerName, _callerContainingType,
                    _genericType);
            }
        }
    }
}
