package PC;

import java.util.LinkedList;
import java.util.concurrent.Semaphore;

public class Consumer implements Runnable {

	private final LinkedList<Integer> list;
	private Object condProd;
	private Object condCons;

	public Consumer(Object condProd, Object condCons, LinkedList<Integer> list) {
		this.list = list;
		this.condCons = condCons;
		this.condProd = condProd;

	}

	@Override
	public void run() {
		while (true) {
			try {
				Thread.sleep(50);
				 
					if (list.size() == 0) {
						synchronized (condCons) {
							condCons.wait();
						}
						
					}
					synchronized (condProd) {
						int item = list.removeFirst();
						condProd.notify();
						System.out.println("consumer consumed: " + item);
					}
					
				
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}

}