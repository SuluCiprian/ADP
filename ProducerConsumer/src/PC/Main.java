package PC;

import java.util.LinkedList;
import java.util.concurrent.Semaphore;

public class Main {

	public static LinkedList<Integer> list1;
	public final static Object condProd1 = new Object();
	public final static Object condCons1 = new Object();

	public static void main(String[] args) throws InterruptedException {

		LinkedList<Integer> list = new LinkedList<>();
		int capacity = 5;
		Object condProd = new Object();
		Object condCons = new Object();

		Thread prodThread = new Thread(new Producer(condProd, condCons, list, capacity));
		Thread consThread = new Thread(new Consumer(condProd, condCons, list));

		prodThread.start();
		consThread.start();

		prodThread.join();
		consThread.join();
	}

}
