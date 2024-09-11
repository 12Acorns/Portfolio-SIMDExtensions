using SIMDExtensions_Generator.Generator.Types.Data;
using System.Collections.Generic;

namespace SIMDExtensions_Generator.Generator.Types.Known;

internal sealed class SIMDSupportGenerator : IGeneratorProvider
{
	private const string WIDTHENUMTEMPLATE =
		@$"
		namespace SIMDExtensions.Core.Intrinsics;

		internal enum {SIMDDataHolder.SIMDWidthsEnumName}
		{{

		";

	public int WriteIndex { get; } = -1;

	public string Generate()
	{
		return string.Concat(
				WIDTHENUMTEMPLATE,
				string.Join("\n", GenerateWidths()),
				"\n}");
	}
	private IEnumerable<string> GenerateWidths()
	{
		foreach(var _width in SIMDDataHolder.SIMDWidths)
		{
			yield return $"    {_width},";
		}
	}
}
