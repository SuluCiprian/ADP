package PC;

import java.util.LinkedList;
import java.util.concurrent.Semaphore;

public class Main {
    
    public static LinkedList<Integer> list1;

	public static void main(String[] args) throws InterruptedException {

		LinkedList<Integer> list = new LinkedList<>();
		int capacity = 5;
                Semaphore fillCount = new Semaphore(0);
                Semaphore emptyCount = new Semaphore(capacity);
		Thread prodThread = new Thread(new Producer(fillCount, emptyCount, list));
		Thread consThread = new Thread(new Consumer(fillCount, emptyCount,list));

		prodThread.start();
		consThread.start();

		prodThread.join();
		consThread.join();
	}

}
