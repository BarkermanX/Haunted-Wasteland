// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

string strFileName = "TestData.txt";

Dictionary<string, Map> dctMaps = new Dictionary<string, Map>();
List<char> lstDirections = new List<char>();
bool boolDirectionReceived = false;

using (StreamReader sr = new StreamReader(strFileName))
{
    // Read and display lines from the file until the end of the file is reached
    while (!sr.EndOfStream)
    {
        string strLine = sr.ReadLine();

        if(string.IsNullOrEmpty(strLine))
        {
            boolDirectionReceived = true;
            continue;
        }


        if (!boolDirectionReceived)
        {
            foreach (char cDirection in strLine)
            {
                lstDirections.Add(cDirection);
            }

            continue;
        }

        // Define a regular expression pattern
        string pattern = @"(\w+) = \((\w+), (\w+)\)";

        // Use Regex.Match to find matches in the input
        Match match = Regex.Match(strLine, pattern);

        // Check if a match is found
        Map objMap = new Map(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value);

        dctMaps.Add(match.Groups[1].Value, objMap);
    }
}

bool bPartOne = false;

if (bPartOne)
{
    // Navigate the map to ZZZ
    int iMoveNumber = 0;
    int iDirectionNumber = 0;
    string strCurrentNode = "AAA";

    while (strCurrentNode != "ZZZ")
    {
        if (iDirectionNumber == lstDirections.Count)
        {
            iDirectionNumber = 0;
        }

        char cMove = lstDirections[iDirectionNumber];

        if (cMove == 'L')
        {
            strCurrentNode = dctMaps[strCurrentNode].Left;
        }
        else if (cMove == 'R')
        {
            strCurrentNode = dctMaps[strCurrentNode].Right;
        }

        iDirectionNumber++;
        iMoveNumber++;
    }


    Console.WriteLine("Number Moves :" + iMoveNumber);
}


//  Part 2
List<string> lstPaths = new List<string>();

foreach(string strMapKey in dctMaps.Keys)
{
    if (strMapKey.EndsWith('A'))
    {
        lstPaths.Add(strMapKey);
    }
}

double iMoveCount = 1;
int iDirectionCount = 0;
int iLoop = 1;

List<double> lstMovesToZ1 = new List<double>();
List<double> lstMovesToZ2 = new List<double>();
List<double> lstMovesToZ3 = new List<double>();
List<double> lstMovesToZ4 = new List<double>();
List<double> lstMovesToZ5 = new List<double>();
List<double> lstMovesToZ6 = new List<double>();


while (lstMovesToZ1.Count < 5 && lstMovesToZ2.Count < 5 && lstMovesToZ3.Count < 5 && lstMovesToZ4.Count < 5 && lstMovesToZ5.Count < 5)
{
    if (iDirectionCount == lstDirections.Count)
    {
        iDirectionCount = 0;
        iLoop++;
    }

    for (int iPath = 0; iPath < lstPaths.Count; iPath++)
    {
        char cMove = lstDirections[iDirectionCount];

        if (cMove == 'L')
        {
            lstPaths[iPath] = dctMaps[lstPaths[iPath]].Left;
        }
        else if (cMove == 'R')
        {
            lstPaths[iPath] = dctMaps[lstPaths[iPath]].Right;
        }

        if (Map.checkPathEndsZ(lstPaths[iPath]))
        {

            switch (iPath)
            {
                case 0:
                    lstMovesToZ1.Add(iMoveCount);
                    break;

                case 1:
                    lstMovesToZ2.Add(iMoveCount);

                    break;

                case 2:
                    lstMovesToZ3.Add(iMoveCount);

                    break;

                case 3:
                    lstMovesToZ4.Add(iMoveCount);

                    break;

                case 4:
                    lstMovesToZ5.Add(iMoveCount);

                    break;

                case 5:
                    lstMovesToZ6.Add(iMoveCount);

                    break;
            }
        }

    }

    if (iMoveCount % 1000 == 0)
    {
        Console.WriteLine("Moves; " + iMoveCount);
    }

    iMoveCount++;
    iDirectionCount++;
}

List<double> lstIncrement = new List<double>();


lstIncrement.Add(lstMovesToZ1[1] - lstMovesToZ1[0]);
lstIncrement.Add(lstMovesToZ2[1] - lstMovesToZ2[0]);
lstIncrement.Add(lstMovesToZ3[1] - lstMovesToZ3[0]);
lstIncrement.Add(lstMovesToZ4[1] - lstMovesToZ4[0]);
lstIncrement.Add(lstMovesToZ5[1] - lstMovesToZ5[0]);
lstIncrement.Add(lstMovesToZ6[1] - lstMovesToZ6[0]);

List<int> lstResults = new List<int>();
for(int iLoopA = 1; iLoopA < 6; iLoopA++)
{
    int iResult = 11610 + ((iLoopA - 1) * 11610);
    lstResults.Add(iResult);
}

Console.WriteLine("Here");

//11610+Y⋅11610 =19710+X⋅19710
//x = 19170+(n−1)⋅19170
//x = 19170+(n−1)⋅19170
//x = 19170+(n−1)⋅19170
//x = 19170+(n−1)⋅19170

long[] arrFirstHitZ = { (long)lstMovesToZ1.First(), (long)lstMovesToZ2.First(), (long)lstMovesToZ3.First(), (long)lstMovesToZ4.First(), (long)lstMovesToZ5.First(), (long)lstMovesToZ6.First() }; // Replace with your actual array of steps


// First time all nodes reach Z will be the lowest common multiple of all steps to their respective end
Console.WriteLine(arrFirstHitZ.Skip(1).Aggregate(arrFirstHitZ[0], (acc, val) => Map.LCM(acc, val)));
    


//int iLargestIncrement = (int)lstIncrement[5];
//bool bCommonMultiplierFound = false;


//while(!bCommonMultiplierFound)
//{
//    // z1 is smallest
//    double iZ1LastValue = lstMovesToZ1.Last() + lstIncrement[0];
//    lstMovesToZ1.Clear();
//    lstMovesToZ1.Add(iZ1LastValue);

//    if (lstMovesToZ2.Last() < lstMovesToZ1.Last())
//    {
//        double dNextZ2 = lstMovesToZ2.Last() + lstIncrement[1];
//        lstMovesToZ2.Clear();
//        lstMovesToZ2.Add(dNextZ2);
//    }

//    if (lstMovesToZ3.Last() < lstMovesToZ1.Last())
//    {
//        double dNextZ3 = lstMovesToZ3.Last() + lstIncrement[2];
//        lstMovesToZ3.Clear();
//        lstMovesToZ3.Add(dNextZ3);
//    }

//    if (lstMovesToZ4.Last() < lstMovesToZ1.Last())
//    {
//        double dNextZ4 = lstMovesToZ4.Last() + lstIncrement[3];
//        lstMovesToZ4.Clear();
//        lstMovesToZ4.Add(dNextZ4);
//    }

//    if (lstMovesToZ5.Last() < lstMovesToZ1.Last())
//    {
//        double dNextZ5 = lstMovesToZ5.Last() + lstIncrement[4];
//        lstMovesToZ5.Clear();
//        lstMovesToZ5.Add(dNextZ5);
//    }

//    if (lstMovesToZ6.Last() < lstMovesToZ1.Last())
//    {
//        double dNextZ6 = lstMovesToZ6.Last() + lstIncrement[5];
//        lstMovesToZ6.Clear();
//        lstMovesToZ6.Add(dNextZ6);
//    }

//    // Check if last z1 value is in the other lists
//    if (lstMovesToZ2.Contains(iZ1LastValue)
//        && lstMovesToZ3.Contains(iZ1LastValue)
//        && lstMovesToZ4.Contains(iZ1LastValue)
//        && lstMovesToZ5.Contains(iZ1LastValue)
//        && lstMovesToZ6.Contains(iZ1LastValue))
//    {

//        bCommonMultiplierFound = true;
//    }
//    Console.WriteLine(lstMovesToZ1.Last());
//}

//Console.WriteLine("Min Moves: " + lstMovesToZ1.Last());



public class Map
{
    public string Node;
    public string Left;
    public string Right;

    public Map(string strNode, string strLeft, string strRight)
    {
        Node = strNode;
        Left = strLeft;
        Right = strRight;
    }

    public static bool checkAllPathsEndInZ(List<string> lstPaths)
    {
        foreach (string strPath in lstPaths)
        {
            if (!strPath.EndsWith('Z'))
            {
                return false;
            }
        }

        return true;
    }

    public static bool checkPathEndsZ(string strPath)
    {
        if (!strPath.EndsWith('Z'))
        {
            return false;
        }
        return true;
    }

    public static double FindIntersectionPosition(double a1_1, double d1, double a1_2, double d2)
    {
        double n = 1;
        {
            if (a1_1 + (n - 1) * d1 == a1_2 + (n - 1) * d2)
            {
                return n;
            }
            n++;
        } while (true);

        return -1;  
    }

    static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static long LCM(long a, long b)
    {
        return (a / Map.GCD(a, b)) * b;
    }
}