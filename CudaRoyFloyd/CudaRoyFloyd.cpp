#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <stdio.h>
#include <iostream>
using namespace std;

#define N 5
#define  INF 9999

__global__ void RoyFloyd (int graph[N][N], int k)
{	
	int i = threadIdx.x;
	int j = threadIdx.y;
	for (int k = 0; k < N; k++)
	{
		if (graph[i][k] + graph[k][j] < graph[i][j]) {
			graph[i][j] = graph[i][k] + graph[k][j];
		}
	}
}

int main()
{
	int *d_distance;
    int h_distance[N][N] = {
	{ 0, 5, INF, 3, INF },
	{ INF, 0, 6, INF, 4 },
	{ INF, INF, 0, 9, INF },
	{ INF, INF, INF, 0, INF },
	{ INF, INF, INF, 2, 0 }
    };

	cudaMalloc(&d_distance, N*N * sizeof(int));
    cudaMemcpy(d_distance, h_distance, N * N * sizeof(int), cudaMemcpyHostToDevice);
	int numBlocks = 1;
	dim3 threadsPerBlock(N, N);

	for (int k = 0; k < N; ++k)
	{		
		RoyFloyd <<< numBlocks, threadsPerBlock >>>(d_distance, k);		
	}

    cudaMemcpy(h_distance, d_distance, N * N * sizeof(int), cudaMemcpyDeviceToHost);
	printMatrix(matrix);

    cudaFree(d_distance);
    cudaFree(h_distance);    
	system("pause");
    return 0;

}

void printMatrix(int *matrix) 
{
    for (int i = 0; i < N; ++i) 
    {
		for (int j = 0; j < N; ++j)
		{
			if (matrix[i][j] == INF)
				cout << "INF ,";
			else
				cout << matrix[i][j] << ", ";
		}
		cout << endl;
	}
}