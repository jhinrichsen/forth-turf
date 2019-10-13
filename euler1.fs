\ https://projecteuler.net/problem=1
\ Environment: mecrisp stellaris 2.5.2 on blue pill (STM32F103)
\
\ euler1(95935) = 2147472998, which is the largest input for 32 bit signed
\ integer

: mod3? 3 mod 0= ;
: mod5? 5 mod 0= ;
: mod3-or-mod5? ( n -- bool )
	dup mod3? swap mod5? or ;

\ short circuit evaluation of mod3-or-mod5?
: m35? ( n -- bool )
	dup
	mod3? if
		drop true
	else 
		mod5?
	then ;

: add-if-v1 ( sum n -- sum ) \ add if n to sum if n is a multiple of 3 or 5
	dup mod3-or-mod5? if + else drop then ;

: add-if-v2 ( sum n -- sum ) \ add if n to sum if n is a multiple of 3 or 5
	dup m35? if + else drop then ;

\ iterate a straight sequence, check each and every number, add to sum if it is
\ a multiple
: variant1
	0 swap 1 do i add-if-v1 loop ;

\ variant 2 uses short circuit on multiples of 3 _or_ 5
: variant2
	0 swap 1 do i add-if-v2 loop ;

: filter ( n -- n | 0 ) \ if not a multiple of 3 or 5 set n to 0 
	dup m35? not if drop 0 then ;
	
\ variant 1 and 2 contain code like `if + else drop then` which indicates bad flow.
\ variant 3 avoids branches by always adding a number, `0` if its not a
\ multiple.
: variant3
	0 swap 1 do i filter + loop ;

: sum3 ( n -- n )
	0 swap 3 do i + 3 +loop ;
: sum5 ( n -- n )
	0 swap 5 do i + 5 +loop ;
: sum15 ( n -- n )
	0 swap 15 do i + 15 +loop ;

: variant4
	dup sum3
	over sum5 +
	swap sum15 -
	;

\ ---------- tests and benchmarks ----------

\ all tests use vectored execution to test variants
' variant1 variable 'variant ( n -- n )
: use-v1 ['] variant1 'variant ! ;
: use-v2 ['] variant2 'variant ! ;
: use-v3 ['] variant3 'variant ! ;
: use-v4 ['] variant4 'variant ! ;

\ execute variant
: ev 'variant @ execute ;

: euler1. 1000 ev . ;

\ use provided example as unit test
: test-example 23 10 ev = ;

: assert. if ." test" else ." test not" then ;
: test-euler1 test-example assert. ;

: bench ( -- )
	1000 ev drop
	;

: time ( word -- n )
	micros swap
	execute
	micros swap -
	;

\ timings for different runs are pretty consistent, which, in the absence of
\ virus scans, disk interrupts, network e.a., makes sense, so there's only one
\ benchmark timing output
: bench. ( -- ) 
	cr
	['] bench time .
	." μs"
	;

use-v1 bench.
use-v2 bench.
use-v3 bench.
use-v4 bench.


