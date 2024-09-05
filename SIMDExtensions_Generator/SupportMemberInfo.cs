using System;

namespace SIMDExtensions_Generator;

internal readonly record struct SupportMemberInfo
{
	public SupportMemberInfo(string _vectorType, ReturnInfo[] _returnTypeIfTIs,
		SupportType _supportedTypes)
	{

		VectorType = _vectorType;
		ReturnTypeIfTIs = _returnTypeIfTIs ?? [];
		SupportedTypes = _supportedTypes;
	}

	public string VectorType { get; }
	public ReturnInfo[] ReturnTypeIfTIs { get; }
	public SupportType SupportedTypes { get; }
}
