using System;
using System.Collections.Generic;
using System.Text;
using MPI;

namespace RoyFloyd
{
    class Program
    {
        private static int INF = 99999;

        static void Main(string[] args)
        {
            int[,] graph = {
            { 0,   5,  INF, 10 },
            { INF, 0,   3, INF },
            { INF, INF, 0,   1 },
            { INF, INF, INF, 0 }};

            using (new MPI.Environment(ref args))
            {
                int ntasks, rank;
                Intracommunicator comm = Communicator.world;
                rank = comm.Rank;
                ntasks = comm.Size;

                RoyFloyd(graph, 4, rank, ntasks, comm);
            }
        }

        public static void RoyFloyd(int[,] graph, int verticesCount, int rank, int ntasks, Intracommunicator comm)
        {
            int[,] distance = new int[verticesCount, verticesCount];


            for (int i = rank; i < verticesCount; i += ntasks)
                for (int j = 0; j < verticesCount; ++j)
                    distance[i, j] = graph[i, j];

            for (int k = 0; k < verticesCount; ++k)
            {
                for (int i = rank; i < verticesCount; i += ntasks)
                {
                    for (int j = 0; j < verticesCount; ++j) 
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                            distance[i, j] = distance[i, k] + distance[k, j];
                    }
                }
            }

            comm.Gather<int[,]>(distance, 0);
            Print(distance, verticesCount, rank);
        }

        private static void Print(int[,] distance, int verticesCount, int rank)
        {
            Console.WriteLine("Shortest distances between every pair of vertices:");

            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = 0; j < verticesCount; ++j)
                {
                    if (rank == 0)
                    {
                        if (distance[i, j] == INF)
                            Console.Write("INF");
                        else
                            Console.Write(distance[i, j]);
                    }
                }
                Console.WriteLine();
                //
            }
        }
    }
}
