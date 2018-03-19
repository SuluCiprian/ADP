package PC;

import java.util.LinkedList;
import java.util.concurrent.Semaphore;

public class Producer implements Runnable {

	private final LinkedList<Integer> list;
	private int capacity;
	private Object condProd;
	private Object condCons;

	public Producer(Object condProd, Object condCons, LinkedList<Integer> list, int capacity) {
		this.list = list;
		this.capacity = capacity;
		this.condCons = condCons;
		this.condProd = condProd;
	}

	@Override
	public void run() {

		int value = 0;
		while (true) {
			System.out.println("Producer produced-" + value);
			try {

				if (list.size() == capacity) {
					synchronized (condProd) {
						condProd.wait();
					}

				}

				synchronized (condCons) {
					list.add(value++);
					condCons.notify();
				}

			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}

	}
}