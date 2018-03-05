package PC;

import java.util.LinkedList;

public class Main {

	public static void main(String[] args) throws InterruptedException {

		LinkedList<Integer> list = new LinkedList<>();
		int capacity = 5;
		Thread prodThread = new Thread(new Producer(list, capacity));
		Thread consThread = new Thread(new Consumer(list));

		prodThread.start();
		consThread.start();

		prodThread.join();
		consThread.join();
	}

}
