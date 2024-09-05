using SIMDExtensions_Core.Vectors;

Console.WriteLine("Hello, World!");

var _testVec = new BaseVector<double>();
var _testVec1 = new BaseVector<float>();

var _vec2 = new BaseVector<int>();
var _vec3 = new BaseVector<int>();

var _vec = _vec2 + _vec3;

Console.ReadLine();
