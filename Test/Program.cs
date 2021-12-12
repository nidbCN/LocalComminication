using NetworkInfomation;

var obj = new NetCode("192.168.5.3",32531);
var code = obj.Encode();
Console.WriteLine(code);

var dec = NetCode.Decode(code);

Console.WriteLine($"ip:{dec.IpAddress}, port:{dec.Port}");

