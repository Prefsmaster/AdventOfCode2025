const string fileName = "test.txt";

var lines = File.ReadAllLines(fileName);
var ops = lines.Length-1;

//Part 1

var parametersLists = new List<ulong>[ops]; 
for (int row =0;row< ops; row++)
{
    parametersLists[row] = lines[row].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => ulong.Parse(x)).ToList();
}
var op = lines[ops].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();

var sum = 0UL;
for (var i = 0; i < op.Count; i++)
{
    var subresult = op[i] == "+" ? 0UL : 1UL;
    for (var par = 0; par < ops; par++)
    {
        if (op[i] == "+") subresult += parametersLists[par][i];
        if (op[i] == "*") subresult *= parametersLists[par][i];
    }
    sum += subresult;
}
Console.WriteLine($"Part1: {sum}");

// Part 2
var index = lines[ops].Length-1;
sum = 0UL;
do
{
    var end = index;

    // find operator and width of sub calculation.
    while (lines[ops][index] == ' ')
        index--;

    var opChar = lines[ops][index];
    var width = end - index + 1;

    // now evaluate
    var subresult = opChar == '+' ? 0UL : 1UL;
    for (var p = 0; p < width; p++)
    {
        var parameter = 0;
        for (var l = 0; l < ops; l++)
        {
            if (lines[l][index+p] != ' ')
                parameter = parameter*10+ (lines[l][index+p] - '0');
        }
        if (opChar == '+')
            subresult += (ulong)parameter;
        else
            subresult *= (ulong)parameter;
    }
    sum += subresult;
    index -= 2;
} while (index > 0);
Console.WriteLine($"Part2: {sum}");
