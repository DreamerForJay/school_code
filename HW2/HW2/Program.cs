using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // 讀取距離矩陣檔案
        string filePath = "distance_matrix.csv"; // 替換為你的檔案路徑
        int[,] distanceMatrix = ReadDistanceMatrix(filePath);

        // 輸出檢查
        Console.WriteLine("Distance Matrix:");
        for (int i = 0; i < distanceMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < distanceMatrix.GetLength(1); j++)
            {
                Console.Write(distanceMatrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        // 解決 TSP 問題
        SolveTSP(distanceMatrix);
    }

    static int[,] ReadDistanceMatrix(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        int size = lines.Length;
        int[,] matrix = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            var values = lines[i].Split(',').Select(int.Parse).ToArray();
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = values[j];
            }
        }

        return matrix;
    }
}
