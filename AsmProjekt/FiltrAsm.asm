;r11  - start index
;r10  - end index
;xmm0 - pixels
;xmm1 - negativ array with -1 values
.code

AsmProc proc


	mov			ebx, dword ptr[rbp + 32]	; load start inex
	mov			r8, rbx					; and give it to r11
	mov			ebx, dword ptr[rbp + 40]	; load end index
	mov			r9, rbx					; and give it to r10

	movdqu		xmm1, oword ptr[rdx]		; load negativ values to xmm1
	

	mov			rdi, r8					; establish counter with stat index value

mainLoop:
	cmp			rdi, r9					; if counter is equal with end index, end loop
	je			endLoop						
	
	;movdqu		xmm0, oword ptr[rcx]		; take to xmm0 one pixel	

	;mulss		xmm0, xmm1					; multiply xmm0 with xmm1

	;movdqu		oword ptr[rcx], xmm0		; save result pixel 
	
	movd		xmm0, dword ptr[rcx]		; take to xmm0 one pixel	

	mulps		xmm0, xmm1					; multiply xmm0 with xmm1

	movd		dword ptr[rcx], xmm0		; save result pixel

	add			rcx, 4						; increase data pointer by 16 bytes	
	add			rdi, 4	
	;add			rdi, 4						; increase counter by 1


	jmp			mainLoop					; go through loop
endLoop:
ret
AsmProc endp

end
