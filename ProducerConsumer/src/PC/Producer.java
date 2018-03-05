package PC;

import java.util.LinkedList;

public class Producer implements Runnable {

	private final LinkedList<Integer> list;
	private int capacity;

	public Producer(LinkedList<Integer> list, int capacity) {
		this.list = list;
		this.capacity = capacity;
	}

	@Override
	public void run() {

		int value = 0;
		while (true) {
			System.out.println("Producer produced-" + value);
			try {
				produce(value);
				value++;
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}

	}

	public void produce(int value) throws InterruptedException {
		while (list.size() == capacity) {
			synchronized (list) {
				System.out.println("Waiting - queue full");
				list.wait();
			}
		}

		synchronized (list) {
			list.add(value);
			list.notifyAll();
		}
		// Thread.sleep(50);

	}
}
