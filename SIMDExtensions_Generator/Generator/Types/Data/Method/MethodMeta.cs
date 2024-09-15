using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIMDExtensions_Generator.Generator.Types.Data.Method;

internal readonly struct MethodMeta
{
    public MethodMeta(string _method, MethodType _type, Arg _returnType, params Arg[] _args)
    {
        Name = _method;
        ReturnType = _returnType;
        Type = _type;

        var _paramNamesSet = new HashSet<string>(_args.Select(x => x.ArgName).Distinct());
        for(int i = 0; i < _args.Length; i++)
		{
			if(i <= 0 || !_paramNamesSet.Contains(_args[i].ArgName))
			{
				continue;
			}
			_args[i] = new Arg(_args[i].TypeName, _args[i].ArgName + i);
		}
		Args = _args;
    }

    public MethodType Type { get; }
    public Arg ReturnType { get; }
    public string Name { get; }
    public Arg[] Args { get; }

	public string ArgsToMethodParams() =>
        string.Join(", ", Args.Select(x => $"{x.TypeName} {x.ArgName}"));
}
