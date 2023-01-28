
;kernel
;-1  1  1
;-1 -2  1 
;-1  1  1 
.data

;multiArray word -1,0,-1,0,-2,0,-1,0
multiArray word -1,0,-2,0,0,0,0,0
.code
AsmProc proc

movdqu xmm4, oword ptr[multiArray]			;Move array with mask values to xmm4	

mov ebx, dword ptr[rbp + 48]				;Move image width to ebx	
mov r10, rbx								;Move image width to r10

xor r11, r11								;Clear r11
sub r11, r10								;Add negative value of image width to r11

mov r12, rdx								;Move new pixels array pointer to r12		

mov rdi, r8									;Move starting index to rdi
add rcx, r8									;
add R12, r8									;

programLoop:
cmp rdi, r9									;End loop condition. If values are equal end loop.
je endLoop															

pxor xmm1,xmm1								;Clear registers				
pxor xmm2,xmm2								;
pxor xmm3,xmm3								;

pinsrb xmm1, byte ptr[RCX + R11]    , 1		;Id.1. Locate mask values 1 in xmm1
pinsrb xmm1, byte ptr[RCX + R11 + 3], 2		;Id.2. Locate mask values 1 in xmm1
pinsrb xmm1, byte ptr[RCX + 3]      , 4		;Id.4. Locate mask values 1 in xmm1
pinsrb xmm1, byte ptr[RCX + R10]    , 6		;Id.6. Locate mask values 1 in xmm1
pinsrb xmm1, byte ptr[RCX + R10 + 3], 7		;Id.7. Locate mask values 1 in xmm1

pinsrb xmm3, byte ptr[RCX + R11 -3] ,0		;Id.0. Locate mask values -1 in xmm3
pinsrb xmm3, byte ptr[RCX -3 ]		,1		;Id.1. Locate mask values -1 in xmm3
pinsrb xmm3, byte ptr[RCX + R10 -3] ,2		;Id.2. Locate mask values -1 in xmm3

psadbw xmm3,xmm2							;Sum pixel values in xmm3

pinsrb xmm3, byte ptr[RCX ],4				;Id.4.  Locate mask value -2 in xmm3

pmullw xmm3, xmm4							;Multiply values in xmm3 with filter mask values stored in xmm4

pxor xmm2, xmm2								;Clear xmm2
psadbw xmm1, xmm2							;Sum pixel values of two registers and move result to xmm1

paddsw xmm1, xmm3							;Sum pixel signed values of two registers. 
pshufd xmm3, xmm3, 00111001b				;Dectrement stack pointer.
paddsw xmm1, xmm3							;Sum pixel signed values of two registers. 

pextrb eax, xmm1,1							;Check if the sum value is lower than 0.				f0 240>
cmp eax, 240								;Compare
jg negativeValue							;If value is negative number jump to negativeValue		

movd eax, xmm1							

mov     ebx, 255							;Check if the sum value is greater than 255
cmp     eax, ebx							;Compare
cmovg   eax, ebx							;Move 255 value if the sum is greater				

mov byte ptr[R12], al						;Place clamped value in place in table					

inc rdi										;Increment loop counter
inc rcx										;Increment orginal pixels index
inc R12										;Increment new pixels index
jmp programLoop

negativeValue:
mov eax, 0									;Set value on 0

mov byte ptr[R12], al						;Place clamped value in place in table				

inc rdi										;Increment loop counter
inc rcx										;Increment orginal pixels index
inc R12										;Increment new pixels index
jmp programLoop

endLoop:
ret
AsmProc endp
end
