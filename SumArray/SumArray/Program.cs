using System;
using MPI;

namespace sum
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int arraySize = 18;
            int[] a = new int[arraySize];
            
            int sum = 0;

            for (int i = 0; i < arraySize; i++)
            {
                a[i] = i;
            }
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;
                int chunksize = arraySize / comm.Size;


                if (comm.Rank == 0)
                {
                    for (int i = 0; i < chunksize; i++)
                    {
                        sum += a[i];
                    }
                    comm.Send(sum, 1, 0);
                    sum = comm.Receive<int>(Communicator.anySource, 0);
                    Console.WriteLine("Rank " + comm.Rank + " recived sum: " + sum);
                }
                else
                {
                    sum = comm.Receive<int>(comm.Rank - 1, 0);
                    Console.WriteLine("Rank " + comm.Rank + " recived sum: " + sum);
                    int lower = comm.Rank * chunksize;
                    int upper = lower + chunksize;
                    if(comm.Rank == comm.Size - 1)
                    {
                        upper = lower + chunksize + arraySize % comm.Size;
                    }
                    for (int i = lower; i < upper; i++)
                    {
                        sum += a[i];
                    }
                    comm.Send(sum, (comm.Rank + 1) % comm.Size, 0);
                }
            }
        }
    }
}
