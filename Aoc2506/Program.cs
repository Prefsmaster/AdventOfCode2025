const string fileName = "input.txt";

//Part 1
var file = new StreamReader(fileName);
//line = file.ReadLine();
//var parts = line.Split(' ');
//Console.WriteLine($"{parts.Count()}");
var v1 = file.ReadLine().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => ulong.Parse(x)).ToList();
var v2 = file.ReadLine().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => ulong.Parse(x)).ToList();
var v3 = file.ReadLine().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => ulong.Parse(x)).ToList();
var v4 = file.ReadLine().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => ulong.Parse(x)).ToList();
var op = file.ReadLine().Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
var sum = 0UL;
for (var i = 0; i < op.Count; i++)
{
    if (op[i] == "+")
        sum += v1[i] + v2[i] + v3[i] + v4[i];
    if (op[i] == "*")
        sum += v1[i] * v2[i] * v3[i] * v4[i];
}
Console.WriteLine($"Part1: {sum}");

// Part 2
var lines = File.ReadAllLines(fileName);

var ops = lines.Length - 1;
var index = lines[ops].Length-1;
sum = 0UL;
do
{
    var end = index;
    // find operator.
    while (lines[ops][index] == ' ')
        index--;
    var opChar = lines[ops][index];

    // cut out value strings
    var parts = new string[ops];
    for (var i = 0; i < ops; i++)
    {
        parts[i] = lines[i].Substring(index, end - index + 1);
//        Console.WriteLine($"'{parts[i]}'");
    }
//    Console.WriteLine($"{opChar}");
    // now evaluate
    var subresult = opChar == '+' ? 0UL : 1UL;
    for (var p = 0; p < parts[0].Length; p++)
    {
        var parameter = 0;
        for (var l = 0; l < ops; l++)
        {
            if (parts[l][p] == ' ')
                continue;
            parameter = parameter*10+ (parts[l][p] - '0');
        }
//Console.WriteLine($"param: {parameter}");
        if (opChar == '+')
            subresult += (ulong)parameter;
        else
            subresult *= (ulong)parameter;
    }
    sum += subresult;
    index -= 2;
    // i is index of op
} while (index > 0);
Console.WriteLine($"Part2: {sum}");
