// See https://aka.ms/new-console-template for more information
using AtlasianPrep.RateLimiter;

Console.WriteLine("Hello, World!");

var test = new RateLimtter(TimeSpan.FromSeconds(10), 1);
var result = test.IsUserAllowed("123");
var result2 = test.IsUserAllowed("123");
var result3 = test.IsUserAllowed("123");

Console.WriteLine($"the first answer is {result}");
Console.WriteLine($"the second answer is {result2}");
Console.WriteLine($"the thirs answer is {result3}");