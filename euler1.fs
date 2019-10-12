\ https://projecteuler.net/problem=1
\ Environment: mecrisp stellaris 2.5.2 on blue pill (STM32F103)

: mod3 3 mod ;
: mod5 5 mod ;
: mod3? mod3 0= ;
: mod5? mod5 0= ;
: mod3-or-mod5? ( n -- bool ) dup mod3? swap mod5? or ;
: add-if ( sum n -- sum ) \ add if n to sum if n is a multiple of 3 or 5
	dup mod3-or-mod5? if + else drop then ;

: euler1 ( n -- )
	0 swap 1 do i add-if loop ;

: euler1-1000. 1000 euler1 . ;

\ example
: test-euler1 23 10 euler1 = ;

: test? if ." test" else ." test not" then ;  ok.
: test-euler1? test-euler1 test? ;

test-euler1?
\ to see the result: 1000 euler1 .

\ euler1(95935) = 2147472998, which is the largest input for 32 bit signed
\ integer

\ Utilities missing in mecrisp

\ dropall does not return for empty stack, should not enter loop, but does
: dropall depth 0 > if dropall-unsafe then ;
: dropall-unsafe depth 0 do drop loop ;

: revo ( x1 x2 -- x2 x1 x2) \ insert top of stack as third element, anti-over
	dup rot ;

: true? if ." yes" else ." no" then ;

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

\ Euler #1 on an STM32F103 (80 MHz) runs in 2894 resp. 2894 milliseconds, i.e.
\ 10^-6 seconds.
