( n m -- n is a multiple of m )
: MUL? MOD 0= ;

( ... -- sum of stack )
: SUM DEPTH 1 DO + LOOP ;

( n -- sum of multiples of 3 and 5 up to n )
: EULER1
	1 DO
		I DUP DUP
		3 MUL? SWAP
		5 MUL? AND INVERT
		IF DROP THEN
	LOOP 
	SUM ;

( now we switch from compiler mode to the REPL )
1000 EULER1 .
