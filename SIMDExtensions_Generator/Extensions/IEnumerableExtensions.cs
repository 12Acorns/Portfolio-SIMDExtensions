using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace SIMDExtensions_Generator.Extensions;

internal static class IEnumerableExtensions
{
	public static IEnumerable<(TData _data, int _index)> Indexed<TData>(this IEnumerable<TData> _enumerable) =>
		_enumerable.Select((_data, _index) => (_data, _index));
	public static IEnumerable<TThis> Enumerate<TThis, TReturn>(this IEnumerable<TThis> _enumerable)
	{
		foreach(var _enumeratorItem in _enumerable)
		{
			yield return _enumeratorItem;
		}
	}
}
