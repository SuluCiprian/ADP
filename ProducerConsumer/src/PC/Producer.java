package PC;
import java.util.concurrent.Semaphore;

public class Producer implements Runnable {

	private int capacity;
	private Semaphore sem;

	public Producer(Semaphore sem, int capacity) {
		this.sem = sem;
		this.capacity = capacity;
	}

	@Override
	public void run() {

		int value = 0;
		while (true) {
			System.out.println("Producer produced-" + value);
			try {
				sem.acquire();
				if (Main.list.size() < capacity) {
					Main.list.add(value++);
				}
				Thread.sleep(50);
				sem.release();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}

	}
}
