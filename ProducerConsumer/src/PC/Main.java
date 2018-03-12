package PC;

import java.util.LinkedList;
import java.util.concurrent.Semaphore;

public class Main {

	public static LinkedList<Integer> list = new LinkedList<>();

	public static void main(String[] args) throws InterruptedException {

		int capacity = 5;
		Semaphore sem = new Semaphore(1);
		Thread prodThread = new Thread(new Producer(sem, capacity));
		Thread consThread = new Thread(new Consumer(sem));

		prodThread.start();
		consThread.start();

		prodThread.join();
		consThread.join();
	}

}
