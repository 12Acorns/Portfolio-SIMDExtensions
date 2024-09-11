using SIMDExtensions_Generator.Generator.Types.Data;
using System.Collections.Generic;

namespace SIMDExtensions_Generator.Generator.Types.Known;

internal sealed class ArchitectureTypeGenerator : IGeneratorProvider
{
	private const string PLATFORMENUMTEMPLATE =
		@$"
		namespace SIMDExtensions.Core.Intrinsics;
		
		internal enum {SIMDDataHolder.SIMDArchitectureEnumName}
		{{

		";

	public int WriteIndex { get; } = -1;

	public string Generate()
	{
		return string.Concat(
				PLATFORMENUMTEMPLATE,
				string.Join("\n", GeneratePlatforms()),
				"\n}");
	}
	private IEnumerable<string> GeneratePlatforms()
	{
		foreach(var _platform in SIMDDataHolder.SupportedArchitectures)
		{
			yield return $"    {_platform},";
		}
	}
}
