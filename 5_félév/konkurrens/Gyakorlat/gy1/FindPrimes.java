import java.util.List;
import java.util.LinkedList;

public interface FindPrimes {

	List<Long> get();

	static FindPrimes makeNaive( long boundary ){ return new Naive(boundary); }
	static FindPrimes makeMixed( long boundary ){ return new Mixed(boundary); }
	static FindPrimes makeSieve( long boundary ){ return new Sieve(boundary); }

	static class Naive implements FindPrimes {
		private final long boundary;
		Naive( long boundary ){ this.boundary = boundary; }
		public List<Long> get(){
			List<Long> primes = new LinkedList<>();
			for( long current = 2L; current <= boundary; ++current ){
				boolean currentMayBePrime = true;
				for( long i = 2; currentMayBePrime && i<=(long)Math.sqrt(current); ++i ){
					if( current % i == 0 ) currentMayBePrime = false;
				}
				if( currentMayBePrime ){
					primes.add(current);
				}
			}
			return primes;
		}
	}

	static class Mixed implements FindPrimes {
		private final long boundary;
		Mixed( long boundary ){ this.boundary = boundary; }
		public List<Long> get(){
			List<Long> primes = new LinkedList<>();
			outer: for( long current = 2L; current <= boundary; ++current ){
				long sqrtCurrent = (long)Math.sqrt(current);
				for( long p: primes ){
					if( p > sqrtCurrent ){
						break;
					}
					if( current % p == 0 ){
						continue outer;
					}
				}
				primes.add(current);
			}
			return primes;
		}
	}

	static class Sieve implements FindPrimes {
		private final int boundary;
		Sieve( long boundary ){
			if( boundary >= Integer.MAX_VALUE )
				throw new IllegalArgumentException("Boundary is too large.");
			this.boundary = (int)boundary;
		}
		public List<Long> get(){
			boolean[] knownToBeCompound = new boolean[boundary+1];   // all false initially
			for( int current = 2; current <= (int)Math.sqrt(boundary); ++current ){
				if( ! knownToBeCompound[current] ){
					for( int multiple = current*current; multiple <= boundary; multiple += current ){
						knownToBeCompound[multiple] = true;
					}
				}
			}
			List<Long> primes = new LinkedList<>();
			for( int i=2; i<knownToBeCompound.length; ++i ){
				if( ! knownToBeCompound[i] ){
					primes.add((long)i);
				}
			}
			return primes;
		}
	}

	public static void main( String[] args ){
		System.out.println( makeNaive(100).get() );
		System.out.println(System.currentTimeMillis());
		System.out.println( makeNaive(100_000_000L).get().size() );
		System.out.println(System.currentTimeMillis());
		System.out.println( makeMixed(100_000_000L).get().size() );
		System.out.println(System.currentTimeMillis());
		System.out.println( makeSieve(100_000_000L).get().size() );
		System.out.println(System.currentTimeMillis());
	}
}
