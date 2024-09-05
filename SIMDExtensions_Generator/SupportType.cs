using System.Collections.Generic;

namespace SIMDExtensions_Generator;

public sealed class SupportType
{
	public SupportType(string[] _supportedTypes)
	{
		supportedTypes = new(_supportedTypes);
	}
	private SupportType() { }

	private readonly HashSet<string> supportedTypes;

	public bool IsSupported(string _comparisonType)
	{
		return supportedTypes.Contains(_comparisonType);
	}
}