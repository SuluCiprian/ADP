using System;
using MPI;

namespace sum
{
    class Program
    {
        static void Main(string[] args)
        {

            Random rand = new Random();

            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;

                if (comm.Rank == 0)
                {
                    int max = -1;
                    int lider = comm.Rank;
                    int curentProcRand = rand.Next(100);
                    Console.WriteLine("Rank " + comm.Rank + " generated rand: " + curentProcRand);
                    if (max < curentProcRand)
                    {
                        max = curentProcRand;
                    }                        
                    comm.Send(max, 1, 0);
                    comm.Send(lider, 1, 1);
                    max = comm.Receive<int>(Communicator.anySource, 0);
                    Console.WriteLine("Rank " + comm.Rank + " recived max: " + max);
                    lider = comm.Receive<int>(Communicator.anySource, 1);
                    Console.WriteLine("The leader is: " + lider);
                }
                else
                {
                    int max = comm.Receive<int>(comm.Rank - 1, 0);
                    int lider = comm.Receive<int>(comm.Rank - 1, 1);
                    Console.WriteLine("Rank " + comm.Rank + " recived max: " + max);
                    int curentProcRand = rand.Next(100);
                    Console.WriteLine("Rank " + comm.Rank + " generated rand: " + curentProcRand);
                    if (max <= curentProcRand)
                    {
                        max = curentProcRand;
                        lider = comm.Rank;
                    }
                    comm.Send(max, (comm.Rank + 1) % comm.Size, 0);
                    comm.Send(lider, (comm.Rank + 1) % comm.Size, 1);
                }
            }
        }
    }
}