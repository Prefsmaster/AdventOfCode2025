// AoC 2025 Day 1: Rotating Arrows

var rotations = File.ReadAllLines(@"input1.txt");
var arrow = 50L;
var atzeros = 0L;
var crossings = 0L;

foreach (var rotation in rotations)
{
    var steps = long.Parse(rotation[1..]) ;
    // add 0-crossings for full rotations
    crossings += steps / 100;
    steps *= rotation[0] == 'R' ? 1 : -1;

    // save old position and update arrow position
    var oldarrow = arrow;
    arrow = (arrow + steps)%100;
    // keep in range 0..99
    if (arrow < 0) arrow += 100;

    if (arrow == 0)
        atzeros++;
    else
        // when not landed on 0 and started from 0, and when we end up higher after a left,
        // or lower after a right, the partial rotation crossed zero and must be counted!
        if ((oldarrow != 0) && (steps < 0 && arrow > oldarrow) || (steps > 0 && arrow < oldarrow)) 
            crossings++;   
}
Console.WriteLine($"Password1: {atzeros}");
Console.WriteLine($"Password2: {atzeros+crossings}");
