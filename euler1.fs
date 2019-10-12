\ https://projecteuler.net/problem=1
\ Environment: mecrisp stellaris 2.5.2 on blue pill (STM32F103)

: mod5 5 mod ;
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

: add-if ( sum n -- sum ) \ add if n to sum if n is a multiple of 3 or 5
	dup m35? if + else drop then ;

: euler1 ( n -- )
	0 swap 1 do i add-if loop ;

: euler1-1000. 1000 euler1 . ;

\ use provided example as unit test
: test-euler1 23 10 euler1 = ;

: test? if ." test" else ." test not" then ;  ok.
: test-euler1? test-euler1 test? ;

test-euler1?
\ to see the result: 1000 euler1 .

\ euler1(95935) = 2147472998, which is the largest input for 32 bit signed
\ integer

: time ( word -- n )
	micros swap
	execute
	micros swap -
	;
: e1 ( -- )
	1000 euler1 drop
	;

\ ticking and execute does not work in compile mode, why?
\: te1 ( -- ) 
\	' e1 time
\	;

' e1 time .
' e1 time .
' e1 time .
' e1 time .
' e1 time .
' e1 time .
' e1 time .
' e1 time .
' e1 time .
' e1 time .

\ Euler #1 on an STM32F103 (80 MHz) runs in 2893 resp. 2894 µs), i.e.
\ 10^-6 seconds.
\ baseline:                             2893 µs 
\ inlining mod3 and mod5:               2532 µs (12 % faster, 88% of baseline)
\ short circuit evaluation of mod 3, 5: 2241 µs (12 % faster, 77% of baseline)
