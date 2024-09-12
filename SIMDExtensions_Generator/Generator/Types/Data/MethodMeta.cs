using System.Collections.Generic;
using System.Text;
using System;

namespace SIMDExtensions_Generator.Generator.Types.Data;

internal readonly struct MethodMeta
{
	public MethodMeta(string _name, MethodType _type, string _returnType, params string[] _args)
	{
		ReturnType = _returnType;
		Args = _args;
		Name = _name;
		Type = _type;
	}

	public MethodType Type { get; }
	public string ReturnType { get; }
	public string Name { get; }
	public string[] Args { get; }

	public static string GetNthParamName(string[] _args, int _index)
	{
		if(_index is < 0 || _index >= _args.Length)
		{
			throw new IndexOutOfRangeException($"{nameof(_index)} is out of the bounds of {nameof(_args)}");
		}

		var _repeatNames = new Dictionary<string, int>();
		int _traverseIndex = 0;
		foreach(var _arg in _args)
		{
			var _name = _arg;
			if(_repeatNames.TryGetValue(_name, out int _count))
			{
				_repeatNames[_name] = ++_count;
			}
			else
			{
				_repeatNames.Add(_name, 0);
			}

			if(_traverseIndex != _index)
			{
				continue;
			}
			_name += _repeatNames[_name];
			_name = $"_{_name}";
			return _name;
		}
		throw new Exception("Could not find type at index");
	}
	public static string ArgsToMethodParams(IEnumerable<string> _args)
	{
		var _repeatNames = new Dictionary<string, int>();

		var _builder = new StringBuilder();
		foreach(var _arg in _args)
		{
			var _name = _arg;
			var _nameSpan = _name.AsSpan();
			var _genericStart = _nameSpan.IndexOf('<');

			if(_genericStart > -1)
			{
				_nameSpan = _nameSpan.Slice(0, _genericStart);
			}

			string _typeNameInMethod = _nameSpan.ToString();
			if(_repeatNames.TryGetValue(_name, out int _count))
			{
				_repeatNames[_name] = ++_count;
				_typeNameInMethod += _count;
			}
			else
			{
				_repeatNames.Add(_name, 0);
			}

			_builder.AppendFormat("{0} _{1}, ", _name, _typeNameInMethod);
		}
		return _builder
			.Remove(_builder.Length - 2, 1)
			.ToString();
	}
}
