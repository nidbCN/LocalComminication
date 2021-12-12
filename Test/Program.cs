using NetworkInfomation;

var obj = new NetCode("192.168.20.246",36666);
var code = obj.Encode();
Console.WriteLine(code);

var dec = NetCode.Decode(code);

Console.WriteLine($"ip:{dec.IpAddress}, port:{dec.Port}");
