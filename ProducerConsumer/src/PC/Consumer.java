package PC;

import java.util.LinkedList;

public class Consumer implements Runnable {

	private final LinkedList<Integer> list;

	public Consumer(LinkedList<Integer> list) {
		this.list = list;

	}

	@Override
	public void run() {
		while (true) {
			try {
				System.out.println("consumer consumed: " + consume());
				Thread.sleep(50);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}

	public int consume() throws InterruptedException {
		while (list.size() == 0) {
			synchronized (list) {
				System.out.println("Waiting - queue empty");
				list.wait();
			}
		}

		synchronized (list) {
			list.notifyAll();
			return list.removeFirst();
		}
	}
}
