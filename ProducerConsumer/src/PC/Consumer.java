package PC;
import java.util.concurrent.Semaphore;

public class Consumer implements Runnable {
	
	private Semaphore sem;

	public Consumer(Semaphore sem) {
		this.sem = sem;
	}

	@Override
	public void run() {
		while (true) {
			try {
				sem.acquire();
				if (Main.list.size() != 0) {
					System.out.println("consumer consumed: " + Main.list.removeFirst());

				}
				Thread.sleep(50);
				sem.release();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}
}
