package PC;

import java.util.LinkedList;
import java.util.concurrent.Semaphore;

public class Consumer implements Runnable {

	private final LinkedList<Integer> list;
	private Semaphore fillCount;
	private Semaphore emptyCount;

	public Consumer(Semaphore fillCount, Semaphore emptyCount, LinkedList<Integer> list) {
		this.list = list;
		this.fillCount = fillCount;
		this.emptyCount = emptyCount;

	}

	@Override
	public void run() {
		while (true) {
			try {
				Thread.sleep(50);
				fillCount.acquire();
				synchronized (list) {
					System.out.println("consumer consumed: " + list.removeFirst());
				}
				emptyCount.release();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}

}