using System.Collections.Generic;
using System.Text;
using System;

namespace SIMDExtensions_Generator.Extensions;
internal static class StringBuilderExtensions
{
	public static StringBuilder AppendIEnumerable<T>(this StringBuilder _builder, IEnumerable<T> _enumerator,
		Func<T, string> _delegate)
	{
		foreach(var _data in _enumerator)
		{
			_builder.Append(_delegate(_data));
		}
		return _builder;
	}
	public static StringBuilder AppendIEnumerable<T>(this StringBuilder _builder, IEnumerable<T> _enumerator,
		Func<string> _delegate)
	{
		foreach(var _ in _enumerator)
		{
			_builder.Append(_delegate);
		}
		return _builder;
	}
}
