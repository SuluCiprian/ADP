using System;
using MPI;

namespace sum
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;
                int arraySize = 16;
                //mpiexec - n 8 - debug 3 SumArray.exe

                int chunkSize = arraySize / comm.Size;

                //if (comm.Rank == 0)
                //{
                //    int[] a = new int[arraySize];
                //    for (int i = 0; i < arraySize; i++)
                //    {
                //        a[i] = i;
                //    }

                //    int sum1 = Sum(chunkSize, a);
                //    comm.Send(sum1, 1, 1);
                //    Console.WriteLine("Partial sum is: " + sum1);

                //    int[] b = new int[chunkSize];
                //    for (int i = 1; i < comm.Size; i++)
                //    {
                //        int lower = i * chunkSize;
                //        int upper = lower + chunkSize;
                //        int k = 0;
                //        for (int j = lower; j < upper; j++)
                //        {
                //            b[k++] = a[j];
                //        }
                //        comm.Send(b, i, 0);
                //    }
                //    int sum3 = comm.Receive<int>(Communicator.anySource, 1);
                //    Console.WriteLine("The sum is:" + sum3);
                //} 
                //else
                //{
                //    int[] c = new int[chunkSize];
                //    int sum3 = comm.Receive<int>(comm.Rank - 1, 1);
                //    Console.WriteLine("sum3:" + sum3);
                //    c = comm.Receive<int[]>(0, 0);
                //    int sum2 = Sum(chunkSize, c);

                //    comm.Send(sum2 + sum3, (comm.Rank + 1) % comm.Size , 1);
                //}


                if (comm.Rank == 0)
                {
                    int[] a = new int[arraySize];
                    for (int i = 0; i < arraySize; i++)
                    {
                        a[i] = i;
                    }
                    int sum1 = Sum(chunkSize, a);
                    Console.WriteLine("Partial sum is: " + sum1);

                    int[] b = new int[chunkSize];
                    for (int i = 1; i < comm.Size; i++)
                    {
                        int lower = i * chunkSize;
                        int upper = lower + chunkSize;
                        int k = 0;
                        for (int j = lower; j < upper; j++)
                        {
                            b[k++] = a[j];
                        }
                        Console.WriteLine("Sending!");
                        comm.Send(b, i, 0);
                        Console.WriteLine("Sent!");
                    }

                }


                if (comm.Rank != 0)
                {
                    int[] c = new int[chunkSize];
                    Console.WriteLine("Rank: " + comm.Rank);
                    c = comm.Receive<int[]>(0, 0);
                    Console.WriteLine("Recived! on rank: " + comm.Rank);
                    int sum2 = Sum(chunkSize, c);
                    comm.Send(sum2, 0, 0);

                }

                if (comm.Rank == 0)
                {
                    int totalSum = 0;

                    for (int i = 1; i < comm.Size; i++)
                    {
                        Console.WriteLine("Recive master");
                        int sum3 = comm.Receive<int>(i, 0);
                        Console.WriteLine("Recived on master");
                        totalSum += sum3;
                    }
                    Console.WriteLine("Total sum is: " + totalSum);
                }


            }
        }

        static int Sum(int length, int[] array)
        {
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += array[i];
            }

            return sum;
        }
    }
}
