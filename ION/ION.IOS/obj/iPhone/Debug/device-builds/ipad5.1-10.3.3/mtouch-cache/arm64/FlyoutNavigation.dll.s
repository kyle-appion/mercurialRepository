.subsections_via_symbols
.section __DWARF, __debug_line,regular,debug
Ldebug_line_section_start:
Ldebug_line_start:
.section __DWARF, __debug_abbrev,regular,debug

	.byte 1,17,1,37,8,3,8,27,8,19,11,17,1,18,1,16,6,0,0,2,46,1,3,8,135,64,8,58,15,59,15,17
	.byte 1,18,1,64,10,0,0,3,5,0,3,8,73,19,2,10,0,0,15,5,0,3,8,73,19,2,6,0,0,4,36,0
	.byte 11,11,62,11,3,8,0,0,5,2,1,3,8,11,15,0,0,17,2,0,3,8,11,15,0,0,6,13,0,3,8,73
	.byte 19,56,10,0,0,7,22,0,3,8,73,19,0,0,8,4,1,3,8,11,15,73,19,0,0,9,40,0,3,8,28,13
	.byte 0,0,10,57,1,3,8,0,0,11,52,0,3,8,73,19,2,10,0,0,12,52,0,3,8,73,19,2,6,0,0,13
	.byte 15,0,73,19,0,0,14,16,0,73,19,0,0,16,28,0,73,19,56,10,0,0,18,46,0,3,8,17,1,18,1,0
	.byte 0,0
.section __DWARF, __debug_info,regular,debug
Ldebug_info_start:

LDIFF_SYM0=Ldebug_info_end - Ldebug_info_begin
	.long LDIFF_SYM0
Ldebug_info_begin:

	.short 2
	.long 0
	.byte 8,1
	.asciz "Mono AOT Compiler 5.2.0 (tarball Fri Sep 15 02:07:52 EDT 2017)"
	.asciz "FlyoutNavigation.dll"
	.asciz ""

	.byte 2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
LDIFF_SYM1=Ldebug_line_start - Ldebug_line_section_start
	.long LDIFF_SYM1
LDIE_I1:

	.byte 4,1,5
	.asciz "sbyte"
LDIE_U1:

	.byte 4,1,7
	.asciz "byte"
LDIE_I2:

	.byte 4,2,5
	.asciz "short"
LDIE_U2:

	.byte 4,2,7
	.asciz "ushort"
LDIE_I4:

	.byte 4,4,5
	.asciz "int"
LDIE_U4:

	.byte 4,4,7
	.asciz "uint"
LDIE_I8:

	.byte 4,8,5
	.asciz "long"
LDIE_U8:

	.byte 4,8,7
	.asciz "ulong"
LDIE_I:

	.byte 4,8,5
	.asciz "intptr"
LDIE_U:

	.byte 4,8,7
	.asciz "uintptr"
LDIE_R4:

	.byte 4,4,4
	.asciz "float"
LDIE_R8:

	.byte 4,8,4
	.asciz "double"
LDIE_BOOLEAN:

	.byte 4,1,2
	.asciz "boolean"
LDIE_CHAR:

	.byte 4,2,8
	.asciz "char"
LDIE_STRING:

	.byte 4,8,1
	.asciz "string"
LDIE_OBJECT:

	.byte 4,8,1
	.asciz "object"
LDIE_SZARRAY:

	.byte 4,8,1
	.asciz "object"
.section __DWARF, __debug_loc,regular,debug
Ldebug_loc_start:
.section __DWARF, __debug_frame,regular,debug
	.align 3

LDIFF_SYM2=Lcie0_end - Lcie0_start
	.long LDIFF_SYM2
Lcie0_start:

	.long -1
	.byte 3
	.asciz ""

	.byte 1,120,30
	.align 3
Lcie0_end:
.text
	.align 3
jit_code_start:

	.byte 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
.text
	.align 4
	.no_dead_strip FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool
FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool:
.file 1 "<unknown>"
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000bb4
.word 0xa901dfb6
.word 0xf90017b8
.word 0xaa0003f8
.word 0xf9001ba1
.word 0xf9001fa2

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #200]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800017
.word 0xf94023b1
.word 0xf9404a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #208]
.word 0xd2800301
.word 0xd2800301
bl _p_1
.word 0xf9003ba0
bl _p_2
.word 0xf94023b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003f7
.word 0xf94023b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9401fa0
.word 0xf9000ae0
.word 0x910042e1
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94023b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9401ba1
.word 0xaa1803e0
bl _p_3
.word 0xf94023b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803f6
.word 0xf9002fb8
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_4
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf90037a0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000a20

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #216]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf94033a0
.word 0xf94037a2
.word 0xf9001022
.word 0x91008023
.word 0xd349fc63
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0063

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x4, [x16, #16]
.word 0x8b040063
.word 0xd280003e
.word 0x3900007e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #224]
.word 0xf9001422

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #232]
.word 0xf9002022

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #240]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f
bl _p_5
.word 0xaa0003f4
.word 0xf94023b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0xb4000174
.word 0xf9400280
.word 0xf9400000
.word 0xf9400800
.word 0xf9400c00

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #248]
.word 0xeb01001f
.word 0x10000011
.word 0x540002e1
.word 0xaa1403e0
.word 0xf9402fa0
.word 0xaa1403e1
bl _p_6
.word 0xf94023b1
.word 0xf9426631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9428631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb4
.word 0xa941dfb6
.word 0xf94017b8
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_7
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_0:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor
FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #256]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_1:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__m__0_UIKit_UIGestureRecognizer_UIKit_UITouch
FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__m__0_UIKit_UIGestureRecognizer_UIKit_UITouch:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xf9001bb7
.word 0xf9001fba
.word 0xf90023a0
.word 0xf90027a1
.word 0xaa0203fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #264]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800017
.word 0xf9402bb1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf940e030
.word 0xd63f0200
.word 0xf90043a0
.word 0xf9402bb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9402030
.word 0xd63f0200
.word 0xf9003fa0
.word 0xf9402bb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa3

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #272]
.word 0xd2800060
.word 0xaa0303e0
.word 0xd2800062
.word 0xf940007e
bl _p_8
.word 0x93407c00
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0x92800001
.word 0xf2bfffe1
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x9a9fd7e0
.word 0xaa0003f7
.word 0xf9402bb1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf940e030
.word 0xd63f0200
.word 0xaa0003f6
.word 0xf9402bb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603f5
.word 0xeb1f02df
.word 0x54000160
.word 0xf94002c0
.word 0xf9400000
.word 0xf9400800
.word 0xf9401400

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #280]
.word 0xeb01001f
.word 0x54000040
.word 0xd2800015
.word 0xaa1503e0
.word 0xb5000495
.word 0xf9402bb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf940e030
.word 0xd63f0200
.word 0xaa0003f4
.word 0xf9402bb1
.word 0xf941c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403f3
.word 0xeb1f029f
.word 0x54000160
.word 0xf9400280
.word 0xf9400000
.word 0xf9400800
.word 0xf9401000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #288]
.word 0xeb01001f
.word 0x54000040
.word 0xd2800013
.word 0xaa1303e0
.word 0xb50000f3
.word 0xf9402bb1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x34000297
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9425e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x1400001e
.word 0xf9402bb1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400803
.word 0xf94027a1
.word 0xaa1a03e0
.word 0xaa0303e0
.word 0xaa1a03e2
.word 0xf9003fa3
.word 0xf9400c70
.word 0xd63f0200
.word 0xf9403fa1
.word 0x53001c00
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf942b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf942d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9402bb1
.word 0xf942ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xf9401bb7
.word 0xf9401fba
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0

Lme_2:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__ctor_intptr
FlyoutNavigation_FlyoutNavigationController__ctor_intptr:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #296]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020
.word 0xd280003e
.word 0x3902833e
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
bl _p_9
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9002b20
.word 0x91014321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0x3902a33f
.word 0xf94013b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0x3902ef3f
.word 0xf94013b1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0x3902fb3f
.word 0xf94013b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0x3902ff3f
.word 0xf94013b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa1
.word 0xaa1903e0
bl _p_10
.word 0xf94013b1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0xaa1903e0
.word 0xd2800001
bl _p_11
.word 0xf94013b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_3:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__ctor_UIKit_UITableViewStyle
FlyoutNavigation_FlyoutNavigationController__ctor_UIKit_UITableViewStyle:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #304]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020
.word 0xd280003e
.word 0x3902833e
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
bl _p_9
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9002b20
.word 0x91014321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0x3902a33f
.word 0xf94013b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0x3902ef3f
.word 0xf94013b1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0x3902fb3f
.word 0xf94013b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0x3902ff3f
.word 0xf94013b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_12
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa1
.word 0xaa1903e0
bl _p_11
.word 0xf94013b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_4:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_NavigationViewController
FlyoutNavigation_FlyoutNavigationController_get_NavigationViewController:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #312]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9402000
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_5:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_MenuBorderColor
FlyoutNavigation_FlyoutNavigationController_get_MenuBorderColor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #320]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9402800
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_6:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_MenuBorderColor_UIKit_UIColor
FlyoutNavigation_FlyoutNavigationController_set_MenuBorderColor_UIKit_UIColor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #328]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa0
.word 0xf9002b20
.word 0x91014321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402720
.word 0xb40003e0
.word 0xf94013b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402722
.word 0xaa1903e0
.word 0xf9402b21
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941c850
.word 0xd63f0200
.word 0xf94013b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402721
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9420c30
.word 0xd63f0200
.word 0xf94013b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_7:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder
FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #336]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x3942a000
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_8:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_ShowMenuBorder_bool
FlyoutNavigation_FlyoutNavigationController_set_ShowMenuBorder_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #344]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x394063a1
.word 0x3902a001
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_9:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_TintColor
FlyoutNavigation_FlyoutNavigationController_get_TintColor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #352]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9403000
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_a:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor
FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #360]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9403000
.word 0xf9400fa1
.word 0xeb01001f
.word 0x540000c1
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000005
.word 0xf94013b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_b:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_Position
FlyoutNavigation_FlyoutNavigationController_get_Position:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #368]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb980a400
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_c:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_Position_FlyoutNavigation_FlyOutNavigationPosition
FlyoutNavigation_FlyoutNavigationController_set_Position_FlyoutNavigation_FlyOutNavigationPosition:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xaa0003f9
.word 0xf9001ba1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #376]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf9401fb1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xb98033a0
.word 0xb900a720
.word 0xf9401fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f21
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9419030
.word 0xd63f0200
.word 0xf9003fa0
.word 0xf9401fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_13
.word 0x93407c00
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9403fa1
.word 0xaa0103f8
.word 0x350000e0
.word 0xaa1803e0
.word 0x92800080
.word 0xf2bfffe0
.word 0x92800097
.word 0xf2bffff7
.word 0x14000004
.word 0xaa1803e0
.word 0xd28000a0
.word 0xd28000b7
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xaa1703e0
bl _p_14
.word 0xfd0043a0
.word 0xf9401fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0x92800000
.word 0xf2bfffe0
.word 0x92800000
.word 0xf2bfffe0
bl _p_14
.word 0xfd0047a0
.word 0xf9401fb1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4043a0
.word 0xfd4047a1
.word 0x910183a0
.word 0xd2800000
.word 0xf90033a0
.word 0xf90037a0
.word 0x910183a0
bl _p_15
.word 0x910183a0
.word 0x910143a0
.word 0xf94033a0
.word 0xf9002ba0
.word 0xf94037a0
.word 0xf9002fa0
.word 0xf9401fb1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x910143a1
.word 0xfd402ba0
.word 0xfd402fa1
.word 0xf9400301
.word 0xf9414430
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9404b20
.word 0xb40003a0
.word 0xf9401fb1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9404b21
.word 0xaa1903e0
.word 0xb980a720
.word 0xaa0103f8
.word 0x350000a0
.word 0xaa1803e0
.word 0xd2800040
.word 0xd2800056
.word 0x14000004
.word 0xaa1803e0
.word 0xd2800100
.word 0xd2800116
.word 0xaa1803e0
.word 0xaa1603e0
.word 0xaa1803e0
.word 0xaa1603e1
.word 0xf9400302
.word 0xf9412050
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9426a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xa94267b8
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0

Lme_d:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged
FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #384]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9403c00
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_e:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action
FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #392]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba1
.word 0xf9400fa0
.word 0xf9003c20
.word 0x9101e021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_f:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu
FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #400]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x3942e400
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_10:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool
FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #408]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x394063a1
.word 0x3902e401
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_11:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen
FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #416]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x3942e800
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_12:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_ForceMenuOpen_bool
FlyoutNavigation_FlyoutNavigationController_set_ForceMenuOpen_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #424]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x394063a1
.word 0x3902e801
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_13:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation
FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #432]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x3942f000
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_14:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool
FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #440]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x394063a1
.word 0x3902f001
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_15:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_HideShadow
FlyoutNavigation_FlyoutNavigationController_get_HideShadow:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #448]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x3942e000
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_16:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_HideShadow_bool
FlyoutNavigation_FlyoutNavigationController_set_HideShadow_bool:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #456]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0x3942e320
.word 0x6b00035f
.word 0x540000c1
.word 0xf94013b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000060
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0x3902e33a
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x3942e320
.word 0x34000780
.word 0xf94013b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xb40004a0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f20
.word 0xf90023a0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf90027a0
.word 0xf94013b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xf94027a2
.word 0xf9402ba3
.word 0xaa0303e0
.word 0xf9400063
.word 0xf9422470
.word 0xd63f0200
.word 0xf94013b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400000f
.word 0xf94013b1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f21
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421830
.word 0xd63f0200
.word 0xf94013b1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_17:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_ShadowViewColor
FlyoutNavigation_FlyoutNavigationController_get_ShadowViewColor:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #464]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9402c01
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9419030
.word 0xd63f0200
.word 0xf90023a0
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9418830
.word 0xd63f0200
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #472]
bl _p_17
.word 0xf9401fa1
.word 0xf9001ba0
bl _p_18
.word 0xf9400fb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_18:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_ShadowViewColor_UIKit_UIColor
FlyoutNavigation_FlyoutNavigationController_set_ShadowViewColor_UIKit_UIColor:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #480]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9402c01
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9419030
.word 0xd63f0200
.word 0xf90027a0
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf940ec30
.word 0xd63f0200
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xf94027a2
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9418450
.word 0xd63f0200
.word 0xf94013b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_19:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController
FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #488]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9404000
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_1a:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController
FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #496]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba1
.word 0xf9400fa0
.word 0xf9004020
.word 0x91020021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_1b:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_mainView
FlyoutNavigation_FlyoutNavigationController_get_mainView:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #504]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_19
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xb5000200
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x14000020
.word 0xf9400fb1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_19
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_1c:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_NavigationRoot
FlyoutNavigation_FlyoutNavigationController_get_NavigationRoot:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #512]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9402001
.word 0xaa0103e0
.word 0xf940003e
bl _p_20
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_1d:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_NavigationRoot_MonoTouch_Dialog_RootElement
FlyoutNavigation_FlyoutNavigationController_set_NavigationRoot_MonoTouch_Dialog_RootElement:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f9
.word 0xf90013a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #520]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800018
.word 0xf94017b1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #528]
.word 0xd2800401
.word 0xd2800401
bl _p_1
.word 0xf90027a0
bl _p_21
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xaa0003f8
.word 0xf94017b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94013a0
.word 0xf9000b00
.word 0x91004301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xf9000f19
.word 0x91006300
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94017b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xf90023a0
.word 0xeb1f031f
.word 0x10000011
.word 0x54000700

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf94023a0
.word 0xf9001020
.word 0x91008022
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #544]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #552]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #560]
.word 0xf9401402
.word 0xf9000c22
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa1903e0
bl _p_22
.word 0xf94017b1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_1e:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView
FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #568]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9402001
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421c30
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_1f:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_ViewControllers
FlyoutNavigation_FlyoutNavigationController_get_ViewControllers:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #576]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9403800
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_20:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_ViewControllers_UIKit_UIViewController__
FlyoutNavigation_FlyoutNavigationController_set_ViewControllers_UIKit_UIViewController__:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f9
.word 0xf90013a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #584]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800018
.word 0xf94017b1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #592]
.word 0xd2800401
.word 0xd2800401
bl _p_1
.word 0xf90027a0
bl _p_23
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xaa0003f8
.word 0xf94017b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94013a0
.word 0xf9000b00
.word 0x91004301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xf9000f19
.word 0x91006300
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94017b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xf90023a0
.word 0xeb1f031f
.word 0x10000011
.word 0x54000700

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf94023a0
.word 0xf9001020
.word 0x91008022
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #600]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #608]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #616]
.word 0xf9401402
.word 0xf9000c22
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa1903e0
bl _p_22
.word 0xf94017b1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_21:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_IsOpen
FlyoutNavigation_FlyoutNavigationController_get_IsOpen:
.loc 1 1 0
.word 0xa9b27bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #624]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0x910243a0
.word 0xd2800000
.word 0xf9004ba0
.word 0xf9004fa0
.word 0xf90053a0
.word 0xf90057a0
.word 0x9101c3a0
.word 0xd2800000
.word 0xf9003ba0
.word 0xf9003fa0
.word 0xf90043a0
.word 0xf90047a0
.word 0xf9400fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_13
.word 0x93407c00
.word 0xf90063a0
.word 0xf9400fb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x350009a0
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9006fa0
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0x910143a0
.word 0xf9005ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9405bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9400fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0x910143a0
.word 0x910243a0
.word 0xf9402ba0
.word 0xf9004ba0
.word 0xf9402fa0
.word 0xf9004fa0
.word 0xf94033a0
.word 0xf90053a0
.word 0xf94037a0
.word 0xf90057a0
.word 0xf9400fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910243a0
bl _p_24
.word 0xfd0067a0
.word 0xf9400fb1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd006ba0
.word 0xf9400fb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4067a0
.word 0xfd406ba1
.word 0x1e612000
.word 0x9a9f17e0
.word 0xf90063a0
.word 0xf9400fb1
.word 0xf941ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x1400004e
.word 0xf9400fb1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9006fa0
.word 0xf9400fb1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0x9100c3a0
.word 0xf9005ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9405bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9400fb1
.word 0xf9425231
.word 0xb4000051
.word 0xd63f0220
.word 0x9100c3a0
.word 0x9101c3a0
.word 0xf9401ba0
.word 0xf9003ba0
.word 0xf9401fa0
.word 0xf9003fa0
.word 0xf94023a0
.word 0xf90043a0
.word 0xf94027a0
.word 0xf90047a0
.word 0xf9400fb1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9101c3a0
bl _p_24
.word 0xfd0067a0
.word 0xf9400fb1
.word 0xf942a631
.word 0xb4000051
.word 0xd63f0220
.word 0x928022e0
.word 0xf2bfffe0
.word 0x928022e0
.word 0xf2bfffe0
bl _p_14
.word 0xfd006ba0
.word 0xf9400fb1
.word 0xf942ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4067a0
.word 0xfd406ba1
.word 0x1e612000
.word 0x9a9f17e0
.word 0xf90063a0
.word 0xf9400fb1
.word 0xf942f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf9400fb1
.word 0xf9432631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8ce7bfd
.word 0xd65f03c0

Lme_22:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_IsOpen_bool
FlyoutNavigation_FlyoutNavigationController_set_IsOpen_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #632]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0x394063a0
.word 0x34000220
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_25
.word 0xf94013b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400000c
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_26
.word 0xf94013b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_23:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen
FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #640]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_27
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0x350009c0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
bl _p_28
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xd2800021
.word 0xd280003e
.word 0xeb1e001f
.word 0x54000921
.word 0xf9400fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_30
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0x34000740
.word 0xf9400fb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9418030
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xd2800081
.word 0xd280009e
.word 0xeb1e001f
.word 0x54000280
.word 0xf9400fb1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9418030
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xd2800061
.word 0xd280007e
.word 0xeb1e001f
.word 0x54000281
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0x14000013
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9422231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9400fb1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_24:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex
FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #648]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb980ac00
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_25:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_SelectedIndex_int
FlyoutNavigation_FlyoutNavigationController_set_SelectedIndex_int:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f9
.word 0xf90013a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #656]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800018
.word 0xf94017b1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #664]
.word 0xd2800401
.word 0xd2800401
bl _p_1
.word 0xf90023a0
bl _p_31
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xaa0003f8
.word 0xf94017b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb98023a0
.word 0xb9001b00
.word 0xf94017b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xf9000b19
.word 0x91004300
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xb980af20
.word 0xaa1803e1
.word 0xb9801b01
.word 0x6b01001f
.word 0x540000c1
.word 0xf94017b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000040
.word 0xf94017b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xb9801b00
.word 0xb900af20
.word 0xf94017b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xeb1f031f
.word 0x10000011
.word 0x540006e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf9001038
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #672]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #680]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xf9401402
.word 0xf9000c22
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa1903e0
bl _p_22
.word 0xf94017b1
.word 0xf9422231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_26:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_DisableRotation
FlyoutNavigation_FlyoutNavigationController_get_DisableRotation:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #696]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x3942f400
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_27:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_DisableRotation_bool
FlyoutNavigation_FlyoutNavigationController_set_DisableRotation_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #704]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x394063a1
.word 0x3902f401
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_28:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_ShouldAutomaticallyForwardRotationMethods
FlyoutNavigation_FlyoutNavigationController_get_ShouldAutomaticallyForwardRotationMethods:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #712]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xf9400fb1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_29:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle
FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle:
.loc 1 1 0
.word 0xd2805810
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xf90023b9
.word 0xaa0003f9
.word 0xf90027a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #720]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800018
.word 0x910623a0
.word 0xd2800000
.word 0xf900c7a0
.word 0xf900cba0
.word 0xf900cfa0
.word 0xf900d3a0
.word 0x9105a3a0
.word 0xd2800000
.word 0xf900b7a0
.word 0xf900bba0
.word 0xf900bfa0
.word 0xf900c3a0
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xd2800013
.word 0xf9402bb1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020
.word 0xaa1903e0
.word 0xd2800021
bl _p_32
.word 0xf9402bb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #728]
bl _p_17
.word 0xf90117a0
bl _p_33
.word 0xf9402bb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94117a0
.word 0xaa0003f8
.word 0xaa1803e2
.word 0xd2800020
.word 0xaa0203e0
.word 0xd2800021
.word 0xf9400042
.word 0xf941b050
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e2

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #736]
.word 0xaa0203e0
.word 0xf940005e
bl _p_34
.word 0xf9402bb1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9003720
.word 0x9101a321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402bb1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94027a0
.word 0xf90113a0
.word 0xd2800000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #744]
bl _p_17
.word 0xf94113a1
.word 0xf9010fa0
.word 0xd2800002
bl _p_35
.word 0xf9402bb1
.word 0xf9420231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410fa0
.word 0xf9002320
.word 0x91010321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402bb1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421c30
.word 0xd63f0200
.word 0xf9010ba0
.word 0xf9402bb1
.word 0xf9427231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba2

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #752]
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941e450
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf90107a0
.word 0xf9402bb1
.word 0xf942de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94107a1
.word 0x910523a0
.word 0xf900d7a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf940d7be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9402bb1
.word 0xf9431e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910523a0
.word 0x910623a0
.word 0xf940a7a0
.word 0xf900c7a0
.word 0xf940aba0
.word 0xf900cba0
.word 0xf940afa0
.word 0xf900cfa0
.word 0xf940b3a0
.word 0xf900d3a0
.word 0xf9402bb1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0x910623a0
.word 0xf900ffa0
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd0103a0
.word 0xf9402bb1
.word 0xf9437e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940ffa0
.word 0xfd4103a0
bl _p_36
.word 0xf9402bb1
.word 0xf9439a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf943aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_13
.word 0x93407c00
.word 0xf900fba0
.word 0xf9402bb1
.word 0xf943ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940fba0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x540008e1
.word 0xf9402bb1
.word 0xf943f231
.word 0xb4000051
.word 0xd63f0220
.word 0x910623a0
.word 0xf900fba0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf9010ba0
.word 0xf9402bb1
.word 0xf9441a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba1
.word 0x9104a3a0
.word 0xf900d7a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf940d7be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9402bb1
.word 0xf9445a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9104a3a0
.word 0x9105a3a0
.word 0xf94097a0
.word 0xf900b7a0
.word 0xf9409ba0
.word 0xf900bba0
.word 0xf9409fa0
.word 0xf900bfa0
.word 0xf940a3a0
.word 0xf900c3a0
.word 0x9105a3a0
bl _p_37
.word 0xfd0103a0
.word 0xf9402bb1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd011fa0
.word 0xf9402bb1
.word 0xf944be31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4103a0
.word 0xfd411fa1
.word 0x1e613800
.word 0xfd011ba0
.word 0xf9402bb1
.word 0xf944de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940fba0
.word 0xfd411ba0
bl _p_38
.word 0xf9402bb1
.word 0xf944fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9451a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9454631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a1
.word 0x910623a0
.word 0x9102e3a0
.word 0xf940c7a0
.word 0xf9005fa0
.word 0xf940cba0
.word 0xf90063a0
.word 0xf940cfa0
.word 0xf90067a0
.word 0xf940d3a0
.word 0xf9006ba0
.word 0xaa0103e0
.word 0x9102e3a2
.word 0xfd405fa0
.word 0xfd4063a1
.word 0xfd4067a2
.word 0xfd406ba3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf945a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf945b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf90117a0
.word 0xf9402bb1
.word 0xf945de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf90113a0
.word 0xf9402bb1
.word 0xf9460a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94113a1
.word 0xf94117a2
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9425450
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf9463231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9464231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
bl _p_39
.word 0xf9010fa0
.word 0xf9402bb1
.word 0xf9465e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410fa1
.word 0xaa1903e0
bl _p_40
.word 0xf9402bb1
.word 0xf9467a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9468a31
.word 0xb4000051
.word 0xd63f0220
bl _p_28
.word 0xf9010ba0
.word 0xf9402bb1
.word 0xf946a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf940e430
.word 0xd63f0200
.word 0xf90107a0
.word 0xf9402bb1
.word 0xf946ca31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #760]
.word 0xd2800401
.word 0xd2800401
bl _p_1
.word 0xf94107a1
.word 0xf90123a0
bl _p_41
.word 0xf9402bb1
.word 0xf946fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0xaa0003f7
.word 0xf9402bb1
.word 0xf9471631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_42
.word 0x93407c00
.word 0xf900ffa0
.word 0xf9402bb1
.word 0xf9474231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940ffa0
.word 0xd2800101
.word 0xd280011e
.word 0x6b1e001f
.word 0x9a9fa7e0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x3902ff20
.word 0xf9402bb1
.word 0xf9477631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1703e1
.word 0xaa0103e0
.word 0xf940003e
bl _p_42
.word 0x93407c00
.word 0xf900fba0
.word 0xf9402bb1
.word 0xf947a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940fba0
.word 0xd28000e1
.word 0xd28000fe
.word 0x6b1e001f
.word 0x9a9fa7e0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x3902fb20
.word 0xf9402bb1
.word 0xf947d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x3942fb20
.word 0x34000ee0
.word 0xf9402bb1
.word 0xf947f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421c30
.word 0xd63f0200
.word 0xf900fba0
.word 0xf9402bb1
.word 0xf9481e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd012ba0
.word 0xf9402bb1
.word 0xf9483e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd012fa0
.word 0xf9402bb1
.word 0xf9485e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2802800
.word 0xd2802800
bl _p_14
.word 0xfd0133a0
.word 0xf9402bb1
.word 0xf9487e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd28002c0
.word 0xd28002c0
bl _p_14
.word 0xfd0137a0
.word 0xf9402bb1
.word 0xf9489e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd412ba0
.word 0xfd412fa1
.word 0xfd4133a2
.word 0xfd4137a3
.word 0x910423a0
.word 0xd2800000
.word 0xf90087a0
.word 0xf9008ba0
.word 0xf9008fa0
.word 0xf90093a0
.word 0x910423a0
bl _p_43
.word 0x910423a0
.word 0x910263a0
.word 0xf94087a0
.word 0xf9004fa0
.word 0xf9408ba0
.word 0xf90053a0
.word 0xf9408fa0
.word 0xf90057a0
.word 0xf94093a0
.word 0xf9005ba0
.word 0xf9402bb1
.word 0xf9490631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #768]
bl _p_17
.word 0xf90107a0
.word 0x910263a1
.word 0xfd404fa0
.word 0xfd4053a1
.word 0xfd4057a2
.word 0xfd405ba3
bl _p_44
.word 0xf9402bb1
.word 0xf9494231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94107a0
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xf90123a0
bl _p_45
.word 0xf900ffa0
.word 0xf9402bb1
.word 0xf9496a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940ffa1
.word 0xf94123a2
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941c850
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf9499231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940fba2
.word 0xaa1603e1
.word 0xaa0203e0
.word 0xf9400042
.word 0xf942c850
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf949ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf949da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421c30
.word 0xd63f0200
.word 0xf90113a0
.word 0xf9402bb1
.word 0xf94a0631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd013fa0
.word 0xf9402bb1
.word 0xf94a2631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd0143a0
.word 0xf9402bb1
.word 0xf94a4631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800c80
.word 0xd2800c80
bl _p_14
.word 0xfd0147a0
.word 0xf9402bb1
.word 0xf94a6631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800c80
.word 0xd2800c80
bl _p_14
.word 0xfd014ba0
.word 0xf9402bb1
.word 0xf94a8631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd413fa0
.word 0xfd4143a1
.word 0xfd4147a2
.word 0xfd414ba3
.word 0x9103a3a0
.word 0xd2800000
.word 0xf90077a0
.word 0xf9007ba0
.word 0xf9007fa0
.word 0xf90083a0
.word 0x9103a3a0
bl _p_43
.word 0x9103a3a0
.word 0x9101e3a0
.word 0xf94077a0
.word 0xf9003fa0
.word 0xf9407ba0
.word 0xf90043a0
.word 0xf9407fa0
.word 0xf90047a0
.word 0xf94083a0
.word 0xf9004ba0
.word 0xf9402bb1
.word 0xf94aee31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #768]
bl _p_17
.word 0xf9013ba0
.word 0x9101e3a1
.word 0xfd403fa0
.word 0xfd4043a1
.word 0xfd4047a2
.word 0xfd404ba3
bl _p_44
.word 0xf9402bb1
.word 0xf94b2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba0
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xf90127a0
bl _p_45
.word 0xf90117a0
.word 0xf9402bb1
.word 0xf94b5231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94117a1
.word 0xf94127a2
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941c850
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94b7a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94113a2
.word 0xaa1603e1
.word 0xaa0203e0
.word 0xf9400042
.word 0xf942cc50
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94ba231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94bb231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421c30
.word 0xd63f0200
.word 0xf9010fa0
.word 0xf9402bb1
.word 0xf94bde31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410fa2
.word 0xd2800000
.word 0xaa0203e0
.word 0xd2800001
.word 0xf9400042
.word 0xf9427050
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94c0a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94c1a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #768]
bl _p_17
.word 0xf9010ba0
bl _p_46
.word 0xf9402bb1
.word 0xf94c4631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba0
.word 0xaa0003f6
.word 0xaa1603e2

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #776]
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941e050
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94c7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e2
.word 0xd2800020
.word 0xaa0203e0
.word 0xd2800021
.word 0xf9400042
.word 0xf9419450
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94caa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9002f20
.word 0x91016321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402bb1
.word 0xf94cee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f20
.word 0xf90107a0
bl _p_45
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf94d1231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a1
.word 0xf94107a2
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941c850
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94d3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94d4a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f21
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9419030
.word 0xd63f0200
.word 0xf900ffa0
.word 0xf9402bb1
.word 0xf94d7631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_13
.word 0x93407c00
.word 0xf900fba0
.word 0xf9402bb1
.word 0xf94d9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940fba0
.word 0xf940ffa1
.word 0xf900dba1
.word 0x35000120
.word 0xf940dba0
.word 0x92800081
.word 0xf2bfffe1
.word 0xf900dba0
.word 0x9280009e
.word 0xf2bffffe
.word 0xb901bbbe
.word 0x14000006
.word 0xf940dba0
.word 0xd28000a1
.word 0xf900dba0
.word 0xd28000be
.word 0xb901bbbe
.word 0xf940dba0
.word 0xf90157a0
.word 0xb981bba0
bl _p_14
.word 0xfd014ba0
.word 0xf9402bb1
.word 0xf94e0231
.word 0xb4000051
.word 0xd63f0220
.word 0x92800000
.word 0xf2bfffe0
.word 0x92800000
.word 0xf2bfffe0
bl _p_14
.word 0xfd015ba0
.word 0xf9402bb1
.word 0xf94e2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd414ba0
.word 0xfd415ba1
.word 0x910363a0
.word 0xd2800000
.word 0xf9006fa0
.word 0xf90073a0
.word 0x910363a0
bl _p_15
.word 0x910363a0
.word 0x9101a3a0
.word 0xf9406fa0
.word 0xf90037a0
.word 0xf94073a0
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf94e7231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94157a1
.word 0xaa0103e0
.word 0x9101a3a2
.word 0xfd4037a0
.word 0xfd403ba1
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94ea231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94eb231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f21
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9419030
.word 0xd63f0200
.word 0xf9014fa0
.word 0xf9402bb1
.word 0xf94ede31
.word 0xb4000051
.word 0xd63f0220
bl _p_39
.word 0xf90153a0
.word 0xf9402bb1
.word 0xf94ef631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94153a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf940ec30
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf94f1e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba1
.word 0xf9414fa2
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9414850
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94f4631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94f5631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f21
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9419030
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf94f8231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a1
.word 0xd280001e
.word 0xf2a7e81e
.word 0x9e6703d0
.word 0x1e22c200
.word 0xaa0103e0
.word 0x1e624000
.word 0xf9400021
.word 0xf9414030
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf94fba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94fca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #784]
bl _p_17
.word 0xf90117a0
bl _p_47
.word 0xf9402bb1
.word 0xf94ff631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94117a0
.word 0xf9001f20
.word 0x9100e321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402bb1
.word 0xf9503a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401f20
.word 0xf90113a0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #792]
.word 0xaa0003f5
.word 0xaa1903e0
.word 0xf9401f22
.word 0xaa1503e0
.word 0xaa0203e0
.word 0xaa1503e1
.word 0xf9400042
.word 0xf941e450
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf9508631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94113a2
.word 0xaa1503e1
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941ec50
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf950ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf950be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401f22

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #800]
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941e050
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf950f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9510231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401f20
.word 0xf9010fa0
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x54002e60

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #808]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf9410fa2
.word 0xf9001039
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #816]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #824]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #832]
.word 0xf9401403
.word 0xf9000c23
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa0203e0
.word 0xf940005e
bl _p_48
.word 0xf9402bb1
.word 0xf951ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf951de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020
.word 0xaa1903e0
.word 0xd2800021
bl _p_49
.word 0xf9402bb1
.word 0xf9520231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9521231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0xaa1903e0
.word 0xd2800001
bl _p_50
.word 0xf9402bb1
.word 0xf9523631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9524631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9526e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x54002340

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xf9001019
.word 0x91008001
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #840]
.word 0xf9001401

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #848]
.word 0xf9002001

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #856]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf9010ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #864]
bl _p_17
.word 0xf9410ba1
.word 0xf90107a0
bl _p_51
.word 0xf9402bb1
.word 0xf9533e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94107a0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf900ffa0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_13
.word 0x93407c00
.word 0xf900fba0
.word 0xf9402bb1
.word 0xf9537231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940fba0
.word 0xf940ffa1
.word 0xf94123a2
.word 0xf900dba2
.word 0xf900e3b9
.word 0xf900e7a1
.word 0x35000140
.word 0xf940dba3
.word 0xf940e3a2
.word 0xf940e7a1
.word 0xd2800040
.word 0xf900dba3
.word 0xf900e3a2
.word 0xf900e7a1
.word 0xf900eba0
.word 0x14000009
.word 0xf940dba3
.word 0xf940e3a2
.word 0xf940e7a1
.word 0xd2800100
.word 0xf900dba3
.word 0xf900e3a2
.word 0xf900e7a1
.word 0xf900eba0
.word 0xf940dba0
.word 0xf9010fa0
.word 0xf940e3a0
.word 0xf9010ba0
.word 0xf940e7a2
.word 0xf940eba1
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9412050
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf9540a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba1
.word 0xf9410fa2
.word 0xaa1403e0
.word 0xf900efb4
.word 0xf940efa0
.word 0xf940efa3
.word 0xaa0303f4
.word 0xf9004820
.word 0x91024021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030021
.word 0xd280003e
.word 0x3900003e
.word 0xaa1403e0
.word 0xaa0203e0
.word 0xaa1403e1
.word 0xf9400042
.word 0xf9425850
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf9547e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9548e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf900ffa0
.word 0xf9402bb1
.word 0xf954b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x54001100

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #872]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xf9001019
.word 0x91008001
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #880]
.word 0xf9001401

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #888]
.word 0xf9002001

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #896]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf90123a0
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x54000c00

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #904]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xf9001019
.word 0x91008001
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #912]
.word 0xf9001401

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #920]
.word 0xf9002001

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #928]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf90107a0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #936]
bl _p_17
.word 0xf94123a1
.word 0xf94107a2
.word 0xf900fba0
bl _p_52
.word 0xf9402bb1
.word 0xf9562a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940fba0
.word 0xf940ffa2
.word 0xf900f3a0
.word 0xf940f3a0
.word 0xf940f3a1
.word 0xaa0103f3
.word 0xf9004720
.word 0x91022321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030021
.word 0xd280003e
.word 0x3900003e
.word 0xaa1303e0
.word 0xaa0203e0
.word 0xaa1303e1
.word 0xf9400042
.word 0xf9425850
.word 0xd63f0200
.word 0xf9402bb1
.word 0xf9569a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf956aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf956ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xf94023b9
.word 0x910003bf
.word 0xa9407bfd
.word 0xd2805810
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_2a:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_CloseButtonTapped_object_System_EventArgs
FlyoutNavigation_FlyoutNavigationController_CloseButtonTapped_object_System_EventArgs:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #944]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94017b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_25
.word 0xf94017b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_2b:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_add_ShouldReceiveTouch_UIKit_UITouchEventArgs
FlyoutNavigation_FlyoutNavigationController_add_ShouldReceiveTouch_UIKit_UITouchEventArgs:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #952]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800018
.word 0xd2800017
.word 0xf94023b1
.word 0xf9404a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9404f20
.word 0xaa0003f8
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803f7
.word 0xf94023b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x540009e0
.word 0x91026336
.word 0xaa1803e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1a03e1
bl _p_5
.word 0xaa0003f5
.word 0xf94023b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xb4000175
.word 0xf94002a0
.word 0xf9400000
.word 0xf9400800
.word 0xf9400c00

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #248]
.word 0xeb01001f
.word 0x10000011
.word 0x540006c1
.word 0xaa1503e0
.word 0xaa1803e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #960]
.word 0xc85f7ed0
.word 0xeb18021f
.word 0x54000061
.word 0xc811fed5
.word 0x35ffff91
.word 0xd50330bf
.word 0xaa1003e0
.word 0xf90037a0
.word 0xd349fec0
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94023b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf90033a0
.word 0xaa0003f8
.word 0xf94023b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003e1
.word 0xaa1703e1
.word 0xeb17001f
.word 0x54fff641
.word 0xf94023b1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf941d231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_7
.word 0xd2801e60
.word 0xaa1103e1
bl _p_7

Lme_2c:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs
FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #968]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800018
.word 0xd2800017
.word 0xf94023b1
.word 0xf9404a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9404f20
.word 0xaa0003f8
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803f7
.word 0xf94023b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x540009e0
.word 0x91026336
.word 0xaa1803e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1a03e1
bl _p_53
.word 0xaa0003f5
.word 0xf94023b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xb4000175
.word 0xf94002a0
.word 0xf9400000
.word 0xf9400800
.word 0xf9400c00

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #248]
.word 0xeb01001f
.word 0x10000011
.word 0x540006c1
.word 0xaa1503e0
.word 0xaa1803e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #960]
.word 0xc85f7ed0
.word 0xeb18021f
.word 0x54000061
.word 0xc811fed5
.word 0x35ffff91
.word 0xd50330bf
.word 0xaa1003e0
.word 0xf90037a0
.word 0xd349fec0
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94023b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf90033a0
.word 0xaa0003f8
.word 0xf94023b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003e1
.word 0xaa1703e1
.word 0xeb17001f
.word 0x54fff641
.word 0xf94023b1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf941d231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_7
.word 0xd2801e60
.word 0xaa1103e1
bl _p_7

Lme_2d:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_DisableGesture
FlyoutNavigation_FlyoutNavigationController_get_DisableGesture:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #976]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x39430000
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_2e:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_DisableGesture_bool
FlyoutNavigation_FlyoutNavigationController_set_DisableGesture_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #984]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x394063a1
.word 0x39030001
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_2f:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_shouldReceiveTouch_UIKit_UIGestureRecognizer_UIKit_UITouch
FlyoutNavigation_FlyoutNavigationController_shouldReceiveTouch_UIKit_UIGestureRecognizer_UIKit_UITouch:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf90013a2

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #992]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94017b1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xf9404700
.word 0xeb00033f
.word 0x540003e1
.word 0xf94017b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_54
.word 0x53001c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x35000200
.word 0xf94017b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x14000056
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_55
.word 0x53001c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x34000200
.word 0xf94017b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x14000034
.word 0xf94017b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9404f00
.word 0xb40003e0
.word 0xf94017b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9404f03
.word 0xaa1903e0
.word 0xf94013a2
.word 0xaa0303e0
.word 0xaa1903e1
.word 0xf90027a3
.word 0xf9400c70
.word 0xd63f0200
.word 0xf94027a1
.word 0x53001c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x1400000f
.word 0xf94017b1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xf94017b1
.word 0xf9424e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_30:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_ViewDidLayoutSubviews
FlyoutNavigation_FlyoutNavigationController_ViewDidLayoutSubviews:
.loc 1 1 0
.word 0xa9a97bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1000]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0x910443a0
.word 0xd2800000
.word 0xf9008ba0
.word 0xf9008fa0
.word 0xf90093a0
.word 0xf90097a0
.word 0x9103c3a0
.word 0xd2800000
.word 0xf9007ba0
.word 0xf9007fa0
.word 0xf90083a0
.word 0xf90087a0
.word 0xf9400fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_56
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x910343a0
.word 0xf9009ba0
.word 0xaa1a03e0
bl _p_57
.word 0xf9409bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9400fb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0x910343a0
.word 0x910443a0
.word 0xf9406ba0
.word 0xf9008ba0
.word 0xf9406fa0
.word 0xf9008fa0
.word 0xf94073a0
.word 0xf90093a0
.word 0xf94077a0
.word 0xf90097a0
.word 0xf9400fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0x910443a0
.word 0xf900a7a0
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd00aba0
.word 0xf9400fb1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a7a0
.word 0xfd40aba0
bl _p_36
.word 0xf9400fb1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_13
.word 0x93407c00
.word 0xf900a3a0
.word 0xf9400fb1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x540008e1
.word 0xf9400fb1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0x910443a0
.word 0xf900a3a0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf900b7a0
.word 0xf9400fb1
.word 0xf941e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940b7a1
.word 0x9102c3a0
.word 0xf9009ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9409bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9400fb1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
.word 0x9103c3a0
.word 0xf9405ba0
.word 0xf9007ba0
.word 0xf9405fa0
.word 0xf9007fa0
.word 0xf94063a0
.word 0xf90083a0
.word 0xf94067a0
.word 0xf90087a0
.word 0x9103c3a0
bl _p_37
.word 0xfd00aba0
.word 0xf9400fb1
.word 0xf9426a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd00b3a0
.word 0xf9400fb1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40aba0
.word 0xfd40b3a1
.word 0x1e613800
.word 0xfd00afa0
.word 0xf9400fb1
.word 0xf942aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xfd40afa0
bl _p_38
.word 0xf9400fb1
.word 0xf942c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402341
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf900a7a0
.word 0xf9400fb1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a7a1
.word 0x910243a0
.word 0xf9009ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9409bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9400fb1
.word 0xf9435231
.word 0xb4000051
.word 0xd63f0220
.word 0x910443a0
.word 0x910143a0
.word 0xf9408ba0
.word 0xf9002ba0
.word 0xf9408fa0
.word 0xf9002fa0
.word 0xf94093a0
.word 0xf90033a0
.word 0xf94097a0
.word 0xf90037a0
.word 0x910243a0
.word 0xfd404ba0
.word 0xfd404fa1
.word 0xfd4053a2
.word 0xfd4057a3
.word 0x910143a0
.word 0xfd402ba4
.word 0xfd402fa5
.word 0xfd4033a6
.word 0xfd4037a7
bl _p_58
.word 0x53001c00
.word 0xf900a3a0
.word 0xf9400fb1
.word 0xf943be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x34000500
.word 0xf9400fb1
.word 0xf943d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402341
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf900a3a0
.word 0xf9400fb1
.word 0xf9440231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a1
.word 0x910443a0
.word 0x9100c3a0
.word 0xf9408ba0
.word 0xf9001ba0
.word 0xf9408fa0
.word 0xf9001fa0
.word 0xf94093a0
.word 0xf90023a0
.word 0xf94097a0
.word 0xf90027a0
.word 0xaa0103e0
.word 0x9100c3a2
.word 0xfd401ba0
.word 0xfd401fa1
.word 0xfd4023a2
.word 0xfd4027a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf9446231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9448231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x3942ef40
.word 0x35001040
.word 0xf9400fb1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020
.word 0xd280003e
.word 0x3902ef5e
.word 0xf9400fb1
.word 0xf944be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_30
.word 0x53001c00
.word 0xf900a3a0
.word 0xf9400fb1
.word 0xf944e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x34000700
.word 0xf9400fb1
.word 0xf944fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9418030
.word 0xd63f0200
.word 0xf900a3a0
.word 0xf9400fb1
.word 0xf9452231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xd2800061
.word 0xd280007e
.word 0xeb1e001f
.word 0x54000280
.word 0xf9400fb1
.word 0xf9454631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9418030
.word 0xd63f0200
.word 0xf900a3a0
.word 0xf9400fb1
.word 0xf9456e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xd2800081
.word 0xd280009e
.word 0xeb1e001f
.word 0x54000241
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf945a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020
.word 0xaa1a03e0
.word 0xd2800021
bl _p_50
.word 0xf9400fb1
.word 0xf945c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf945e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x3942a340
.word 0x34000520
.word 0xf9400fb1
.word 0xf9460231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf900a3a0
.word 0xf9400fb1
.word 0xf9462631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a1
.word 0x9101c3a0
.word 0xf9009ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9409bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9400fb1
.word 0xf9466631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x9101c3a1
.word 0xfd403ba0
.word 0xfd403fa1
.word 0xfd4043a2
.word 0xfd4047a3
bl _p_59
.word 0xf9400fb1
.word 0xf9469231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf946b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf946c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8d77bfd
.word 0xd65f03c0

Lme_31:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer
FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer:
.loc 1 1 0
.word 0xa9a77bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xaa0003f9
.word 0xf90023a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1008]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xd2800018
.word 0x910403a0
.word 0xd2800000
.word 0xf90083a0
.word 0xf90087a0
.word 0xf9008ba0
.word 0xf9008fa0
.word 0x9e6703e0
.word 0xfd0093a0
.word 0x9103c3a0
.word 0xd2800000
.word 0xf9007ba0
.word 0xf9007fa0
.word 0x9e6703e0
.word 0xfd0097a0
.word 0x910383a0
.word 0xd2800000
.word 0xf90073a0
.word 0xf90077a0
.word 0x9e6703e0
.word 0xfd009ba0
.word 0xd2800017
.word 0xf94027b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b6
.word 0xaa1603f5
.word 0xeb1f02df
.word 0x54000160
.word 0xf94002c0
.word 0xf9400000
.word 0xf9400800
.word 0xf9400c00

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #1016]
.word 0xeb01001f
.word 0x54000040
.word 0xd2800015
.word 0xaa1503e0
.word 0xaa1503f8
.word 0xf94027b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_60
.word 0x53001c00
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x350001e0
.word 0xf94027b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xb5000140
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000358
.word 0xf94027b1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_61
.word 0x53001c00
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf941c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x350004a0
.word 0xf94027b1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf900aba0
.word 0xf94027b1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f20
.word 0xf900a3a0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf900a7a0
.word 0xf94027b1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a1
.word 0xf940a7a2
.word 0xf940aba3
.word 0xaa0303e0
.word 0xf9400063
.word 0xf9422470
.word 0xd63f0200
.word 0xf94027b1
.word 0xf9425e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9427e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf900b7a0
.word 0xf94027b1
.word 0xf942aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940b7a2
.word 0xd2800000
.word 0xaa0203e0
.word 0xd2800001
.word 0xf9400042
.word 0xf9419850
.word 0xd63f0200
.word 0xf94027b1
.word 0xf942d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf900b3a0
.word 0xf94027b1
.word 0xf9430631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940b3a1
.word 0x910303a0
.word 0xf9009fa0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9409fbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94027b1
.word 0xf9434631
.word 0xb4000051
.word 0xd63f0220
.word 0x910303a0
.word 0x910403a0
.word 0xf94063a0
.word 0xf90083a0
.word 0xf94067a0
.word 0xf90087a0
.word 0xf9406ba0
.word 0xf9008ba0
.word 0xf9406fa0
.word 0xf9008fa0
.word 0xf94027b1
.word 0xf9437e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f21
.word 0x910403a0
.word 0x910203a0
.word 0xf94083a0
.word 0xf90043a0
.word 0xf94087a0
.word 0xf90047a0
.word 0xf9408ba0
.word 0xf9004ba0
.word 0xf9408fa0
.word 0xf9004fa0
.word 0xaa0103e0
.word 0x910203a2
.word 0xfd4043a0
.word 0xfd4047a1
.word 0xfd404ba2
.word 0xfd404fa3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf94027b1
.word 0xf943e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf943f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf900aba0
.word 0xf94027b1
.word 0xf9441e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940aba1
.word 0x9102c3a0
.word 0xf9009fa0
.word 0xaa1803e0
.word 0xf9400302
.word 0xf9411c50
.word 0xd63f0200
.word 0xf9409fbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xf94027b1
.word 0xf9445631
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
.word 0x9103c3a0
.word 0xf9405ba0
.word 0xf9007ba0
.word 0xf9405fa0
.word 0xf9007fa0
.word 0xf94027b1
.word 0xf9447e31
.word 0xb4000051
.word 0xd63f0220
.word 0x9103c3a0
bl _p_62
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf9449a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd0093a0
.word 0xf94027b1
.word 0xf944b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf940f830
.word 0xd63f0200
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf944da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xd2800021
.word 0xd280003e
.word 0xeb1e001f
.word 0x54000281
.word 0xf94027b1
.word 0xf944fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x910403a0
bl _p_24
.word 0xfd00bba0
.word 0xf94027b1
.word 0xf9451e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40bba0
.word 0xfd005b20
.word 0xf94027b1
.word 0xf9453631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000266
.word 0xf94027b1
.word 0xf9454a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf940f830
.word 0xd63f0200
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf9457231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xd2800041
.word 0xd280005e
.word 0xeb1e001f
.word 0x54002581
.word 0xf94027b1
.word 0xf9459631
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
.word 0xf900a7a0
.word 0xfd4093a0
.word 0xaa1903e0
.word 0xfd405b21
.word 0x1e612800
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf945c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a7a0
.word 0xfd40bfa0
bl _p_38
.word 0xf94027b1
.word 0xf945de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf945ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_13
.word 0x93407c00
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf9461231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x35000ea0
.word 0xf94027b1
.word 0xf9462a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
bl _p_24
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf9464631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf9466631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd40bfa1
.word 0x1e612000
.word 0x9a9f57e0
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf9468a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x34000360
.word 0xf94027b1
.word 0xf946a231
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
.word 0xf900a3a0
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf946ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xfd40afa0
bl _p_38
.word 0xf94027b1
.word 0xf946e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf946f631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000034
.word 0xf94027b1
.word 0xf9470a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
bl _p_24
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf9472631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf9474631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd40bfa1
.word 0x1e612000
.word 0x9a9fd7e0
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf9476a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x340002c0
.word 0xf94027b1
.word 0xf9478231
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
.word 0xf900a3a0
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf947aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xfd40afa0
bl _p_38
.word 0xf94027b1
.word 0xf947c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf947e631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000070
.word 0xf94027b1
.word 0xf947fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
bl _p_24
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf9481631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf9483631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd40bfa1
.word 0x1e612000
.word 0x9a9fd7e0
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf9485a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x34000360
.word 0xf94027b1
.word 0xf9487231
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
.word 0xf900a3a0
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf9489a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xfd40afa0
bl _p_38
.word 0xf94027b1
.word 0xf948b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf948c631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000038
.word 0xf94027b1
.word 0xf948da31
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
bl _p_24
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf948f631
.word 0xb4000051
.word 0xd63f0220
.word 0x928022e0
.word 0xf2bfffe0
.word 0x928022e0
.word 0xf2bfffe0
bl _p_14
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf9491e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd40bfa1
.word 0x1e612000
.word 0x9a9f57e0
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf9494231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0x34000300
.word 0xf94027b1
.word 0xf9495a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
.word 0xf900a3a0
.word 0x928022e0
.word 0xf2bfffe0
.word 0x928022e0
.word 0xf2bfffe0
bl _p_14
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf9498a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xfd40afa0
bl _p_38
.word 0xf94027b1
.word 0xf949a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf949c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x910403a0
.word 0x910183a0
.word 0xf94083a0
.word 0xf90033a0
.word 0xf94087a0
.word 0xf90037a0
.word 0xf9408ba0
.word 0xf9003ba0
.word 0xf9408fa0
.word 0xf9003fa0
.word 0xaa1903e0
.word 0x910183a1
.word 0xfd4033a0
.word 0xfd4037a1
.word 0xfd403ba2
.word 0xfd403fa3
bl _p_63
.word 0xf94027b1
.word 0xf94a1e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf94a2e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000128
.word 0xf94027b1
.word 0xf94a4231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf940f830
.word 0xd63f0200
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf94a6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xd2800061
.word 0xd280007e
.word 0xeb1e001f
.word 0x540022a1
.word 0xf94027b1
.word 0xf94a8e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf900b7a0
.word 0xf94027b1
.word 0xf94aba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940b7a1
.word 0x910283a0
.word 0xf9009fa0
.word 0xaa1803e0
.word 0xf9400302
.word 0xf9411850
.word 0xd63f0200
.word 0xf9409fbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xf94027b1
.word 0xf94af231
.word 0xb4000051
.word 0xd63f0220
.word 0x910283a0
.word 0x910383a0
.word 0xf94053a0
.word 0xf90073a0
.word 0xf94057a0
.word 0xf90077a0
.word 0xf94027b1
.word 0xf94b1a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910383a0
bl _p_62
.word 0xfd00c3a0
.word 0xf94027b1
.word 0xf94b3631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40c3a0
.word 0xfd0097a0
.word 0xf94027b1
.word 0xf94b4e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4093a0
.word 0xaa1903e0
.word 0xfd405b21
.word 0x1e612800
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf94b7231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40bfa0
.word 0xfd009ba0
.word 0xf94027b1
.word 0xf94b8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4097a0
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf94ba231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
bl _p_64
.word 0xfd00bba0
.word 0xf94027b1
.word 0xf94bbe31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40bba0
.word 0xd280001e
.word 0xf2c8001e
.word 0xf2e811fe
.word 0x9e6703c1
.word 0x1e612000
.word 0x54000380
.word 0x5400036b
.word 0xf94027b1
.word 0xf94bee31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4097a0
.word 0xfd00afa0
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf94c1631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd40bfa1
.word 0x1e612000
.word 0x9a9fd7e0
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf94c3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xaa0003f4
.word 0x1400001a
.word 0xf94027b1
.word 0xf94c5631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd409ba0
.word 0xfd00afa0
.word 0xd2801180
.word 0xd2801180
bl _p_14
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf94c7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd40bfa1
.word 0x1e612000
.word 0x9a9fd7e0
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf94ca231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xaa1403f7
.word 0xf94027b1
.word 0xf94cc231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_13
.word 0x93407c00
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf94ce631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000a21
.word 0xf94027b1
.word 0xf94d0a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4097a0
.word 0xfd00afa0
.word 0xf94027b1
.word 0xf94d2231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
bl _p_64
.word 0xfd00bba0
.word 0xf94027b1
.word 0xf94d3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40bba0
.word 0xd280001e
.word 0xf2c8001e
.word 0xf2e811fe
.word 0x9e6703c1
.word 0x1e612000
.word 0x54000380
.word 0x5400036b
.word 0xf94027b1
.word 0xf94d6e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4097a0
.word 0xfd00afa0
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf94d9631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd40bfa1
.word 0x1e612000
.word 0x9a9f57e0
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf94dba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xaa0003f4
.word 0x1400001c
.word 0xf94027b1
.word 0xf94dd631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd409ba0
.word 0xfd00afa0
.word 0x92801160
.word 0xf2bfffe0
.word 0x92801160
.word 0xf2bfffe0
bl _p_14
.word 0xfd00bfa0
.word 0xf94027b1
.word 0xf94e0631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40afa0
.word 0xfd40bfa1
.word 0x1e612000
.word 0x9a9f57e0
.word 0xf900a3a0
.word 0xf94027b1
.word 0xf94e2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xaa1403f7
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf94e5a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x34000237
.word 0xf94027b1
.word 0xf94e7231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_26
.word 0xf94027b1
.word 0xf94e8e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf94e9e31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400000c
.word 0xf94027b1
.word 0xf94eb231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_25
.word 0xf94027b1
.word 0xf94ece31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf94eee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf94efe31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0x910003bf
.word 0xa8d97bfd
.word 0xd65f03c0

Lme_32:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_ViewWillAppear_bool
FlyoutNavigation_FlyoutNavigationController_ViewWillAppear_bool:
.loc 1 1 0
.word 0xa9a67bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1024]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x9104a3a0
.word 0xd2800000
.word 0xf90097a0
.word 0xf9009ba0
.word 0xf9009fa0
.word 0xf900a3a0
.word 0x910423a0
.word 0xd2800000
.word 0xf90087a0
.word 0xf9008ba0
.word 0xf9008fa0
.word 0xf90093a0
.word 0x9103a3a0
.word 0xd2800000
.word 0xf90077a0
.word 0xf9007ba0
.word 0xf9007fa0
.word 0xf90083a0
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf900b7a0
.word 0xf94013b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940b7a1
.word 0x910323a0
.word 0xf900a7a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf940a7be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910323a0
.word 0x9104a3a0
.word 0xf94067a0
.word 0xf90097a0
.word 0xf9406ba0
.word 0xf9009ba0
.word 0xf9406fa0
.word 0xf9009fa0
.word 0xf94073a0
.word 0xf900a3a0
.word 0xf94013b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0x9104a3a0
.word 0xf900afa0
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd00b3a0
.word 0xf94013b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940afa0
.word 0xfd40b3a0
bl _p_36
.word 0xf94013b1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_13
.word 0x93407c00
.word 0xf900aba0
.word 0xf94013b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940aba0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x540008e1
.word 0xf94013b1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0x9104a3a0
.word 0xf900aba0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf900c3a0
.word 0xf94013b1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940c3a1
.word 0x9102a3a0
.word 0xf900a7a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf940a7be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94013b1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9102a3a0
.word 0x910423a0
.word 0xf94057a0
.word 0xf90087a0
.word 0xf9405ba0
.word 0xf9008ba0
.word 0xf9405fa0
.word 0xf9008fa0
.word 0xf94063a0
.word 0xf90093a0
.word 0x910423a0
bl _p_37
.word 0xfd00b3a0
.word 0xf94013b1
.word 0xf9428e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2802300
.word 0xd2802300
bl _p_14
.word 0xfd00bfa0
.word 0xf94013b1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd40b3a0
.word 0xfd40bfa1
.word 0x1e613800
.word 0xfd00bba0
.word 0xf94013b1
.word 0xf942ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940aba0
.word 0xfd40bba0
bl _p_38
.word 0xf94013b1
.word 0xf942ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9430a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9104a3a0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #1032]
.word 0x9101e3a2
.word 0xf9400022
.word 0xf9003fa2
.word 0xf9400421
.word 0xf90043a1
.word 0x9101e3a1
.word 0xfd403fa0
.word 0xfd4043a1
bl _p_65
.word 0xf94013b1
.word 0xf9434e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9435e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf900cba0
.word 0xf94013b1
.word 0xf9438a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940cba1
.word 0x9104a3a0
.word 0x910163a0
.word 0xf94097a0
.word 0xf9002fa0
.word 0xf9409ba0
.word 0xf90033a0
.word 0xf9409fa0
.word 0xf90037a0
.word 0xf940a3a0
.word 0xf9003ba0
.word 0xaa0103e0
.word 0x910163a2
.word 0xfd402fa0
.word 0xfd4033a1
.word 0xfd4037a2
.word 0xfd403ba3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf94013b1
.word 0xf943ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf943fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf900b7a0
.word 0xf94013b1
.word 0xf9442231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_66
.word 0xf900c3a0
.word 0xf94013b1
.word 0xf9444231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940c3a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941cc30
.word 0xd63f0200
.word 0xf900c7a0
.word 0xf94013b1
.word 0xf9446a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940c7a1
.word 0xf940b7a2
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941c850
.word 0xd63f0200
.word 0xf94013b1
.word 0xf9449231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf944a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf900afa0
.word 0xf94013b1
.word 0xf944c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940afa1
.word 0x910223a0
.word 0xf900a7a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf940a7be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94013b1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0x910223a0
.word 0x9103a3a0
.word 0xf94047a0
.word 0xf90077a0
.word 0xf9404ba0
.word 0xf9007ba0
.word 0xf9404fa0
.word 0xf9007fa0
.word 0xf94053a0
.word 0xf90083a0
.word 0xf94013b1
.word 0xf9453a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_67
.word 0xf94013b1
.word 0xf9455631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9456631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x9103a3a0
.word 0x9100e3a0
.word 0xf94077a0
.word 0xf9001fa0
.word 0xf9407ba0
.word 0xf90023a0
.word 0xf9407fa0
.word 0xf90027a0
.word 0xf94083a0
.word 0xf9002ba0
.word 0xaa1903e0
.word 0x9100e3a1
.word 0xfd401fa0
.word 0xfd4023a1
.word 0xfd4027a2
.word 0xfd402ba3
bl _p_63
.word 0xf94013b1
.word 0xf945be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf945ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402320
.word 0xf900aba0
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x540008a0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1040]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf940aba2
.word 0xf9001039
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1048]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1056]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1064]
.word 0xf9401403
.word 0xf9000c23
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa0203e0
.word 0xf940005e
bl _p_68
.word 0xf94013b1
.word 0xf9469a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf946aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x394063a1
.word 0xaa1903e0
bl _p_69
.word 0xf94013b1
.word 0xf946ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf946da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf946ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8da7bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_33:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_ViewDidAppear_bool
FlyoutNavigation_FlyoutNavigationController_ViewDidAppear_bool:
.loc 1 1 0
.word 0xa9b57bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1072]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x9101e3a0
.word 0xd2800000
.word 0xf9003fa0
.word 0xf90043a0
.word 0xf90047a0
.word 0xf9004ba0
.word 0xf94013b1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x394063a1
.word 0xaa1903e0
bl _p_70
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf90053a0
.word 0xf94013b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0x910163a0
.word 0xf9004fa0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9404fbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0x910163a0
.word 0x9101e3a0
.word 0xf9402fa0
.word 0xf9003fa0
.word 0xf94033a0
.word 0xf90043a0
.word 0xf94037a0
.word 0xf90047a0
.word 0xf9403ba0
.word 0xf9004ba0
.word 0xf94013b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_67
.word 0xf94013b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x9101e3a0
.word 0x9100e3a0
.word 0xf9403fa0
.word 0xf9001fa0
.word 0xf94043a0
.word 0xf90023a0
.word 0xf94047a0
.word 0xf90027a0
.word 0xf9404ba0
.word 0xf9002ba0
.word 0xaa1903e0
.word 0x9100e3a1
.word 0xfd401fa0
.word 0xfd4023a1
.word 0xfd4027a2
.word 0xfd402ba3
bl _p_63
.word 0xf94013b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_34:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_ViewWillDisappear_bool
FlyoutNavigation_FlyoutNavigationController_ViewWillDisappear_bool:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1080]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x394063a1
.word 0xaa1903e0
bl _p_71
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402320
.word 0xf90023a0
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x54000720

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1040]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf94023a2
.word 0xf9001039
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1048]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1056]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1064]
.word 0xf9401403
.word 0xf9000c23
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa0203e0
.word 0xf940005e
bl _p_72
.word 0xf94013b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_35:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath
FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f9
.word 0xf90013a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1088]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800018
.word 0xf94017b1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94013a1
.word 0xaa1903e0
bl _p_73
.word 0x93407c00
.word 0xf90027a0
.word 0xf94017b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf90023a0
.word 0xaa0003f8
.word 0xf94017b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa1903e0
.word 0xaa0103e0
.word 0xaa1903e0
bl _p_74
.word 0xf94017b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_36:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int
FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int:
.loc 1 1 0
.word 0xa9b17bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1096]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800018
.word 0x910263a0
.word 0xd2800000
.word 0xf9004fa0
.word 0xf90053a0
.word 0xf90057a0
.word 0xf9005ba0
.word 0xf94023b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xb900af3a
.word 0xf94023b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9403b20
.word 0xb4000260
.word 0xf94023b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9403b20
.word 0xb9801800
.word 0xaa1a03e1
.word 0x6b1a001f
.word 0x5400012d
.word 0xf94023b1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800000
.word 0x6b1f035f
.word 0x5400064a
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_75
.word 0xf90063a0
.word 0xf94023b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xb40002e0
.word 0xf94023b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_75
.word 0xf90067a0
.word 0xf94023b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xaa0103e0
.word 0xf90063a1
.word 0xf9400c30
.word 0xd63f0200
.word 0xf94063a0
.word 0xf94023b1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x140001ed
.word 0xf94023b1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_76
.word 0xf90063a0
.word 0xf94023b1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa1a03e1
.word 0x93407f41
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54003c89
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400000
.word 0xb50005c0
.word 0xf94023b1
.word 0xf9422231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_75
.word 0xf90063a0
.word 0xf94023b1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xb40002e0
.word 0xf94023b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_75
.word 0xf90067a0
.word 0xf94023b1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xaa0103e0
.word 0xf90063a1
.word 0xf9400c30
.word 0xd63f0200
.word 0xf94063a0
.word 0xf94023b1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0x140001a8
.word 0xf94023b1
.word 0xf942d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_77
.word 0x53001c00
.word 0xf90063a0
.word 0xf94023b1
.word 0xf942fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x350004e0
.word 0xf94023b1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_60
.word 0x53001c00
.word 0xf90063a0
.word 0xf94023b1
.word 0xf9433631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x35000300
.word 0xf94023b1
.word 0xf9434e31
.word 0xb4000051
.word 0xd63f0220
bl _p_78
.word 0xf90063a0
.word 0xf94023b1
.word 0xf9436631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a3
.word 0xd2800000
.word 0xd2800020
.word 0xaa0303e0
.word 0xd2800001
.word 0xd2800022
.word 0xf9400063
.word 0xf9411870
.word 0xd63f0200
.word 0xf94023b1
.word 0xf9439a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf943ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800018
.word 0xf94023b1
.word 0xf943ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf90063a0
.word 0xf94023b1
.word 0xf943ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xb40004a0
.word 0xf94023b1
.word 0xf9440631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf90067a0
.word 0xf94023b1
.word 0xf9442631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421830
.word 0xd63f0200
.word 0xf94023b1
.word 0xf9444a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9445a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_54
.word 0x53001c00
.word 0xf90063a0
.word 0xf94023b1
.word 0xf9447e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f8
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf944a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_76
.word 0xf90063a0
.word 0xf94023b1
.word 0xf944ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_79
.word 0x93407c00
.word 0xf90067a0
.word 0xf94023b1
.word 0xf944ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf94067a1
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002429
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400001
.word 0xaa1903e0
bl _p_80
.word 0xf94023b1
.word 0xf9453231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9454231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x9101e3a0
.word 0xf9005fa0
.word 0xaa1903e0
bl _p_57
.word 0xf9405fbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94023b1
.word 0xf9457a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9101e3a0
.word 0x910263a0
.word 0xf9403fa0
.word 0xf9004fa0
.word 0xf94043a0
.word 0xf90053a0
.word 0xf94047a0
.word 0xf90057a0
.word 0xf9404ba0
.word 0xf9005ba0
.word 0xf94023b1
.word 0xf945b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x35000218
.word 0xf94023b1
.word 0xf945ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_60
.word 0x53001c00
.word 0xf90063a0
.word 0xf94023b1
.word 0xf945ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x34000660
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9461631
.word 0xb4000051
.word 0xd63f0220
.word 0x910263a0
.word 0xf90067a0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_13
.word 0x93407c00
.word 0xf90063a0
.word 0xf94023b1
.word 0xf9464231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf94067a1
.word 0xaa0103f7
.word 0x350000c0
.word 0xaa1703e0
.word 0xd2802300
.word 0xaa1703f6
.word 0xd2802315
.word 0x14000007
.word 0xaa1703e0
.word 0x928022e0
.word 0xf2bfffe0
.word 0xaa1703f6
.word 0x928022f5
.word 0xf2bffff5
.word 0xaa1603e0
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_14
.word 0xfd006ba0
.word 0xf94023b1
.word 0xf946a231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd406ba0
.word 0xaa1603e0
bl _p_38
.word 0xf94023b1
.word 0xf946be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf946de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf90073a0
.word 0xf94023b1
.word 0xf9470631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf9006fa0
.word 0xf94023b1
.word 0xf9472631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0xf94073a2
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9425450
.word 0xd63f0200
.word 0xf94023b1
.word 0xf9474e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9475e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_19
.word 0xf90067a0
.word 0xf94023b1
.word 0xf9478231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xaa1903e0
.word 0xf9400322
.word 0xf9421050
.word 0xd63f0200
.word 0xf94023b1
.word 0xf947a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf947b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_67
.word 0xf94023b1
.word 0xf947d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf947e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x910263a0
.word 0x910163a0
.word 0xf9404fa0
.word 0xf9002fa0
.word 0xf94053a0
.word 0xf90033a0
.word 0xf94057a0
.word 0xf90037a0
.word 0xf9405ba0
.word 0xf9003ba0
.word 0xaa1903e0
.word 0x910163a1
.word 0xfd402fa0
.word 0xfd4033a1
.word 0xfd4037a2
.word 0xfd403ba3
bl _p_63
.word 0xf94023b1
.word 0xf9483a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9484a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_60
.word 0x53001c00
.word 0xf90063a0
.word 0xf94023b1
.word 0xf9486e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x35000180
.word 0xf94023b1
.word 0xf9488631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_25
.word 0xf94023b1
.word 0xf948a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf948c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_75
.word 0xf90063a0
.word 0xf94023b1
.word 0xf948e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xb40002e0
.word 0xf94023b1
.word 0xf948fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_75
.word 0xf90067a0
.word 0xf94023b1
.word 0xf9491a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xaa0103e0
.word 0xf90063a1
.word 0xf9400c30
.word 0xd63f0200
.word 0xf94063a0
.word 0xf94023b1
.word 0xf9494231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9496231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9497231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_7

Lme_37:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_ShowMenu
FlyoutNavigation_FlyoutNavigationController_ShowMenu:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1104]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xb50000c0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000038
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xeb1f035f
.word 0x10000011
.word 0x540006e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf900103a
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1112]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1120]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1128]
.word 0xf9401402
.word 0xf9000c22
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa1a03e0
bl _p_22
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_38:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_setViewSize
FlyoutNavigation_FlyoutNavigationController_setViewSize:
.loc 1 1 0
.word 0xa9af7bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1136]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0x9102a3a0
.word 0xd2800000
.word 0xf90057a0
.word 0xf9005ba0
.word 0xf9005fa0
.word 0xf90063a0
.word 0xf9401bb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x910223a0
.word 0xf90067a0
.word 0xaa1a03e0
bl _p_57
.word 0xf94067be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401bb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0x910223a0
.word 0x9102a3a0
.word 0xf94047a0
.word 0xf90057a0
.word 0xf9404ba0
.word 0xf9005ba0
.word 0xf9404fa0
.word 0xf9005fa0
.word 0xf94053a0
.word 0xf90063a0
.word 0xf9401bb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0x9102a3a0
.word 0xaa0003f9
.word 0xaa1903e0
.word 0xf90077a0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_37
.word 0xfd007ba0
.word 0xf9401bb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_60
.word 0x53001c00
.word 0xf90073a0
.word 0xf9401bb1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf94077a1
.word 0xfd407ba0
.word 0xaa0103f8
.word 0xfd006ba0
.word 0x340000e0
.word 0xaa1803e0
.word 0xfd406ba0
.word 0xd2802300
.word 0xfd006ba0
.word 0xd2802317
.word 0x14000006
.word 0xaa1803e0
.word 0xfd406ba0
.word 0xd2800000
.word 0xfd006ba0
.word 0xd2800017
.word 0xaa1803e0
.word 0xfd406ba0
.word 0xfd007fa0
.word 0xaa1703e0
.word 0xaa1703e0
bl _p_14
.word 0xfd0083a0
.word 0xf9401bb1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd407fa0
.word 0xfd4083a1
.word 0x1e613800
.word 0xfd007ba0
.word 0xf9401bb1
.word 0xf941c631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd407ba0
.word 0xaa1803e0
bl _p_36
.word 0xf9401bb1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90077a0
.word 0xf9401bb1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a1
.word 0x9102a3a0
.word 0x910123a0
.word 0xf94057a0
.word 0xf90027a0
.word 0xf9405ba0
.word 0xf9002ba0
.word 0xf9405fa0
.word 0xf9002fa0
.word 0xf94063a0
.word 0xf90033a0
.word 0xaa0103e0
.word 0x910123a2
.word 0xfd4027a0
.word 0xfd402ba1
.word 0xfd402fa2
.word 0xfd4033a3
.word 0xf9400021
.word 0xf941c030
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf9427231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9428231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90073a0
.word 0xf9401bb1
.word 0xf942a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a1
.word 0x9101a3a0
.word 0xf90067a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf94067be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401bb1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x9101a3a1
.word 0xfd4037a0
.word 0xfd403ba1
.word 0xfd403fa2
.word 0xfd4043a3
bl _p_59
.word 0xf9401bb1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9432231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9433231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8d17bfd
.word 0xd65f03c0

Lme_39:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_GetViewBounds
FlyoutNavigation_FlyoutNavigationController_GetViewBounds:
.loc 1 1 0
.word 0xa9ad7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1144]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0x9102c3a0
.word 0xd2800000
.word 0xf9005ba0
.word 0xf9005fa0
.word 0xf90063a0
.word 0xf90067a0
.word 0x9e6703e0
.word 0xfd006ba0
.word 0x9e6703e0
.word 0xfd006fa0
.word 0xf9402fb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9414430
.word 0xd63f0200
.word 0xf9007fa0
.word 0xf9402fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa1
.word 0x910243a0
.word 0xf90073a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941c430
.word 0xd63f0200
.word 0xf94073be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9402fb1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0x910243a0
.word 0x9102c3a0
.word 0xf9404ba0
.word 0xf9005ba0
.word 0xf9404fa0
.word 0xf9005fa0
.word 0xf94053a0
.word 0xf90063a0
.word 0xf94057a0
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9418030
.word 0xd63f0200
.word 0xf9007ba0
.word 0xf9402fb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba0
.word 0xd2800081
.word 0xd280009e
.word 0xeb1e001f
.word 0x54000280
.word 0xf9402fb1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9418030
.word 0xd63f0200
.word 0xf9007ba0
.word 0xf9402fb1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba0
.word 0xd2800061
.word 0xd280007e
.word 0xeb1e001f
.word 0x54000a61
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf941c631
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
bl _p_37
.word 0xfd0093a0
.word 0xf9402fb1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
bl _p_81
.word 0xfd0097a0
.word 0xf9402fb1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4093a0
.word 0xfd4097a1
bl _p_82
.word 0xfd008fa0
.word 0xf9402fb1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd408fa0
.word 0xfd006ba0
.word 0xf9402fb1
.word 0xf9423631
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
bl _p_37
.word 0xfd0087a0
.word 0xf9402fb1
.word 0xf9425231
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
bl _p_81
.word 0xfd008ba0
.word 0xf9402fb1
.word 0xf9426e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4087a0
.word 0xfd408ba1
bl _p_83
.word 0xfd0083a0
.word 0xf9402fb1
.word 0xf9428e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4083a0
.word 0xfd006fa0
.word 0xf9402fb1
.word 0xf942a631
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
.word 0xfd406ba0
bl _p_36
.word 0xf9402fb1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf942d231
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
.word 0xfd406fa0
bl _p_84
.word 0xf9402fb1
.word 0xf942ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9430e31
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
.word 0x9101c3a0
.word 0xf9405ba0
.word 0xf9003ba0
.word 0xf9405fa0
.word 0xf9003fa0
.word 0xf94063a0
.word 0xf90043a0
.word 0xf94067a0
.word 0xf90047a0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0x9101c3a0
.word 0x910063a0
.word 0xf9403ba0
.word 0xf9000fa0
.word 0xf9403fa0
.word 0xf90013a0
.word 0xf94043a0
.word 0xf90017a0
.word 0xf94047a0
.word 0xf9001ba0
.word 0xf9402fb1
.word 0xf9438e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0xfd400fa0
.word 0xfd4013a1
.word 0xfd4017a2
.word 0xfd401ba3
.word 0x910003bf
.word 0xa8d37bfd
.word 0xd65f03c0

Lme_3a:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect
FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect:
.loc 1 1 0
.word 0xd2806010
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa
.word 0xfd000fa0
.word 0xfd0013a1
.word 0xfd0017a2
.word 0xfd001ba3

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1152]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0x910843a0
.word 0xd2800000
.word 0xf9010ba0
.word 0xf9010fa0
.word 0xf90113a0
.word 0xf90117a0
.word 0x9107c3a0
.word 0xd2800000
.word 0xf900fba0
.word 0xf900ffa0
.word 0xf90103a0
.word 0xf90107a0
.word 0x910783a0
.word 0xd2800000
.word 0xf900f3a0
.word 0xf900f7a0
.word 0x910703a0
.word 0xd2800000
.word 0xf900e3a0
.word 0xf900e7a0
.word 0xf900eba0
.word 0xf900efa0
.word 0x910683a0
.word 0xd2800000
.word 0xf900d3a0
.word 0xf900d7a0
.word 0xf900dba0
.word 0xf900dfa0
.word 0xf9402fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9013fa0
.word 0xf9402fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9419030
.word 0xd63f0200
.word 0xf90133a0
.word 0xf9402fb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd280001e
.word 0xf2a7e01e
.word 0x9e6703d0
.word 0x1e22c200
.word 0xfd0137a0
.word 0xf9402fb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xd280001e
.word 0xf2a7e01e
.word 0x9e6703d0
.word 0x1e22c200
.word 0xfd013ba0
.word 0xf9402fb1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4137a0
.word 0xfd413ba1
.word 0x910643a0
.word 0xd2800000
.word 0xf900cba0
.word 0xf900cfa0
.word 0x910643a0
bl _p_85
.word 0x910643a0
.word 0x910343a0
.word 0xf940cba0
.word 0xf9006ba0
.word 0xf940cfa0
.word 0xf9006fa0
.word 0xf9402fb1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a1
.word 0xaa0103e0
.word 0x910343a2
.word 0xfd406ba0
.word 0xfd406fa1
.word 0xf9400021
.word 0xf9419430
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x910063a0
.word 0xf9012ba0
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd012fa0
.word 0xf9402fb1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba0
.word 0xfd412fa0
bl _p_86
.word 0xf9402fb1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9425231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90127a0
.word 0xf9402fb1
.word 0xf9427231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a1
.word 0x9105c3a0
.word 0xf9011ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9411bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9402fb1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0x9105c3a0
.word 0x910843a0
.word 0xf940bba0
.word 0xf9010ba0
.word 0xf940bfa0
.word 0xf9010fa0
.word 0xf940c3a0
.word 0xf90113a0
.word 0xf940c7a0
.word 0xf90117a0
.word 0xf9402fb1
.word 0xf942ea31
.word 0xb4000051
.word 0xd63f0220
.word 0x910843a0
.word 0x910583a1
.word 0xf9011ba1
bl _p_87
.word 0xf9411bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xf9402fb1
.word 0xf9431631
.word 0xb4000051
.word 0xd63f0220
.word 0x910063a0
.word 0x910543a1
.word 0xf9011ba1
bl _p_87
.word 0xf9411bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xf9402fb1
.word 0xf9434231
.word 0xb4000051
.word 0xd63f0220
.word 0x910583a0
.word 0xfd40b3a0
.word 0xfd40b7a1
.word 0x910543a0
.word 0xfd40aba2
.word 0xfd40afa3
bl _p_88
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9437631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x340000c0
.word 0xf9402fb1
.word 0xf9438e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000198
.word 0xf9402fb1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0x910063a0
.word 0xf90177a0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9017ba0
.word 0xf9402fb1
.word 0xf943ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba1
.word 0x9104c3a0
.word 0xf9011ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9411bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9402fb1
.word 0xf9440a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9104c3a0
.word 0x9107c3a0
.word 0xf9409ba0
.word 0xf900fba0
.word 0xf9409fa0
.word 0xf900ffa0
.word 0xf940a3a0
.word 0xf90103a0
.word 0xf940a7a0
.word 0xf90107a0
.word 0x9107c3a0
.word 0x910483a1
.word 0xf9011ba1
bl _p_89
.word 0xf9411bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xf9402fb1
.word 0xf9445e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94177a0
.word 0x910483a1
.word 0xfd4093a0
.word 0xfd4097a1
bl _p_90
.word 0xf9402fb1
.word 0xf9448231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9449231
.word 0xb4000051
.word 0xd63f0220
.word 0x910783a0
.word 0xf9014fa0
.word 0x910063a0
bl _p_91
.word 0xfd0167a0
.word 0xf9402fb1
.word 0xf944b631
.word 0xb4000051
.word 0xd63f0220
.word 0x910063a0
bl _p_37
.word 0xfd016fa0
.word 0xf9402fb1
.word 0xf944d231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800040
.word 0xd2800040
bl _p_14
.word 0xfd0173a0
.word 0xf9402fb1
.word 0xf944f231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd416fa0
.word 0xfd4173a1
.word 0x1e611800
.word 0xfd016ba0
.word 0xf9402fb1
.word 0xf9451231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4167a0
.word 0xfd416ba1
.word 0x1e612800
.word 0xfd013ba0
.word 0xf9402fb1
.word 0xf9453231
.word 0xb4000051
.word 0xd63f0220
.word 0x910063a0
bl _p_92
.word 0xfd0157a0
.word 0xf9402fb1
.word 0xf9454e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910063a0
bl _p_81
.word 0xfd015fa0
.word 0xf9402fb1
.word 0xf9456a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800040
.word 0xd2800040
bl _p_14
.word 0xfd0163a0
.word 0xf9402fb1
.word 0xf9458a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd415fa0
.word 0xfd4163a1
.word 0x1e611800
.word 0xfd015ba0
.word 0xf9402fb1
.word 0xf945aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4157a0
.word 0xfd415ba1
.word 0x1e612800
.word 0xfd0153a0
.word 0xf9402fb1
.word 0xf945ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9414fa0
.word 0xfd413ba0
.word 0xfd4153a1
bl _p_85
.word 0xf9402fb1
.word 0xf945ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf945fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90133a0
.word 0xf9402fb1
.word 0xf9461a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a1
.word 0x910783a0
.word 0x910303a0
.word 0xf940f3a0
.word 0xf90063a0
.word 0xf940f7a0
.word 0xf90067a0
.word 0xaa0103e0
.word 0x910303a2
.word 0xfd4063a0
.word 0xfd4067a1
.word 0xf9400021
.word 0xf941b430
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf9466231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9467231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402f41
.word 0x910783a0
.word 0x9102c3a0
.word 0xf940f3a0
.word 0xf9005ba0
.word 0xf940f7a0
.word 0xf9005fa0
.word 0xaa0103e0
.word 0x9102c3a2
.word 0xfd405ba0
.word 0xfd405fa1
.word 0xf9400021
.word 0xf941b430
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf946be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf946ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x910063a0
.word 0x910243a0
.word 0xf9400fa0
.word 0xf9004ba0
.word 0xf94013a0
.word 0xf9004fa0
.word 0xf94017a0
.word 0xf90053a0
.word 0xf9401ba0
.word 0xf90057a0
.word 0xaa1a03e0
.word 0x910243a1
.word 0xfd404ba0
.word 0xfd404fa1
.word 0xfd4053a2
.word 0xfd4057a3
bl _p_59
.word 0xf9402fb1
.word 0xf9472631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9473631
.word 0xb4000051
.word 0xd63f0220
.word 0x910063a0
bl _p_24
.word 0xfd014ba0
.word 0xf9402fb1
.word 0xf9475231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd012fa0
.word 0xf9402fb1
.word 0xf9477231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd414ba0
.word 0xfd412fa1
.word 0x1e613800
.word 0xfd0147a0
.word 0xf9402fb1
.word 0xf9479231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf947a231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4147a0
bl _p_64
.word 0xfd0143a0
.word 0xf9402fb1
.word 0xf947be31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4143a0
.word 0xd280001e
.word 0xf2e6d41e
.word 0x9e6703c1
.word 0x1e612000
.word 0x54000fe0
.word 0x54000fcb
.word 0xf9402fb1
.word 0xf947ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_93
.word 0xf9402fb1
.word 0xf9480631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9481631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403741
.word 0x910403a0
.word 0xf9011ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9411bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9402fb1
.word 0xf9485a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
.word 0x910703a0
.word 0xf94083a0
.word 0xf900e3a0
.word 0xf94087a0
.word 0xf900e7a0
.word 0xf9408ba0
.word 0xf900eba0
.word 0xf9408fa0
.word 0xf900efa0
.word 0xf9402fb1
.word 0xf9489231
.word 0xb4000051
.word 0xd63f0220
.word 0x910703a0
.word 0xf90123a0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9012ba0
.word 0xf9402fb1
.word 0xf948ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba1
.word 0x910383a0
.word 0xf9011ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9411bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9402fb1
.word 0xf948fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x910383a0
.word 0x910683a0
.word 0xf94073a0
.word 0xf900d3a0
.word 0xf94077a0
.word 0xf900d7a0
.word 0xf9407ba0
.word 0xf900dba0
.word 0xf9407fa0
.word 0xf900dfa0
.word 0x910683a0
bl _p_24
.word 0xfd0147a0
.word 0xf9402fb1
.word 0xf9493e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0xfd4147a0
bl _p_38
.word 0xf9402fb1
.word 0xf9495a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9496a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403741
.word 0x910703a0
.word 0x9101c3a0
.word 0xf940e3a0
.word 0xf9003ba0
.word 0xf940e7a0
.word 0xf9003fa0
.word 0xf940eba0
.word 0xf90043a0
.word 0xf940efa0
.word 0xf90047a0
.word 0xaa0103e0
.word 0x9101c3a2
.word 0xfd403ba0
.word 0xfd403fa1
.word 0xfd4043a2
.word 0xfd4047a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf949ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf949ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf949fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa9407bfd
.word 0xd2806010
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0

Lme_3b:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving
FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1160]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x3942ff40
.word 0x34000200
.word 0xf9400fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0x14000011
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x39430740
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3c:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool
FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1168]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x394063a1
.word 0x39030401
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3d:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect
FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect:
.loc 1 1 0
.word 0xa9a97bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa
.word 0xfd000fa0
.word 0xfd0013a1
.word 0xfd0017a2
.word 0xfd001ba3

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1176]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0x910343a0
.word 0xd2800000
.word 0xf9006ba0
.word 0xf9006fa0
.word 0xf90073a0
.word 0xf90077a0
.word 0x9102c3a0
.word 0xd2800000
.word 0xf9005ba0
.word 0xf9005fa0
.word 0xf90063a0
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_94
.word 0x53001c00
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0x34000b00
.word 0xf9402fb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402740
.word 0xb5000a20
.word 0xf9402fb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #768]
bl _p_17
.word 0xf9008fa0
bl _p_46
.word 0xf9402fb1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa0
.word 0xf9002740
.word 0x91012341
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402742
.word 0xaa1a03e0
.word 0xf9402b41
.word 0xaa0203e0
.word 0xf9400042
.word 0xf941c850
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9414430
.word 0xd63f0200
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf941c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402740
.word 0xf90083a0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90087a0
.word 0xf9402fb1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a1
.word 0xf94087a2
.word 0xf9408ba3
.word 0xaa0303e0
.word 0xf9400063
.word 0xf9422870
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_94
.word 0x53001c00
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf9426231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0x34001580
.word 0xf9402fb1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910343a0
.word 0xd2800000
.word 0xf9006ba0
.word 0xf9006fa0
.word 0xf90073a0
.word 0xf90077a0
.word 0xf9402fb1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0x910343a0
.word 0xf900aba0
bl _p_95
.word 0xf900b3a0
.word 0xf9402fb1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940b3a1
.word 0x910243a0
.word 0xf9007ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf940ec30
.word 0xd63f0200
.word 0xf9407bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9402fb1
.word 0xf9430231
.word 0xb4000051
.word 0xd63f0220
.word 0x910243a0
.word 0x9102c3a0
.word 0xf9404ba0
.word 0xf9005ba0
.word 0xf9404fa0
.word 0xf9005fa0
.word 0xf94053a0
.word 0xf90063a0
.word 0xf94057a0
.word 0xf90067a0
.word 0x9102c3a0
bl _p_81
.word 0xfd00afa0
.word 0xf9402fb1
.word 0xf9434631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940aba0
.word 0xfd40afa0
bl _p_84
.word 0xf9402fb1
.word 0xf9436231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9437231
.word 0xb4000051
.word 0xd63f0220
.word 0x910343a0
.word 0xf900a3a0
.word 0xd280001e
.word 0xf2a7f01e
.word 0x9e6703d0
.word 0x1e22c200
.word 0xfd00a7a0
.word 0xf9402fb1
.word 0xf9439e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xfd40a7a0
bl _p_36
.word 0xf9402fb1
.word 0xf943ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf943ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910343a0
.word 0xf9008ba0
.word 0x910063a0
bl _p_24
.word 0xfd009ba0
.word 0xf9402fb1
.word 0xf943ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xd280001e
.word 0xf2a7f01e
.word 0x9e6703d0
.word 0x1e22c200
.word 0xfd009fa0
.word 0xf9402fb1
.word 0xf9441231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd409ba0
.word 0xfd409fa1
.word 0x1e613800
.word 0xfd0097a0
.word 0xf9402fb1
.word 0xf9443231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba0
.word 0xfd4097a0
bl _p_38
.word 0xf9402fb1
.word 0xf9444e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9445e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910343a0
.word 0xf90083a0
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd0093a0
.word 0xf9402fb1
.word 0xf9448631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xfd4093a0
bl _p_86
.word 0xf9402fb1
.word 0xf944a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf944b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402741
.word 0x910343a0
.word 0x9101c3a0
.word 0xf9406ba0
.word 0xf9003ba0
.word 0xf9406fa0
.word 0xf9003fa0
.word 0xf94073a0
.word 0xf90043a0
.word 0xf94077a0
.word 0xf90047a0
.word 0xaa0103e0
.word 0x9101c3a2
.word 0xfd403ba0
.word 0xfd403fa1
.word 0xfd4043a2
.word 0xfd4047a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf9451631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9453631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9454631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8d77bfd
.word 0xd65f03c0

Lme_3e:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_getStatus
FlyoutNavigation_FlyoutNavigationController_getStatus:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1184]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xd2800019
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf9401fb1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_77
.word 0x53001c00
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35000500
.word 0xf9401fb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x3942fb40
.word 0x34000420
.word 0xf9401fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403741
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9417830
.word 0xd63f0200
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xb5000200
.word 0xf9401fb1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_60
.word 0x53001c00
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x34000140
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0x140000d5
.word 0xf9401fb1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_96
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf90043a0
.word 0xaa0003f9
.word 0xf9401fb1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xaa0003e1
.word 0xb50000c0
.word 0xf9401fb1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0x140000ba
.word 0xf9401fb1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9414430
.word 0xd63f0200
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a2
.word 0xaa1a03e0
.word 0xf9403741
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9425450
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf9422a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403741
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9417c30
.word 0xd63f0200
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9426631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xaa0003f7
.word 0xf9401fb1
.word 0xf9427e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800016
.word 0xf9401fb1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000025
.word 0xf9401fb1
.word 0xf942a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1603e0
.word 0x93407ec0
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x540010a9
.word 0xd37df000
.word 0x8b0002e0
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f8
.word 0xf9401fb1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf9421830
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf9430a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9431a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0x110006c0
.word 0xaa0003f6
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9434631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xb9801ae0
.word 0x6b0002df
.word 0x54fffa0b
.word 0xf9401fb1
.word 0xf9436a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403742
.word 0xaa1903e0
.word 0xaa0203e0
.word 0xaa1903e1
.word 0xf9400042
.word 0xf9425450
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf9439a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf943aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403740
.word 0xf90047a0
bl _p_78
.word 0xf9004ba0
.word 0xf9401fb1
.word 0xf943ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0x910143a0
.word 0xf9003ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9410830
.word 0xd63f0200
.word 0xf9403bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401fb1
.word 0xf9440e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa0103e0
.word 0x910143a2
.word 0xfd402ba0
.word 0xfd402fa1
.word 0xfd4033a2
.word 0xfd4037a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf9444631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9445631
.word 0xb4000051
.word 0xd63f0220
bl _p_78
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9446e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a2
.word 0xd2800020
.word 0xaa0203e0
.word 0xd2800021
.word 0xf9400042
.word 0xf9410450
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf9449a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf944aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf944ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_7

Lme_3f:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage
FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1192]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xd280001a
.word 0xf9001fbf
.word 0xf90023bf
.word 0xf94013b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
bl _p_95
.word 0xf90043a0
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a2
.word 0xd2800000
.word 0xaa0203e0
.word 0xd2800001
.word 0xf9400042
.word 0xf940f850
.word 0xd63f0200
.word 0xf9003fa0
.word 0xf94013b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xf9003ba0
.word 0xaa0003fa
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003e1
.word 0xf9001fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000014
.word 0xf90027a0
.word 0xf94027a0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9001fbf
.word 0xf94013b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
bl _p_97
.word 0xf90037a0
.word 0xf94037a0
.word 0xb4000060
.word 0xf94037a0
bl _p_98
.word 0x14000001
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0x14000001
.word 0xf94013b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0

Lme_40:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_hideStatus
FlyoutNavigation_FlyoutNavigationController_hideStatus:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1200]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x3942fb40
.word 0x350000c0
.word 0xf9400fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000028
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403741
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421830
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
bl _p_78
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba2
.word 0xd2800000
.word 0xaa0203e0
.word 0xd2800001
.word 0xf9400042
.word 0xf9410450
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_41:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_HideMenu
FlyoutNavigation_FlyoutNavigationController_HideMenu:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1208]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0x910143a0
.word 0xd2800000
.word 0xf9002ba0
.word 0xf9002fa0
.word 0xf90033a0
.word 0xf90037a0
.word 0xf9400fb1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90043a0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xb4000a80
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9004fa0
.word 0xf9400fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0x9100c3a0
.word 0xf9003ba0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9403bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0x9100c3a0
.word 0x910143a0
.word 0xf9401ba0
.word 0xf9002ba0
.word 0xf9401fa0
.word 0xf9002fa0
.word 0xf94023a0
.word 0xf90033a0
.word 0xf94027a0
.word 0xf90037a0
.word 0xf9400fb1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910143a0
bl _p_24
.word 0xfd0047a0
.word 0xf9400fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd004ba0
.word 0xf9400fb1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4047a0
.word 0xfd404ba1
.word 0x1e612000
.word 0x9a9f17e0
.word 0xf90043a0
.word 0xf9400fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35000200
.word 0xf9400fb1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_60
.word 0x53001c00
.word 0xf90043a0
.word 0xf9400fb1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x34000140
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9420231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000038
.word 0xf9400fb1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xeb1f035f
.word 0x10000011
.word 0x540006e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf900103a
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1216]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1224]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1232]
.word 0xf9401402
.word 0xf9000c22
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa1a03e0
bl _p_22
.word 0xf9400fb1
.word 0xf942d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf942e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf942f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_42:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_hideComplete
FlyoutNavigation_FlyoutNavigationController_hideComplete:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1240]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_99
.word 0xf9400fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402f41
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421830
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402341
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba2
.word 0xd2800020
.word 0xaa0203e0
.word 0xd2800021
.word 0xf9400042
.word 0xf9419850
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_43:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView
FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1248]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf9401fb1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9417c30
.word 0xd63f0200
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xb50000c0
.word 0xf9401fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000073
.word 0xf9401fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9417c30
.word 0xd63f0200
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa0003f7
.word 0xf9401fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800016
.word 0xf9401fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000048
.word 0xf9401fb1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1603e0
.word 0x93407ec0
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54000b09
.word 0xd37df000
.word 0x8b0002e0
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f8
.word 0xf9401fb1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf940f030
.word 0xd63f0200
.word 0x53001c00
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x340001e0
.word 0xf9401fb1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf940f430
.word 0xd63f0200
.word 0x53001c00
.word 0xf9401fb1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1803e1
bl _p_100
.word 0xf9401fb1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9422a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0x110006c0
.word 0xaa0003f6
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9425631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xb9801ae0
.word 0x6b0002df
.word 0x54fff5ab
.word 0xf9401fb1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_7

Lme_44:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_ToggleMenu
FlyoutNavigation_FlyoutNavigationController_ToggleMenu:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1256]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xeb1f035f
.word 0x10000011
.word 0x540006e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf900103a
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1264]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1272]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1280]
.word 0xf9401402
.word 0xf9000c22
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa1a03e0
bl _p_22
.word 0xf9400fb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_45:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath
FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1288]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xd2800018
.word 0xd2800017
.word 0xf9401bb1
.word 0xf9404631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800018
.word 0xf9401bb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800017
.word 0xf9401bb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400002f
.word 0xf9401bb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf940003e
bl _p_20
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a2
.word 0xaa1803e0
.word 0xaa0203e0
.word 0xaa1803e1
.word 0xf940005e
bl _p_101
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_102
.word 0x93407c00
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xb0002e0
.word 0xaa0003f7
.word 0xf9401bb1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x11000700
.word 0xaa0003f8
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_103
.word 0x93407c00
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x6b00031f
.word 0x54fff7ab
.word 0xf9401bb1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_104
.word 0x93407c00
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb0002e0
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401bb1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_46:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int
FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1296]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf9002fbf
.word 0xd2800015
.word 0xf94023b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf940003e
bl _p_20
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xb5000520
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x93407c00
.word 0xf90047a0
.word 0xf94023b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x93407c00
.word 0xf9004ba0
.word 0xf94023b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf9404ba1
bl _p_105
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x14000106
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800018
.word 0xf94023b1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800017
.word 0xf94023b1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402321
.word 0xaa0103e0
.word 0xf940003e
bl _p_20
.word 0xf90047a0
.word 0xf94023b1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x15, [x16, #1304]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90043a0
.word 0xf94023b1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf9002fa0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000056
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x15, [x16, #1312]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9004ba0
.word 0xf94023b1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xf90047a0
.word 0xaa0003f6
.word 0xf94023b1
.word 0xf9429631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf940003e
bl _p_102
.word 0x93407c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xaa1803e1
.word 0xb180000
.word 0xaa1a03e1
.word 0x6b1a001f
.word 0x5400014d
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf942fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000038
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9431e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_102
.word 0x93407c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9434a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xb000300
.word 0xaa0003f8
.word 0xf94023b1
.word 0xf9436631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x110006e0
.word 0xaa0003f7
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9439231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x15, [x16, #1320]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf943d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35fff240
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf943fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000028
.word 0xf9003bbe
.word 0xf94023b1
.word 0xf9441631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb40002e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9443e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x15, [x16, #1328]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94023b1
.word 0xf9447631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9449631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bbe
.word 0xd61f03c0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf944be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0x4b180340
.word 0xaa0003f5
.word 0xf94023b1
.word 0xf944de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0x93407ea0
.word 0xf90047a0
.word 0xf94023b1
.word 0xf944fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x93407ee0
.word 0xf9004ba0
.word 0xf94023b1
.word 0xf9451631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf9404ba1
bl _p_105
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9453631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9455631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf94023b1
.word 0xf9456a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0

Lme_47:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_ShouldAutorotateToInterfaceOrientation_UIKit_UIInterfaceOrientation
FlyoutNavigation_FlyoutNavigationController_ShouldAutorotateToInterfaceOrientation_UIKit_UIInterfaceOrientation:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa9026bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1336]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf90027bf
.word 0xf9002bbf
.word 0xd2800016
.word 0xf9401bb1
.word 0xf9404a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_106
.word 0x53001c00
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x340003c0
.word 0xf9401bb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9418030
.word 0xd63f0200
.word 0xf90037a0
.word 0xf9401bb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xeb00035f
.word 0x9a9f17e0
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x14000087
.word 0xf9401bb1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_19
.word 0xf90043a0
.word 0xf9401bb1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941ec30
.word 0xd63f0200
.word 0xf9003fa0
.word 0xf9401bb1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xf90027a0
.word 0xf9401bb1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_19
.word 0xf9003ba0
.word 0xf9401bb1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941e030
.word 0xd63f0200
.word 0xf90037a0
.word 0xf9401bb1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_19
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xb5000100
.word 0xf9401bb1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xd2800035
.word 0x1400001c
.word 0xf9401bb1
.word 0xf9422e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_19
.word 0xf90037a0
.word 0xf9401bb1
.word 0xf9424e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a2
.word 0xaa1a03e0
.word 0xaa0203e0
.word 0xaa1a03e1
.word 0xf9400042
.word 0xf941c850
.word 0xd63f0200
.word 0x53001c00
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf9428231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503f6
.word 0xf9401bb1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_19
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xb40000a0
.word 0xf9401bb1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf942fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9431e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9401bb1
.word 0xf9433231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0

Lme_48:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_GetSupportedInterfaceOrientations
FlyoutNavigation_FlyoutNavigationController_GetSupportedInterfaceOrientations:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1344]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_19
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xb4000420
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_19
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941ec30
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0x1400000f
.word 0xf9400fb1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd28003c0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xd28003c0
.word 0xf9400fb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_49:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_WillRotate_UIKit_UIInterfaceOrientation_double
FlyoutNavigation_FlyoutNavigationController_WillRotate_UIKit_UIInterfaceOrientation_double:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xfd0013a0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1352]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94017b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fa1
.word 0xfd4013a0
bl _p_107
.word 0xf94017b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_4a:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_DidRotate_UIKit_UIInterfaceOrientation
FlyoutNavigation_FlyoutNavigationController_DidRotate_UIKit_UIInterfaceOrientation:
.loc 1 1 0
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f9
.word 0xf90013a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1360]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800018
.word 0xf94017b1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94013a1
.word 0xaa1903e0
bl _p_108
.word 0xf94017b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402f20
.word 0xf90053a0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf90057a0
.word 0xf94017b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a1
.word 0x910183a0
.word 0xf90043a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf94043be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94017b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xaa0103e0
.word 0x910183a2
.word 0xfd4033a0
.word 0xfd4037a1
.word 0xfd403ba2
.word 0xfd403fa3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf94017b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
bl _p_28
.word 0xf9004fa0
.word 0xf94017b1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xd2800001
.word 0xb50000c0
.word 0xf94017b1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0x140000ce
.word 0xf94017b1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_30
.word 0x53001c00
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf941d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x34001760
.word 0xf94017b1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9418030
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf94017b1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf9004ba0
.word 0xaa0003f8
.word 0xf94017b1
.word 0xf9422e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003e1
.word 0xd2800081
.word 0xd280009e
.word 0xeb1e001f
.word 0x540001e0
.word 0xf94017b1
.word 0xf9425631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xd2800060
.word 0xd280007e
.word 0xeb1e031f
.word 0x540000c0
.word 0xf94017b1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000035
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9429e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_54
.word 0x53001c00
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x35000320
.word 0xf94017b1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020
.word 0xaa1903e0
.word 0xd2800021
bl _p_50
.word 0xf94017b1
.word 0xf942fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9430e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_26
.word 0xf94017b1
.word 0xf9432a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9434a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000062
.word 0xf94017b1
.word 0xf9435e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_109
.word 0x53001c00
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf9438231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x340003c0
.word 0xf94017b1
.word 0xf9439a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0xaa1903e0
.word 0xd2800001
bl _p_50
.word 0xf94017b1
.word 0xf943be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf943ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_25
.word 0xf94017b1
.word 0xf943ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf943fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000029
.word 0xf94017b1
.word 0xf9440e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_16
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf9443231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0x910103a0
.word 0xf90043a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf94043be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94017b1
.word 0xf9447231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x910103a1
.word 0xfd4023a0
.word 0xfd4027a1
.word 0xfd402ba2
.word 0xfd402fa3
bl _p_59
.word 0xf94017b1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf944be31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000005
.word 0xf94017b1
.word 0xf944d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf944e231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_4b:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double
FlyoutNavigation_FlyoutNavigationController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xfd0013a0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1368]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94017b1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fa1
.word 0xfd4013a0
bl _p_110
.word 0xf94017b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_4c:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action
FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fa0
.word 0xf90013a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1376]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800018
.word 0xf94017b1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1384]
.word 0xd2800301
.word 0xd2800301
bl _p_1
.word 0xf90027a0
bl _p_111
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xaa0003f8
.word 0xf94017b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94013a0
.word 0xf9000b00
.word 0x91004301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
bl _p_112
.word 0x53001c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x340002a0
.word 0xf94017b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400b01
.word 0xaa0103e0
.word 0xf90023a1
.word 0xf9400c30
.word 0xd63f0200
.word 0xf94023a0
.word 0xf94017b1
.word 0xf9414e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000039
.word 0xf94017b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf90023a0
.word 0xaa1803e0
.word 0xeb1f031f
.word 0x10000011
.word 0x540006e0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf94023a0
.word 0xf9001038
.word 0x91008022
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #1392]
.word 0xf9001422

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #1400]
.word 0xf9002022

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #1408]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f
bl _p_113
.word 0xf94017b1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9425231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7

Lme_4d:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_IsMainThread
FlyoutNavigation_FlyoutNavigationController_IsMainThread:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1416]
.word 0xf9000bb0
.word 0xf9400a11
.word 0xf9000fb1
.word 0xf9400bb1
.word 0xf9402e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
bl _p_114
.word 0xf9001fa0
.word 0xf9400bb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf940e030
.word 0xd63f0200
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400bb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400bb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_4e:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_Dispose_bool
FlyoutNavigation_FlyoutNavigationController_Dispose_bool:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xaa0003f9
.word 0xf90023a1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1424]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf94027b1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x394103a1
.word 0xaa1903e0
bl _p_115
.word 0xf94027b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0xaa1903e0
.word 0xd2800001
bl _p_116
.word 0xf94027b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9404f20
.word 0xb4000b20
.word 0xf94027b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9404f21
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404030
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94027b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f7
.word 0xf94027b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800016
.word 0xf94027b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000031
.word 0xf94027b1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1603e0
.word 0x93407ec0
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54001a69
.word 0xd37df000
.word 0x8b0002e0
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f8
.word 0xf94027b1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903f5
.word 0xaa1803f4
.word 0xb4000178
.word 0xf9400280
.word 0xf9400000
.word 0xf9400800
.word 0xf9400c00

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #248]
.word 0xeb01001f
.word 0x10000011
.word 0x54001741
.word 0xaa1403e0
.word 0xaa1503e0
.word 0xaa1403e1
bl _p_117
.word 0xf94027b1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0x110006c0
.word 0xaa0003f6
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9422a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xb9801ae0
.word 0x6b0002df
.word 0x54fff88b
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9425e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9414430
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf9428631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba2
.word 0xaa1903e0
.word 0xf9404721
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9421450
.word 0xd63f0200
.word 0xf94027b1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401f20
.word 0xf90037a0
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x54000e60

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #808]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf94037a2
.word 0xf9001039
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #816]
.word 0xf9001420

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #824]
.word 0xf9002020

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #832]
.word 0xf9401403
.word 0xf9000c23
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa0203e0
.word 0xf940005e
bl _p_118
.word 0xf94027b1
.word 0xf9438e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9439e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800000
.word 0xf9001f3f
.word 0xf94027b1
.word 0xf943ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_19
.word 0xf90033a0
.word 0xf94027b1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xb4000400
.word 0xf94027b1
.word 0xf943f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_19
.word 0xf90037a0
.word 0xf94027b1
.word 0xf9441231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94027b1
.word 0xf9443a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421830
.word 0xd63f0200
.word 0xf94027b1
.word 0xf9445e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9447e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9448e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7
.word 0xd2801c60
.word 0xaa1103e1
bl _p_7
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_7

Lme_4f:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__Initializem__0
FlyoutNavigation_FlyoutNavigationController__Initializem__0:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1432]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9404b41
.word 0xaa1a03e0
bl _p_119
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_50:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__ShowMenum__1
FlyoutNavigation_FlyoutNavigationController__ShowMenum__1:
.loc 1 1 0
.word 0xd2804e10
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1440]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0x9107a3a0
.word 0xd2800000
.word 0xf900f7a0
.word 0xf900fba0
.word 0xf900ffa0
.word 0xf90103a0
.word 0x910723a0
.word 0xd2800000
.word 0xf900e7a0
.word 0xf900eba0
.word 0xf900efa0
.word 0xf900f3a0
.word 0x9106a3a0
.word 0xd2800000
.word 0xf900d7a0
.word 0xf900dba0
.word 0xf900dfa0
.word 0xf900e3a0
.word 0x910623a0
.word 0xd2800000
.word 0xf900c7a0
.word 0xf900cba0
.word 0xf900cfa0
.word 0xf900d3a0
.word 0xf9401bb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402341
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9401bb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a2
.word 0xd2800000
.word 0xaa0203e0
.word 0xd2800001
.word 0xf9400042
.word 0xf9419850
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402f40
.word 0xf9011fa0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90123a0
.word 0xf9401bb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a1
.word 0x9105a3a0
.word 0xf90107a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf94107be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401bb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9411fa1
.word 0xaa0103e0
.word 0x9105a3a2
.word 0xfd40b7a0
.word 0xfd40bba1
.word 0xfd40bfa2
.word 0xfd40c3a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf941e631
.word 0xb4000051
.word 0xd63f0220
bl _p_78
.word 0xf9011ba0
.word 0xf9401bb1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9411ba1
.word 0x910523a0
.word 0xf90107a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9410830
.word 0xd63f0200
.word 0xf94107be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401bb1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910523a0
.word 0x9107a3a0
.word 0xf940a7a0
.word 0xf900f7a0
.word 0xf940aba0
.word 0xf900fba0
.word 0xf940afa0
.word 0xf900ffa0
.word 0xf940b3a0
.word 0xf90103a0
.word 0xf9401bb1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220
.word 0x9107a3a0
.word 0xf9010fa0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90117a0
.word 0xf9401bb1
.word 0xf9429e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94117a1
.word 0x9104a3a0
.word 0xf90107a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf94107be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401bb1
.word 0xf942de31
.word 0xb4000051
.word 0xd63f0220
.word 0x9104a3a0
.word 0x910723a0
.word 0xf94097a0
.word 0xf900e7a0
.word 0xf9409ba0
.word 0xf900eba0
.word 0xf9409fa0
.word 0xf900efa0
.word 0xf940a3a0
.word 0xf900f3a0
.word 0x910723a0
bl _p_24
.word 0xfd0113a0
.word 0xf9401bb1
.word 0xf9432231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410fa0
.word 0xfd4113a0
bl _p_38
.word 0xf9401bb1
.word 0xf9433e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9434e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403741
.word 0x9107a3a0
.word 0x9102a3a0
.word 0xf940f7a0
.word 0xf90057a0
.word 0xf940fba0
.word 0xf9005ba0
.word 0xf940ffa0
.word 0xf9005fa0
.word 0xf94103a0
.word 0xf90063a0
.word 0xaa0103e0
.word 0x9102a3a2
.word 0xfd4057a0
.word 0xfd405ba1
.word 0xfd405fa2
.word 0xfd4063a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf943b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf943c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_60
.word 0x53001c00
.word 0xf9010ba0
.word 0xf9401bb1
.word 0xf943e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba0
.word 0x35000340
.word 0xf9401bb1
.word 0xf943fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9414430
.word 0xd63f0200
.word 0xf9010ba0
.word 0xf9401bb1
.word 0xf9442631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba2
.word 0xaa1a03e0
.word 0xf9401f41
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9425450
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf9445231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9447231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_61
.word 0x53001c00
.word 0xf9010ba0
.word 0xf9401bb1
.word 0xf9449631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba0
.word 0x350004a0
.word 0xf9401bb1
.word 0xf944ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9414430
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9401bb1
.word 0xf944d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402f40
.word 0xf9010ba0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9010fa0
.word 0xf9401bb1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba1
.word 0xf9410fa2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xf9400063
.word 0xf9422470
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf9452e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9454e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_94
.word 0x53001c00
.word 0xf9010ba0
.word 0xf9401bb1
.word 0xf9457231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba0
.word 0x340004a0
.word 0xf9401bb1
.word 0xf9458a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9414430
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9401bb1
.word 0xf945b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402740
.word 0xf9010ba0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9010fa0
.word 0xf9401bb1
.word 0xf945de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba1
.word 0xf9410fa2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xf9400063
.word 0xf9422470
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf9460a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9462a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1448]
bl _p_120
.word 0xf9401bb1
.word 0xf9464a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9465a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xd2800020
bl _p_121
.word 0xf9401bb1
.word 0xf9467631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9468631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_67
.word 0xf9401bb1
.word 0xf946a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf946b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9012ba0
.word 0xf9401bb1
.word 0xf946d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba1
.word 0x910423a0
.word 0xf90107a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf94107be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401bb1
.word 0xf9471231
.word 0xb4000051
.word 0xd63f0220
.word 0x910423a0
.word 0x9106a3a0
.word 0xf94087a0
.word 0xf900d7a0
.word 0xf9408ba0
.word 0xf900dba0
.word 0xf9408fa0
.word 0xf900dfa0
.word 0xf94093a0
.word 0xf900e3a0
.word 0xf9401bb1
.word 0xf9474a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9106a3a0
.word 0xf9010fa0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_13
.word 0x93407c00
.word 0xf9010ba0
.word 0xf9401bb1
.word 0xf9477631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba0
.word 0xf9410fa1
.word 0xaa0103f9
.word 0x350000c0
.word 0xaa1903e0
.word 0xd2802300
.word 0xaa1903f8
.word 0xd2802317
.word 0x14000007
.word 0xaa1903e0
.word 0x928022e0
.word 0xf2bfffe0
.word 0xaa1903f8
.word 0x928022f7
.word 0xf2bffff7
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xaa1703e0
bl _p_14
.word 0xfd0133a0
.word 0xf9401bb1
.word 0xf947d631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4133a0
.word 0xaa1803e0
bl _p_38
.word 0xf9401bb1
.word 0xf947f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9480231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x9106a3a0
.word 0x910223a0
.word 0xf940d7a0
.word 0xf90047a0
.word 0xf940dba0
.word 0xf9004ba0
.word 0xf940dfa0
.word 0xf9004fa0
.word 0xf940e3a0
.word 0xf90053a0
.word 0xaa1a03e0
.word 0x910223a1
.word 0xfd4047a0
.word 0xfd404ba1
.word 0xfd404fa2
.word 0xfd4053a3
bl _p_63
.word 0xf9401bb1
.word 0xf9485a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9486a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_67
.word 0xf9401bb1
.word 0xf9488631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9489631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90117a0
.word 0xf9401bb1
.word 0xf948b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94117a1
.word 0x9103a3a0
.word 0xf90107a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf94107be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401bb1
.word 0xf948f631
.word 0xb4000051
.word 0xd63f0220
.word 0x9103a3a0
.word 0x9106a3a0
.word 0xf94077a0
.word 0xf900d7a0
.word 0xf9407ba0
.word 0xf900dba0
.word 0xf9407fa0
.word 0xf900dfa0
.word 0xf94083a0
.word 0xf900e3a0
.word 0xf9401bb1
.word 0xf9492e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402f41
.word 0x9106a3a0
.word 0x9101a3a0
.word 0xf940d7a0
.word 0xf90037a0
.word 0xf940dba0
.word 0xf9003ba0
.word 0xf940dfa0
.word 0xf9003fa0
.word 0xf940e3a0
.word 0xf90043a0
.word 0xaa0103e0
.word 0x9101a3a2
.word 0xfd4037a0
.word 0xfd403ba1
.word 0xfd403fa2
.word 0xfd4043a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf9499231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf949a231
.word 0xb4000051
.word 0xd63f0220
.word 0x9107a3a0
.word 0xf9010ba0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9012ba0
.word 0xf9401bb1
.word 0xf949ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba1
.word 0x910323a0
.word 0xf90107a0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf94107be
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9401bb1
.word 0xf94a0a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910323a0
.word 0x910623a0
.word 0xf94067a0
.word 0xf900c7a0
.word 0xf9406ba0
.word 0xf900cba0
.word 0xf9406fa0
.word 0xf900cfa0
.word 0xf94073a0
.word 0xf900d3a0
.word 0x910623a0
bl _p_24
.word 0xfd012fa0
.word 0xf9401bb1
.word 0xf94a4e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9410ba0
.word 0xfd412fa0
bl _p_38
.word 0xf9401bb1
.word 0xf94a6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf94a7a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403741
.word 0x9107a3a0
.word 0x910123a0
.word 0xf940f7a0
.word 0xf90027a0
.word 0xf940fba0
.word 0xf9002ba0
.word 0xf940ffa0
.word 0xf9002fa0
.word 0xf94103a0
.word 0xf90033a0
.word 0xaa0103e0
.word 0x910123a2
.word 0xfd4027a0
.word 0xfd402ba1
.word 0xfd402fa2
.word 0xfd4033a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf94ade31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf94aee31
.word 0xb4000051
.word 0xd63f0220
bl _p_122
.word 0xf9401bb1
.word 0xf94b0231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf94b1231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf94b2231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa9407bfd
.word 0xd2804e10
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0

Lme_51:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__HideMenum__2
FlyoutNavigation_FlyoutNavigationController__HideMenum__2:
.loc 1 1 0
.word 0xa9ad7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1456]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xd2800019
.word 0x9102e3a0
.word 0xd2800000
.word 0xf9005fa0
.word 0xf90063a0
.word 0xf90067a0
.word 0xf9006ba0
.word 0xf94013b1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1464]
.word 0xd2800701
.word 0xd2800701
bl _p_1
.word 0xf90093a0
bl _p_123
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94093a0
.word 0xaa0003f9
.word 0xf94013b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xf9000b3a
.word 0x91004320
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402341
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9424430
.word 0xd63f0200
.word 0xf94013b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9401f41
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421830
.word 0xd63f0200
.word 0xf94013b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402f40
.word 0xf9008ba0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf9008fa0
.word 0xf94013b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa1
.word 0x910263a0
.word 0xf9006fa0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9406fbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94013b1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba1
.word 0xaa0103e0
.word 0x910263a2
.word 0xfd404fa0
.word 0xfd4053a1
.word 0xfd4057a2
.word 0xfd405ba3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf94013b1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf90087a0
.word 0xaa1a03e0
.word 0xf9403741
.word 0x9101e3a0
.word 0xf9006fa0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9406fbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94013b1
.word 0xf9427231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94087a0
.word 0x9101e3a1
.word 0x91006000
.word 0xf9403fa1
.word 0xf9000001
.word 0xf94043a1
.word 0xf9000401
.word 0xf94047a1
.word 0xf9000801
.word 0xf9404ba1
.word 0xf9000c01
.word 0xf94013b1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xeb1f001f
.word 0x10000011
.word 0x540018c0
.word 0x91006000
.word 0xf9007ba0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_16
.word 0xf90083a0
.word 0xf94013b1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a1
.word 0x910163a0
.word 0xf9006fa0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941a430
.word 0xd63f0200
.word 0xf9406fbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf94013b1
.word 0xf9432631
.word 0xb4000051
.word 0xd63f0220
.word 0x910163a0
.word 0x9102e3a0
.word 0xf9402fa0
.word 0xf9005fa0
.word 0xf94033a0
.word 0xf90063a0
.word 0xf94037a0
.word 0xf90067a0
.word 0xf9403ba0
.word 0xf9006ba0
.word 0x9102e3a0
bl _p_24
.word 0xfd007fa0
.word 0xf94013b1
.word 0xf9436a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba0
.word 0xfd407fa0
bl _p_38
.word 0xf94013b1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9439631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403741
.word 0xaa1903e0
.word 0x91006000
.word 0x9100e3a2
.word 0xf9400002
.word 0xf9001fa2
.word 0xf9400402
.word 0xf90023a2
.word 0xf9400802
.word 0xf90027a2
.word 0xf9400c00
.word 0xf9002ba0
.word 0xaa0103e0
.word 0x9100e3a2
.word 0xfd401fa0
.word 0xfd4023a1
.word 0xfd4027a2
.word 0xfd402ba3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf94013b1
.word 0xf943fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9440e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd293335e
.word 0xf2b3333e
.word 0xf2d3333e
.word 0xf2e7f93e
.word 0x9e6703c0
.word 0xaa1903e0
.word 0xf90077a0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000ca0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xf94077a1
.word 0xf9001001
.word 0x91008002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #1472]
.word 0xf9001401

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #1480]
.word 0xf9002001

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x1, [x16, #1488]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf90073a0
.word 0xaa1a03e0
.word 0xeb1f035f
.word 0x10000011
.word 0x54000780

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #536]
.word 0xd2800e01
.word 0xd2800e01
bl _p_1
.word 0xaa0003e1
.word 0xf94073a0
.word 0xf900103a
.word 0x91008022
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #1496]
.word 0xf9001422

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #1504]
.word 0xf9002022

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #1512]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f
.word 0xd293335e
.word 0xf2b3333e
.word 0xf2d3333e
.word 0xf2e7f93e
.word 0x9e6703c0
bl _p_124
.word 0xf94013b1
.word 0xf9459631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf945a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf945b631
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8d37bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_7
.word 0xd2801e60
.word 0xaa1103e1
bl _p_7

Lme_52:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__ToggleMenum__3
FlyoutNavigation_FlyoutNavigationController__ToggleMenum__3:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1520]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_54
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0x350008c0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_19
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xb4000700
.word 0xf9400fb1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_19
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9417830
.word 0xd63f0200
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0x340003e0
.word 0xf9400fb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_19
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9414430
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1
.word 0xaa1a03e0
bl _p_100
.word 0xf9400fb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_54
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0x340003c0
.word 0xf9400fb1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_25
.word 0xf9400fb1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800000
.word 0xaa1a03e0
.word 0xd2800001
bl _p_50
.word 0xf9400fb1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9425231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400000c
.word 0xf9400fb1
.word 0xf9426631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_26
.word 0xf9400fb1
.word 0xf9428231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_53:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor
FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1528]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_46
.word 0xf9400fb1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_54:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_UAUIView_get_AccessibilityId
FlyoutNavigation_FlyoutNavigationController_UAUIView_get_AccessibilityId:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1536]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9401800
.word 0xf9001ba0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_55:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string
FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1544]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba1
.word 0xf9400fa0
.word 0xf9001820
.word 0x9100c021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_56:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor
FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1552]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_57:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__m__0
FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__m__0:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1560]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xf9402002
.word 0xaa1a03e0
.word 0xf9400b41
.word 0xaa0203e0
.word 0xf940005e
bl _p_125
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_58:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor
FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1568]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_59:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__m__0
FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__m__0:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1576]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f41
.word 0xaa1a03e0
.word 0xf9400b40
.word 0xf9003820
.word 0x9101c021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xf9001ba0
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xf90023a0
.word 0xaa1a03e0
.word 0xf9400f40
bl _p_79
.word 0x93407c00
.word 0xf90027a0
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94027a1
bl _p_126
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9401fa1
bl _p_127
.word 0xf9400fb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_5a:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor
FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1584]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_5b:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__m__0
FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__m__0:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1592]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400b40
.word 0xaa1a03e1
.word 0xb9801b41
bl _p_74
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_5c:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor
FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1600]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_5d:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__m__0
FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__m__0:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1608]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf9001ba1
.word 0xf9400c30
.word 0xd63f0200
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_5e:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor
FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1616]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_5f:
.text
	.align 4
	.no_dead_strip FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__m__0
FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__m__0:
.loc 1 1 0
.word 0xa9b07bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1624]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0x9102c3a0
.word 0xd2800000
.word 0xf9005ba0
.word 0xf9005fa0
.word 0xf90063a0
.word 0xf90067a0
.word 0xf9400fb1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800000
bl _p_121
.word 0xf9400fb1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400b40
.word 0x910243a1
.word 0xf9006ba1
bl _p_57
.word 0xf9406bbe
.word 0xfd0003c0
.word 0xfd0007c1
.word 0xfd000bc2
.word 0xfd000fc3
.word 0xf9400fb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0x910243a0
.word 0x9102c3a0
.word 0xf9404ba0
.word 0xf9005ba0
.word 0xf9404fa0
.word 0xf9005fa0
.word 0xf94053a0
.word 0xf90063a0
.word 0xf94057a0
.word 0xf90067a0
.word 0xf9400fb1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
.word 0xf9007ba0
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd007fa0
.word 0xf9400fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba0
.word 0xfd407fa0
bl _p_38
.word 0xf9400fb1
.word 0xf9414e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400b40
bl _p_67
.word 0xf9400fb1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400b40
.word 0x9102c3a1
.word 0x9101c3a1
.word 0xf9405ba1
.word 0xf9003ba1
.word 0xf9405fa1
.word 0xf9003fa1
.word 0xf94063a1
.word 0xf90043a1
.word 0xf94067a1
.word 0xf90047a1
.word 0x9101c3a1
.word 0xfd403ba0
.word 0xfd403fa1
.word 0xfd4043a2
.word 0xfd4047a3
bl _p_63
.word 0xf9400fb1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400b40
.word 0xf9402c01
.word 0x9102c3a0
.word 0x910143a0
.word 0xf9405ba0
.word 0xf9002ba0
.word 0xf9405fa0
.word 0xf9002fa0
.word 0xf94063a0
.word 0xf90033a0
.word 0xf94067a0
.word 0xf90037a0
.word 0xaa0103e0
.word 0x910143a2
.word 0xfd402ba0
.word 0xfd402fa1
.word 0xfd4033a2
.word 0xfd4037a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9426a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xeb1f035f
.word 0x10000011
.word 0x540007a0
.word 0x91006340
.word 0xf90073a0
.word 0xd2800000
.word 0xd2800000
bl _p_14
.word 0xfd0077a0
.word 0xf9400fb1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xfd4077a0
bl _p_38
.word 0xf9400fb1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf942ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400b40
.word 0xf9403401
.word 0xaa1a03e0
.word 0x91006340
.word 0x9100c3a2
.word 0xf9400002
.word 0xf9001ba2
.word 0xf9400402
.word 0xf9001fa2
.word 0xf9400802
.word 0xf90023a2
.word 0xf9400c00
.word 0xf90027a0
.word 0xaa0103e0
.word 0x9100c3a2
.word 0xfd401ba0
.word 0xfd401fa1
.word 0xfd4023a2
.word 0xfd4027a3
.word 0xf9400021
.word 0xf941a030
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf9433a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9434a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9435a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8d07bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_7

Lme_60:
.text
	.align 4
	.no_dead_strip wrapper_delegate_invoke_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool_invoke_TResult_T1_T2_UIKit_UIGestureRecognizer_UIKit_UITouch
wrapper_delegate_invoke_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool_invoke_TResult_T1_T2_UIKit_UIGestureRecognizer_UIKit_UITouch:
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xaa0203fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1632]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xd2800013
.word 0x3901a3bf
.word 0xf9402bb1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1640]
.word 0xb9400000
.word 0x34000140
bl _p_128
.word 0xf9003ba0
.word 0xf9403ba1
.word 0xf9403ba0
.word 0xf9003fa1
.word 0xb4000060
.word 0xf9403fa0
bl _p_98
.word 0xf9403fa0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0x9101a300
.word 0xf9403700
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xb5000500
.word 0xaa1803e0
.word 0xaa1803e0
.word 0x91008300
.word 0xf9401300
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xb4000240
.word 0xaa1303e0
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0x9100e300
.word 0xf9401f00
.word 0xaa1803e0
.word 0xaa1803e0
.word 0x91004300
.word 0xf9400b03
.word 0xaa1303e0
.word 0xaa1903e1
.word 0xaa1a03e2
.word 0xd63f0060
.word 0x53001c00
.word 0x1400003a
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0x9100e300
.word 0xf9401f00
.word 0xaa1803e0
.word 0xaa1803e0
.word 0x91004300
.word 0xf9400b02
.word 0xaa1903e0
.word 0xaa1a03e1
.word 0xd63f0040
.word 0x53001c00
.word 0x1400002b
.word 0xaa1503e0
.word 0xb9801aa0
.word 0xaa0003f6
.word 0xd2800017
.word 0xaa1503e0
.word 0xaa1703e0
.word 0x93407ee0
.word 0xb9801aa1
.word 0xeb00003f
.word 0x10000011
.word 0x54000569
.word 0xd37df000
.word 0x8b0002a0
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f4
.word 0xaa1403e3
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xaa0303e0
.word 0xaa1903e1
.word 0xaa1a03e2
.word 0xf90047a3
.word 0xf9400c70
.word 0xd63f0200
.word 0xf94047a1
.word 0xf90043a0
.word 0x53001c00
.word 0xf9402bb1
.word 0xf941d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x3901a3a0
.word 0xaa1703e0
.word 0x110006e0
.word 0xaa0003f7
.word 0xaa1703e0
.word 0xaa1603e1
.word 0x6b16001f
.word 0x54fffb8b
.word 0x3941a3a0
.word 0xf9402bb1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_7

Lme_66:
.text
	.align 4
	.no_dead_strip wrapper_delegate_invoke_System_Action_1_UIKit_UIPanGestureRecognizer_invoke_void_T_UIKit_UIPanGestureRecognizer
wrapper_delegate_invoke_System_Action_1_UIKit_UIPanGestureRecognizer_invoke_void_T_UIKit_UIPanGestureRecognizer:
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1648]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xf9402bb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1640]
.word 0xb9400000
.word 0x34000140
bl _p_128
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xaa1303e1
.word 0xf90037a0
.word 0xb4000073
.word 0xf94037a0
bl _p_98
.word 0xf94037a0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x9101a320
.word 0xf9403720
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xb5000440
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x91008320
.word 0xf9401320
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xb40001e0
.word 0xaa1403e0
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x9100e320
.word 0xf9401f20
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x91004320
.word 0xf9400b22
.word 0xaa1403e0
.word 0xaa1a03e1
.word 0xd63f0040
.word 0x14000030
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x9100e320
.word 0xf9401f20
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x91004320
.word 0xf9400b21
.word 0xaa1a03e0
.word 0xd63f0020
.word 0x14000024
.word 0xaa1603e0
.word 0xb9801ac0
.word 0xaa0003f7
.word 0xd2800018
.word 0xaa1603e0
.word 0xaa1803e0
.word 0x93407f00
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54000489
.word 0xd37df000
.word 0x8b0002c0
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f5
.word 0xaa1503e2
.word 0xaa1a03e0
.word 0xaa0203e0
.word 0xaa1a03e1
.word 0xf9003ba2
.word 0xf9400c50
.word 0xd63f0200
.word 0xf9403ba0
.word 0xf9402bb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x11000700
.word 0xaa0003f8
.word 0xaa1803e0
.word 0xaa1703e1
.word 0x6b17001f
.word 0x54fffc4b
.word 0xf9402bb1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_7

Lme_67:
.text
	.align 4
	.no_dead_strip wrapper_delegate_invoke_System_Action_1_Foundation_NSIndexPath_invoke_void_T_Foundation_NSIndexPath
wrapper_delegate_invoke_System_Action_1_Foundation_NSIndexPath_invoke_void_T_Foundation_NSIndexPath:
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1656]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xf9402bb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x0, [x16, #1640]
.word 0xb9400000
.word 0x34000140
bl _p_128
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xaa1303e1
.word 0xf90037a0
.word 0xb4000073
.word 0xf94037a0
bl _p_98
.word 0xf94037a0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x9101a320
.word 0xf9403720
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xb5000440
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x91008320
.word 0xf9401320
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xb40001e0
.word 0xaa1403e0
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x9100e320
.word 0xf9401f20
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x91004320
.word 0xf9400b22
.word 0xaa1403e0
.word 0xaa1a03e1
.word 0xd63f0040
.word 0x14000030
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x9100e320
.word 0xf9401f20
.word 0xaa1903e0
.word 0xaa1903e0
.word 0x91004320
.word 0xf9400b21
.word 0xaa1a03e0
.word 0xd63f0020
.word 0x14000024
.word 0xaa1603e0
.word 0xb9801ac0
.word 0xaa0003f7
.word 0xd2800018
.word 0xaa1603e0
.word 0xaa1803e0
.word 0x93407f00
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54000489
.word 0xd37df000
.word 0x8b0002c0
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f5
.word 0xaa1503e2
.word 0xaa1a03e0
.word 0xaa0203e0
.word 0xaa1a03e1
.word 0xf9003ba2
.word 0xf9400c50
.word 0xd63f0200
.word 0xf9403ba0
.word 0xf9402bb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x11000700
.word 0xaa0003f8
.word 0xaa1803e0
.word 0xaa1703e1
.word 0x6b17001f
.word 0x54fffc4b
.word 0xf9402bb1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_7

Lme_68:
.text
	.align 4
	.no_dead_strip System_Array_InternalArray__IEnumerable_GetEnumerator_T_REF
System_Array_InternalArray__IEnumerable_GetEnumerator_T_REF:
.file 2 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/mcs/class/corlib/System/Array.cs"
.loc 2 71 0 prologue_end
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9002baf
.word 0xf9000ba0

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1664]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf90037a0
.word 0x910103a0
.word 0xd2800000
.word 0xf90023a0
.word 0xf90027a0
.word 0x910103a0
.word 0xf90033a0
.word 0xf9402ba0
bl _p_129
.word 0xaa0003ef
.word 0xf94033a0
.word 0xf94037a1
bl _p_130
.word 0x910103a0
.word 0x9100c3a0
.word 0xf94023a0
.word 0xf9001ba0
.word 0xf94027a0
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_129
.word 0xd2800401
.word 0xd2800401
bl _p_1
.word 0x9100c3a1
.word 0x91004003
.word 0xaa0303e1
.word 0xf9401ba2
.word 0xf9000062
.word 0xd349fc23
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0063

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x4, [x16, #16]
.word 0x8b040063
.word 0xd280003e
.word 0x3900007e
.word 0x91002021
.word 0xf9401fa2
.word 0xf9000022
.word 0xf9400fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_69:
.text
ut_106:
add x0, x0, 16
b System_Array_InternalEnumerator_1_T_REF__ctor_System_Array
ut_end:
.section __TEXT, __const
_unbox_trampoline_p:

	.long 0
LDIFF_SYM3=ut_end - ut_106
	.long LDIFF_SYM3
.text
	.align 4
	.no_dead_strip System_Array_InternalEnumerator_1_T_REF__ctor_System_Array
System_Array_InternalEnumerator_1_T_REF__ctor_System_Array:
.loc 2 215 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xf9001faf
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1672]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf94013b1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa0
.word 0xf9000320
.word 0xaa1903e1
.word 0xd349ff21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 2 216 0
.word 0xf94013b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x92800020
.word 0xf2bfffe0
.word 0x9280003e
.word 0xf2bffffe
.word 0xb9000b3e
.loc 2 217 0
.word 0xf94013b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_6a:
.text
	.align 3
jit_code_end:

	.byte 0,0,0,0
.text
	.align 3
method_addresses:
	.no_dead_strip method_addresses
bl FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool
bl FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor
bl FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__m__0_UIKit_UIGestureRecognizer_UIKit_UITouch
bl FlyoutNavigation_FlyoutNavigationController__ctor_intptr
bl FlyoutNavigation_FlyoutNavigationController__ctor_UIKit_UITableViewStyle
bl FlyoutNavigation_FlyoutNavigationController_get_NavigationViewController
bl FlyoutNavigation_FlyoutNavigationController_get_MenuBorderColor
bl FlyoutNavigation_FlyoutNavigationController_set_MenuBorderColor_UIKit_UIColor
bl FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder
bl FlyoutNavigation_FlyoutNavigationController_set_ShowMenuBorder_bool
bl FlyoutNavigation_FlyoutNavigationController_get_TintColor
bl FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor
bl FlyoutNavigation_FlyoutNavigationController_get_Position
bl FlyoutNavigation_FlyoutNavigationController_set_Position_FlyoutNavigation_FlyOutNavigationPosition
bl FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged
bl FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action
bl FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu
bl FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool
bl FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen
bl FlyoutNavigation_FlyoutNavigationController_set_ForceMenuOpen_bool
bl FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation
bl FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool
bl FlyoutNavigation_FlyoutNavigationController_get_HideShadow
bl FlyoutNavigation_FlyoutNavigationController_set_HideShadow_bool
bl FlyoutNavigation_FlyoutNavigationController_get_ShadowViewColor
bl FlyoutNavigation_FlyoutNavigationController_set_ShadowViewColor_UIKit_UIColor
bl FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController
bl FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController
bl FlyoutNavigation_FlyoutNavigationController_get_mainView
bl FlyoutNavigation_FlyoutNavigationController_get_NavigationRoot
bl FlyoutNavigation_FlyoutNavigationController_set_NavigationRoot_MonoTouch_Dialog_RootElement
bl FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView
bl FlyoutNavigation_FlyoutNavigationController_get_ViewControllers
bl FlyoutNavigation_FlyoutNavigationController_set_ViewControllers_UIKit_UIViewController__
bl FlyoutNavigation_FlyoutNavigationController_get_IsOpen
bl FlyoutNavigation_FlyoutNavigationController_set_IsOpen_bool
bl FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen
bl FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex
bl FlyoutNavigation_FlyoutNavigationController_set_SelectedIndex_int
bl FlyoutNavigation_FlyoutNavigationController_get_DisableRotation
bl FlyoutNavigation_FlyoutNavigationController_set_DisableRotation_bool
bl FlyoutNavigation_FlyoutNavigationController_get_ShouldAutomaticallyForwardRotationMethods
bl FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle
bl FlyoutNavigation_FlyoutNavigationController_CloseButtonTapped_object_System_EventArgs
bl FlyoutNavigation_FlyoutNavigationController_add_ShouldReceiveTouch_UIKit_UITouchEventArgs
bl FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs
bl FlyoutNavigation_FlyoutNavigationController_get_DisableGesture
bl FlyoutNavigation_FlyoutNavigationController_set_DisableGesture_bool
bl FlyoutNavigation_FlyoutNavigationController_shouldReceiveTouch_UIKit_UIGestureRecognizer_UIKit_UITouch
bl FlyoutNavigation_FlyoutNavigationController_ViewDidLayoutSubviews
bl FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer
bl FlyoutNavigation_FlyoutNavigationController_ViewWillAppear_bool
bl FlyoutNavigation_FlyoutNavigationController_ViewDidAppear_bool
bl FlyoutNavigation_FlyoutNavigationController_ViewWillDisappear_bool
bl FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath
bl FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int
bl FlyoutNavigation_FlyoutNavigationController_ShowMenu
bl FlyoutNavigation_FlyoutNavigationController_setViewSize
bl FlyoutNavigation_FlyoutNavigationController_GetViewBounds
bl FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect
bl FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving
bl FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool
bl FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect
bl FlyoutNavigation_FlyoutNavigationController_getStatus
bl FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage
bl FlyoutNavigation_FlyoutNavigationController_hideStatus
bl FlyoutNavigation_FlyoutNavigationController_HideMenu
bl FlyoutNavigation_FlyoutNavigationController_hideComplete
bl FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView
bl FlyoutNavigation_FlyoutNavigationController_ToggleMenu
bl FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath
bl FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int
bl FlyoutNavigation_FlyoutNavigationController_ShouldAutorotateToInterfaceOrientation_UIKit_UIInterfaceOrientation
bl FlyoutNavigation_FlyoutNavigationController_GetSupportedInterfaceOrientations
bl FlyoutNavigation_FlyoutNavigationController_WillRotate_UIKit_UIInterfaceOrientation_double
bl FlyoutNavigation_FlyoutNavigationController_DidRotate_UIKit_UIInterfaceOrientation
bl FlyoutNavigation_FlyoutNavigationController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double
bl FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action
bl FlyoutNavigation_FlyoutNavigationController_IsMainThread
bl FlyoutNavigation_FlyoutNavigationController_Dispose_bool
bl FlyoutNavigation_FlyoutNavigationController__Initializem__0
bl FlyoutNavigation_FlyoutNavigationController__ShowMenum__1
bl FlyoutNavigation_FlyoutNavigationController__HideMenum__2
bl FlyoutNavigation_FlyoutNavigationController__ToggleMenum__3
bl FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor
bl FlyoutNavigation_FlyoutNavigationController_UAUIView_get_AccessibilityId
bl FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string
bl FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor
bl FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__m__0
bl FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor
bl FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__m__0
bl FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor
bl FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__m__0
bl FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor
bl FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__m__0
bl FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor
bl FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__m__0
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl wrapper_delegate_invoke_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool_invoke_TResult_T1_T2_UIKit_UIGestureRecognizer_UIKit_UITouch
bl wrapper_delegate_invoke_System_Action_1_UIKit_UIPanGestureRecognizer_invoke_void_T_UIKit_UIPanGestureRecognizer
bl wrapper_delegate_invoke_System_Action_1_Foundation_NSIndexPath_invoke_void_T_Foundation_NSIndexPath
bl System_Array_InternalArray__IEnumerable_GetEnumerator_T_REF
bl System_Array_InternalEnumerator_1_T_REF__ctor_System_Array
method_addresses_end:

.section __TEXT, __const
	.align 3
unbox_trampolines:

	.long 106
unbox_trampolines_end:

	.long 0
.text
	.align 3
unbox_trampoline_addresses:
bl ut_106

	.long 0
.section __TEXT, __const
	.align 3
unwind_info:

	.byte 0,25,12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,148,14,68,150,13,151,12,68,152,11,13,12,31,0,68
	.byte 14,48,157,6,158,5,68,13,29,30,12,31,0,68,14,144,1,157,18,158,17,68,13,29,68,147,16,148,15,68,149,14
	.byte 150,13,68,151,12,68,154,11,16,12,31,0,68,14,80,157,10,158,9,68,13,29,68,153,8,13,12,31,0,68,14,64
	.byte 157,8,158,7,68,13,29,16,12,31,0,68,14,64,157,8,158,7,68,13,29,68,153,6,24,12,31,0,68,14,144,1
	.byte 157,18,158,17,68,13,29,68,150,16,151,15,68,152,14,153,13,18,12,31,0,68,14,96,157,12,158,11,68,13,29,68
	.byte 153,10,154,9,13,12,31,0,68,14,80,157,10,158,9,68,13,29,16,12,31,0,68,14,64,157,8,158,7,68,13,29
	.byte 68,154,6,18,12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,153,7,17,12,31,0,68,14,224,1,157,28
	.byte 158,27,68,13,29,68,154,26,32,12,31,0,84,14,192,5,157,88,158,87,68,13,29,68,147,86,148,85,68,149,84,150
	.byte 83,68,151,82,152,81,68,153,80,28,12,31,0,68,14,112,157,14,158,13,68,13,29,68,149,12,150,11,68,151,10,152
	.byte 9,68,153,8,154,7,17,12,31,0,68,14,240,2,157,46,158,45,68,13,29,68,154,44,29,12,31,0,68,14,144,3
	.byte 157,50,158,49,68,13,29,68,148,48,149,47,68,150,46,151,45,68,152,44,153,43,17,12,31,0,68,14,160,3,157,52
	.byte 158,51,68,13,29,68,153,50,17,12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,153,20,29,12,31,0,68,14
	.byte 240,1,157,30,158,29,68,13,29,68,149,28,150,27,68,151,26,152,25,68,153,24,154,23,24,12,31,0,68,14,144,2
	.byte 157,34,158,33,68,13,29,68,151,32,152,31,68,153,30,154,29,17,12,31,0,68,14,176,2,157,38,158,37,68,13,29
	.byte 68,154,36,17,12,31,0,84,14,128,6,157,96,158,95,68,13,29,68,154,94,27,12,31,0,68,14,160,1,157,20,158
	.byte 19,68,13,29,68,150,18,151,17,68,152,16,153,15,68,154,14,17,12,31,0,68,14,144,1,157,18,158,17,68,13,29
	.byte 68,154,16,17,12,31,0,68,14,160,1,157,20,158,19,68,13,29,68,154,18,26,12,31,0,68,14,96,157,12,158,11
	.byte 68,13,29,68,150,10,151,9,68,152,8,153,7,68,154,6,16,12,31,0,68,14,48,157,6,158,5,68,13,29,68,154
	.byte 4,23,12,31,0,68,14,112,157,14,158,13,68,13,29,68,151,12,152,11,68,153,10,154,9,29,12,31,0,68,14,160
	.byte 1,157,20,158,19,68,13,29,68,149,18,150,17,68,151,16,152,15,68,153,14,154,13,24,12,31,0,68,14,144,1,157
	.byte 18,158,17,68,13,29,68,149,16,150,15,68,153,14,154,13,19,12,31,0,68,14,176,1,157,22,158,21,68,13,29,68
	.byte 152,20,153,19,16,12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,29,12,31,0,68,14,128,1,157,16,158
	.byte 15,68,13,29,68,148,14,149,13,68,150,12,151,11,68,152,10,153,9,24,12,31,0,84,14,240,4,157,78,158,77,68
	.byte 13,29,68,151,76,152,75,68,153,74,154,73,19,12,31,0,68,14,176,2,157,38,158,37,68,13,29,68,153,36,154,35
	.byte 16,12,31,0,68,14,80,157,10,158,9,68,13,29,68,154,8,17,12,31,0,68,14,128,2,157,32,158,31,68,13,29
	.byte 68,154,30,34,12,31,0,68,14,144,1,157,18,158,17,68,13,29,68,147,16,148,15,68,149,14,150,13,68,151,12,152
	.byte 11,68,153,10,154,9,34,12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,147,14,148,13,68,149,12,150,11,68
	.byte 151,10,152,9,68,153,8,154,7,13,12,31,0,68,14,112,157,14,158,13,68,13,29

.text
	.align 4
plt:
mono_aot_FlyoutNavigation_plt:
	.no_dead_strip plt_wrapper_alloc_object_AllocSmall_intptr_intptr
plt_wrapper_alloc_object_AllocSmall_intptr_intptr:
_p_1:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1688]
br x16
.word 1481
	.no_dead_strip plt_FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor
plt_FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor:
_p_2:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1696]
br x16
.word 1489
	.no_dead_strip plt_UIKit_UIPanGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer
plt_UIKit_UIPanGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer:
_p_3:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1704]
br x16
.word 1494
	.no_dead_strip plt_UIKit_UIGestureRecognizer_get_ShouldReceiveTouch
plt_UIKit_UIGestureRecognizer_get_ShouldReceiveTouch:
_p_4:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1712]
br x16
.word 1499
	.no_dead_strip plt_System_Delegate_Combine_System_Delegate_System_Delegate
plt_System_Delegate_Combine_System_Delegate_System_Delegate:
_p_5:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1720]
br x16
.word 1504
	.no_dead_strip plt_UIKit_UIGestureRecognizer_set_ShouldReceiveTouch_UIKit_UITouchEventArgs
plt_UIKit_UIGestureRecognizer_set_ShouldReceiveTouch_UIKit_UITouchEventArgs:
_p_6:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1728]
br x16
.word 1507
	.no_dead_strip plt__jit_icall_mono_arch_throw_corlib_exception
plt__jit_icall_mono_arch_throw_corlib_exception:
_p_7:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1736]
br x16
.word 1512
	.no_dead_strip plt_string_IndexOf_string_System_StringComparison
plt_string_IndexOf_string_System_StringComparison:
_p_8:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1744]
br x16
.word 1547
	.no_dead_strip plt_UIKit_UIColor_get_LightGray
plt_UIKit_UIColor_get_LightGray:
_p_9:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1752]
br x16
.word 1550
	.no_dead_strip plt_UIKit_UIViewController__ctor_intptr
plt_UIKit_UIViewController__ctor_intptr:
_p_10:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1760]
br x16
.word 1555
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle
plt_FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle:
_p_11:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1768]
br x16
.word 1560
	.no_dead_strip plt_UIKit_UIViewController__ctor
plt_UIKit_UIViewController__ctor:
_p_12:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1776]
br x16
.word 1565
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_Position
plt_FlyoutNavigation_FlyoutNavigationController_get_Position:
_p_13:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1784]
br x16
.word 1570
	.no_dead_strip plt_System_nfloat_op_Implicit_int
plt_System_nfloat_op_Implicit_int:
_p_14:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1792]
br x16
.word 1575
	.no_dead_strip plt_CoreGraphics_CGSize__ctor_System_nfloat_System_nfloat
plt_CoreGraphics_CGSize__ctor_System_nfloat_System_nfloat:
_p_15:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1800]
br x16
.word 1580
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_mainView
plt_FlyoutNavigation_FlyoutNavigationController_get_mainView:
_p_16:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1808]
br x16
.word 1585
	.no_dead_strip plt__jit_icall_ves_icall_object_new_specific
plt__jit_icall_ves_icall_object_new_specific:
_p_17:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1816]
br x16
.word 1590
	.no_dead_strip plt_UIKit_UIColor__ctor_CoreGraphics_CGColor
plt_UIKit_UIColor__ctor_CoreGraphics_CGColor:
_p_18:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1824]
br x16
.word 1622
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController
plt_FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController:
_p_19:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1832]
br x16
.word 1627
	.no_dead_strip plt_MonoTouch_Dialog_DialogViewController_get_Root
plt_MonoTouch_Dialog_DialogViewController_get_Root:
_p_20:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1840]
br x16
.word 1632
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor
plt_FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor:
_p_21:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1848]
br x16
.word 1637
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action
plt_FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action:
_p_22:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1856]
br x16
.word 1642
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor
plt_FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor:
_p_23:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1864]
br x16
.word 1647
	.no_dead_strip plt_CoreGraphics_CGRect_get_X
plt_CoreGraphics_CGRect_get_X:
_p_24:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1872]
br x16
.word 1652
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_HideMenu
plt_FlyoutNavigation_FlyoutNavigationController_HideMenu:
_p_25:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1880]
br x16
.word 1657
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_ShowMenu
plt_FlyoutNavigation_FlyoutNavigationController_ShowMenu:
_p_26:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1888]
br x16
.word 1662
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen
plt_FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen:
_p_27:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1896]
br x16
.word 1667
	.no_dead_strip plt_UIKit_UIDevice_get_CurrentDevice
plt_UIKit_UIDevice_get_CurrentDevice:
_p_28:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1904]
br x16
.word 1672
	.no_dead_strip plt_UIKit_UIDevice_get_UserInterfaceIdiom
plt_UIKit_UIDevice_get_UserInterfaceIdiom:
_p_29:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1912]
br x16
.word 1677
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu
plt_FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu:
_p_30:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1920]
br x16
.word 1682
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor
plt_FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor:
_p_31:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1928]
br x16
.word 1687
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool
plt_FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool:
_p_32:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1936]
br x16
.word 1692
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor
plt_FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor:
_p_33:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1944]
br x16
.word 1697
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string
plt_FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string:
_p_34:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1952]
br x16
.word 1702
	.no_dead_strip plt_MonoTouch_Dialog_DialogViewController__ctor_UIKit_UITableViewStyle_MonoTouch_Dialog_RootElement
plt_MonoTouch_Dialog_DialogViewController__ctor_UIKit_UITableViewStyle_MonoTouch_Dialog_RootElement:
_p_35:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1960]
br x16
.word 1707
	.no_dead_strip plt_CoreGraphics_CGRect_set_Width_System_nfloat
plt_CoreGraphics_CGRect_set_Width_System_nfloat:
_p_36:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1968]
br x16
.word 1712
	.no_dead_strip plt_CoreGraphics_CGRect_get_Width
plt_CoreGraphics_CGRect_get_Width:
_p_37:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1976]
br x16
.word 1717
	.no_dead_strip plt_CoreGraphics_CGRect_set_X_System_nfloat
plt_CoreGraphics_CGRect_set_X_System_nfloat:
_p_38:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1984]
br x16
.word 1722
	.no_dead_strip plt_UIKit_UIColor_get_Black
plt_UIKit_UIColor_get_Black:
_p_39:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #1992]
br x16
.word 1727
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor
plt_FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor:
_p_40:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2000]
br x16
.word 1732
	.no_dead_strip plt_System_Version__ctor_string
plt_System_Version__ctor_string:
_p_41:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2008]
br x16
.word 1737
	.no_dead_strip plt_System_Version_get_Major
plt_System_Version_get_Major:
_p_42:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2016]
br x16
.word 1740
	.no_dead_strip plt_CoreGraphics_CGRect__ctor_System_nfloat_System_nfloat_System_nfloat_System_nfloat
plt_CoreGraphics_CGRect__ctor_System_nfloat_System_nfloat_System_nfloat_System_nfloat:
_p_43:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2024]
br x16
.word 1743
	.no_dead_strip plt_UIKit_UIView__ctor_CoreGraphics_CGRect
plt_UIKit_UIView__ctor_CoreGraphics_CGRect:
_p_44:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2032]
br x16
.word 1748
	.no_dead_strip plt_UIKit_UIColor_get_Clear
plt_UIKit_UIColor_get_Clear:
_p_45:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2040]
br x16
.word 1753
	.no_dead_strip plt_UIKit_UIView__ctor
plt_UIKit_UIView__ctor:
_p_46:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2048]
br x16
.word 1758
	.no_dead_strip plt_UIKit_UIButton__ctor
plt_UIKit_UIButton__ctor:
_p_47:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2056]
br x16
.word 1763
	.no_dead_strip plt_UIKit_UIControl_add_TouchUpInside_System_EventHandler
plt_UIKit_UIControl_add_TouchUpInside_System_EventHandler:
_p_48:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2064]
br x16
.word 1768
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool
plt_FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool:
_p_49:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2072]
br x16
.word 1773
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool
plt_FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool:
_p_50:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2080]
br x16
.word 1778
	.no_dead_strip plt_UIKit_UIScreenEdgePanGestureRecognizer__ctor_System_Action
plt_UIKit_UIScreenEdgePanGestureRecognizer__ctor_System_Action:
_p_51:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2088]
br x16
.word 1783
	.no_dead_strip plt_FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool
plt_FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool:
_p_52:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2096]
br x16
.word 1788
	.no_dead_strip plt_System_Delegate_Remove_System_Delegate_System_Delegate
plt_System_Delegate_Remove_System_Delegate_System_Delegate:
_p_53:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2104]
br x16
.word 1793
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_IsOpen
plt_FlyoutNavigation_FlyoutNavigationController_get_IsOpen:
_p_54:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2112]
br x16
.word 1796
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_DisableGesture
plt_FlyoutNavigation_FlyoutNavigationController_get_DisableGesture:
_p_55:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2120]
br x16
.word 1801
	.no_dead_strip plt_UIKit_UIViewController_ViewDidLayoutSubviews
plt_UIKit_UIViewController_ViewDidLayoutSubviews:
_p_56:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2128]
br x16
.word 1806
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_GetViewBounds
plt_FlyoutNavigation_FlyoutNavigationController_GetViewBounds:
_p_57:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2136]
br x16
.word 1811
	.no_dead_strip plt_CoreGraphics_CGRect_op_Inequality_CoreGraphics_CGRect_CoreGraphics_CGRect
plt_CoreGraphics_CGRect_op_Inequality_CoreGraphics_CGRect_CoreGraphics_CGRect:
_p_58:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2144]
br x16
.word 1816
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect
plt_FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect:
_p_59:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2152]
br x16
.word 1821
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen
plt_FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen:
_p_60:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2160]
br x16
.word 1826
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_HideShadow
plt_FlyoutNavigation_FlyoutNavigationController_get_HideShadow:
_p_61:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2168]
br x16
.word 1831
	.no_dead_strip plt_CoreGraphics_CGPoint_get_X
plt_CoreGraphics_CGPoint_get_X:
_p_62:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2176]
br x16
.word 1836
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect
plt_FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect:
_p_63:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2184]
br x16
.word 1841
	.no_dead_strip plt_System_Math_Abs_double
plt_System_Math_Abs_double:
_p_64:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2192]
br x16
.word 1846
	.no_dead_strip plt_CoreGraphics_CGRect_set_Location_CoreGraphics_CGPoint
plt_CoreGraphics_CGRect_set_Location_CoreGraphics_CGPoint:
_p_65:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2200]
br x16
.word 1849
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView
plt_FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView:
_p_66:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2208]
br x16
.word 1854
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_setViewSize
plt_FlyoutNavigation_FlyoutNavigationController_setViewSize:
_p_67:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2216]
br x16
.word 1859
	.no_dead_strip plt_MonoTouch_Dialog_DialogViewController_add_OnSelection_System_Action_1_Foundation_NSIndexPath
plt_MonoTouch_Dialog_DialogViewController_add_OnSelection_System_Action_1_Foundation_NSIndexPath:
_p_68:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2224]
br x16
.word 1864
	.no_dead_strip plt_UIKit_UIViewController_ViewWillAppear_bool
plt_UIKit_UIViewController_ViewWillAppear_bool:
_p_69:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2232]
br x16
.word 1869
	.no_dead_strip plt_UIKit_UIViewController_ViewDidAppear_bool
plt_UIKit_UIViewController_ViewDidAppear_bool:
_p_70:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2240]
br x16
.word 1874
	.no_dead_strip plt_UIKit_UIViewController_ViewWillDisappear_bool
plt_UIKit_UIViewController_ViewWillDisappear_bool:
_p_71:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2248]
br x16
.word 1879
	.no_dead_strip plt_MonoTouch_Dialog_DialogViewController_remove_OnSelection_System_Action_1_Foundation_NSIndexPath
plt_MonoTouch_Dialog_DialogViewController_remove_OnSelection_System_Action_1_Foundation_NSIndexPath:
_p_72:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2256]
br x16
.word 1884
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath
plt_FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath:
_p_73:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2264]
br x16
.word 1889
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int
plt_FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int:
_p_74:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2272]
br x16
.word 1894
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged
plt_FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged:
_p_75:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2280]
br x16
.word 1899
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_ViewControllers
plt_FlyoutNavigation_FlyoutNavigationController_get_ViewControllers:
_p_76:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2288]
br x16
.word 1904
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving
plt_FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving:
_p_77:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2296]
br x16
.word 1909
	.no_dead_strip plt_UIKit_UIApplication_get_SharedApplication
plt_UIKit_UIApplication_get_SharedApplication:
_p_78:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2304]
br x16
.word 1914
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex
plt_FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex:
_p_79:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2312]
br x16
.word 1919
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController
plt_FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController:
_p_80:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2320]
br x16
.word 1924
	.no_dead_strip plt_CoreGraphics_CGRect_get_Height
plt_CoreGraphics_CGRect_get_Height:
_p_81:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2328]
br x16
.word 1929
	.no_dead_strip plt_System_NMath_Max_System_nfloat_System_nfloat
plt_System_NMath_Max_System_nfloat_System_nfloat:
_p_82:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2336]
br x16
.word 1934
	.no_dead_strip plt_System_NMath_Min_System_nfloat_System_nfloat
plt_System_NMath_Min_System_nfloat_System_nfloat:
_p_83:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2344]
br x16
.word 1939
	.no_dead_strip plt_CoreGraphics_CGRect_set_Height_System_nfloat
plt_CoreGraphics_CGRect_set_Height_System_nfloat:
_p_84:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2352]
br x16
.word 1944
	.no_dead_strip plt_CoreGraphics_CGPoint__ctor_System_nfloat_System_nfloat
plt_CoreGraphics_CGPoint__ctor_System_nfloat_System_nfloat:
_p_85:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2360]
br x16
.word 1949
	.no_dead_strip plt_CoreGraphics_CGRect_set_Y_System_nfloat
plt_CoreGraphics_CGRect_set_Y_System_nfloat:
_p_86:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2368]
br x16
.word 1954
	.no_dead_strip plt_CoreGraphics_CGRect_get_Location
plt_CoreGraphics_CGRect_get_Location:
_p_87:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2376]
br x16
.word 1959
	.no_dead_strip plt_CoreGraphics_CGPoint_op_Equality_CoreGraphics_CGPoint_CoreGraphics_CGPoint
plt_CoreGraphics_CGPoint_op_Equality_CoreGraphics_CGPoint_CoreGraphics_CGPoint:
_p_88:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2384]
br x16
.word 1964
	.no_dead_strip plt_CoreGraphics_CGRect_get_Size
plt_CoreGraphics_CGRect_get_Size:
_p_89:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2392]
br x16
.word 1969
	.no_dead_strip plt_CoreGraphics_CGRect_set_Size_CoreGraphics_CGSize
plt_CoreGraphics_CGRect_set_Size_CoreGraphics_CGSize:
_p_90:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2400]
br x16
.word 1974
	.no_dead_strip plt_CoreGraphics_CGRect_get_Left
plt_CoreGraphics_CGRect_get_Left:
_p_91:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2408]
br x16
.word 1979
	.no_dead_strip plt_CoreGraphics_CGRect_get_Top
plt_CoreGraphics_CGRect_get_Top:
_p_92:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2416]
br x16
.word 1984
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_getStatus
plt_FlyoutNavigation_FlyoutNavigationController_getStatus:
_p_93:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2424]
br x16
.word 1989
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder
plt_FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder:
_p_94:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2432]
br x16
.word 1994
	.no_dead_strip plt_UIKit_UIScreen_get_MainScreen
plt_UIKit_UIScreen_get_MainScreen:
_p_95:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2440]
br x16
.word 1999
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage
plt_FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage:
_p_96:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2448]
br x16
.word 2004
	.no_dead_strip plt__jit_icall_mono_thread_get_undeniable_exception
plt__jit_icall_mono_thread_get_undeniable_exception:
_p_97:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2456]
br x16
.word 2009
	.no_dead_strip plt__jit_icall_mono_arch_throw_exception
plt__jit_icall_mono_arch_throw_exception:
_p_98:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2464]
br x16
.word 2048
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_hideStatus
plt_FlyoutNavigation_FlyoutNavigationController_hideStatus:
_p_99:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2472]
br x16
.word 2076
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView
plt_FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView:
_p_100:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2480]
br x16
.word 2081
	.no_dead_strip plt_MonoTouch_Dialog_RootElement_get_Item_int
plt_MonoTouch_Dialog_RootElement_get_Item_int:
_p_101:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2488]
br x16
.word 2086
	.no_dead_strip plt_MonoTouch_Dialog_Section_get_Count
plt_MonoTouch_Dialog_Section_get_Count:
_p_102:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2496]
br x16
.word 2091
	.no_dead_strip plt_Foundation_NSIndexPath_get_Section
plt_Foundation_NSIndexPath_get_Section:
_p_103:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2504]
br x16
.word 2096
	.no_dead_strip plt_Foundation_NSIndexPath_get_Row
plt_Foundation_NSIndexPath_get_Row:
_p_104:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2512]
br x16
.word 2101
	.no_dead_strip plt_Foundation_NSIndexPath_FromRowSection_System_nint_System_nint
plt_Foundation_NSIndexPath_FromRowSection_System_nint_System_nint:
_p_105:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2520]
br x16
.word 2106
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_DisableRotation
plt_FlyoutNavigation_FlyoutNavigationController_get_DisableRotation:
_p_106:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2528]
br x16
.word 2111
	.no_dead_strip plt_UIKit_UIViewController_WillRotate_UIKit_UIInterfaceOrientation_double
plt_UIKit_UIViewController_WillRotate_UIKit_UIInterfaceOrientation_double:
_p_107:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2536]
br x16
.word 2116
	.no_dead_strip plt_UIKit_UIViewController_DidRotate_UIKit_UIInterfaceOrientation
plt_UIKit_UIViewController_DidRotate_UIKit_UIInterfaceOrientation:
_p_108:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2544]
br x16
.word 2121
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation
plt_FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation:
_p_109:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2552]
br x16
.word 2126
	.no_dead_strip plt_UIKit_UIViewController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double
plt_UIKit_UIViewController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double:
_p_110:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2560]
br x16
.word 2131
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor
plt_FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor:
_p_111:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2568]
br x16
.word 2136
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_IsMainThread
plt_FlyoutNavigation_FlyoutNavigationController_IsMainThread:
_p_112:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2576]
br x16
.word 2141
	.no_dead_strip plt_Foundation_NSObject_BeginInvokeOnMainThread_System_Action
plt_Foundation_NSObject_BeginInvokeOnMainThread_System_Action:
_p_113:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2584]
br x16
.word 2146
	.no_dead_strip plt_Foundation_NSThread_get_Current
plt_Foundation_NSThread_get_Current:
_p_114:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2592]
br x16
.word 2151
	.no_dead_strip plt_UIKit_UIViewController_Dispose_bool
plt_UIKit_UIViewController_Dispose_bool:
_p_115:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2600]
br x16
.word 2156
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action
plt_FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action:
_p_116:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2608]
br x16
.word 2161
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs
plt_FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs:
_p_117:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2616]
br x16
.word 2166
	.no_dead_strip plt_UIKit_UIControl_remove_TouchUpInside_System_EventHandler
plt_UIKit_UIControl_remove_TouchUpInside_System_EventHandler:
_p_118:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2624]
br x16
.word 2171
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer
plt_FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer:
_p_119:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2632]
br x16
.word 2176
	.no_dead_strip plt_UIKit_UIView_BeginAnimations_string
plt_UIKit_UIView_BeginAnimations_string:
_p_120:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2640]
br x16
.word 2181
	.no_dead_strip plt_UIKit_UIView_SetAnimationCurve_UIKit_UIViewAnimationCurve
plt_UIKit_UIView_SetAnimationCurve_UIKit_UIViewAnimationCurve:
_p_121:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2648]
br x16
.word 2186
	.no_dead_strip plt_UIKit_UIView_CommitAnimations
plt_UIKit_UIView_CommitAnimations:
_p_122:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2656]
br x16
.word 2191
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor
plt_FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor:
_p_123:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2664]
br x16
.word 2196
	.no_dead_strip plt_UIKit_UIView_Animate_double_System_Action_System_Action
plt_UIKit_UIView_Animate_double_System_Action_System_Action:
_p_124:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2672]
br x16
.word 2201
	.no_dead_strip plt_MonoTouch_Dialog_DialogViewController_set_Root_MonoTouch_Dialog_RootElement
plt_MonoTouch_Dialog_DialogViewController_set_Root_MonoTouch_Dialog_RootElement:
_p_125:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2680]
br x16
.word 2206
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int
plt_FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int:
_p_126:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2688]
br x16
.word 2211
	.no_dead_strip plt_FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath
plt_FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath:
_p_127:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2696]
br x16
.word 2216
	.no_dead_strip plt__jit_icall_mono_thread_interruption_checkpoint
plt__jit_icall_mono_thread_interruption_checkpoint:
_p_128:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2704]
br x16
.word 2221
	.no_dead_strip plt__rgctx_fetch_0
plt__rgctx_fetch_0:
_p_129:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2712]
br x16
.word 2285
	.no_dead_strip plt_System_Array_InternalEnumerator_1_T_REF__ctor_System_Array
plt_System_Array_InternalEnumerator_1_T_REF__ctor_System_Array:
_p_130:
adrp x16, mono_aot_FlyoutNavigation_got@PAGE+0
add x16, x16, mono_aot_FlyoutNavigation_got@PAGEOFF
ldr x16, [x16, #2720]
br x16
.word 2293
plt_end:
.section __DATA, __bss
	.align 3
.lcomm mono_aot_FlyoutNavigation_got, 2728
got_end:
.section __TEXT, __const
	.align 3
Lglobals_hash:

	.short 11, 0, 0, 0, 0, 0, 0, 0
	.short 0, 0, 0, 0, 0, 1, 0, 0
	.short 0, 0, 0, 0, 0, 0, 0
.section __TEXT, __const
	.align 2
name_0:
	.asciz "_unbox_trampoline_p"
.data
	.align 3
globals:
	.align 3
	.quad Lglobals_hash
	.align 3
	.quad name_0
	.align 3
	.quad _unbox_trampoline_p

	.long 0,0
.section __TEXT, __const
	.align 2
runtime_version:
	.asciz ""
.section __TEXT, __const
	.align 2
assembly_guid:
	.asciz "06CF566F-435A-4FE4-A991-01C92B84D389"
.section __TEXT, __const
	.align 2
assembly_name:
	.asciz "FlyoutNavigation"
.data
	.align 3
_mono_aot_file_info:

	.long 139,0
	.align 3
	.quad mono_aot_FlyoutNavigation_got
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad jit_code_start
	.align 3
	.quad jit_code_end
	.align 3
	.quad method_addresses
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad mem_end
	.align 3
	.quad assembly_guid
	.align 3
	.quad runtime_version
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad 0
	.align 3
	.quad globals
	.align 3
	.quad assembly_name
	.align 3
	.quad plt
	.align 3
	.quad plt_end
	.align 3
	.quad unwind_info
	.align 3
	.quad unbox_trampolines
	.align 3
	.quad unbox_trampolines_end
	.align 3
	.quad unbox_trampoline_addresses

	.long 210,2728,131,107,70,387000831,0,23868
	.long 128,8,8,10,0,25,25280,1400
	.long 1040,392,0,776,992,480,0,312
	.long 160,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0
	.byte 59,16,95,116,95,12,32,76,247,28,170,19,227,28,228,103
	.globl _mono_aot_module_FlyoutNavigation_info
	.align 3
_mono_aot_module_FlyoutNavigation_info:
	.align 3
	.quad _mono_aot_file_info
.section __DWARF, __debug_info,regular,debug
LTDIE_4:

	.byte 17
	.asciz "System_Object"

	.byte 16,7
	.asciz "System_Object"

LDIFF_SYM4=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM4
LTDIE_4_POINTER:

	.byte 13
LDIFF_SYM5=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM5
LTDIE_4_REFERENCE:

	.byte 14
LDIFF_SYM6=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM6
LTDIE_5:

	.byte 8
	.asciz "_Flags"

	.byte 1
LDIFF_SYM7=LDIE_U1 - Ldebug_info_start
	.long LDIFF_SYM7
	.byte 9
	.asciz "Disposed"

	.byte 1,9
	.asciz "NativeRef"

	.byte 2,9
	.asciz "IsDirectBinding"

	.byte 4,9
	.asciz "RegisteredToggleRef"

	.byte 8,9
	.asciz "InFinalizerQueue"

	.byte 16,9
	.asciz "HasManagedRef"

	.byte 32,0,7
	.asciz "_Flags"

LDIFF_SYM8=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM8
LTDIE_5_POINTER:

	.byte 13
LDIFF_SYM9=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM9
LTDIE_5_REFERENCE:

	.byte 14
LDIFF_SYM10=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM10
LTDIE_3:

	.byte 5
	.asciz "Foundation_NSObject"

	.byte 40,16
LDIFF_SYM11=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM11
	.byte 2,35,0,6
	.asciz "handle"

LDIFF_SYM12=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM12
	.byte 2,35,16,6
	.asciz "class_handle"

LDIFF_SYM13=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM13
	.byte 2,35,24,6
	.asciz "flags"

LDIFF_SYM14=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM14
	.byte 2,35,32,0,7
	.asciz "Foundation_NSObject"

LDIFF_SYM15=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM15
LTDIE_3_POINTER:

	.byte 13
LDIFF_SYM16=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM16
LTDIE_3_REFERENCE:

	.byte 14
LDIFF_SYM17=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM17
LTDIE_2:

	.byte 5
	.asciz "UIKit_UIGestureRecognizer"

	.byte 56,16
LDIFF_SYM18=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM18
	.byte 2,35,0,6
	.asciz "__mt_WeakDelegate_var"

LDIFF_SYM19=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM19
	.byte 2,35,40,6
	.asciz "recognizers"

LDIFF_SYM20=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM20
	.byte 2,35,48,0,7
	.asciz "UIKit_UIGestureRecognizer"

LDIFF_SYM21=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM21
LTDIE_2_POINTER:

	.byte 13
LDIFF_SYM22=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM22
LTDIE_2_REFERENCE:

	.byte 14
LDIFF_SYM23=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM23
LTDIE_1:

	.byte 5
	.asciz "UIKit_UIPanGestureRecognizer"

	.byte 56,16
LDIFF_SYM24=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM24
	.byte 2,35,0,0,7
	.asciz "UIKit_UIPanGestureRecognizer"

LDIFF_SYM25=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM25
LTDIE_1_POINTER:

	.byte 13
LDIFF_SYM26=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM26
LTDIE_1_REFERENCE:

	.byte 14
LDIFF_SYM27=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM27
LTDIE_0:

	.byte 5
	.asciz "FlyoutNavigation_OpenMenuGestureRecognizer"

	.byte 56,16
LDIFF_SYM28=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM28
	.byte 2,35,0,0,7
	.asciz "FlyoutNavigation_OpenMenuGestureRecognizer"

LDIFF_SYM29=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM29
LTDIE_0_POINTER:

	.byte 13
LDIFF_SYM30=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM30
LTDIE_0_REFERENCE:

	.byte 14
LDIFF_SYM31=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM31
LTDIE_11:

	.byte 5
	.asciz "System_Reflection_MemberInfo"

	.byte 16,16
LDIFF_SYM32=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM32
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MemberInfo"

LDIFF_SYM33=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM33
LTDIE_11_POINTER:

	.byte 13
LDIFF_SYM34=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM34
LTDIE_11_REFERENCE:

	.byte 14
LDIFF_SYM35=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM35
LTDIE_10:

	.byte 5
	.asciz "System_Reflection_MethodBase"

	.byte 16,16
LDIFF_SYM36=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM36
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MethodBase"

LDIFF_SYM37=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM37
LTDIE_10_POINTER:

	.byte 13
LDIFF_SYM38=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM38
LTDIE_10_REFERENCE:

	.byte 14
LDIFF_SYM39=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM39
LTDIE_9:

	.byte 5
	.asciz "System_Reflection_MethodInfo"

	.byte 16,16
LDIFF_SYM40=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM40
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MethodInfo"

LDIFF_SYM41=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM41
LTDIE_9_POINTER:

	.byte 13
LDIFF_SYM42=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM42
LTDIE_9_REFERENCE:

	.byte 14
LDIFF_SYM43=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM43
LTDIE_13:

	.byte 5
	.asciz "System_Type"

	.byte 24,16
LDIFF_SYM44=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM44
	.byte 2,35,0,6
	.asciz "_impl"

LDIFF_SYM45=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM45
	.byte 2,35,16,0,7
	.asciz "System_Type"

LDIFF_SYM46=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM46
LTDIE_13_POINTER:

	.byte 13
LDIFF_SYM47=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM47
LTDIE_13_REFERENCE:

	.byte 14
LDIFF_SYM48=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM48
LTDIE_15:

	.byte 5
	.asciz "System_ValueType"

	.byte 16,16
LDIFF_SYM49=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM49
	.byte 2,35,0,0,7
	.asciz "System_ValueType"

LDIFF_SYM50=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM50
LTDIE_15_POINTER:

	.byte 13
LDIFF_SYM51=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM51
LTDIE_15_REFERENCE:

	.byte 14
LDIFF_SYM52=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM52
LTDIE_14:

	.byte 5
	.asciz "System_Boolean"

	.byte 17,16
LDIFF_SYM53=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM53
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM54=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM54
	.byte 2,35,16,0,7
	.asciz "System_Boolean"

LDIFF_SYM55=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM55
LTDIE_14_POINTER:

	.byte 13
LDIFF_SYM56=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM56
LTDIE_14_REFERENCE:

	.byte 14
LDIFF_SYM57=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM57
LTDIE_12:

	.byte 5
	.asciz "System_DelegateData"

	.byte 40,16
LDIFF_SYM58=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM58
	.byte 2,35,0,6
	.asciz "target_type"

LDIFF_SYM59=LTDIE_13_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM59
	.byte 2,35,16,6
	.asciz "method_name"

LDIFF_SYM60=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM60
	.byte 2,35,24,6
	.asciz "curried_first_arg"

LDIFF_SYM61=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM61
	.byte 2,35,32,0,7
	.asciz "System_DelegateData"

LDIFF_SYM62=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM62
LTDIE_12_POINTER:

	.byte 13
LDIFF_SYM63=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM63
LTDIE_12_REFERENCE:

	.byte 14
LDIFF_SYM64=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM64
LTDIE_8:

	.byte 5
	.asciz "System_Delegate"

	.byte 104,16
LDIFF_SYM65=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM65
	.byte 2,35,0,6
	.asciz "method_ptr"

LDIFF_SYM66=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM66
	.byte 2,35,16,6
	.asciz "invoke_impl"

LDIFF_SYM67=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM67
	.byte 2,35,24,6
	.asciz "m_target"

LDIFF_SYM68=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM68
	.byte 2,35,32,6
	.asciz "method"

LDIFF_SYM69=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM69
	.byte 2,35,40,6
	.asciz "delegate_trampoline"

LDIFF_SYM70=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM70
	.byte 2,35,48,6
	.asciz "extra_arg"

LDIFF_SYM71=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM71
	.byte 2,35,56,6
	.asciz "method_code"

LDIFF_SYM72=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM72
	.byte 2,35,64,6
	.asciz "method_info"

LDIFF_SYM73=LTDIE_9_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM73
	.byte 2,35,72,6
	.asciz "original_method_info"

LDIFF_SYM74=LTDIE_9_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM74
	.byte 2,35,80,6
	.asciz "data"

LDIFF_SYM75=LTDIE_12_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM75
	.byte 2,35,88,6
	.asciz "method_is_virtual"

LDIFF_SYM76=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM76
	.byte 2,35,96,0,7
	.asciz "System_Delegate"

LDIFF_SYM77=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM77
LTDIE_8_POINTER:

	.byte 13
LDIFF_SYM78=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM78
LTDIE_8_REFERENCE:

	.byte 14
LDIFF_SYM79=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM79
LTDIE_7:

	.byte 5
	.asciz "System_MulticastDelegate"

	.byte 112,16
LDIFF_SYM80=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM80
	.byte 2,35,0,6
	.asciz "delegates"

LDIFF_SYM81=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM81
	.byte 2,35,104,0,7
	.asciz "System_MulticastDelegate"

LDIFF_SYM82=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM82
LTDIE_7_POINTER:

	.byte 13
LDIFF_SYM83=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM83
LTDIE_7_REFERENCE:

	.byte 14
LDIFF_SYM84=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM84
LTDIE_6:

	.byte 5
	.asciz "System_Action`1"

	.byte 112,16
LDIFF_SYM85=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM85
	.byte 2,35,0,0,7
	.asciz "System_Action`1"

LDIFF_SYM86=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM86
LTDIE_6_POINTER:

	.byte 13
LDIFF_SYM87=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM87
LTDIE_6_REFERENCE:

	.byte 14
LDIFF_SYM88=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM88
LTDIE_16:

	.byte 5
	.asciz "System_Func`3"

	.byte 112,16
LDIFF_SYM89=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM89
	.byte 2,35,0,0,7
	.asciz "System_Func`3"

LDIFF_SYM90=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM90
LTDIE_16_POINTER:

	.byte 13
LDIFF_SYM91=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM91
LTDIE_16_REFERENCE:

	.byte 14
LDIFF_SYM92=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM92
LTDIE_17:

	.byte 5
	.asciz "_<OpenMenuGestureRecognizer>c__AnonStorey0"

	.byte 24,16
LDIFF_SYM93=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM93
	.byte 2,35,0,6
	.asciz "shouldReceiveTouch"

LDIFF_SYM94=LTDIE_16_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM94
	.byte 2,35,16,0,7
	.asciz "_<OpenMenuGestureRecognizer>c__AnonStorey0"

LDIFF_SYM95=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM95
LTDIE_17_POINTER:

	.byte 13
LDIFF_SYM96=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM96
LTDIE_17_REFERENCE:

	.byte 14
LDIFF_SYM97=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM97
	.byte 2
	.asciz "FlyoutNavigation.OpenMenuGestureRecognizer:.ctor"
	.asciz "FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool"

	.byte 0,0
	.quad FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool
	.quad Lme_0

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM98=LTDIE_0_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM98
	.byte 1,104,3
	.asciz "callback"

LDIFF_SYM99=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM99
	.byte 2,141,48,3
	.asciz "shouldReceiveTouch"

LDIFF_SYM100=LTDIE_16_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM100
	.byte 2,141,56,11
	.asciz "V_0"

LDIFF_SYM101=LTDIE_17_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM101
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM102=Lfde0_end - Lfde0_start
	.long LDIFF_SYM102
Lfde0_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool

LDIFF_SYM103=Lme_0 - FlyoutNavigation_OpenMenuGestureRecognizer__ctor_System_Action_1_UIKit_UIPanGestureRecognizer_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool
	.long LDIFF_SYM103
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,148,14,68,150,13,151,12,68,152,11
	.align 3
Lfde0_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.OpenMenuGestureRecognizer/<OpenMenuGestureRecognizer>c__AnonStorey0:.ctor"
	.asciz "FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor"

	.byte 0,0
	.quad FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor
	.quad Lme_1

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM104=LTDIE_17_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM104
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM105=Lfde1_end - Lfde1_start
	.long LDIFF_SYM105
Lfde1_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor

LDIFF_SYM106=Lme_1 - FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__ctor
	.long LDIFF_SYM106
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde1_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_18:

	.byte 5
	.asciz "UIKit_UITouch"

	.byte 40,16
LDIFF_SYM107=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM107
	.byte 2,35,0,0,7
	.asciz "UIKit_UITouch"

LDIFF_SYM108=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM108
LTDIE_18_POINTER:

	.byte 13
LDIFF_SYM109=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM109
LTDIE_18_REFERENCE:

	.byte 14
LDIFF_SYM110=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM110
	.byte 2
	.asciz "FlyoutNavigation.OpenMenuGestureRecognizer/<OpenMenuGestureRecognizer>c__AnonStorey0:<>m__0"
	.asciz "FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__m__0_UIKit_UIGestureRecognizer_UIKit_UITouch"

	.byte 0,0
	.quad FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__m__0_UIKit_UIGestureRecognizer_UIKit_UITouch
	.quad Lme_2

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM111=LTDIE_17_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM111
	.byte 3,141,192,0,3
	.asciz "sender"

LDIFF_SYM112=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM112
	.byte 3,141,200,0,3
	.asciz "touch"

LDIFF_SYM113=LTDIE_18_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM113
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM114=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM114
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM115=Lfde2_end - Lfde2_start
	.long LDIFF_SYM115
Lfde2_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__m__0_UIKit_UIGestureRecognizer_UIKit_UITouch

LDIFF_SYM116=Lme_2 - FlyoutNavigation_OpenMenuGestureRecognizer__OpenMenuGestureRecognizerc__AnonStorey0__m__0_UIKit_UIGestureRecognizer_UIKit_UITouch
	.long LDIFF_SYM116
	.long 0
	.byte 12,31,0,68,14,144,1,157,18,158,17,68,13,29,68,147,16,148,15,68,149,14,150,13,68,151,12,68,154,11
	.align 3
Lfde2_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_21:

	.byte 5
	.asciz "UIKit_UIResponder"

	.byte 40,16
LDIFF_SYM117=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM117
	.byte 2,35,0,0,7
	.asciz "UIKit_UIResponder"

LDIFF_SYM118=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM118
LTDIE_21_POINTER:

	.byte 13
LDIFF_SYM119=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM119
LTDIE_21_REFERENCE:

	.byte 14
LDIFF_SYM120=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM120
LTDIE_20:

	.byte 5
	.asciz "UIKit_UIViewController"

	.byte 56,16
LDIFF_SYM121=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM121
	.byte 2,35,0,6
	.asciz "__mt_PreferredFocusedView_var"

LDIFF_SYM122=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM122
	.byte 2,35,40,6
	.asciz "__mt_WeakTransitioningDelegate_var"

LDIFF_SYM123=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM123
	.byte 2,35,48,0,7
	.asciz "UIKit_UIViewController"

LDIFF_SYM124=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM124
LTDIE_20_POINTER:

	.byte 13
LDIFF_SYM125=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM125
LTDIE_20_REFERENCE:

	.byte 14
LDIFF_SYM126=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM126
LTDIE_24:

	.byte 5
	.asciz "UIKit_UIView"

	.byte 48,16
LDIFF_SYM127=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM127
	.byte 2,35,0,6
	.asciz "__mt_PreferredFocusedView_var"

LDIFF_SYM128=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM128
	.byte 2,35,40,0,7
	.asciz "UIKit_UIView"

LDIFF_SYM129=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM129
LTDIE_24_POINTER:

	.byte 13
LDIFF_SYM130=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM130
LTDIE_24_REFERENCE:

	.byte 14
LDIFF_SYM131=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM131
LTDIE_23:

	.byte 5
	.asciz "UIKit_UIControl"

	.byte 48,16
LDIFF_SYM132=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM132
	.byte 2,35,0,0,7
	.asciz "UIKit_UIControl"

LDIFF_SYM133=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM133
LTDIE_23_POINTER:

	.byte 13
LDIFF_SYM134=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM134
LTDIE_23_REFERENCE:

	.byte 14
LDIFF_SYM135=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM135
LTDIE_22:

	.byte 5
	.asciz "UIKit_UIButton"

	.byte 48,16
LDIFF_SYM136=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM136
	.byte 2,35,0,0,7
	.asciz "UIKit_UIButton"

LDIFF_SYM137=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM137
LTDIE_22_POINTER:

	.byte 13
LDIFF_SYM138=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM138
LTDIE_22_REFERENCE:

	.byte 14
LDIFF_SYM139=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM139
LTDIE_25:

	.byte 8
	.asciz "FlyoutNavigation_FlyOutNavigationPosition"

	.byte 4
LDIFF_SYM140=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM140
	.byte 9
	.asciz "Left"

	.byte 0,9
	.asciz "Right"

	.byte 1,0,7
	.asciz "FlyoutNavigation_FlyOutNavigationPosition"

LDIFF_SYM141=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM141
LTDIE_25_POINTER:

	.byte 13
LDIFF_SYM142=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM142
LTDIE_25_REFERENCE:

	.byte 14
LDIFF_SYM143=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM143
LTDIE_27:

	.byte 5
	.asciz "UIKit_UITableViewController"

	.byte 56,16
LDIFF_SYM144=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM144
	.byte 2,35,0,0,7
	.asciz "UIKit_UITableViewController"

LDIFF_SYM145=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM145
LTDIE_27_POINTER:

	.byte 13
LDIFF_SYM146=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM146
LTDIE_27_REFERENCE:

	.byte 14
LDIFF_SYM147=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM147
LTDIE_28:

	.byte 8
	.asciz "UIKit_UITableViewStyle"

	.byte 8
LDIFF_SYM148=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM148
	.byte 9
	.asciz "Plain"

	.byte 0,9
	.asciz "Grouped"

	.byte 1,0,7
	.asciz "UIKit_UITableViewStyle"

LDIFF_SYM149=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM149
LTDIE_28_POINTER:

	.byte 13
LDIFF_SYM150=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM150
LTDIE_28_REFERENCE:

	.byte 14
LDIFF_SYM151=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM151
LTDIE_29:

	.byte 5
	.asciz "System_Action`1"

	.byte 112,16
LDIFF_SYM152=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM152
	.byte 2,35,0,0,7
	.asciz "System_Action`1"

LDIFF_SYM153=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM153
LTDIE_29_POINTER:

	.byte 13
LDIFF_SYM154=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM154
LTDIE_29_REFERENCE:

	.byte 14
LDIFF_SYM155=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM155
LTDIE_30:

	.byte 5
	.asciz "UIKit_UISearchBar"

	.byte 56,16
LDIFF_SYM156=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM156
	.byte 2,35,0,6
	.asciz "__mt_WeakDelegate_var"

LDIFF_SYM157=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM157
	.byte 2,35,48,0,7
	.asciz "UIKit_UISearchBar"

LDIFF_SYM158=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM158
LTDIE_30_POINTER:

	.byte 13
LDIFF_SYM159=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM159
LTDIE_30_REFERENCE:

	.byte 14
LDIFF_SYM160=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM160
LTDIE_32:

	.byte 5
	.asciz "UIKit_UIScrollView"

	.byte 56,16
LDIFF_SYM161=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM161
	.byte 2,35,0,6
	.asciz "__mt_WeakDelegate_var"

LDIFF_SYM162=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM162
	.byte 2,35,48,0,7
	.asciz "UIKit_UIScrollView"

LDIFF_SYM163=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM163
LTDIE_32_POINTER:

	.byte 13
LDIFF_SYM164=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM164
LTDIE_32_REFERENCE:

	.byte 14
LDIFF_SYM165=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM165
LTDIE_31:

	.byte 5
	.asciz "UIKit_UITableView"

	.byte 72,16
LDIFF_SYM166=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM166
	.byte 2,35,0,6
	.asciz "__mt_WeakDataSource_var"

LDIFF_SYM167=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM167
	.byte 2,35,56,6
	.asciz "__mt_WeakDelegate_var"

LDIFF_SYM168=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM168
	.byte 2,35,64,0,7
	.asciz "UIKit_UITableView"

LDIFF_SYM169=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM169
LTDIE_31_POINTER:

	.byte 13
LDIFF_SYM170=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM170
LTDIE_31_REFERENCE:

	.byte 14
LDIFF_SYM171=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM171
LTDIE_34:

	.byte 5
	.asciz "UIKit_UIActivityIndicatorView"

	.byte 48,16
LDIFF_SYM172=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM172
	.byte 2,35,0,0,7
	.asciz "UIKit_UIActivityIndicatorView"

LDIFF_SYM173=LTDIE_34 - Ldebug_info_start
	.long LDIFF_SYM173
LTDIE_34_POINTER:

	.byte 13
LDIFF_SYM174=LTDIE_34 - Ldebug_info_start
	.long LDIFF_SYM174
LTDIE_34_REFERENCE:

	.byte 14
LDIFF_SYM175=LTDIE_34 - Ldebug_info_start
	.long LDIFF_SYM175
LTDIE_35:

	.byte 5
	.asciz "UIKit_UILabel"

	.byte 48,16
LDIFF_SYM176=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM176
	.byte 2,35,0,0,7
	.asciz "UIKit_UILabel"

LDIFF_SYM177=LTDIE_35 - Ldebug_info_start
	.long LDIFF_SYM177
LTDIE_35_POINTER:

	.byte 13
LDIFF_SYM178=LTDIE_35 - Ldebug_info_start
	.long LDIFF_SYM178
LTDIE_35_REFERENCE:

	.byte 14
LDIFF_SYM179=LTDIE_35 - Ldebug_info_start
	.long LDIFF_SYM179
LTDIE_36:

	.byte 5
	.asciz "UIKit_UIImageView"

	.byte 48,16
LDIFF_SYM180=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM180
	.byte 2,35,0,0,7
	.asciz "UIKit_UIImageView"

LDIFF_SYM181=LTDIE_36 - Ldebug_info_start
	.long LDIFF_SYM181
LTDIE_36_POINTER:

	.byte 13
LDIFF_SYM182=LTDIE_36 - Ldebug_info_start
	.long LDIFF_SYM182
LTDIE_36_REFERENCE:

	.byte 14
LDIFF_SYM183=LTDIE_36 - Ldebug_info_start
	.long LDIFF_SYM183
LTDIE_37:

	.byte 8
	.asciz "MonoTouch_Dialog_RefreshViewStatus"

	.byte 4
LDIFF_SYM184=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM184
	.byte 9
	.asciz "ReleaseToReload"

	.byte 0,9
	.asciz "PullToReload"

	.byte 1,9
	.asciz "Loading"

	.byte 2,0,7
	.asciz "MonoTouch_Dialog_RefreshViewStatus"

LDIFF_SYM185=LTDIE_37 - Ldebug_info_start
	.long LDIFF_SYM185
LTDIE_37_POINTER:

	.byte 13
LDIFF_SYM186=LTDIE_37 - Ldebug_info_start
	.long LDIFF_SYM186
LTDIE_37_REFERENCE:

	.byte 14
LDIFF_SYM187=LTDIE_37 - Ldebug_info_start
	.long LDIFF_SYM187
LTDIE_33:

	.byte 5
	.asciz "MonoTouch_Dialog_RefreshTableHeaderView"

	.byte 96,16
LDIFF_SYM188=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM188
	.byte 2,35,0,6
	.asciz "Activity"

LDIFF_SYM189=LTDIE_34_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM189
	.byte 2,35,48,6
	.asciz "LastUpdateLabel"

LDIFF_SYM190=LTDIE_35_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM190
	.byte 2,35,56,6
	.asciz "StatusLabel"

LDIFF_SYM191=LTDIE_35_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM191
	.byte 2,35,64,6
	.asciz "ArrowView"

LDIFF_SYM192=LTDIE_36_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM192
	.byte 2,35,72,6
	.asciz "status"

LDIFF_SYM193=LTDIE_37 - Ldebug_info_start
	.long LDIFF_SYM193
	.byte 2,35,80,6
	.asciz "IsFlipped"

LDIFF_SYM194=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM194
	.byte 2,35,84,6
	.asciz "lastUpdateTime"

LDIFF_SYM195=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM195
	.byte 2,35,88,0,7
	.asciz "MonoTouch_Dialog_RefreshTableHeaderView"

LDIFF_SYM196=LTDIE_33 - Ldebug_info_start
	.long LDIFF_SYM196
LTDIE_33_POINTER:

	.byte 13
LDIFF_SYM197=LTDIE_33 - Ldebug_info_start
	.long LDIFF_SYM197
LTDIE_33_REFERENCE:

	.byte 14
LDIFF_SYM198=LTDIE_33 - Ldebug_info_start
	.long LDIFF_SYM198
LTDIE_39:

	.byte 5
	.asciz "MonoTouch_Dialog_Element"

	.byte 32,16
LDIFF_SYM199=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM199
	.byte 2,35,0,6
	.asciz "Parent"

LDIFF_SYM200=LTDIE_39_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM200
	.byte 2,35,16,6
	.asciz "Caption"

LDIFF_SYM201=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM201
	.byte 2,35,24,0,7
	.asciz "MonoTouch_Dialog_Element"

LDIFF_SYM202=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM202
LTDIE_39_POINTER:

	.byte 13
LDIFF_SYM203=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM203
LTDIE_39_REFERENCE:

	.byte 14
LDIFF_SYM204=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM204
LTDIE_40:

	.byte 5
	.asciz "System_Int32"

	.byte 20,16
LDIFF_SYM205=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM205
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM206=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM206
	.byte 2,35,16,0,7
	.asciz "System_Int32"

LDIFF_SYM207=LTDIE_40 - Ldebug_info_start
	.long LDIFF_SYM207
LTDIE_40_POINTER:

	.byte 13
LDIFF_SYM208=LTDIE_40 - Ldebug_info_start
	.long LDIFF_SYM208
LTDIE_40_REFERENCE:

	.byte 14
LDIFF_SYM209=LTDIE_40 - Ldebug_info_start
	.long LDIFF_SYM209
LTDIE_41:

	.byte 5
	.asciz "MonoTouch_Dialog_Group"

	.byte 24,16
LDIFF_SYM210=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM210
	.byte 2,35,0,6
	.asciz "Key"

LDIFF_SYM211=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM211
	.byte 2,35,16,0,7
	.asciz "MonoTouch_Dialog_Group"

LDIFF_SYM212=LTDIE_41 - Ldebug_info_start
	.long LDIFF_SYM212
LTDIE_41_POINTER:

	.byte 13
LDIFF_SYM213=LTDIE_41 - Ldebug_info_start
	.long LDIFF_SYM213
LTDIE_41_REFERENCE:

	.byte 14
LDIFF_SYM214=LTDIE_41 - Ldebug_info_start
	.long LDIFF_SYM214
LTDIE_42:

	.byte 5
	.asciz "System_Func`2"

	.byte 112,16
LDIFF_SYM215=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM215
	.byte 2,35,0,0,7
	.asciz "System_Func`2"

LDIFF_SYM216=LTDIE_42 - Ldebug_info_start
	.long LDIFF_SYM216
LTDIE_42_POINTER:

	.byte 13
LDIFF_SYM217=LTDIE_42 - Ldebug_info_start
	.long LDIFF_SYM217
LTDIE_42_REFERENCE:

	.byte 14
LDIFF_SYM218=LTDIE_42 - Ldebug_info_start
	.long LDIFF_SYM218
LTDIE_43:

	.byte 5
	.asciz "System_Collections_Generic_List`1"

	.byte 32,16
LDIFF_SYM219=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM219
	.byte 2,35,0,6
	.asciz "_items"

LDIFF_SYM220=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM220
	.byte 2,35,16,6
	.asciz "_size"

LDIFF_SYM221=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM221
	.byte 2,35,24,6
	.asciz "_version"

LDIFF_SYM222=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM222
	.byte 2,35,28,0,7
	.asciz "System_Collections_Generic_List`1"

LDIFF_SYM223=LTDIE_43 - Ldebug_info_start
	.long LDIFF_SYM223
LTDIE_43_POINTER:

	.byte 13
LDIFF_SYM224=LTDIE_43 - Ldebug_info_start
	.long LDIFF_SYM224
LTDIE_43_REFERENCE:

	.byte 14
LDIFF_SYM225=LTDIE_43 - Ldebug_info_start
	.long LDIFF_SYM225
LTDIE_38:

	.byte 5
	.asciz "MonoTouch_Dialog_RootElement"

	.byte 80,16
LDIFF_SYM226=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM226
	.byte 2,35,0,6
	.asciz "summarySection"

LDIFF_SYM227=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM227
	.byte 2,35,64,6
	.asciz "summaryElement"

LDIFF_SYM228=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM228
	.byte 2,35,68,6
	.asciz "group"

LDIFF_SYM229=LTDIE_41_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM229
	.byte 2,35,32,6
	.asciz "UnevenRows"

LDIFF_SYM230=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM230
	.byte 2,35,72,6
	.asciz "createOnSelected"

LDIFF_SYM231=LTDIE_42_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM231
	.byte 2,35,40,6
	.asciz "TableView"

LDIFF_SYM232=LTDIE_31_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM232
	.byte 2,35,48,6
	.asciz "NeedColorUpdate"

LDIFF_SYM233=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM233
	.byte 2,35,73,6
	.asciz "Sections"

LDIFF_SYM234=LTDIE_43_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM234
	.byte 2,35,56,0,7
	.asciz "MonoTouch_Dialog_RootElement"

LDIFF_SYM235=LTDIE_38 - Ldebug_info_start
	.long LDIFF_SYM235
LTDIE_38_POINTER:

	.byte 13
LDIFF_SYM236=LTDIE_38 - Ldebug_info_start
	.long LDIFF_SYM236
LTDIE_38_REFERENCE:

	.byte 14
LDIFF_SYM237=LTDIE_38 - Ldebug_info_start
	.long LDIFF_SYM237
LTDIE_44:

	.byte 5
	.asciz "System_EventHandler"

	.byte 112,16
LDIFF_SYM238=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM238
	.byte 2,35,0,0,7
	.asciz "System_EventHandler"

LDIFF_SYM239=LTDIE_44 - Ldebug_info_start
	.long LDIFF_SYM239
LTDIE_44_POINTER:

	.byte 13
LDIFF_SYM240=LTDIE_44 - Ldebug_info_start
	.long LDIFF_SYM240
LTDIE_44_REFERENCE:

	.byte 14
LDIFF_SYM241=LTDIE_44 - Ldebug_info_start
	.long LDIFF_SYM241
LTDIE_45:

	.byte 5
	.asciz "_SearchTextEventHandler"

	.byte 112,16
LDIFF_SYM242=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM242
	.byte 2,35,0,0,7
	.asciz "_SearchTextEventHandler"

LDIFF_SYM243=LTDIE_45 - Ldebug_info_start
	.long LDIFF_SYM243
LTDIE_45_POINTER:

	.byte 13
LDIFF_SYM244=LTDIE_45 - Ldebug_info_start
	.long LDIFF_SYM244
LTDIE_45_REFERENCE:

	.byte 14
LDIFF_SYM245=LTDIE_45 - Ldebug_info_start
	.long LDIFF_SYM245
LTDIE_48:

	.byte 5
	.asciz "UIKit_UIScrollViewDelegate"

	.byte 40,16
LDIFF_SYM246=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM246
	.byte 2,35,0,0,7
	.asciz "UIKit_UIScrollViewDelegate"

LDIFF_SYM247=LTDIE_48 - Ldebug_info_start
	.long LDIFF_SYM247
LTDIE_48_POINTER:

	.byte 13
LDIFF_SYM248=LTDIE_48 - Ldebug_info_start
	.long LDIFF_SYM248
LTDIE_48_REFERENCE:

	.byte 14
LDIFF_SYM249=LTDIE_48 - Ldebug_info_start
	.long LDIFF_SYM249
LTDIE_47:

	.byte 5
	.asciz "UIKit_UITableViewSource"

	.byte 40,16
LDIFF_SYM250=LTDIE_48 - Ldebug_info_start
	.long LDIFF_SYM250
	.byte 2,35,0,0,7
	.asciz "UIKit_UITableViewSource"

LDIFF_SYM251=LTDIE_47 - Ldebug_info_start
	.long LDIFF_SYM251
LTDIE_47_POINTER:

	.byte 13
LDIFF_SYM252=LTDIE_47 - Ldebug_info_start
	.long LDIFF_SYM252
LTDIE_47_REFERENCE:

	.byte 14
LDIFF_SYM253=LTDIE_47 - Ldebug_info_start
	.long LDIFF_SYM253
LTDIE_46:

	.byte 5
	.asciz "_Source"

	.byte 64,16
LDIFF_SYM254=LTDIE_47 - Ldebug_info_start
	.long LDIFF_SYM254
	.byte 2,35,0,6
	.asciz "Container"

LDIFF_SYM255=LTDIE_26_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM255
	.byte 2,35,40,6
	.asciz "Root"

LDIFF_SYM256=LTDIE_38_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM256
	.byte 2,35,48,6
	.asciz "checkForRefresh"

LDIFF_SYM257=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM257
	.byte 2,35,56,0,7
	.asciz "_Source"

LDIFF_SYM258=LTDIE_46 - Ldebug_info_start
	.long LDIFF_SYM258
LTDIE_46_POINTER:

	.byte 13
LDIFF_SYM259=LTDIE_46 - Ldebug_info_start
	.long LDIFF_SYM259
LTDIE_46_REFERENCE:

	.byte 14
LDIFF_SYM260=LTDIE_46 - Ldebug_info_start
	.long LDIFF_SYM260
LTDIE_26:

	.byte 5
	.asciz "MonoTouch_Dialog_DialogViewController"

	.byte 176,1,16
LDIFF_SYM261=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM261
	.byte 2,35,0,6
	.asciz "Style"

LDIFF_SYM262=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM262
	.byte 3,35,160,1,6
	.asciz "OnSelection"

LDIFF_SYM263=LTDIE_29_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM263
	.byte 2,35,56,6
	.asciz "searchBar"

LDIFF_SYM264=LTDIE_30_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM264
	.byte 2,35,64,6
	.asciz "tableView"

LDIFF_SYM265=LTDIE_31_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM265
	.byte 2,35,72,6
	.asciz "refreshView"

LDIFF_SYM266=LTDIE_33_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM266
	.byte 2,35,80,6
	.asciz "root"

LDIFF_SYM267=LTDIE_38_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM267
	.byte 2,35,88,6
	.asciz "pushing"

LDIFF_SYM268=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM268
	.byte 3,35,168,1,6
	.asciz "dirty"

LDIFF_SYM269=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM269
	.byte 3,35,169,1,6
	.asciz "reloading"

LDIFF_SYM270=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM270
	.byte 3,35,170,1,6
	.asciz "refreshRequested"

LDIFF_SYM271=LTDIE_44_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM271
	.byte 2,35,96,6
	.asciz "enableSearch"

LDIFF_SYM272=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM272
	.byte 3,35,171,1,6
	.asciz "<AutoHideSearch>k__BackingField"

LDIFF_SYM273=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM273
	.byte 3,35,172,1,6
	.asciz "<SearchPlaceholder>k__BackingField"

LDIFF_SYM274=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM274
	.byte 2,35,104,6
	.asciz "<Autorotate>k__BackingField"

LDIFF_SYM275=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM275
	.byte 3,35,173,1,6
	.asciz "originalSections"

LDIFF_SYM276=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM276
	.byte 2,35,112,6
	.asciz "originalElements"

LDIFF_SYM277=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM277
	.byte 2,35,120,6
	.asciz "SearchTextChanged"

LDIFF_SYM278=LTDIE_45_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM278
	.byte 3,35,128,1,6
	.asciz "ViewAppearing"

LDIFF_SYM279=LTDIE_44_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM279
	.byte 3,35,136,1,6
	.asciz "TableSource"

LDIFF_SYM280=LTDIE_46_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM280
	.byte 3,35,144,1,6
	.asciz "ViewDisappearing"

LDIFF_SYM281=LTDIE_44_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM281
	.byte 3,35,152,1,0,7
	.asciz "MonoTouch_Dialog_DialogViewController"

LDIFF_SYM282=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM282
LTDIE_26_POINTER:

	.byte 13
LDIFF_SYM283=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM283
LTDIE_26_REFERENCE:

	.byte 14
LDIFF_SYM284=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM284
LTDIE_49:

	.byte 5
	.asciz "UIKit_UIColor"

	.byte 40,16
LDIFF_SYM285=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM285
	.byte 2,35,0,0,7
	.asciz "UIKit_UIColor"

LDIFF_SYM286=LTDIE_49 - Ldebug_info_start
	.long LDIFF_SYM286
LTDIE_49_POINTER:

	.byte 13
LDIFF_SYM287=LTDIE_49 - Ldebug_info_start
	.long LDIFF_SYM287
LTDIE_49_REFERENCE:

	.byte 14
LDIFF_SYM288=LTDIE_49 - Ldebug_info_start
	.long LDIFF_SYM288
LTDIE_50:

	.byte 5
	.asciz "System_Action"

	.byte 112,16
LDIFF_SYM289=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM289
	.byte 2,35,0,0,7
	.asciz "System_Action"

LDIFF_SYM290=LTDIE_50 - Ldebug_info_start
	.long LDIFF_SYM290
LTDIE_50_POINTER:

	.byte 13
LDIFF_SYM291=LTDIE_50 - Ldebug_info_start
	.long LDIFF_SYM291
LTDIE_50_REFERENCE:

	.byte 14
LDIFF_SYM292=LTDIE_50 - Ldebug_info_start
	.long LDIFF_SYM292
LTDIE_51:

	.byte 5
	.asciz "UIKit_UIScreenEdgePanGestureRecognizer"

	.byte 56,16
LDIFF_SYM293=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM293
	.byte 2,35,0,0,7
	.asciz "UIKit_UIScreenEdgePanGestureRecognizer"

LDIFF_SYM294=LTDIE_51 - Ldebug_info_start
	.long LDIFF_SYM294
LTDIE_51_POINTER:

	.byte 13
LDIFF_SYM295=LTDIE_51 - Ldebug_info_start
	.long LDIFF_SYM295
LTDIE_51_REFERENCE:

	.byte 14
LDIFF_SYM296=LTDIE_51 - Ldebug_info_start
	.long LDIFF_SYM296
LTDIE_52:

	.byte 5
	.asciz "UIKit_UITouchEventArgs"

	.byte 112,16
LDIFF_SYM297=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM297
	.byte 2,35,0,0,7
	.asciz "UIKit_UITouchEventArgs"

LDIFF_SYM298=LTDIE_52 - Ldebug_info_start
	.long LDIFF_SYM298
LTDIE_52_POINTER:

	.byte 13
LDIFF_SYM299=LTDIE_52 - Ldebug_info_start
	.long LDIFF_SYM299
LTDIE_52_REFERENCE:

	.byte 14
LDIFF_SYM300=LTDIE_52 - Ldebug_info_start
	.long LDIFF_SYM300
LTDIE_19:

	.byte 5
	.asciz "FlyoutNavigation_FlyoutNavigationController"

	.byte 200,1,16
LDIFF_SYM301=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM301
	.byte 2,35,0,6
	.asciz "closeButton"

LDIFF_SYM302=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM302
	.byte 2,35,56,6
	.asciz "firstLaunch"

LDIFF_SYM303=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM303
	.byte 3,35,160,1,6
	.asciz "position"

LDIFF_SYM304=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM304
	.byte 3,35,164,1,6
	.asciz "navigation"

LDIFF_SYM305=LTDIE_26_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM305
	.byte 2,35,64,6
	.asciz "menuBorder"

LDIFF_SYM306=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM306
	.byte 2,35,72,6
	.asciz "menuBorderColor"

LDIFF_SYM307=LTDIE_49_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM307
	.byte 2,35,80,6
	.asciz "showMenuBorder"

LDIFF_SYM308=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM308
	.byte 3,35,168,1,6
	.asciz "selectedIndex"

LDIFF_SYM309=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM309
	.byte 3,35,172,1,6
	.asciz "shadowView"

LDIFF_SYM310=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM310
	.byte 2,35,88,6
	.asciz "startX"

LDIFF_SYM311=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM311
	.byte 3,35,176,1,6
	.asciz "tintColor"

LDIFF_SYM312=LTDIE_49_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM312
	.byte 2,35,96,6
	.asciz "statusImage"

LDIFF_SYM313=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM313
	.byte 2,35,104,6
	.asciz "viewControllers"

LDIFF_SYM314=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM314
	.byte 2,35,112,6
	.asciz "hideShadow"

LDIFF_SYM315=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM315
	.byte 3,35,184,1,6
	.asciz "<SelectedIndexChanged>k__BackingField"

LDIFF_SYM316=LTDIE_50_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM316
	.byte 2,35,120,6
	.asciz "<AlwaysShowLandscapeMenu>k__BackingField"

LDIFF_SYM317=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM317
	.byte 3,35,185,1,6
	.asciz "<ForceMenuOpen>k__BackingField"

LDIFF_SYM318=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM318
	.byte 3,35,186,1,6
	.asciz "AlreadyLayedOut"

LDIFF_SYM319=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM319
	.byte 3,35,187,1,6
	.asciz "<NavigationOpenedByLandscapeRotation>k__BackingField"

LDIFF_SYM320=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM320
	.byte 3,35,188,1,6
	.asciz "<CurrentViewController>k__BackingField"

LDIFF_SYM321=LTDIE_20_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM321
	.byte 3,35,128,1,6
	.asciz "<DisableRotation>k__BackingField"

LDIFF_SYM322=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM322
	.byte 3,35,189,1,6
	.asciz "isIos7"

LDIFF_SYM323=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM323
	.byte 3,35,190,1,6
	.asciz "isIos8"

LDIFF_SYM324=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM324
	.byte 3,35,191,1,6
	.asciz "closeGesture"

LDIFF_SYM325=LTDIE_0_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM325
	.byte 3,35,136,1,6
	.asciz "openGesture"

LDIFF_SYM326=LTDIE_51_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM326
	.byte 3,35,144,1,6
	.asciz "ShouldReceiveTouch"

LDIFF_SYM327=LTDIE_52_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM327
	.byte 3,35,152,1,6
	.asciz "<DisableGesture>k__BackingField"

LDIFF_SYM328=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM328
	.byte 3,35,192,1,6
	.asciz "disableStatusBarMoving"

LDIFF_SYM329=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM329
	.byte 3,35,193,1,0,7
	.asciz "FlyoutNavigation_FlyoutNavigationController"

LDIFF_SYM330=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM330
LTDIE_19_POINTER:

	.byte 13
LDIFF_SYM331=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM331
LTDIE_19_REFERENCE:

	.byte 14
LDIFF_SYM332=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM332
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:.ctor"
	.asciz "FlyoutNavigation_FlyoutNavigationController__ctor_intptr"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__ctor_intptr
	.quad Lme_3

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM333=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM333
	.byte 1,105,3
	.asciz "handle"

LDIFF_SYM334=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM334
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM335=Lfde3_end - Lfde3_start
	.long LDIFF_SYM335
Lfde3_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__ctor_intptr

LDIFF_SYM336=Lme_3 - FlyoutNavigation_FlyoutNavigationController__ctor_intptr
	.long LDIFF_SYM336
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,153,8
	.align 3
Lfde3_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:.ctor"
	.asciz "FlyoutNavigation_FlyoutNavigationController__ctor_UIKit_UITableViewStyle"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__ctor_UIKit_UITableViewStyle
	.quad Lme_4

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM337=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM337
	.byte 1,105,3
	.asciz "navigationStyle"

LDIFF_SYM338=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM338
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM339=Lfde4_end - Lfde4_start
	.long LDIFF_SYM339
Lfde4_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__ctor_UIKit_UITableViewStyle

LDIFF_SYM340=Lme_4 - FlyoutNavigation_FlyoutNavigationController__ctor_UIKit_UITableViewStyle
	.long LDIFF_SYM340
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,153,8
	.align 3
Lfde4_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_NavigationViewController"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_NavigationViewController"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_NavigationViewController
	.quad Lme_5

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM341=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM341
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM342=Lfde5_end - Lfde5_start
	.long LDIFF_SYM342
Lfde5_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_NavigationViewController

LDIFF_SYM343=Lme_5 - FlyoutNavigation_FlyoutNavigationController_get_NavigationViewController
	.long LDIFF_SYM343
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde5_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_MenuBorderColor"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_MenuBorderColor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_MenuBorderColor
	.quad Lme_6

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM344=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM344
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM345=Lfde6_end - Lfde6_start
	.long LDIFF_SYM345
Lfde6_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_MenuBorderColor

LDIFF_SYM346=Lme_6 - FlyoutNavigation_FlyoutNavigationController_get_MenuBorderColor
	.long LDIFF_SYM346
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde6_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_MenuBorderColor"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_MenuBorderColor_UIKit_UIColor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_MenuBorderColor_UIKit_UIColor
	.quad Lme_7

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM347=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM347
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM348=LTDIE_49_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM348
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM349=Lfde7_end - Lfde7_start
	.long LDIFF_SYM349
Lfde7_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_MenuBorderColor_UIKit_UIColor

LDIFF_SYM350=Lme_7 - FlyoutNavigation_FlyoutNavigationController_set_MenuBorderColor_UIKit_UIColor
	.long LDIFF_SYM350
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,153,6
	.align 3
Lfde7_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_ShowMenuBorder"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder
	.quad Lme_8

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM351=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM351
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM352=Lfde8_end - Lfde8_start
	.long LDIFF_SYM352
Lfde8_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder

LDIFF_SYM353=Lme_8 - FlyoutNavigation_FlyoutNavigationController_get_ShowMenuBorder
	.long LDIFF_SYM353
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde8_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_ShowMenuBorder"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_ShowMenuBorder_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_ShowMenuBorder_bool
	.quad Lme_9

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM354=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM354
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM355=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM355
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM356=Lfde9_end - Lfde9_start
	.long LDIFF_SYM356
Lfde9_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_ShowMenuBorder_bool

LDIFF_SYM357=Lme_9 - FlyoutNavigation_FlyoutNavigationController_set_ShowMenuBorder_bool
	.long LDIFF_SYM357
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde9_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_TintColor"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_TintColor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_TintColor
	.quad Lme_a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM358=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM358
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM359=Lfde10_end - Lfde10_start
	.long LDIFF_SYM359
Lfde10_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_TintColor

LDIFF_SYM360=Lme_a - FlyoutNavigation_FlyoutNavigationController_get_TintColor
	.long LDIFF_SYM360
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde10_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_TintColor"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor
	.quad Lme_b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM361=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM361
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM362=LTDIE_49_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM362
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM363=Lfde11_end - Lfde11_start
	.long LDIFF_SYM363
Lfde11_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor

LDIFF_SYM364=Lme_b - FlyoutNavigation_FlyoutNavigationController_set_TintColor_UIKit_UIColor
	.long LDIFF_SYM364
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde11_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_Position"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_Position"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_Position
	.quad Lme_c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM365=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM365
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM366=Lfde12_end - Lfde12_start
	.long LDIFF_SYM366
Lfde12_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_Position

LDIFF_SYM367=Lme_c - FlyoutNavigation_FlyoutNavigationController_get_Position
	.long LDIFF_SYM367
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde12_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_Position"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_Position_FlyoutNavigation_FlyOutNavigationPosition"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_Position_FlyoutNavigation_FlyOutNavigationPosition
	.quad Lme_d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM368=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM368
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM369=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM369
	.byte 2,141,48,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM370=Lfde13_end - Lfde13_start
	.long LDIFF_SYM370
Lfde13_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_Position_FlyoutNavigation_FlyOutNavigationPosition

LDIFF_SYM371=Lme_d - FlyoutNavigation_FlyoutNavigationController_set_Position_FlyoutNavigation_FlyOutNavigationPosition
	.long LDIFF_SYM371
	.long 0
	.byte 12,31,0,68,14,144,1,157,18,158,17,68,13,29,68,150,16,151,15,68,152,14,153,13
	.align 3
Lfde13_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_SelectedIndexChanged"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged
	.quad Lme_e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM372=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM372
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM373=Lfde14_end - Lfde14_start
	.long LDIFF_SYM373
Lfde14_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged

LDIFF_SYM374=Lme_e - FlyoutNavigation_FlyoutNavigationController_get_SelectedIndexChanged
	.long LDIFF_SYM374
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde14_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_SelectedIndexChanged"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action
	.quad Lme_f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM375=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM375
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM376=LTDIE_50_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM376
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM377=Lfde15_end - Lfde15_start
	.long LDIFF_SYM377
Lfde15_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action

LDIFF_SYM378=Lme_f - FlyoutNavigation_FlyoutNavigationController_set_SelectedIndexChanged_System_Action
	.long LDIFF_SYM378
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde15_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_AlwaysShowLandscapeMenu"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu
	.quad Lme_10

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM379=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM379
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM380=Lfde16_end - Lfde16_start
	.long LDIFF_SYM380
Lfde16_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu

LDIFF_SYM381=Lme_10 - FlyoutNavigation_FlyoutNavigationController_get_AlwaysShowLandscapeMenu
	.long LDIFF_SYM381
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde16_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_AlwaysShowLandscapeMenu"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool
	.quad Lme_11

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM382=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM382
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM383=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM383
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM384=Lfde17_end - Lfde17_start
	.long LDIFF_SYM384
Lfde17_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool

LDIFF_SYM385=Lme_11 - FlyoutNavigation_FlyoutNavigationController_set_AlwaysShowLandscapeMenu_bool
	.long LDIFF_SYM385
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde17_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_ForceMenuOpen"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen
	.quad Lme_12

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM386=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM386
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM387=Lfde18_end - Lfde18_start
	.long LDIFF_SYM387
Lfde18_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen

LDIFF_SYM388=Lme_12 - FlyoutNavigation_FlyoutNavigationController_get_ForceMenuOpen
	.long LDIFF_SYM388
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde18_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_ForceMenuOpen"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_ForceMenuOpen_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_ForceMenuOpen_bool
	.quad Lme_13

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM389=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM389
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM390=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM390
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM391=Lfde19_end - Lfde19_start
	.long LDIFF_SYM391
Lfde19_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_ForceMenuOpen_bool

LDIFF_SYM392=Lme_13 - FlyoutNavigation_FlyoutNavigationController_set_ForceMenuOpen_bool
	.long LDIFF_SYM392
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde19_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_NavigationOpenedByLandscapeRotation"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation
	.quad Lme_14

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM393=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM393
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM394=Lfde20_end - Lfde20_start
	.long LDIFF_SYM394
Lfde20_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation

LDIFF_SYM395=Lme_14 - FlyoutNavigation_FlyoutNavigationController_get_NavigationOpenedByLandscapeRotation
	.long LDIFF_SYM395
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde20_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_NavigationOpenedByLandscapeRotation"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool
	.quad Lme_15

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM396=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM396
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM397=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM397
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM398=Lfde21_end - Lfde21_start
	.long LDIFF_SYM398
Lfde21_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool

LDIFF_SYM399=Lme_15 - FlyoutNavigation_FlyoutNavigationController_set_NavigationOpenedByLandscapeRotation_bool
	.long LDIFF_SYM399
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde21_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_HideShadow"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_HideShadow"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_HideShadow
	.quad Lme_16

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM400=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM400
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM401=Lfde22_end - Lfde22_start
	.long LDIFF_SYM401
Lfde22_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_HideShadow

LDIFF_SYM402=Lme_16 - FlyoutNavigation_FlyoutNavigationController_get_HideShadow
	.long LDIFF_SYM402
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde22_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_HideShadow"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_HideShadow_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_HideShadow_bool
	.quad Lme_17

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM403=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM403
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM404=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM404
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM405=Lfde23_end - Lfde23_start
	.long LDIFF_SYM405
Lfde23_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_HideShadow_bool

LDIFF_SYM406=Lme_17 - FlyoutNavigation_FlyoutNavigationController_set_HideShadow_bool
	.long LDIFF_SYM406
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29,68,153,10,154,9
	.align 3
Lfde23_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_ShadowViewColor"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_ShadowViewColor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_ShadowViewColor
	.quad Lme_18

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM407=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM407
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM408=Lfde24_end - Lfde24_start
	.long LDIFF_SYM408
Lfde24_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_ShadowViewColor

LDIFF_SYM409=Lme_18 - FlyoutNavigation_FlyoutNavigationController_get_ShadowViewColor
	.long LDIFF_SYM409
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29
	.align 3
Lfde24_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_ShadowViewColor"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_ShadowViewColor_UIKit_UIColor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_ShadowViewColor_UIKit_UIColor
	.quad Lme_19

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM410=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM410
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM411=LTDIE_49_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM411
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM412=Lfde25_end - Lfde25_start
	.long LDIFF_SYM412
Lfde25_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_ShadowViewColor_UIKit_UIColor

LDIFF_SYM413=Lme_19 - FlyoutNavigation_FlyoutNavigationController_set_ShadowViewColor_UIKit_UIColor
	.long LDIFF_SYM413
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29
	.align 3
Lfde25_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_CurrentViewController"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController
	.quad Lme_1a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM414=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM414
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM415=Lfde26_end - Lfde26_start
	.long LDIFF_SYM415
Lfde26_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController

LDIFF_SYM416=Lme_1a - FlyoutNavigation_FlyoutNavigationController_get_CurrentViewController
	.long LDIFF_SYM416
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde26_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_CurrentViewController"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController
	.quad Lme_1b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM417=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM417
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM418=LTDIE_20_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM418
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM419=Lfde27_end - Lfde27_start
	.long LDIFF_SYM419
Lfde27_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController

LDIFF_SYM420=Lme_1b - FlyoutNavigation_FlyoutNavigationController_set_CurrentViewController_UIKit_UIViewController
	.long LDIFF_SYM420
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde27_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_mainView"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_mainView"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_mainView
	.quad Lme_1c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM421=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM421
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM422=Lfde28_end - Lfde28_start
	.long LDIFF_SYM422
Lfde28_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_mainView

LDIFF_SYM423=Lme_1c - FlyoutNavigation_FlyoutNavigationController_get_mainView
	.long LDIFF_SYM423
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde28_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_NavigationRoot"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_NavigationRoot"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_NavigationRoot
	.quad Lme_1d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM424=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM424
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM425=Lfde29_end - Lfde29_start
	.long LDIFF_SYM425
Lfde29_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_NavigationRoot

LDIFF_SYM426=Lme_1d - FlyoutNavigation_FlyoutNavigationController_get_NavigationRoot
	.long LDIFF_SYM426
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde29_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_53:

	.byte 5
	.asciz "_<>c__AnonStorey0"

	.byte 32,16
LDIFF_SYM427=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM427
	.byte 2,35,0,6
	.asciz "value"

LDIFF_SYM428=LTDIE_38_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM428
	.byte 2,35,16,6
	.asciz "$this"

LDIFF_SYM429=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM429
	.byte 2,35,24,0,7
	.asciz "_<>c__AnonStorey0"

LDIFF_SYM430=LTDIE_53 - Ldebug_info_start
	.long LDIFF_SYM430
LTDIE_53_POINTER:

	.byte 13
LDIFF_SYM431=LTDIE_53 - Ldebug_info_start
	.long LDIFF_SYM431
LTDIE_53_REFERENCE:

	.byte 14
LDIFF_SYM432=LTDIE_53 - Ldebug_info_start
	.long LDIFF_SYM432
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_NavigationRoot"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_NavigationRoot_MonoTouch_Dialog_RootElement"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_NavigationRoot_MonoTouch_Dialog_RootElement
	.quad Lme_1e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM433=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM433
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM434=LTDIE_38_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM434
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM435=LTDIE_53_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM435
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM436=Lfde30_end - Lfde30_start
	.long LDIFF_SYM436
Lfde30_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_NavigationRoot_MonoTouch_Dialog_RootElement

LDIFF_SYM437=Lme_1e - FlyoutNavigation_FlyoutNavigationController_set_NavigationRoot_MonoTouch_Dialog_RootElement
	.long LDIFF_SYM437
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,153,7
	.align 3
Lfde30_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_NavigationTableView"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView
	.quad Lme_1f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM438=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM438
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM439=Lfde31_end - Lfde31_start
	.long LDIFF_SYM439
Lfde31_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView

LDIFF_SYM440=Lme_1f - FlyoutNavigation_FlyoutNavigationController_get_NavigationTableView
	.long LDIFF_SYM440
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde31_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_ViewControllers"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_ViewControllers"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_ViewControllers
	.quad Lme_20

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM441=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM441
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM442=Lfde32_end - Lfde32_start
	.long LDIFF_SYM442
Lfde32_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_ViewControllers

LDIFF_SYM443=Lme_20 - FlyoutNavigation_FlyoutNavigationController_get_ViewControllers
	.long LDIFF_SYM443
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde32_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_54:

	.byte 5
	.asciz "_<>c__AnonStorey1"

	.byte 32,16
LDIFF_SYM444=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM444
	.byte 2,35,0,6
	.asciz "value"

LDIFF_SYM445=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM445
	.byte 2,35,16,6
	.asciz "$this"

LDIFF_SYM446=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM446
	.byte 2,35,24,0,7
	.asciz "_<>c__AnonStorey1"

LDIFF_SYM447=LTDIE_54 - Ldebug_info_start
	.long LDIFF_SYM447
LTDIE_54_POINTER:

	.byte 13
LDIFF_SYM448=LTDIE_54 - Ldebug_info_start
	.long LDIFF_SYM448
LTDIE_54_REFERENCE:

	.byte 14
LDIFF_SYM449=LTDIE_54 - Ldebug_info_start
	.long LDIFF_SYM449
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_ViewControllers"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_ViewControllers_UIKit_UIViewController__"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_ViewControllers_UIKit_UIViewController__
	.quad Lme_21

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM450=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM450
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM451=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM451
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM452=LTDIE_54_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM452
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM453=Lfde33_end - Lfde33_start
	.long LDIFF_SYM453
Lfde33_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_ViewControllers_UIKit_UIViewController__

LDIFF_SYM454=Lme_21 - FlyoutNavigation_FlyoutNavigationController_set_ViewControllers_UIKit_UIViewController__
	.long LDIFF_SYM454
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,153,7
	.align 3
Lfde33_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_IsOpen"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_IsOpen"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_IsOpen
	.quad Lme_22

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM455=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM455
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM456=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM456
	.byte 3,141,144,1,11
	.asciz "V_1"

LDIFF_SYM457=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM457
	.byte 3,141,240,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM458=Lfde34_end - Lfde34_start
	.long LDIFF_SYM458
Lfde34_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_IsOpen

LDIFF_SYM459=Lme_22 - FlyoutNavigation_FlyoutNavigationController_get_IsOpen
	.long LDIFF_SYM459
	.long 0
	.byte 12,31,0,68,14,224,1,157,28,158,27,68,13,29,68,154,26
	.align 3
Lfde34_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_IsOpen"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_IsOpen_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_IsOpen_bool
	.quad Lme_23

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM460=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM460
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM461=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM461
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM462=Lfde35_end - Lfde35_start
	.long LDIFF_SYM462
Lfde35_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_IsOpen_bool

LDIFF_SYM463=Lme_23 - FlyoutNavigation_FlyoutNavigationController_set_IsOpen_bool
	.long LDIFF_SYM463
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,153,6
	.align 3
Lfde35_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_ShouldStayOpen"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen
	.quad Lme_24

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM464=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM464
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM465=Lfde36_end - Lfde36_start
	.long LDIFF_SYM465
Lfde36_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen

LDIFF_SYM466=Lme_24 - FlyoutNavigation_FlyoutNavigationController_get_ShouldStayOpen
	.long LDIFF_SYM466
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde36_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_SelectedIndex"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex
	.quad Lme_25

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM467=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM467
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM468=Lfde37_end - Lfde37_start
	.long LDIFF_SYM468
Lfde37_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex

LDIFF_SYM469=Lme_25 - FlyoutNavigation_FlyoutNavigationController_get_SelectedIndex
	.long LDIFF_SYM469
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde37_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_55:

	.byte 5
	.asciz "_<>c__AnonStorey2"

	.byte 32,16
LDIFF_SYM470=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM470
	.byte 2,35,0,6
	.asciz "value"

LDIFF_SYM471=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM471
	.byte 2,35,24,6
	.asciz "$this"

LDIFF_SYM472=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM472
	.byte 2,35,16,0,7
	.asciz "_<>c__AnonStorey2"

LDIFF_SYM473=LTDIE_55 - Ldebug_info_start
	.long LDIFF_SYM473
LTDIE_55_POINTER:

	.byte 13
LDIFF_SYM474=LTDIE_55 - Ldebug_info_start
	.long LDIFF_SYM474
LTDIE_55_REFERENCE:

	.byte 14
LDIFF_SYM475=LTDIE_55 - Ldebug_info_start
	.long LDIFF_SYM475
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_SelectedIndex"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_SelectedIndex_int"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_SelectedIndex_int
	.quad Lme_26

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM476=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM476
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM477=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM477
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM478=LTDIE_55_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM478
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM479=Lfde38_end - Lfde38_start
	.long LDIFF_SYM479
Lfde38_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_SelectedIndex_int

LDIFF_SYM480=Lme_26 - FlyoutNavigation_FlyoutNavigationController_set_SelectedIndex_int
	.long LDIFF_SYM480
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,153,7
	.align 3
Lfde38_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_DisableRotation"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_DisableRotation"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_DisableRotation
	.quad Lme_27

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM481=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM481
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM482=Lfde39_end - Lfde39_start
	.long LDIFF_SYM482
Lfde39_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_DisableRotation

LDIFF_SYM483=Lme_27 - FlyoutNavigation_FlyoutNavigationController_get_DisableRotation
	.long LDIFF_SYM483
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde39_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_DisableRotation"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_DisableRotation_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_DisableRotation_bool
	.quad Lme_28

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM484=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM484
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM485=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM485
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM486=Lfde40_end - Lfde40_start
	.long LDIFF_SYM486
Lfde40_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_DisableRotation_bool

LDIFF_SYM487=Lme_28 - FlyoutNavigation_FlyoutNavigationController_set_DisableRotation_bool
	.long LDIFF_SYM487
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde40_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_ShouldAutomaticallyForwardRotationMethods"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_ShouldAutomaticallyForwardRotationMethods"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_ShouldAutomaticallyForwardRotationMethods
	.quad Lme_29

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM488=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM488
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM489=Lfde41_end - Lfde41_start
	.long LDIFF_SYM489
Lfde41_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_ShouldAutomaticallyForwardRotationMethods

LDIFF_SYM490=Lme_29 - FlyoutNavigation_FlyoutNavigationController_get_ShouldAutomaticallyForwardRotationMethods
	.long LDIFF_SYM490
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde41_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_56:

	.byte 5
	.asciz "_UAUIView"

	.byte 56,16
LDIFF_SYM491=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM491
	.byte 2,35,0,6
	.asciz "<AccessibilityId>k__BackingField"

LDIFF_SYM492=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM492
	.byte 2,35,48,0,7
	.asciz "_UAUIView"

LDIFF_SYM493=LTDIE_56 - Ldebug_info_start
	.long LDIFF_SYM493
LTDIE_56_POINTER:

	.byte 13
LDIFF_SYM494=LTDIE_56 - Ldebug_info_start
	.long LDIFF_SYM494
LTDIE_56_REFERENCE:

	.byte 14
LDIFF_SYM495=LTDIE_56 - Ldebug_info_start
	.long LDIFF_SYM495
LTDIE_57:

	.byte 5
	.asciz "System_Version"

	.byte 32,16
LDIFF_SYM496=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM496
	.byte 2,35,0,6
	.asciz "_Major"

LDIFF_SYM497=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM497
	.byte 2,35,16,6
	.asciz "_Minor"

LDIFF_SYM498=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM498
	.byte 2,35,20,6
	.asciz "_Build"

LDIFF_SYM499=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM499
	.byte 2,35,24,6
	.asciz "_Revision"

LDIFF_SYM500=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM500
	.byte 2,35,28,0,7
	.asciz "System_Version"

LDIFF_SYM501=LTDIE_57 - Ldebug_info_start
	.long LDIFF_SYM501
LTDIE_57_POINTER:

	.byte 13
LDIFF_SYM502=LTDIE_57 - Ldebug_info_start
	.long LDIFF_SYM502
LTDIE_57_REFERENCE:

	.byte 14
LDIFF_SYM503=LTDIE_57 - Ldebug_info_start
	.long LDIFF_SYM503
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:Initialize"
	.asciz "FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle
	.quad Lme_2a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM504=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM504
	.byte 1,105,3
	.asciz "navigationStyle"

LDIFF_SYM505=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM505
	.byte 3,141,200,0,11
	.asciz "V_0"

LDIFF_SYM506=LTDIE_56_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM506
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM507=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM507
	.byte 3,141,136,3,11
	.asciz "V_2"

LDIFF_SYM508=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM508
	.byte 3,141,232,2,11
	.asciz "V_3"

LDIFF_SYM509=LTDIE_57_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM509
	.byte 1,103,11
	.asciz "V_4"

LDIFF_SYM510=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM510
	.byte 1,102,11
	.asciz "V_5"

LDIFF_SYM511=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM511
	.byte 1,101,11
	.asciz "V_6"

LDIFF_SYM512=LTDIE_51_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM512
	.byte 1,100,11
	.asciz "V_7"

LDIFF_SYM513=LTDIE_0_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM513
	.byte 1,99,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM514=Lfde42_end - Lfde42_start
	.long LDIFF_SYM514
Lfde42_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle

LDIFF_SYM515=Lme_2a - FlyoutNavigation_FlyoutNavigationController_Initialize_UIKit_UITableViewStyle
	.long LDIFF_SYM515
	.long 0
	.byte 12,31,0,84,14,192,5,157,88,158,87,68,13,29,68,147,86,148,85,68,149,84,150,83,68,151,82,152,81,68,153,80
	.align 3
Lfde42_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_58:

	.byte 5
	.asciz "System_EventArgs"

	.byte 16,16
LDIFF_SYM516=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM516
	.byte 2,35,0,0,7
	.asciz "System_EventArgs"

LDIFF_SYM517=LTDIE_58 - Ldebug_info_start
	.long LDIFF_SYM517
LTDIE_58_POINTER:

	.byte 13
LDIFF_SYM518=LTDIE_58 - Ldebug_info_start
	.long LDIFF_SYM518
LTDIE_58_REFERENCE:

	.byte 14
LDIFF_SYM519=LTDIE_58 - Ldebug_info_start
	.long LDIFF_SYM519
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:CloseButtonTapped"
	.asciz "FlyoutNavigation_FlyoutNavigationController_CloseButtonTapped_object_System_EventArgs"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_CloseButtonTapped_object_System_EventArgs
	.quad Lme_2b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM520=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM520
	.byte 2,141,16,3
	.asciz "sender"

LDIFF_SYM521=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM521
	.byte 2,141,24,3
	.asciz "e"

LDIFF_SYM522=LTDIE_58_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM522
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM523=Lfde43_end - Lfde43_start
	.long LDIFF_SYM523
Lfde43_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_CloseButtonTapped_object_System_EventArgs

LDIFF_SYM524=Lme_2b - FlyoutNavigation_FlyoutNavigationController_CloseButtonTapped_object_System_EventArgs
	.long LDIFF_SYM524
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde43_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:add_ShouldReceiveTouch"
	.asciz "FlyoutNavigation_FlyoutNavigationController_add_ShouldReceiveTouch_UIKit_UITouchEventArgs"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_add_ShouldReceiveTouch_UIKit_UITouchEventArgs
	.quad Lme_2c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM525=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM525
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM526=LTDIE_52_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM526
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM527=LTDIE_52_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM527
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM528=LTDIE_52_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM528
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM529=Lfde44_end - Lfde44_start
	.long LDIFF_SYM529
Lfde44_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_add_ShouldReceiveTouch_UIKit_UITouchEventArgs

LDIFF_SYM530=Lme_2c - FlyoutNavigation_FlyoutNavigationController_add_ShouldReceiveTouch_UIKit_UITouchEventArgs
	.long LDIFF_SYM530
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29,68,149,12,150,11,68,151,10,152,9,68,153,8,154,7
	.align 3
Lfde44_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:remove_ShouldReceiveTouch"
	.asciz "FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs
	.quad Lme_2d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM531=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM531
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM532=LTDIE_52_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM532
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM533=LTDIE_52_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM533
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM534=LTDIE_52_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM534
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM535=Lfde45_end - Lfde45_start
	.long LDIFF_SYM535
Lfde45_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs

LDIFF_SYM536=Lme_2d - FlyoutNavigation_FlyoutNavigationController_remove_ShouldReceiveTouch_UIKit_UITouchEventArgs
	.long LDIFF_SYM536
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29,68,149,12,150,11,68,151,10,152,9,68,153,8,154,7
	.align 3
Lfde45_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_DisableGesture"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_DisableGesture"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_DisableGesture
	.quad Lme_2e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM537=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM537
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM538=Lfde46_end - Lfde46_start
	.long LDIFF_SYM538
Lfde46_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_DisableGesture

LDIFF_SYM539=Lme_2e - FlyoutNavigation_FlyoutNavigationController_get_DisableGesture
	.long LDIFF_SYM539
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde46_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_DisableGesture"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_DisableGesture_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_DisableGesture_bool
	.quad Lme_2f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM540=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM540
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM541=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM541
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM542=Lfde47_end - Lfde47_start
	.long LDIFF_SYM542
Lfde47_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_DisableGesture_bool

LDIFF_SYM543=Lme_2f - FlyoutNavigation_FlyoutNavigationController_set_DisableGesture_bool
	.long LDIFF_SYM543
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde47_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:shouldReceiveTouch"
	.asciz "FlyoutNavigation_FlyoutNavigationController_shouldReceiveTouch_UIKit_UIGestureRecognizer_UIKit_UITouch"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_shouldReceiveTouch_UIKit_UIGestureRecognizer_UIKit_UITouch
	.quad Lme_30

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM544=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM544
	.byte 1,104,3
	.asciz "gesture"

LDIFF_SYM545=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM545
	.byte 1,105,3
	.asciz "touch"

LDIFF_SYM546=LTDIE_18_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM546
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM547=Lfde48_end - Lfde48_start
	.long LDIFF_SYM547
Lfde48_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_shouldReceiveTouch_UIKit_UIGestureRecognizer_UIKit_UITouch

LDIFF_SYM548=Lme_30 - FlyoutNavigation_FlyoutNavigationController_shouldReceiveTouch_UIKit_UIGestureRecognizer_UIKit_UITouch
	.long LDIFF_SYM548
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,153,7
	.align 3
Lfde48_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:ViewDidLayoutSubviews"
	.asciz "FlyoutNavigation_FlyoutNavigationController_ViewDidLayoutSubviews"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_ViewDidLayoutSubviews
	.quad Lme_31

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM549=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM549
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM550=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM550
	.byte 3,141,144,2,11
	.asciz "V_1"

LDIFF_SYM551=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM551
	.byte 3,141,240,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM552=Lfde49_end - Lfde49_start
	.long LDIFF_SYM552
Lfde49_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_ViewDidLayoutSubviews

LDIFF_SYM553=Lme_31 - FlyoutNavigation_FlyoutNavigationController_ViewDidLayoutSubviews
	.long LDIFF_SYM553
	.long 0
	.byte 12,31,0,68,14,240,2,157,46,158,45,68,13,29,68,154,44
	.align 3
Lfde49_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:DragContentView"
	.asciz "FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer
	.quad Lme_32

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM554=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM554
	.byte 1,105,3
	.asciz "gesture"

LDIFF_SYM555=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM555
	.byte 3,141,192,0,11
	.asciz "V_0"

LDIFF_SYM556=LTDIE_1_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM556
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM557=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM557
	.byte 3,141,128,2,11
	.asciz "V_2"

LDIFF_SYM558=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM558
	.byte 3,141,160,2,11
	.asciz "V_3"

LDIFF_SYM559=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM559
	.byte 3,141,240,1,11
	.asciz "V_4"

LDIFF_SYM560=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM560
	.byte 3,141,168,2,11
	.asciz "V_5"

LDIFF_SYM561=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM561
	.byte 3,141,224,1,11
	.asciz "V_6"

LDIFF_SYM562=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM562
	.byte 3,141,176,2,11
	.asciz "V_7"

LDIFF_SYM563=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM563
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM564=Lfde50_end - Lfde50_start
	.long LDIFF_SYM564
Lfde50_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer

LDIFF_SYM565=Lme_32 - FlyoutNavigation_FlyoutNavigationController_DragContentView_UIKit_UIGestureRecognizer
	.long LDIFF_SYM565
	.long 0
	.byte 12,31,0,68,14,144,3,157,50,158,49,68,13,29,68,148,48,149,47,68,150,46,151,45,68,152,44,153,43
	.align 3
Lfde50_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:ViewWillAppear"
	.asciz "FlyoutNavigation_FlyoutNavigationController_ViewWillAppear_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_ViewWillAppear_bool
	.quad Lme_33

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM566=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM566
	.byte 1,105,3
	.asciz "animated"

LDIFF_SYM567=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM567
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM568=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM568
	.byte 3,141,168,2,11
	.asciz "V_1"

LDIFF_SYM569=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM569
	.byte 3,141,136,2,11
	.asciz "V_2"

LDIFF_SYM570=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM570
	.byte 3,141,232,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM571=Lfde51_end - Lfde51_start
	.long LDIFF_SYM571
Lfde51_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_ViewWillAppear_bool

LDIFF_SYM572=Lme_33 - FlyoutNavigation_FlyoutNavigationController_ViewWillAppear_bool
	.long LDIFF_SYM572
	.long 0
	.byte 12,31,0,68,14,160,3,157,52,158,51,68,13,29,68,153,50
	.align 3
Lfde51_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:ViewDidAppear"
	.asciz "FlyoutNavigation_FlyoutNavigationController_ViewDidAppear_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_ViewDidAppear_bool
	.quad Lme_34

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM573=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM573
	.byte 1,105,3
	.asciz "animated"

LDIFF_SYM574=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM574
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM575=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM575
	.byte 3,141,248,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM576=Lfde52_end - Lfde52_start
	.long LDIFF_SYM576
Lfde52_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_ViewDidAppear_bool

LDIFF_SYM577=Lme_34 - FlyoutNavigation_FlyoutNavigationController_ViewDidAppear_bool
	.long LDIFF_SYM577
	.long 0
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,153,20
	.align 3
Lfde52_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:ViewWillDisappear"
	.asciz "FlyoutNavigation_FlyoutNavigationController_ViewWillDisappear_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_ViewWillDisappear_bool
	.quad Lme_35

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM578=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM578
	.byte 1,105,3
	.asciz "animated"

LDIFF_SYM579=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM579
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM580=Lfde53_end - Lfde53_start
	.long LDIFF_SYM580
Lfde53_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_ViewWillDisappear_bool

LDIFF_SYM581=Lme_35 - FlyoutNavigation_FlyoutNavigationController_ViewWillDisappear_bool
	.long LDIFF_SYM581
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,153,8
	.align 3
Lfde53_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_59:

	.byte 5
	.asciz "Foundation_NSIndexPath"

	.byte 40,16
LDIFF_SYM582=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM582
	.byte 2,35,0,0,7
	.asciz "Foundation_NSIndexPath"

LDIFF_SYM583=LTDIE_59 - Ldebug_info_start
	.long LDIFF_SYM583
LTDIE_59_POINTER:

	.byte 13
LDIFF_SYM584=LTDIE_59 - Ldebug_info_start
	.long LDIFF_SYM584
LTDIE_59_REFERENCE:

	.byte 14
LDIFF_SYM585=LTDIE_59 - Ldebug_info_start
	.long LDIFF_SYM585
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:NavigationItemSelected"
	.asciz "FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath
	.quad Lme_36

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM586=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM586
	.byte 1,105,3
	.asciz "indexPath"

LDIFF_SYM587=LTDIE_59_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM587
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM588=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM588
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM589=Lfde54_end - Lfde54_start
	.long LDIFF_SYM589
Lfde54_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath

LDIFF_SYM590=Lme_36 - FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_Foundation_NSIndexPath
	.long LDIFF_SYM590
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,153,7
	.align 3
Lfde54_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:NavigationItemSelected"
	.asciz "FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int
	.quad Lme_37

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM591=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM591
	.byte 1,105,3
	.asciz "index"

LDIFF_SYM592=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM592
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM593=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM593
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM594=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM594
	.byte 3,141,152,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM595=Lfde55_end - Lfde55_start
	.long LDIFF_SYM595
Lfde55_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int

LDIFF_SYM596=Lme_37 - FlyoutNavigation_FlyoutNavigationController_NavigationItemSelected_int
	.long LDIFF_SYM596
	.long 0
	.byte 12,31,0,68,14,240,1,157,30,158,29,68,13,29,68,149,28,150,27,68,151,26,152,25,68,153,24,154,23
	.align 3
Lfde55_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:ShowMenu"
	.asciz "FlyoutNavigation_FlyoutNavigationController_ShowMenu"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_ShowMenu
	.quad Lme_38

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM597=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM597
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM598=Lfde56_end - Lfde56_start
	.long LDIFF_SYM598
Lfde56_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_ShowMenu

LDIFF_SYM599=Lme_38 - FlyoutNavigation_FlyoutNavigationController_ShowMenu
	.long LDIFF_SYM599
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde56_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:setViewSize"
	.asciz "FlyoutNavigation_FlyoutNavigationController_setViewSize"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_setViewSize
	.quad Lme_39

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM600=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM600
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM601=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM601
	.byte 3,141,168,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM602=Lfde57_end - Lfde57_start
	.long LDIFF_SYM602
Lfde57_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_setViewSize

LDIFF_SYM603=Lme_39 - FlyoutNavigation_FlyoutNavigationController_setViewSize
	.long LDIFF_SYM603
	.long 0
	.byte 12,31,0,68,14,144,2,157,34,158,33,68,13,29,68,151,32,152,31,68,153,30,154,29
	.align 3
Lfde57_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:GetViewBounds"
	.asciz "FlyoutNavigation_FlyoutNavigationController_GetViewBounds"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_GetViewBounds
	.quad Lme_3a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM604=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM604
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM605=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM605
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM606=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM606
	.byte 3,141,208,1,11
	.asciz "V_2"

LDIFF_SYM607=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM607
	.byte 3,141,216,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM608=Lfde58_end - Lfde58_start
	.long LDIFF_SYM608
Lfde58_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_GetViewBounds

LDIFF_SYM609=Lme_3a - FlyoutNavigation_FlyoutNavigationController_GetViewBounds
	.long LDIFF_SYM609
	.long 0
	.byte 12,31,0,68,14,176,2,157,38,158,37,68,13,29,68,154,36
	.align 3
Lfde58_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:SetLocation"
	.asciz "FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect
	.quad Lme_3b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM610=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM610
	.byte 1,106,3
	.asciz "frame"

LDIFF_SYM611=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM611
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM612=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM612
	.byte 3,141,144,4,11
	.asciz "V_1"

LDIFF_SYM613=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM613
	.byte 3,141,240,3,11
	.asciz "V_2"

LDIFF_SYM614=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM614
	.byte 3,141,224,3,11
	.asciz "V_3"

LDIFF_SYM615=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM615
	.byte 3,141,192,3,11
	.asciz "V_4"

LDIFF_SYM616=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM616
	.byte 3,141,160,3,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM617=Lfde59_end - Lfde59_start
	.long LDIFF_SYM617
Lfde59_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect

LDIFF_SYM618=Lme_3b - FlyoutNavigation_FlyoutNavigationController_SetLocation_CoreGraphics_CGRect
	.long LDIFF_SYM618
	.long 0
	.byte 12,31,0,84,14,128,6,157,96,158,95,68,13,29,68,154,94
	.align 3
Lfde59_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:get_DisableStatusBarMoving"
	.asciz "FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving
	.quad Lme_3c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM619=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM619
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM620=Lfde60_end - Lfde60_start
	.long LDIFF_SYM620
Lfde60_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving

LDIFF_SYM621=Lme_3c - FlyoutNavigation_FlyoutNavigationController_get_DisableStatusBarMoving
	.long LDIFF_SYM621
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde60_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:set_DisableStatusBarMoving"
	.asciz "FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool
	.quad Lme_3d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM622=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM622
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM623=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM623
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM624=Lfde61_end - Lfde61_start
	.long LDIFF_SYM624
Lfde61_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool

LDIFF_SYM625=Lme_3d - FlyoutNavigation_FlyoutNavigationController_set_DisableStatusBarMoving_bool
	.long LDIFF_SYM625
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde61_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:DisplayMenuBorder"
	.asciz "FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect
	.quad Lme_3e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM626=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM626
	.byte 1,106,3
	.asciz "frame"

LDIFF_SYM627=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM627
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM628=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM628
	.byte 3,141,208,1,11
	.asciz "V_1"

LDIFF_SYM629=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM629
	.byte 3,141,176,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM630=Lfde62_end - Lfde62_start
	.long LDIFF_SYM630
Lfde62_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect

LDIFF_SYM631=Lme_3e - FlyoutNavigation_FlyoutNavigationController_DisplayMenuBorder_CoreGraphics_CGRect
	.long LDIFF_SYM631
	.long 0
	.byte 12,31,0,68,14,240,2,157,46,158,45,68,13,29,68,154,44
	.align 3
Lfde62_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:getStatus"
	.asciz "FlyoutNavigation_FlyoutNavigationController_getStatus"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_getStatus
	.quad Lme_3f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM632=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM632
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM633=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM633
	.byte 1,105,11
	.asciz "V_1"

LDIFF_SYM634=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM634
	.byte 1,104,11
	.asciz "V_2"

LDIFF_SYM635=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM635
	.byte 1,103,11
	.asciz "V_3"

LDIFF_SYM636=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM636
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM637=Lfde63_end - Lfde63_start
	.long LDIFF_SYM637
Lfde63_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_getStatus

LDIFF_SYM638=Lme_3f - FlyoutNavigation_FlyoutNavigationController_getStatus
	.long LDIFF_SYM638
	.long 0
	.byte 12,31,0,68,14,160,1,157,20,158,19,68,13,29,68,150,18,151,17,68,152,16,153,15,68,154,14
	.align 3
Lfde63_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_61:

	.byte 17
	.asciz "System_Collections_IDictionary"

	.byte 16,7
	.asciz "System_Collections_IDictionary"

LDIFF_SYM639=LTDIE_61 - Ldebug_info_start
	.long LDIFF_SYM639
LTDIE_61_POINTER:

	.byte 13
LDIFF_SYM640=LTDIE_61 - Ldebug_info_start
	.long LDIFF_SYM640
LTDIE_61_REFERENCE:

	.byte 14
LDIFF_SYM641=LTDIE_61 - Ldebug_info_start
	.long LDIFF_SYM641
LTDIE_63:

	.byte 17
	.asciz "System_Collections_Generic_IList`1"

	.byte 16,7
	.asciz "System_Collections_Generic_IList`1"

LDIFF_SYM642=LTDIE_63 - Ldebug_info_start
	.long LDIFF_SYM642
LTDIE_63_POINTER:

	.byte 13
LDIFF_SYM643=LTDIE_63 - Ldebug_info_start
	.long LDIFF_SYM643
LTDIE_63_REFERENCE:

	.byte 14
LDIFF_SYM644=LTDIE_63 - Ldebug_info_start
	.long LDIFF_SYM644
LTDIE_66:

	.byte 17
	.asciz "System_Collections_Generic_IEqualityComparer`1"

	.byte 16,7
	.asciz "System_Collections_Generic_IEqualityComparer`1"

LDIFF_SYM645=LTDIE_66 - Ldebug_info_start
	.long LDIFF_SYM645
LTDIE_66_POINTER:

	.byte 13
LDIFF_SYM646=LTDIE_66 - Ldebug_info_start
	.long LDIFF_SYM646
LTDIE_66_REFERENCE:

	.byte 14
LDIFF_SYM647=LTDIE_66 - Ldebug_info_start
	.long LDIFF_SYM647
LTDIE_67:

	.byte 5
	.asciz "_KeyCollection"

	.byte 24,16
LDIFF_SYM648=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM648
	.byte 2,35,0,6
	.asciz "dictionary"

LDIFF_SYM649=LTDIE_65_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM649
	.byte 2,35,16,0,7
	.asciz "_KeyCollection"

LDIFF_SYM650=LTDIE_67 - Ldebug_info_start
	.long LDIFF_SYM650
LTDIE_67_POINTER:

	.byte 13
LDIFF_SYM651=LTDIE_67 - Ldebug_info_start
	.long LDIFF_SYM651
LTDIE_67_REFERENCE:

	.byte 14
LDIFF_SYM652=LTDIE_67 - Ldebug_info_start
	.long LDIFF_SYM652
LTDIE_68:

	.byte 5
	.asciz "_ValueCollection"

	.byte 24,16
LDIFF_SYM653=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM653
	.byte 2,35,0,6
	.asciz "dictionary"

LDIFF_SYM654=LTDIE_65_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM654
	.byte 2,35,16,0,7
	.asciz "_ValueCollection"

LDIFF_SYM655=LTDIE_68 - Ldebug_info_start
	.long LDIFF_SYM655
LTDIE_68_POINTER:

	.byte 13
LDIFF_SYM656=LTDIE_68 - Ldebug_info_start
	.long LDIFF_SYM656
LTDIE_68_REFERENCE:

	.byte 14
LDIFF_SYM657=LTDIE_68 - Ldebug_info_start
	.long LDIFF_SYM657
LTDIE_65:

	.byte 5
	.asciz "System_Collections_Generic_Dictionary`2"

	.byte 72,16
LDIFF_SYM658=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM658
	.byte 2,35,0,6
	.asciz "buckets"

LDIFF_SYM659=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM659
	.byte 2,35,16,6
	.asciz "entries"

LDIFF_SYM660=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM660
	.byte 2,35,24,6
	.asciz "count"

LDIFF_SYM661=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM661
	.byte 2,35,56,6
	.asciz "version"

LDIFF_SYM662=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM662
	.byte 2,35,60,6
	.asciz "freeList"

LDIFF_SYM663=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM663
	.byte 2,35,64,6
	.asciz "freeCount"

LDIFF_SYM664=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM664
	.byte 2,35,68,6
	.asciz "comparer"

LDIFF_SYM665=LTDIE_66_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM665
	.byte 2,35,32,6
	.asciz "keys"

LDIFF_SYM666=LTDIE_67_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM666
	.byte 2,35,40,6
	.asciz "values"

LDIFF_SYM667=LTDIE_68_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM667
	.byte 2,35,48,0,7
	.asciz "System_Collections_Generic_Dictionary`2"

LDIFF_SYM668=LTDIE_65 - Ldebug_info_start
	.long LDIFF_SYM668
LTDIE_65_POINTER:

	.byte 13
LDIFF_SYM669=LTDIE_65 - Ldebug_info_start
	.long LDIFF_SYM669
LTDIE_65_REFERENCE:

	.byte 14
LDIFF_SYM670=LTDIE_65 - Ldebug_info_start
	.long LDIFF_SYM670
LTDIE_69:

	.byte 17
	.asciz "System_Runtime_Serialization_IFormatterConverter"

	.byte 16,7
	.asciz "System_Runtime_Serialization_IFormatterConverter"

LDIFF_SYM671=LTDIE_69 - Ldebug_info_start
	.long LDIFF_SYM671
LTDIE_69_POINTER:

	.byte 13
LDIFF_SYM672=LTDIE_69 - Ldebug_info_start
	.long LDIFF_SYM672
LTDIE_69_REFERENCE:

	.byte 14
LDIFF_SYM673=LTDIE_69 - Ldebug_info_start
	.long LDIFF_SYM673
LTDIE_64:

	.byte 5
	.asciz "System_Runtime_Serialization_SerializationInfo"

	.byte 88,16
LDIFF_SYM674=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM674
	.byte 2,35,0,6
	.asciz "m_members"

LDIFF_SYM675=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM675
	.byte 2,35,16,6
	.asciz "m_data"

LDIFF_SYM676=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM676
	.byte 2,35,24,6
	.asciz "m_types"

LDIFF_SYM677=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM677
	.byte 2,35,32,6
	.asciz "m_nameToIndex"

LDIFF_SYM678=LTDIE_65_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM678
	.byte 2,35,40,6
	.asciz "m_currMember"

LDIFF_SYM679=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM679
	.byte 2,35,80,6
	.asciz "m_converter"

LDIFF_SYM680=LTDIE_69_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM680
	.byte 2,35,48,6
	.asciz "m_fullTypeName"

LDIFF_SYM681=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM681
	.byte 2,35,56,6
	.asciz "m_assemName"

LDIFF_SYM682=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM682
	.byte 2,35,64,6
	.asciz "objectType"

LDIFF_SYM683=LTDIE_13_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM683
	.byte 2,35,72,6
	.asciz "isFullTypeNameSetExplicit"

LDIFF_SYM684=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM684
	.byte 2,35,84,6
	.asciz "isAssemblyNameSetExplicit"

LDIFF_SYM685=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM685
	.byte 2,35,85,6
	.asciz "requireSameTokenInPartialTrust"

LDIFF_SYM686=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM686
	.byte 2,35,86,0,7
	.asciz "System_Runtime_Serialization_SerializationInfo"

LDIFF_SYM687=LTDIE_64 - Ldebug_info_start
	.long LDIFF_SYM687
LTDIE_64_POINTER:

	.byte 13
LDIFF_SYM688=LTDIE_64 - Ldebug_info_start
	.long LDIFF_SYM688
LTDIE_64_REFERENCE:

	.byte 14
LDIFF_SYM689=LTDIE_64 - Ldebug_info_start
	.long LDIFF_SYM689
LTDIE_71:

	.byte 5
	.asciz "System_Reflection_TypeInfo"

	.byte 24,16
LDIFF_SYM690=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM690
	.byte 2,35,0,0,7
	.asciz "System_Reflection_TypeInfo"

LDIFF_SYM691=LTDIE_71 - Ldebug_info_start
	.long LDIFF_SYM691
LTDIE_71_POINTER:

	.byte 13
LDIFF_SYM692=LTDIE_71 - Ldebug_info_start
	.long LDIFF_SYM692
LTDIE_71_REFERENCE:

	.byte 14
LDIFF_SYM693=LTDIE_71 - Ldebug_info_start
	.long LDIFF_SYM693
LTDIE_75:

	.byte 5
	.asciz "System_Reflection_ConstructorInfo"

	.byte 16,16
LDIFF_SYM694=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM694
	.byte 2,35,0,0,7
	.asciz "System_Reflection_ConstructorInfo"

LDIFF_SYM695=LTDIE_75 - Ldebug_info_start
	.long LDIFF_SYM695
LTDIE_75_POINTER:

	.byte 13
LDIFF_SYM696=LTDIE_75 - Ldebug_info_start
	.long LDIFF_SYM696
LTDIE_75_REFERENCE:

	.byte 14
LDIFF_SYM697=LTDIE_75 - Ldebug_info_start
	.long LDIFF_SYM697
LTDIE_74:

	.byte 5
	.asciz "System_Reflection_RuntimeConstructorInfo"

	.byte 16,16
LDIFF_SYM698=LTDIE_75 - Ldebug_info_start
	.long LDIFF_SYM698
	.byte 2,35,0,0,7
	.asciz "System_Reflection_RuntimeConstructorInfo"

LDIFF_SYM699=LTDIE_74 - Ldebug_info_start
	.long LDIFF_SYM699
LTDIE_74_POINTER:

	.byte 13
LDIFF_SYM700=LTDIE_74 - Ldebug_info_start
	.long LDIFF_SYM700
LTDIE_74_REFERENCE:

	.byte 14
LDIFF_SYM701=LTDIE_74 - Ldebug_info_start
	.long LDIFF_SYM701
LTDIE_73:

	.byte 5
	.asciz "System_Reflection_MonoCMethod"

	.byte 40,16
LDIFF_SYM702=LTDIE_74 - Ldebug_info_start
	.long LDIFF_SYM702
	.byte 2,35,0,6
	.asciz "mhandle"

LDIFF_SYM703=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM703
	.byte 2,35,16,6
	.asciz "name"

LDIFF_SYM704=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM704
	.byte 2,35,24,6
	.asciz "reftype"

LDIFF_SYM705=LTDIE_13_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM705
	.byte 2,35,32,0,7
	.asciz "System_Reflection_MonoCMethod"

LDIFF_SYM706=LTDIE_73 - Ldebug_info_start
	.long LDIFF_SYM706
LTDIE_73_POINTER:

	.byte 13
LDIFF_SYM707=LTDIE_73 - Ldebug_info_start
	.long LDIFF_SYM707
LTDIE_73_REFERENCE:

	.byte 14
LDIFF_SYM708=LTDIE_73 - Ldebug_info_start
	.long LDIFF_SYM708
LTDIE_72:

	.byte 5
	.asciz "System_MonoTypeInfo"

	.byte 32,16
LDIFF_SYM709=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM709
	.byte 2,35,0,6
	.asciz "full_name"

LDIFF_SYM710=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM710
	.byte 2,35,16,6
	.asciz "default_ctor"

LDIFF_SYM711=LTDIE_73_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM711
	.byte 2,35,24,0,7
	.asciz "System_MonoTypeInfo"

LDIFF_SYM712=LTDIE_72 - Ldebug_info_start
	.long LDIFF_SYM712
LTDIE_72_POINTER:

	.byte 13
LDIFF_SYM713=LTDIE_72 - Ldebug_info_start
	.long LDIFF_SYM713
LTDIE_72_REFERENCE:

	.byte 14
LDIFF_SYM714=LTDIE_72 - Ldebug_info_start
	.long LDIFF_SYM714
LTDIE_70:

	.byte 5
	.asciz "System_RuntimeType"

	.byte 48,16
LDIFF_SYM715=LTDIE_71 - Ldebug_info_start
	.long LDIFF_SYM715
	.byte 2,35,0,6
	.asciz "type_info"

LDIFF_SYM716=LTDIE_72_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM716
	.byte 2,35,24,6
	.asciz "GenericCache"

LDIFF_SYM717=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM717
	.byte 2,35,32,6
	.asciz "m_serializationCtor"

LDIFF_SYM718=LTDIE_74_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM718
	.byte 2,35,40,0,7
	.asciz "System_RuntimeType"

LDIFF_SYM719=LTDIE_70 - Ldebug_info_start
	.long LDIFF_SYM719
LTDIE_70_POINTER:

	.byte 13
LDIFF_SYM720=LTDIE_70 - Ldebug_info_start
	.long LDIFF_SYM720
LTDIE_70_REFERENCE:

	.byte 14
LDIFF_SYM721=LTDIE_70 - Ldebug_info_start
	.long LDIFF_SYM721
LTDIE_76:

	.byte 5
	.asciz "System_EventHandler`1"

	.byte 112,16
LDIFF_SYM722=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM722
	.byte 2,35,0,0,7
	.asciz "System_EventHandler`1"

LDIFF_SYM723=LTDIE_76 - Ldebug_info_start
	.long LDIFF_SYM723
LTDIE_76_POINTER:

	.byte 13
LDIFF_SYM724=LTDIE_76 - Ldebug_info_start
	.long LDIFF_SYM724
LTDIE_76_REFERENCE:

	.byte 14
LDIFF_SYM725=LTDIE_76 - Ldebug_info_start
	.long LDIFF_SYM725
LTDIE_62:

	.byte 5
	.asciz "System_Runtime_Serialization_SafeSerializationManager"

	.byte 56,16
LDIFF_SYM726=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM726
	.byte 2,35,0,6
	.asciz "m_serializedStates"

LDIFF_SYM727=LTDIE_63_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM727
	.byte 2,35,16,6
	.asciz "m_savedSerializationInfo"

LDIFF_SYM728=LTDIE_64_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM728
	.byte 2,35,24,6
	.asciz "m_realObject"

LDIFF_SYM729=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM729
	.byte 2,35,32,6
	.asciz "m_realType"

LDIFF_SYM730=LTDIE_70_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM730
	.byte 2,35,40,6
	.asciz "SerializeObjectState"

LDIFF_SYM731=LTDIE_76_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM731
	.byte 2,35,48,0,7
	.asciz "System_Runtime_Serialization_SafeSerializationManager"

LDIFF_SYM732=LTDIE_62 - Ldebug_info_start
	.long LDIFF_SYM732
LTDIE_62_POINTER:

	.byte 13
LDIFF_SYM733=LTDIE_62 - Ldebug_info_start
	.long LDIFF_SYM733
LTDIE_62_REFERENCE:

	.byte 14
LDIFF_SYM734=LTDIE_62 - Ldebug_info_start
	.long LDIFF_SYM734
LTDIE_60:

	.byte 5
	.asciz "System_Exception"

	.byte 136,1,16
LDIFF_SYM735=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM735
	.byte 2,35,0,6
	.asciz "_className"

LDIFF_SYM736=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM736
	.byte 2,35,16,6
	.asciz "_message"

LDIFF_SYM737=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM737
	.byte 2,35,24,6
	.asciz "_data"

LDIFF_SYM738=LTDIE_61_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM738
	.byte 2,35,32,6
	.asciz "_innerException"

LDIFF_SYM739=LTDIE_60_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM739
	.byte 2,35,40,6
	.asciz "_helpURL"

LDIFF_SYM740=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM740
	.byte 2,35,48,6
	.asciz "_stackTrace"

LDIFF_SYM741=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM741
	.byte 2,35,56,6
	.asciz "_stackTraceString"

LDIFF_SYM742=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM742
	.byte 2,35,64,6
	.asciz "_remoteStackTraceString"

LDIFF_SYM743=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM743
	.byte 2,35,72,6
	.asciz "_remoteStackIndex"

LDIFF_SYM744=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM744
	.byte 2,35,80,6
	.asciz "_dynamicMethods"

LDIFF_SYM745=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM745
	.byte 2,35,88,6
	.asciz "_HResult"

LDIFF_SYM746=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM746
	.byte 2,35,96,6
	.asciz "_source"

LDIFF_SYM747=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM747
	.byte 2,35,104,6
	.asciz "_safeSerializationManager"

LDIFF_SYM748=LTDIE_62_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM748
	.byte 2,35,112,6
	.asciz "captured_traces"

LDIFF_SYM749=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM749
	.byte 2,35,120,6
	.asciz "native_trace_ips"

LDIFF_SYM750=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM750
	.byte 3,35,128,1,0,7
	.asciz "System_Exception"

LDIFF_SYM751=LTDIE_60 - Ldebug_info_start
	.long LDIFF_SYM751
LTDIE_60_POINTER:

	.byte 13
LDIFF_SYM752=LTDIE_60 - Ldebug_info_start
	.long LDIFF_SYM752
LTDIE_60_REFERENCE:

	.byte 14
LDIFF_SYM753=LTDIE_60 - Ldebug_info_start
	.long LDIFF_SYM753
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:captureStatusBarImage"
	.asciz "FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage
	.quad Lme_40

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM754=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM754
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM755=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM755
	.byte 1,106,11
	.asciz "V_1"

LDIFF_SYM756=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM756
	.byte 2,141,56,11
	.asciz "V_2"

LDIFF_SYM757=LTDIE_60_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM757
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM758=Lfde64_end - Lfde64_start
	.long LDIFF_SYM758
Lfde64_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage

LDIFF_SYM759=Lme_40 - FlyoutNavigation_FlyoutNavigationController_captureStatusBarImage
	.long LDIFF_SYM759
	.long 0
	.byte 12,31,0,68,14,144,1,157,18,158,17,68,13,29,68,154,16
	.align 3
Lfde64_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:hideStatus"
	.asciz "FlyoutNavigation_FlyoutNavigationController_hideStatus"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_hideStatus
	.quad Lme_41

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM760=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM760
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM761=Lfde65_end - Lfde65_start
	.long LDIFF_SYM761
Lfde65_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_hideStatus

LDIFF_SYM762=Lme_41 - FlyoutNavigation_FlyoutNavigationController_hideStatus
	.long LDIFF_SYM762
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde65_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:HideMenu"
	.asciz "FlyoutNavigation_FlyoutNavigationController_HideMenu"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_HideMenu
	.quad Lme_42

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM763=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM763
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM764=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM764
	.byte 3,141,208,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM765=Lfde66_end - Lfde66_start
	.long LDIFF_SYM765
Lfde66_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_HideMenu

LDIFF_SYM766=Lme_42 - FlyoutNavigation_FlyoutNavigationController_HideMenu
	.long LDIFF_SYM766
	.long 0
	.byte 12,31,0,68,14,160,1,157,20,158,19,68,13,29,68,154,18
	.align 3
Lfde66_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:hideComplete"
	.asciz "FlyoutNavigation_FlyoutNavigationController_hideComplete"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_hideComplete
	.quad Lme_43

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM767=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM767
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM768=Lfde67_end - Lfde67_start
	.long LDIFF_SYM768
Lfde67_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_hideComplete

LDIFF_SYM769=Lme_43 - FlyoutNavigation_FlyoutNavigationController_hideComplete
	.long LDIFF_SYM769
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde67_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:ResignFirstResponders"
	.asciz "FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView
	.quad Lme_44

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM770=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM770
	.byte 1,105,3
	.asciz "view"

LDIFF_SYM771=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM771
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM772=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM772
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM773=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM773
	.byte 1,103,11
	.asciz "V_2"

LDIFF_SYM774=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM774
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM775=Lfde68_end - Lfde68_start
	.long LDIFF_SYM775
Lfde68_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView

LDIFF_SYM776=Lme_44 - FlyoutNavigation_FlyoutNavigationController_ResignFirstResponders_UIKit_UIView
	.long LDIFF_SYM776
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29,68,150,10,151,9,68,152,8,153,7,68,154,6
	.align 3
Lfde68_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:ToggleMenu"
	.asciz "FlyoutNavigation_FlyoutNavigationController_ToggleMenu"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_ToggleMenu
	.quad Lme_45

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM777=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM777
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM778=Lfde69_end - Lfde69_start
	.long LDIFF_SYM778
Lfde69_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_ToggleMenu

LDIFF_SYM779=Lme_45 - FlyoutNavigation_FlyoutNavigationController_ToggleMenu
	.long LDIFF_SYM779
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,154,4
	.align 3
Lfde69_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:GetIndex"
	.asciz "FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath
	.quad Lme_46

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM780=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM780
	.byte 1,105,3
	.asciz "indexPath"

LDIFF_SYM781=LTDIE_59_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM781
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM782=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM782
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM783=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM783
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM784=Lfde70_end - Lfde70_start
	.long LDIFF_SYM784
Lfde70_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath

LDIFF_SYM785=Lme_46 - FlyoutNavigation_FlyoutNavigationController_GetIndex_Foundation_NSIndexPath
	.long LDIFF_SYM785
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29,68,151,12,152,11,68,153,10,154,9
	.align 3
Lfde70_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_78:

	.byte 5
	.asciz "System_Collections_Generic_List`1"

	.byte 32,16
LDIFF_SYM786=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM786
	.byte 2,35,0,6
	.asciz "_items"

LDIFF_SYM787=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM787
	.byte 2,35,16,6
	.asciz "_size"

LDIFF_SYM788=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM788
	.byte 2,35,24,6
	.asciz "_version"

LDIFF_SYM789=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM789
	.byte 2,35,28,0,7
	.asciz "System_Collections_Generic_List`1"

LDIFF_SYM790=LTDIE_78 - Ldebug_info_start
	.long LDIFF_SYM790
LTDIE_78_POINTER:

	.byte 13
LDIFF_SYM791=LTDIE_78 - Ldebug_info_start
	.long LDIFF_SYM791
LTDIE_78_REFERENCE:

	.byte 14
LDIFF_SYM792=LTDIE_78 - Ldebug_info_start
	.long LDIFF_SYM792
LTDIE_77:

	.byte 5
	.asciz "MonoTouch_Dialog_Section"

	.byte 56,16
LDIFF_SYM793=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM793
	.byte 2,35,0,6
	.asciz "header"

LDIFF_SYM794=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM794
	.byte 2,35,32,6
	.asciz "footer"

LDIFF_SYM795=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM795
	.byte 2,35,40,6
	.asciz "Elements"

LDIFF_SYM796=LTDIE_78_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM796
	.byte 2,35,48,0,7
	.asciz "MonoTouch_Dialog_Section"

LDIFF_SYM797=LTDIE_77 - Ldebug_info_start
	.long LDIFF_SYM797
LTDIE_77_POINTER:

	.byte 13
LDIFF_SYM798=LTDIE_77 - Ldebug_info_start
	.long LDIFF_SYM798
LTDIE_77_REFERENCE:

	.byte 14
LDIFF_SYM799=LTDIE_77 - Ldebug_info_start
	.long LDIFF_SYM799
LTDIE_79:

	.byte 17
	.asciz "System_Collections_Generic_IEnumerator`1"

	.byte 16,7
	.asciz "System_Collections_Generic_IEnumerator`1"

LDIFF_SYM800=LTDIE_79 - Ldebug_info_start
	.long LDIFF_SYM800
LTDIE_79_POINTER:

	.byte 13
LDIFF_SYM801=LTDIE_79 - Ldebug_info_start
	.long LDIFF_SYM801
LTDIE_79_REFERENCE:

	.byte 14
LDIFF_SYM802=LTDIE_79 - Ldebug_info_start
	.long LDIFF_SYM802
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:GetIndexPath"
	.asciz "FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int
	.quad Lme_47

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM803=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM803
	.byte 1,105,3
	.asciz "index"

LDIFF_SYM804=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM804
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM805=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM805
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM806=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM806
	.byte 1,103,11
	.asciz "V_2"

LDIFF_SYM807=LTDIE_77_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM807
	.byte 1,102,11
	.asciz "V_3"

LDIFF_SYM808=LTDIE_79_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM808
	.byte 3,141,216,0,11
	.asciz "V_4"

LDIFF_SYM809=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM809
	.byte 1,101,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM810=Lfde71_end - Lfde71_start
	.long LDIFF_SYM810
Lfde71_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int

LDIFF_SYM811=Lme_47 - FlyoutNavigation_FlyoutNavigationController_GetIndexPath_int
	.long LDIFF_SYM811
	.long 0
	.byte 12,31,0,68,14,160,1,157,20,158,19,68,13,29,68,149,18,150,17,68,151,16,152,15,68,153,14,154,13
	.align 3
Lfde71_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_80:

	.byte 8
	.asciz "UIKit_UIInterfaceOrientation"

	.byte 8
LDIFF_SYM812=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM812
	.byte 9
	.asciz "Unknown"

	.byte 0,9
	.asciz "Portrait"

	.byte 1,9
	.asciz "PortraitUpsideDown"

	.byte 2,9
	.asciz "LandscapeLeft"

	.byte 4,9
	.asciz "LandscapeRight"

	.byte 3,0,7
	.asciz "UIKit_UIInterfaceOrientation"

LDIFF_SYM813=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM813
LTDIE_80_POINTER:

	.byte 13
LDIFF_SYM814=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM814
LTDIE_80_REFERENCE:

	.byte 14
LDIFF_SYM815=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM815
LTDIE_81:

	.byte 8
	.asciz "UIKit_UIInterfaceOrientationMask"

	.byte 8
LDIFF_SYM816=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM816
	.byte 9
	.asciz "Portrait"

	.byte 2,9
	.asciz "LandscapeLeft"

	.byte 16,9
	.asciz "LandscapeRight"

	.byte 8,9
	.asciz "PortraitUpsideDown"

	.byte 4,9
	.asciz "Landscape"

	.byte 24,9
	.asciz "All"

	.byte 30,9
	.asciz "AllButUpsideDown"

	.byte 26,0,7
	.asciz "UIKit_UIInterfaceOrientationMask"

LDIFF_SYM817=LTDIE_81 - Ldebug_info_start
	.long LDIFF_SYM817
LTDIE_81_POINTER:

	.byte 13
LDIFF_SYM818=LTDIE_81 - Ldebug_info_start
	.long LDIFF_SYM818
LTDIE_81_REFERENCE:

	.byte 14
LDIFF_SYM819=LTDIE_81 - Ldebug_info_start
	.long LDIFF_SYM819
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:ShouldAutorotateToInterfaceOrientation"
	.asciz "FlyoutNavigation_FlyoutNavigationController_ShouldAutorotateToInterfaceOrientation_UIKit_UIInterfaceOrientation"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_ShouldAutorotateToInterfaceOrientation_UIKit_UIInterfaceOrientation
	.quad Lme_48

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM820=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM820
	.byte 1,105,3
	.asciz "toInterfaceOrientation"

LDIFF_SYM821=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM821
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM822=LTDIE_81 - Ldebug_info_start
	.long LDIFF_SYM822
	.byte 3,141,200,0,11
	.asciz "V_1"

LDIFF_SYM823=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM823
	.byte 3,141,208,0,11
	.asciz "V_2"

LDIFF_SYM824=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM824
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM825=Lfde72_end - Lfde72_start
	.long LDIFF_SYM825
Lfde72_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_ShouldAutorotateToInterfaceOrientation_UIKit_UIInterfaceOrientation

LDIFF_SYM826=Lme_48 - FlyoutNavigation_FlyoutNavigationController_ShouldAutorotateToInterfaceOrientation_UIKit_UIInterfaceOrientation
	.long LDIFF_SYM826
	.long 0
	.byte 12,31,0,68,14,144,1,157,18,158,17,68,13,29,68,149,16,150,15,68,153,14,154,13
	.align 3
Lfde72_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:GetSupportedInterfaceOrientations"
	.asciz "FlyoutNavigation_FlyoutNavigationController_GetSupportedInterfaceOrientations"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_GetSupportedInterfaceOrientations
	.quad Lme_49

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM827=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM827
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM828=Lfde73_end - Lfde73_start
	.long LDIFF_SYM828
Lfde73_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_GetSupportedInterfaceOrientations

LDIFF_SYM829=Lme_49 - FlyoutNavigation_FlyoutNavigationController_GetSupportedInterfaceOrientations
	.long LDIFF_SYM829
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde73_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_82:

	.byte 5
	.asciz "System_Double"

	.byte 24,16
LDIFF_SYM830=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM830
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM831=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM831
	.byte 2,35,16,0,7
	.asciz "System_Double"

LDIFF_SYM832=LTDIE_82 - Ldebug_info_start
	.long LDIFF_SYM832
LTDIE_82_POINTER:

	.byte 13
LDIFF_SYM833=LTDIE_82 - Ldebug_info_start
	.long LDIFF_SYM833
LTDIE_82_REFERENCE:

	.byte 14
LDIFF_SYM834=LTDIE_82 - Ldebug_info_start
	.long LDIFF_SYM834
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:WillRotate"
	.asciz "FlyoutNavigation_FlyoutNavigationController_WillRotate_UIKit_UIInterfaceOrientation_double"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_WillRotate_UIKit_UIInterfaceOrientation_double
	.quad Lme_4a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM835=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM835
	.byte 2,141,16,3
	.asciz "toInterfaceOrientation"

LDIFF_SYM836=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM836
	.byte 2,141,24,3
	.asciz "duration"

LDIFF_SYM837=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM837
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM838=Lfde74_end - Lfde74_start
	.long LDIFF_SYM838
Lfde74_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_WillRotate_UIKit_UIInterfaceOrientation_double

LDIFF_SYM839=Lme_4a - FlyoutNavigation_FlyoutNavigationController_WillRotate_UIKit_UIInterfaceOrientation_double
	.long LDIFF_SYM839
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde74_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:DidRotate"
	.asciz "FlyoutNavigation_FlyoutNavigationController_DidRotate_UIKit_UIInterfaceOrientation"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_DidRotate_UIKit_UIInterfaceOrientation
	.quad Lme_4b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM840=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM840
	.byte 1,105,3
	.asciz "fromInterfaceOrientation"

LDIFF_SYM841=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM841
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM842=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM842
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM843=Lfde75_end - Lfde75_start
	.long LDIFF_SYM843
Lfde75_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_DidRotate_UIKit_UIInterfaceOrientation

LDIFF_SYM844=Lme_4b - FlyoutNavigation_FlyoutNavigationController_DidRotate_UIKit_UIInterfaceOrientation
	.long LDIFF_SYM844
	.long 0
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,152,20,153,19
	.align 3
Lfde75_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:WillAnimateRotation"
	.asciz "FlyoutNavigation_FlyoutNavigationController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double
	.quad Lme_4c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM845=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM845
	.byte 2,141,16,3
	.asciz "toInterfaceOrientation"

LDIFF_SYM846=LTDIE_80 - Ldebug_info_start
	.long LDIFF_SYM846
	.byte 2,141,24,3
	.asciz "duration"

LDIFF_SYM847=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM847
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM848=Lfde76_end - Lfde76_start
	.long LDIFF_SYM848
Lfde76_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double

LDIFF_SYM849=Lme_4c - FlyoutNavigation_FlyoutNavigationController_WillAnimateRotation_UIKit_UIInterfaceOrientation_double
	.long LDIFF_SYM849
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde76_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_83:

	.byte 5
	.asciz "_<EnsureInvokedOnMainThread>c__AnonStorey4"

	.byte 24,16
LDIFF_SYM850=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM850
	.byte 2,35,0,6
	.asciz "action"

LDIFF_SYM851=LTDIE_50_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM851
	.byte 2,35,16,0,7
	.asciz "_<EnsureInvokedOnMainThread>c__AnonStorey4"

LDIFF_SYM852=LTDIE_83 - Ldebug_info_start
	.long LDIFF_SYM852
LTDIE_83_POINTER:

	.byte 13
LDIFF_SYM853=LTDIE_83 - Ldebug_info_start
	.long LDIFF_SYM853
LTDIE_83_REFERENCE:

	.byte 14
LDIFF_SYM854=LTDIE_83 - Ldebug_info_start
	.long LDIFF_SYM854
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:EnsureInvokedOnMainThread"
	.asciz "FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action
	.quad Lme_4d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM855=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM855
	.byte 2,141,24,3
	.asciz "action"

LDIFF_SYM856=LTDIE_50_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM856
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM857=LTDIE_83_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM857
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM858=Lfde77_end - Lfde77_start
	.long LDIFF_SYM858
Lfde77_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action

LDIFF_SYM859=Lme_4d - FlyoutNavigation_FlyoutNavigationController_EnsureInvokedOnMainThread_System_Action
	.long LDIFF_SYM859
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8
	.align 3
Lfde77_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:IsMainThread"
	.asciz "FlyoutNavigation_FlyoutNavigationController_IsMainThread"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_IsMainThread
	.quad Lme_4e

	.byte 2,118,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM860=Lfde78_end - Lfde78_start
	.long LDIFF_SYM860
Lfde78_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_IsMainThread

LDIFF_SYM861=Lme_4e - FlyoutNavigation_FlyoutNavigationController_IsMainThread
	.long LDIFF_SYM861
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde78_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:Dispose"
	.asciz "FlyoutNavigation_FlyoutNavigationController_Dispose_bool"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_Dispose_bool
	.quad Lme_4f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM862=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM862
	.byte 1,105,3
	.asciz "disposing"

LDIFF_SYM863=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM863
	.byte 3,141,192,0,11
	.asciz "V_0"

LDIFF_SYM864=LTDIE_8_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM864
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM865=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM865
	.byte 1,103,11
	.asciz "V_2"

LDIFF_SYM866=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM866
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM867=Lfde79_end - Lfde79_start
	.long LDIFF_SYM867
Lfde79_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_Dispose_bool

LDIFF_SYM868=Lme_4f - FlyoutNavigation_FlyoutNavigationController_Dispose_bool
	.long LDIFF_SYM868
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,148,14,149,13,68,150,12,151,11,68,152,10,153,9
	.align 3
Lfde79_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:<Initialize>m__0"
	.asciz "FlyoutNavigation_FlyoutNavigationController__Initializem__0"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__Initializem__0
	.quad Lme_50

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM869=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM869
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM870=Lfde80_end - Lfde80_start
	.long LDIFF_SYM870
Lfde80_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__Initializem__0

LDIFF_SYM871=Lme_50 - FlyoutNavigation_FlyoutNavigationController__Initializem__0
	.long LDIFF_SYM871
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,154,4
	.align 3
Lfde80_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:<ShowMenu>m__1"
	.asciz "FlyoutNavigation_FlyoutNavigationController__ShowMenum__1"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__ShowMenum__1
	.quad Lme_51

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM872=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM872
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM873=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM873
	.byte 3,141,232,3,11
	.asciz "V_1"

LDIFF_SYM874=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM874
	.byte 3,141,200,3,11
	.asciz "V_2"

LDIFF_SYM875=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM875
	.byte 3,141,168,3,11
	.asciz "V_3"

LDIFF_SYM876=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM876
	.byte 3,141,136,3,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM877=Lfde81_end - Lfde81_start
	.long LDIFF_SYM877
Lfde81_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__ShowMenum__1

LDIFF_SYM878=Lme_51 - FlyoutNavigation_FlyoutNavigationController__ShowMenum__1
	.long LDIFF_SYM878
	.long 0
	.byte 12,31,0,84,14,240,4,157,78,158,77,68,13,29,68,151,76,152,75,68,153,74,154,73
	.align 3
Lfde81_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_84:

	.byte 5
	.asciz "_<HideMenu>c__AnonStorey3"

	.byte 56,16
LDIFF_SYM879=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM879
	.byte 2,35,0,6
	.asciz "statusFrame"

LDIFF_SYM880=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM880
	.byte 2,35,24,6
	.asciz "$this"

LDIFF_SYM881=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM881
	.byte 2,35,16,0,7
	.asciz "_<HideMenu>c__AnonStorey3"

LDIFF_SYM882=LTDIE_84 - Ldebug_info_start
	.long LDIFF_SYM882
LTDIE_84_POINTER:

	.byte 13
LDIFF_SYM883=LTDIE_84 - Ldebug_info_start
	.long LDIFF_SYM883
LTDIE_84_REFERENCE:

	.byte 14
LDIFF_SYM884=LTDIE_84 - Ldebug_info_start
	.long LDIFF_SYM884
	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:<HideMenu>m__2"
	.asciz "FlyoutNavigation_FlyoutNavigationController__HideMenum__2"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__HideMenum__2
	.quad Lme_52

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM885=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM885
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM886=LTDIE_84_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM886
	.byte 1,105,11
	.asciz "V_1"

LDIFF_SYM887=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM887
	.byte 3,141,184,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM888=Lfde82_end - Lfde82_start
	.long LDIFF_SYM888
Lfde82_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__HideMenum__2

LDIFF_SYM889=Lme_52 - FlyoutNavigation_FlyoutNavigationController__HideMenum__2
	.long LDIFF_SYM889
	.long 0
	.byte 12,31,0,68,14,176,2,157,38,158,37,68,13,29,68,153,36,154,35
	.align 3
Lfde82_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController:<ToggleMenu>m__3"
	.asciz "FlyoutNavigation_FlyoutNavigationController__ToggleMenum__3"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__ToggleMenum__3
	.quad Lme_53

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM890=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM890
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM891=Lfde83_end - Lfde83_start
	.long LDIFF_SYM891
Lfde83_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__ToggleMenum__3

LDIFF_SYM892=Lme_53 - FlyoutNavigation_FlyoutNavigationController__ToggleMenum__3
	.long LDIFF_SYM892
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde83_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/UAUIView:.ctor"
	.asciz "FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor
	.quad Lme_54

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM893=LTDIE_56_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM893
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM894=Lfde84_end - Lfde84_start
	.long LDIFF_SYM894
Lfde84_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor

LDIFF_SYM895=Lme_54 - FlyoutNavigation_FlyoutNavigationController_UAUIView__ctor
	.long LDIFF_SYM895
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde84_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/UAUIView:get_AccessibilityId"
	.asciz "FlyoutNavigation_FlyoutNavigationController_UAUIView_get_AccessibilityId"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_UAUIView_get_AccessibilityId
	.quad Lme_55

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM896=LTDIE_56_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM896
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM897=Lfde85_end - Lfde85_start
	.long LDIFF_SYM897
Lfde85_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_UAUIView_get_AccessibilityId

LDIFF_SYM898=Lme_55 - FlyoutNavigation_FlyoutNavigationController_UAUIView_get_AccessibilityId
	.long LDIFF_SYM898
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde85_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/UAUIView:set_AccessibilityId"
	.asciz "FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string
	.quad Lme_56

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM899=LTDIE_56_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM899
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM900=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM900
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM901=Lfde86_end - Lfde86_start
	.long LDIFF_SYM901
Lfde86_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string

LDIFF_SYM902=Lme_56 - FlyoutNavigation_FlyoutNavigationController_UAUIView_set_AccessibilityId_string
	.long LDIFF_SYM902
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde86_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<>c__AnonStorey0:.ctor"
	.asciz "FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor
	.quad Lme_57

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM903=LTDIE_53_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM903
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM904=Lfde87_end - Lfde87_start
	.long LDIFF_SYM904
Lfde87_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor

LDIFF_SYM905=Lme_57 - FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__ctor
	.long LDIFF_SYM905
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde87_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<>c__AnonStorey0:<>m__0"
	.asciz "FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__m__0"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__m__0
	.quad Lme_58

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM906=LTDIE_53_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM906
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM907=Lfde88_end - Lfde88_start
	.long LDIFF_SYM907
Lfde88_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__m__0

LDIFF_SYM908=Lme_58 - FlyoutNavigation_FlyoutNavigationController__c__AnonStorey0__m__0
	.long LDIFF_SYM908
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,154,4
	.align 3
Lfde88_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<>c__AnonStorey1:.ctor"
	.asciz "FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor
	.quad Lme_59

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM909=LTDIE_54_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM909
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM910=Lfde89_end - Lfde89_start
	.long LDIFF_SYM910
Lfde89_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor

LDIFF_SYM911=Lme_59 - FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__ctor
	.long LDIFF_SYM911
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde89_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<>c__AnonStorey1:<>m__0"
	.asciz "FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__m__0"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__m__0
	.quad Lme_5a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM912=LTDIE_54_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM912
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM913=Lfde90_end - Lfde90_start
	.long LDIFF_SYM913
Lfde90_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__m__0

LDIFF_SYM914=Lme_5a - FlyoutNavigation_FlyoutNavigationController__c__AnonStorey1__m__0
	.long LDIFF_SYM914
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,154,8
	.align 3
Lfde90_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<>c__AnonStorey2:.ctor"
	.asciz "FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor
	.quad Lme_5b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM915=LTDIE_55_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM915
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM916=Lfde91_end - Lfde91_start
	.long LDIFF_SYM916
Lfde91_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor

LDIFF_SYM917=Lme_5b - FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__ctor
	.long LDIFF_SYM917
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde91_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<>c__AnonStorey2:<>m__0"
	.asciz "FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__m__0"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__m__0
	.quad Lme_5c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM918=LTDIE_55_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM918
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM919=Lfde92_end - Lfde92_start
	.long LDIFF_SYM919
Lfde92_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__m__0

LDIFF_SYM920=Lme_5c - FlyoutNavigation_FlyoutNavigationController__c__AnonStorey2__m__0
	.long LDIFF_SYM920
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,154,4
	.align 3
Lfde92_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<EnsureInvokedOnMainThread>c__AnonStorey4:.ctor"
	.asciz "FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor
	.quad Lme_5d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM921=LTDIE_83_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM921
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM922=Lfde93_end - Lfde93_start
	.long LDIFF_SYM922
Lfde93_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor

LDIFF_SYM923=Lme_5d - FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__ctor
	.long LDIFF_SYM923
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde93_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<EnsureInvokedOnMainThread>c__AnonStorey4:<>m__0"
	.asciz "FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__m__0"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__m__0
	.quad Lme_5e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM924=LTDIE_83_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM924
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM925=Lfde94_end - Lfde94_start
	.long LDIFF_SYM925
Lfde94_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__m__0

LDIFF_SYM926=Lme_5e - FlyoutNavigation_FlyoutNavigationController__EnsureInvokedOnMainThreadc__AnonStorey4__m__0
	.long LDIFF_SYM926
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde94_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<HideMenu>c__AnonStorey3:.ctor"
	.asciz "FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor
	.quad Lme_5f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM927=LTDIE_84_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM927
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM928=Lfde95_end - Lfde95_start
	.long LDIFF_SYM928
Lfde95_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor

LDIFF_SYM929=Lme_5f - FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__ctor
	.long LDIFF_SYM929
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde95_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "FlyoutNavigation.FlyoutNavigationController/<HideMenu>c__AnonStorey3:<>m__0"
	.asciz "FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__m__0"

	.byte 0,0
	.quad FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__m__0
	.quad Lme_60

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM930=LTDIE_84_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM930
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM931=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM931
	.byte 3,141,176,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM932=Lfde96_end - Lfde96_start
	.long LDIFF_SYM932
Lfde96_start:

	.long 0
	.align 3
	.quad FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__m__0

LDIFF_SYM933=Lme_60 - FlyoutNavigation_FlyoutNavigationController__HideMenuc__AnonStorey3__m__0
	.long LDIFF_SYM933
	.long 0
	.byte 12,31,0,68,14,128,2,157,32,158,31,68,13,29,68,154,30
	.align 3
Lfde96_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_85:

	.byte 5
	.asciz "System_Array"

	.byte 16,16
LDIFF_SYM934=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM934
	.byte 2,35,0,0,7
	.asciz "System_Array"

LDIFF_SYM935=LTDIE_85 - Ldebug_info_start
	.long LDIFF_SYM935
LTDIE_85_POINTER:

	.byte 13
LDIFF_SYM936=LTDIE_85 - Ldebug_info_start
	.long LDIFF_SYM936
LTDIE_85_REFERENCE:

	.byte 14
LDIFF_SYM937=LTDIE_85 - Ldebug_info_start
	.long LDIFF_SYM937
	.byte 2
	.asciz "(wrapper_delegate-invoke)_System.Func`3<UIKit.UIGestureRecognizer,_UIKit.UITouch,_bool>:invoke_TResult_T1_T2"
	.asciz "wrapper_delegate_invoke_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool_invoke_TResult_T1_T2_UIKit_UIGestureRecognizer_UIKit_UITouch"

	.byte 0,0
	.quad wrapper_delegate_invoke_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool_invoke_TResult_T1_T2_UIKit_UIGestureRecognizer_UIKit_UITouch
	.quad Lme_66

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM938=LTDIE_16_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM938
	.byte 1,104,3
	.asciz "param0"

LDIFF_SYM939=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM939
	.byte 1,105,3
	.asciz "param1"

LDIFF_SYM940=LTDIE_18_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM940
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM941=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM941
	.byte 1,103,11
	.asciz "V_1"

LDIFF_SYM942=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM942
	.byte 1,102,11
	.asciz "V_2"

LDIFF_SYM943=LTDIE_85_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM943
	.byte 1,101,11
	.asciz "V_3"

LDIFF_SYM944=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM944
	.byte 1,100,11
	.asciz "V_4"

LDIFF_SYM945=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM945
	.byte 1,99,11
	.asciz "V_5"

LDIFF_SYM946=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM946
	.byte 3,141,232,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM947=Lfde97_end - Lfde97_start
	.long LDIFF_SYM947
Lfde97_start:

	.long 0
	.align 3
	.quad wrapper_delegate_invoke_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool_invoke_TResult_T1_T2_UIKit_UIGestureRecognizer_UIKit_UITouch

LDIFF_SYM948=Lme_66 - wrapper_delegate_invoke_System_Func_3_UIKit_UIGestureRecognizer_UIKit_UITouch_bool_invoke_TResult_T1_T2_UIKit_UIGestureRecognizer_UIKit_UITouch
	.long LDIFF_SYM948
	.long 0
	.byte 12,31,0,68,14,144,1,157,18,158,17,68,13,29,68,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9
	.align 3
Lfde97_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_delegate-invoke)_System.Action`1<UIKit.UIPanGestureRecognizer>:invoke_void_T"
	.asciz "wrapper_delegate_invoke_System_Action_1_UIKit_UIPanGestureRecognizer_invoke_void_T_UIKit_UIPanGestureRecognizer"

	.byte 0,0
	.quad wrapper_delegate_invoke_System_Action_1_UIKit_UIPanGestureRecognizer_invoke_void_T_UIKit_UIPanGestureRecognizer
	.quad Lme_67

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM949=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM949
	.byte 1,105,3
	.asciz "param0"

LDIFF_SYM950=LTDIE_1_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM950
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM951=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM951
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM952=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM952
	.byte 1,103,11
	.asciz "V_2"

LDIFF_SYM953=LTDIE_85_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM953
	.byte 1,102,11
	.asciz "V_3"

LDIFF_SYM954=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM954
	.byte 1,101,11
	.asciz "V_4"

LDIFF_SYM955=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM955
	.byte 1,100,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM956=Lfde98_end - Lfde98_start
	.long LDIFF_SYM956
Lfde98_start:

	.long 0
	.align 3
	.quad wrapper_delegate_invoke_System_Action_1_UIKit_UIPanGestureRecognizer_invoke_void_T_UIKit_UIPanGestureRecognizer

LDIFF_SYM957=Lme_67 - wrapper_delegate_invoke_System_Action_1_UIKit_UIPanGestureRecognizer_invoke_void_T_UIKit_UIPanGestureRecognizer
	.long LDIFF_SYM957
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,147,14,148,13,68,149,12,150,11,68,151,10,152,9,68,153,8
	.byte 154,7
	.align 3
Lfde98_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_delegate-invoke)_System.Action`1<Foundation.NSIndexPath>:invoke_void_T"
	.asciz "wrapper_delegate_invoke_System_Action_1_Foundation_NSIndexPath_invoke_void_T_Foundation_NSIndexPath"

	.byte 0,0
	.quad wrapper_delegate_invoke_System_Action_1_Foundation_NSIndexPath_invoke_void_T_Foundation_NSIndexPath
	.quad Lme_68

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM958=LTDIE_29_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM958
	.byte 1,105,3
	.asciz "param0"

LDIFF_SYM959=LTDIE_59_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM959
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM960=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM960
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM961=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM961
	.byte 1,103,11
	.asciz "V_2"

LDIFF_SYM962=LTDIE_85_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM962
	.byte 1,102,11
	.asciz "V_3"

LDIFF_SYM963=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM963
	.byte 1,101,11
	.asciz "V_4"

LDIFF_SYM964=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM964
	.byte 1,100,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM965=Lfde99_end - Lfde99_start
	.long LDIFF_SYM965
Lfde99_start:

	.long 0
	.align 3
	.quad wrapper_delegate_invoke_System_Action_1_Foundation_NSIndexPath_invoke_void_T_Foundation_NSIndexPath

LDIFF_SYM966=Lme_68 - wrapper_delegate_invoke_System_Action_1_Foundation_NSIndexPath_invoke_void_T_Foundation_NSIndexPath
	.long LDIFF_SYM966
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,147,14,148,13,68,149,12,150,11,68,151,10,152,9,68,153,8
	.byte 154,7
	.align 3
Lfde99_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Array:InternalArray__IEnumerable_GetEnumerator<T_REF>"
	.asciz "System_Array_InternalArray__IEnumerable_GetEnumerator_T_REF"

	.byte 1,71
	.quad System_Array_InternalArray__IEnumerable_GetEnumerator_T_REF
	.quad Lme_69

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM967=LTDIE_85_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM967
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM968=Lfde100_end - Lfde100_start
	.long LDIFF_SYM968
Lfde100_start:

	.long 0
	.align 3
	.quad System_Array_InternalArray__IEnumerable_GetEnumerator_T_REF

LDIFF_SYM969=Lme_69 - System_Array_InternalArray__IEnumerable_GetEnumerator_T_REF
	.long LDIFF_SYM969
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29
	.align 3
Lfde100_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_86:

	.byte 5
	.asciz "_InternalEnumerator`1"

	.byte 32,16
LDIFF_SYM970=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM970
	.byte 2,35,0,6
	.asciz "array"

LDIFF_SYM971=LTDIE_85_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM971
	.byte 2,35,16,6
	.asciz "idx"

LDIFF_SYM972=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM972
	.byte 2,35,24,0,7
	.asciz "_InternalEnumerator`1"

LDIFF_SYM973=LTDIE_86 - Ldebug_info_start
	.long LDIFF_SYM973
LTDIE_86_POINTER:

	.byte 13
LDIFF_SYM974=LTDIE_86 - Ldebug_info_start
	.long LDIFF_SYM974
LTDIE_86_REFERENCE:

	.byte 14
LDIFF_SYM975=LTDIE_86 - Ldebug_info_start
	.long LDIFF_SYM975
	.byte 2
	.asciz "System.Array/InternalEnumerator`1<T_REF>:.ctor"
	.asciz "System_Array_InternalEnumerator_1_T_REF__ctor_System_Array"

	.byte 1,215,1
	.quad System_Array_InternalEnumerator_1_T_REF__ctor_System_Array
	.quad Lme_6a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM976=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM976
	.byte 1,105,3
	.asciz "array"

LDIFF_SYM977=LTDIE_85_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM977
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM978=Lfde101_end - Lfde101_start
	.long LDIFF_SYM978
Lfde101_start:

	.long 0
	.align 3
	.quad System_Array_InternalEnumerator_1_T_REF__ctor_System_Array

LDIFF_SYM979=Lme_6a - System_Array_InternalEnumerator_1_T_REF__ctor_System_Array
	.long LDIFF_SYM979
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,153,6
	.align 3
Lfde101_end:

.section __DWARF, __debug_info,regular,debug

	.byte 0
Ldebug_info_end:
.text
	.align 3
mem_end:
