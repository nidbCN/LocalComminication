using NetworkInfomation;

var obj = new NetCode("172.16.20.5",32131);
var code = obj.Encode();
Console.WriteLine(code);

var dec = NetCode.Decode(code);

Console.WriteLine($"ip:{dec.IpAddress}, port:{dec.Port}");

