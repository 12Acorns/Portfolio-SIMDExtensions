using System.Text;

namespace SIMDExtensions_Generator.Generators.Types.Meta;

internal sealed partial class MetaGenerator : IVectorGenerator
{
    private MetaCallerGenerator? callerMeta;
    private MetaSymbolGenerator? symbolMeta;

    public MetaGenerator WithSymbolMeta(MetaSymbolGenerator _meta)
    {
        symbolMeta = _meta;
		return this;
    }
    public MetaGenerator WithCallerMeta(MetaCallerGenerator _meta)
    {
		callerMeta = _meta;
        return this;
	}

	public string Generate()
    {
        return new StringBuilder()
            .AppendLine(callerMeta?.Generate())
            .AppendLine(symbolMeta?.Generate())
            .ToString();
    }
}
