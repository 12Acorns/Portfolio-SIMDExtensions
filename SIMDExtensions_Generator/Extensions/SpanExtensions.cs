using System;
using System.Collections.Generic;

namespace SIMDExtensions_Generator.Extensions;

internal static class SpanExtensions
{
	public static IEnumerable<(TData, int _index)> Indexed<TData>(this ReadOnlySpan<TData> _span)
	{
		for(int i = 0; i < _span.Length; i++)
		{
			yield return (_span[i], i);
		}
	}
}
