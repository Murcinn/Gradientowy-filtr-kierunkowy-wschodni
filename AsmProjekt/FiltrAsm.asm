
;MASKA
;-1  1  1
;-1 -2  1 
;-1  1  1 
.data

multiArray word -1,0,-1,0,-2,0,-1,0

.code
AsmProc proc

movdqu xmm4, oword ptr[multiArray]

mov ebx, dword ptr[rbp + 48]			
mov r10, rbx							

xor r11, r11							
sub r11, r10							

mov r12, rdx							

mov rdi, r8								
add rcx, r8								
add R12, r8								

programLoop:
cmp rdi, r9								
je endLoop															

pxor xmm1,xmm1
pxor xmm2,xmm2
pxor xmm3,xmm3
;pinsrb xmm1, byte ptr[RCX + R11 - 3], 0 ;Place maskValue on index 0 in xmm1
pinsrb xmm1, byte ptr[RCX + R11]    , 1 ;Place maskValue on index 1 in xmm1
pinsrb xmm1, byte ptr[RCX + R11 + 3], 2 ;Place maskValue on index 2 in xmm1
;pinsrb xmm1, byte ptr[RCX - 3]      , 3 ;Place maskValue on index 3 in xmm1
;movzx  ebx , byte ptr[RCX] 				;Place middle maskValue in ebx
pinsrb xmm1, byte ptr[RCX + 3]      , 4 ;Place maskValue on index 4 in xmm1
;pinsrb xmm1, byte ptr[RCX + R10 - 3], 5 ;Place maskValue on index 5 in xmm1
pinsrb xmm1, byte ptr[RCX + R10]    , 6 ;Place maskValue on index 6 in xmm1
pinsrb xmm1, byte ptr[RCX + R10 + 3], 7 ;Place maskValue on index 7 in xmm1

pinsrb xmm3, byte ptr[RCX + R11 -3],0 ;-1
pinsrb xmm3, byte ptr[RCX -3 ],4      ;-1
pinsrb xmm3, byte ptr[RCX ],8	          ;-2
pinsrb xmm3, byte ptr[RCX + R10 -3],12   ;-1

;PMADDWD xmm3, xmm4
;pmuldq xmm3, xmm4
;pmulhw xmm3, xmm4
pmullw xmm3, xmm4

pxor xmm2, xmm2							
psadbw xmm1, xmm2

paddsw xmm1, xmm3
pshufd xmm3, xmm3, 00111001b ;o1 w prawo
paddsw xmm1, xmm3
pshufd xmm3, xmm3, 00111001b
paddsw xmm1, xmm3
pshufd xmm3, xmm3, 00111001b
paddsw xmm1, xmm3


;mov eax, 9								
; ebx									

pextrb eax, xmm1,1	;f0 240>
cmp eax, 240
jg mniejsze


movd eax, xmm1							

;sub eax, ebx							



mov     ebx, 255						
cmp     eax, ebx							
cmovg   eax, ebx						
test    eax, eax						
mov     ebx, 0							
cmovl   eax, ebx						

mov byte ptr[R12], al					

inc rdi									
inc rcx									
inc R12									
jmp programLoop

mniejsze:
mov eax, 0

mov byte ptr[R12], al					

inc rdi									
inc rcx									
inc R12									
jmp programLoop

endLoop:
ret
AsmProc endp
end
