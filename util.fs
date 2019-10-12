\ Utilities missing in mecrisp

\ dropall does not return for empty stack, should not enter loop, but does
: dropall depth 0 > if dropall-unsafe then ;
: dropall-unsafe depth 0 do drop loop ;

: revo ( x1 x2 -- x2 x1 x2) \ insert top of stack as third element, anti-over
	dup rot ;

: true? if ." yes" else ." no" then ;
