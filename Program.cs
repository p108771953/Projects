using System;
using System.Collections.Generic;
using System.IO;

namespace W_B_randomGeneration
{
    class Program
    {
        static int sum;
        static int L1size = 4;
        static int L2size = 4;
        static int L3size = 4;
        static int x = 64 * L1size + L1size * L2size + L2size * L3size + L3size * 3 + L1size + L2size + L3size + 3;

        static void Main(string[] args)
        {
            double[] parameters;
            double mean;
            double sD;

            parameters = GenerateParameters();
            mean=CalculateMean(parameters);
            sD = CalculateSD(parameters, mean);
            for(int i=0; i<x; i++)
            {
                parameters[i] = (parameters[i] - mean) / sD;
            }
            StreamWriter writer = new StreamWriter("NN_Parameters.csv");
            for (int j = 0; j < L1size; j++)//writes weights 1st layer
            {
                for (int k = 0; k < 64; k++)
                {
                    writer.Write(parameters[j * 64 + k]);
                    writer.Write(", ");
                }
                writer.WriteLine();
            }
            sum = sum + L1size * 64;
            for (int j = 0; j < L1size; j++)//writes biases 1st layer
            {
                writer.Write(parameters[sum + j]);
                writer.WriteLine(", ");
            }
            sum = sum + L1size;
            for (int j = 0; j < L2size; j++)//writes weights 2nd layer
            {
                for (int k = 0; k <L1size; k++)
                {
                    writer.Write(parameters[sum+j * L1size + k]);
                    writer.Write(", ");
                }
                writer.WriteLine();
            }
            sum = sum + L1size * L2size;
            for (int j = 0; j < L2size; j++)//writes biases 2nd layer
            {
                writer.Write(parameters[sum + j]);
                writer.WriteLine(", ");
            }
            sum = sum + L2size;
            for (int j = 0; j < L3size; j++)//writes weights 3rd layer
            {
                for (int k = 0; k < L2size; k++)
                {
                    writer.Write(parameters[sum + j * L2size + k]);
                    writer.Write(", ");
                }
                writer.WriteLine();
            }
            sum = sum + L2size * L3size;
            for (int j = 0; j < L3size; j++)//writes biases 3rd layer
            {
                writer.Write(parameters[sum+ j]);
                writer.WriteLine(", ");
            }
            sum = sum + L3size;
            for (int j = 0; j < 3; j++)//writes weights 4th layer
            {
                for (int k = 0; k < L3size; k++)
                {
                    writer.Write(parameters[sum + j * L3size + k]);
                    writer.Write(", ");
                }
                writer.WriteLine();
            }
            sum = sum + 3*L3size;
            for (int j = 0; j < 3; j++)//writes biases 4th layer
            {
                writer.Write(parameters[sum + j]);
                writer.WriteLine(", ");
            }
            writer.Close();
        }
        static double[] GenerateParameters()
        {
            Random r = new Random();
            double[] p = new double[x];

            for(int i=0; i<x; i++)
            {
                p[i] = r.NextDouble();
            }

            return p;
        }
        static double CalculateMean(double[] parameters)
        {
            double mean=0;

            for(int i=0; i<parameters.Length; i++)
            {
                mean = mean + parameters[i];
            }
            mean = mean / parameters.Length;
            return mean;
        }
        static double CalculateSD(double[] parameters, double mean)
        {
            double SD = 0;

            for (int i = 0; i < parameters.Length; i++)
            {
                SD = SD + (parameters[i]-mean)*(parameters[i]-mean);
            }
            SD = SD / (parameters.Length-1);
            SD = Math.Sqrt(SD);
            return SD;
        }
    }
}
