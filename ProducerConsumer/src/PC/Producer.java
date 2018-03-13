package PC;

import java.util.LinkedList;
import java.util.concurrent.Semaphore;

public class Producer implements Runnable {

	private final LinkedList<Integer> list;
	private Semaphore fillCount;
	private Semaphore emptyCount;

	public Producer(Semaphore fillCount, Semaphore emptyCount, LinkedList<Integer> list) {
		this.list = list;
		this.fillCount = fillCount;
		this.emptyCount = emptyCount;
	}

	@Override
	public void run() {

		int value = 0;
		while (true) {
			System.out.println("Producer produced-" + value);
			try {
				emptyCount.acquire();
				synchronized (list) {

					list.add(value++);
				}
				fillCount.release();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}

	}
}