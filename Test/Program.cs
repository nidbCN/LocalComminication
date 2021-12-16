using NetworkInfomation;

//var obj = new NetCode("10.101.199.299",34251);
//var code = obj.Encode();

var code = "E1  Ua";
Console.WriteLine(code);

var dec = NetCode.Decode(code);

Console.WriteLine($"ip:{dec.IpAddress}, port:{dec.Port}");
