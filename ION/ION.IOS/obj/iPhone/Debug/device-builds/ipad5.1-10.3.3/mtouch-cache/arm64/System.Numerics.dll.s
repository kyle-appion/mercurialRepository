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
	.asciz "System.Numerics.dll"
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
	.no_dead_strip SR_Format_string_object
SR_Format_string_object:
.file 1 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/mcs/build/common/SR.cs"
.loc 1 36 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #200]
.word 0x3980b410
.word 0xb5000050
bl _p_1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #208]
.word 0xf9400000
.word 0xf9400ba1
.word 0xf9400fa2
bl _p_2
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_0:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int
System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int:
.file 2 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/external/corefx/src/System.Runtime.Numerics/src/System/Globalization/FormatProvider.BigInteger.cs"
.loc 2 18 0 prologue_end
.word 0xa9b27bfd
.word 0x910003fd
.word 0xf9000bb3
.word 0xf9000fba
.word 0xf90013a0
.word 0xf90017a1
.word 0xf9001ba2
.word 0xf9001fa3
.word 0xf90023a4
.word 0xf90027a5
.word 0xf9002ba6
.word 0xb900d3bf
.word 0xd2800000
.word 0xf9005fa0
.word 0xf90063a0
.word 0xf90067a0
.word 0x910343a1
.word 0xf9401fa0
bl _p_3
.word 0x53003c13
.loc 2 20 0
.word 0xf94027a0
.word 0xf9006fa0
.word 0xf94027a0
.word 0xb4000080
.word 0xf9406fa0
.word 0xb9801800
.word 0x35000080
.word 0xd2800000
.word 0x2a0003fa
.word 0x1400000b
.word 0xd2800000
.word 0x93407c01
.word 0xf9406fa0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x540007a9
.word 0xd37ff821
.word 0x8b010000
.word 0x9100801a
.loc 2 22 0
.word 0xd2800000
.word 0xf9005fa0
.word 0xf90063a0
.word 0xf90067a0
.loc 2 23 0
.word 0xb98053a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b000340
.word 0xf90067a0
.loc 2 24 0
.word 0xb98023a0
.word 0xb900bba0
.loc 2 25 0
.word 0xb9802ba0
.word 0xb900bfa0
.loc 2 26 0
.word 0x3940c3a0
.word 0x390303a0
.loc 2 27 0
.word 0x34000293
.loc 2 28 0
.word 0xf9405fa0
.word 0xf90053a0
.word 0xf94063a0
.word 0xf90057a0
.word 0xf94067a0
.word 0xf9005ba0
.word 0xb980d3a2
.word 0x910223a0
.word 0xf94053a1
.word 0xf90047a1
.word 0xf94057a1
.word 0xf9004ba1
.word 0xf9405ba1
.word 0xf9004fa1
.word 0xaa1303e1
.word 0xf94023a3
.word 0xd2800004
bl _p_4
.word 0x14000011
.loc 2 29 0
.word 0xf9405fa0
.word 0xf9003ba0
.word 0xf94063a0
.word 0xf9003fa0
.word 0xf94067a0
.word 0xf90043a0
.word 0x910163a0
.word 0xf9403ba1
.word 0xf9002fa1
.word 0xf9403fa1
.word 0xf90033a1
.word 0xf94043a1
.word 0xf90037a1
.word 0xf9401fa1
.word 0xf94023a2
bl _p_5
.word 0xf9400bb3
.word 0xf9400fba
.word 0x910003bf
.word 0xa8ce7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_1:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_
System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_:
.loc 2 45 0 prologue_end
.word 0xa9b57bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9001ba4
.word 0xf9001fa5
.word 0xf90023a6
.word 0xd2800000
.word 0xf9004ba0
.word 0xf9004fa0
.word 0xf90053a0
.word 0xd2800000
.word 0xf9004ba0
.word 0xf9004fa0
.word 0xf90053a0
.loc 2 55 0
.word 0xd2800020
.word 0x93407c00
.word 0xf90053a0
.loc 2 57 0
.word 0x910243a2
.word 0xf9400ba0
.word 0xb9801ba1
.word 0xf94017a3
.word 0xf94013a4
.word 0xd2800005
bl _p_7
.word 0x53001c00
.word 0x35000120
.loc 2 59 0
.word 0xf9401ba0
.word 0xb900001f
.loc 2 60 0
.word 0xf9401fa0
.word 0xb900001f
.loc 2 61 0
.word 0xf94023a0
.word 0x3900001f
.loc 2 62 0
.word 0xd2800000
.word 0x1400001d
.loc 2 66 0
.word 0xf9404ba0
.word 0xf9003fa0
.word 0xf9404fa0
.word 0xf90043a0
.word 0xf94053a0
.word 0xf90047a0
.word 0xb9807ba1
.word 0xf9401ba0
.word 0xb9000001
.loc 2 67 0
.word 0xf9404ba0
.word 0xf90033a0
.word 0xf9404fa0
.word 0xf90037a0
.word 0xf94053a0
.word 0xf9003ba0
.word 0xb98067a1
.word 0xf9401fa0
.word 0xb9000001
.loc 2 68 0
.word 0xf9404ba0
.word 0xf90027a0
.word 0xf9404fa0
.word 0xf9002ba0
.word 0xf94053a0
.word 0xf9002fa0
.word 0x394143a1
.word 0xf94023a0
.word 0x39000001
.loc 2 69 0
.word 0xd2800020
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_2:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_IsWhite_char
System_Globalization_FormatProvider_Number_IsWhite_char:
.file 3 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/external/corefx/src/Common/src/System/Globalization/FormatProvider.Number.cs"
.loc 3 296 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0x794023a0
.word 0xd280041e
.word 0x6b1e001f
.word 0x540001c0
.word 0x794023a0
.word 0xd280013e
.word 0x6b1e001f
.word 0x5400010b
.word 0x794023a0
.word 0xd28001be
.word 0x6b1e001f
.word 0x9a9fd7e0
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x14000004
.word 0xd2800000
.word 0x14000002
.word 0xd2800020
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_3:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_MatchChars_char__string
System_Globalization_FormatProvider_Number_MatchChars_char__string:
.loc 3 301 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fa0
.word 0xf90013a1
.word 0xf94013b8
.word 0xf94013a0
.word 0xb4000040
.word 0x91005318
.loc 3 303 0
.word 0xf9400fa0
.word 0xaa1803e1
bl _p_8
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_4:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_MatchChars_char__char_
System_Globalization_FormatProvider_Number_MatchChars_char__char_:
.loc 3 311 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0x79400340
.word 0x35000160
.loc 3 313 0
.word 0xd2800000
.word 0x2a0003e0
.word 0x14000016
.loc 3 321 0
.word 0x91000b39
.loc 3 322 0
.word 0x91000b5a
.loc 3 323 0
.word 0xaa1a03e0
.word 0x79400000
.word 0x35000060
.word 0xaa1903e0
.word 0x1400000f
.loc 3 319 0
.word 0x79400320
.word 0x79400341
.word 0x6b01001f
.word 0x54fffec0
.word 0x79400340
.word 0xd280141e
.word 0x6b1e001f
.word 0x540000a1
.word 0x79400320
.word 0xd280041e
.word 0x6b1e001f
.word 0x54fffdc0
.loc 3 325 0
.word 0xd2800000
.word 0x2a0003e0
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_5:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool:
.loc 3 337 0 prologue_end
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xf90013b6
.word 0xa902e7b8
.word 0xf9001fba
.word 0xf90023a0
.word 0xaa0103f6
.word 0xf90027a2
.word 0xf9002ba3
.word 0xf9002fa4
.word 0xf90033a5
.word 0xf94027a0
.word 0xb900041f
.loc 3 338 0
.word 0x3900201f
.loc 3 341 0
.word 0xd2800014
.loc 3 343 0
.word 0xd2800013
.loc 3 344 0
.word 0xd280201e
.word 0xa1e02c0
.word 0x34000160
.loc 3 346 0
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9402c14
.loc 3 349 0
.word 0xf940001e
.word 0xf9402801
.word 0xf90037a1
.loc 3 350 0
.word 0xf940001e
.word 0xf940241a
.loc 3 351 0
.word 0xd2800033
.loc 3 352 0
.word 0x14000007
.loc 3 355 0
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9401c01
.word 0xf90037a1
.loc 3 356 0
.word 0xf940001e
.word 0xf940201a
.loc 3 359 0
.word 0xb90073bf
.loc 3 360 0
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x9a9f97f9
.loc 3 361 0
.word 0xaa1903e0
.word 0x35000080
.word 0xd280041e
.word 0xb9007bbe
.word 0x14000004
.word 0xd29ffffe
.word 0xf2affffe
.word 0xb9007bbe
.word 0xb9807ba0
.word 0xb90083a0
.loc 3 363 0
.word 0xf94023a0
.word 0xf9400000
.word 0xf90047a0
.loc 3 364 0
.word 0x79400018
.loc 3 367 0
.word 0xf94027a0
.word 0xf9400800
.word 0xf9004ba0
.loc 3 373 0
.word 0xaa1803e0
bl _p_9
.word 0x53001c00
.word 0x34000240
.word 0xd280003e
.word 0xa1e02c0
.word 0x340001e0
.word 0xb98073a0
.word 0xd280003e
.word 0xa1e0000
.word 0x34000b80
.word 0xb98073a0
.word 0xd280041e
.word 0xa1e0000
.word 0x35000b00
.word 0xf9402fa0
.word 0xf940001e
.word 0xb980bc00
.word 0xd280005e
.word 0x6b1e001f
.word 0x54000a40
.loc 3 375 0
.word 0xd280009e
.word 0xa1e02c0
.word 0x34000540
.word 0xb98073a0
.word 0xd280003e
.word 0xa1e0000
.word 0x350004c0
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9401401
.word 0xf94047a0
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x54000241
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9401801
.word 0xf94047a0
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x540001c0
.word 0xf94027a0
.word 0xd280003e
.word 0x3900201e
.word 0xd2800020
.word 0x6b1f001f
.loc 3 377 0
.word 0xb98073a0
.word 0xd280003e
.word 0x2a1e0000
.word 0xb90073a0
.loc 3 378 0
.word 0xf94057a0
.word 0xd1000800
.word 0xf90047a0
.loc 3 379 0
.word 0x14000026
.loc 3 380 0
.word 0xd280051e
.word 0x6b1e031f
.word 0x54000201
.word 0xd280021e
.word 0xa1e02c0
.word 0x340001a0
.word 0xb98073a0
.word 0xd280003e
.word 0xa1e0000
.word 0x35000120
.loc 3 382 0
.word 0xb98073a0
.word 0xd280007e
.word 0x2a1e0000
.word 0xb90073a0
.loc 3 383 0
.word 0xf94027a0
.word 0xd280003e
.word 0x3900201e
.loc 3 384 0
.word 0x14000014
.loc 3 385 0
.word 0xb4000334
.word 0xf94047a0
.word 0xaa1403e1
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x540001e0
.loc 3 387 0
.word 0xb98073a0
.word 0xd280041e
.word 0x2a1e0000
.word 0xb90073a0
.loc 3 388 0
.word 0xd2800014
.loc 3 392 0
.word 0xf94057a0
.word 0xd1000800
.word 0xf90047a0
.loc 3 399 0
.word 0xf94047a0
.word 0x91000801
.word 0xaa0103e0
.word 0xf90047a1
.word 0x79400018
.loc 3 369 0
.word 0x17ffff95
.loc 3 402 0
.word 0xb9009bbf
.loc 3 403 0
.word 0xb900a3bf
.loc 3 406 0
.word 0xd280061e
.word 0x6b1e031f
.word 0x5400008b
.word 0xd280073e
.word 0x6b1e031f
.word 0x5400020d
.word 0xd280401e
.word 0xa1e02c0
.word 0x34000a60
.word 0xd2800c3e
.word 0x6b1e031f
.word 0x5400008b
.word 0xd2800cde
.word 0x6b1e031f
.word 0x540000ed
.word 0xd280083e
.word 0x6b1e031f
.word 0x5400094b
.word 0xd28008de
.word 0x6b1e031f
.word 0x540008ec
.loc 3 408 0
.word 0xb98073a0
.word 0xd280009e
.word 0x2a1e0000
.word 0xb90073a0
.loc 3 410 0
.word 0xd280061e
.word 0x6b1e031f
.word 0x54000121
.word 0xb98073a0
.word 0xd280011e
.word 0xa1e0000
.word 0x350000a0
.word 0x34000639
.word 0xd280401e
.word 0xa1e02c0
.word 0x340005c0
.loc 3 412 0
.word 0xb9809ba0
.word 0xb98083a1
.word 0x6b01001f
.word 0x5400038a
.loc 3 414 0
.word 0x340000f9
.loc 3 415 0
.word 0xf9402ba0
.word 0xaa1803e1
.word 0xf9402ba2
.word 0xf940005e
bl _p_11
.word 0x1400000b
.loc 3 417 0
.word 0xb9809ba0
.word 0xb9809ba1
.word 0x11000421
.word 0xb9009ba1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf9404ba0
.word 0x8b010000
.word 0x79000018
.loc 3 418 0
.word 0xd280061e
.word 0x6b1e031f
.word 0x9a9f17e0
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x394183a1
.word 0x2a010000
.word 0x34000060
.loc 3 420 0
.word 0xb9809ba0
.word 0xb900a3a0
.loc 3 423 0
.word 0xb98073a0
.word 0xd280021e
.word 0xa1e0000
.word 0x350000c0
.loc 3 425 0
.word 0xf94027a1
.word 0x91001020
.word 0xb9800421
.word 0x11000421
.word 0xb9000001
.loc 3 427 0
.word 0xb98073a0
.word 0xd280011e
.word 0x2a1e0000
.word 0xb90073a0
.loc 3 428 0
.word 0x1400005e
.loc 3 429 0
.word 0xb98073a0
.word 0xd280021e
.word 0xa1e0000
.word 0x34000b40
.loc 3 431 0
.word 0xf94027a1
.word 0x91001020
.word 0xb9800421
.word 0x51000421
.word 0xb9000001
.loc 3 433 0
.word 0x14000054
.loc 3 434 0
.word 0xd280041e
.word 0xa1e02c0
.word 0x34000500
.word 0xb98073a0
.word 0xd280021e
.word 0xa1e0000
.word 0x35000480
.word 0xf94047a0
.word 0xf94037a1
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x54000241
.word 0x34000333
.word 0xb98073a0
.word 0xd280041e
.word 0xa1e0000
.word 0x350002a0
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9401c01
.word 0xf94047a0
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x54000120
.loc 3 436 0
.word 0xb98073a0
.word 0xd280021e
.word 0x2a1e0000
.word 0xb90073a0
.loc 3 437 0
.word 0xf94057a0
.word 0xd1000800
.word 0xf90047a0
.loc 3 438 0
.word 0x1400002a
.loc 3 439 0
.word 0xd280081e
.word 0xa1e02c0
.word 0x340005a0
.word 0xb98073a0
.word 0xd280009e
.word 0xa1e0000
.word 0x34000520
.word 0xb98073a0
.word 0xd280021e
.word 0xa1e0000
.word 0x350004a0
.word 0xf94047a0
.word 0xaa1a03e1
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x54000241
.word 0x34000353
.word 0xb98073a0
.word 0xd280041e
.word 0xa1e0000
.word 0x350002c0
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9402001
.word 0xf94047a0
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x54000140
.loc 3 441 0
.word 0xf94057a0
.word 0xd1000800
.word 0xf90047a0
.loc 3 447 0
.word 0xf94047a0
.word 0x91000801
.word 0xaa0103e0
.word 0xf90047a1
.word 0x79400018
.loc 3 404 0
.word 0x17ffff4d
.loc 3 450 0
.word 0xd280001a
.loc 3 451 0
.word 0xb980a3a1
.word 0xf94027a0
.word 0xb9000001
.loc 3 452 0
.word 0x340000f9
.loc 3 453 0
.word 0xf9402ba0
.word 0xd2800001
.word 0xf9402ba2
.word 0xf940005e
bl _p_11
.word 0x14000008
.loc 3 455 0
.word 0xb980a3a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf9404ba0
.word 0x8b010000
.word 0x7900001f
.loc 3 456 0
.word 0xb98073a0
.word 0xd280009e
.word 0xa1e0000
.word 0x340019a0
.loc 3 458 0
.word 0xd28008be
.word 0x6b1e031f
.word 0x54000080
.word 0xd2800cbe
.word 0x6b1e031f
.word 0x54000b61
.word 0xd280101e
.word 0xa1e02c0
.word 0x34000b00
.loc 3 460 0
.word 0xf94047b9
.loc 3 461 0
.word 0xf94047a0
.word 0x91000800
.word 0xaa0003e1
.word 0xf90047a0
.word 0x79400038
.loc 3 462 0
.word 0xf9402fa1
.word 0xf940003e
.word 0xf9401421
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x540000c0
.loc 3 464 0
.word 0xf94057a0
.word 0xf90047a0
.word 0xf94057a0
.word 0x79400018
.loc 3 465 0
.word 0x14000012
.loc 3 466 0
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9401801
.word 0xf94047a0
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x540000c0
.loc 3 468 0
.word 0xf94057a0
.word 0xf90047a0
.word 0xf94057a0
.word 0x79400018
.loc 3 469 0
.word 0xd280003a
.loc 3 471 0
.word 0xd280061e
.word 0x6b1e031f
.word 0x5400058b
.word 0xd280073e
.word 0x6b1e031f
.word 0x5400052c
.loc 3 473 0
.word 0xd2800019
.loc 3 476 0
.word 0xd280015e
.word 0x1b1e7f20
.word 0x5100c301
.word 0xb010019
.loc 3 477 0
.word 0xf94047a0
.word 0x91000801
.word 0xaa0103e0
.word 0xf90047a1
.word 0x79400018
.loc 3 478 0
.word 0xd2807d1e
.word 0x6b1e033f
.word 0x540001cd
.loc 3 480 0
.word 0xd284e1f9
.word 0x14000006
.loc 3 483 0
.word 0xf94047a0
.word 0x91000801
.word 0xaa0103e0
.word 0xf90047a1
.word 0x79400018
.loc 3 481 0
.word 0xd280061e
.word 0x6b1e031f
.word 0x5400008b
.word 0xd280073e
.word 0x6b1e031f
.word 0x54fffecd
.loc 3 486 0
.word 0xd280061e
.word 0x6b1e031f
.word 0x5400008b
.word 0xd280073e
.word 0x6b1e031f
.word 0x54fffc4d
.loc 3 487 0
.word 0x3400005a
.loc 3 489 0
.word 0x4b1903f9
.loc 3 491 0
.word 0xf94027a1
.word 0x91001020
.word 0xb9800421
.word 0xb190021
.word 0xb9000001
.loc 3 492 0
.word 0x14000003
.loc 3 495 0
.word 0xf90047b9
.loc 3 496 0
.word 0x79400338
.loc 3 501 0
.word 0xaa1803e0
bl _p_9
.word 0x53001c00
.word 0x34000080
.word 0xd280005e
.word 0xa1e02c0
.word 0x35000920
.loc 3 503 0
.word 0xd280011e
.word 0xa1e02c0
.word 0x34000540
.word 0xb98073a0
.word 0xd280003e
.word 0xa1e0000
.word 0x350004c0
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9401401
.word 0xf94047a0
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x54000241
.word 0xf9402fa0
.word 0xf940001e
.word 0xf9401801
.word 0xf94047a0
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x540001c0
.word 0xf94027a0
.word 0xd280003e
.word 0x3900201e
.word 0xd2800020
.word 0x6b1f001f
.loc 3 505 0
.word 0xb98073a0
.word 0xd280003e
.word 0x2a1e0000
.word 0xb90073a0
.loc 3 506 0
.word 0xf94057a0
.word 0xd1000800
.word 0xf90047a0
.loc 3 507 0
.word 0x1400001d
.loc 3 508 0
.word 0xd280053e
.word 0x6b1e031f
.word 0x54000161
.word 0xb98073a0
.word 0xd280005e
.word 0xa1e0000
.word 0x340000e0
.loc 3 510 0
.word 0xb98073a0
.word 0x9280005e
.word 0xf2bffffe
.word 0xa1e0000
.word 0xb90073a0
.loc 3 511 0
.word 0x14000010
.loc 3 512 0
.word 0xb40002b4
.word 0xf94047a0
.word 0xaa1403e1
bl _p_10
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90057a1
.word 0xd2800001
.word 0x2a0103e1
.word 0xeb01001f
.word 0x54000160
.loc 3 514 0
.word 0xd2800014
.loc 3 515 0
.word 0xf94057a0
.word 0xd1000800
.word 0xf90047a0
.loc 3 522 0
.word 0xf94047a0
.word 0x91000801
.word 0xaa0103e0
.word 0xf90047a1
.word 0x79400018
.loc 3 499 0
.word 0x17ffffac
.loc 3 524 0
.word 0xb98073a0
.word 0xd280005e
.word 0xa1e0000
.word 0x35000280
.loc 3 526 0
.word 0xb98073a0
.word 0xd280011e
.word 0xa1e0000
.word 0x35000160
.loc 3 528 0
.word 0x394183a0
.word 0x35000060
.loc 3 530 0
.word 0xf94027a0
.word 0xb900041f
.loc 3 532 0
.word 0xb98073a0
.word 0xd280021e
.word 0xa1e0000
.word 0x35000060
.loc 3 534 0
.word 0xf94027a0
.word 0x3900201f
.loc 3 537 0
.word 0xf94047a1
.word 0xf94023a0
.word 0xf9000001
.loc 3 538 0
.word 0xd2800020
.word 0x14000005
.loc 3 541 0
.word 0xf94047a1
.word 0xf94023a0
.word 0xf9000001
.loc 3 542 0
.word 0xd2800000
.word 0xa94153b3
.word 0xf94013b6
.word 0xa942e7b8
.word 0xf9401fba
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_6:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_TrailingZeros_string_int
System_Globalization_FormatProvider_Number_TrailingZeros_string_int:
.loc 3 548 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f9
.word 0xf90013a1
.word 0xb98023b8
.word 0x1400000d
.loc 3 550 0
.word 0x93407f00
.word 0xb9801321
.word 0xeb00003f
.word 0x10000011
.word 0x54000209
.word 0xd37ff800
.word 0x8b190000
.word 0x79402800
.word 0x34000060
.loc 3 552 0
.word 0xd2800000
.word 0x14000006
.loc 3 548 0
.word 0x11000718
.word 0xb9801320
.word 0x6b00031f
.word 0x54fffe4b
.loc 3 555 0
.word 0xd2800020
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_7:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool:
.loc 3 560 0 prologue_end
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bb3
.word 0xf9000fb5
.word 0xaa0003f5
.word 0xf90013a1
.word 0xf90017a2
.word 0xf9001ba3
.word 0xf9001fa4
.word 0xf90023a5
.word 0xf90027bf
.word 0xb5000075
.loc 3 562 0
.word 0xd2800000
.word 0x1400006b
.word 0xf9002bb5
.loc 3 566 0
.word 0xaa1503f3
.word 0xb4000055
.word 0x91005273
.loc 3 568 0
.word 0xf90027b3
.loc 3 569 0
.word 0x910123a0
.word 0xb98023a1
.word 0xf94017a2
.word 0xf9401ba3
.word 0xf9401fa4
.word 0x394103a5
bl _p_12
.word 0x53001c00
.word 0x34000b40
.word 0xf94027a0
.word 0xcb130000
.word 0xd2800041
.word 0x93407c21
.word 0xeb1f003f
.word 0x10000011
.word 0x54000bc0
.word 0xd29ffffe
.word 0xf2bffffe
.word 0xf2dffffe
.word 0xf2fffffe
.word 0xeb1e003f
.word 0x9a9f17e2
.word 0xd2800003
.word 0xf2f00003
.word 0xeb03001f
.word 0x9a9f17e3
.word 0xa030042
.word 0xd280003e
.word 0x6b1e005f
.word 0x10000011
.word 0x54000a40
.word 0xf100003f
.word 0x10000011
.word 0x54000980
.word 0xd2800010
.word 0xf2f00010
.word 0xeb10001f
.word 0x9a9f17f1
.word 0xd29ffff0
.word 0xf2bffff0
.word 0xf2dffff0
.word 0xf2fffff0
.word 0xeb10003f
.word 0x9a9f17f0
.word 0x8a110210
.word 0xf100061f
.word 0x10000011
.word 0x54000760
.word 0x9ac10c00
.word 0xb98012a1
.word 0x93407c21
.word 0xeb01001f
.word 0x5400060a
.word 0xf94027a0
.word 0xcb130000
.word 0xd2800041
.word 0x93407c21
.word 0xeb1f003f
.word 0x10000011
.word 0x54000640
.word 0xd29ffffe
.word 0xf2bffffe
.word 0xf2dffffe
.word 0xf2fffffe
.word 0xeb1e003f
.word 0x9a9f17e2
.word 0xd2800003
.word 0xf2f00003
.word 0xeb03001f
.word 0x9a9f17e3
.word 0xa030042
.word 0xd280003e
.word 0x6b1e005f
.word 0x10000011
.word 0x540004c0
.word 0xf100003f
.word 0x10000011
.word 0x54000400
.word 0xd2800010
.word 0xf2f00010
.word 0xeb10001f
.word 0x9a9f17f1
.word 0xd29ffff0
.word 0xf2bffff0
.word 0xf2dffff0
.word 0xf2fffff0
.word 0xeb10003f
.word 0x9a9f17f0
.word 0x8a110210
.word 0xf100061f
.word 0x10000011
.word 0x540001e0
.word 0x9ac10c00
.word 0x93407c01
.word 0xaa1503e0
bl _p_13
.word 0x53001c00
.word 0x35000060
.loc 3 572 0
.word 0xd2800000
.word 0x14000002
.loc 3 576 0
.word 0xd2800020
.word 0xf9400bb3
.word 0xf9400fb5
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd28011a0
.word 0xaa1103e1
bl _p_6
.word 0xd2801580
.word 0xaa1103e1
bl _p_6
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_8:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int
System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int:
.loc 3 0 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003f7
.word 0xaa0103f8
.word 0xaa0203f9
.word 0xaa0303fa
.word 0x14000016
.loc 3 629 0
.word 0xb9800300
.word 0x51000416
.word 0xaa1603e0
.word 0xb9000300
.word 0x93407ec0
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0xd2800141
.word 0xf100003f
.word 0x10000011
.word 0x540002c0
.word 0x1ac10b3e
.word 0x1b01e7c1
.word 0x1100c021
.word 0x79000001
.loc 3 630 0
.word 0xd29999a1
.word 0xf2b99981
.word 0x2a1903e0
.word 0x9b017c00
.word 0xd363fc19
.loc 3 627 0
.word 0x51000741
.word 0xaa0103e0
.word 0xaa0103fa
.word 0x6b1f001f
.word 0x54fffcea
.word 0x35fffcd9
.loc 3 632 0
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801580
.word 0xaa1103e1
bl _p_6

Lme_9:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_
System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_:
.loc 3 636 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xaa0003f9
.word 0xf9001ba1
.word 0xb4000cb9
.loc 3 637 0
.word 0xaa1903f8
.loc 3 638 0
.word 0xaa1903f7
.word 0xb4000059
.word 0x910052f7
.loc 3 640 0
.word 0xd2800019
.loc 3 641 0
.word 0xd2800000
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400018
.loc 3 642 0
.word 0xaa1803e0
.word 0x34000b00
.loc 3 644 0
.word 0xd280083e
.word 0x6b1e031f
.word 0x5400008b
.word 0xd2800b5e
.word 0x6b1e031f
.word 0x540000ed
.word 0xd2800c3e
.word 0x6b1e031f
.word 0x5400092b
.word 0xd2800f5e
.word 0x6b1e031f
.word 0x540008cc
.loc 3 646 0
.word 0x11000739
.loc 3 647 0
.word 0x92800016
.word 0xf2bffff6
.loc 3 648 0
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x5400062b
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0xd280073e
.word 0x6b1e001f
.word 0x5400052c
.loc 3 650 0
.word 0xaa1903e0
.word 0x11000739
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0x5100c016
.word 0x14000010
.loc 3 653 0
.word 0xd280015e
.word 0x1b1e7ec0
.word 0xaa1903e1
.word 0x11000739
.word 0x93407c21
.word 0xd2800042
.word 0x9b027c21
.word 0x8b0102e1
.word 0x79400021
.word 0xb010000
.word 0x5100c016
.loc 3 654 0
.word 0xaa1603e0
.word 0xd280015e
.word 0x6b1e001f
.word 0x5400022a
.loc 3 651 0
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x5400012b
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0xd280073e
.word 0x6b1e001f
.word 0x54fffc4d
.loc 3 658 0
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0x350000a0
.loc 3 660 0
.word 0xf9401ba0
.word 0xb9000016
.loc 3 661 0
.word 0xaa1803e0
.word 0x1400000c
.loc 3 665 0
.word 0xf9401ba0
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900001e
.loc 3 666 0
.word 0xd2800000
.word 0x14000006
.loc 3 671 0
.word 0xf9401ba0
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900001e
.loc 3 672 0
.word 0xd28008e0
.word 0xa9415fb6
.word 0xa94267b8
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_a:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool
System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool:
.loc 3 679 0 prologue_end
.word 0xd2804810
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xf90023ba
.word 0xf90027a0
.word 0xaa0103f7
.word 0xaa0203f8
.word 0xaa0303f9
.word 0xaa0403fa

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #216]
.word 0xd2800601
bl _p_14
.word 0xf9011ba0
.word 0xd2800d21
bl _p_15
.word 0xf9411ba0
.word 0xaa0003f6
.loc 3 681 0
.word 0xd2800a1e
.word 0x6b1e02ff
.word 0x54000288
.word 0x51010ef5
.word 0xd28000be
.word 0x6b1e02bf
.word 0x54000122
.word 0xd37df2a0
.word 0x2a0003e1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #224]
.word 0x8b010000
.word 0xf9400000
.word 0xd61f0000
.word 0xd28009de
.word 0x6b1e02ff
.word 0x54000f80
.word 0xd2800a1e
.word 0x6b1e02ff
.word 0x54002aa1
.word 0x14000119
.word 0x51018ef5
.word 0xd28000be
.word 0x6b1e02bf
.word 0x54000122
.word 0xd37df2a0
.word 0x2a0003e1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #232]
.word 0x8b010000
.word 0xf9400000
.word 0xd61f0000
.word 0xd2800dde
.word 0x6b1e02ff
.word 0x54000d20
.word 0xd2800e1e
.word 0x6b1e02ff
.word 0x54002841
.word 0x14000106
.loc 3 686 0
.word 0x6b1f031f
.word 0x5400008a
.word 0xf940033e
.word 0xb980b33a
.word 0x14000002
.word 0xaa1803fa
.word 0xaa1a03f5
.loc 3 687 0
.word 0x6b1f031f
.word 0x5400006a
.loc 3 688 0
.word 0xf940033e
.word 0xb980b338
.loc 3 690 0
.word 0xf94027a0
.word 0xf94027a1
.word 0xf9400022
.word 0xf9010fa2
.word 0xf9400422
.word 0xf90113a2
.word 0xf9400821
.word 0xf90117a1
.word 0xb9821fa1
.word 0xb180021
bl _p_16
.loc 3 692 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf900afa1
.word 0xf9400401
.word 0xf900b3a1
.word 0xf9400800
.word 0xf900b7a0
.word 0xaa1603e0
.word 0x910503a1
.word 0xf940afa2
.word 0xf900a3a2
.word 0xf940b3a2
.word 0xf900a7a2
.word 0xf940b7a2
.word 0xf900aba2
.word 0xaa1503e2
.word 0xaa1803e3
.word 0xaa1903e4
bl _p_17
.loc 3 694 0
.word 0x14000108
.loc 3 700 0
.word 0x6b1f031f
.word 0x540000ea
.loc 3 701 0
.word 0xf940033e
.word 0xb980af21
.word 0xaa0103e0
.word 0xaa0103f5
.word 0xaa0003f8
.word 0x14000002
.loc 3 703 0
.word 0xaa1803f5
.loc 3 705 0
.word 0xf94027a0
.word 0xf94027a1
.word 0xf9400022
.word 0xf90103a2
.word 0xf9400422
.word 0xf90107a2
.word 0xf9400821
.word 0xf9010ba1
.word 0xb98207a1
.word 0xb180021
bl _p_16
.loc 3 707 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf900f7a1
.word 0xf9400401
.word 0xf900fba1
.word 0xf9400800
.word 0xf900ffa0
.word 0x3947c3a0
.word 0x340000c0
.loc 3 708 0
.word 0xf940033e
.word 0xf9401b21
.word 0xaa1603e0
.word 0xf94002de
bl _p_18
.loc 3 710 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf90097a1
.word 0xf9400401
.word 0xf9009ba1
.word 0xf9400800
.word 0xf9009fa0
.word 0xf940033e
.word 0xf9401f26
.word 0xaa1603e0
.word 0x910443a1
.word 0xf94097a2
.word 0xf9008ba2
.word 0xf9409ba2
.word 0xf9008fa2
.word 0xf9409fa2
.word 0xf90093a2
.word 0xaa1503e2
.word 0xaa1803e3
.word 0xaa1903e4
.word 0xd2800005
.word 0xd2800007
bl _p_19
.loc 3 712 0
.word 0x140000ce
.loc 3 718 0
.word 0x6b1f031f
.word 0x540000ea
.loc 3 719 0
.word 0xf940033e
.word 0xb980af21
.word 0xaa0103e0
.word 0xaa0103f5
.word 0xaa0003f8
.word 0x14000002
.loc 3 721 0
.word 0xaa1803f5
.loc 3 723 0
.word 0xf94027a0
.word 0xf94027a1
.word 0xf9400022
.word 0xf900eba2
.word 0xf9400422
.word 0xf900efa2
.word 0xf9400821
.word 0xf900f3a1
.word 0xb981d7a1
.word 0xb180021
bl _p_16
.loc 3 725 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf9007fa1
.word 0xf9400401
.word 0xf90083a1
.word 0xf9400800
.word 0xf90087a0
.word 0xaa1603e0
.word 0x910383a1
.word 0xf9407fa2
.word 0xf90073a2
.word 0xf94083a2
.word 0xf90077a2
.word 0xf94087a2
.word 0xf9007ba2
.word 0xaa1503e2
.word 0xaa1803e3
.word 0xaa1903e4
bl _p_20
.loc 3 727 0
.word 0x140000a6
.loc 3 733 0
.word 0x6b1f031f
.word 0x5400008a
.loc 3 734 0
.word 0xd28000d5
.word 0xd28000d8
.word 0x14000002
.loc 3 736 0
.word 0xaa1803f5
.loc 3 737 0
.word 0x11000718
.loc 3 739 0
.word 0xf94027a0
.word 0xaa1803e1
bl _p_16
.loc 3 741 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf900dfa1
.word 0xf9400401
.word 0xf900e3a1
.word 0xf9400800
.word 0xf900e7a0
.word 0x394703a0
.word 0x340000c0
.loc 3 742 0
.word 0xf940033e
.word 0xf9401b21
.word 0xaa1603e0
.word 0xf94002de
bl _p_18
.loc 3 744 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf90067a1
.word 0xf9400401
.word 0xf9006ba1
.word 0xf9400800
.word 0xf9006fa0
.word 0xaa1603e0
.word 0x9102c3a1
.word 0xf94067a2
.word 0xf9005ba2
.word 0xf9406ba2
.word 0xf9005fa2
.word 0xf9406fa2
.word 0xf90063a2
.word 0xaa1503e2
.word 0xaa1803e3
.word 0xaa1903e4
.word 0xaa1703e5
bl _p_21
.loc 3 746 0
.word 0x14000079
.loc 3 752 0
.word 0xd2800034
.loc 3 753 0
.word 0xd280003e
.word 0x6b1e031f
.word 0x540002ca
.loc 3 755 0
.word 0x3400013a
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e031f
.word 0x540000a1
.loc 3 759 0
.word 0xd28003b5
.word 0xd28003b8
.loc 3 760 0
.word 0xd2800014
.loc 3 761 0
.word 0x1400000e
.loc 3 765 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf900d3a1
.word 0xf9400401
.word 0xf900d7a1
.word 0xf9400800
.word 0xf900dba0
.word 0xb981a3a1
.word 0xaa0103e0
.word 0xaa0103f5
.word 0xaa0003f8
.loc 3 767 0
.word 0x14000002
.loc 3 769 0
.word 0xaa1803f5
.loc 3 771 0
.word 0x340000b4
.loc 3 772 0
.word 0xf94027a0
.word 0xaa1803e1
bl _p_16
.word 0x14000008
.loc 3 775 0
.word 0x340000fa
.word 0xf94027a0
.word 0xf9400800
.word 0x79400000
.word 0x35000060
.loc 3 778 0
.word 0xf94027a0
.word 0x3900201f
.loc 3 782 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf900c7a1
.word 0xf9400401
.word 0xf900cba1
.word 0xf9400800
.word 0xf900cfa0
.word 0x394643a0
.word 0x340000c0
.loc 3 783 0
.word 0xf940033e
.word 0xf9401b21
.word 0xaa1603e0
.word 0xf94002de
bl _p_18
.loc 3 785 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf9004fa1
.word 0xf9400401
.word 0xf90053a1
.word 0xf9400800
.word 0xf90057a0
.word 0x51000ae0
.word 0x53003c05
.word 0x6b1f029f
.word 0x9a9f17e6
.word 0xaa1603e0
.word 0x910203a1
.word 0xf9404fa2
.word 0xf90043a2
.word 0xf94053a2
.word 0xf90047a2
.word 0xf94057a2
.word 0xf9004ba2
.word 0xaa1503e2
.word 0xaa1803e3
.word 0xaa1903e4
bl _p_22
.loc 3 787 0
.word 0x1400002d
.loc 3 793 0
.word 0x6b1f031f
.word 0x540000ea
.loc 3 794 0
.word 0xf940033e
.word 0xb980cb21
.word 0xaa0103e0
.word 0xaa0103f5
.word 0xaa0003f8
.word 0x14000002
.loc 3 796 0
.word 0xaa1803f5
.loc 3 797 0
.word 0xf94027a1
.word 0x91001020
.word 0xb9800421
.word 0x11000821
.word 0xb9000001
.loc 3 799 0
.word 0xf94027a0
.word 0xf94027a1
.word 0xf9400022
.word 0xf900bba2
.word 0xf9400422
.word 0xf900bfa2
.word 0xf9400821
.word 0xf900c3a1
.word 0xb98177a1
.word 0xb180021
bl _p_16
.loc 3 801 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf90037a1
.word 0xf9400401
.word 0xf9003ba1
.word 0xf9400800
.word 0xf9003fa0
.word 0xaa1603e0
.word 0x910143a1
.word 0xf94037a2
.word 0xf9002ba2
.word 0xf9403ba2
.word 0xf9002fa2
.word 0xf9403fa2
.word 0xf90033a2
.word 0xaa1503e2
.word 0xaa1803e3
.word 0xaa1903e4
bl _p_23
.loc 3 810 0
.word 0xaa1603e0
.word 0xf94002c1
.word 0xf9402030
.word 0xd63f0200
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0xf94023ba
.word 0x910003bf
.word 0xa9407bfd
.word 0xd2804810
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0
.loc 3 807 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2800021
bl _p_24
.word 0xaa0003e1
.word 0xd28017a0
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25

Lme_b:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo:
.loc 3 815 0 prologue_end
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xf90023ba
.word 0xaa0003f7
.word 0xf90027a1
.word 0xaa0203f8
.word 0xaa0303f9
.word 0xaa0403fa
.word 0xf94027a0
.word 0xf9400001
.word 0xf90037a1
.word 0xf9400401
.word 0xf9003ba1
.word 0xf9400800
.word 0xf9003fa0
.word 0x3941c3a0
.word 0x35000220

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #240]
.word 0xf9400000
.word 0xf940035e
.word 0xb980b741
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000b89
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400016
.word 0x14000010

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #248]
.word 0xf9400000
.word 0xf940035e
.word 0xb980bb41
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000989
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400016
.loc 3 819 0
.word 0xaa1603f5
.word 0xd2800016
.word 0x1400003b
.word 0x93407ec0
.word 0xb98012a1
.word 0xeb00003f
.word 0x10000011
.word 0x54000809
.word 0xd37ff800
.word 0x8b150000
.word 0x79402814
.loc 3 821 0
.word 0xaa1403e0
.word 0xd280047e
.word 0x6b1e001f
.word 0x54000100
.word 0xd280049e
.word 0x6b1e029f
.word 0x54000420
.word 0xd28005be
.word 0x6b1e029f
.word 0x54000300
.word 0x14000023
.loc 3 824 0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_26
.word 0xaa0003e5
.word 0xf940035e
.word 0xf9402b46
.word 0xf940035e
.word 0xf9402747
.word 0xaa1703e0
.word 0xf94027a2
.word 0x910143a1
.word 0xf9400043
.word 0xf9002ba3
.word 0xf9400443
.word 0xf9002fa3
.word 0xf9400842
.word 0xf90033a2
.word 0xaa1803e2
.word 0xaa1903e3
.word 0xaa1a03e4
bl _p_19
.loc 3 825 0
.word 0x14000011
.loc 3 827 0
.word 0xf940035e
.word 0xf9401b41
.word 0xaa1703e0
.word 0xf94002fe
bl _p_18
.loc 3 828 0
.word 0x1400000b
.loc 3 830 0
.word 0xf940035e
.word 0xf9402f41
.word 0xaa1703e0
.word 0xf94002fe
bl _p_18
.loc 3 831 0
.word 0x14000005
.loc 3 833 0
.word 0xaa1703e0
.word 0xaa1403e1
.word 0xf94002fe
bl _p_11
.word 0x110006d6
.loc 3 819 0
.word 0xb98012a0
.word 0x6b0002df
.word 0x54fff88b
.loc 3 837 0
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0xf94023ba
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_c:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_wcslen_char_
System_Globalization_FormatProvider_Number_wcslen_char_:
.loc 3 841 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa
.word 0xd2800019
.word 0x14000002
.loc 3 843 0
.word 0x11000739
.loc 3 842 0
.word 0xaa1a03e0
.word 0x91000b5a
.word 0x79400000
.word 0x35ffff80
.loc 3 844 0
.word 0xaa1903e0
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_d:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string
System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string:
.loc 3 849 0 prologue_end
.word 0xa9b27bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xf90033a2
.word 0xf90037a3
.word 0xf9003ba4
.word 0xaa0503f8
.word 0xaa0603f9
.word 0xaa0703fa
.word 0xf9402fa0
.word 0xf9400001
.word 0xf9003fa1
.word 0xf9400401
.word 0xf90043a1
.word 0xf9400800
.word 0xf90047a0
.word 0xb9807fb5
.loc 3 850 0
.word 0xf9402fa0
.word 0xf9400800
.word 0xf9004ba0
.loc 3 851 0
.word 0xaa0003f3
.word 0xb9009bbf
.word 0x14000004
.word 0xb9809ba0
.word 0x11000400
.word 0xb9009ba0
.word 0xaa1303e0
.word 0x91000a73
.word 0x79400000
.word 0x35ffff40
.word 0xb9809bb3
.loc 3 853 0
.word 0x6b1f02bf
.word 0x54001bed
.loc 3 855 0
.word 0xb4001858
.loc 3 857 0
.word 0xb9009bbf
.loc 3 858 0
.word 0xd2800000
.word 0x93407c00
.word 0xb9801b01
.word 0xeb00003f
.word 0x10000011
.word 0x54002369
.word 0xd37ef400
.word 0x8b000300
.word 0x91008000
.word 0xb9800017
.loc 3 859 0
.word 0xb9801b00
.word 0xb900a3a0
.loc 3 860 0
.word 0xb900abb5
.loc 3 861 0
.word 0xb9801341
.word 0xb900b3a1
.loc 3 862 0
.word 0xb900bbbf
.loc 3 865 0
.word 0x340006c0
.word 0x1400002a
.loc 3 869 0
.word 0xb9809ba0
.word 0x93407c00
.word 0xb9801b01
.word 0xeb00003f
.word 0x10000011
.word 0x54002129
.word 0xd37ef400
.word 0x8b000300
.word 0x91008000
.word 0xb9800000
.word 0xb900bba0
.loc 3 870 0
.word 0x34000400
.loc 3 873 0
.word 0xb980aba0
.word 0xb980b3a1
.word 0xb010000
.word 0xb900aba0
.loc 3 874 0
.word 0xb980a3a0
.word 0x51000401
.word 0xb9809ba0
.word 0x6b01001f
.word 0x5400008a
.loc 3 875 0
.word 0xb9809ba0
.word 0x11000400
.word 0xb9009ba0
.loc 3 877 0
.word 0xb9809ba0
.word 0x93407c00
.word 0xb9801b01
.word 0xeb00003f
.word 0x10000011
.word 0x54001e29
.word 0xd37ef400
.word 0x8b000300
.word 0x91008000
.word 0xb9800000
.word 0xb0002f7
.loc 3 878 0
.word 0xaa1703e0
.word 0x6b1f001f
.word 0x54001cab
.word 0xb980aba0
.word 0x6b1f001f
.word 0x54001c4b
.loc 3 867 0
.word 0x6b1702bf
.word 0x54fffacc
.loc 3 881 0
.word 0x35000077
.loc 3 882 0
.word 0xb900bbbf
.word 0x14000007
.loc 3 884 0
.word 0xb9801b00
.word 0xeb1f001f
.word 0x10000011
.word 0x54001ba9
.word 0xb9802300
.word 0xb900bba0
.loc 3 887 0
.word 0xb980aba0
.word 0x2a0003e0
.word 0xd2800041
.word 0x93407c21
bl _p_27
.word 0xaa0003f7
.word 0xb5000077
.word 0xd2800016
.word 0x1400000f
.word 0x91003ef0
.word 0x928001f1
.word 0xf2bffff1
.word 0x8a110210
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0x8b100230
.word 0xeb10023f
.word 0x54000080
.word 0xa9007e3f
.word 0x91004231
.word 0x17fffffc
.word 0x910003f6
.word 0xaa1603f7
.loc 3 888 0
.word 0xb9009bbf
.loc 3 889 0
.word 0xd2800016
.loc 3 891 0
.word 0x6b1302bf
.word 0x5400004b
.word 0x14000002
.word 0xaa1503f3
.word 0xb900c3b3
.loc 3 892 0
.word 0xb980aba0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0xd1000800
.word 0xf90067a0
.loc 3 893 0
.word 0x510006b4
.word 0x14000043
.loc 3 895 0
.word 0xf94067a0
.word 0xf94067a1
.word 0xd1000821
.word 0xf90067a1
.word 0xf9006ba0
.word 0xb980c3a0
.word 0x6b00029f
.word 0x5400006b
.word 0xd2800613
.word 0x14000007
.word 0x93407e80
.word 0xd2800041
.word 0x9b017c01
.word 0xf9404ba0
.word 0x8b010000
.word 0x79400013
.word 0xf9406ba0
.word 0x79000013
.loc 3 897 0
.word 0xb980bba0
.word 0x6b1f001f
.word 0x540005ad
.loc 3 899 0
.word 0x110006d6
.loc 3 900 0
.word 0xaa1603e0
.word 0xb980bba1
.word 0x6b01001f
.word 0x54000501
.word 0x340004f4
.loc 3 902 0
.word 0xb980b3a0
.word 0x51000416
.word 0x1400000f
.loc 3 903 0
.word 0xf94067a0
.word 0xf94067a1
.word 0xd1000821
.word 0xf90067a1
.word 0x93407ec1
.word 0xb9801342
.word 0xeb01005f
.word 0x10000011
.word 0x54001189
.word 0xd37ff821
.word 0x8b1a0021
.word 0x79402821
.word 0x79000001
.loc 3 902 0
.word 0x510006d6
.word 0x6b1f02df
.word 0x54fffe2a
.loc 3 905 0
.word 0xb980a3a0
.word 0x51000401
.word 0xb9809ba0
.word 0x6b01001f
.word 0x540001ca
.loc 3 907 0
.word 0xb9809ba0
.word 0x11000400
.word 0xb9009ba0
.loc 3 908 0
.word 0x93407c00
.word 0xb9801b01
.word 0xeb00003f
.word 0x10000011
.word 0x54000f09
.word 0xd37ef400
.word 0x8b000300
.word 0x91008000
.word 0xb9800000
.word 0xb900bba0
.loc 3 910 0
.word 0xd2800016
.loc 3 893 0
.word 0x51000694
.word 0x6b1f029f
.word 0x54fff7aa
.loc 3 915 0
.word 0xf9402ba0
.word 0xaa1703e1
.word 0xb980aba2
.word 0xf9402ba3
.word 0xf940007e
bl _p_28
.loc 3 916 0
.word 0xb980c3a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf9404ba0
.word 0x8b010000
.word 0xf9004ba0
.loc 3 917 0
.word 0x14000022
.loc 3 920 0
.word 0xaa1303fa
.word 0xaa1503f8
.word 0x6b15027f
.word 0x5400004d
.word 0x14000002
.word 0xaa1a03f8
.word 0xaa1803fa
.loc 3 921 0
.word 0xf9402ba0
.word 0xf9404ba1
.word 0xaa1803e2
.word 0xf9402ba3
.word 0xf940007e
bl _p_28
.loc 3 922 0
.word 0x93407f00
.word 0xd2800041
.word 0x9b017c01
.word 0xf9404ba0
.word 0x8b010000
.word 0xf9004ba0
.loc 3 923 0
.word 0x6b1302bf
.word 0x540001ad
.loc 3 924 0
.word 0x4b1302a2
.word 0xf9402ba0
.word 0xd2800601
.word 0xf9402ba3
.word 0xf940007e
bl _p_29
.loc 3 926 0
.word 0x14000006
.loc 3 929 0
.word 0xf9402ba0
.word 0xd2800601
.word 0xf9402ba2
.word 0xf940005e
bl _p_11
.loc 3 932 0
.word 0xb9806ba0
.word 0x6b1f001f
.word 0x5400064d
.loc 3 934 0
.word 0xf9402ba0
.word 0xaa1903e1
.word 0xf9402ba2
.word 0xf940005e
bl _p_18
.loc 3 935 0
.word 0x6b1f02bf
.word 0x5400050a
.word 0xb9806ba0
.word 0x6b1f001f
.word 0x540004ad
.loc 3 937 0
.word 0x4b1503fa
.word 0xb9806bb9
.word 0x6b19035f
.word 0x5400004d
.word 0x14000002
.word 0xaa1a03f9
.word 0xaa1903fa
.loc 3 938 0
.word 0xf9402ba0
.word 0xd2800601
.word 0xaa1903e2
.word 0xf9402ba3
.word 0xf940007e
bl _p_29
.loc 3 939 0
.word 0xb1902b5
.loc 3 940 0
.word 0xb9806ba0
.word 0x4b190000
.word 0xb9006ba0
.word 0x14000013
.loc 3 945 0
.word 0xf9404ba0
.word 0x79400000
.word 0xf9402bba
.word 0x35000060
.word 0xd2800613
.word 0x14000006
.word 0xf9404ba0
.word 0xf9404ba1
.word 0x91000821
.word 0xf9004ba1
.word 0x79400013
.word 0xaa1a03e0
.word 0xaa1303e1
.word 0xf940035e
bl _p_11
.loc 3 946 0
.word 0xb9806ba0
.word 0x51000400
.word 0xb9006ba0
.loc 3 943 0
.word 0xb9806ba0
.word 0x6b1f001f
.word 0x54fffd8c
.loc 3 949 0
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8ce7bfd
.word 0xd65f03c0
.loc 3 879 0
.word 0xd2801180
.word 0xf2a04000
bl _mono_create_corlib_exception_0
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_e:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo:
.loc 3 953 0 prologue_end
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xf90023ba
.word 0xaa0003f7
.word 0xf90027a1
.word 0xaa0203f8
.word 0xaa0303f9
.word 0xaa0403fa
.word 0xf94027a0
.word 0xf9400001
.word 0xf90037a1
.word 0xf9400401
.word 0xf9003ba1
.word 0xf9400800
.word 0xf9003fa0
.word 0x3941c3a0
.word 0x350000c0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #256]
.word 0xf9400016
.word 0x14000010

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #264]
.word 0xf9400000
.word 0xf940035e
.word 0xb980bf41
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000869
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400016
.loc 3 957 0
.word 0xaa1603f5
.word 0xd2800016
.word 0x14000032
.word 0x93407ec0
.word 0xb98012a1
.word 0xeb00003f
.word 0x10000011
.word 0x540006e9
.word 0xd37ff800
.word 0x8b150000
.word 0x79402814
.loc 3 959 0
.word 0xaa1403e0
.word 0xd280047e
.word 0x6b1e001f
.word 0x540000a0
.word 0xd28005be
.word 0x6b1e029f
.word 0x54000300
.word 0x1400001d
.loc 3 962 0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_30
.word 0xaa0003e5
.word 0xf940035e
.word 0xf9401f46
.word 0xf940035e
.word 0xf9402347
.word 0xaa1703e0
.word 0xf94027a2
.word 0x910143a1
.word 0xf9400043
.word 0xf9002ba3
.word 0xf9400443
.word 0xf9002fa3
.word 0xf9400842
.word 0xf90033a2
.word 0xaa1803e2
.word 0xaa1903e3
.word 0xaa1a03e4
bl _p_19
.loc 3 963 0
.word 0x1400000b
.loc 3 965 0
.word 0xf940035e
.word 0xf9401b41
.word 0xaa1703e0
.word 0xf94002fe
bl _p_18
.loc 3 966 0
.word 0x14000005
.loc 3 968 0
.word 0xaa1703e0
.word 0xaa1403e1
.word 0xf94002fe
bl _p_11
.word 0x110006d6
.loc 3 957 0
.word 0xb98012a0
.word 0x6b0002df
.word 0x54fff9ab
.loc 3 972 0
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0xf94023ba
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_f:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char
System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char:
.loc 3 976 0 prologue_end
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xaa0003f7
.word 0xf90023a1
.word 0xf90027a2
.word 0xaa0303f8
.word 0xaa0403f9
.word 0xf9002ba5
.word 0xf94023a0
.word 0xf9400816
.loc 3 978 0
.word 0x794002c0
.word 0xaa1703f5
.word 0x35000060
.word 0xd2800614
.word 0x14000004
.word 0xaa1603e0
.word 0x91000ad6
.word 0x79400014
.word 0xaa1503e0
.word 0xaa1403e1
.word 0xf94002be
bl _p_11
.loc 3 980 0
.word 0xd280003e
.word 0x6b1e031f
.word 0x54000260
.loc 3 981 0
.word 0xf940033e
.word 0xf9401f21
.word 0xaa1703e0
.word 0xf94002fe
bl _p_18
.word 0x1400000d
.loc 3 984 0
.word 0x794002c0
.word 0xaa1703f5
.word 0x35000060
.word 0xd2800614
.word 0x14000004
.word 0xaa1603e0
.word 0x91000ad6
.word 0x79400014
.word 0xaa1503e0
.word 0xaa1403e1
.word 0xf94002be
bl _p_11
.loc 3 983 0
.word 0x51000701
.word 0xaa0103e0
.word 0xaa0103f8
.word 0x6b1f001f
.word 0x54fffe0c
.loc 3 986 0
.word 0xf94023a0
.word 0xf9400800
.word 0x79400000
.word 0x34000160
.word 0xf94023a0
.word 0xf9400001
.word 0xf9002fa1
.word 0xf9400401
.word 0xf90033a1
.word 0xf9400800
.word 0xf90037a0
.word 0xb9805fa0
.word 0x51000418
.word 0x14000002
.word 0xd2800018
.word 0xaa1803f6
.loc 3 987 0
.word 0xaa1703e0
.word 0xaa1903e1
.word 0xaa1803e2
.word 0x7940a3a3
.word 0xd2800064
.word 0xd2800025
bl _p_31
.loc 3 988 0
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_10:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool
System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool:
.loc 3 992 0 prologue_end
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003f5
.word 0xaa0103f6
.word 0xaa0203f7
.word 0xaa0303f8
.word 0xaa0403f9
.word 0xaa0503fa
.word 0xb90043bf
.word 0xaa1503e0
.word 0xaa1803e1
.word 0xf94002be
bl _p_11
.loc 3 994 0
.word 0x6b1f02ff
.word 0x5400010a
.loc 3 996 0
.word 0xf94002de
.word 0xf9401ac1
.word 0xaa1503e0
.word 0xf94002be
bl _p_18
.loc 3 997 0
.word 0x4b1703f7
.loc 3 998 0
.word 0x14000007
.loc 3 1001 0
.word 0x340000da
.loc 3 1002 0
.word 0xf94002de
.word 0xf94016c1
.word 0xaa1503e0
.word 0xf94002be
bl _p_18
.loc 3 1005 0
.word 0xd2800160
.word 0x2a0003e0
.word 0xd2800041
.word 0x93407c21
bl _p_27
.word 0xaa0003fa
.word 0xb500007a
.word 0xd2800018
.word 0x1400000f
.word 0x91003f50
.word 0x928001f1
.word 0xf2bffff1
.word 0x8a110210
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0x8b100230
.word 0xeb10023f
.word 0x54000080
.word 0xa9007e3f
.word 0x91004231
.word 0x17fffffc
.word 0x910003f8
.word 0xaa1803fa
.loc 3 1006 0
.word 0xd280015e
.word 0xb90043be
.loc 3 1007 0
.word 0x910103a1
.word 0xaa1803e0
.word 0xaa1703e2
.word 0xaa1903e3
bl _p_32
.loc 3 1008 0
.word 0xd2800140
.word 0xb98043a1
.word 0x4b010019
.word 0x1400000d
.loc 3 1010 0
.word 0xb98043a1
.word 0xaa0103e0
.word 0x11000421
.word 0xb90043a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b000340
.word 0x79400001
.word 0xaa1503e0
.word 0xf94002be
bl _p_11
.loc 3 1009 0
.word 0x51000721
.word 0xaa0103e0
.word 0xaa0103f9
.word 0x6b1f001f
.word 0x54fffe0a
.loc 3 1011 0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_11:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool
System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool:
.loc 3 1015 0 prologue_end
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xf90023ba
.word 0xaa0003f6
.word 0xf90027a1
.word 0xf9002ba2
.word 0xaa0303f7
.word 0xaa0403f8
.word 0xf9002fa5
.word 0xaa0603fa
.word 0xf94027a0
.word 0xf9400001
.word 0xf9003fa1
.word 0xf9400401
.word 0xf90043a1
.word 0xf9400800
.word 0xf90047a0
.word 0xb9807fb5
.loc 3 1016 0
.word 0xd2800014
.loc 3 1018 0
.word 0x3500013a
.loc 3 1021 0
.word 0x6b1702bf
.word 0x540000ac
.word 0x9280005e
.word 0xf2bffffe
.word 0x6b1e02bf
.word 0x5400006a
.loc 3 1023 0
.word 0xd2800035
.loc 3 1024 0
.word 0xd2800034
.loc 3 1028 0
.word 0xf94027a0
.word 0xf940081a
.loc 3 1030 0
.word 0x6b1f02bf
.word 0x5400026d
.loc 3 1034 0
.word 0x79400340
.word 0xaa1603f7
.word 0x35000060
.word 0xd2800613
.word 0x14000004
.word 0xaa1a03e0
.word 0x91000b5a
.word 0x79400013
.word 0xaa1703e0
.word 0xaa1303e1
.word 0xf94002fe
bl _p_11
.loc 3 1035 0
.word 0x510006a1
.word 0xaa0103e0
.word 0xaa0103f5
.word 0x6b1f001f
.word 0x54fffe0c
.word 0x14000005
.loc 3 1039 0
.word 0xaa1603e0
.word 0xd2800601
.word 0xf94002de
bl _p_11
.loc 3 1042 0
.word 0x79400340
.word 0x35000060
.word 0x6b1f02bf
.word 0x540002ea
.loc 3 1044 0
.word 0xf940031e
.word 0xf9401f01
.word 0xaa1603e0
.word 0xf94002de
bl _p_18
.word 0x14000006
.loc 3 1048 0
.word 0xaa1603e0
.word 0xd2800601
.word 0xf94002de
bl _p_11
.loc 3 1049 0
.word 0x110006b5
.loc 3 1046 0
.word 0x6b1f02bf
.word 0x54ffff4b
.word 0x14000007
.loc 3 1053 0
.word 0xaa1a03e0
.word 0x91000b5a
.word 0x79400001
.word 0xaa1603e0
.word 0xf94002de
bl _p_11
.loc 3 1052 0
.word 0x79400340
.word 0x35ffff20
.loc 3 1056 0
.word 0x34000214
.loc 3 1057 0
.word 0xf94027a0
.word 0xf9400001
.word 0xf90033a1
.word 0xf9400401
.word 0xf90037a1
.word 0xf9400800
.word 0xf9003ba0
.word 0xb98067a0
.word 0x51000402
.word 0xaa1603e0
.word 0xaa1803e1
.word 0x7940b3a3
.word 0xd2800044
.word 0xd2800025
bl _p_31
.loc 3 1058 0
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xf94023ba
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0

Lme_12:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo:
.loc 3 1062 0 prologue_end
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xf90023ba
.word 0xaa0003f7
.word 0xf90027a1
.word 0xaa0203f8
.word 0xaa0303f9
.word 0xaa0403fa
.word 0xf94027a0
.word 0xf9400001
.word 0xf90037a1
.word 0xf9400401
.word 0xf9003ba1
.word 0xf9400800
.word 0xf9003fa0
.word 0x3941c3a0
.word 0x35000220

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #272]
.word 0xf9400000
.word 0xf940035e
.word 0xb980c341
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000b89
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400016
.word 0x14000010

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #280]
.word 0xf9400000
.word 0xf940035e
.word 0xb980c741
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000989
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400016
.loc 3 1066 0
.word 0xaa1603f5
.word 0xd2800016
.word 0x1400003b
.word 0x93407ec0
.word 0xb98012a1
.word 0xeb00003f
.word 0x10000011
.word 0x54000809
.word 0xd37ff800
.word 0x8b150000
.word 0x79402814
.loc 3 1068 0
.word 0xaa1403e0
.word 0xd280047e
.word 0x6b1e001f
.word 0x54000100
.word 0xd28004be
.word 0x6b1e029f
.word 0x54000420
.word 0xd28005be
.word 0x6b1e029f
.word 0x54000300
.word 0x14000023
.loc 3 1071 0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_33
.word 0xaa0003e5
.word 0xf940035e
.word 0xf9404346
.word 0xf940035e
.word 0xf9404747
.word 0xaa1703e0
.word 0xf94027a2
.word 0x910143a1
.word 0xf9400043
.word 0xf9002ba3
.word 0xf9400443
.word 0xf9002fa3
.word 0xf9400842
.word 0xf90033a2
.word 0xaa1803e2
.word 0xaa1903e3
.word 0xaa1a03e4
bl _p_19
.loc 3 1072 0
.word 0x14000011
.loc 3 1074 0
.word 0xf940035e
.word 0xf9401b41
.word 0xaa1703e0
.word 0xf94002fe
bl _p_18
.loc 3 1075 0
.word 0x1400000b
.loc 3 1077 0
.word 0xf940035e
.word 0xf9404b41
.word 0xaa1703e0
.word 0xf94002fe
bl _p_18
.loc 3 1078 0
.word 0x14000005
.loc 3 1080 0
.word 0xaa1703e0
.word 0xaa1403e1
.word 0xf94002fe
bl _p_11
.word 0x110006d6
.loc 3 1066 0
.word 0xb98012a0
.word 0x6b0002df
.word 0x54fff88b
.loc 3 1084 0
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0xf94023ba
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_13:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int
System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int:
.loc 3 1088 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xf9400b38
.loc 3 1090 0
.word 0xd2800017
.word 0x14000002
.loc 3 1092 0
.word 0x110006f7
.loc 3 1091 0
.word 0x6b1a02ff
.word 0x540000ea
.word 0x93407ee0
.word 0xd2800041
.word 0x9b017c00
.word 0x8b000300
.word 0x79400000
.word 0x35ffff00
.loc 3 1094 0
.word 0x6b1a02ff
.word 0x54000541
.word 0x93407ee0
.word 0xd2800041
.word 0x9b017c00
.word 0x8b000300
.word 0x79400000
.word 0xd28006be
.word 0x6b1e001f
.word 0x5400044b
.word 0x14000002
.loc 3 1097 0
.word 0x510006f7
.loc 3 1096 0
.word 0x6b1f02ff
.word 0x5400014d
.word 0x510006e0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b000300
.word 0x79400000
.word 0xd280073e
.word 0x6b1e001f
.word 0x54fffea0
.loc 3 1099 0
.word 0x6b1f02ff
.word 0x5400014d
.loc 3 1101 0
.word 0x510006e0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b000300
.word 0x79400001
.word 0x11000421
.word 0x79000001
.loc 3 1102 0
.word 0x14000015
.loc 3 1105 0
.word 0x91001320
.word 0xb9800721
.word 0x11000421
.word 0xb9000001
.loc 3 1106 0
.word 0xd280063e
.word 0x7900031e
.loc 3 1107 0
.word 0xd2800037
.loc 3 1109 0
.word 0x1400000d
.loc 3 1113 0
.word 0x510006f7
.loc 3 1112 0
.word 0x6b1f02ff
.word 0x5400014d
.word 0x510006e0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b000300
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54fffea0
.loc 3 1115 0
.word 0x35000077
.loc 3 1117 0
.word 0xb900073f
.loc 3 1118 0
.word 0x3900233f
.loc 3 1120 0
.word 0x93407ee0
.word 0xd2800041
.word 0x9b017c00
.word 0x8b000300
.word 0x7900001f
.loc 3 1121 0
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_14:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_FindSection_string_int
System_Globalization_FormatProvider_Number_FindSection_string_int:
.loc 3 1128 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003f9
.word 0xaa0103fa
.word 0x3500007a
.loc 3 1129 0
.word 0xd2800000
.word 0x1400004f
.word 0xaa1903f8
.loc 3 1131 0
.word 0xaa1903f7
.word 0xb4000059
.word 0x910052f7
.loc 3 1133 0
.word 0xd2800019
.loc 3 1136 0
.word 0xaa1903e0
.word 0x11000739
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400001
.word 0xaa0103e0
.word 0xaa0103f8
.word 0xaa0003f6
.word 0xd280045e
.word 0x6b1e001f
.word 0x540000c8
.word 0x34000776
.word 0xd280045e
.word 0x6b1e02df
.word 0x54000180
.word 0x17ffffef
.word 0xd28004fe
.word 0x6b1e02df
.word 0x54000100
.word 0xd280077e
.word 0x6b1e02df
.word 0x540003a0
.word 0xd2800b9e
.word 0x6b1e02df
.word 0x54000240
.word 0x17ffffe5
.loc 3 1140 0
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0x34fffbe0
.word 0xaa1903e0
.word 0x11000739
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0x6b18001f
.word 0x54fffe41
.word 0x17ffffd5
.loc 3 1144 0
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0x34fff9e0
.loc 3 1145 0
.word 0x11000739
.loc 3 1146 0
.word 0x17ffffcd
.loc 3 1148 0
.word 0x51000741
.word 0xaa0103e0
.word 0xaa0103fa
.word 0x35fff920
.loc 3 1150 0
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0x34000160
.word 0x93407f20
.word 0xd2800041
.word 0x9b017c00
.word 0x8b0002e0
.word 0x79400000
.word 0xd280077e
.word 0x6b1e001f
.word 0x54000060
.loc 3 1151 0
.word 0xaa1903e0
.word 0x14000002
.loc 3 1154 0
.word 0xd2800000
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_15:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo
System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo:
.loc 3 1169 0 prologue_end
.word 0xa9a47bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xf90033a2
.word 0xf9008bbf
.word 0xd2800018
.loc 3 1176 0
.word 0xf9402ba0
.word 0xf9400800
.word 0xf9008fa0
.loc 3 1179 0
.word 0x79400000
.word 0xf9402fb6
.word 0x340001c0
.word 0xf9402ba0
.word 0xf9400001
.word 0xf9007fa1
.word 0xf9400401
.word 0xf90083a1
.word 0xf9400800
.word 0xf90087a0
.word 0x394403a0
.word 0x35000060
.word 0xd2800015
.word 0x14000004
.word 0xd2800035
.word 0x14000002
.word 0xd2800055
.word 0xaa1603e0
.word 0xaa1503e1
bl _p_34
.word 0x93407c00
.word 0xb90123a0
.loc 3 1183 0
.word 0xd2800014
.loc 3 1184 0
.word 0x92800013
.word 0xf2bffff3
.loc 3 1185 0
.word 0xd29ffffe
.word 0xf2affffe
.word 0xb9012bbe
.loc 3 1186 0
.word 0xb90133bf
.loc 3 1187 0
.word 0x3904e3bf
.loc 3 1188 0
.word 0x92800015
.word 0xf2bffff5
.loc 3 1189 0
.word 0x390503bf
.loc 3 1190 0
.word 0xb9014bbf
.loc 3 1191 0
.word 0xb98123a0
.word 0xb90153a0
.word 0xf9402fba
.loc 3 1193 0
.word 0xf9402fa0
.word 0xf900afa0
.word 0xf9402fa0
.word 0xb40015a0
.word 0xf940afa0
.word 0x91005000
.word 0xf900afa0
.word 0x140000a9
.loc 3 1197 0
.word 0xd28008be
.word 0x6b1e035f
.word 0x540003a8
.word 0x51008b59
.word 0xd28000de
.word 0x6b1e033f
.word 0x54000122
.word 0xd37df320
.word 0x2a0003e1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #288]
.word 0x8b010000
.word 0xf9400000
.word 0xd61f0000
.word 0x5100b359
.word 0xd28000be
.word 0x6b1e033f
.word 0x54000122
.word 0xd37df320
.word 0x2a0003e1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #296]
.word 0x8b010000
.word 0xf9400000
.word 0xd61f0000
.word 0xd28008be
.word 0x6b1e035f
.word 0x54000a60
.word 0x1400008a
.word 0xd2800b9e
.word 0x6b1e035f
.word 0x54000860
.word 0xd2800cbe
.word 0x6b1e035f
.word 0x54000980
.word 0xd284061e
.word 0x6b1e035f
.word 0x54000480
.word 0x14000080
.loc 3 1200 0
.word 0x11000694
.loc 3 1201 0
.word 0x1400007e
.loc 3 1203 0
.word 0xb9812ba0
.word 0xd29ffffe
.word 0xf2affffe
.word 0x6b1e001f
.word 0x54000041
.loc 3 1204 0
.word 0xb9012bb4
.loc 3 1205 0
.word 0x11000694
.loc 3 1206 0
.word 0xb90133b4
.loc 3 1207 0
.word 0x14000075
.loc 3 1209 0
.word 0x6b1f027f
.word 0x54000e6a
.loc 3 1210 0
.word 0xaa1403f3
.loc 3 1211 0
.word 0x14000071
.loc 3 1213 0
.word 0x6b1f029f
.word 0x54000ded
.word 0x6b1f027f
.word 0x54000daa
.loc 3 1215 0
.word 0x6b1f02bf
.word 0x540000eb
.loc 3 1217 0
.word 0x6b1402bf
.word 0x54000061
.loc 3 1219 0
.word 0x11000718
.loc 3 1220 0
.word 0x14000067
.loc 3 1222 0
.word 0xd280003e
.word 0x390503be
.loc 3 1224 0
.word 0xaa1403f5
.loc 3 1225 0
.word 0xd2800038
.loc 3 1227 0
.word 0x14000062
.loc 3 1229 0
.word 0xb9814ba0
.word 0x11000800
.word 0xb9014ba0
.loc 3 1230 0
.word 0x1400005e
.loc 3 1232 0
.word 0xb9814ba0
.word 0x11000c00
.word 0xb9014ba0
.loc 3 1233 0
.word 0x1400005a
.loc 3 1236 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400000
.word 0x34000a40
.word 0xb98153a0
.word 0xb98153a1
.word 0x11000421
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400000
.word 0x6b1a001f
.word 0x54fffda1
.word 0x14000045
.loc 3 1240 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400000
.word 0x340007a0
.loc 3 1241 0
.word 0xb98153a0
.word 0x11000400
.word 0xb90153a0
.loc 3 1242 0
.word 0x14000039
.loc 3 1245 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54000400
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400000
.word 0xd280057e
.word 0x6b1e001f
.word 0x54000160
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400000
.word 0xd28005be
.word 0x6b1e001f
.word 0x54000361
.word 0xb98153a0
.word 0x11000400
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54000201
.loc 3 1247 0
.word 0xb98153a0
.word 0x11000401
.word 0xaa0103e0
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54fffe80
.loc 3 1249 0
.word 0xd280003e
.word 0x3904e3be
.loc 3 1195 0
.word 0xb98153a0
.word 0xb98153a1
.word 0x11000421
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940afa0
.word 0x8b010000
.word 0x79400001
.word 0xaa0103e0
.word 0xaa0103fa
.word 0x34000080
.word 0xd280077e
.word 0x6b1e035f
.word 0x54ffe921
.loc 3 1256 0
.word 0x6b1f027f
.word 0x5400004a
.loc 3 1257 0
.word 0xaa1403f3
.loc 3 1259 0
.word 0x6b1f02bf
.word 0x5400016b
.loc 3 1261 0
.word 0x6b1302bf
.word 0x540000e1
.loc 3 1262 0
.word 0xd280007e
.word 0x1b1e7f01
.word 0xb9814ba0
.word 0x4b010000
.word 0xb9014ba0
.word 0x14000003
.loc 3 1264 0
.word 0xd280003e
.word 0x390503be
.loc 3 1267 0
.word 0xf9408fa0
.word 0x79400000
.word 0x34000500
.loc 3 1269 0
.word 0xf9402ba1
.word 0x91001020
.word 0xb9800421
.word 0xb9814ba2
.word 0xb020021
.word 0xb9000001
.loc 3 1270 0
.word 0x3944e3a0
.word 0x35000180
.word 0xf9402ba0
.word 0xf9400001
.word 0xf90073a1
.word 0xf9400401
.word 0xf90077a1
.word 0xf9400800
.word 0xf9007ba0
.word 0xb980e7a0
.word 0xb140000
.word 0x4b130019
.word 0x14000002
.word 0xaa1403f9
.word 0xaa1903fa
.loc 3 1271 0
.word 0xf9402ba0
.word 0xaa1903e1
bl _p_16
.loc 3 1272 0
.word 0xf9408fa0
.word 0x79400000
.word 0x35000220
.loc 3 1274 0
.word 0xf9402fa0
.word 0xd2800041
bl _p_34
.word 0x93407c00
.word 0xb90153a0
.loc 3 1275 0
.word 0xb98153a0
.word 0xb98123a1
.word 0x6b01001f
.word 0x54000100
.loc 3 1277 0
.word 0xb98153a0
.word 0xb90123a0
.loc 3 1278 0
.word 0x17fffef9
.loc 3 1284 0
.word 0xf9402ba0
.word 0x3900201f
.loc 3 1285 0
.word 0xf9402ba0
.word 0xb900041f
.loc 3 1291 0
.word 0xb9812ba0
.word 0x6b13001f
.word 0x5400006b
.word 0xd2800019
.word 0x14000003
.word 0xb9812ba0
.word 0x4b000279
.word 0xb9012bb9
.loc 3 1292 0
.word 0xb98133a0
.word 0x6b13001f
.word 0x5400006c
.word 0xd2800019
.word 0x14000003
.word 0xb98133a0
.word 0x4b000279
.word 0xb90133b9
.loc 3 1293 0
.word 0x3944e3a0
.word 0x34000080
.loc 3 1295 0
.word 0xaa1303f8
.loc 3 1296 0
.word 0xb90163bf
.loc 3 1297 0
.word 0x14000020
.loc 3 1300 0
.word 0xf9402ba0
.word 0xf9400001
.word 0xf90067a1
.word 0xf9400401
.word 0xf9006ba1
.word 0xf9400800
.word 0xf9006fa0
.word 0xb980cfa0
.word 0x6b13001f
.word 0x5400006c
.word 0xaa1303f9
.word 0x14000009
.word 0xf9402ba0
.word 0xf9400001
.word 0xf9005ba1
.word 0xf9400401
.word 0xf9005fa1
.word 0xf9400800
.word 0xf90063a0
.word 0xb980b7b9
.word 0xaa1903f8
.loc 3 1301 0
.word 0xf9402ba0
.word 0xf9400001
.word 0xf9004fa1
.word 0xf9400401
.word 0xf90053a1
.word 0xf9400800
.word 0xf90057a0
.word 0xb9809fa0
.word 0x4b130000
.word 0xb90163a0
.loc 3 1303 0
.word 0xb98123a0
.word 0xb90153a0
.loc 3 1309 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #304]
.word 0xd2800081
bl _p_35
.word 0xf9008ba0
.loc 3 1310 0
.word 0x92800017
.word 0xf2bffff7
.loc 3 1312 0
.word 0x394503a0
.word 0x34000be0
.loc 3 1315 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9402000
.word 0xb9801000
.word 0x6b1f001f
.word 0x54000b2d
.loc 3 1323 0
.word 0xf94033a0
.word 0xf94033a1
.word 0xf940003e
bl _p_30
.word 0xaa0003fa
.loc 3 1325 0
.word 0xd2800016
.loc 3 1326 0
.word 0xb9018bbf
.loc 3 1327 0
.word 0xb9801b40
.word 0xb90193a0
.loc 3 1328 0
.word 0x34000160
.loc 3 1329 0
.word 0x93407ec0
.word 0xb9801b41
.word 0xeb00003f
.word 0x10000011
.word 0x54004a69
.word 0xd37ef400
.word 0x8b000340
.word 0x91008000
.word 0xb9800000
.word 0xb9018ba0
.loc 3 1330 0
.word 0xb9818ba0
.word 0xb9019ba0
.loc 3 1332 0
.word 0xaa1803f9
.word 0xb98163a0
.word 0x6b1f001f
.word 0x5400006b
.word 0xd2800015
.word 0x14000002
.word 0xb98163b5
.word 0xb150321
.word 0xb901a3a1
.loc 3 1333 0
.word 0xb9812ba0
.word 0x6b01001f
.word 0x5400006c
.word 0xb981a3b9
.word 0x14000002
.word 0xb9812bb9
.word 0xaa1903f5
.word 0x1400002f
.loc 3 1336 0
.word 0xb9819ba0
.word 0x34000600
.loc 3 1338 0
.word 0x110006f7
.loc 3 1339 0
.word 0xaa1703e0
.word 0xf9408ba1
.word 0xb9801821
.word 0x6b01001f
.word 0x5400012b
.loc 3 1340 0
.word 0x910443a0
.word 0xf9408ba1
.word 0xb9801821
.word 0x531f7821

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x15, [x16, #312]
bl _p_36
.loc 3 1342 0
.word 0xf9408ba0
.word 0x93407ee1
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x540044a9
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9818ba1
.word 0xb9000001
.loc 3 1343 0
.word 0xb98193a0
.word 0x51000400
.word 0x6b0002df
.word 0x5400018a
.loc 3 1345 0
.word 0x110006d6
.loc 3 1346 0
.word 0x93407ec0
.word 0xb9801b41
.word 0xeb00003f
.word 0x10000011
.word 0x540042c9
.word 0xd37ef400
.word 0x8b000340
.word 0x91008000
.word 0xb9800000
.word 0xb9019ba0
.loc 3 1348 0
.word 0xb9818ba0
.word 0xb9819ba1
.word 0xb010000
.word 0xb9018ba0
.loc 3 1334 0
.word 0xb9818ba0
.word 0x6b0002bf
.word 0x54fffa0c
.loc 3 1353 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #216]
.word 0xd2800601
bl _p_14
.word 0xf900dba0
.word 0xd2800d21
bl _p_15
.word 0xf940dba0
.word 0xf900b7a0
.loc 3 1355 0
.word 0xf9402ba0
.word 0xf9400001
.word 0xf90043a1
.word 0xf9400401
.word 0xf90047a1
.word 0xf9400800
.word 0xf9004ba0
.word 0x394223a0
.word 0x34000140
.word 0xb98123a0
.word 0x35000100
.loc 3 1356 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9401801
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_18
.loc 3 1358 0
.word 0x3905c3bf
.word 0xf9402fba
.loc 3 1360 0
.word 0xf9402fa0
.word 0xf900bfa0
.word 0xf9402fa0
.word 0xb4000080
.word 0xf940bfa0
.word 0x91005000
.word 0xf900bfa0
.loc 3 1362 0
.word 0xf9408fa0
.word 0xf900c3a0
.word 0x140001c6
.loc 3 1366 0
.word 0xb98163a0
.word 0x6b1f001f
.word 0x540007ad
.loc 3 1368 0
.word 0xd280047e
.word 0x6b1e035f
.word 0x540006e0
.word 0xd28005de
.word 0x6b1e035f
.word 0x54000680
.word 0xd280061e
.word 0x6b1e035f
.word 0x54000620
.word 0x14000033
.loc 3 1377 0
.word 0xf940c3a0
.word 0x79400000
.word 0xf940b7b6
.word 0x35000060
.word 0xd2800615
.word 0x14000006
.word 0xf940c3a0
.word 0xf940c3a1
.word 0x91000821
.word 0xf900c3a1
.word 0x79400015
.word 0xaa1603e0
.word 0xaa1503e1
.word 0xf94002de
bl _p_11
.loc 3 1378 0
.word 0x394503a0
.word 0x34000360
.word 0xd280003e
.word 0x6b1e031f
.word 0x5400030d
.word 0x6b1f02ff
.word 0x540002cb
.loc 3 1380 0
.word 0xf9408ba0
.word 0x93407ee1
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54003729
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9800000
.word 0x11000400
.word 0x6b00031f
.word 0x54000121
.loc 3 1382 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9402001
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_18
.loc 3 1383 0
.word 0x510006f7
.loc 3 1386 0
.word 0x51000718
.loc 3 1387 0
.word 0xb98163a0
.word 0x51000400
.word 0xb90163a0
.loc 3 1373 0
.word 0xb98163a0
.word 0x6b1f001f
.word 0x54fff9ec
.loc 3 1393 0
.word 0xd28008be
.word 0x6b1e035f
.word 0x540003a8
.word 0x51008b59
.word 0xd28000de
.word 0x6b1e033f
.word 0x54000122
.word 0xd37df320
.word 0x2a0003e1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #320]
.word 0x8b010000
.word 0xf9400000
.word 0xd61f0000
.word 0x5100b359
.word 0xd28000be
.word 0x6b1e033f
.word 0x54000122
.word 0xd37df320
.word 0x2a0003e1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #328]
.word 0x8b010000
.word 0xf9400000
.word 0xd61f0000
.word 0xd28008be
.word 0x6b1e035f
.word 0x540016e0
.word 0x14000163
.word 0xd2800b9e
.word 0x6b1e035f
.word 0x54001380
.word 0xd2800cbe
.word 0x6b1e035f
.word 0x54001600
.word 0xd284061e
.word 0x6b1e035f
.word 0x54000b60
.word 0x14000159
.loc 3 1398 0
.word 0xb98163a0
.word 0x6b1f001f
.word 0x5400018a
.loc 3 1400 0
.word 0xb98163a0
.word 0x11000400
.word 0xb90163a0
.loc 3 1401 0
.word 0xb9812ba0
.word 0x6b00031f
.word 0x5400006d
.word 0xd2800019
.word 0x14000002
.word 0xd2800619
.word 0xaa1903fa
.loc 3 1402 0
.word 0x14000011
.loc 3 1405 0
.word 0xf940c3a0
.word 0x79400000
.word 0x35000100
.word 0xb98133a0
.word 0x6b00031f
.word 0x5400006c
.word 0xd2800019
.word 0x14000008
.word 0xd2800619
.word 0x14000006
.word 0xf940c3a0
.word 0xf940c3a1
.word 0x91000821
.word 0xf900c3a1
.word 0x79400019
.word 0xaa1903fa
.loc 3 1407 0
.word 0x3400045a
.loc 3 1409 0
.word 0xf940b7a0
.word 0xaa1a03e1
.word 0xf940b7a2
.word 0xf940005e
bl _p_11
.loc 3 1410 0
.word 0x394503a0
.word 0x34000360
.word 0xd280003e
.word 0x6b1e031f
.word 0x5400030d
.word 0x6b1f02ff
.word 0x540002cb
.loc 3 1412 0
.word 0xf9408ba0
.word 0x93407ee1
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002929
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9800000
.word 0x11000400
.word 0x6b00031f
.word 0x54000121
.loc 3 1414 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9402001
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_18
.loc 3 1415 0
.word 0x510006f7
.loc 3 1420 0
.word 0x51000718
.loc 3 1421 0
.word 0x1400011c
.loc 3 1425 0
.word 0x6b1f031f
.word 0x9a9f97e0
.word 0x3945c3a1
.word 0x2a010000
.word 0x350022e0
.loc 3 1431 0
.word 0xb98133a0
.word 0x6b1f001f
.word 0x540000cb
.word 0x6b14027f
.word 0x5400224a
.word 0xf940c3a0
.word 0x79400000
.word 0x340021e0
.loc 3 1433 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9401c01
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_18
.loc 3 1434 0
.word 0xd280003e
.word 0x3905c3be
.loc 3 1436 0
.word 0x14000105
.loc 3 1439 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9404c01
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_18
.loc 3 1440 0
.word 0x140000fd
.loc 3 1442 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9404801
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_18
.loc 3 1443 0
.word 0x140000f5
.loc 3 1449 0
.word 0xb98153a0
.word 0xb98153a1
.word 0x11000421
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400001
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_11
.loc 3 1448 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0x34000140
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0x6b1a001f
.word 0x54fffc41
.loc 3 1450 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0x340019c0
.loc 3 1451 0
.word 0xb98153a0
.word 0x11000400
.word 0xb90153a0
.loc 3 1452 0
.word 0x140000ca
.loc 3 1454 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0x34001840
.loc 3 1455 0
.word 0xb98153a0
.word 0xb98153a1
.word 0x11000421
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400001
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_11
.loc 3 1456 0
.word 0x140000b3
.loc 3 1460 0
.word 0xd2800016
.loc 3 1461 0
.word 0xd2800015
.loc 3 1462 0
.word 0x3944e3a0
.word 0x34000d20
.loc 3 1464 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54000061
.loc 3 1467 0
.word 0x110006b5
.loc 3 1468 0
.word 0x14000034
.loc 3 1469 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd280057e
.word 0x6b1e001f
.word 0x540001c1
.word 0xb98153a0
.word 0x11000400
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54000061
.loc 3 1472 0
.word 0xd2800036
.loc 3 1473 0
.word 0x1400001d
.loc 3 1474 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd28005be
.word 0x6b1e001f
.word 0x54000181
.word 0xb98153a0
.word 0x11000400
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54000100
.loc 3 1481 0
.word 0xf940b7a0
.word 0xaa1a03e1
.word 0xf940b7a2
.word 0xf940005e
bl _p_11
.loc 3 1482 0
.word 0x14000071
.loc 3 1486 0
.word 0x110006b5
.loc 3 1485 0
.word 0xb98153a0
.word 0x11000401
.word 0xaa0103e0
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54fffe60
.loc 3 1487 0
.word 0xd280015e
.word 0x6b1e02bf
.word 0x5400004d
.loc 3 1488 0
.word 0xd2800155
.loc 3 1490 0
.word 0xf9408fa0
.word 0x79400000
.word 0x34000160
.word 0xf9402ba0
.word 0xf9400001
.word 0xf90037a1
.word 0xf9400401
.word 0xf9003ba1
.word 0xf9400800
.word 0xf9003fa0
.word 0xb9806fa0
.word 0x4b130019
.word 0x14000002
.word 0xd2800019
.word 0xb9018bb9
.loc 3 1491 0
.word 0xf940b7a0
.word 0xf94033a1
.word 0xaa1903e2
.word 0xaa1a03e3
.word 0xaa1503e4
.word 0xaa1603e5
bl _p_31
.loc 3 1492 0
.word 0x3904e3bf
.loc 3 1493 0
.word 0x14000047
.loc 3 1496 0
.word 0xf940b7a0
.word 0xaa1a03e1
.word 0xf940b7a2
.word 0xf940005e
bl _p_11
.loc 3 1497 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd280057e
.word 0x6b1e001f
.word 0x54000160
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd28005be
.word 0x6b1e001f
.word 0x540003c1
.loc 3 1498 0
.word 0xb98153a0
.word 0xb98153a1
.word 0x11000421
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400001
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_11
.word 0x1400000f
.loc 3 1500 0
.word 0xb98153a0
.word 0xb98153a1
.word 0x11000421
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400001
.word 0xf940b7a0
.word 0xf940b7a2
.word 0xf940005e
bl _p_11
.loc 3 1499 0
.word 0xb98153a0
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400000
.word 0xd280061e
.word 0x6b1e001f
.word 0x54fffd20
.word 0x14000006
.loc 3 1505 0
.word 0xf940b7a0
.word 0xaa1a03e1
.word 0xf940b7a2
.word 0xf940005e
bl _p_11
.loc 3 1364 0
.word 0xb98153a0
.word 0xb98153a1
.word 0x11000421
.word 0xb90153a1
.word 0x93407c00
.word 0xd2800041
.word 0x9b017c01
.word 0xf940bfa0
.word 0x8b010000
.word 0x79400001
.word 0xaa0103e0
.word 0xaa0103fa
.word 0x34000080
.word 0xd280077e
.word 0x6b1e035f
.word 0x54ffc581
.loc 3 1511 0
.word 0xf940b7a0
.word 0xf940b7a1
.word 0xf9400021
.word 0xf9402030
.word 0xd63f0200
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8dc7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_16:
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number__cctor
System_Globalization_FormatProvider_Number__cctor:
.loc 3 591 0 prologue_end
.word 0xa9aa7bfd
.word 0x910003fd

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #336]
.word 0xd2800081
bl _p_35
.word 0xaa0003e3
.word 0xaa0303e0
.word 0xf900aba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #344]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf940aba3
.word 0xaa0303e0
.word 0xf900a7a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #352]
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf940a7a3
.word 0xaa0303e0
.word 0xf900a3a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #360]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf940a3a3
.word 0xaa0303e0
.word 0xf9009fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #368]
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9409fa1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #240]
.word 0xf9000001
.loc 3 596 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #336]
.word 0xd2800201
bl _p_35
.word 0xaa0003e3
.word 0xaa0303e0
.word 0xf9009ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #376]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9409ba3
.word 0xaa0303e0
.word 0xf90097a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #384]
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94097a3
.word 0xaa0303e0
.word 0xf90093a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #392]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94093a3
.word 0xaa0303e0
.word 0xf9008fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #400]
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9408fa3
.word 0xaa0303e0
.word 0xf9008ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #408]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9408ba3
.word 0xaa0303e0
.word 0xf90087a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #416]
.word 0xaa0303e0
.word 0xd28000a1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94087a3
.word 0xaa0303e0
.word 0xf90083a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #424]
.word 0xaa0303e0
.word 0xd28000c1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94083a3
.word 0xaa0303e0
.word 0xf9007fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #432]
.word 0xaa0303e0
.word 0xd28000e1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407fa3
.word 0xaa0303e0
.word 0xf9007ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #440]
.word 0xaa0303e0
.word 0xd2800101
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407ba3
.word 0xaa0303e0
.word 0xf90077a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #448]
.word 0xaa0303e0
.word 0xd2800121
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94077a3
.word 0xaa0303e0
.word 0xf90073a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #456]
.word 0xaa0303e0
.word 0xd2800141
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94073a3
.word 0xaa0303e0
.word 0xf9006fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #464]
.word 0xaa0303e0
.word 0xd2800161
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406fa3
.word 0xaa0303e0
.word 0xf9006ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #472]
.word 0xaa0303e0
.word 0xd2800181
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406ba3
.word 0xaa0303e0
.word 0xf90067a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #480]
.word 0xaa0303e0
.word 0xd28001a1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94067a3
.word 0xaa0303e0
.word 0xf90063a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #488]
.word 0xaa0303e0
.word 0xd28001c1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94063a3
.word 0xaa0303e0
.word 0xf9005fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #496]
.word 0xaa0303e0
.word 0xd28001e1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9405fa1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #248]
.word 0xf9000001
.loc 3 604 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #336]
.word 0xd2800081
bl _p_35
.word 0xaa0003e3
.word 0xaa0303e0
.word 0xf9005ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #504]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9405ba3
.word 0xaa0303e0
.word 0xf90057a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #512]
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94057a3
.word 0xaa0303e0
.word 0xf90053a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #520]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94053a3
.word 0xaa0303e0
.word 0xf9004fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #528]
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9404fa1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #272]
.word 0xf9000001
.loc 3 609 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #336]
.word 0xd2800181
bl _p_35
.word 0xaa0003e3
.word 0xaa0303e0
.word 0xf9004ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #536]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9404ba3
.word 0xaa0303e0
.word 0xf90047a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #544]
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94047a3
.word 0xaa0303e0
.word 0xf90043a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #552]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94043a3
.word 0xaa0303e0
.word 0xf9003fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #560]
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9403fa3
.word 0xaa0303e0
.word 0xf9003ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #568]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9403ba3
.word 0xaa0303e0
.word 0xf90037a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #576]
.word 0xaa0303e0
.word 0xd28000a1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94037a3
.word 0xaa0303e0
.word 0xf90033a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #584]
.word 0xaa0303e0
.word 0xd28000c1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94033a3
.word 0xaa0303e0
.word 0xf9002fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #592]
.word 0xaa0303e0
.word 0xd28000e1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9402fa3
.word 0xaa0303e0
.word 0xf9002ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #600]
.word 0xaa0303e0
.word 0xd2800101
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9402ba3
.word 0xaa0303e0
.word 0xf90027a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #608]
.word 0xaa0303e0
.word 0xd2800121
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94027a3
.word 0xaa0303e0
.word 0xf90023a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #616]
.word 0xaa0303e0
.word 0xd2800141
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94023a3
.word 0xaa0303e0
.word 0xf9001fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #624]
.word 0xaa0303e0
.word 0xd2800161
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9401fa1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #280]
.word 0xf9000001
.loc 3 618 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #336]
.word 0xd28000a1
bl _p_35
.word 0xaa0003e3
.word 0xaa0303e0
.word 0xf9001ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #632]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9401ba3
.word 0xaa0303e0
.word 0xf90017a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #640]
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94017a3
.word 0xaa0303e0
.word 0xf90013a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #648]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94013a3
.word 0xaa0303e0
.word 0xf9000fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #656]
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9400fa3
.word 0xaa0303e0
.word 0xf9000ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #664]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9400ba1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #264]
.word 0xf9000001
.loc 3 623 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #672]

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #256]
.word 0xf9000001
.word 0x910003bf
.word 0xa8d67bfd
.word 0xd65f03c0

Lme_17:
.text
ut_24:
add x0, x0, 16
b System_Globalization_FormatProvider_Number_NumberBuffer_get_digits
ut_end:
.section __TEXT, __const
_unbox_trampoline_p:

	.long 0
LDIFF_SYM3=ut_end - ut_24
	.long LDIFF_SYM3
.text
	.align 4
	.no_dead_strip System_Globalization_FormatProvider_Number_NumberBuffer_get_digits
System_Globalization_FormatProvider_Number_NumberBuffer_get_digits:
.file 4 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/external/corefx/src/System.Runtime.Numerics/src/System/Globalization/FormatProvider.NumberBuffer.cs"
.loc 4 26 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9400ba0
.word 0xf9400800
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_18:
.text
ut_25:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_int
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_int
System_Numerics_BigInteger__ctor_int:
.file 5 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/external/corefx/src/System.Runtime.Numerics/src/System/Numerics/BigInteger.cs"
.loc 5 36 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xb9801ba0
.word 0x929ffffe
.word 0xf2b0001e
.word 0x6b1e001f
.word 0x54000341
.loc 5 37 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #680]
.word 0xf9400001
.word 0xf90013a1
.word 0xf9400400
.word 0xf90017a0
.word 0xf9400ba1
.word 0xaa0103e0
.word 0xf94013a2
.word 0xf9000022
.word 0x91002001
.word 0xf94017a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0x14000005
.loc 5 40 0
.word 0xb9801ba1
.word 0xf9400ba0
.word 0xb9000001
.loc 5 41 0
.word 0xf900041f
.loc 5 44 0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_19:
.text
ut_26:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_uint
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_uint
System_Numerics_BigInteger__ctor_uint:
.loc 5 49 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1
.word 0xb9401ba0
.word 0xd29ffffe
.word 0xf2affffe
.word 0x6b1e001f
.word 0x540000a8
.loc 5 51 0
.word 0xb9401ba0
.word 0xb9000320
.loc 5 52 0
.word 0xf900073f
.loc 5 53 0
.word 0x1400001b
.loc 5 56 0
.word 0xd280003e
.word 0xb900033e
.loc 5 57 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800021
bl _p_35
.word 0xf9000720
.word 0x91002321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 58 0
.word 0xf9400720
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x540000e9
.word 0xb9401ba1
.word 0xb9002001
.loc 5 61 0
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_1a:
.text
ut_27:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_long
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_long
System_Numerics_BigInteger__ctor_long:
.loc 5 65 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xd2800000
.word 0xf2b00000
.word 0xf2dfffe0
.word 0xf2ffffe0
.word 0xeb1a001f
.word 0x5400012a
.word 0xd29ffffe
.word 0xf2affffe
.word 0xeb1e035f
.word 0x540000ac
.loc 5 67 0
.word 0x93407f40
.word 0xb9000320
.loc 5 68 0
.word 0xf900073f
.loc 5 69 0
.word 0x14000066
.loc 5 70 0
.word 0xd280001e
.word 0xf2b0001e
.word 0xf2dffffe
.word 0xf2fffffe
.word 0xeb1e035f
.word 0x54000301
.loc 5 72 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #680]
.word 0xf9400001
.word 0xf90017a1
.word 0xf9400400
.word 0xf9001ba0
.word 0xf94017a0
.word 0xf9000320
.word 0x91002321
.word 0xf9401ba0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 73 0
.word 0x14000049
.loc 5 77 0
.word 0xeb1f035f
.word 0x540000ca
.loc 5 79 0
.word 0xcb1a03f8
.loc 5 80 0
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900033e
.loc 5 81 0
.word 0x14000004
.loc 5 84 0
.word 0xaa1a03f8
.loc 5 85 0
.word 0xd280003e
.word 0xb900033e
.loc 5 88 0
.word 0xd29fffe0
.word 0xf2bfffe0
.word 0xeb00031f
.word 0x54000348
.loc 5 90 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800021
bl _p_35
.word 0xf9000720
.word 0x91002321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 91 0
.word 0xf9400720
.word 0xaa1803e1
.word 0xb9801802
.word 0xeb1f005f
.word 0x10000011
.word 0x54000529
.word 0xb9002001
.loc 5 92 0
.word 0x14000022
.loc 5 95 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800041
bl _p_35
.word 0xf9000720
.word 0x91002321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 96 0
.word 0xf9400720
.word 0xaa1803e1
.word 0xb9801802
.word 0xeb1f005f
.word 0x10000011
.word 0x54000209
.word 0xb9002001
.loc 5 97 0
.word 0xf9400720
.word 0xd360ff01
.word 0xaa0103e1
.word 0xb9801802
.word 0xd280003e
.word 0xeb1e005f
.word 0x10000011
.word 0x540000e9
.word 0xb9002401
.loc 5 102 0
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_1b:
.text
ut_28:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_ulong
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_ulong
System_Numerics_BigInteger__ctor_ulong:
.loc 5 107 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xd29ffffe
.word 0xf2affffe
.word 0xeb1e035f
.word 0x540000a8
.loc 5 109 0
.word 0x93407f40
.word 0xb9000320
.loc 5 110 0
.word 0xf900073f
.loc 5 111 0
.word 0x14000043
.loc 5 112 0
.word 0xd29fffe0
.word 0xf2bfffe0
.word 0xeb00035f
.word 0x54000388
.loc 5 114 0
.word 0xd280003e
.word 0xb900033e
.loc 5 115 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800021
bl _p_35
.word 0xf9000720
.word 0x91002321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 116 0
.word 0xf9400720
.word 0xaa1a03e1
.word 0xb9801802
.word 0xeb1f005f
.word 0x10000011
.word 0x54000549
.word 0xb9002001
.loc 5 117 0
.word 0x14000024
.loc 5 120 0
.word 0xd280003e
.word 0xb900033e
.loc 5 121 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800041
bl _p_35
.word 0xf9000720
.word 0x91002321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 122 0
.word 0xf9400720
.word 0xaa1a03e1
.word 0xb9801802
.word 0xeb1f005f
.word 0x10000011
.word 0x540001e9
.word 0xb9002001
.loc 5 123 0
.word 0xf9400720
.word 0xd360ff41
.word 0xaa0103e1
.word 0xb9801802
.word 0xd280003e
.word 0xeb1e005f
.word 0x10000011
.word 0x540000c9
.word 0xb9002401
.loc 5 127 0
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_1c:
.text
ut_29:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_single
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_single
System_Numerics_BigInteger__ctor_single:
.loc 5 129 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xbd001ba0
.word 0xbd401bb0
.word 0x1e22c200
.word 0xf9400ba0
bl _p_37
.loc 5 131 0
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_1d:
.text
ut_30:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_double
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_double
System_Numerics_BigInteger__ctor_double:
.loc 5 135 0 prologue_end
.word 0xa9b47bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xaa0003fa
.word 0xfd0017a0
.word 0xb90073bf
.word 0xb9007bbf
.word 0xf90043bf
.word 0x390223bf
.word 0xfd4017a0
bl _p_38
.word 0x53001c00
.word 0x35001fe0
.loc 5 137 0
.word 0xfd4017a0
bl _p_39
.word 0x53001c00
.word 0x35001e20
.loc 5 141 0
.word 0xb900035f
.loc 5 142 0
.word 0xf900075f
.loc 5 147 0
.word 0xfd4017a0
.word 0x9101c3a0
.word 0x9101e3a1
.word 0x910203a2
.word 0x910223a3
bl _p_40
.loc 5 150 0
.word 0xf94043a0
.word 0xb50003c0
.loc 5 152 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #696]
.word 0x3980b410
.word 0xb5000050
bl _p_1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf9400001
.word 0xf90033a1
.word 0xf9400400
.word 0xf90037a0
.word 0xf94033a0
.word 0xf9000340
.word 0x91002341
.word 0xf94037a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 153 0
.word 0x140000c5
.loc 5 159 0
.word 0xb9807ba0
.word 0x6b1f001f
.word 0x540008ac
.loc 5 161 0
.word 0xb9807ba0
.word 0x928007fe
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x540003cc
.loc 5 163 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #696]
.word 0x3980b410
.word 0xb5000050
bl _p_1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf9400001
.word 0xf9002ba1
.word 0xf9400400
.word 0xf9002fa0
.word 0xf9402ba0
.word 0xf9000340
.word 0x91002341
.word 0xf9402fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 164 0
.word 0x140000a0
.loc 5 166 0
.word 0xf94043a0
.word 0xb9807ba1
.word 0x4b0103e1
.word 0xd28007fe
.word 0xa1e0021
.word 0x9ac12400
.word 0x910103a1
.word 0xf9004ba1
bl _p_41
.word 0xf9404bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0xf94023a0
.word 0xf9000340
.word 0x91002341
.word 0xf94027a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 167 0
.word 0xb98073a0
.word 0x6b1f001f
.word 0x5400104a
.loc 5 168 0
.word 0xb9800340
.word 0x4b0003e0
.word 0xb9000340
.loc 5 169 0
.word 0x1400007e
.loc 5 170 0
.word 0xb9807ba0
.word 0xd280017e
.word 0x6b1e001f
.word 0x5400044c
.loc 5 172 0
.word 0xf94043a0
.word 0xb9807ba1
.word 0xd28007fe
.word 0xa1e0021
.word 0x9ac12000
.word 0x9100c3a1
.word 0xf9004ba1
bl _p_41
.word 0xf9404bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9401ba0
.word 0xf9000340
.word 0x91002341
.word 0xf9401fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 173 0
.word 0xb98073a0
.word 0x6b1f001f
.word 0x54000baa
.loc 5 174 0
.word 0xb9800340
.word 0x4b0003e0
.word 0xb9000340
.loc 5 175 0
.word 0x14000059
.loc 5 180 0
.word 0xf94043a0
.word 0xd375d000
.word 0xf90043a0
.loc 5 181 0
.word 0xb9807ba0
.word 0x51002c00
.word 0xb9007ba0
.loc 5 184 0
.word 0xb9807ba0
.word 0x51000401
.word 0x131f7c20
.word 0x531b7c00
.word 0xb010000
.word 0x13057c00
.word 0x11000419
.loc 5 185 0
.word 0xaa1903e0
.word 0x531b6800
.word 0xb9807ba1
.word 0x4b010018
.loc 5 190 0
.word 0x11000b21

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xf9000740
.word 0x91002341
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 191 0
.word 0xf9400740
.word 0x11000722
.word 0xf94043a1
.word 0x11008303
.word 0xd28007fe
.word 0xa1e0063
.word 0x9ac32421
.word 0xaa0103e1
.word 0x93407c42
.word 0xb9801803
.word 0xeb02007f
.word 0x10000011
.word 0x54000869
.word 0xd37ef442
.word 0x8b020000
.word 0x91008000
.word 0xb9000001
.loc 5 192 0
.word 0xf9400740
.word 0xf94043a1
.word 0xd28007fe
.word 0xa1e0302
.word 0x9ac22421
.word 0xaa0103e1
.word 0x93407f22
.word 0xb9801803
.word 0xeb02007f
.word 0x10000011
.word 0x54000689
.word 0xd37ef442
.word 0x8b020000
.word 0x91008000
.word 0xb9000001
.loc 5 193 0
.word 0x6b1f031f
.word 0x5400026d
.loc 5 194 0
.word 0xf9400740
.word 0x51000722
.word 0xf94043a1
.word 0xaa0103e1
.word 0xd2800403
.word 0x4b180063
.word 0xd28003fe
.word 0xa1e0063
.word 0x1ac32021
.word 0x93407c42
.word 0xb9801803
.word 0xeb02007f
.word 0x10000011
.word 0x54000409
.word 0xd37ef442
.word 0x8b020000
.word 0x91008000
.word 0xb9000001
.loc 5 195 0
.word 0xb98073a0
.word 0xb9000340
.loc 5 199 0
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0
.loc 5 138 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2803da1
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.loc 5 136 0
.word 0xd2803421
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_1e:
.text
ut_31:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_System_Decimal
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_System_Decimal
System_Numerics_BigInteger__ctor_System_Decimal:
.loc 5 204 0 prologue_end
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003fa
.word 0xf9001ba1
.word 0xf9001fa2
.word 0x910143a0
.word 0xf90033a0
.word 0xf9401ba0
.word 0xf9401fa1
bl _p_42
.word 0xf94033be
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9402ba0
.word 0xf9402fa1
bl _p_43
.word 0xaa0003f9
.loc 5 208 0
.word 0xd2800078
.word 0x14000002
.loc 5 210 0
.word 0x51000718
.loc 5 209 0
.word 0x6b1f031f
.word 0x5400018d
.word 0x51000700
.word 0x93407c00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x54001289
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9800000
.word 0x34fffe60
.loc 5 211 0
.word 0x35000318
.loc 5 213 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf9400001
.word 0xf90023a1
.word 0xf9400400
.word 0xf90027a0
.word 0xf94023a0
.word 0xf9000340
.word 0x91002341
.word 0xf94027a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 214 0
.word 0x14000072
.loc 5 215 0
.word 0xd280003e
.word 0x6b1e031f
.word 0x54000461
.word 0xb9801b20
.word 0xeb1f001f
.word 0x10000011
.word 0x54000e09
.word 0xb9802320
.word 0x6b1f001f
.word 0x5400038d
.loc 5 219 0
.word 0xb9801b20
.word 0xeb1f001f
.word 0x10000011
.word 0x54000d29
.word 0xb9802320
.word 0xb9000340
.loc 5 220 0
.word 0xb9800341
.word 0xb9801b20
.word 0xd280007e
.word 0xeb1e001f
.word 0x10000011
.word 0x54000c29
.word 0xb9802f20
.word 0x929ffffe
.word 0xf2b0001e
.word 0xa1e0000
.word 0xaa1a03f9
.word 0xaa0103f8
.word 0x35000060
.word 0xd2800037
.word 0x14000003
.word 0x92800017
.word 0xf2bffff7
.word 0x1b177f00
.word 0xb9000320
.loc 5 221 0
.word 0xf900075f
.loc 5 222 0
.word 0x1400004d
.loc 5 225 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1803e1
bl _p_35
.word 0xf9000740
.word 0x91002341
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 229 0
.word 0xf9400740
.word 0xb9801b21
.word 0xeb1f003f
.word 0x10000011
.word 0x54000789
.word 0xb9802321
.word 0xb9801802
.word 0xeb1f005f
.word 0x10000011
.word 0x540006e9
.word 0xb9002001
.loc 5 230 0
.word 0xd280003e
.word 0x6b1e031f
.word 0x540001cd
.loc 5 231 0
.word 0xf9400740
.word 0xb9801b21
.word 0xd280003e
.word 0xeb1e003f
.word 0x10000011
.word 0x540005a9
.word 0xb9802721
.word 0xb9801802
.word 0xd280003e
.word 0xeb1e005f
.word 0x10000011
.word 0x540004e9
.word 0xb9002401
.loc 5 232 0
.word 0xd280005e
.word 0x6b1e031f
.word 0x540001cd
.loc 5 233 0
.word 0xf9400740
.word 0xb9801b21
.word 0xd280005e
.word 0xeb1e003f
.word 0x10000011
.word 0x540003a9
.word 0xb9802b21
.word 0xb9801802
.word 0xd280005e
.word 0xeb1e005f
.word 0x10000011
.word 0x540002e9
.word 0xb9002801
.loc 5 236 0
.word 0xb9801b20
.word 0xd280007e
.word 0xeb1e001f
.word 0x10000011
.word 0x54000229
.word 0xb9802f20
.word 0x929ffffe
.word 0xf2b0001e
.word 0xa1e0000
.word 0xaa1a03f9
.word 0x35000060
.word 0xd2800038
.word 0x14000003
.word 0x92800018
.word 0xf2bffff8
.word 0xb9000338
.loc 5 239 0
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_1f:
.text
ut_32:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_byte__
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_byte__
System_Numerics_BigInteger__ctor_byte__:
.loc 5 248 0 prologue_end
.word 0xa9b47bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xf9402fa0
.word 0xb4003fc0
.loc 5 252 0
.word 0xf9402fa0
.word 0xb9801818
.loc 5 253 0
.word 0xaa1803e0
.word 0x6b1f001f
.word 0x5400022d
.word 0x51000700
.word 0x93407c01
.word 0xf9402fa0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54003f89
.word 0x8b010000
.word 0x91008000
.word 0x39400000
.word 0xd280101e
.word 0xa1e0000
.word 0xd280101e
.word 0x6b1e001f
.word 0x9a9f17f7
.word 0x14000002
.word 0xd2800017
.word 0xaa1703f6
.word 0x14000002
.loc 5 256 0
.word 0x51000718
.word 0x6b1f031f
.word 0x5400018d
.word 0x51000700
.word 0x93407c01
.word 0xf9402fa0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54003cc9
.word 0x8b010000
.word 0x91008000
.word 0x39400000
.word 0x34fffe60
.loc 5 258 0
.word 0x350000b8
.loc 5 261 0
.word 0xf9402ba0
.word 0xb900001f
.loc 5 262 0
.word 0xf900041f
.loc 5 264 0
.word 0x140001cc
.loc 5 267 0
.word 0xd280009e
.word 0x6b1e031f
.word 0x54000bec
.loc 5 269 0
.word 0x340000d6
.loc 5 270 0
.word 0xf9402ba0
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900001e
.word 0x14000003
.loc 5 272 0
.word 0xf9402ba0
.word 0xb900001f
.loc 5 273 0
.word 0x5100071a
.word 0x14000012
.loc 5 275 0
.word 0xf9402ba0
.word 0xb9800001
.word 0x53185c21
.word 0xb9000001
.loc 5 276 0
.word 0xb9800001
.word 0x93407f43
.word 0xf9402fa2
.word 0xb9801844
.word 0xeb03009f
.word 0x10000011
.word 0x540038a9
.word 0x8b030042
.word 0x91008042
.word 0x39400042
.word 0x2a020021
.word 0xb9000001
.loc 5 273 0
.word 0x5100075a
.word 0x6b1f035f
.word 0x54fffdca
.loc 5 278 0
.word 0xf9402ba0
.word 0xf900041f
.loc 5 280 0
.word 0xb9800000
.word 0x6b1f001f
.word 0x540003ca
.word 0x350003b6
.loc 5 286 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800021
bl _p_35
.word 0xaa0003e1
.word 0xf9402ba0
.word 0xf9000401
.word 0x91002002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.loc 5 287 0
.word 0xf9400401
.word 0xb9800002
.word 0xb9801823
.word 0xeb1f007f
.word 0x10000011
.word 0x540033c9
.word 0xb9002022
.loc 5 288 0
.word 0xd280003e
.word 0xb900001e
.loc 5 290 0
.word 0xf9402ba0
.word 0xb9800000
.word 0x929ffffe
.word 0xf2b0001e
.word 0x6b1e001f
.word 0x54003081
.loc 5 291 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #680]
.word 0xf9400001
.word 0xf9004ba1
.word 0xf9400400
.word 0xf9004fa0
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf9404ba2
.word 0xf9000022
.word 0x91002001
.word 0xf9404fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 292 0
.word 0x1400016b
.loc 5 295 0
.word 0x131f7f00
.word 0x531e7c01
.word 0xb010300
.word 0xd280007e
.word 0x8a1e0000
.word 0x4b010015
.loc 5 296 0
.word 0x131f7f00
.word 0x531e7c00
.word 0xb180000
.word 0x13027c17
.word 0x34000075
.word 0xd2800034
.word 0x14000002
.word 0xd2800014
.word 0xb1402f3
.loc 5 297 0
.word 0xd280003e
.word 0x390283be
.loc 5 298 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1303e1
bl _p_35
.word 0xf90057a0
.loc 5 302 0
.word 0xd280007a
.loc 5 303 0
.word 0xb900b3bf
.word 0x14000038
.loc 5 305 0
.word 0xd2800019
.word 0x1400002f
.loc 5 308 0
.word 0x93407f41
.word 0xf9402fa0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002b49
.word 0x8b010000
.word 0x91008000
.word 0x39400000
.word 0x34000040
.word 0x390283bf
.loc 5 309 0
.word 0xb980b3a1
.word 0x93407c22
.word 0xf94057a0
.word 0xb9801803
.word 0xeb02007f
.word 0x10000011
.word 0x540029c9
.word 0xd37ef442
.word 0x8b020003
.word 0x91008062
.word 0xb9402063
.word 0x53185c63
.word 0xb9000043
.loc 5 310 0
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002869
.word 0xd37ef421
.word 0x8b010001
.word 0x91008020
.word 0xb9402021
.word 0x93407f43
.word 0xf9402fa2
.word 0xb9801844
.word 0xeb03009f
.word 0x10000011
.word 0x54002729
.word 0x8b030042
.word 0x91008042
.word 0x39400042
.word 0x2a020021
.word 0xb9000001
.loc 5 311 0
.word 0x5100075a
.loc 5 312 0
.word 0x11000739
.loc 5 306 0
.word 0xd280009e
.word 0x6b1e033f
.word 0x54fffa0b
.loc 5 314 0
.word 0x1100235a
.loc 5 303 0
.word 0xb980b3a0
.word 0x11000400
.word 0xb900b3a0
.word 0xb980b3b7
.word 0xaa1303f4
.word 0x34000075
.word 0xd2800039
.word 0x14000002
.word 0xd2800019
.word 0x4b190280
.word 0x6b0002ff
.word 0x54fff82b
.loc 5 318 0
.word 0x34000835
.loc 5 320 0
.word 0x340001d6
.word 0x51000660
.word 0x93407c01
.word 0xf94057a0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002329
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900001e
.loc 5 321 0
.word 0x5100071a
.word 0x1400002e
.loc 5 323 0
.word 0x93407f41
.word 0xf9402fa0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002169
.word 0x8b010000
.word 0x91008000
.word 0x39400000
.word 0x34000040
.word 0x390283bf
.loc 5 324 0
.word 0xb980b3a1
.word 0x93407c22
.word 0xf94057a0
.word 0xb9801803
.word 0xeb02007f
.word 0x10000011
.word 0x54001fe9
.word 0xd37ef442
.word 0x8b020003
.word 0x91008062
.word 0xb9402063
.word 0x53185c63
.word 0xb9000043
.loc 5 325 0
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54001e89
.word 0xd37ef421
.word 0x8b010001
.word 0x91008020
.word 0xb9402021
.word 0x93407f43
.word 0xf9402fa2
.word 0xb9801844
.word 0xeb03009f
.word 0x10000011
.word 0x54001d49
.word 0x8b030042
.word 0x91008042
.word 0x39400042
.word 0x2a020021
.word 0xb9000001
.loc 5 321 0
.word 0x5100075a
.word 0x4b150300
.word 0x6b00035f
.word 0x54fffa2a
.loc 5 329 0
.word 0x394283a0
.word 0x34000340
.loc 5 331 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf9400001
.word 0xf90043a1
.word 0xf9400400
.word 0xf90047a0
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf94043a2
.word 0xf9000022
.word 0x91002001
.word 0xf94047a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 332 0
.word 0x140000b5
.loc 5 333 0
.word 0x34001496
.loc 5 335 0
.word 0xf94057a0
bl _p_44
.loc 5 338 0
.word 0xf94057a0
.word 0xb980181a
.word 0x14000002
.loc 5 340 0
.word 0x5100075a
.loc 5 339 0
.word 0x6b1f035f
.word 0x540001ad
.word 0x51000740
.word 0x93407c01
.word 0xf94057a0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x540016c9
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9400000
.word 0x34fffe40
.loc 5 341 0
.word 0xd280003e
.word 0x6b1e035f
.word 0x54000b21
.word 0xf94057a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54001529
.word 0xb9402000
.word 0x6b1f001f
.word 0x54000a2d
.loc 5 343 0
.word 0xf94057a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54001429
.word 0xb9402000
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000341
.loc 5 345 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #712]
.word 0xf9400001
.word 0xf9003ba1
.word 0xf9400400
.word 0xf9003fa0
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf9403ba2
.word 0xf9000022
.word 0x91002001
.word 0xf9403fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 346 0
.word 0x14000073
.loc 5 347 0
.word 0xf94057a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54000fe9
.word 0xb9402000
.word 0x929ffffe
.word 0xf2b0001e
.word 0x6b1e001f
.word 0x54000341
.loc 5 349 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #680]
.word 0xf9400001
.word 0xf90033a1
.word 0xf9400400
.word 0xf90037a0
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf94033a2
.word 0xf9000022
.word 0x91002001
.word 0xf94037a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 350 0
.word 0x14000050
.loc 5 353 0
.word 0xf94057a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54000b89
.word 0xb9402000
.word 0x4b0003e1
.word 0xf9402ba0
.word 0xb9000001
.loc 5 354 0
.word 0xf900041f
.loc 5 356 0
.word 0x14000045
.loc 5 357 0
.word 0xf94057a0
.word 0xb9801800
.word 0x6b00035f
.word 0x540003e0
.loc 5 359 0
.word 0xf9402ba0
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900001e
.loc 5 360 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1a03e1
bl _p_35
.word 0xaa0003e1
.word 0xf9402ba0
.word 0xf9000401
.word 0x91002002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.loc 5 361 0
.word 0xf9400402
.word 0xf94057a0
.word 0xd2800001
.word 0xd2800003
.word 0xaa1a03e4
bl _p_45
.loc 5 362 0
.word 0x14000023
.loc 5 365 0
.word 0xf9402ba1
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900003e
.loc 5 366 0
.word 0xf94057a0
.word 0xf9000420
.word 0x91002021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 368 0
.word 0x14000011
.loc 5 371 0
.word 0xf9402ba1
.word 0xd280003e
.word 0xb900003e
.loc 5 372 0
.word 0xf94057a0
.word 0xf9000420
.word 0x91002021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 376 0
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0
.loc 5 249 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2804461
bl _p_24
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_20:
.text
ut_33:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_int_uint__
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_int_uint__
System_Numerics_BigInteger__ctor_int_uint__:
.loc 5 380 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xb9801ba0
.word 0xf9400ba1
.word 0xb9000020
.loc 5 381 0
.word 0xf94013a0
.word 0xf9000420
.word 0x91002021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 383 0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_21:
.text
ut_34:
add x0, x0, 16
b System_Numerics_BigInteger__ctor_uint___bool
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__ctor_uint___bool
System_Numerics_BigInteger__ctor_uint___bool:
.loc 5 393 0 prologue_end
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xaa0203fa
.word 0xb40010b9
.loc 5 401 0
.word 0xb9801b37
.word 0x14000002
.word 0x510006f7
.word 0x6b1f02ff
.word 0x5400018d
.word 0x510006e0
.word 0x93407c00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x54001089
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x34fffe60
.loc 5 403 0
.word 0x35000317
.loc 5 404 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf9400001
.word 0xf90027a1
.word 0xf9400400
.word 0xf9002ba0
.word 0xf94027a0
.word 0xf9000300
.word 0x91002301
.word 0xf9402ba0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0x14000057
.loc 5 406 0
.word 0xd280003e
.word 0x6b1e02ff
.word 0x540006c1
.word 0xb9801b20
.word 0xeb1f001f
.word 0x10000011
.word 0x54000c09
.word 0xb9402320
.word 0x929ffffe
.word 0xf2b0001e
.word 0x6b1e001f
.word 0x540005a2
.loc 5 408 0
.word 0xaa1803f6
.word 0x350000fa
.word 0xb9801b20
.word 0xeb1f001f
.word 0x10000011
.word 0x54000aa9
.word 0xb940233a
.word 0x14000007
.word 0xb9801b20
.word 0xeb1f001f
.word 0x10000011
.word 0x540009e9
.word 0xb9402320
.word 0x4b0003fa
.word 0xb90002da
.loc 5 409 0
.word 0xf900071f
.loc 5 411 0
.word 0xb9800300
.word 0x929ffffe
.word 0xf2b0001e
.word 0x6b1e001f
.word 0x540006c1
.loc 5 412 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #680]
.word 0xf9400001
.word 0xf9001fa1
.word 0xf9400400
.word 0xf90023a0
.word 0xf9401fa0
.word 0xf9000300
.word 0x91002301
.word 0xf94023a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 413 0
.word 0x1400001f
.loc 5 416 0
.word 0xaa1803f6
.word 0x3500007a
.word 0xd280003a
.word 0x14000003
.word 0x9280001a
.word 0xf2bffffa
.word 0xb90002da
.loc 5 417 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1703e1
bl _p_35
.word 0xf9000700
.word 0x91002301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 418 0
.word 0xf9400702
.word 0xaa1903e0
.word 0xd2800001
.word 0xd2800003
.word 0xaa1703e4
bl _p_45
.loc 5 421 0
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.loc 5 394 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2804461
bl _p_24
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_22:
.text
ut_35:
add x0, x0, 16
b System_Numerics_BigInteger_get_Zero
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_get_Zero
System_Numerics_BigInteger_get_Zero:
.loc 5 530 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf9400001
.word 0xf9000ba1
.word 0xf9400400
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_23:
.text
ut_36:
add x0, x0, 16
b System_Numerics_BigInteger_get_IsPowerOfTwo
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_get_IsPowerOfTwo
System_Numerics_BigInteger_get_IsPowerOfTwo:
.loc 5 542 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa
.word 0xf9400740
.word 0xb5000180
.loc 5 543 0
.word 0xb9800340
.word 0xb9800341
.word 0x51000421
.word 0xa010000
.word 0x350000a0
.word 0xb9800340
.word 0x6b1f001f
.word 0x9a9f97e0
.word 0x14000038
.word 0xd2800000
.word 0x14000036
.loc 5 545 0
.word 0xb9800340
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000060
.loc 5 546 0
.word 0xd2800000
.word 0x14000030
.loc 5 547 0
.word 0xf9400740
.word 0xb9801800
.word 0x51000419
.loc 5 548 0
.word 0xf9400740
.word 0x93407f21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000569
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9400000
.word 0xf9400741
.word 0x93407f22
.word 0xb9801823
.word 0xeb02007f
.word 0x10000011
.word 0x54000429
.word 0xd37ef442
.word 0x8b020021
.word 0x91008021
.word 0xb9400021
.word 0x51000421
.word 0xa010000
.word 0x34000200
.loc 5 549 0
.word 0xd2800000
.word 0x14000014
.loc 5 552 0
.word 0xf9400740
.word 0x93407f21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000249
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9400000
.word 0x34000060
.loc 5 553 0
.word 0xd2800000
.word 0x14000007
.loc 5 550 0
.word 0x51000721
.word 0xaa0103e0
.word 0xaa0103f9
.word 0x6b1f001f
.word 0x54fffdea
.loc 5 555 0
.word 0xd2800020
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_24:
.text
ut_37:
add x0, x0, 16
b System_Numerics_BigInteger_get_IsZero
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_get_IsZero
System_Numerics_BigInteger_get_IsZero:
.loc 5 559 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9400ba0
.word 0xb9800000
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_25:
.text
ut_38:
add x0, x0, 16
b System_Numerics_BigInteger_get_IsOne
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_get_IsOne
System_Numerics_BigInteger_get_IsOne:
.loc 5 561 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9400ba0
.word 0xb9800000
.word 0xd280003e
.word 0x6b1e001f
.word 0x540000c1
.word 0xf9400ba0
.word 0xf9400400
.word 0xeb1f001f
.word 0x9a9f17e0
.word 0x14000002
.word 0xd2800000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_26:
.text
ut_39:
add x0, x0, 16
b System_Numerics_BigInteger_get_IsEven
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_get_IsEven
System_Numerics_BigInteger_get_IsEven:
.loc 5 563 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9400ba0
.word 0xf9400400
.word 0xb40001a0
.word 0xf9400ba0
.word 0xf9400400
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54000209
.word 0xb9402000
.word 0xd280003e
.word 0xa1e0000
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x14000007
.word 0xf9400ba0
.word 0xb9800000
.word 0xd280003e
.word 0xa1e0000
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_27:
.text
ut_40:
add x0, x0, 16
b System_Numerics_BigInteger_get_Sign
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_get_Sign
System_Numerics_BigInteger_get_Sign:
.loc 5 567 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9400ba1
.word 0xb9800020
.word 0x131f7c00
.word 0xb9800021
.word 0x4b0103e1
.word 0x131f7c21
.word 0x4b010000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_28:
.text
ut_41:
add x0, x0, 16
b System_Numerics_BigInteger_Parse_string_System_IFormatProvider
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_Parse_string_System_IFormatProvider
System_Numerics_BigInteger_Parse_string_System_IFormatProvider:
.loc 5 582 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xf90017a1
.word 0xf94017a0
bl _p_46
.word 0xaa0003e2
.word 0x910043a0
.word 0xf9001ba0
.word 0xf94013a0
.word 0xd28000e1
bl _p_47
.word 0xf9401bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_29:
.text
ut_42:
add x0, x0, 16
b System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider
System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider:
.loc 5 587 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xf90017a1
.word 0xf9001ba2
.word 0xf9401ba0
bl _p_46
.word 0xaa0003e2
.word 0x910043a0
.word 0xf9001fa0
.word 0xf94013a0
.word 0xb9802ba1
bl _p_48
.word 0xf9401fbe
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_2a:
.text
ut_43:
add x0, x0, 16
b System_Numerics_BigInteger_GetHashCode
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_GetHashCode
System_Numerics_BigInteger_GetHashCode:
.loc 5 869 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xaa0003fa
.word 0xf9400740
.word 0xb5000060
.loc 5 870 0
.word 0xb9800340
.word 0x14000019
.loc 5 871 0
.word 0xb9800359
.loc 5 872 0
.word 0xf9400740
.word 0xb9801818
.word 0x1400000f
.loc 5 873 0
.word 0xf9400740
.word 0x93407f01
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000289
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9400001
.word 0x53196320
.word 0x53197f22
.word 0x2a020000
.word 0x4a010019
.loc 5 872 0
.word 0x51000701
.word 0xaa0103e0
.word 0xaa0103f8
.word 0x6b1f001f
.word 0x54fffdca
.loc 5 874 0
.word 0xaa1903e0
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_2b:
.text
ut_44:
add x0, x0, 16
b System_Numerics_BigInteger_Equals_object
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_Equals_object
System_Numerics_BigInteger_Equals_object:
.loc 5 881 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fba
.word 0xf90013a0
.word 0xaa0103fa
.word 0xaa1a03f8
.word 0xeb1f035f
.word 0x54000160
.word 0xf9400340
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #720]
.word 0xeb01001f
.word 0x54000040
.word 0xd2800018
.word 0xb5000078
.loc 5 882 0
.word 0xd2800000
.word 0x14000018
.loc 5 883 0
.word 0xf9400340
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x54000301
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #720]
.word 0xeb01001f
.word 0x10000011
.word 0x54000201
.word 0x91004340
.word 0xf9400001
.word 0xf90017a1
.word 0xf9400400
.word 0xf9001ba0
.word 0xf94013a0
.word 0xf94017a1
.word 0xf9401ba2
bl _p_49
.word 0x53001c00
.word 0xf9400bb8
.word 0xf9400fba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_6

Lme_2c:
.text
ut_45:
add x0, x0, 16
b System_Numerics_BigInteger_Equals_long
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_Equals_long
System_Numerics_BigInteger_Equals_long:
.loc 5 890 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bb7
.word 0xa901ebb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xf9400720
.word 0xb50000c0
.loc 5 891 0
.word 0xb9800320
.word 0x93407c00
.word 0xeb1a001f
.word 0x9a9f17e0
.word 0x14000036
.loc 5 894 0
.word 0xb9800320
.word 0x93407c00
.word 0xca1a0000
.word 0xeb1f001f
.word 0x5400010b
.word 0xf9400720
.word 0xb9801801
.word 0xaa0103e0
.word 0xb9002ba1
.word 0xd280005e
.word 0x6b1e001f
.word 0x5400006d
.loc 5 895 0
.word 0xd2800000
.word 0x14000028
.loc 5 897 0
.word 0xeb1f035f
.word 0x5400006b
.word 0xaa1a03f7
.word 0x14000002
.word 0xcb1a03f7
.word 0xaa1703fa
.loc 5 898 0
.word 0xb9802ba0
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000161
.loc 5 899 0
.word 0xf9400720
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x540003c9
.word 0xb9402000
.word 0x2a0003e0
.word 0xeb1a001f
.word 0x9a9f17e0
.word 0x14000014
.loc 5 901 0
.word 0xf9400720
.word 0xb9801801
.word 0xd280003e
.word 0xeb1e003f
.word 0x10000011
.word 0x54000269
.word 0xb9402400
.word 0xf9400721
.word 0xb9801822
.word 0xeb1f005f
.word 0x10000011
.word 0x540001a9
.word 0xb9402021
.word 0x2a0003e0
.word 0xd3607c00
.word 0x2a0103e1
.word 0xaa010000
.word 0xeb1a001f
.word 0x9a9f17e0
.word 0xf9400bb7
.word 0xa941ebb9
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_2d:
.text
ut_46:
add x0, x0, 16
b System_Numerics_BigInteger_Equals_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_Equals_System_Numerics_BigInteger
System_Numerics_BigInteger_Equals_System_Numerics_BigInteger:
.loc 5 927 0 prologue_end
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa
.word 0xf90013a1
.word 0xf90017a2
.word 0xb9800340
.word 0xf94013a1
.word 0xf9003ba1
.word 0xf94017a1
.word 0xf9003fa1
.word 0xb98073a1
.word 0x6b01001f
.word 0x54000060
.loc 5 928 0
.word 0xd2800000
.word 0x1400002d
.loc 5 929 0
.word 0xf9400740
.word 0xf94013a1
.word 0xf90033a1
.word 0xf94017a1
.word 0xf90037a1
.word 0xf94037a1
.word 0xeb01001f
.word 0x54000061
.loc 5 931 0
.word 0xd2800020
.word 0x14000023
.loc 5 933 0
.word 0xf9400740
.word 0xb40000e0
.word 0xf94013a0
.word 0xf9002ba0
.word 0xf94017a0
.word 0xf9002fa0
.word 0xf9402fa0
.word 0xb5000060
.loc 5 934 0
.word 0xd2800000
.word 0x14000019
.loc 5 935 0
.word 0xf9400740
.word 0xb9801819
.loc 5 936 0
.word 0xaa1903e0
.word 0xf94013a1
.word 0xf90023a1
.word 0xf94017a1
.word 0xf90027a1
.word 0xf94027a1
.word 0xb9801821
.word 0x6b01001f
.word 0x54000060
.loc 5 937 0
.word 0xd2800000
.word 0x1400000c
.loc 5 938 0
.word 0xf9400740
.word 0xf94013a1
.word 0xf9001ba1
.word 0xf94017a1
.word 0xf9001fa1
.word 0xf9401fa1
.word 0xaa1903e2
bl _p_50
.word 0x93407c00
.loc 5 939 0
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_2e:
.text
ut_47:
add x0, x0, 16
b System_Numerics_BigInteger_CompareTo_long
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_CompareTo_long
System_Numerics_BigInteger_CompareTo_long:
.loc 5 946 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xf9001bbf
.word 0xf9001fbf
.word 0xf9400720
.word 0xb5000280
.loc 5 947 0
.word 0xb9800320
.word 0x93407c00
.word 0xf9001fa0
.word 0x9100e3b9
.word 0xaa1a03f8
.word 0xf9401fa0
.word 0xeb1a001f
.word 0x5400008a
.word 0x9280001a
.word 0xf2bffffa
.word 0x14000007
.word 0xf9400320
.word 0xeb18001f
.word 0x5400006d
.word 0xd280003a
.word 0x14000002
.word 0xd280001a
.word 0xaa1a03e0
.word 0x14000042
.loc 5 949 0
.word 0xb9800320
.word 0x93407c00
.word 0xca1a0000
.word 0xeb1f001f
.word 0x5400010b
.word 0xf9400720
.word 0xb9801801
.word 0xaa0103e0
.word 0xaa0103f8
.word 0xd280005e
.word 0x6b1e001f
.word 0x5400006d
.loc 5 950 0
.word 0xb9800320
.word 0x14000034
.loc 5 951 0
.word 0xeb1f035f
.word 0x5400006b
.word 0xaa1a03f7
.word 0x14000002
.word 0xcb1a03f7
.word 0xaa1703fa
.loc 5 952 0
.word 0xd280005e
.word 0x6b1e031f
.word 0x54000120
.word 0xf9400720
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54000569
.word 0xb9402000
.word 0x2a0003f7
.word 0x14000012
.word 0xf9400720
.word 0xb9801801
.word 0xd280003e
.word 0xeb1e003f
.word 0x10000011
.word 0x54000449
.word 0xb9402400
.word 0xf9400721
.word 0xb9801822
.word 0xeb1f005f
.word 0x10000011
.word 0x54000389
.word 0xb9402021
.word 0x2a0003e0
.word 0xd3607c00
.word 0x2a0103e1
.word 0xaa010017
.word 0xf9001bb7
.loc 5 953 0
.word 0xb9800338
.word 0x9100c3b9
.word 0xaa1a03f7
.word 0xf9401ba0
.word 0xeb1a001f
.word 0x54000082
.word 0x9280001a
.word 0xf2bffffa
.word 0x14000007
.word 0xf9400320
.word 0xeb17001f
.word 0x54000069
.word 0xd280003a
.word 0x14000002
.word 0xd280001a
.word 0x1b1a7f00
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_2f:
.text
ut_48:
add x0, x0, 16
b System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger
System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger:
.loc 5 977 0 prologue_end
.word 0xa9b37bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa
.word 0xf90013a1
.word 0xf90017a2
.word 0xb9800340
.word 0xf94013a1
.word 0xf9005ba1
.word 0xf94017a1
.word 0xf9005fa1
.word 0xb980b3a1
.word 0x4a010000
.word 0x6b1f001f
.word 0x5400012a
.loc 5 980 0
.word 0xb9800340
.word 0x6b1f001f
.word 0x5400006b
.word 0xd2800020
.word 0x14000074
.word 0x92800000
.word 0xf2bfffe0
.word 0x14000071
.loc 5 984 0
.word 0xf9400740
.word 0xb50004a0
.loc 5 986 0
.word 0xf94013a0
.word 0xf90053a0
.word 0xf94017a0
.word 0xf90057a0
.word 0xf94057a0
.word 0xb5000300
.loc 5 987 0
.word 0xb9800340
.word 0xf94013a1
.word 0xf9004ba1
.word 0xf94017a1
.word 0xf9004fa1
.word 0xb98093a1
.word 0x6b01001f
.word 0x540001ab
.word 0xb9800340
.word 0xf94013a1
.word 0xf90043a1
.word 0xf94017a1
.word 0xf90047a1
.word 0xb98083a1
.word 0x6b01001f
.word 0x5400006c
.word 0xd2800000
.word 0x14000057
.word 0xd2800020
.word 0x14000055
.word 0x92800000
.word 0xf2bfffe0
.word 0x14000052
.loc 5 988 0
.word 0xf94013a0
.word 0xf9003ba0
.word 0xf94017a0
.word 0xf9003fa0
.word 0xb98073a0
.word 0x4b0003e0
.word 0x1400004b
.loc 5 991 0
.word 0xf94013a0
.word 0xf90033a0
.word 0xf94017a0
.word 0xf90037a0
.word 0xf94037a0
.word 0xb40001e0
.word 0xf9400740
.word 0xb9801801
.word 0xaa0103e0
.word 0xaa0103f9
.word 0xf94013a1
.word 0xf9002ba1
.word 0xf94017a1
.word 0xf9002fa1
.word 0xf9402fa1
.word 0xb9801822
.word 0xaa0203e1
.word 0xb900c3a2
.word 0x6b01001f
.word 0x5400006d
.loc 5 992 0
.word 0xb9800340
.word 0x14000035
.loc 5 993 0
.word 0xb980c3a0
.word 0x6b00033f
.word 0x5400008a
.loc 5 994 0
.word 0xb9800340
.word 0x4b0003e0
.word 0x1400002f
.loc 5 996 0
.word 0xf9400740
.word 0xf94013a1
.word 0xf90023a1
.word 0xf94017a1
.word 0xf90027a1
.word 0xf94027a1
.word 0xaa1903e2
bl _p_50
.word 0x93407c00
.word 0xaa0003f9
.loc 5 997 0
.word 0xaa1903e0
.word 0x35000060
.loc 5 998 0
.word 0xd2800000
.word 0x14000021
.loc 5 999 0
.word 0xf9400740
.word 0x51000721
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x540003c9
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9400000
.word 0xf94013a1
.word 0xf9001ba1
.word 0xf94017a1
.word 0xf9001fa1
.word 0xf9401fa1
.word 0x51000722
.word 0x93407c42
.word 0xb9801823
.word 0xeb02007f
.word 0x10000011
.word 0x540001e9
.word 0xd37ef442
.word 0x8b020021
.word 0x91008021
.word 0xb9400021
.word 0x6b01001f
.word 0x54000063
.word 0xb9800340
.word 0x14000003
.word 0xb9800340
.word 0x4b0003e0
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_30:
.text
ut_49:
add x0, x0, 16
b System_Numerics_BigInteger_CompareTo_object
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_CompareTo_object
System_Numerics_BigInteger_CompareTo_object:
.loc 5 1004 0 prologue_end
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fba
.word 0xf90013a0
.word 0xaa0103fa
.word 0xb500007a
.loc 5 1005 0
.word 0xd2800020
.word 0x14000026
.loc 5 1006 0
.word 0xaa1a03f8
.word 0xeb1f035f
.word 0x54000160
.word 0xf9400340
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #720]
.word 0xeb01001f
.word 0x54000040
.word 0xd2800018
.word 0xb40003b8
.loc 5 1008 0
.word 0xf9400340
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x54000521
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #720]
.word 0xeb01001f
.word 0x10000011
.word 0x54000421
.word 0x91004340
.word 0xf9400001
.word 0xf90017a1
.word 0xf9400400
.word 0xf9001ba0
.word 0xf94013a0
.word 0xf94017a1
.word 0xf9401ba2
bl _p_51
.word 0x93407c00
.word 0xf9400bb8
.word 0xf9400fba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0
.loc 5 1007 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28045e1
bl _p_24
.word 0xf90023a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2804ee1
bl _p_24
.word 0xaa0003e2
.word 0xf94023a1
.word 0xd2801140
.word 0xf2a04000
bl _mono_create_corlib_exception_2
bl _p_25
.word 0xd2801c60
.word 0xaa1103e1
bl _p_6

Lme_31:
.text
ut_50:
add x0, x0, 16
b System_Numerics_BigInteger_ToByteArray
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_ToByteArray
System_Numerics_BigInteger_ToByteArray:
.loc 5 1019 0 prologue_end
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003fa
.word 0xb9800340
.word 0xb90053a0
.loc 5 1020 0
.word 0x350000e0
.loc 5 1022 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #728]
.word 0xd2800021
bl _p_35
.word 0x140000e9
.loc 5 1026 0
.word 0xb9005bbf
.loc 5 1028 0
.word 0xf9400757
.loc 5 1029 0
.word 0xaa1703e0
.word 0xb5000180
.loc 5 1031 0
.word 0xb98053a0
.word 0x6b1f001f
.word 0x5400006b
.word 0xb90063bf
.word 0x14000003
.word 0xd2801ffe
.word 0xb90063be
.word 0xb98063a0
.word 0x53001c1a
.loc 5 1032 0
.word 0xb98053b6
.loc 5 1033 0
.word 0x14000035
.loc 5 1034 0
.word 0xb98053a0
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x54000481
.loc 5 1036 0
.word 0xd2801ffa
.word 0x14000004
.loc 5 1051 0
.word 0xb9805ba0
.word 0x11000400
.word 0xb9005ba0
.loc 5 1049 0
.word 0xb9805ba0
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54001a29
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9400000
.word 0x34fffe60
.loc 5 1054 0
.word 0xb9801ae0
.word 0x51000400
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x540018a9
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9400000
.word 0x2a2003f6
.loc 5 1055 0
.word 0xb9801ae0
.word 0x51000400
.word 0xb9805ba1
.word 0x6b01001f
.word 0x540001e1
.loc 5 1059 0
.word 0x110006d6
.loc 5 1061 0
.word 0x1400000d
.loc 5 1065 0
.word 0xd280001a
.loc 5 1066 0
.word 0xb9801ae0
.word 0x51000400
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54001629
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9400016
.loc 5 1071 0
.word 0x53187ec0
.word 0x53001c01
.word 0xaa0103e0
.word 0xaa0103f5
.word 0x6b1a001f
.word 0x54000060
.loc 5 1073 0
.word 0xd2800074
.loc 5 1074 0
.word 0x14000013
.loc 5 1075 0
.word 0x53107ec0
.word 0x53001c01
.word 0xaa0103e0
.word 0xaa0103f5
.word 0x6b1a001f
.word 0x54000060
.loc 5 1077 0
.word 0xd2800054
.loc 5 1078 0
.word 0x1400000b
.loc 5 1079 0
.word 0x53087ec0
.word 0x53001c01
.word 0xaa0103e0
.word 0xaa0103f5
.word 0x6b1a001f
.word 0x54000060
.loc 5 1081 0
.word 0xd2800034
.loc 5 1082 0
.word 0x14000003
.loc 5 1085 0
.word 0x53001ed5
.loc 5 1086 0
.word 0xd2800014
.loc 5 1090 0
.word 0xd280101e
.word 0xa1e02a0
.word 0xd280101e
.word 0xa1e0341
.word 0x6b01001f
.word 0x9a9f17e0
.word 0x6b1f001f
.word 0x9a9f17f5
.loc 5 1092 0
.word 0xd2800013
.loc 5 1093 0
.word 0xb5000237
.loc 5 1095 0
.word 0x11000680
.word 0xb90063a0
.word 0x35000075
.word 0xb9006bbf
.word 0x14000003
.word 0xd280003e
.word 0xb9006bbe
.word 0xb98063a0
.word 0xb9806ba1
.word 0xb010001

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #728]
bl _p_35
.word 0xf9003ba0
.loc 5 1097 0
.word 0x14000054
.loc 5 1100 0
.word 0xb9801ae0
.word 0xd2800021
.word 0x6b010001
.word 0x10000011
.word 0x54000ee6
.word 0xd2800080
bl _p_52
.word 0x93407c00
.word 0x2b140000
.word 0x10000011
.word 0x54000e26
.word 0xd2800021
.word 0x2b010000
.word 0x10000011
.word 0x54000da6
.word 0xb90063a0
.word 0x35000075
.word 0xb9006bbf
.word 0x14000003
.word 0xd280003e
.word 0xb9006bbe
.word 0xb98063a0
.word 0xb9806ba1
.word 0x2b010001
.word 0x10000011
.word 0x54000c46

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #728]
bl _p_35
.word 0xf9003ba0
.loc 5 1102 0
.word 0xd2800019
.word 0x1400002f
.loc 5 1104 0
.word 0x93407f20
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54000a69
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9400000
.word 0xb9007ba0
.loc 5 1105 0
.word 0xb98053a0
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x54000141
.loc 5 1107 0
.word 0xb9407ba0
.word 0x2a2003e0
.word 0xb9007ba0
.loc 5 1108 0
.word 0xb9805ba0
.word 0x6b00033f
.word 0x5400008c
.loc 5 1110 0
.word 0xb9407ba0
.word 0x11000400
.word 0xb9007ba0
.loc 5 1113 0
.word 0xd2800018
.word 0x14000011
.loc 5 1115 0
.word 0xaa1303e0
.word 0x11000673
.word 0x93407c01
.word 0xf9403ba0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x540006c9
.word 0x8b010000
.word 0x91008000
.word 0xb9407ba1
.word 0x39000001
.loc 5 1116 0
.word 0xb9407ba0
.word 0x53087c00
.word 0xb9007ba0
.loc 5 1113 0
.word 0x11000718
.word 0xd280009e
.word 0x6b1e031f
.word 0x54fffdcb
.loc 5 1102 0
.word 0x11000739
.word 0xb9801ae0
.word 0x51000400
.word 0x6b00033f
.word 0x54fff9eb
.loc 5 1120 0
.word 0xd2800019
.word 0x1400000e
.loc 5 1122 0
.word 0xaa1303e0
.word 0x11000673
.word 0x93407c01
.word 0xf9403ba0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000389
.word 0x8b010000
.word 0x91008000
.word 0x39000016
.loc 5 1123 0
.word 0x53087ed6
.loc 5 1120 0
.word 0x11000739
.word 0x6b14033f
.word 0x54fffe4d
.loc 5 1125 0
.word 0x34000195
.loc 5 1127 0
.word 0xf9403ba0
.word 0xb9801801
.word 0x51000421
.word 0x93407c21
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000189
.word 0x8b010000
.word 0x91008000
.word 0x3900001a
.loc 5 1129 0
.word 0xf9403ba0
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_32:
.text
ut_51:
add x0, x0, 16
b System_Numerics_BigInteger_ToUInt32Array
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_ToUInt32Array
System_Numerics_BigInteger_ToUInt32Array:
.loc 5 1140 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003fa
.word 0xf9400740
.word 0xb5000120
.word 0xb9800340
.word 0x350000e0
.loc 5 1141 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800021
bl _p_35
.word 0x14000083
.loc 5 1146 0
.word 0xf9400740
.word 0xb5000300
.loc 5 1148 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800021
bl _p_35
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xb9800342
.word 0xb9801823
.word 0xeb1f007f
.word 0x10000011
.word 0x54000f69
.word 0xb9002022
.word 0xaa0003f9
.loc 5 1149 0
.word 0xb9800340
.word 0x6b1f001f
.word 0x5400006b
.word 0xd2800016
.word 0x14000003
.word 0x92800016
.word 0xf2bffff6
.word 0xaa1603fa
.loc 5 1150 0
.word 0x14000028
.loc 5 1151 0
.word 0xb9800340
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x54000421
.loc 5 1153 0
.word 0xf9400741
.word 0xaa0103e0
.word 0xf940003e
bl _p_53
.word 0xaa0003fa
.word 0xb40002ba
.word 0xf9400340
.word 0xf9400000
.word 0x39406801
.word 0xd280003e
.word 0xeb1e003f
.word 0x10000011
.word 0x54000c21
.word 0xf9400400
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #736]
.word 0xeb01001f
.word 0x10000011
.word 0x54000b01
.word 0xf9400b40
.word 0xeb1f001f
.word 0x10000011
.word 0x54000a81
.word 0xaa1a03f9
.loc 5 1154 0
.word 0xaa1a03e0
bl _p_44
.loc 5 1155 0
.word 0x9280001a
.word 0xf2bffffa
.loc 5 1156 0
.word 0x14000003
.loc 5 1159 0
.word 0xf9400759
.loc 5 1160 0
.word 0xd280001a
.loc 5 1165 0
.word 0xb9801b20
.word 0x51000418
.word 0x1400000d
.loc 5 1167 0
.word 0x93407f00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x54000829
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x6b1a001f
.word 0x54000081
.loc 5 1165 0
.word 0x51000718
.word 0x6b1f031f
.word 0x54fffe6c
.loc 5 1170 0
.word 0x93407f00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x54000669
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x929ffffe
.word 0xf2b0001e
.word 0xa1e0000
.word 0x929ffffe
.word 0xf2b0001e
.word 0xa1e0341
.word 0x6b01001f
.word 0x9a9f17e0
.word 0x6b1f001f
.word 0x9a9f17f7
.loc 5 1172 0
.word 0x11000716
.word 0x35000077
.word 0xd2800015
.word 0x14000002
.word 0xd2800035
.word 0xb1502c1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xaa0003f6
.loc 5 1173 0
.word 0x11000704
.word 0xaa1903e0
.word 0xd2800001
.word 0xaa1603e2
.word 0xd2800003
bl _p_45
.loc 5 1175 0
.word 0x34000197
.word 0xb9801ac0
.word 0x51000400
.word 0x93407c00
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54000189
.word 0xd37ef400
.word 0x8b0002c0
.word 0x91008000
.word 0xb900001a
.loc 5 1176 0
.word 0xaa1603e0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6
.word 0xd2801c60
.word 0xaa1103e1
bl _p_6

Lme_33:
.text
ut_52:
add x0, x0, 16
b System_Numerics_BigInteger_ToString
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_ToString
System_Numerics_BigInteger_ToString:
.loc 5 1181 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9400ba0
.word 0xf9400001
.word 0xf9000fa1
.word 0xf9400400
.word 0xf90013a0
bl _p_54
.word 0xaa0003e3
.word 0xf9400fa0
.word 0xf94013a1
.word 0xd2800002
bl _p_55
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_34:
.text
ut_53:
add x0, x0, 16
b System_Numerics_BigInteger_ToString_System_IFormatProvider
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_ToString_System_IFormatProvider
System_Numerics_BigInteger_ToString_System_IFormatProvider:
.loc 5 1186 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf9400001
.word 0xf90013a1
.word 0xf9400400
.word 0xf90017a0
.word 0xf9400fa0
bl _p_46
.word 0xaa0003e3
.word 0xf94013a0
.word 0xf94017a1
.word 0xd2800002
bl _p_55
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_35:
.text
ut_54:
add x0, x0, 16
b System_Numerics_BigInteger_ToString_string_System_IFormatProvider
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_ToString_string_System_IFormatProvider
System_Numerics_BigInteger_ToString_string_System_IFormatProvider:
.loc 5 1196 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf9400ba0
.word 0xf9400001
.word 0xf90017a1
.word 0xf9400400
.word 0xf9001ba0
.word 0xf94013a0
bl _p_46
.word 0xaa0003e3
.word 0xf94017a0
.word 0xf9401ba1
.word 0xf9400fa2
bl _p_55
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_36:
.text
ut_55:
add x0, x0, 16
b System_Numerics_BigInteger_Add_uint___int_uint___int
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_Add_uint___int_uint___int
System_Numerics_BigInteger_Add_uint___int_uint___int:
.loc 5 1201 0 prologue_end
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xf9001bb9
.word 0xaa0003f7
.word 0xaa0103f8
.word 0xaa0203f9
.word 0xf90027a3
.word 0xeb1f02ff
.word 0x9a9f17f6
.loc 5 1202 0
.word 0xeb1f033f
.word 0x9a9f17f5
.loc 5 1204 0
.word 0xa1502c0
.word 0x34000180
.loc 5 1206 0
.word 0x93407f00
.word 0xb9804ba1
.word 0x93407c21
.word 0x8b010000
.word 0x9100e3a1
.word 0xf9004ba1
bl _p_56
.word 0xf9404bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0x1400004b
.loc 5 1209 0
.word 0x34000276
.loc 5 1211 0
.word 0x131f7f01
.word 0x4a010300
.word 0x4b010001
.word 0xaa1903e0
bl _p_57
.word 0xaa0003e1
.loc 5 1212 0
.word 0x6b1f031f
.word 0x9a9fa7e2
.word 0xd2800000
.word 0xf90043a0
.word 0xf90047a0
.word 0x910203a0
bl _p_58
.word 0xf94043a0
.word 0xf9001fa0
.word 0xf94047a0
.word 0xf90023a0
.word 0x14000038
.loc 5 1215 0
.word 0x34000295
.loc 5 1217 0
.word 0xb9804ba0
.word 0x131f7c01
.word 0x4a010000
.word 0x4b010001
.word 0xaa1703e0
bl _p_57
.word 0xaa0003e1
.loc 5 1218 0
.word 0x6b1f031f
.word 0x9a9fa7e2
.word 0xd2800000
.word 0xf9003ba0
.word 0xf9003fa0
.word 0x9101c3a0
bl _p_58
.word 0xf9403ba0
.word 0xf9001fa0
.word 0xf9403fa0
.word 0xf90023a0
.word 0x14000024
.loc 5 1221 0
.word 0xb9801ae0
.word 0xb9801b21
.word 0x6b01001f
.word 0x5400022a
.loc 5 1223 0
.word 0xaa1903e0
.word 0xaa1703e1
bl _p_59
.word 0xaa0003e1
.loc 5 1224 0
.word 0x6b1f031f
.word 0x9a9fa7e2
.word 0xd2800000
.word 0xf90033a0
.word 0xf90037a0
.word 0x910183a0
bl _p_58
.word 0xf94033a0
.word 0xf9001fa0
.word 0xf94037a0
.word 0xf90023a0
.word 0x14000010
.loc 5 1228 0
.word 0xaa1703e0
.word 0xaa1903e1
bl _p_59
.word 0xaa0003e1
.loc 5 1229 0
.word 0x6b1f031f
.word 0x9a9fa7e2
.word 0xd2800000
.word 0xf9002ba0
.word 0xf9002fa0
.word 0x910143a0
bl _p_58
.word 0xf9402ba0
.word 0xf9001fa0
.word 0xf9402fa0
.word 0xf90023a0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xf9401bb9
.word 0xf9401fa0
.word 0xf94023a1
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_37:
.text
ut_56:
add x0, x0, 16
b System_Numerics_BigInteger_op_Subtraction_System_Numerics_BigInteger_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Subtraction_System_Numerics_BigInteger_System_Numerics_BigInteger
System_Numerics_BigInteger_op_Subtraction_System_Numerics_BigInteger_System_Numerics_BigInteger:
.loc 5 1238 0 prologue_end
.word 0xa9b17bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xf90017a1
.word 0xf9001ba2
.word 0xf9001fa3
.word 0xf94013a0
.word 0xf9006ba0
.word 0xf94017a0
.word 0xf9006fa0
.word 0xb980d3a0
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0xf9401ba1
.word 0xf90063a1
.word 0xf9401fa1
.word 0xf90067a1
.word 0xb980c3a1
.word 0x6b1f003f
.word 0x9a9fa7e1
.word 0x6b01001f
.word 0x540003a0
.loc 5 1239 0
.word 0xf94013a0
.word 0xf9005ba0
.word 0xf94017a0
.word 0xf9005fa0
.word 0xf9405fa0
.word 0xf94013a1
.word 0xf90053a1
.word 0xf94017a1
.word 0xf90057a1
.word 0xb980a3a1
.word 0xf9401ba2
.word 0xf9004ba2
.word 0xf9401fa2
.word 0xf9004fa2
.word 0xf9404fa2
.word 0xf9401ba3
.word 0xf90043a3
.word 0xf9401fa3
.word 0xf90047a3
.word 0xb98083a3
.word 0x4b0303e3
.word 0x910043a4
.word 0xf90073a4
bl _p_60
.word 0xf94073be
.word 0xf90003c0
.word 0xf90007c1
.word 0x1400001b
.loc 5 1240 0
.word 0xf94013a0
.word 0xf9003ba0
.word 0xf94017a0
.word 0xf9003fa0
.word 0xf9403fa0
.word 0xf94013a1
.word 0xf90033a1
.word 0xf94017a1
.word 0xf90037a1
.word 0xb98063a1
.word 0xf9401ba2
.word 0xf9002ba2
.word 0xf9401fa2
.word 0xf9002fa2
.word 0xf9402fa2
.word 0xf9401ba3
.word 0xf90023a3
.word 0xf9401fa3
.word 0xf90027a3
.word 0xb98043a3
.word 0x910043a4
.word 0xf90073a4
bl _p_61
.word 0xf94073be
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0

Lme_38:
.text
ut_57:
add x0, x0, 16
b System_Numerics_BigInteger_Subtract_uint___int_uint___int
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_Subtract_uint___int_uint___int
System_Numerics_BigInteger_Subtract_uint___int_uint___int:
.loc 5 1245 0 prologue_end
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xf9001bb9
.word 0xaa0003f7
.word 0xaa0103f8
.word 0xaa0203f9
.word 0xf90027a3
.word 0xeb1f02ff
.word 0x9a9f17f6
.loc 5 1246 0
.word 0xeb1f033f
.word 0x9a9f17f5
.loc 5 1248 0
.word 0xa1502c0
.word 0x34000180
.loc 5 1250 0
.word 0x93407f00
.word 0xb9804ba1
.word 0x93407c21
.word 0xcb010000
.word 0x9100e3a1
.word 0xf9004ba1
bl _p_56
.word 0xf9404bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0x14000051
.loc 5 1253 0
.word 0x340002b6
.loc 5 1255 0
.word 0x131f7f01
.word 0x4a010300
.word 0x4b010001
.word 0xaa1903e0
bl _p_62
.word 0xaa0003e1
.loc 5 1256 0
.word 0x6b1f031f
.word 0x9a9fa7e0
.word 0x6b1f001f
.word 0x9a9f17e2
.word 0xd2800000
.word 0xf90043a0
.word 0xf90047a0
.word 0x910203a0
bl _p_58
.word 0xf94043a0
.word 0xf9001fa0
.word 0xf94047a0
.word 0xf90023a0
.word 0x1400003c
.loc 5 1259 0
.word 0x34000295
.loc 5 1261 0
.word 0xb9804ba0
.word 0x131f7c01
.word 0x4a010000
.word 0x4b010001
.word 0xaa1703e0
bl _p_62
.word 0xaa0003e1
.loc 5 1262 0
.word 0x6b1f031f
.word 0x9a9fa7e2
.word 0xd2800000
.word 0xf9003ba0
.word 0xf9003fa0
.word 0x9101c3a0
bl _p_58
.word 0xf9403ba0
.word 0xf9001fa0
.word 0xf9403fa0
.word 0xf90023a0
.word 0x14000028
.loc 5 1265 0
.word 0xaa1703e0
.word 0xaa1903e1
bl _p_63
.word 0x93407c00
.word 0x6b1f001f
.word 0x5400026a
.loc 5 1267 0
.word 0xaa1903e0
.word 0xaa1703e1
bl _p_64
.word 0xaa0003e1
.loc 5 1268 0
.word 0x6b1f031f
.word 0x9a9fa7e0
.word 0x6b1f001f
.word 0x9a9f17e2
.word 0xd2800000
.word 0xf90033a0
.word 0xf90037a0
.word 0x910183a0
bl _p_58
.word 0xf94033a0
.word 0xf9001fa0
.word 0xf94037a0
.word 0xf90023a0
.word 0x14000010
.loc 5 1272 0
.word 0xaa1703e0
.word 0xaa1903e1
bl _p_64
.word 0xaa0003e1
.loc 5 1273 0
.word 0x6b1f031f
.word 0x9a9fa7e2
.word 0xd2800000
.word 0xf9002ba0
.word 0xf9002fa0
.word 0x910143a0
bl _p_58
.word 0xf9402ba0
.word 0xf9001fa0
.word 0xf9402fa0
.word 0xf90023a0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xf9401bb9
.word 0xf9401fa0
.word 0xf94023a1
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_39:
.text
ut_58:
add x0, x0, 16
b System_Numerics_BigInteger_op_Implicit_byte
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Implicit_byte
System_Numerics_BigInteger_op_Implicit_byte:
.loc 5 1279 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xd2800000
.word 0xf90017a0
.word 0xf9001ba0
.word 0x9100a3a0
.word 0x394083a1
bl _p_65
.word 0xf94017a0
.word 0xf9000ba0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3a:
.text
ut_59:
add x0, x0, 16
b System_Numerics_BigInteger_op_Implicit_sbyte
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Implicit_sbyte
System_Numerics_BigInteger_op_Implicit_sbyte:
.loc 5 1285 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xd2800000
.word 0xf90017a0
.word 0xf9001ba0
.word 0x9100a3a0
.word 0x398083a1
bl _p_65
.word 0xf94017a0
.word 0xf9000ba0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3b:
.text
ut_60:
add x0, x0, 16
b System_Numerics_BigInteger_op_Implicit_int16
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Implicit_int16
System_Numerics_BigInteger_op_Implicit_int16:
.loc 5 1290 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xd2800000
.word 0xf90017a0
.word 0xf9001ba0
.word 0x9100a3a0
.word 0x798043a1
bl _p_65
.word 0xf94017a0
.word 0xf9000ba0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3c:
.text
ut_61:
add x0, x0, 16
b System_Numerics_BigInteger_op_Implicit_uint16
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Implicit_uint16
System_Numerics_BigInteger_op_Implicit_uint16:
.loc 5 1296 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xd2800000
.word 0xf90017a0
.word 0xf9001ba0
.word 0x9100a3a0
.word 0x794043a1
bl _p_65
.word 0xf94017a0
.word 0xf9000ba0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3d:
.text
ut_62:
add x0, x0, 16
b System_Numerics_BigInteger_op_Implicit_int
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Implicit_int
System_Numerics_BigInteger_op_Implicit_int:
.loc 5 1301 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xd2800000
.word 0xf90017a0
.word 0xf9001ba0
.word 0x9100a3a0
.word 0xb98023a1
bl _p_65
.word 0xf94017a0
.word 0xf9000ba0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3e:
.text
ut_63:
add x0, x0, 16
b System_Numerics_BigInteger_op_Implicit_uint
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Implicit_uint
System_Numerics_BigInteger_op_Implicit_uint:
.loc 5 1307 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xd2800000
.word 0xf90017a0
.word 0xf9001ba0
.word 0x9100a3a0
.word 0xb94023a1
bl _p_66
.word 0xf94017a0
.word 0xf9000ba0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3f:
.text
ut_64:
add x0, x0, 16
b System_Numerics_BigInteger_op_Implicit_long
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Implicit_long
System_Numerics_BigInteger_op_Implicit_long:
.loc 5 1312 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xd2800000
.word 0xf90017a0
.word 0xf9001ba0
.word 0x9100a3a0
.word 0xf94013a1
bl _p_67
.word 0xf94017a0
.word 0xf9000ba0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_40:
.text
ut_65:
add x0, x0, 16
b System_Numerics_BigInteger_op_Implicit_ulong
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Implicit_ulong
System_Numerics_BigInteger_op_Implicit_ulong:
.loc 5 1318 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xd2800000
.word 0xf90017a0
.word 0xf9001ba0
.word 0x9100a3a0
.word 0xf94013a1
bl _p_68
.word 0xf94017a0
.word 0xf9000ba0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_41:
.text
ut_66:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger:
.loc 5 1338 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf9400fa1
bl _p_69
.word 0x93407c00
.word 0xd2801ffe
.word 0xeb1e001f
.word 0x10000011
.word 0x540000c8
.word 0xd2801ffe
.word 0xa1e0000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_42:
.text
ut_67:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0:
.loc 5 1344 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf9400fa1
bl _p_69
.word 0x93407c00
.word 0xd2800ffe
.word 0x6b1e001f
.word 0x10000011
.word 0x5400014c
.word 0x92800ffe
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x10000011
.word 0x540000ab
.word 0x93401c00
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_43:
.text
ut_68:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_1
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_1
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_1:
.loc 5 1349 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf9400fa1
bl _p_69
.word 0x93407c00
.word 0xd28ffffe
.word 0x6b1e001f
.word 0x10000011
.word 0x5400014c
.word 0x928ffffe
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x10000011
.word 0x540000ab
.word 0x93403c00
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_44:
.text
ut_69:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_2
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_2
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_2:
.loc 5 1355 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf9400fa1
bl _p_69
.word 0x93407c00
.word 0xd29ffffe
.word 0x6b1e001f
.word 0x10000011
.word 0x540000c8
.word 0xd29ffffe
.word 0xa1e0000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_45:
.text
ut_70:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_3
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_3
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_3:
.loc 5 1361 0 prologue_end
.word 0xa9b77bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf90043a0
.word 0xf9400fa0
.word 0xf90047a0
.word 0xf94047a0
.word 0xb50000e0
.loc 5 1363 0
.word 0xf9400ba0
.word 0xf9003ba0
.word 0xf9400fa0
.word 0xf9003fa0
.word 0xb98073a0
.word 0x14000038
.loc 5 1365 0
.word 0xf9400ba0
.word 0xf90033a0
.word 0xf9400fa0
.word 0xf90037a0
.word 0xf94037a0
.word 0xb9801800
.word 0xd280003e
.word 0x6b1e001f
.word 0x5400064c
.loc 5 1370 0
.word 0xf9400ba0
.word 0xf9002ba0
.word 0xf9400fa0
.word 0xf9002fa0
.word 0xb98053a0
.word 0x6b1f001f
.word 0x540001ed
.loc 5 1372 0
.word 0xf9400ba0
.word 0xf90023a0
.word 0xf9400fa0
.word 0xf90027a0
.word 0xf94027a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x540006c9
.word 0xb9402000
.word 0x6b1f001f
.word 0x10000011
.word 0x540006ab
.word 0x1400001a
.loc 5 1374 0
.word 0xf9400ba0
.word 0xf9001ba0
.word 0xf9400fa0
.word 0xf9001fa0
.word 0xf9401fa0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54000509
.word 0xb9402000
.word 0x929ffffe
.word 0xf2b0001e
.word 0x6b1e001f
.word 0x54000328
.loc 5 1379 0
.word 0xf9400ba0
.word 0xf90013a0
.word 0xf9400fa0
.word 0xf90017a0
.word 0xf94017a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54000349
.word 0xb9402000
.word 0x4b0003e0
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0
.loc 5 1368 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2804fe1
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.loc 5 1377 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2804fe1
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_46:
.text
ut_71:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_4
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_4
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_4:
.loc 5 1386 0 prologue_end
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf90033a0
.word 0xf9400fa0
.word 0xf90037a0
.word 0xf94037a0
.word 0xb5000140
.loc 5 1388 0
.word 0xf9400ba0
.word 0xf9002ba0
.word 0xf9400fa0
.word 0xf9002fa0
.word 0xb98053a0
.word 0x6b1f001f
.word 0x10000011
.word 0x5400058b
.word 0x1400001b
.loc 5 1390 0
.word 0xf9400ba0
.word 0xf90023a0
.word 0xf9400fa0
.word 0xf90027a0
.word 0xf94027a0
.word 0xb9801800
.word 0xd280003e
.word 0x6b1e001f
.word 0x540002ac
.word 0xf9400ba0
.word 0xf9001ba0
.word 0xf9400fa0
.word 0xf9001fa0
.word 0xb98033a0
.word 0x6b1f001f
.word 0x540001cb
.loc 5 1396 0
.word 0xf9400ba0
.word 0xf90013a0
.word 0xf9400fa0
.word 0xf90017a0
.word 0xf94017a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x540001e9
.word 0xb9402000
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.loc 5 1392 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2805d61
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_47:
.text
ut_72:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_5
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_5
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_5:
.loc 5 1403 0 prologue_end
.word 0xa9b47bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a0
.word 0xf90017a1
.word 0xf94013a0
.word 0xf9005ba0
.word 0xf94017a0
.word 0xf9005fa0
.word 0xf9405fa0
.word 0xb5000100
.loc 5 1405 0
.word 0xf94013a0
.word 0xf90053a0
.word 0xf94017a0
.word 0xf90057a0
.word 0xb980a3a0
.word 0x93407c00
.word 0x1400005c
.loc 5 1408 0
.word 0xf94013a0
.word 0xf9004ba0
.word 0xf94017a0
.word 0xf9004fa0
.word 0xf9404fa0
.word 0xb9801800
.loc 5 1409 0
.word 0xaa0003fa
.word 0xd280005e
.word 0x6b1e001f
.word 0x54000acc
.loc 5 1415 0
.word 0xd280003e
.word 0x6b1e035f
.word 0x5400036d
.loc 5 1417 0
.word 0xf94013a0
.word 0xf90043a0
.word 0xf94017a0
.word 0xf90047a0
.word 0xf94047a0
.word 0xb9801801
.word 0xd280003e
.word 0xeb1e003f
.word 0x10000011
.word 0x54000a69
.word 0xb9402400
.word 0xf94013a1
.word 0xf9003ba1
.word 0xf94017a1
.word 0xf9003fa1
.word 0xf9403fa1
.word 0xb9801822
.word 0xeb1f005f
.word 0x10000011
.word 0x54000929
.word 0xb9402021
.word 0x2a0003e0
.word 0xd3607c00
.word 0x2a0103e1
.word 0xaa01001a
.loc 5 1418 0
.word 0x1400000c
.loc 5 1421 0
.word 0xf94013a0
.word 0xf90033a0
.word 0xf94017a0
.word 0xf90037a0
.word 0xf94037a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54000749
.word 0xb9402000
.word 0x2a0003fa
.loc 5 1424 0
.word 0xf94013a0
.word 0xf9002ba0
.word 0xf94017a0
.word 0xf9002fa0
.word 0xb98053a0
.word 0x6b1f001f
.word 0x5400006c
.word 0xcb1a03f9
.word 0x14000002
.word 0xaa1a03f9
.word 0xaa1903fa
.loc 5 1425 0
.word 0xeb1f033f
.word 0x5400010d
.word 0xf94013a0
.word 0xf90023a0
.word 0xf94017a0
.word 0xf90027a0
.word 0xb98043a0
.word 0x6b1f001f
.word 0x5400014c
.word 0xeb1f035f
.word 0x5400014a
.word 0xf94013a0
.word 0xf9001ba0
.word 0xf94017a0
.word 0xf9001fa0
.word 0xb98033a0
.word 0x6b1f001f
.word 0x5400006a
.loc 5 1428 0
.word 0xaa1a03e0
.word 0x1400000b
.loc 5 1430 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2806ae1
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0
.loc 5 1411 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2806ae1
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_48:
.text
ut_73:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_6
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_6
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_6:
.loc 5 1437 0 prologue_end
.word 0xa9b67bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf90043a0
.word 0xf9400fa0
.word 0xf90047a0
.word 0xf94047a0
.word 0xb5000160
.loc 5 1439 0
.word 0xf9400ba0
.word 0xf9003ba0
.word 0xf9400fa0
.word 0xf9003fa0
.word 0xb98073a0
.word 0xeb1f001f
.word 0x10000011
.word 0x540009cb
.word 0x2a0003e0
.word 0x1400003c
.loc 5 1442 0
.word 0xf9400ba0
.word 0xf90033a0
.word 0xf9400fa0
.word 0xf90037a0
.word 0xf94037a0
.word 0xb9801800
.loc 5 1443 0
.word 0xaa0003e1
.word 0xb90093a1
.word 0xd280005e
.word 0x6b1e001f
.word 0x5400068c
.word 0xf9400ba0
.word 0xf9002ba0
.word 0xf9400fa0
.word 0xf9002fa0
.word 0xb98053a0
.word 0x6b1f001f
.word 0x540005ab
.word 0xb98093a0
.loc 5 1448 0
.word 0xd280003e
.word 0x6b1e001f
.word 0x5400036d
.loc 5 1450 0
.word 0xf9400ba0
.word 0xf90023a0
.word 0xf9400fa0
.word 0xf90027a0
.word 0xf94027a0
.word 0xb9801801
.word 0xd280003e
.word 0xeb1e003f
.word 0x10000011
.word 0x54000529
.word 0xb9402400
.word 0xf9400ba1
.word 0xf9001ba1
.word 0xf9400fa1
.word 0xf9001fa1
.word 0xf9401fa1
.word 0xb9801822
.word 0xeb1f005f
.word 0x10000011
.word 0x540003e9
.word 0xb9402021
.word 0x2a0003e0
.word 0xd3607c00
.word 0x2a0103e1
.word 0xaa010000
.word 0x1400000c
.loc 5 1452 0
.word 0xf9400ba0
.word 0xf90013a0
.word 0xf9400fa0
.word 0xf90017a0
.word 0xf94017a0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54000209
.word 0xb9402000
.word 0x2a0003e0
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0
.loc 5 1445 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2807861
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_49:
.text
ut_74:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_7
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_7
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_7:
.loc 5 1457 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400ba0
.word 0xf9400fa1
bl _p_70
.word 0x1e624010
.word 0x1e22c200
.word 0x1e624000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_4a:
.text
ut_75:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_8
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_8
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_8:
.loc 5 1464 0 prologue_end
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xf9001fa0
.word 0xf90023a1
.word 0xf9401fa0
.word 0xf9002fa0
.word 0xf94023a0
.word 0xf90033a0
.word 0xb9805bba
.loc 5 1465 0
.word 0xf9401fa0
.word 0xf90027a0
.word 0xf94023a0
.word 0xf9002ba0
.word 0xf9402bb9
.loc 5 1467 0
.word 0xaa1903e0
.word 0xb5000060
.loc 5 1468 0
.word 0x1e620340
.word 0x14000057
.loc 5 1470 0
.word 0xb9801b38
.loc 5 1477 0
.word 0xaa1803e0
.word 0xd280041e
.word 0x6b1e001f
.word 0x5400018d
.loc 5 1479 0
.word 0xd280003e
.word 0x6b1e035f
.word 0x540000a1
.loc 5 1480 0
.word 0xd280001e
.word 0xf2effe1e
.word 0x9e6703c0
.word 0x1400004b
.loc 5 1482 0
.word 0xd280001e
.word 0xf2fffe1e
.word 0x9e6703c0
.word 0x14000047
.loc 5 1485 0
.word 0x51000700
.word 0x93407c00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x540008e9
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x2a0003f7
.loc 5 1486 0
.word 0xd280003e
.word 0x6b1e031f
.word 0x5400006c
.word 0xd2800016
.word 0x1400000b
.word 0x51000b00
.word 0x93407c00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x540006e9
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400016
.word 0x2a1603e0
.word 0xf90037a0
.loc 5 1487 0
.word 0xd280005e
.word 0x6b1e031f
.word 0x5400006c
.word 0xd2800016
.word 0x1400000b
.word 0x51000f00
.word 0x93407c00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x540004c9
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400016
.word 0x2a1603f9
.loc 5 1489 0
.word 0xaa1703e0
bl _p_71
.word 0x93407c00
.word 0xaa0003f6
.loc 5 1491 0
.word 0x51000b00
.word 0x531b6800
.word 0x4b160018
.loc 5 1492 0
.word 0x110082c0
.word 0xd28007fe
.word 0xa1e0000
.word 0x9ac022e0
.word 0xd28007fe
.word 0xa1e02c2
.word 0xf94037a1
.word 0x9ac22021
.word 0xaa010000
.word 0xd2800401
.word 0x4b160021
.word 0xd28007fe
.word 0xa1e0021
.word 0x9ac12721
.word 0xaa010019
.loc 5 1494 0
.word 0xaa1a03e0
.word 0xaa1803e1
.word 0xaa1903e2
bl _p_72
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_4b:
.text
ut_76:
add x0, x0, 16
b System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_9
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_9
System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_9:
.loc 5 1500 0 prologue_end
.word 0xa9b17bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xf90023a0
.word 0xf90027a1
.word 0xf94023a0
.word 0xf90063a0
.word 0xf94027a0
.word 0xf90067a0
.word 0xf94067a0
.word 0xb50001a0
.loc 5 1501 0
.word 0xf94023a0
.word 0xf9005ba0
.word 0xf94027a0
.word 0xf9005fa0
.word 0xb980b3a0
.word 0x9100c3a1
.word 0xf9006ba1
bl _p_73
.word 0xf9406bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0x1400004a
.loc 5 1503 0
.word 0xf94023a0
.word 0xf90053a0
.word 0xf94027a0
.word 0xf90057a0
.word 0xf94057a0
.word 0xb9801800
.loc 5 1504 0
.word 0xaa0003fa
.word 0xd280007e
.word 0x6b1e001f
.word 0x540008ec
.loc 5 1506 0
.word 0xd2800019
.word 0xd2800018
.word 0xd2800017
.loc 5 1510 0
.word 0xd280005e
.word 0x6b1e035f
.word 0x5400018d
.word 0xf94023a0
.word 0xf9004ba0
.word 0xf94027a0
.word 0xf9004fa0
.word 0xf9404fa0
.word 0xb9801801
.word 0xd280005e
.word 0xeb1e003f
.word 0x10000011
.word 0x54000829
.word 0xb9402817
.loc 5 1511 0
.word 0xd280003e
.word 0x6b1e035f
.word 0x5400018d
.word 0xf94023a0
.word 0xf90043a0
.word 0xf94027a0
.word 0xf90047a0
.word 0xf94047a0
.word 0xb9801801
.word 0xd280003e
.word 0xeb1e003f
.word 0x10000011
.word 0x54000669
.word 0xb9402418
.loc 5 1512 0
.word 0x6b1f035f
.word 0x5400016d
.word 0xf94023a0
.word 0xf9003ba0
.word 0xf94027a0
.word 0xf9003fa0
.word 0xf9403fa0
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x540004e9
.word 0xb9402019
.loc 5 1515 0
.word 0xf94023a0
.word 0xf90033a0
.word 0xf94027a0
.word 0xf90037a0
.word 0xb98063a0
.word 0x6b1f001f
.word 0x9a9fa7e4
.word 0xd2800000
.word 0xf9002ba0
.word 0xf9002fa0
.word 0x910143a0
.word 0xaa1903e1
.word 0xaa1803e2
.word 0xaa1703e3
.word 0xd2800005
bl _p_74
.word 0xf9402ba0
.word 0xf9001ba0
.word 0xf9402fa0
.word 0xf9001fa0
.word 0xa94163b7
.word 0xa9426bb9
.word 0xf9401ba0
.word 0xf9401fa1
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0
.loc 5 1504 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28085e1
bl _p_24
.word 0xaa0003e1
.word 0xd2801f40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_4c:
.text
ut_77:
add x0, x0, 16
b System_Numerics_BigInteger_op_UnaryNegation_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_UnaryNegation_System_Numerics_BigInteger
System_Numerics_BigInteger_op_UnaryNegation_System_Numerics_BigInteger:
.loc 5 1698 0 prologue_end
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xf90017a1
.word 0xf94013a0
.word 0xf9002ba0
.word 0xf94017a0
.word 0xf9002fa0
.word 0xb98053a0
.word 0x4b0003e2
.word 0xf94013a0
.word 0xf90023a0
.word 0xf94017a0
.word 0xf90027a0
.word 0xf94027a0
.word 0xd2800001
.word 0xf9001ba1
.word 0xf9001fa1
.word 0x9100c3a1
.word 0xb90033a2
.word 0xf9001fa0
.word 0x91002021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9401ba0
.word 0xf9000ba0
.word 0xf9401fa0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_4d:
.text
ut_78:
add x0, x0, 16
b System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger
System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger:
.loc 5 1722 0 prologue_end
.word 0xa9b17bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xf90017a1
.word 0xf9001ba2
.word 0xf9001fa3
.word 0xf94013a0
.word 0xf9006ba0
.word 0xf94017a0
.word 0xf9006fa0
.word 0xb980d3a0
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0xf9401ba1
.word 0xf90063a1
.word 0xf9401fa1
.word 0xf90067a1
.word 0xb980c3a1
.word 0x6b1f003f
.word 0x9a9fa7e1
.word 0x6b01001f
.word 0x540003a0
.loc 5 1723 0
.word 0xf94013a0
.word 0xf9005ba0
.word 0xf94017a0
.word 0xf9005fa0
.word 0xf9405fa0
.word 0xf94013a1
.word 0xf90053a1
.word 0xf94017a1
.word 0xf90057a1
.word 0xb980a3a1
.word 0xf9401ba2
.word 0xf9004ba2
.word 0xf9401fa2
.word 0xf9004fa2
.word 0xf9404fa2
.word 0xf9401ba3
.word 0xf90043a3
.word 0xf9401fa3
.word 0xf90047a3
.word 0xb98083a3
.word 0x4b0303e3
.word 0x910043a4
.word 0xf90073a4
bl _p_61
.word 0xf94073be
.word 0xf90003c0
.word 0xf90007c1
.word 0x1400001b
.loc 5 1724 0
.word 0xf94013a0
.word 0xf9003ba0
.word 0xf94017a0
.word 0xf9003fa0
.word 0xf9403fa0
.word 0xf94013a1
.word 0xf90033a1
.word 0xf94017a1
.word 0xf90037a1
.word 0xb98063a1
.word 0xf9401ba2
.word 0xf9002ba2
.word 0xf9401fa2
.word 0xf9002fa2
.word 0xf9402fa2
.word 0xf9401ba3
.word 0xf90023a3
.word 0xf9401fa3
.word 0xf90027a3
.word 0xb98043a3
.word 0x910043a4
.word 0xf90073a4
bl _p_60
.word 0xf94073be
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0

Lme_4e:
.text
ut_79:
add x0, x0, 16
b System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger
System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger:
.loc 5 1732 0 prologue_end
.word 0xd2804e10
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf9001ba0
.word 0xf9001fa1
.word 0xf90023a2
.word 0xf90027a3
.word 0xf9401ba0
.word 0xf90123a0
.word 0xf9401fa0
.word 0xf90127a0
.word 0xf94127a0
.word 0xeb1f001f
.word 0x9a9f17fa
.loc 5 1733 0
.word 0xf94023a0
.word 0xf9011ba0
.word 0xf94027a0
.word 0xf9011fa0
.word 0xf9411fa0
.word 0xeb1f001f
.word 0x9a9f17f9
.loc 5 1735 0
.word 0xa190340
.word 0x340002a0
.loc 5 1737 0
.word 0xf9401ba0
.word 0xf90113a0
.word 0xf9401fa0
.word 0xf90117a0
.word 0xb98223a0
.word 0x93407c00
.word 0xf94023a1
.word 0xf9010ba1
.word 0xf94027a1
.word 0xf9010fa1
.word 0xb98213a1
.word 0x93407c21
.word 0x9b017c00
.word 0x910083a1
.word 0xf9012ba1
bl _p_56
.word 0xf9412bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0x140000d6
.loc 5 1740 0
.word 0x3400053a
.loc 5 1742 0
.word 0xf94023a0
.word 0xf90103a0
.word 0xf94027a0
.word 0xf90107a0
.word 0xf94107a0
.word 0xf9401ba1
.word 0xf900fba1
.word 0xf9401fa1
.word 0xf900ffa1
.word 0xb981f3a1
.word 0x131f7c22
.word 0x4a020021
.word 0x4b020021
bl _p_75
.word 0xaa0003e1
.word 0xf9401ba0
.word 0xf900f3a0
.word 0xf9401fa0
.word 0xf900f7a0
.loc 5 1743 0
.word 0xb981e3a0
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0xf94023a2
.word 0xf900eba2
.word 0xf94027a2
.word 0xf900efa2
.word 0xb981d3a2
.word 0x6b1f005f
.word 0x9a9fa7e2
.word 0x4a020002
.word 0xd2800000
.word 0xf900e3a0
.word 0xf900e7a0
.word 0x910703a0
bl _p_58
.word 0xf940e3a0
.word 0xf90013a0
.word 0xf940e7a0
.word 0xf90017a0
.word 0x140000ad
.loc 5 1746 0
.word 0x34000539
.loc 5 1748 0
.word 0xf9401ba0
.word 0xf900dba0
.word 0xf9401fa0
.word 0xf900dfa0
.word 0xf940dfa0
.word 0xf94023a1
.word 0xf900d3a1
.word 0xf94027a1
.word 0xf900d7a1
.word 0xb981a3a1
.word 0x131f7c22
.word 0x4a020021
.word 0x4b020021
bl _p_75
.word 0xaa0003e1
.word 0xf9401ba0
.word 0xf900cba0
.word 0xf9401fa0
.word 0xf900cfa0
.loc 5 1749 0
.word 0xb98193a0
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0xf94023a2
.word 0xf900c3a2
.word 0xf94027a2
.word 0xf900c7a2
.word 0xb98183a2
.word 0x6b1f005f
.word 0x9a9fa7e2
.word 0x4a020002
.word 0xd2800000
.word 0xf900bba0
.word 0xf900bfa0
.word 0x9105c3a0
bl _p_58
.word 0xf940bba0
.word 0xf90013a0
.word 0xf940bfa0
.word 0xf90017a0
.word 0x14000084
.loc 5 1752 0
.word 0xf9401ba0
.word 0xf900b3a0
.word 0xf9401fa0
.word 0xf900b7a0
.word 0xf940b7a0
.word 0xf94023a1
.word 0xf900aba1
.word 0xf94027a1
.word 0xf900afa1
.word 0xf940afa1
.word 0xeb01001f
.word 0x54000421
.loc 5 1754 0
.word 0xf9401ba0
.word 0xf900a3a0
.word 0xf9401fa0
.word 0xf900a7a0
.word 0xf940a7a0
bl _p_76
.word 0xaa0003e1
.word 0xf9401ba0
.word 0xf9009ba0
.word 0xf9401fa0
.word 0xf9009fa0
.loc 5 1755 0
.word 0xb98133a0
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0xf94023a2
.word 0xf90093a2
.word 0xf94027a2
.word 0xf90097a2
.word 0xb98123a2
.word 0x6b1f005f
.word 0x9a9fa7e2
.word 0x4a020002
.word 0xd2800000
.word 0xf9008ba0
.word 0xf9008fa0
.word 0x910443a0
bl _p_58
.word 0xf9408ba0
.word 0xf90013a0
.word 0xf9408fa0
.word 0xf90017a0
.word 0x14000058
.loc 5 1758 0
.word 0xf9401ba0
.word 0xf90083a0
.word 0xf9401fa0
.word 0xf90087a0
.word 0xf94087a0
.word 0xb9801800
.word 0xf94023a1
.word 0xf9007ba1
.word 0xf94027a1
.word 0xf9007fa1
.word 0xf9407fa1
.word 0xb9801821
.word 0x6b01001f
.word 0x540004ca
.loc 5 1760 0
.word 0xf94023a0
.word 0xf90073a0
.word 0xf94027a0
.word 0xf90077a0
.word 0xf94077a0
.word 0xf9401ba1
.word 0xf9006ba1
.word 0xf9401fa1
.word 0xf9006fa1
.word 0xf9406fa1
bl _p_77
.word 0xaa0003e1
.word 0xf9401ba0
.word 0xf90063a0
.word 0xf9401fa0
.word 0xf90067a0
.loc 5 1761 0
.word 0xb980c3a0
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0xf94023a2
.word 0xf9005ba2
.word 0xf94027a2
.word 0xf9005fa2
.word 0xb980b3a2
.word 0x6b1f005f
.word 0x9a9fa7e2
.word 0x4a020002
.word 0xd2800000
.word 0xf90053a0
.word 0xf90057a0
.word 0x910283a0
bl _p_58
.word 0xf94053a0
.word 0xf90013a0
.word 0xf94057a0
.word 0xf90017a0
.word 0x14000025
.loc 5 1765 0
.word 0xf9401ba0
.word 0xf9004ba0
.word 0xf9401fa0
.word 0xf9004fa0
.word 0xf9404fa0
.word 0xf94023a1
.word 0xf90043a1
.word 0xf94027a1
.word 0xf90047a1
.word 0xf94047a1
bl _p_77
.word 0xaa0003e1
.word 0xf9401ba0
.word 0xf9003ba0
.word 0xf9401fa0
.word 0xf9003fa0
.loc 5 1766 0
.word 0xb98073a0
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0xf94023a2
.word 0xf90033a2
.word 0xf94027a2
.word 0xf90037a2
.word 0xb98063a2
.word 0x6b1f005f
.word 0x9a9fa7e2
.word 0x4a020002
.word 0xd2800000
.word 0xf9002ba0
.word 0xf9002fa0
.word 0x910143a0
bl _p_58
.word 0xf9402ba0
.word 0xf90013a0
.word 0xf9402fa0
.word 0xf90017a0
.word 0xa9416bb9
.word 0xf94013a0
.word 0xf94017a1
.word 0x910003bf
.word 0xa9407bfd
.word 0xd2804e10
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0

Lme_4f:
.text
ut_80:
add x0, x0, 16
b System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_System_Numerics_BigInteger
System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_System_Numerics_BigInteger:
.loc 5 1848 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0x910043a0
.word 0xf94013a1
.word 0xf94017a2
bl _p_51
.word 0x93407c00
.word 0x6b1f001f
.word 0x9a9fd7e0
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_50:
.text
ut_81:
add x0, x0, 16
b System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_System_Numerics_BigInteger
System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_System_Numerics_BigInteger:
.loc 5 1867 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0x910043a0
.word 0xf94013a1
.word 0xf94017a2
bl _p_49
.word 0x53001c00
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_51:
.text
ut_82:
add x0, x0, 16
b System_Numerics_BigInteger_op_LessThan_System_Numerics_BigInteger_long
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_LessThan_System_Numerics_BigInteger_long
System_Numerics_BigInteger_op_LessThan_System_Numerics_BigInteger_long:
.loc 5 1872 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0x910043a0
.word 0xf94013a1
bl _p_78
.word 0x93407c00
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_52:
.text
ut_83:
add x0, x0, 16
b System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_long
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_long
System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_long:
.loc 5 1877 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0x910043a0
.word 0xf94013a1
bl _p_78
.word 0x93407c00
.word 0x6b1f001f
.word 0x9a9fd7e0
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_53:
.text
ut_84:
add x0, x0, 16
b System_Numerics_BigInteger_op_Equality_System_Numerics_BigInteger_long
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Equality_System_Numerics_BigInteger_long
System_Numerics_BigInteger_op_Equality_System_Numerics_BigInteger_long:
.loc 5 1892 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0x910043a0
.word 0xf94013a1
bl _p_79
.word 0x53001c00
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_54:
.text
ut_85:
add x0, x0, 16
b System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_long
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_long
System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_long:
.loc 5 1897 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0x910043a0
.word 0xf94013a1
bl _p_79
.word 0x53001c00
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_55:
.text
ut_86:
add x0, x0, 16
b System_Numerics_BigInteger_op_LessThan_long_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_LessThan_long_System_Numerics_BigInteger
System_Numerics_BigInteger_op_LessThan_long_System_Numerics_BigInteger:
.loc 5 1902 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0x910063a0
.word 0xf9400ba1
bl _p_78
.word 0x93407c00
.word 0x6b1f001f
.word 0x9a9fd7e0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_56:
.text
ut_87:
add x0, x0, 16
b System_Numerics_BigInteger_op_LessThanOrEqual_long_System_Numerics_BigInteger
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_op_LessThanOrEqual_long_System_Numerics_BigInteger
System_Numerics_BigInteger_op_LessThanOrEqual_long_System_Numerics_BigInteger:
.loc 5 1907 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0x910063a0
.word 0xf9400ba1
bl _p_78
.word 0x93407c00
.word 0x6b1f001f
.word 0x9a9fa7e0
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_57:
.text
ut_88:
add x0, x0, 16
b System_Numerics_BigInteger_GetDiffLength_uint___uint___int
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_GetDiffLength_uint___uint___int
System_Numerics_BigInteger_GetDiffLength_uint___uint___int:
.loc 5 2038 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xf90013b9
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf90017a2
.word 0xb9802bb7
.word 0x14000017
.loc 5 2040 0
.word 0x93407ee0
.word 0xb9801b01
.word 0xeb00003f
.word 0x10000011
.word 0x540003a9
.word 0xd37ef400
.word 0x8b000300
.word 0x91008000
.word 0xb9400000
.word 0x93407ee1
.word 0xb9801b22
.word 0xeb01005f
.word 0x10000011
.word 0x54000289
.word 0xd37ef421
.word 0x8b010321
.word 0x91008021
.word 0xb9400021
.word 0x6b01001f
.word 0x54000060
.loc 5 2041 0
.word 0x110006e0
.word 0x14000007
.loc 5 2038 0
.word 0x510006e1
.word 0xaa0103e0
.word 0xaa0103f7
.word 0x6b1f001f
.word 0x54fffcca
.loc 5 2043 0
.word 0xd2800000
.word 0xa94163b7
.word 0xf94013b9
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_58:
.text
ut_89:
add x0, x0, 16
b System_Numerics_BigInteger_AssertValid
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger_AssertValid
System_Numerics_BigInteger_AssertValid:
.loc 5 2049 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9400ba0
.word 0xf9400400
.loc 5 2065 0
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_59:
.text
ut_90:
add x0, x0, 16
b System_Numerics_BigInteger__cctor
.text
	.align 4
	.no_dead_strip System_Numerics_BigInteger__cctor
System_Numerics_BigInteger__cctor:
.loc 5 29 0 prologue_end
.word 0xa9b77bfd
.word 0x910003fd

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xd2800021
bl _p_35
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xb9801822
.word 0xeb1f005f
.word 0x10000011
.word 0x540010a9
.word 0x929ffffe
.word 0xf2b0001e
.word 0xb900203e
.word 0xd2800001
.word 0xf90043a1
.word 0xf90047a1
.word 0x910203a1
.word 0x9280001e
.word 0xf2bffffe
.word 0xb90083be
.word 0xf90047a0
.word 0x91002021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94043a0
.word 0xf90023a0
.word 0xf94047a0
.word 0xf90027a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #680]
.word 0xf94023a1
.word 0xf9000001
.word 0x91002001
.word 0xf94027a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 30 0
.word 0xd2800000
.word 0xf9003ba0
.word 0xf9003fa0
.word 0x9101c3a0
.word 0xd2800021
bl _p_65
.word 0xf9403ba0
.word 0xf9001ba0
.word 0xf9403fa0
.word 0xf9001fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #744]
.word 0xf9401ba1
.word 0xf9000001
.word 0x91002001
.word 0xf9401fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 31 0
.word 0xd2800000
.word 0xf90033a0
.word 0xf90037a0
.word 0x910183a0
.word 0xd2800001
bl _p_65
.word 0xf94033a0
.word 0xf90013a0
.word 0xf94037a0
.word 0xf90017a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf94013a1
.word 0xf9000001
.word 0x91002001
.word 0xf94017a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 5 32 0
.word 0xd2800000
.word 0xf9002ba0
.word 0xf9002fa0
.word 0x910143a0
.word 0x92800001
.word 0xf2bfffe1
bl _p_65
.word 0xf9402ba0
.word 0xf9000ba0
.word 0xf9402fa0
.word 0xf9000fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #712]
.word 0xf9400ba1
.word 0xf9000001
.word 0x91002001
.word 0xf9400fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_5a:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Add_uint___uint
System_Numerics_BigIntegerCalculator_Add_uint___uint:
.file 6 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/external/corefx/src/System.Runtime.Numerics/src/System/Numerics/BigIntegerCalculator.AddSub.cs"
.loc 6 21 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xb9801b20
.word 0x11000401

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xaa0003f8
.loc 6 23 0
.word 0xb9801b20
.word 0xeb1f001f
.word 0x10000011
.word 0x54000749
.word 0xb9402320
.word 0x2a0003e0
.word 0x2a1a03e1
.word 0x8b01001a
.loc 6 24 0
.word 0xaa1a03e0
.word 0xb9801b01
.word 0xeb1f003f
.word 0x10000011
.word 0x54000629
.word 0xb9002300
.loc 6 25 0
.word 0x9360ff57
.loc 6 27 0
.word 0xd2800036
.word 0x14000018
.loc 6 29 0
.word 0x93407ec0
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x54000509
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b17001a
.loc 6 30 0
.word 0xaa1a03e1
.word 0x93407ec0
.word 0xb9801b02
.word 0xeb00005f
.word 0x10000011
.word 0x54000389
.word 0xd37ef400
.word 0x8b000300
.word 0x91008000
.word 0xb9000001
.loc 6 31 0
.word 0x9360ff57
.loc 6 27 0
.word 0x110006d6
.word 0xb9801b20
.word 0x6b0002df
.word 0x54fffceb
.loc 6 33 0
.word 0xb9801b20
.word 0xaa1703e1
.word 0x93407c00
.word 0xb9801b02
.word 0xeb00005f
.word 0x10000011
.word 0x54000189
.word 0xd37ef400
.word 0x8b000300
.word 0x91008000
.word 0xb9000001
.loc 6 35 0
.word 0xaa1803e0
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_5b:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Add_uint___uint__
System_Numerics_BigIntegerCalculator_Add_uint___uint__:
.loc 6 48 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xf9001bb9
.word 0xaa0003f9
.word 0xf9001fa1
.word 0xb9801b20
.word 0x11000401

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xaa0003f8
.loc 6 50 0
.word 0xaa1903f7
.word 0xb4000079
.word 0xb9801ae0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f7
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54000669
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008017
.word 0xf9401fb6
.word 0xf9401fa0
.word 0xb4000060
.word 0xb9801ac0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f6
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54000449
.word 0xd37ef400
.word 0x8b0002c0
.word 0x91008016
.word 0xd2800000
.word 0x93407c00
.word 0xb9801b01
.word 0xeb00003f
.word 0x10000011
.word 0x54000329
.word 0xd37ef400
.word 0x8b000300
.word 0x91008015
.loc 6 52 0
.word 0xb9801b21
.word 0xf9401fa0
.word 0xb9801803
.word 0xb9801b05
.word 0xaa1703e0
.word 0xaa1603e2
.word 0xaa1503e4
bl _p_80
.word 0xd2800000
.word 0x2a0003f7
.word 0xd2800000
.word 0x2a0003f6
.word 0xd2800000
.word 0x2a0003f5
.loc 6 57 0
.word 0xaa1803e0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xf9401bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_5c:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int
System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int:
.loc 6 75 0 prologue_end
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xf90023ba
.word 0xaa0003f6
.word 0xaa0103f7
.word 0xaa0203f8
.word 0xaa0303f9
.word 0xaa0403fa
.word 0xf90027a5
.word 0xd2800015
.loc 6 76 0
.word 0xd2800014
.word 0x14000017
.loc 6 80 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002c0
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b140000
.word 0x93407ea1
.word 0xd2800082
.word 0x9b027c21
.word 0x8b010301
.word 0xb9400021
.word 0x2a0103e1
.word 0x8b010014
.loc 6 81 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000340
.word 0xaa1403e1
.word 0xb9000001
.loc 6 82 0
.word 0x9360fe94
.loc 6 78 0
.word 0x110006b5
.word 0x6b1902bf
.word 0x54fffd2b
.word 0x14000010
.loc 6 86 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002c0
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b140019
.loc 6 87 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000340
.word 0xaa1903e1
.word 0xb9000001
.loc 6 88 0
.word 0x9360ff34
.loc 6 84 0
.word 0x110006b5
.word 0x6b1702bf
.word 0x54fffe0b
.loc 6 90 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000340
.word 0xaa1403e1
.word 0xb9000001
.loc 6 91 0
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0xf94023ba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_5d:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int
System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int:
.loc 6 105 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003f7
.word 0xaa0103f8
.word 0xaa0203f9
.word 0xaa0303fa
.word 0xd2800016
.loc 6 106 0
.word 0xd2800015
.word 0x14000017
.loc 6 110 0
.word 0x93407ec0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002e0
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b150000
.word 0x93407ec1
.word 0xd2800082
.word 0x9b027c21
.word 0x8b010321
.word 0xb9400021
.word 0x2a0103e1
.word 0x8b010015
.loc 6 111 0
.word 0x93407ec0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002e0
.word 0xaa1503e1
.word 0xb9000001
.loc 6 112 0
.word 0x9360feb5
.loc 6 108 0
.word 0x110006d6
.word 0x6b1a02df
.word 0x54fffd2b
.word 0x14000010
.loc 6 116 0
.word 0x93407ec0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002e0
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b15001a
.loc 6 117 0
.word 0x93407ec0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002e0
.word 0xaa1a03e1
.word 0xb9000001
.loc 6 118 0
.word 0x9360ff55
.loc 6 114 0
.word 0x110006d6
.word 0xb4000075
.word 0x6b1802df
.word 0x54fffdeb
.loc 6 122 0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_5e:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Subtract_uint___uint
System_Numerics_BigIntegerCalculator_Subtract_uint___uint:
.loc 6 134 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xb9801b21

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xaa0003f8
.loc 6 136 0
.word 0xb9801b20
.word 0xeb1f001f
.word 0x10000011
.word 0x540005e9
.word 0xb9402320
.word 0x2a0003e0
.word 0x2a1a03e1
.word 0xcb01001a
.loc 6 137 0
.word 0xaa1a03e0
.word 0xb9801b01
.word 0xeb1f003f
.word 0x10000011
.word 0x540004c9
.word 0xb9002300
.loc 6 138 0
.word 0x9360ff57
.loc 6 140 0
.word 0xd2800036
.word 0x14000018
.loc 6 142 0
.word 0x93407ec0
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x540003a9
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b17001a
.loc 6 143 0
.word 0xaa1a03e1
.word 0x93407ec0
.word 0xb9801b02
.word 0xeb00005f
.word 0x10000011
.word 0x54000229
.word 0xd37ef400
.word 0x8b000300
.word 0x91008000
.word 0xb9000001
.loc 6 144 0
.word 0x9360ff57
.loc 6 140 0
.word 0x110006d6
.word 0xb9801b20
.word 0x6b0002df
.word 0x54fffceb
.loc 6 147 0
.word 0xaa1803e0
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_5f:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Subtract_uint___uint__
System_Numerics_BigIntegerCalculator_Subtract_uint___uint__:
.loc 6 161 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xf9001bb9
.word 0xaa0003f9
.word 0xf9001fa1
.word 0xb9801b21

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xaa0003f8
.loc 6 163 0
.word 0xaa1903f7
.word 0xb4000079
.word 0xb9801ae0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f7
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54000749
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008017
.word 0xf9401fb6
.word 0xf9401fa0
.word 0xb4000060
.word 0xb9801ac0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f6
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54000529
.word 0xd37ef400
.word 0x8b0002c0
.word 0x91008016
.word 0xaa1803f5
.word 0xb4000078
.word 0xb9801aa0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f5
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801aa1
.word 0xeb00003f
.word 0x10000011
.word 0x54000329
.word 0xd37ef400
.word 0x8b0002a0
.word 0x91008015
.loc 6 165 0
.word 0xb9801b21
.word 0xf9401fa0
.word 0xb9801803
.word 0xb9801b05
.word 0xaa1703e0
.word 0xaa1603e2
.word 0xaa1503e4
bl _p_81
.word 0xd2800000
.word 0x2a0003f7
.word 0xd2800000
.word 0x2a0003f6
.word 0xd2800000
.word 0x2a0003f5
.loc 6 170 0
.word 0xaa1803e0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xf9401bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_60:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int
System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int:
.loc 6 189 0 prologue_end
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xf90023ba
.word 0xaa0003f6
.word 0xaa0103f7
.word 0xaa0203f8
.word 0xaa0303f9
.word 0xaa0403fa
.word 0xf90027a5
.word 0xd2800015
.loc 6 190 0
.word 0xd2800014
.word 0x14000017
.loc 6 194 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002c0
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b140000
.word 0x93407ea1
.word 0xd2800082
.word 0x9b027c21
.word 0x8b010301
.word 0xb9400021
.word 0x2a0103e1
.word 0xcb010014
.loc 6 195 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000340
.word 0xaa1403e1
.word 0xb9000001
.loc 6 196 0
.word 0x9360fe94
.loc 6 192 0
.word 0x110006b5
.word 0x6b1902bf
.word 0x54fffd2b
.word 0x14000010
.loc 6 200 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002c0
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b140019
.loc 6 201 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000340
.word 0xaa1903e1
.word 0xb9000001
.loc 6 202 0
.word 0x9360ff34
.loc 6 198 0
.word 0x110006b5
.word 0x6b1702bf
.word 0x54fffe0b
.loc 6 206 0
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0xf94023ba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_61:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Compare_uint___uint__
System_Numerics_BigIntegerCalculator_Compare_uint___uint__:
.loc 6 245 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xb9801b20
.word 0xb9801b41
.word 0x6b01001f
.word 0x5400008a
.loc 6 246 0
.word 0x92800000
.word 0xf2bfffe0
.word 0x1400003b
.loc 6 247 0
.word 0xb9801b20
.word 0xb9801b41
.word 0x6b01001f
.word 0x5400006d
.loc 6 248 0
.word 0xd2800020
.word 0x14000035
.loc 6 250 0
.word 0xb9801b20
.word 0x51000418
.word 0x1400002f
.loc 6 252 0
.word 0x93407f00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x54000649
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x93407f01
.word 0xb9801b42
.word 0xeb01005f
.word 0x10000011
.word 0x54000529
.word 0xd37ef421
.word 0x8b010341
.word 0x91008021
.word 0xb9400021
.word 0x6b01001f
.word 0x54000082
.loc 6 253 0
.word 0x92800000
.word 0xf2bfffe0
.word 0x1400001b
.loc 6 254 0
.word 0x93407f00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x54000369
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x93407f01
.word 0xb9801b42
.word 0xeb01005f
.word 0x10000011
.word 0x54000249
.word 0xd37ef421
.word 0x8b010341
.word 0x91008021
.word 0xb9400021
.word 0x6b01001f
.word 0x54000069
.loc 6 255 0
.word 0xd2800020
.word 0x14000005
.loc 6 250 0
.word 0x51000718
.word 0x6b1f031f
.word 0x54fffa2a
.loc 6 258 0
.word 0xd2800000
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_62:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Square_uint__
System_Numerics_BigIntegerCalculator_Square_uint__:
.file 7 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/external/corefx/src/System.Runtime.Numerics/src/System/Numerics/BigIntegerCalculator.SquMul.cs"
.loc 7 20 0 prologue_end
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003fa
.word 0xb9801b40
.word 0xb9801b41
.word 0xb010001

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xaa0003f9
.loc 7 22 0
.word 0xaa1a03f8
.word 0xb400007a
.word 0xb9801b00
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f8
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801b01
.word 0xeb00003f
.word 0x10000011
.word 0x54000469
.word 0xd37ef400
.word 0x8b000300
.word 0x91008018
.word 0xaa1903f7
.word 0xb4000079
.word 0xb9801ae0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f7
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54000269
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008017
.loc 7 24 0
.word 0xb9801b41
.word 0xb9801b23
.word 0xaa1803e0
.word 0xaa1703e2
bl _p_82
.word 0xd2800000
.word 0x2a0003f8
.word 0xd2800000
.word 0x2a0003f7
.loc 7 28 0
.word 0xaa1903e0
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_63:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int
System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int:
.loc 7 51 0 prologue_end
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f7
.word 0xaa0103f8
.word 0xaa0203f9
.word 0xf9002ba3

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #752]
.word 0xb9800000
.word 0x6b00031f
.word 0x5400098a
.loc 7 65 0
.word 0xd280001a
.word 0x14000047
.loc 7 67 0
.word 0xd2800016
.loc 7 68 0
.word 0xd2800015
.word 0x14000023
.loc 7 70 0
.word 0xb150340
.word 0x93407c00
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b160014
.loc 7 71 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002e0
.word 0xb9400000
.word 0x2a0003e0
.word 0x93407f41
.word 0xd2800082
.word 0x9b027c21
.word 0x8b0102e1
.word 0xb9400021
.word 0x2a0103e1
.word 0x9b017c16
.loc 7 72 0
.word 0xb150340
.word 0x93407c00
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xd37ffac1
.word 0x8b010281
.word 0xaa0103e1
.word 0xb9000001
.loc 7 73 0
.word 0xd341fe80
.word 0x8b0002c0
.word 0xd35ffc16
.loc 7 68 0
.word 0x110006b5
.word 0x6b1a02bf
.word 0x54fffbab
.loc 7 75 0
.word 0x93407f40
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002e0
.word 0xb9400000
.word 0x2a0003e0
.word 0x93407f41
.word 0xd2800082
.word 0x9b027c21
.word 0x8b0102e1
.word 0xb9400021
.word 0x2a0103e1
.word 0x9b017c00
.word 0x8b160015
.loc 7 76 0
.word 0xb1a0340
.word 0x93407c00
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xaa1503e1
.word 0xb9000001
.loc 7 77 0
.word 0xb1a0340
.word 0x11000400
.word 0x93407c00
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xd360fea1
.word 0xaa0103e1
.word 0xb9000001
.loc 7 65 0
.word 0x1100075a
.word 0x6b18035f
.word 0x54fff72b
.word 0x140000d5
.loc 7 94 0
.word 0x13017f16
.loc 7 95 0
.word 0xaa1603e0
.word 0x531f7815
.loc 7 98 0
.word 0xaa1703f4
.loc 7 99 0
.word 0xaa1603f3
.loc 7 100 0
.word 0x93407ec0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002e0
.word 0xf9002fa0
.loc 7 101 0
.word 0x4b16031a
.loc 7 104 0
.word 0xaa1903f8
.loc 7 105 0
.word 0xb90063b5
.loc 7 106 0
.word 0x93407ea0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xf90037a0
.loc 7 107 0
.word 0xb98053a0
.word 0x4b150000
.word 0xb90073a0
.loc 7 110 0
.word 0xaa1703e0
.word 0xaa1603e1
.word 0xaa1903e2
.word 0xaa1503e3
bl _p_82
.loc 7 114 0
.word 0xf9402fa0
.word 0xaa1a03e1
.word 0xf94037a2
.word 0xb98073a3
bl _p_82
.loc 7 117 0
.word 0x11000757
.loc 7 118 0
.word 0xaa1703e0
.word 0xb170015
.loc 7 120 0
.word 0xaa1503e0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #760]
.word 0xb9800021
.word 0x6b01001f
.word 0x54000a8a
.loc 7 122 0
.word 0x2a1703e0
.word 0xd2800081
.word 0x93407c21
bl _p_27
.word 0xf90043a0
.word 0xf94043a0
.word 0xb5000060
.word 0xf9004bbf
.word 0x14000011
.word 0xf94043a0
.word 0x91003c10
.word 0x928001f1
.word 0xf2bffff1
.word 0x8a110210
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0x8b100230
.word 0xeb10023f
.word 0x54000080
.word 0xa9007e3f
.word 0x91004231
.word 0x17fffffc
.word 0x910003e0
.word 0xf9004ba0
.word 0xf9404ba0
.word 0xf90043a0
.loc 7 123 0
.word 0x2a1503e0
.word 0xd2800081
.word 0x93407c21
bl _p_27
.word 0xf9004fa0
.word 0xf9404fa0
.word 0xb5000060
.word 0xf90053bf
.word 0x14000011
.word 0xf9404fa0
.word 0x91003c10
.word 0x928001f1
.word 0xf2bffff1
.word 0x8a110210
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0x8b100230
.word 0xeb10023f
.word 0x54000080
.word 0xa9007e3f
.word 0x91004231
.word 0x17fffffc
.word 0x910003e0
.word 0xf90053a0
.word 0xf94053a0
.word 0xf9004fa0
.loc 7 126 0
.word 0xf9402fa0
.word 0xaa1a03e1
.word 0xaa1403e2
.word 0xaa1303e3
.word 0xf94043a4
.word 0xaa1703e5
bl _p_80
.loc 7 131 0
.word 0xf94043a0
.word 0xaa1703e1
.word 0xf94053a2
.word 0xaa1503e3
bl _p_82
.loc 7 133 0
.word 0xf94037a0
.word 0xb98073a1
.word 0xaa1803e2
.word 0xb98063a3
.word 0xf94053a4
.word 0xaa1503e5
bl _p_83
.loc 7 138 0
.word 0x93407ec0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb98053a1
.word 0x4b160021
.word 0xf94053a2
.word 0xaa1503e3
bl _p_84
.loc 7 139 0
.word 0x14000059
.loc 7 142 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1703e1
bl _p_35
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf9003fa1
.word 0xb4000080
.word 0xf9403fa0
.word 0xb9801800
.word 0x350000a0
.word 0xd2800000
.word 0x2a0003e0
.word 0xf90043a0
.word 0x1400000c
.word 0xd2800000
.word 0x93407c01
.word 0xf9403fa0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000929
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xf90043a0
.loc 7 143 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1503e1
bl _p_35
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90047a1
.word 0xb4000080
.word 0xf94047a0
.word 0xb9801800
.word 0x350000a0
.word 0xd2800000
.word 0x2a0003e0
.word 0xf9004ba0
.word 0x1400000c
.word 0xd2800000
.word 0x93407c01
.word 0xf94047a0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x540005c9
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xf9004ba0
.loc 7 146 0
.word 0xf9402fa0
.word 0xaa1a03e1
.word 0xaa1403e2
.word 0xaa1303e3
.word 0xf94043a4
.word 0xaa1703e5
bl _p_80
.loc 7 151 0
.word 0xf94043a0
.word 0xaa1703e1
.word 0xf9404ba2
.word 0xaa1503e3
bl _p_82
.loc 7 153 0
.word 0xf94037a0
.word 0xb98073a1
.word 0xaa1803e2
.word 0xb98063a3
.word 0xf9404ba4
.word 0xaa1503e5
bl _p_83
.loc 7 158 0
.word 0x93407ec0
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb98053a1
.word 0x4b160021
.word 0xf9404ba2
.word 0xaa1503e3
bl _p_84
.word 0xd2800000
.word 0x2a0003e0
.word 0xf90043a0
.word 0xd2800000
.word 0x2a0003e0
.word 0xf9004ba0
.loc 7 162 0
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_64:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Multiply_uint___uint
System_Numerics_BigIntegerCalculator_Multiply_uint___uint:
.loc 7 173 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xd2800018
.loc 7 174 0
.word 0xd2800017
.loc 7 175 0
.word 0xb9801b20
.word 0x11000401

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xaa0003f6
.word 0x1400001a
.loc 7 179 0
.word 0x93407f00
.word 0xb9801b21
.word 0xeb00003f
.word 0x10000011
.word 0x54000529
.word 0xd37ef400
.word 0x8b000320
.word 0x91008000
.word 0xb9400000
.word 0x2a0003e0
.word 0x2a1a03e1
.word 0x9b017c00
.word 0x8b170015
.loc 7 180 0
.word 0xaa1503e1
.word 0x93407f00
.word 0xb9801ac2
.word 0xeb00005f
.word 0x10000011
.word 0x54000369
.word 0xd37ef400
.word 0x8b0002c0
.word 0x91008000
.word 0xb9000001
.loc 7 181 0
.word 0xd360feb7
.loc 7 177 0
.word 0x11000718
.word 0xb9801b20
.word 0x6b00031f
.word 0x54fffcab
.loc 7 183 0
.word 0xaa1703e1
.word 0x93407f00
.word 0xb9801ac2
.word 0xeb00005f
.word 0x10000011
.word 0x54000189
.word 0xd37ef400
.word 0x8b0002c0
.word 0x91008000
.word 0xb9000001
.loc 7 185 0
.word 0xaa1603e0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_65:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Multiply_uint___uint__
System_Numerics_BigIntegerCalculator_Multiply_uint___uint__:
.loc 7 198 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xb9801b20
.word 0xb9801b41
.word 0xb010001

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
bl _p_35
.word 0xaa0003f8
.loc 7 200 0
.word 0xaa1903f7
.word 0xb4000079
.word 0xb9801ae0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f7
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54000709
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008017
.word 0xaa1a03f6
.word 0xb400007a
.word 0xb9801ac0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f6
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54000509
.word 0xd37ef400
.word 0x8b0002c0
.word 0x91008016
.word 0xaa1803f5
.word 0xb4000078
.word 0xb9801aa0
.word 0x35000080
.word 0xd2800000
.word 0x2a0003f5
.word 0x1400000a
.word 0xd2800000
.word 0x93407c00
.word 0xb9801aa1
.word 0xeb00003f
.word 0x10000011
.word 0x54000309
.word 0xd37ef400
.word 0x8b0002a0
.word 0x91008015
.loc 7 202 0
.word 0xb9801b21
.word 0xb9801b43
.word 0xb9801b05
.word 0xaa1703e0
.word 0xaa1603e2
.word 0xaa1503e4
bl _p_85
.word 0xd2800000
.word 0x2a0003f7
.word 0xd2800000
.word 0x2a0003f6
.word 0xd2800000
.word 0x2a0003f5
.loc 7 207 0
.word 0xaa1803e0
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_66:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int
System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int:
.loc 7 232 0 prologue_end
.word 0xa9b17bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f5
.word 0xaa0103f6
.word 0xaa0203f7
.word 0xaa0303f8
.word 0xaa0403f9
.word 0xf9002ba5

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9800000
.word 0x6b00031f
.word 0x5400064a
.loc 7 241 0
.word 0xd280001a
.word 0x1400002d
.loc 7 243 0
.word 0xd2800014
.loc 7 244 0
.word 0xd2800013
.word 0x14000020
.loc 7 246 0
.word 0xb130340
.word 0x93407c00
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b140000
.word 0x93407e61
.word 0xd2800082
.word 0x9b027c21
.word 0x8b0102a1
.word 0xb9400021
.word 0x2a0103e1
.word 0x93407f42
.word 0xd2800083
.word 0x9b037c42
.word 0x8b0202e2
.word 0xb9400042
.word 0x2a0203e2
.word 0x9b027c21
.word 0x8b010014
.loc 7 248 0
.word 0xb130340
.word 0x93407c00
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xaa1403e1
.word 0xb9000001
.loc 7 249 0
.word 0xd360fe94
.loc 7 244 0
.word 0x11000673
.word 0x6b16027f
.word 0x54fffc0b
.loc 7 251 0
.word 0xb160340
.word 0x93407c00
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xaa1403e1
.word 0xb9000001
.loc 7 241 0
.word 0x1100075a
.word 0x6b18035f
.word 0x54fffa6b
.word 0x1400012e
.loc 7 268 0
.word 0x13017f14
.loc 7 269 0
.word 0xaa1403e0
.word 0x531f7813
.loc 7 272 0
.word 0xf9002fb5
.loc 7 273 0
.word 0xb90063b4
.loc 7 274 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002a0
.word 0xf90037a0
.loc 7 275 0
.word 0x4b1402da
.loc 7 278 0
.word 0xaa1703f6
.loc 7 279 0
.word 0xb90073b4
.loc 7 280 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b0002e0
.word 0xf9003fa0
.loc 7 281 0
.word 0x4b140300
.word 0xb90083a0
.loc 7 284 0
.word 0xaa1903f8
.loc 7 285 0
.word 0xb9008bb3
.loc 7 286 0
.word 0x93407e60
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xf9004ba0
.loc 7 287 0
.word 0xb98053a0
.word 0x4b130000
.word 0xb9009ba0
.loc 7 290 0
.word 0xaa1503e0
.word 0xaa1403e1
.word 0xaa1703e2
.word 0xaa1403e3
.word 0xaa1903e4
.word 0xaa1303e5
bl _p_85
.loc 7 295 0
.word 0xf94037a0
.word 0xaa1a03e1
.word 0xf9403fa2
.word 0xb98083a3
.word 0xf9404ba4
.word 0xb9809ba5
bl _p_85
.loc 7 299 0
.word 0x11000757
.loc 7 300 0
.word 0xb98083a0
.word 0x11000415
.loc 7 301 0
.word 0xb1502f3
.loc 7 303 0
.word 0xaa1303e0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #760]
.word 0xb9800021
.word 0x6b01001f
.word 0x54000f0a
.loc 7 305 0
.word 0x2a1703e0
.word 0xd2800081
.word 0x93407c21
bl _p_27
.word 0xf90057a0
.word 0xf94057a0
.word 0xb5000060
.word 0xf9005fbf
.word 0x14000011
.word 0xf94057a0
.word 0x91003c10
.word 0x928001f1
.word 0xf2bffff1
.word 0x8a110210
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0x8b100230
.word 0xeb10023f
.word 0x54000080
.word 0xa9007e3f
.word 0x91004231
.word 0x17fffffc
.word 0x910003e0
.word 0xf9005fa0
.word 0xf9405fa0
.word 0xf90057a0
.loc 7 306 0
.word 0x2a1503e0
.word 0xd2800081
.word 0x93407c21
bl _p_27
.word 0xf90067a0
.word 0xf94067a0
.word 0xb5000060
.word 0xf9006bbf
.word 0x14000011
.word 0xf94067a0
.word 0x91003c10
.word 0x928001f1
.word 0xf2bffff1
.word 0x8a110210
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0x8b100230
.word 0xeb10023f
.word 0x54000080
.word 0xa9007e3f
.word 0x91004231
.word 0x17fffffc
.word 0x910003e0
.word 0xf9006ba0
.word 0xf9406ba0
.word 0xf90067a0
.loc 7 307 0
.word 0x2a1303e0
.word 0xd2800081
.word 0x93407c21
bl _p_27
.word 0xf9006fa0
.word 0xf9406fa0
.word 0xb5000060
.word 0xf90073bf
.word 0x14000011
.word 0xf9406fa0
.word 0x91003c10
.word 0x928001f1
.word 0xf2bffff1
.word 0x8a110210
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0x8b100230
.word 0xeb10023f
.word 0x54000080
.word 0xa9007e3f
.word 0x91004231
.word 0x17fffffc
.word 0x910003e0
.word 0xf90073a0
.word 0xf94073a0
.word 0xf9006fa0
.loc 7 310 0
.word 0xf94037a0
.word 0xaa1a03e1
.word 0xf9402fa2
.word 0xb98063a3
.word 0xf94057a4
.word 0xaa1703e5
bl _p_80
.loc 7 315 0
.word 0xf9403fa0
.word 0xb98083a1
.word 0xaa1603e2
.word 0xb98073a3
.word 0xf94067a4
.word 0xaa1503e5
bl _p_80
.loc 7 320 0
.word 0xf94057a0
.word 0xaa1703e1
.word 0xf94067a2
.word 0xaa1503e3
.word 0xf94073a4
.word 0xaa1303e5
bl _p_85
.loc 7 323 0
.word 0xf9404ba0
.word 0xb9809ba1
.word 0xaa1803e2
.word 0xb9808ba3
.word 0xf94073a4
.word 0xaa1303e5
bl _p_83
.loc 7 328 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb98053a1
.word 0x4b140021
.word 0xf94073a2
.word 0xaa1303e3
bl _p_84
.loc 7 329 0
.word 0x14000080
.loc 7 332 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1703e1
bl _p_35
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90053a1
.word 0xb4000080
.word 0xf94053a0
.word 0xb9801800
.word 0x350000a0
.word 0xd2800000
.word 0x2a0003e0
.word 0xf90057a0
.word 0x1400000c
.word 0xd2800000
.word 0x93407c01
.word 0xf94053a0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000e09
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xf90057a0
.loc 7 333 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1503e1
bl _p_35
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf9005ba1
.word 0xb4000080
.word 0xf9405ba0
.word 0xb9801800
.word 0x350000a0
.word 0xd2800000
.word 0x2a0003e0
.word 0xf9005fa0
.word 0x1400000c
.word 0xd2800000
.word 0x93407c01
.word 0xf9405ba0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000aa9
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xf9005fa0
.loc 7 334 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1303e1
bl _p_35
.word 0xaa0003e1
.word 0xaa0103e0
.word 0xf90063a1
.word 0xb4000080
.word 0xf94063a0
.word 0xb9801800
.word 0x350000a0
.word 0xd2800000
.word 0x2a0003e0
.word 0xf90067a0
.word 0x1400000c
.word 0xd2800000
.word 0x93407c01
.word 0xf94063a0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000749
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xf90067a0
.loc 7 337 0
.word 0xf94037a0
.word 0xaa1a03e1
.word 0xf9402fa2
.word 0xb98063a3
.word 0xf94057a4
.word 0xaa1703e5
bl _p_80
.loc 7 342 0
.word 0xf9403fa0
.word 0xb98083a1
.word 0xaa1603e2
.word 0xb98073a3
.word 0xf9405fa4
.word 0xaa1503e5
bl _p_80
.loc 7 347 0
.word 0xf94057a0
.word 0xaa1703e1
.word 0xf9405fa2
.word 0xaa1503e3
.word 0xf94067a4
.word 0xaa1303e5
bl _p_85
.loc 7 350 0
.word 0xf9404ba0
.word 0xb9809ba1
.word 0xaa1803e2
.word 0xb9808ba3
.word 0xf94067a4
.word 0xaa1303e5
bl _p_83
.loc 7 355 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb98053a1
.word 0x4b140021
.word 0xf94067a2
.word 0xaa1303e3
bl _p_84
.word 0xd2800000
.word 0x2a0003e0
.word 0xf90057a0
.word 0xd2800000
.word 0x2a0003e0
.word 0xf9005fa0
.word 0xd2800000
.word 0x2a0003e0
.word 0xf90067a0
.loc 7 359 0
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_67:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int
System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int:
.loc 7 379 0 prologue_end
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f5
.word 0xaa0103f6
.word 0xaa0203f7
.word 0xaa0303f8
.word 0xaa0403f9
.word 0xaa0503fa
.word 0xd2800014
.loc 7 380 0
.word 0xd2800013
.word 0x1400001e
.loc 7 384 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b130000
.word 0x93407e81
.word 0xd2800082
.word 0x9b027c21
.word 0x8b0102a1
.word 0xb9400021
.word 0x2a0103e1
.word 0xcb010000
.word 0x93407e81
.word 0xd2800082
.word 0x9b027c21
.word 0x8b0102e1
.word 0xb9400021
.word 0x2a0103e1
.word 0xcb010013
.loc 7 385 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xaa1303e1
.word 0xb9000001
.loc 7 386 0
.word 0x9360fe73
.loc 7 382 0
.word 0x11000694
.word 0x6b18029f
.word 0x54fffc4b
.word 0x14000017
.loc 7 390 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b130000
.word 0x93407e81
.word 0xd2800082
.word 0x9b027c21
.word 0x8b0102a1
.word 0xb9400021
.word 0x2a0103e1
.word 0xcb010018
.loc 7 391 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xaa1803e1
.word 0xb9000001
.loc 7 392 0
.word 0x9360ff13
.loc 7 388 0
.word 0x11000694
.word 0x6b16029f
.word 0x54fffd2b
.word 0x14000010
.loc 7 396 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xb9400000
.word 0x2a0003e0
.word 0x8b130018
.loc 7 397 0
.word 0x93407e80
.word 0xd2800081
.word 0x9b017c00
.word 0x8b000320
.word 0xaa1803e1
.word 0xb9000001
.loc 7 398 0
.word 0x9360ff13
.loc 7 394 0
.word 0x11000694
.word 0xb4000073
.word 0x6b1a029f
.word 0x54fffdeb
.loc 7 400 0
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_68:
.text
	.align 4
	.no_dead_strip System_Numerics_BigIntegerCalculator__cctor
System_Numerics_BigIntegerCalculator__cctor:
.loc 7 229 0 prologue_end
.word 0xa9bf7bfd
.word 0x910003fd

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #776]
.word 0xd280041e
.word 0xb900001e
.loc 7 32 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #752]
.word 0xd280041e
.word 0xb900001e
.loc 7 33 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #760]
.word 0xd280201e
.word 0xb900001e
.loc 7 211 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xd280041e
.word 0xb900001e
.word 0x910003bf
.word 0xa8c17bfd
.word 0xd65f03c0

Lme_69:
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_
System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_:
.file 8 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/external/corefx/src/System.Runtime.Numerics/src/System/Numerics/BigNumber.cs"
.loc 8 308 0 prologue_end
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0
.word 0xaa0103fa
.word 0xb9801ba0
.word 0x92807ffe
.word 0xf2bffffe
.word 0xa1e0000
.word 0x340003e0
.loc 8 310 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #784]

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #792]
bl _p_86
.word 0xf90027a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #800]
.word 0xd2801201
bl _p_14
.word 0xf94027a1
.word 0xf90023a0
bl _p_87
.word 0xf94023a0
.word 0xf9000340
.word 0xd349ff41
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 8 311 0
.word 0xd2800000
.word 0x14000026
.loc 8 313 0
.word 0xb9801ba0
.word 0xd280401e
.word 0xa1e0000
.word 0x34000400
.loc 8 315 0
.word 0xb9801ba0
.word 0x9280407e
.word 0xf2bffffe
.word 0xa1e0000
.word 0x34000360
.loc 8 317 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #808]
.word 0xf90027a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #800]
.word 0xd2801201
bl _p_14
.word 0xf94027a1
.word 0xf90023a0
bl _p_87
.word 0xf94023a0
.word 0xf9000340
.word 0xd349ff41
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 8 318 0
.word 0xd2800000
.word 0x14000003
.loc 8 321 0
.word 0xf900035f
.loc 8 322 0
.word 0xd2800020
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_6a:
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_
System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_:
.loc 8 330 0 prologue_end
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9003bbf
.word 0xd2800000
.word 0xf9002fa0
.word 0xf90033a0
.word 0xf90037a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #696]
.word 0x3980b410
.word 0xb5000050
bl _p_1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf9400001
.word 0xf9001ba1
.word 0xf9400400
.word 0xf9001fa0
.word 0xf94017a1
.word 0xaa0103e0
.word 0xf9401ba2
.word 0xf9000022
.word 0x91002001
.word 0xf9401fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 8 332 0
.word 0x9101c3a1
.word 0xb9801ba0
bl _p_88
.word 0x53001c00
.word 0x340005c0
.loc 8 335 0
.word 0x910163a8
bl _p_89
.loc 8 336 0
.word 0xf9402fa0
.word 0xf90023a0
.word 0xf94033a0
.word 0xf90027a0
.word 0xf94037a0
.word 0xf9002ba0
.word 0xf94023a3
.word 0x910163a0
.word 0x91002004
.word 0x910163a0
.word 0x91003005
.word 0x910163a0
.word 0x91004006
.word 0xf9400ba0
.word 0xb9801ba1
.word 0xf94013a2
bl _p_90
.word 0x53001c00
.word 0x35000060
.loc 8 337 0
.word 0xd2800000
.word 0x14000014
.loc 8 339 0
.word 0xb9801ba0
.word 0xd280401e
.word 0xa1e0000
.word 0x34000100
.loc 8 341 0
.word 0x910163a0
.word 0xf94017a1
bl _p_91
.word 0x53001c00
.word 0x35000140
.loc 8 343 0
.word 0xd2800000
.word 0x14000009
.loc 8 348 0
.word 0x910163a0
.word 0xf94017a1
bl _p_92
.word 0x53001c00
.word 0x35000060
.loc 8 350 0
.word 0xd2800000
.word 0x14000002
.loc 8 353 0
.word 0xd2800020
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.loc 8 333 0
.word 0xf9403ba0
bl _p_25

Lme_6b:
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo
System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo:
.loc 8 359 0 prologue_end
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf90013a0
.word 0xf90017a1
.word 0xf9001ba2
.word 0xf90027bf
.word 0xd2800000
.word 0xf9001fa0
.word 0xf90023a0
.word 0xf94013a0
.word 0xb40005e0
.loc 8 363 0
.word 0x910123a1
.word 0xb9802ba0
bl _p_88
.word 0x53001c00
.word 0x340003c0
.loc 8 366 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #696]
.word 0x3980b410
.word 0xb5000050
bl _p_1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xf9400001
.word 0xf9001fa1
.word 0xf9400400
.word 0xf90023a0
.loc 8 367 0
.word 0x9100e3a3
.word 0xf94013a0
.word 0xb9802ba1
.word 0xf9401ba2
bl _p_93
.word 0x53001c00
.word 0x34000180
.loc 8 371 0
.word 0xf9401fa0
.word 0xf9000ba0
.word 0xf94023a0
.word 0xf9000fa0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0
.loc 8 364 0
.word 0xf94027a0
bl _p_25
.loc 8 369 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd280ca41
bl _p_24
.word 0xaa0003e1
.word 0xd28017a0
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.loc 8 360 0
.word 0xd2804461
bl _p_24
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_25

Lme_6c:
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_:
.loc 8 376 0 prologue_end
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f9
.word 0xf9002ba1
.word 0xf9400320
.word 0xb4000100
.word 0xf9400321
.word 0xaa0103e0
.word 0xf940001e
.word 0xb9802420
.word 0xb9802021
.word 0xb010000
.word 0x35000060
.loc 8 377 0
.word 0xd2800000
.word 0x1400008a
.loc 8 379 0
.word 0xf9400321
.word 0xaa0103e0
.word 0xf940001e
.word 0xb9802420
.word 0xb9802021
.word 0xb010000
.word 0x51000418
.loc 8 380 0
.word 0xaa1803e1
.word 0x531f7c20
.word 0xb010000
.word 0x13017c00
.word 0x531f7f02
.word 0xb020301
.word 0xd280003e
.word 0x8a1e0021
.word 0x4b020021
.word 0xb010001

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #728]
bl _p_35
.word 0xaa0003f7
.loc 8 382 0
.word 0xd2800016
.loc 8 383 0
.word 0xd2800015
.loc 8 384 0
.word 0xd2800014
.loc 8 391 0
.word 0x51000713
.word 0x1400004f
.loc 8 393 0
.word 0xf9400322
.word 0xaa0203e0
.word 0xaa1303e1
.word 0xf940005e
bl _p_94
.word 0x53003c18
.loc 8 396 0
.word 0xaa1803e0
.word 0xd280061e
.word 0x6b1e001f
.word 0x540000eb
.word 0xd280073e
.word 0x6b1e031f
.word 0x5400008c
.loc 8 398 0
.word 0x5100c300
.word 0x53001c18
.loc 8 399 0
.word 0x14000010
.loc 8 400 0
.word 0xd280083e
.word 0x6b1e031f
.word 0x5400012b
.word 0xd28008de
.word 0x6b1e031f
.word 0x540000cc
.loc 8 402 0
.word 0x928006de
.word 0xf2bffffe
.word 0xb1e0300
.word 0x53001c18
.loc 8 403 0
.word 0x14000005
.loc 8 407 0
.word 0x92800ade
.word 0xf2bffffe
.word 0xb1e0300
.word 0x53001c18
.loc 8 409 0
.word 0x350000f3
.word 0xd280011e
.word 0xa1e0300
.word 0xd280011e
.word 0x6b1e001f
.word 0x54000041
.loc 8 410 0
.word 0xd2800035
.loc 8 412 0
.word 0x340002b6
.loc 8 414 0
.word 0x93407e80
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54000949
.word 0x8b0002e0
.word 0x91008000
.word 0x39400000
.word 0x531c6f01
.word 0x2a010001
.word 0x93407e80
.word 0xb9801ae2
.word 0xeb00005f
.word 0x10000011
.word 0x54000809
.word 0x8b0002e0
.word 0x91008000
.word 0x39000001
.loc 8 415 0
.word 0x11000694
.loc 8 416 0
.word 0x14000011
.loc 8 419 0
.word 0xf9003fb7
.word 0xaa1403fa
.word 0x35000055
.word 0x14000004
.word 0xd2801e1e
.word 0x2a1e0300
.word 0x53001c18
.word 0x93407f41
.word 0xf9403fa0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x540005c9
.word 0x8b010000
.word 0x91008000
.word 0x39000018
.loc 8 421 0
.word 0x6b1f02df
.word 0x9a9f17f6
.loc 8 391 0
.word 0x51000673
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e027f
.word 0x54fff5ec
.loc 8 424 0
.word 0xd2800000
.word 0xf90037a0
.word 0xf9003ba0
.word 0x9101a3a0
.word 0xaa1703e1
bl _p_95
.word 0xf94037a0
.word 0xf9002fa0
.word 0xf9403ba0
.word 0xf90033a0
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf9402fa2
.word 0xf9000022
.word 0x91002001
.word 0xf94033a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 8 425 0
.word 0xd2800020
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_6d:
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_:
.loc 8 430 0 prologue_end
.word 0xa9ad7bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xb9800f38
.loc 8 431 0
.word 0xd2800017
.loc 8 433 0
.word 0x910383a0
.word 0xf9007ba0
.word 0xd2800140
bl _p_96
.word 0xf9407bbe
.word 0xf90003c0
.word 0xf90007c1
.loc 8 434 0
.word 0x910283a0
.word 0xf9007ba0
.word 0xd2800000
bl _p_96
.word 0xf9407bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0xf94053a0
.word 0xf9000340
.word 0x91002341
.word 0xf94057a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0x14000050
.loc 8 437 0
.word 0xf9400340
.word 0xf9004ba0
.word 0xf9400740
.word 0xf9004fa0
.word 0x910203a0
.word 0xf9007ba0
.word 0xf9404ba0
.word 0xf9404fa1
.word 0xf94073a2
.word 0xf94077a3
bl _p_97
.word 0xf9407bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0xf94043a0
.word 0xf9000340
.word 0x91002341
.word 0xf94047a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 8 438 0
.word 0xf9400322
.word 0xaa0203e0
.word 0xaa1703e1
.word 0xf940005e
bl _p_94
.word 0x53003c00
.word 0x34000580
.loc 8 440 0
.word 0xf9400340
.word 0xf9003ba0
.word 0xf9400740
.word 0xf9003fa0
.word 0xf9400322
.word 0xaa1703e1
.word 0x110006f7
.word 0xaa0203e0
.word 0xf940005e
bl _p_94
.word 0x53003c00
.word 0x5100c000
.word 0x910183a1
.word 0xf9007ba1
bl _p_96
.word 0xf9407bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0x910143a0
.word 0xf9007ba0
.word 0xf9403ba0
.word 0xf9403fa1
.word 0xf94033a2
.word 0xf94037a3
bl _p_98
.word 0xf9407bbe
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9402ba0
.word 0xf9000340
.word 0x91002341
.word 0xf9402fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 8 435 0
.word 0x51000701
.word 0xaa0103e0
.word 0xaa0103f8
.word 0x6b1f001f
.word 0x54fff5aa
.word 0x1400000d
.loc 8 445 0
.word 0xf9400322
.word 0xaa1703e1
.word 0x110006f7
.word 0xaa0203e0
.word 0xf940005e
bl _p_94
.word 0x53003c00
.word 0xd280061e
.word 0x6b1e001f
.word 0x54000060
.word 0xd2800000
.word 0x1400003e
.loc 8 443 0
.word 0xf9400322
.word 0xaa0203e0
.word 0xaa1703e1
.word 0xf940005e
bl _p_94
.word 0x53003c00
.word 0x35fffdc0
.loc 8 447 0
.word 0x39404320
.word 0x34000680
.loc 8 449 0
.word 0xf9400340
.word 0xf90023a0
.word 0xf9400740
.word 0xf90027a0
.word 0xf94023a0
.word 0xf9006ba0
.word 0xf94027a0
.word 0xf9006fa0
.word 0xb980d3a0
.word 0x4b0003e2
.word 0xf94023a0
.word 0xf90063a0
.word 0xf94027a0
.word 0xf90067a0
.word 0xf94067a0
.word 0xd2800001
.word 0xf9005ba1
.word 0xf9005fa1
.word 0x9102c3a1
.word 0xb900b3a2
.word 0xf9005fa0
.word 0x91002021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9405ba0
.word 0xf9001ba0
.word 0xf9405fa0
.word 0xf9001fa0
.word 0xf9401ba0
.word 0xf9000340
.word 0x91002341
.word 0xf9401fa0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 8 451 0
.word 0xd2800020
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8d37bfd
.word 0xd65f03c0

Lme_6e:
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_ParseFormatSpecifier_string_int_
System_Numerics_BigNumber_ParseFormatSpecifier_string_int_:
.loc 8 457 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xaa0003f9
.word 0xf9001ba1
.word 0xf9401ba0
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900001e
.loc 8 458 0
.word 0xaa1903f8
.word 0xb40000b9
.word 0xb9801300
.word 0x6b1f001f
.word 0x9a9f17f8
.word 0x14000002
.word 0xd2800038
.word 0x34000078
.loc 8 460 0
.word 0xd2800a40
.word 0x1400007c
.loc 8 463 0
.word 0xd2800018
.loc 8 464 0
.word 0xd2800000
.word 0x93407c00
.word 0xb9801321
.word 0xeb00003f
.word 0x10000011
.word 0x54000f49
.word 0xd37ff800
.word 0x8b190000
.word 0x79402817
.loc 8 465 0
.word 0xaa1703e0
.word 0xd280083e
.word 0x6b1e001f
.word 0x5400008b
.word 0xd2800b5e
.word 0x6b1e02ff
.word 0x540000ed
.word 0xd2800c3e
.word 0x6b1e02ff
.word 0x54000ceb
.word 0xd2800f5e
.word 0x6b1e02ff
.word 0x54000c8c
.loc 8 467 0
.word 0x11000718
.loc 8 468 0
.word 0x92800016
.word 0xf2bffff6
.loc 8 470 0
.word 0xb9801320
.word 0x6b00031f
.word 0x540009ca
.word 0x93407f00
.word 0xb9801321
.word 0xeb00003f
.word 0x10000011
.word 0x54000be9
.word 0xd37ff800
.word 0x8b190000
.word 0x79402800
.word 0xd280061e
.word 0x6b1e001f
.word 0x5400086b
.word 0x93407f00
.word 0xb9801321
.word 0xeb00003f
.word 0x10000011
.word 0x54000a89
.word 0xd37ff800
.word 0x8b190000
.word 0x79402800
.word 0xd280073e
.word 0x6b1e001f
.word 0x5400070c
.loc 8 472 0
.word 0xaa1803e0
.word 0x11000718
.word 0x93407c00
.word 0xb9801321
.word 0xeb00003f
.word 0x10000011
.word 0x540008e9
.word 0xd37ff800
.word 0x8b190000
.word 0x79402800
.word 0x5100c016
.word 0x14000013
.loc 8 475 0
.word 0xd280015e
.word 0x1b1e7ec0
.word 0xaa1803e1
.word 0x11000718
.word 0x93407c21
.word 0xb9801322
.word 0xeb01005f
.word 0x10000011
.word 0x54000729
.word 0xd37ff821
.word 0x8b190021
.word 0x79402821
.word 0x5100c021
.word 0xb010016
.loc 8 476 0
.word 0xaa1603e0
.word 0xd280015e
.word 0x6b1e001f
.word 0x5400034a
.loc 8 473 0
.word 0xb9801320
.word 0x6b00031f
.word 0x540002ea
.word 0x93407f00
.word 0xb9801321
.word 0xeb00003f
.word 0x10000011
.word 0x54000509
.word 0xd37ff800
.word 0x8b190000
.word 0x79402800
.word 0xd280061e
.word 0x6b1e001f
.word 0x5400018b
.word 0x93407f00
.word 0xb9801321
.word 0xeb00003f
.word 0x10000011
.word 0x540003a9
.word 0xd37ff800
.word 0x8b190000
.word 0x79402800
.word 0xd280073e
.word 0x6b1e001f
.word 0x54fffacd
.loc 8 480 0
.word 0xb9801320
.word 0x6b00031f
.word 0x5400014a
.word 0x93407f00
.word 0xb9801321
.word 0xeb00003f
.word 0x10000011
.word 0x540001e9
.word 0xd37ff800
.word 0x8b190000
.word 0x79402800
.word 0x350000a0
.loc 8 482 0
.word 0xf9401ba0
.word 0xb9000016
.loc 8 483 0
.word 0xaa1703e0
.word 0x14000002
.loc 8 486 0
.word 0xd2800000
.word 0xa9415fb6
.word 0xa94267b8
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_6f:
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo
System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo:
.loc 8 491 0 prologue_end
.word 0xa9b47bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xf9001bb8
.word 0xf9001fba
.word 0xf90023a0
.word 0xf90027a1
.word 0xaa0203f8
.word 0xf9002ba3
.word 0xaa0403fa
.word 0x3901a3bf

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #216]
.word 0xd2800601
bl _p_14
.word 0xf9005ba0
bl _p_99
.word 0xf9405ba0
.word 0xaa0003f7
.loc 8 492 0
.word 0x910103a0
bl _p_100
.word 0xaa0003f6
.loc 8 494 0
.word 0xb9801ac0
.word 0x51000415
.loc 8 496 0
.word 0xaa1503e0
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x5400078d
.loc 8 501 0
.word 0xd2800014
.loc 8 502 0
.word 0x93407ea0
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54001409
.word 0x8b0002c0
.word 0x91008000
.word 0x39400000
.word 0x3901a3a0
.loc 8 503 0
.word 0x3941a3a0
.word 0xd2801efe
.word 0x6b1e001f
.word 0x540000ad
.loc 8 505 0
.word 0x3941a3a0
.word 0x5103c000
.word 0x3901a3a0
.loc 8 506 0
.word 0xd2800034
.loc 8 508 0
.word 0x3941a3a0
.word 0xd280011e
.word 0x6b1e001f
.word 0x9a9fa7e0
.word 0x2a140000
.word 0x34000480
.loc 8 512 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #200]
.word 0x3980b410
.word 0xb5000050
bl _p_1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #208]
.word 0xf9400000
.word 0xf9005ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #816]
.word 0xf9005fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #824]
.word 0xd2800241
bl _p_14
.word 0xaa0003e2
.word 0xf9405ba0
.word 0xf9405fa1
.word 0x79002058
bl _p_2
.word 0xaa0003f4
.loc 8 513 0
.word 0x9101a3a0
.word 0xaa1403e1
.word 0xaa1a03e2
bl _p_101
.word 0xaa0003e1
.word 0xaa1703e0
.word 0xf94002fe
bl _p_18
.loc 8 514 0
.word 0x510006b5
.loc 8 517 0
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e02bf
.word 0x5400060d
.loc 8 519 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #200]
.word 0x3980b410
.word 0xb5000050
bl _p_1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #208]
.word 0xf9400000
.word 0xf9005ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #832]
.word 0xf9005fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #824]
.word 0xd2800241
bl _p_14
.word 0xaa0003e2
.word 0xf9405ba0
.word 0xf9405fa1
.word 0x79002058
bl _p_2
.word 0xaa0003f4
.word 0x14000011
.loc 8 522 0
.word 0xaa1503e0
.word 0x510006b5
.word 0x93407c00
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x540008a9
.word 0x8b0002c0
.word 0x91008000
.word 0xaa1403e1
.word 0xaa1a03e2
bl _p_101
.word 0xaa0003e1
.word 0xaa1703e0
.word 0xf94002fe
bl _p_18
.loc 8 520 0
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e02bf
.word 0x54fffdac
.loc 8 525 0
.word 0xb98053a0
.word 0x6b1f001f
.word 0x5400054d
.word 0xf94002fe
.word 0xb98026e0
.word 0xb98022e1
.word 0xb010001
.word 0xb98053a0
.word 0x6b01001f
.word 0x5400046d
.loc 8 528 0
.word 0xf94023a0
.word 0xf9002fa0
.word 0xf94027a0
.word 0xf90033a0
.word 0xb9805ba0
.word 0xaa1703fa
.word 0xd2800016
.word 0x6b1f001f
.word 0x5400018a
.word 0xd2800f1e
.word 0x6b1e031f
.word 0x540000a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x24, [x16, #840]
.word 0x14000008

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x24, [x16, #848]
.word 0x14000004

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x24, [x16, #856]
.word 0xf94002fe
.word 0xb98026e0
.word 0xb98022e1
.word 0xb010001
.word 0xb98053a0
.word 0x4b010003
.word 0xaa1a03e0
.word 0xaa1603e1
.word 0xaa1803e2
.word 0xf940035e
bl _p_102
.loc 8 530 0
.word 0xaa1703e0
.word 0xf94002e1
.word 0xf9402030
.word 0xd63f0200
.word 0xa94157b4
.word 0xa9425fb6
.word 0xf9401bb8
.word 0xf9401fba
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_70:
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo
System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo:
.loc 8 536 0 prologue_end
.word 0xa9a47bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xaa0203f9
.word 0xf90033a3
.word 0xb900dbbf
.word 0xb900e3bf
.word 0xf90077bf
.word 0xf9007bbf
.word 0xf9007fbf
.word 0xf90083bf
.word 0xb900dbbf
.loc 8 537 0
.word 0x910363a1
.word 0xaa1903e0
bl _p_103
.word 0x53003c18
.loc 8 538 0
.word 0xaa1803e0
.word 0xd2800f1e
.word 0x6b1e001f
.word 0x54000080
.word 0xd2800b1e
.word 0x6b1e031f
.word 0x54000101
.loc 8 539 0
.word 0xb980dba3
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xaa1803e2
.word 0xf94033a4
bl _p_104
.word 0x14000210
.loc 8 541 0
.word 0xd2800cfe
.word 0x6b1e031f
.word 0x54000220
.word 0xd28008fe
.word 0x6b1e031f
.word 0x540001c0
.word 0xd2800c9e
.word 0x6b1e031f
.word 0x54000160
.word 0xd280089e
.word 0x6b1e031f
.word 0x54000100
.word 0xd2800e5e
.word 0x6b1e031f
.word 0x540000a0
.word 0xd2800a5e
.word 0x6b1e031f
.word 0x9a9f17f7
.word 0x14000002
.word 0xd2800037
.word 0xaa1703f6
.loc 8 543 0
.word 0xf9402ba0
.word 0xf90067a0
.word 0xf9402fa0
.word 0xf9006ba0
.word 0xf9406ba0
.word 0xb5000820
.loc 8 545 0
.word 0xd2800cfe
.word 0x6b1e031f
.word 0x54000140
.word 0xd28008fe
.word 0x6b1e031f
.word 0x540000e0
.word 0xd2800e5e
.word 0x6b1e031f
.word 0x54000080
.word 0xd2800a5e
.word 0x6b1e031f
.word 0x54000541
.loc 8 547 0
.word 0xb980dba0
.word 0x6b1f001f
.word 0x5400048d
.loc 8 548 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #200]
.word 0x3980b410
.word 0xb5000050
bl _p_1

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #208]
.word 0xf9400000
.word 0xf900d3a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #864]
.word 0xf900d7a0
.word 0x910363a0
.word 0xf900dba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #200]
.word 0x3980b410
.word 0xb5000050
bl _p_1
.word 0xf940dba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #208]
.word 0xf9400021
bl _p_105
.word 0xaa0003e2
.word 0xf940d3a0
.word 0xf940d7a1
bl _p_2
.word 0xaa0003f9
.word 0x14000004
.loc 8 550 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x25, [x16, #872]
.loc 8 552 0
.word 0xf9402ba0
.word 0xf9005fa0
.word 0xf9402fa0
.word 0xf90063a0
.word 0xb980bba0
.word 0xb900e3a0
.word 0x910383a0
.word 0xaa1903e1
.word 0xf94033a2
bl _p_106
.word 0x140001b5
.loc 8 560 0
.word 0xf9402ba0
.word 0xf90057a0
.word 0xf9402fa0
.word 0xf9005ba0
.word 0xf9405ba0
.word 0xb9801818
.loc 8 564 0
.word 0xaa1803e0
.word 0xd2800141
bl _p_52
.word 0x93407c00
.word 0xd291c721
.word 0xf2a71c61
.word 0x93407c00
.word 0x9b017c00
.word 0x9361fc01
.word 0xd37ffc20
.word 0x8b010000
.word 0xd2800041
.word 0x2b010000
.word 0x10000011
.word 0x540035a6
.word 0xaa0003f7
.loc 8 565 0
.word 0x14000014
.word 0xf9008fa0
.word 0xf9408fa0
.loc 8 566 0
.word 0xf90077a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd280d7c1
bl _p_24
.word 0xf900d7a0
.word 0xf94077a0
.word 0xf900dba0
.word 0xd28017a0
bl _p_107
.word 0xf940d7a1
.word 0xf940dba2
.word 0xf900d3a0
bl _p_108
.word 0xf940d3a0
bl _p_25
.loc 8 567 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xaa1703e1
bl _p_35
.word 0xaa0003f7
.loc 8 568 0
.word 0xd2800015
.loc 8 570 0
.word 0xaa1803f4
.word 0x14000066
.loc 8 572 0
.word 0xf9402ba0
.word 0xf9004fa0
.word 0xf9402fa0
.word 0xf90053a0
.word 0xf94053a0
.word 0x93407e81
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002fe9
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9400018
.loc 8 573 0
.word 0xd2800013
.word 0x1400002d
.loc 8 576 0
.word 0x93407e60
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54002e89
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9400000
.word 0x2a0003e0
.word 0xd3607c00
.word 0x2a1803e1
.word 0xaa010018
.loc 8 577 0
.word 0xd2994000
.word 0xf2a77340
.word 0xeb1f001f
.word 0x10000011
.word 0x54002d40
.word 0xf100001f
.word 0x10000011
.word 0x54002ce0
.word 0x9ac00b1e
.word 0x9b00e3c0
.word 0xaa0003e1
.word 0x93407e60
.word 0xb9801ae2
.word 0xeb00005f
.word 0x10000011
.word 0x54002b89
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9000001
.loc 8 578 0
.word 0xd2994000
.word 0xf2a77340
.word 0xeb1f001f
.word 0x10000011
.word 0x54002ac0
.word 0xf100001f
.word 0x10000011
.word 0x54002a60
.word 0x9ac00b00
.word 0xaa0003f8
.loc 8 573 0
.word 0x11000673
.word 0x6b15027f
.word 0x54fffa6b
.loc 8 580 0
.word 0x340004f8
.loc 8 582 0
.word 0xaa1503e0
.word 0x110006b5
.word 0xd2994001
.word 0xf2a77341
.word 0xf100003f
.word 0x10000011
.word 0x540028c0
.word 0x1ac10b1e
.word 0x1b01e3c1
.word 0x93407c00
.word 0xb9801ae2
.word 0xeb00005f
.word 0x10000011
.word 0x54002789
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9000001
.loc 8 583 0
.word 0xd297d060
.word 0xf2a25c00
.word 0x2a1803e1
.word 0x9b007c20
.word 0xd360fc00
.word 0x8b010000
.word 0xd35efc18
.loc 8 584 0
.word 0xaa1803e0
.word 0x34000180
.loc 8 585 0
.word 0xaa1503e0
.word 0x110006b5
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54002509
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9000018
.loc 8 570 0
.word 0x51000681
.word 0xaa0103e0
.word 0xaa0103f4
.word 0x6b1f001f
.word 0x54fff2ea
.loc 8 593 0
.word 0xaa1503e0
.word 0xd2800121
bl _p_52
.word 0x93407c00
.word 0xaa0003f8
.loc 8 594 0
.word 0x14000014
.word 0xf90093a0
.word 0xf94093a0
.loc 8 595 0
.word 0xf9007ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd280d7c1
bl _p_24
.word 0xf900d7a0
.word 0xf9407ba0
.word 0xf900dba0
.word 0xd28017a0
bl _p_107
.word 0xf940d7a1
.word 0xf940dba2
.word 0xf900d3a0
bl _p_108
.word 0xf940d3a0
bl _p_25
.loc 8 597 0
.word 0x34000576
.loc 8 599 0
.word 0xb980dba0
.word 0x6b1f001f
.word 0x540000ad
.word 0xb980dba0
.word 0x6b18001f
.word 0x5400004d
.loc 8 600 0
.word 0xb980dbb8
.loc 8 601 0
.word 0xf9402ba0
.word 0xf90047a0
.word 0xf9402fa0
.word 0xf9004ba0
.word 0xb9808ba0
.word 0x6b1f001f
.word 0x540003aa
.loc 8 606 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9401800
.word 0xb9801000
.word 0x2b000300
.word 0x10000011
.word 0x54001ec6
.word 0xaa0003f8
.loc 8 607 0
.word 0x14000014
.word 0xf90097a0
.word 0xf94097a0
.loc 8 608 0
.word 0xf9007fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd280d7c1
bl _p_24
.word 0xf900d7a0
.word 0xf9407fa0
.word 0xf900dba0
.word 0xd28017a0
bl _p_107
.word 0xf940d7a1
.word 0xf940dba2
.word 0xf900d3a0
bl _p_108
.word 0xf940d3a0
bl _p_25
.loc 8 618 0
.word 0xd2800020
.word 0x2b000300
.word 0x10000011
.word 0x54001ba6
.word 0xaa0003f4
.loc 8 619 0
.word 0x14000014
.word 0xf9009ba0
.word 0xf9409ba0
.loc 8 620 0
.word 0xf90083a0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd280d7c1
bl _p_24
.word 0xf900d7a0
.word 0xf94083a0
.word 0xf900dba0
.word 0xd28017a0
bl _p_107
.word 0xf940d7a1
.word 0xf940dba2
.word 0xf900d3a0
bl _p_108
.word 0xf940d3a0
bl _p_25
.loc 8 622 0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xaa1403e1
bl _p_35
.word 0xaa0003f4
.loc 8 624 0
.word 0xaa1803f3
.loc 8 626 0
.word 0xb9010bbf
.word 0x14000030
.loc 8 628 0
.word 0xb9810ba0
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54001669
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb9400000
.word 0xb90113a0
.loc 8 630 0
.word 0xd280013a
.word 0x1400001b
.loc 8 632 0
.word 0x51000660
.word 0xaa0003e1
.word 0xaa0003f3
.word 0xb94113a0
.word 0xd2800142
.word 0xf100005f
.word 0x10000011
.word 0x540014e0
.word 0x1ac2081e
.word 0x1b0283c2
.word 0x1100c042
.word 0x93407c21
.word 0xb9801a83
.word 0xeb01007f
.word 0x10000011
.word 0x54001389
.word 0xd37ff821
.word 0x8b010281
.word 0x91008021
.word 0x79000022
.loc 8 633 0
.word 0xd29999a1
.word 0xf2b99981
.word 0x2a0003e0
.word 0x9b017c00
.word 0xd363fc00
.word 0xb90113a0
.loc 8 630 0
.word 0x51000741
.word 0xaa0103e0
.word 0xaa0103fa
.word 0x6b1f001f
.word 0x54fffc4a
.loc 8 626 0
.word 0xb9810ba0
.word 0x11000400
.word 0xb9010ba0
.word 0x510006a1
.word 0xb9810ba0
.word 0x6b01001f
.word 0x54fff9cb
.loc 8 636 0
.word 0x510006a0
.word 0x93407c00
.word 0xb9801ae1
.word 0xeb00003f
.word 0x10000011
.word 0x54001009
.word 0xd37ef400
.word 0x8b0002e0
.word 0x91008000
.word 0xb940001a
.word 0x14000019
.loc 8 638 0
.word 0x51000661
.word 0xaa0103e0
.word 0xaa0103f3
.word 0xd2800141
.word 0xf100003f
.word 0x10000011
.word 0x54000ee0
.word 0x1ac10b5e
.word 0x1b01ebc1
.word 0x1100c021
.word 0x93407c00
.word 0xb9801a82
.word 0xeb00005f
.word 0x10000011
.word 0x54000d89
.word 0xd37ff800
.word 0x8b000280
.word 0x91008000
.word 0x79000001
.loc 8 639 0
.word 0xd29999a1
.word 0xf2b99981
.word 0x2a1a03e0
.word 0x9b017c00
.word 0xd363fc1a
.loc 8 636 0
.word 0x35fffd1a
.loc 8 642 0
.word 0x35000256
.loc 8 645 0
.word 0xf9402ba0
.word 0xf9003fa0
.word 0xf9402fa0
.word 0xf90043a0
.word 0xb9807ba0
.word 0x6b1f001f
.word 0x9a9fa7fa
.loc 8 648 0
.word 0x4b130317
.loc 8 650 0
.word 0xaa1703e1
.word 0xd28003a0
.word 0xaa1a03e2
.word 0xaa1903e3
.word 0xf94033a4
.word 0xaa1403e5
.word 0xaa1303e6
bl _p_109
.word 0x14000049
.loc 8 659 0
.word 0x4b13031a
.word 0x14000011
.loc 8 663 0
.word 0x51000661
.word 0xaa0103e0
.word 0xaa0103f3
.word 0x93407c00
.word 0xb9801a81
.word 0xeb00003f
.word 0x10000011
.word 0x540008c9
.word 0xd37ff800
.word 0x8b000280
.word 0x91008000
.word 0xd280061e
.word 0x7900001e
.loc 8 664 0
.word 0xb980dba0
.word 0x51000400
.word 0xb900dba0
.loc 8 660 0
.word 0xb980dba0
.word 0x6b1f001f
.word 0x5400008d
.word 0xb980dba0
.word 0x6b1a001f
.word 0x54fffd6c
.loc 8 666 0
.word 0xf9402ba0
.word 0xf90037a0
.word 0xf9402fa0
.word 0xf9003ba0
.word 0xb9806ba0
.word 0x6b1f001f
.word 0x540004aa
.loc 8 668 0
.word 0xf94033a0
.word 0xf940001e
.word 0xf9401801
.loc 8 669 0
.word 0xf940001e
.word 0xf9401800
.word 0xb9801000
.word 0x5100041a
.word 0x14000019
.loc 8 670 0
.word 0x51000661
.word 0xaa0103e0
.word 0xaa0103f3
.word 0xf94033a1
.word 0xf940003e
.word 0xf9401822
.word 0x93407f41
.word 0xb9801043
.word 0xeb01007f
.word 0x10000011
.word 0x540003c9
.word 0xd37ff821
.word 0x8b020021
.word 0x79402821
.word 0x93407c00
.word 0xb9801a82
.word 0xeb00005f
.word 0x10000011
.word 0x540002c9
.word 0xd37ff800
.word 0x8b000280
.word 0x91008000
.word 0x79000001
.loc 8 669 0
.word 0x5100075a
.word 0x9280001e
.word 0xf2bffffe
.word 0x6b1e035f
.word 0x54fffcac
.loc 8 672 0
.word 0x4b130303
.word 0xd2800000
.word 0xaa1403e1
.word 0xaa1303e2
bl _p_110
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8dc7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6
.word 0xd2801580
.word 0xaa1103e1
bl _p_6
.word 0xd2801f40
.word 0xaa1103e1
bl _p_6

Lme_71:
.text
ut_114:
add x0, x0, 16
b System_Numerics_BigNumber_BigNumberBuffer_Create
.text
	.align 4
	.no_dead_strip System_Numerics_BigNumber_BigNumberBuffer_Create
System_Numerics_BigNumber_BigNumberBuffer_Create:
.loc 8 298 0 prologue_end
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba8
.word 0xd2800000
.word 0xf9001ba0
.word 0xf9001fa0
.word 0xf90023a0
.word 0xd2800000
.word 0xf9001ba0
.word 0xf9001fa0
.word 0xf90023a0
.loc 8 299 0
.word 0x9100c3a0
.word 0xf9002fa0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #216]
.word 0xd2800601
bl _p_14
.word 0xf9002ba0
bl _p_99
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xf9001ba0
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 8 300 0
.word 0xf9401ba0
.word 0xf9000fa0
.word 0xf9401fa0
.word 0xf90013a0
.word 0xf94023a0
.word 0xf90017a0
.word 0xf9400ba0
.word 0xf9400fa1
.word 0xf9000001
.word 0xd349fc02
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0x91002000
.word 0xf94013a1
.word 0xf9000001
.word 0x91002000
.word 0xf94017a1
.word 0xf9000001
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_72:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_
System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_:
.file 9 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/external/corefx/src/System.Runtime.Numerics/src/System/Numerics/NumericsHelpers.cs"
.loc 9 29 0 prologue_end
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xfd0013a0
.word 0xf90017a0
.word 0xaa0103f8
.word 0xaa0203f9
.word 0xf9001ba3
.word 0xf9002bbf
.word 0xd2800000
.word 0xf9002ba0
.loc 9 30 0
.word 0xfd4013a0
.word 0xfd002ba0
.loc 9 32 0
.word 0xd2800020
.word 0xf9402ba1
.word 0xf90027a1
.word 0xf94027a1
.word 0xd37efc21
.word 0x93407c21
.word 0xd280005e
.word 0xa1e0021
.word 0x4b010001
.word 0xf94017a0
.word 0xb9000001
.loc 9 33 0
.word 0xf9402ba0
.word 0xf90023a0
.word 0xf94023a0
.word 0xd29fffe1
.word 0xf2bfffe1
.word 0xf2dfffe1
.word 0xf2e001e1
.word 0x8a010000
.word 0xf9000320
.loc 9 34 0
.word 0xf9402ba0
.word 0xf9001fa0
.word 0xf9401fa0
.word 0xd374fc00
.word 0x93407c00
.word 0xd280fffe
.word 0xa1e0000
.word 0xb9000300
.loc 9 35 0
.word 0xb9800300
.word 0x35000140
.loc 9 38 0
.word 0xf9401ba0
.word 0xd280003e
.word 0x3900001e
.loc 9 39 0
.word 0xf9400320
.word 0xb4000340
.loc 9 40 0
.word 0x9280863e
.word 0xf2bffffe
.word 0xb900031e
.loc 9 41 0
.word 0x14000016
.loc 9 42 0
.word 0xb9800300
.word 0xd280fffe
.word 0x6b1e001f
.word 0x540000e1
.loc 9 45 0
.word 0xf9401ba0
.word 0x3900001f
.loc 9 46 0
.word 0xd29ffffe
.word 0xf2affffe
.word 0xb900031e
.loc 9 47 0
.word 0x1400000c
.loc 9 50 0
.word 0xf9401ba0
.word 0xd280003e
.word 0x3900001e
.loc 9 51 0
.word 0xf9400320
.word 0xd2800001
.word 0xf2e00201
.word 0xaa010000
.word 0xf9000320
.loc 9 52 0
.word 0xb9800300
.word 0x5110cc00
.word 0xb9000300
.loc 9 54 0
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_73:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong
System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong:
.loc 9 59 0 prologue_end
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb7
.word 0xa901ebb9
.word 0xf90017a0
.word 0xaa0103f9
.word 0xaa0203fa
.word 0xf9001fbf
.word 0x9e6703e0
.word 0xfd001fa0
.loc 9 61 0
.word 0xb500009a
.loc 9 62 0
.word 0xd2800000
.word 0xf9001fa0
.word 0x14000035
.loc 9 66 0
.word 0xaa1a03e0
bl _p_111
.word 0x93407c00
.word 0x51002c17
.loc 9 67 0
.word 0xaa1703e0
.word 0x6b1f001f
.word 0x540000ca
.loc 9 68 0
.word 0x4b1703e0
.word 0xd28007fe
.word 0xa1e0000
.word 0x9ac0275a
.word 0x14000004
.loc 9 70 0
.word 0xd28007fe
.word 0xa1e02e0
.word 0x9ac0235a
.loc 9 71 0
.word 0x4b170339
.loc 9 76 0
.word 0xaa1903e0
.word 0x1110cc19
.loc 9 78 0
.word 0xaa1903e0
.word 0xd280fffe
.word 0x6b1e001f
.word 0x540000ab
.loc 9 81 0
.word 0xd2800000
.word 0xf2effe00
.word 0xf9001fa0
.loc 9 82 0
.word 0x1400001b
.loc 9 83 0
.word 0x6b1f033f
.word 0x5400020c
.loc 9 86 0
.word 0x51000739
.loc 9 87 0
.word 0xaa1903e0
.word 0x9280067e
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x5400008a
.loc 9 90 0
.word 0xd2800000
.word 0xf9001fa0
.loc 9 91 0
.word 0x14000010
.loc 9 94 0
.word 0x4b1903e0
.word 0xd28007fe
.word 0xa1e0000
.word 0x9ac02740
.word 0xf9001fa0
.loc 9 97 0
.word 0x1400000a
.loc 9 101 0
.word 0xd29fffe0
.word 0xf2bfffe0
.word 0xf2dfffe0
.word 0xf2e001e0
.word 0x8a000340
.word 0x93407f21
.word 0xd34c2c21
.word 0xaa010000
.word 0xf9001fa0
.loc 9 105 0
.word 0xb9802ba0
.word 0x6b1f001f
.word 0x540000ca
.loc 9 106 0
.word 0xf9401fa0
.word 0xd2800001
.word 0xf2f00001
.word 0xaa010000
.word 0xf9001fa0
.loc 9 108 0
.word 0xf9401fa0
.word 0xf9001ba0
.word 0xfd401ba0
.word 0xf9400bb7
.word 0xa941ebb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_74:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__
System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__:
.loc 9 115 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa
.word 0xb400099a
.word 0xb9801b40
.word 0x34000940
.loc 9 117 0
.word 0xb9801b40
.word 0xeb1f001f
.word 0x10000011
.word 0x54000949
.word 0xb9402340
.word 0x2a2003e0
.word 0x11000400
.word 0xb9801b41
.word 0xeb1f003f
.word 0x10000011
.word 0x54000869
.word 0xb9002340
.loc 9 119 0
.word 0xd2800039
.word 0x14000016
.loc 9 123 0
.word 0x93407f20
.word 0xb9801b41
.word 0xeb00003f
.word 0x10000011
.word 0x54000769
.word 0xd37ef400
.word 0x8b000340
.word 0x91008000
.word 0xb9400000
.word 0x2a2003e0
.word 0x11000401
.word 0x93407f20
.word 0xb9801b42
.word 0xeb00005f
.word 0x10000011
.word 0x54000609
.word 0xd37ef400
.word 0x8b000340
.word 0x91008000
.word 0xb9000001
.loc 9 121 0
.word 0x11000739
.word 0x51000720
.word 0x93407c00
.word 0xb9801b41
.word 0xeb00003f
.word 0x10000011
.word 0x540004a9
.word 0xd37ef400
.word 0x8b000340
.word 0x91008000
.word 0xb9400000
.word 0x35000320
.word 0xb9801b40
.word 0x6b00033f
.word 0x54fffbcb
.word 0x14000015
.loc 9 128 0
.word 0x93407f20
.word 0xb9801b41
.word 0xeb00003f
.word 0x10000011
.word 0x540002e9
.word 0xd37ef400
.word 0x8b000340
.word 0x91008000
.word 0xb9400000
.word 0x2a2003e1
.word 0x93407f20
.word 0xb9801b42
.word 0xeb00005f
.word 0x10000011
.word 0x540001a9
.word 0xd37ef400
.word 0x8b000340
.word 0x91008000
.word 0xb9000001
.loc 9 126 0
.word 0x11000739
.word 0xb9801b40
.word 0x6b00033f
.word 0x54fffd4b
.loc 9 131 0
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_75:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_MakeUlong_uint_uint
System_Numerics_NumericsHelpers_MakeUlong_uint_uint:
.loc 9 135 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xb94013a0
.word 0x2a0003e0
.word 0xd3607c00
.word 0xb9401ba1
.word 0x2a0103e1
.word 0xaa010000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_76:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_Abs_int
System_Numerics_NumericsHelpers_Abs_int:
.loc 9 142 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xb98013a0
.word 0x131f7c01
.loc 9 143 0
.word 0x4a010000
.word 0x4b010000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_77:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_CombineHash_uint_uint
System_Numerics_NumericsHelpers_CombineHash_uint_uint:
.loc 9 149 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xb94013a1
.word 0x53196020
.word 0x53197c21
.word 0x2a010000
.word 0xb9401ba1
.word 0x4a010000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_78:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_CombineHash_int_int
System_Numerics_NumericsHelpers_CombineHash_int_int:
.loc 9 154 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xb98013a1
.word 0x53196020
.word 0x53197c21
.word 0x2a010000
.word 0xb9801ba1
.word 0x4a010000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_79:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_CbitHighZero_uint
System_Numerics_NumericsHelpers_CbitHighZero_uint:
.loc 9 159 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa
.word 0x3500007a
.loc 9 160 0
.word 0xd2800400
.word 0x14000020
.loc 9 162 0
.word 0xd2800019
.loc 9 163 0
.word 0x929ffffe
.word 0xf2bffffe
.word 0xa1e0340
.word 0x35000060
.loc 9 165 0
.word 0x11004339
.loc 9 166 0
.word 0x53103f5a
.loc 9 168 0
.word 0x929ffffe
.word 0xf2bfe01e
.word 0xa1e0340
.word 0x35000060
.loc 9 170 0
.word 0x11002339
.loc 9 171 0
.word 0x53185f5a
.loc 9 173 0
.word 0x929ffffe
.word 0xf2be001e
.word 0xa1e0340
.word 0x35000060
.loc 9 175 0
.word 0x11001339
.loc 9 176 0
.word 0x531c6f5a
.loc 9 178 0
.word 0x929ffffe
.word 0xf2b8001e
.word 0xa1e0340
.word 0x35000060
.loc 9 180 0
.word 0x11000b39
.loc 9 181 0
.word 0x531e775a
.loc 9 183 0
.word 0x929ffffe
.word 0xf2b0001e
.word 0xa1e0340
.word 0x35000040
.loc 9 184 0
.word 0x11000739
.loc 9 185 0
.word 0xaa1903e0
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_7a:
.text
	.align 4
	.no_dead_strip System_Numerics_NumericsHelpers_CbitHighZero_ulong
System_Numerics_NumericsHelpers_CbitHighZero_ulong:
.loc 9 190 0 prologue_end
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xd2800001
.word 0xf2dfffe1
.word 0xf2ffffe1
.word 0xf9400ba0
.word 0x8a010000
.word 0xb50000e0
.loc 9 191 0
.word 0xf9400ba0
.word 0xaa0003e0
bl _p_71
.word 0x93407c00
.word 0x11008000
.word 0x14000006
.loc 9 192 0
.word 0xf9400ba0
.word 0xd360fc00
.word 0xaa0003e0
bl _p_71
.word 0x93407c00
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_7b:
.text
	.align 4
	.no_dead_strip wrapper_unknown_uint___Get_int
wrapper_unknown_uint___Get_int:
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xb9801ba0
.word 0x93407c01
.word 0xf9400ba0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000109
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb9400000
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_7f:
.text
	.align 4
	.no_dead_strip wrapper_runtime_invoke__Module_runtime_invoke_uint__this___int_object_intptr_intptr_intptr
wrapper_runtime_invoke__Module_runtime_invoke_uint__this___int_object_intptr_intptr_intptr:
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000bb7
.word 0xf9000fa0
.word 0xf90013a1
.word 0xf90017a2
.word 0xf9001ba3
.word 0xf9001fbf
.word 0xf90023bf
.word 0xf94017a0
.word 0xb40005c0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #888]
.word 0xb9400000
.word 0x340000c0
bl _p_112
.word 0xaa0003f7
.word 0xb4000060
.word 0xaa1703e0
bl _p_25
.word 0xf94013a0
.word 0xf9400000
.word 0xb9800001
.word 0xf9400fa0
.word 0xf9401ba2
.word 0xd63f0040
.word 0xf9003ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #896]
.word 0xd2800281
bl _p_14
.word 0xf9403ba1
.word 0xb9001001
.word 0xf9001fa0
.word 0x14000012
.word 0xf90027a0
.word 0xf94027a0
.word 0xf90023a0
.word 0xf94017a1
.word 0xf94023a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0x14000001
.word 0xf9401fa0
.word 0x14000017

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #888]
.word 0xb9400000
.word 0x350002c0
.word 0x14000001
.word 0xf94013a0
.word 0xf9400000
.word 0xb9800001
.word 0xf9400fa0
.word 0xf9401ba2
.word 0xd63f0040
.word 0xf9003ba0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #896]
.word 0xd2800281
bl _p_14
.word 0xf9403ba1
.word 0xb9001001
.word 0xf9001fa0
.word 0xf9401fa0
.word 0xf9400bb7
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
bl _p_112
.word 0xaa0003f7
.word 0xb4fffd40
.word 0xaa1703e0
bl _p_25

Lme_80:
.text
	.align 4
	.no_dead_strip wrapper_unknown_uint___Set_int_uint
wrapper_unknown_uint___Set_int_uint:
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xb9801ba0
.word 0x93407c01
.word 0xf9400ba0
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54000129
.word 0xd37ef421
.word 0x8b010000
.word 0x91008000
.word 0xb94023a1
.word 0xb9000001
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_6

Lme_81:
.text
	.align 4
	.no_dead_strip wrapper_runtime_invoke__Module_runtime_invoke_void__this___int_uint_object_intptr_intptr_intptr
wrapper_runtime_invoke__Module_runtime_invoke_void__this___int_uint_object_intptr_intptr_intptr:
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bb7
.word 0xf9000fb9
.word 0xf90013a0
.word 0xaa0103f9
.word 0xf90017a2
.word 0xf9001ba3
.word 0xf9001fbf
.word 0xf90023bf
.word 0xf94017a0
.word 0xb40004c0

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #888]
.word 0xb9400000
.word 0x340000c0
bl _p_112
.word 0xaa0003f7
.word 0xb4000060
.word 0xaa1703e0
bl _p_25
.word 0xf9400320
.word 0xb9800001
.word 0xf9400720
.word 0xb9400002
.word 0xf94013a0
.word 0xf9401ba3
.word 0xd63f0060
.word 0x14000012
.word 0xf90027a0
.word 0xf94027a0
.word 0xf90023a0
.word 0xf94017a1
.word 0xf94023a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0x14000001
.word 0xf9401fa0
.word 0x1400000f

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x0, [x16, #888]
.word 0xb9400000
.word 0x350001e0
.word 0x14000001
.word 0xf9400320
.word 0xb9800001
.word 0xf9400720
.word 0xb9400002
.word 0xf94013a0
.word 0xf9401ba3
.word 0xd63f0060
.word 0xf9401fa0
.word 0xf9400bb7
.word 0xf9400fb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
bl _p_112
.word 0xaa0003f7
.word 0xb4fffe20
.word 0xaa1703e0
bl _p_25

Lme_82:
.text
ut_131:
add x0, x0, 16
b wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_StructureToPtr_object_intptr_bool
.text
	.align 4
	.no_dead_strip wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_StructureToPtr_object_intptr_bool
wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_StructureToPtr_object_intptr_bool:
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf9400ba0
.word 0x91004001
.word 0xb9801002
.word 0xf9400fa0
.word 0xb9000002
.word 0x91001021
.word 0x91001000
.word 0xb9800022
.word 0xb9000002
.word 0x91001021
.word 0x91001000
.word 0x39400022
.word 0xb9000002
.word 0x91002021
.word 0x91002000
.word 0xf9400021
.word 0xf9000001
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_83:
.text
ut_132:
add x0, x0, 16
b wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_PtrToStructure_intptr_object
.text
	.align 4
	.no_dead_strip wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_PtrToStructure_intptr_object
wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_PtrToStructure_intptr_object:
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xf9400340
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x540004e1
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #904]
.word 0xeb01001f
.word 0x10000011
.word 0x540003e1
.word 0x91004358
.word 0xb9800320
.word 0xb9000300
.word 0x9100133a
.word 0x91001318
.word 0xb9800340
.word 0xb9000300
.word 0x9100135a
.word 0x91001318
.word 0xaa1a03f9
.word 0xf90017b8
.word 0xb9800340
.word 0xaa1803fa
.word 0x34000060
.word 0xd2800038
.word 0x14000002
.word 0xd2800018
.word 0x39000358
.word 0x9100233a
.word 0xf94017a0
.word 0x91002018
.word 0xf9400340
.word 0xf9000300
.word 0x9100235a
.word 0x91002318
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_6

Lme_84:
.text
ut_133:
add x0, x0, 16
b wrapper_unknown_System_Numerics_DoubleUlong_StructureToPtr_object_intptr_bool
.text
	.align 4
	.no_dead_strip wrapper_unknown_System_Numerics_DoubleUlong_StructureToPtr_object_intptr_bool
wrapper_unknown_System_Numerics_DoubleUlong_StructureToPtr_object_intptr_bool:
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf9400ba0
.word 0xf9400801
.word 0xf9400fa0
.word 0xf9000001
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_85:
.text
ut_134:
add x0, x0, 16
b wrapper_unknown_System_Numerics_DoubleUlong_PtrToStructure_intptr_object
.text
	.align 4
	.no_dead_strip wrapper_unknown_System_Numerics_DoubleUlong_PtrToStructure_intptr_object
wrapper_unknown_System_Numerics_DoubleUlong_PtrToStructure_intptr_object:
.word 0xa9be7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf9400fa0
.word 0x91004000
.word 0xf9400ba1
.word 0xf9400021
.word 0xf9000001
.word 0x910003bf
.word 0xa8c27bfd
.word 0xd65f03c0

Lme_86:
.text
	.align 4
	.no_dead_strip System_Array_Resize_T_INT_T_INT____int
System_Array_Resize_T_INT_T_INT____int:
.file 10 "/Library/Frameworks/Xamarin.iOS.framework/Versions/11.0.0.0/src/mono/mcs/class/corlib/corert/Array.Portable.cs"
.loc 10 55 0 prologue_end
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xf9001baf
.word 0xaa0003f9
.word 0xaa0103fa
.word 0x6b1f035f
.word 0x5400078b
.loc 10 58 0
.word 0xf9400338
.loc 10 59 0
.word 0xaa1803e0
.word 0xb5000220
.loc 10 61 0
.word 0xf9401ba0
bl _p_113
.word 0xaa1a03e1
bl _p_35
.word 0xf9000320
.word 0xd349ff21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.loc 10 62 0
.word 0x14000024
.loc 10 65 0
.word 0xb9801b00
.word 0x6b1a001f
.word 0x54000420
.loc 10 67 0
.word 0xf9401ba0
bl _p_113
.word 0xaa1a03e1
bl _p_35
.word 0xaa0003f7
.loc 10 68 0
.word 0xb9801b00
.word 0xf9001fb8
.word 0xb90043bf
.word 0xf90027b7
.word 0xb90053bf
.word 0x6b1a001f
.word 0x5400006c
.word 0xb9801b18
.word 0x14000002
.word 0xaa1a03f8
.word 0xf9401fa0
.word 0xb98043a1
.word 0xf94027a2
.word 0xb98053a3
.word 0xaa1803e4
bl _p_45
.loc 10 69 0
.word 0xf9000337
.word 0xd349ff20
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.loc 10 71 0
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.loc 10 56 0
.word 0xd29b4a60
bl _p_114
.word 0xf90033a0
.word 0xd286d540
bl _p_114
.word 0xaa0003e2
.word 0xf94033a1
.word 0xd2801180
.word 0xf2a04000
bl _mono_create_corlib_exception_2
bl _p_25

Lme_87:
.text
	.align 3
jit_code_end:

	.byte 0,0,0,0
.text
	.align 3
method_addresses:
	.no_dead_strip method_addresses
bl SR_Format_string_object
bl System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int
bl System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_
bl System_Globalization_FormatProvider_Number_IsWhite_char
bl System_Globalization_FormatProvider_Number_MatchChars_char__string
bl System_Globalization_FormatProvider_Number_MatchChars_char__char_
bl System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
bl System_Globalization_FormatProvider_Number_TrailingZeros_string_int
bl System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
bl System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int
bl System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_
bl System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool
bl System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
bl System_Globalization_FormatProvider_Number_wcslen_char_
bl System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string
bl System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
bl System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char
bl System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool
bl System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool
bl System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
bl System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int
bl System_Globalization_FormatProvider_Number_FindSection_string_int
bl System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo
bl System_Globalization_FormatProvider_Number__cctor
bl System_Globalization_FormatProvider_Number_NumberBuffer_get_digits
bl System_Numerics_BigInteger__ctor_int
bl System_Numerics_BigInteger__ctor_uint
bl System_Numerics_BigInteger__ctor_long
bl System_Numerics_BigInteger__ctor_ulong
bl System_Numerics_BigInteger__ctor_single
bl System_Numerics_BigInteger__ctor_double
bl System_Numerics_BigInteger__ctor_System_Decimal
bl System_Numerics_BigInteger__ctor_byte__
bl System_Numerics_BigInteger__ctor_int_uint__
bl System_Numerics_BigInteger__ctor_uint___bool
bl System_Numerics_BigInteger_get_Zero
bl System_Numerics_BigInteger_get_IsPowerOfTwo
bl System_Numerics_BigInteger_get_IsZero
bl System_Numerics_BigInteger_get_IsOne
bl System_Numerics_BigInteger_get_IsEven
bl System_Numerics_BigInteger_get_Sign
bl System_Numerics_BigInteger_Parse_string_System_IFormatProvider
bl System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider
bl System_Numerics_BigInteger_GetHashCode
bl System_Numerics_BigInteger_Equals_object
bl System_Numerics_BigInteger_Equals_long
bl System_Numerics_BigInteger_Equals_System_Numerics_BigInteger
bl System_Numerics_BigInteger_CompareTo_long
bl System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger
bl System_Numerics_BigInteger_CompareTo_object
bl System_Numerics_BigInteger_ToByteArray
bl System_Numerics_BigInteger_ToUInt32Array
bl System_Numerics_BigInteger_ToString
bl System_Numerics_BigInteger_ToString_System_IFormatProvider
bl System_Numerics_BigInteger_ToString_string_System_IFormatProvider
bl System_Numerics_BigInteger_Add_uint___int_uint___int
bl System_Numerics_BigInteger_op_Subtraction_System_Numerics_BigInteger_System_Numerics_BigInteger
bl System_Numerics_BigInteger_Subtract_uint___int_uint___int
bl System_Numerics_BigInteger_op_Implicit_byte
bl System_Numerics_BigInteger_op_Implicit_sbyte
bl System_Numerics_BigInteger_op_Implicit_int16
bl System_Numerics_BigInteger_op_Implicit_uint16
bl System_Numerics_BigInteger_op_Implicit_int
bl System_Numerics_BigInteger_op_Implicit_uint
bl System_Numerics_BigInteger_op_Implicit_long
bl System_Numerics_BigInteger_op_Implicit_ulong
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_1
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_2
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_3
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_4
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_5
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_6
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_7
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_8
bl System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_9
bl System_Numerics_BigInteger_op_UnaryNegation_System_Numerics_BigInteger
bl System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger
bl System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger
bl System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_System_Numerics_BigInteger
bl System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_System_Numerics_BigInteger
bl System_Numerics_BigInteger_op_LessThan_System_Numerics_BigInteger_long
bl System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_long
bl System_Numerics_BigInteger_op_Equality_System_Numerics_BigInteger_long
bl System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_long
bl System_Numerics_BigInteger_op_LessThan_long_System_Numerics_BigInteger
bl System_Numerics_BigInteger_op_LessThanOrEqual_long_System_Numerics_BigInteger
bl System_Numerics_BigInteger_GetDiffLength_uint___uint___int
bl System_Numerics_BigInteger_AssertValid
bl System_Numerics_BigInteger__cctor
bl System_Numerics_BigIntegerCalculator_Add_uint___uint
bl System_Numerics_BigIntegerCalculator_Add_uint___uint__
bl System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int
bl System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int
bl System_Numerics_BigIntegerCalculator_Subtract_uint___uint
bl System_Numerics_BigIntegerCalculator_Subtract_uint___uint__
bl System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int
bl System_Numerics_BigIntegerCalculator_Compare_uint___uint__
bl System_Numerics_BigIntegerCalculator_Square_uint__
bl System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int
bl System_Numerics_BigIntegerCalculator_Multiply_uint___uint
bl System_Numerics_BigIntegerCalculator_Multiply_uint___uint__
bl System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int
bl System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int
bl System_Numerics_BigIntegerCalculator__cctor
bl System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_
bl System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_
bl System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo
bl System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
bl System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
bl System_Numerics_BigNumber_ParseFormatSpecifier_string_int_
bl System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo
bl System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo
bl System_Numerics_BigNumber_BigNumberBuffer_Create
bl System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_
bl System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong
bl System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__
bl System_Numerics_NumericsHelpers_MakeUlong_uint_uint
bl System_Numerics_NumericsHelpers_Abs_int
bl System_Numerics_NumericsHelpers_CombineHash_uint_uint
bl System_Numerics_NumericsHelpers_CombineHash_int_int
bl System_Numerics_NumericsHelpers_CbitHighZero_uint
bl System_Numerics_NumericsHelpers_CbitHighZero_ulong
bl method_addresses
bl method_addresses
bl method_addresses
bl wrapper_unknown_uint___Get_int
bl wrapper_runtime_invoke__Module_runtime_invoke_uint__this___int_object_intptr_intptr_intptr
bl wrapper_unknown_uint___Set_int_uint
bl wrapper_runtime_invoke__Module_runtime_invoke_void__this___int_uint_object_intptr_intptr_intptr
bl wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_StructureToPtr_object_intptr_bool
bl wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_PtrToStructure_intptr_object
bl wrapper_unknown_System_Numerics_DoubleUlong_StructureToPtr_object_intptr_bool
bl wrapper_unknown_System_Numerics_DoubleUlong_PtrToStructure_intptr_object
bl System_Array_Resize_T_INT_T_INT____int
method_addresses_end:

.section __TEXT, __const
	.align 3
unbox_trampolines:

	.long 24,25,26,27,28,29,30,31
	.long 32,33,34,35,36,37,38,39
	.long 40,41,42,43,44,45,46,47
	.long 48,49,50,51,52,53,54,55
	.long 56,57,58,59,60,61,62,63
	.long 64,65,66,67,68,69,70,71
	.long 72,73,74,75,76,77,78,79
	.long 80,81,82,83,84,85,86,87
	.long 88,89,90,114,131,132,133,134
unbox_trampolines_end:

	.long 0
.text
	.align 3
unbox_trampoline_addresses:
bl ut_24
bl ut_25
bl ut_26
bl ut_27
bl ut_28
bl ut_29
bl ut_30
bl ut_31
bl ut_32
bl ut_33
bl ut_34
bl ut_35
bl ut_36
bl ut_37
bl ut_38
bl ut_39
bl ut_40
bl ut_41
bl ut_42
bl ut_43
bl ut_44
bl ut_45
bl ut_46
bl ut_47
bl ut_48
bl ut_49
bl ut_50
bl ut_51
bl ut_52
bl ut_53
bl ut_54
bl ut_55
bl ut_56
bl ut_57
bl ut_58
bl ut_59
bl ut_60
bl ut_61
bl ut_62
bl ut_63
bl ut_64
bl ut_65
bl ut_66
bl ut_67
bl ut_68
bl ut_69
bl ut_70
bl ut_71
bl ut_72
bl ut_73
bl ut_74
bl ut_75
bl ut_76
bl ut_77
bl ut_78
bl ut_79
bl ut_80
bl ut_81
bl ut_82
bl ut_83
bl ut_84
bl ut_85
bl ut_86
bl ut_87
bl ut_88
bl ut_89
bl ut_90
bl ut_114
bl ut_131
bl ut_132
bl ut_133
bl ut_134

	.long 0
.section __TEXT, __const
	.align 3
unwind_info:

	.byte 0,13,12,31,0,68,14,64,157,8,158,7,68,13,29,20,12,31,0,68,14,224,1,157,28,158,27,68,13,29,68,147
	.byte 26,68,154,25,14,12,31,0,68,14,176,1,157,22,158,21,68,13,29,13,12,31,0,68,14,32,157,4,158,3,68,13
	.byte 29,16,12,31,0,68,14,48,157,6,158,5,68,13,29,68,152,4,18,12,31,0,68,14,32,157,4,158,3,68,13,29
	.byte 68,153,2,154,1,30,12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,147,20,148,19,68,150,18,68,152,17,153
	.byte 16,68,154,15,18,12,31,0,68,14,48,157,6,158,5,68,13,29,68,152,4,153,3,19,12,31,0,68,14,96,157,12
	.byte 158,11,68,13,29,68,147,10,68,149,9,26,12,31,0,68,14,64,157,8,158,7,68,13,29,68,150,6,151,5,68,152
	.byte 4,153,3,68,154,2,23,12,31,0,68,14,64,157,8,158,7,68,13,29,68,150,6,151,5,68,152,4,153,3,32,12
	.byte 31,0,84,14,192,4,157,72,158,71,68,13,29,68,148,70,149,69,68,150,68,151,67,68,152,66,153,65,68,154,64,32
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,148,14,149,13,68,150,12,151,11,68,152,10,153,9,68,154,8
	.byte 34,12,31,0,68,14,224,1,157,28,158,27,68,13,29,68,147,26,148,25,68,149,24,150,23,68,151,22,152,21,68,153
	.byte 20,154,19,28,12,31,0,68,14,112,157,14,158,13,68,13,29,68,148,12,149,11,68,150,10,151,9,68,152,8,153,7
	.byte 28,12,31,0,68,14,80,157,10,158,9,68,13,29,68,149,8,150,7,68,151,6,152,5,68,153,4,154,3,32,12,31
	.byte 0,68,14,144,1,157,18,158,17,68,13,29,68,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,154,10,23,12
	.byte 31,0,68,14,48,157,6,158,5,68,13,29,68,151,4,152,3,68,153,2,154,1,34,12,31,0,68,14,192,3,157,56
	.byte 158,55,68,13,29,68,147,54,148,53,68,149,52,150,51,68,151,50,152,49,68,153,48,154,47,14,12,31,0,68,14,224
	.byte 2,157,44,158,43,68,13,29,13,12,31,0,68,14,48,157,6,158,5,68,13,29,16,12,31,0,68,14,32,157,4,158
	.byte 3,68,13,29,68,153,2,21,12,31,0,68,14,64,157,8,158,7,68,13,29,68,152,6,153,5,68,154,4,22,12,31
	.byte 0,68,14,192,1,157,24,158,23,68,13,29,68,152,22,153,21,68,154,20,24,12,31,0,68,14,128,1,157,16,158,15
	.byte 68,13,29,68,151,14,152,13,68,153,12,154,11,34,12,31,0,68,14,192,1,157,24,158,23,68,13,29,68,147,22,148
	.byte 21,68,149,20,150,19,68,151,18,152,17,68,153,16,154,15,26,12,31,0,68,14,96,157,12,158,11,68,13,29,68,150
	.byte 10,151,9,68,152,8,153,7,68,154,6,21,12,31,0,68,14,48,157,6,158,5,68,13,29,68,152,4,153,3,68,154
	.byte 2,19,12,31,0,68,14,64,157,8,158,7,68,13,29,68,152,6,68,154,5,21,12,31,0,68,14,48,157,6,158,5
	.byte 68,13,29,68,151,4,68,153,3,154,2,19,12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,153,14,154,13,23
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,151,6,152,5,68,153,4,154,3,19,12,31,0,68,14,208,1,157
	.byte 26,158,25,68,13,29,68,153,24,154,23,19,12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,68,154,7,34
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,147,14,148,13,68,149,12,150,11,68,151,10,152,9,68,153,8
	.byte 154,7,28,12,31,0,68,14,64,157,8,158,7,68,13,29,68,149,6,150,5,68,151,4,152,3,68,153,2,154,1,27
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,149,20,150,19,68,151,18,152,17,68,153,16,14,12,31,0,68
	.byte 14,240,1,157,30,158,29,68,13,29,14,12,31,0,68,14,144,1,157,18,158,17,68,13,29,13,12,31,0,68,14,112
	.byte 157,14,158,13,68,13,29,19,12,31,0,68,14,192,1,157,24,158,23,68,13,29,68,153,22,154,21,14,12,31,0,68
	.byte 14,160,1,157,20,158,19,68,13,29,26,12,31,0,68,14,112,157,14,158,13,68,13,29,68,150,12,151,11,68,152,10
	.byte 153,9,68,154,8,24,12,31,0,68,14,240,1,157,30,158,29,68,13,29,68,151,28,152,27,68,153,26,154,25,13,12
	.byte 31,0,68,14,96,157,12,158,11,68,13,29,19,12,31,0,84,14,240,4,157,78,158,77,68,13,29,68,153,76,154,75
	.byte 21,12,31,0,68,14,48,157,6,158,5,68,13,29,68,151,4,152,3,68,153,2,26,12,31,0,68,14,64,157,8,158
	.byte 7,68,13,29,68,149,6,150,5,68,151,4,152,3,68,153,2,31,12,31,0,68,14,80,157,10,158,9,68,13,29,68
	.byte 148,8,149,7,68,150,6,151,5,68,152,4,153,3,68,154,2,34,12,31,0,68,14,176,1,157,22,158,21,68,13,29
	.byte 68,147,20,148,19,68,149,18,150,17,68,151,16,152,15,68,153,14,154,13,34,12,31,0,68,14,240,1,157,30,158,29
	.byte 68,13,29,68,147,28,148,27,68,149,26,150,25,68,151,24,152,23,68,153,22,154,21,33,12,31,0,68,14,80,157,10
	.byte 158,9,68,13,29,68,147,8,148,7,68,149,6,150,5,68,151,4,152,3,68,153,2,154,1,13,12,31,0,68,14,16
	.byte 157,2,158,1,68,13,29,16,12,31,0,68,14,80,157,10,158,9,68,13,29,68,154,8,14,12,31,0,68,14,128,1
	.byte 157,16,158,15,68,13,29,13,12,31,0,68,14,80,157,10,158,9,68,13,29,24,12,31,0,68,14,176,2,157,38,158
	.byte 37,68,13,29,68,151,36,152,35,68,153,34,154,33,30,12,31,0,68,14,192,1,157,24,158,23,68,13,29,68,148,22
	.byte 149,21,68,150,20,151,19,68,152,18,68,154,17,18,12,31,0,68,14,96,157,12,158,11,68,13,29,68,152,10,153,9
	.byte 21,12,31,0,68,14,64,157,8,158,7,68,13,29,68,151,6,68,153,5,154,4,17,12,31,0,68,14,128,1,157,16
	.byte 158,15,68,13,29,68,151,14,19,12,31,0,68,14,112,157,14,158,13,68,13,29,68,151,12,68,153,11,23,12,31,0
	.byte 68,14,112,157,14,158,13,68,13,29,68,151,12,152,11,68,153,10,154,9

.text
	.align 4
plt:
mono_aot_System_Numerics_plt:
	.no_dead_strip plt__jit_icall_mono_generic_class_init
plt__jit_icall_mono_generic_class_init:
_p_1:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #920]
br x16
.word 1485
	.no_dead_strip plt_string_Format_System_IFormatProvider_string_object
plt_string_Format_System_IFormatProvider_string_object:
_p_2:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #928]
br x16
.word 1511
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_
plt_System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_:
_p_3:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #936]
br x16
.word 1516
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool
plt_System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool:
_p_4:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #944]
br x16
.word 1518
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo
plt_System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo:
_p_5:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #952]
br x16
.word 1520
	.no_dead_strip plt__jit_icall_mono_arch_throw_corlib_exception
plt__jit_icall_mono_arch_throw_corlib_exception:
_p_6:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #960]
br x16
.word 1522
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
plt_System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool:
_p_7:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #968]
br x16
.word 1557
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_MatchChars_char__char_
plt_System_Globalization_FormatProvider_Number_MatchChars_char__char_:
_p_8:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #976]
br x16
.word 1559
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_IsWhite_char
plt_System_Globalization_FormatProvider_Number_IsWhite_char:
_p_9:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #984]
br x16
.word 1561
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_MatchChars_char__string
plt_System_Globalization_FormatProvider_Number_MatchChars_char__string:
_p_10:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #992]
br x16
.word 1563
	.no_dead_strip plt_System_Text_StringBuilder_Append_char
plt_System_Text_StringBuilder_Append_char:
_p_11:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1000]
br x16
.word 1565
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
plt_System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool:
_p_12:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1008]
br x16
.word 1570
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_TrailingZeros_string_int
plt_System_Globalization_FormatProvider_Number_TrailingZeros_string_int:
_p_13:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1016]
br x16
.word 1572
	.no_dead_strip plt_wrapper_alloc_object_AllocSmall_intptr_intptr
plt_wrapper_alloc_object_AllocSmall_intptr_intptr:
_p_14:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1024]
br x16
.word 1574
	.no_dead_strip plt_System_Text_StringBuilder__ctor_int
plt_System_Text_StringBuilder__ctor_int:
_p_15:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1032]
br x16
.word 1582
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int
plt_System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int:
_p_16:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1040]
br x16
.word 1587
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
plt_System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo:
_p_17:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1048]
br x16
.word 1589
	.no_dead_strip plt_System_Text_StringBuilder_Append_string
plt_System_Text_StringBuilder_Append_string:
_p_18:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1056]
br x16
.word 1591
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string
plt_System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string:
_p_19:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1064]
br x16
.word 1596
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
plt_System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo:
_p_20:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1072]
br x16
.word 1598
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char
plt_System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char:
_p_21:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1080]
br x16
.word 1600
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool
plt_System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool:
_p_22:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1088]
br x16
.word 1602
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
plt_System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo:
_p_23:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1096]
br x16
.word 1604
	.no_dead_strip plt__jit_icall_mono_helper_ldstr
plt__jit_icall_mono_helper_ldstr:
_p_24:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1104]
br x16
.word 1606
	.no_dead_strip plt__jit_icall_mono_arch_throw_exception
plt__jit_icall_mono_arch_throw_exception:
_p_25:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1112]
br x16
.word 1626
	.no_dead_strip plt_System_Globalization_NumberFormatInfo_get_CurrencyGroupSizes
plt_System_Globalization_NumberFormatInfo_get_CurrencyGroupSizes:
_p_26:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1120]
br x16
.word 1654
	.no_dead_strip plt__jit_icall___emul_lmul_ovf_un
plt__jit_icall___emul_lmul_ovf_un:
_p_27:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1128]
br x16
.word 1659
	.no_dead_strip plt_System_Text_StringBuilder_Append_char__int
plt_System_Text_StringBuilder_Append_char__int:
_p_28:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1136]
br x16
.word 1680
	.no_dead_strip plt_System_Text_StringBuilder_Append_char_int
plt_System_Text_StringBuilder_Append_char_int:
_p_29:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1144]
br x16
.word 1685
	.no_dead_strip plt_System_Globalization_NumberFormatInfo_get_NumberGroupSizes
plt_System_Globalization_NumberFormatInfo_get_NumberGroupSizes:
_p_30:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1152]
br x16
.word 1690
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool
plt_System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool:
_p_31:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1160]
br x16
.word 1695
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int
plt_System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int:
_p_32:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1168]
br x16
.word 1697
	.no_dead_strip plt_System_Globalization_NumberFormatInfo_get_PercentGroupSizes
plt_System_Globalization_NumberFormatInfo_get_PercentGroupSizes:
_p_33:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1176]
br x16
.word 1699
	.no_dead_strip plt_System_Globalization_FormatProvider_Number_FindSection_string_int
plt_System_Globalization_FormatProvider_Number_FindSection_string_int:
_p_34:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1184]
br x16
.word 1704
	.no_dead_strip plt_wrapper_alloc_object_AllocVector_intptr_intptr
plt_wrapper_alloc_object_AllocVector_intptr_intptr:
_p_35:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1192]
br x16
.word 1706
	.no_dead_strip plt_System_Array_Resize_int_int____int
plt_System_Array_Resize_int_int____int:
_p_36:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1200]
br x16
.word 1714
	.no_dead_strip plt_System_Numerics_BigInteger__ctor_double
plt_System_Numerics_BigInteger__ctor_double:
_p_37:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1208]
br x16
.word 1726
	.no_dead_strip plt_double_IsInfinity_double
plt_double_IsInfinity_double:
_p_38:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1216]
br x16
.word 1728
	.no_dead_strip plt_double_IsNaN_double
plt_double_IsNaN_double:
_p_39:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1224]
br x16
.word 1733
	.no_dead_strip plt_System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_
plt_System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_:
_p_40:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1232]
br x16
.word 1738
	.no_dead_strip plt_System_Numerics_BigInteger_op_Implicit_ulong
plt_System_Numerics_BigInteger_op_Implicit_ulong:
_p_41:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1240]
br x16
.word 1740
	.no_dead_strip plt_System_Decimal_Truncate_System_Decimal
plt_System_Decimal_Truncate_System_Decimal:
_p_42:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1248]
br x16
.word 1742
	.no_dead_strip plt_System_Decimal_GetBits_System_Decimal
plt_System_Decimal_GetBits_System_Decimal:
_p_43:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1256]
br x16
.word 1747
	.no_dead_strip plt_System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__
plt_System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__:
_p_44:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1264]
br x16
.word 1752
	.no_dead_strip plt_System_Array_Copy_System_Array_int_System_Array_int_int
plt_System_Array_Copy_System_Array_int_System_Array_int_int:
_p_45:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1272]
br x16
.word 1754
	.no_dead_strip plt_System_Globalization_NumberFormatInfo_GetInstance_System_IFormatProvider
plt_System_Globalization_NumberFormatInfo_GetInstance_System_IFormatProvider:
_p_46:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1280]
br x16
.word 1759
	.no_dead_strip plt_System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider
plt_System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider:
_p_47:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1288]
br x16
.word 1764
	.no_dead_strip plt_System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo
plt_System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo:
_p_48:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1296]
br x16
.word 1766
	.no_dead_strip plt_System_Numerics_BigInteger_Equals_System_Numerics_BigInteger
plt_System_Numerics_BigInteger_Equals_System_Numerics_BigInteger:
_p_49:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1304]
br x16
.word 1768
	.no_dead_strip plt_System_Numerics_BigInteger_GetDiffLength_uint___uint___int
plt_System_Numerics_BigInteger_GetDiffLength_uint___uint___int:
_p_50:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1312]
br x16
.word 1770
	.no_dead_strip plt_System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger
plt_System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger:
_p_51:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1320]
br x16
.word 1772
	.no_dead_strip plt__jit_icall___emul_op_imul_ovf
plt__jit_icall___emul_op_imul_ovf:
_p_52:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1328]
br x16
.word 1774
	.no_dead_strip plt_System_Array_Clone
plt_System_Array_Clone:
_p_53:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1336]
br x16
.word 1795
	.no_dead_strip plt_System_Globalization_NumberFormatInfo_get_CurrentInfo
plt_System_Globalization_NumberFormatInfo_get_CurrentInfo:
_p_54:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1344]
br x16
.word 1800
	.no_dead_strip plt_System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo
plt_System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo:
_p_55:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1352]
br x16
.word 1805
	.no_dead_strip plt_System_Numerics_BigInteger_op_Implicit_long
plt_System_Numerics_BigInteger_op_Implicit_long:
_p_56:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1360]
br x16
.word 1807
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Add_uint___uint
plt_System_Numerics_BigIntegerCalculator_Add_uint___uint:
_p_57:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1368]
br x16
.word 1809
	.no_dead_strip plt_System_Numerics_BigInteger__ctor_uint___bool
plt_System_Numerics_BigInteger__ctor_uint___bool:
_p_58:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1376]
br x16
.word 1811
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Add_uint___uint__
plt_System_Numerics_BigIntegerCalculator_Add_uint___uint__:
_p_59:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1384]
br x16
.word 1813
	.no_dead_strip plt_System_Numerics_BigInteger_Add_uint___int_uint___int
plt_System_Numerics_BigInteger_Add_uint___int_uint___int:
_p_60:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1392]
br x16
.word 1815
	.no_dead_strip plt_System_Numerics_BigInteger_Subtract_uint___int_uint___int
plt_System_Numerics_BigInteger_Subtract_uint___int_uint___int:
_p_61:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1400]
br x16
.word 1817
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Subtract_uint___uint
plt_System_Numerics_BigIntegerCalculator_Subtract_uint___uint:
_p_62:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1408]
br x16
.word 1819
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Compare_uint___uint__
plt_System_Numerics_BigIntegerCalculator_Compare_uint___uint__:
_p_63:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1416]
br x16
.word 1821
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Subtract_uint___uint__
plt_System_Numerics_BigIntegerCalculator_Subtract_uint___uint__:
_p_64:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1424]
br x16
.word 1823
	.no_dead_strip plt_System_Numerics_BigInteger__ctor_int
plt_System_Numerics_BigInteger__ctor_int:
_p_65:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1432]
br x16
.word 1825
	.no_dead_strip plt_System_Numerics_BigInteger__ctor_uint
plt_System_Numerics_BigInteger__ctor_uint:
_p_66:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1440]
br x16
.word 1827
	.no_dead_strip plt_System_Numerics_BigInteger__ctor_long
plt_System_Numerics_BigInteger__ctor_long:
_p_67:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1448]
br x16
.word 1829
	.no_dead_strip plt_System_Numerics_BigInteger__ctor_ulong
plt_System_Numerics_BigInteger__ctor_ulong:
_p_68:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1456]
br x16
.word 1831
	.no_dead_strip plt_System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger
plt_System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger:
_p_69:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1464]
br x16
.word 1833
	.no_dead_strip plt_System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0
plt_System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0:
_p_70:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1472]
br x16
.word 1835
	.no_dead_strip plt_System_Numerics_NumericsHelpers_CbitHighZero_uint
plt_System_Numerics_NumericsHelpers_CbitHighZero_uint:
_p_71:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1480]
br x16
.word 1837
	.no_dead_strip plt_System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong
plt_System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong:
_p_72:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1488]
br x16
.word 1839
	.no_dead_strip plt_System_Decimal_op_Implicit_int
plt_System_Decimal_op_Implicit_int:
_p_73:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1496]
br x16
.word 1841
	.no_dead_strip plt_System_Decimal__ctor_int_int_int_bool_byte
plt_System_Decimal__ctor_int_int_int_bool_byte:
_p_74:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1504]
br x16
.word 1846
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Multiply_uint___uint
plt_System_Numerics_BigIntegerCalculator_Multiply_uint___uint:
_p_75:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1512]
br x16
.word 1851
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Square_uint__
plt_System_Numerics_BigIntegerCalculator_Square_uint__:
_p_76:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1520]
br x16
.word 1853
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Multiply_uint___uint__
plt_System_Numerics_BigIntegerCalculator_Multiply_uint___uint__:
_p_77:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1528]
br x16
.word 1855
	.no_dead_strip plt_System_Numerics_BigInteger_CompareTo_long
plt_System_Numerics_BigInteger_CompareTo_long:
_p_78:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1536]
br x16
.word 1857
	.no_dead_strip plt_System_Numerics_BigInteger_Equals_long
plt_System_Numerics_BigInteger_Equals_long:
_p_79:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1544]
br x16
.word 1859
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int
plt_System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int:
_p_80:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1552]
br x16
.word 1861
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int
plt_System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int:
_p_81:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1560]
br x16
.word 1863
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int
plt_System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int:
_p_82:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1568]
br x16
.word 1865
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int
plt_System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int:
_p_83:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1576]
br x16
.word 1867
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int
plt_System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int:
_p_84:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1584]
br x16
.word 1869
	.no_dead_strip plt_System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int
plt_System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int:
_p_85:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1592]
br x16
.word 1871
	.no_dead_strip plt_SR_Format_string_object
plt_SR_Format_string_object:
_p_86:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1600]
br x16
.word 1873
	.no_dead_strip plt_System_ArgumentException__ctor_string
plt_System_ArgumentException__ctor_string:
_p_87:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1608]
br x16
.word 1875
	.no_dead_strip plt_System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_
plt_System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_:
_p_88:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1616]
br x16
.word 1880
	.no_dead_strip plt_System_Numerics_BigNumber_BigNumberBuffer_Create
plt_System_Numerics_BigNumber_BigNumberBuffer_Create:
_p_89:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1624]
br x16
.word 1882
	.no_dead_strip plt_System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_
plt_System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_:
_p_90:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1632]
br x16
.word 1884
	.no_dead_strip plt_System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
plt_System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_:
_p_91:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1640]
br x16
.word 1886
	.no_dead_strip plt_System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
plt_System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_:
_p_92:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1648]
br x16
.word 1888
	.no_dead_strip plt_System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_
plt_System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_:
_p_93:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1656]
br x16
.word 1890
	.no_dead_strip plt_System_Text_StringBuilder_get_Chars_int
plt_System_Text_StringBuilder_get_Chars_int:
_p_94:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1664]
br x16
.word 1892
	.no_dead_strip plt_System_Numerics_BigInteger__ctor_byte__
plt_System_Numerics_BigInteger__ctor_byte__:
_p_95:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1672]
br x16
.word 1897
	.no_dead_strip plt_System_Numerics_BigInteger_op_Implicit_int
plt_System_Numerics_BigInteger_op_Implicit_int:
_p_96:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1680]
br x16
.word 1899
	.no_dead_strip plt_System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger
plt_System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger:
_p_97:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1688]
br x16
.word 1901
	.no_dead_strip plt_System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger
plt_System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger:
_p_98:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1696]
br x16
.word 1903
	.no_dead_strip plt_System_Text_StringBuilder__ctor
plt_System_Text_StringBuilder__ctor:
_p_99:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1704]
br x16
.word 1905
	.no_dead_strip plt_System_Numerics_BigInteger_ToByteArray
plt_System_Numerics_BigInteger_ToByteArray:
_p_100:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1712]
br x16
.word 1910
	.no_dead_strip plt_byte_ToString_string_System_IFormatProvider
plt_byte_ToString_string_System_IFormatProvider:
_p_101:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1720]
br x16
.word 1912
	.no_dead_strip plt_System_Text_StringBuilder_Insert_int_string_int
plt_System_Text_StringBuilder_Insert_int_string_int:
_p_102:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1728]
br x16
.word 1917
	.no_dead_strip plt_System_Numerics_BigNumber_ParseFormatSpecifier_string_int_
plt_System_Numerics_BigNumber_ParseFormatSpecifier_string_int_:
_p_103:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1736]
br x16
.word 1922
	.no_dead_strip plt_System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo
plt_System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo:
_p_104:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1744]
br x16
.word 1924
	.no_dead_strip plt_int_ToString_System_IFormatProvider
plt_int_ToString_System_IFormatProvider:
_p_105:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1752]
br x16
.word 1926
	.no_dead_strip plt_int_ToString_string_System_IFormatProvider
plt_int_ToString_string_System_IFormatProvider:
_p_106:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1760]
br x16
.word 1931
	.no_dead_strip plt__jit_icall_mono_helper_newobj_mscorlib
plt__jit_icall_mono_helper_newobj_mscorlib:
_p_107:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1768]
br x16
.word 1936
	.no_dead_strip plt_System_FormatException__ctor_string_System_Exception
plt_System_FormatException__ctor_string_System_Exception:
_p_108:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1776]
br x16
.word 1966
	.no_dead_strip plt_System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int
plt_System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int:
_p_109:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1784]
br x16
.word 1971
	.no_dead_strip plt_string__ctor_char___int_int
plt_string__ctor_char___int_int:
_p_110:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1792]
br x16
.word 1973
	.no_dead_strip plt_System_Numerics_NumericsHelpers_CbitHighZero_ulong
plt_System_Numerics_NumericsHelpers_CbitHighZero_ulong:
_p_111:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1800]
br x16
.word 1978
	.no_dead_strip plt__jit_icall_mono_thread_force_interruption_checkpoint_noraise
plt__jit_icall_mono_thread_force_interruption_checkpoint_noraise:
_p_112:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1808]
br x16
.word 1980
	.no_dead_strip plt__rgctx_fetch_0
plt__rgctx_fetch_0:
_p_113:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1816]
br x16
.word 2051
	.no_dead_strip plt__jit_icall_mono_helper_ldstr_mscorlib
plt__jit_icall_mono_helper_ldstr_mscorlib:
_p_114:
adrp x16, mono_aot_System_Numerics_got@PAGE+0
add x16, x16, mono_aot_System_Numerics_got@PAGEOFF
ldr x16, [x16, #1824]
br x16
.word 2061
plt_end:
.section __DATA, __bss
	.align 3
.lcomm mono_aot_System_Numerics_got, 1832
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
	.asciz "67CDE07E-8E91-4B73-A506-DD30BA7C702B"
.section __TEXT, __const
	.align 2
assembly_name:
	.asciz "System.Numerics"
.data
	.align 3
_mono_aot_file_info:

	.long 139,0
	.align 3
	.quad mono_aot_System_Numerics_got
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

	.long 114,1832,115,136,66,387000831,0,28533
	.long 128,8,8,10,0,25,29872,1328
	.long 1136,560,0,816,1096,648,0,464
	.long 200,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0
	.byte 66,163,51,200,87,84,71,18,31,149,209,89,126,155,227,192
	.globl _mono_aot_module_System_Numerics_info
	.align 3
_mono_aot_module_System_Numerics_info:
	.align 3
	.quad _mono_aot_file_info
.section __DWARF, __debug_info,regular,debug
LTDIE_0:

	.byte 17
	.asciz "System_Object"

	.byte 16,7
	.asciz "System_Object"

LDIFF_SYM4=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM4
LTDIE_0_POINTER:

	.byte 13
LDIFF_SYM5=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM5
LTDIE_0_REFERENCE:

	.byte 14
LDIFF_SYM6=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM6
	.byte 2
	.asciz "SR:Format"
	.asciz "SR_Format_string_object"

	.byte 1,36
	.quad SR_Format_string_object
	.quad Lme_0

	.byte 2,118,16,3
	.asciz "resourceFormat"

LDIFF_SYM7=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM7
	.byte 2,141,16,3
	.asciz "p1"

LDIFF_SYM8=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM8
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM9=Lfde0_end - Lfde0_start
	.long LDIFF_SYM9
Lfde0_start:

	.long 0
	.align 3
	.quad SR_Format_string_object

LDIFF_SYM10=Lme_0 - SR_Format_string_object
	.long LDIFF_SYM10
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde0_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_2:

	.byte 5
	.asciz "System_ValueType"

	.byte 16,16
LDIFF_SYM11=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM11
	.byte 2,35,0,0,7
	.asciz "System_ValueType"

LDIFF_SYM12=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM12
LTDIE_2_POINTER:

	.byte 13
LDIFF_SYM13=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM13
LTDIE_2_REFERENCE:

	.byte 14
LDIFF_SYM14=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM14
LTDIE_1:

	.byte 5
	.asciz "System_Int32"

	.byte 20,16
LDIFF_SYM15=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM15
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM16=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM16
	.byte 2,35,16,0,7
	.asciz "System_Int32"

LDIFF_SYM17=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM17
LTDIE_1_POINTER:

	.byte 13
LDIFF_SYM18=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM18
LTDIE_1_REFERENCE:

	.byte 14
LDIFF_SYM19=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM19
LTDIE_3:

	.byte 5
	.asciz "System_Boolean"

	.byte 17,16
LDIFF_SYM20=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM20
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM21=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM21
	.byte 2,35,16,0,7
	.asciz "System_Boolean"

LDIFF_SYM22=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM22
LTDIE_3_POINTER:

	.byte 13
LDIFF_SYM23=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM23
LTDIE_3_REFERENCE:

	.byte 14
LDIFF_SYM24=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM24
LTDIE_4:

	.byte 5
	.asciz "System_Globalization_NumberFormatInfo"

	.byte 216,1,16
LDIFF_SYM25=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM25
	.byte 2,35,0,6
	.asciz "numberGroupSizes"

LDIFF_SYM26=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM26
	.byte 2,35,16,6
	.asciz "currencyGroupSizes"

LDIFF_SYM27=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM27
	.byte 2,35,24,6
	.asciz "percentGroupSizes"

LDIFF_SYM28=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM28
	.byte 2,35,32,6
	.asciz "positiveSign"

LDIFF_SYM29=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM29
	.byte 2,35,40,6
	.asciz "negativeSign"

LDIFF_SYM30=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM30
	.byte 2,35,48,6
	.asciz "numberDecimalSeparator"

LDIFF_SYM31=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM31
	.byte 2,35,56,6
	.asciz "numberGroupSeparator"

LDIFF_SYM32=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM32
	.byte 2,35,64,6
	.asciz "currencyGroupSeparator"

LDIFF_SYM33=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM33
	.byte 2,35,72,6
	.asciz "currencyDecimalSeparator"

LDIFF_SYM34=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM34
	.byte 2,35,80,6
	.asciz "currencySymbol"

LDIFF_SYM35=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM35
	.byte 2,35,88,6
	.asciz "ansiCurrencySymbol"

LDIFF_SYM36=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM36
	.byte 2,35,96,6
	.asciz "nanSymbol"

LDIFF_SYM37=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM37
	.byte 2,35,104,6
	.asciz "positiveInfinitySymbol"

LDIFF_SYM38=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM38
	.byte 2,35,112,6
	.asciz "negativeInfinitySymbol"

LDIFF_SYM39=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM39
	.byte 2,35,120,6
	.asciz "percentDecimalSeparator"

LDIFF_SYM40=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM40
	.byte 3,35,128,1,6
	.asciz "percentGroupSeparator"

LDIFF_SYM41=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM41
	.byte 3,35,136,1,6
	.asciz "percentSymbol"

LDIFF_SYM42=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM42
	.byte 3,35,144,1,6
	.asciz "perMilleSymbol"

LDIFF_SYM43=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM43
	.byte 3,35,152,1,6
	.asciz "nativeDigits"

LDIFF_SYM44=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM44
	.byte 3,35,160,1,6
	.asciz "m_dataItem"

LDIFF_SYM45=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM45
	.byte 3,35,168,1,6
	.asciz "numberDecimalDigits"

LDIFF_SYM46=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM46
	.byte 3,35,172,1,6
	.asciz "currencyDecimalDigits"

LDIFF_SYM47=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM47
	.byte 3,35,176,1,6
	.asciz "currencyPositivePattern"

LDIFF_SYM48=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM48
	.byte 3,35,180,1,6
	.asciz "currencyNegativePattern"

LDIFF_SYM49=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM49
	.byte 3,35,184,1,6
	.asciz "numberNegativePattern"

LDIFF_SYM50=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM50
	.byte 3,35,188,1,6
	.asciz "percentPositivePattern"

LDIFF_SYM51=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM51
	.byte 3,35,192,1,6
	.asciz "percentNegativePattern"

LDIFF_SYM52=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM52
	.byte 3,35,196,1,6
	.asciz "percentDecimalDigits"

LDIFF_SYM53=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM53
	.byte 3,35,200,1,6
	.asciz "digitSubstitution"

LDIFF_SYM54=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM54
	.byte 3,35,204,1,6
	.asciz "isReadOnly"

LDIFF_SYM55=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM55
	.byte 3,35,208,1,6
	.asciz "m_useUserOverride"

LDIFF_SYM56=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM56
	.byte 3,35,209,1,6
	.asciz "m_isInvariant"

LDIFF_SYM57=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM57
	.byte 3,35,210,1,6
	.asciz "validForParseAsNumber"

LDIFF_SYM58=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM58
	.byte 3,35,211,1,6
	.asciz "validForParseAsCurrency"

LDIFF_SYM59=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM59
	.byte 3,35,212,1,0,7
	.asciz "System_Globalization_NumberFormatInfo"

LDIFF_SYM60=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM60
LTDIE_4_POINTER:

	.byte 13
LDIFF_SYM61=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM61
LTDIE_4_REFERENCE:

	.byte 14
LDIFF_SYM62=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM62
LTDIE_5:

	.byte 5
	.asciz "System_Char"

	.byte 18,16
LDIFF_SYM63=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM63
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM64=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM64
	.byte 2,35,16,0,7
	.asciz "System_Char"

LDIFF_SYM65=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM65
LTDIE_5_POINTER:

	.byte 13
LDIFF_SYM66=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM66
LTDIE_5_REFERENCE:

	.byte 14
LDIFF_SYM67=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM67
	.byte 2
	.asciz "System.Globalization.FormatProvider:FormatBigInteger"
	.asciz "System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int"

	.byte 2,18
	.quad System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int
	.quad Lme_1

	.byte 2,118,16,3
	.asciz "precision"

LDIFF_SYM68=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM68
	.byte 2,141,32,3
	.asciz "scale"

LDIFF_SYM69=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM69
	.byte 2,141,40,3
	.asciz "sign"

LDIFF_SYM70=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM70
	.byte 2,141,48,3
	.asciz "format"

LDIFF_SYM71=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM71
	.byte 2,141,56,3
	.asciz "numberFormatInfo"

LDIFF_SYM72=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM72
	.byte 3,141,192,0,3
	.asciz "digits"

LDIFF_SYM73=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM73
	.byte 3,141,200,0,3
	.asciz "startIndex"

LDIFF_SYM74=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM74
	.byte 3,141,208,0,11
	.asciz "maxDigits"

LDIFF_SYM75=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM75
	.byte 3,141,208,1,11
	.asciz "fmt"

LDIFF_SYM76=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM76
	.byte 1,99,11
	.asciz "overrideDigits"

LDIFF_SYM77=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM77
	.byte 1,106,11
	.asciz "V_3"

LDIFF_SYM78=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM78
	.byte 3,141,216,1,11
	.asciz "numberBuffer"

LDIFF_SYM79=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM79
	.byte 3,141,184,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM80=Lfde1_end - Lfde1_start
	.long LDIFF_SYM80
Lfde1_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int

LDIFF_SYM81=Lme_1 - System_Globalization_FormatProvider_FormatBigInteger_int_int_bool_string_System_Globalization_NumberFormatInfo_char___int
	.long LDIFF_SYM81
	.long 0
	.byte 12,31,0,68,14,224,1,157,28,158,27,68,13,29,68,147,26,68,154,25
	.align 3
Lfde1_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_6:

	.byte 8
	.asciz "System_Globalization_NumberStyles"

	.byte 4
LDIFF_SYM82=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM82
	.byte 9
	.asciz "None"

	.byte 0,9
	.asciz "AllowLeadingWhite"

	.byte 1,9
	.asciz "AllowTrailingWhite"

	.byte 2,9
	.asciz "AllowLeadingSign"

	.byte 4,9
	.asciz "AllowTrailingSign"

	.byte 8,9
	.asciz "AllowParentheses"

	.byte 16,9
	.asciz "AllowDecimalPoint"

	.byte 32,9
	.asciz "AllowThousands"

	.byte 192,0,9
	.asciz "AllowExponent"

	.byte 128,1,9
	.asciz "AllowCurrencySymbol"

	.byte 128,2,9
	.asciz "AllowHexSpecifier"

	.byte 128,4,9
	.asciz "Integer"

	.byte 7,9
	.asciz "HexNumber"

	.byte 131,4,9
	.asciz "Number"

	.byte 239,0,9
	.asciz "Float"

	.byte 167,1,9
	.asciz "Currency"

	.byte 255,2,9
	.asciz "Any"

	.byte 255,3,0,7
	.asciz "System_Globalization_NumberStyles"

LDIFF_SYM83=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM83
LTDIE_6_POINTER:

	.byte 13
LDIFF_SYM84=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM84
LTDIE_6_REFERENCE:

	.byte 14
LDIFF_SYM85=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM85
LTDIE_7:

	.byte 5
	.asciz "System_Text_StringBuilder"

	.byte 48,16
LDIFF_SYM86=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM86
	.byte 2,35,0,6
	.asciz "m_ChunkChars"

LDIFF_SYM87=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM87
	.byte 2,35,16,6
	.asciz "m_ChunkPrevious"

LDIFF_SYM88=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM88
	.byte 2,35,24,6
	.asciz "m_ChunkLength"

LDIFF_SYM89=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM89
	.byte 2,35,32,6
	.asciz "m_ChunkOffset"

LDIFF_SYM90=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM90
	.byte 2,35,36,6
	.asciz "m_MaxCapacity"

LDIFF_SYM91=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM91
	.byte 2,35,40,0,7
	.asciz "System_Text_StringBuilder"

LDIFF_SYM92=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM92
LTDIE_7_POINTER:

	.byte 13
LDIFF_SYM93=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM93
LTDIE_7_REFERENCE:

	.byte 14
LDIFF_SYM94=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM94
	.byte 2
	.asciz "System.Globalization.FormatProvider:TryStringToBigInteger"
	.asciz "System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_"

	.byte 2,45
	.quad System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_
	.quad Lme_2

	.byte 2,118,16,3
	.asciz "s"

LDIFF_SYM95=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM95
	.byte 2,141,16,3
	.asciz "styles"

LDIFF_SYM96=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM96
	.byte 2,141,24,3
	.asciz "numberFormatInfo"

LDIFF_SYM97=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM97
	.byte 2,141,32,3
	.asciz "receiver"

LDIFF_SYM98=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM98
	.byte 2,141,40,3
	.asciz "precision"

LDIFF_SYM99=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM99
	.byte 2,141,48,3
	.asciz "scale"

LDIFF_SYM100=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM100
	.byte 2,141,56,3
	.asciz "sign"

LDIFF_SYM101=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM101
	.byte 3,141,192,0,11
	.asciz "numberBuffer"

LDIFF_SYM102=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM102
	.byte 3,141,144,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM103=Lfde2_end - Lfde2_start
	.long LDIFF_SYM103
Lfde2_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_

LDIFF_SYM104=Lme_2 - System_Globalization_FormatProvider_TryStringToBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Text_StringBuilder_int__int__bool_
	.long LDIFF_SYM104
	.long 0
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29
	.align 3
Lfde2_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:IsWhite"
	.asciz "System_Globalization_FormatProvider_Number_IsWhite_char"

	.byte 3,168,2
	.quad System_Globalization_FormatProvider_Number_IsWhite_char
	.quad Lme_3

	.byte 2,118,16,3
	.asciz "ch"

LDIFF_SYM105=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM105
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM106=Lfde3_end - Lfde3_start
	.long LDIFF_SYM106
Lfde3_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_IsWhite_char

LDIFF_SYM107=Lme_3 - System_Globalization_FormatProvider_Number_IsWhite_char
	.long LDIFF_SYM107
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde3_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:MatchChars"
	.asciz "System_Globalization_FormatProvider_Number_MatchChars_char__string"

	.byte 3,172,2
	.quad System_Globalization_FormatProvider_Number_MatchChars_char__string
	.quad Lme_4

	.byte 2,118,16,3
	.asciz "p"

LDIFF_SYM108=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM108
	.byte 2,141,24,3
	.asciz "str"

LDIFF_SYM109=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM109
	.byte 2,141,32,11
	.asciz "stringPointer"

LDIFF_SYM110=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM110
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM111=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM111
	.byte 0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM112=Lfde4_end - Lfde4_start
	.long LDIFF_SYM112
Lfde4_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_MatchChars_char__string

LDIFF_SYM113=Lme_4 - System_Globalization_FormatProvider_Number_MatchChars_char__string
	.long LDIFF_SYM113
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,152,4
	.align 3
Lfde4_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:MatchChars"
	.asciz "System_Globalization_FormatProvider_Number_MatchChars_char__char_"

	.byte 3,183,2
	.quad System_Globalization_FormatProvider_Number_MatchChars_char__char_
	.quad Lme_5

	.byte 2,118,16,3
	.asciz "p"

LDIFF_SYM114=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM114
	.byte 1,105,3
	.asciz "str"

LDIFF_SYM115=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM115
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM116=Lfde5_end - Lfde5_start
	.long LDIFF_SYM116
Lfde5_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_MatchChars_char__char_

LDIFF_SYM117=Lme_5 - System_Globalization_FormatProvider_Number_MatchChars_char__char_
	.long LDIFF_SYM117
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29,68,153,2,154,1
	.align 3
Lfde5_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_8:

	.byte 5
	.asciz "_NumberBuffer"

	.byte 40,16
LDIFF_SYM118=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM118
	.byte 2,35,0,6
	.asciz "precision"

LDIFF_SYM119=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM119
	.byte 2,35,0,6
	.asciz "scale"

LDIFF_SYM120=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM120
	.byte 2,35,4,6
	.asciz "sign"

LDIFF_SYM121=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM121
	.byte 2,35,8,6
	.asciz "overrideDigits"

LDIFF_SYM122=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM122
	.byte 2,35,16,0,7
	.asciz "_NumberBuffer"

LDIFF_SYM123=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM123
LTDIE_8_POINTER:

	.byte 13
LDIFF_SYM124=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM124
LTDIE_8_REFERENCE:

	.byte 14
LDIFF_SYM125=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM125
	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:ParseNumber"
	.asciz "System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool"

	.byte 3,209,2
	.quad System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
	.quad Lme_6

	.byte 2,118,16,3
	.asciz "str"

LDIFF_SYM126=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM126
	.byte 3,141,192,0,3
	.asciz "options"

LDIFF_SYM127=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM127
	.byte 1,102,3
	.asciz "number"

LDIFF_SYM128=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM128
	.byte 3,141,200,0,3
	.asciz "sb"

LDIFF_SYM129=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM129
	.byte 3,141,208,0,3
	.asciz "numfmt"

LDIFF_SYM130=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM130
	.byte 3,141,216,0,3
	.asciz "parseDecimal"

LDIFF_SYM131=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM131
	.byte 3,141,224,0,11
	.asciz "decSep"

LDIFF_SYM132=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM132
	.byte 3,141,232,0,11
	.asciz "groupSep"

LDIFF_SYM133=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM133
	.byte 1,106,11
	.asciz "currSymbol"

LDIFF_SYM134=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM134
	.byte 1,100,11
	.asciz "parsingCurrency"

LDIFF_SYM135=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM135
	.byte 1,99,11
	.asciz "state"

LDIFF_SYM136=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM136
	.byte 3,141,240,0,11
	.asciz "bigNumber"

LDIFF_SYM137=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM137
	.byte 1,105,11
	.asciz "maxParseDigits"

LDIFF_SYM138=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM138
	.byte 3,141,128,1,11
	.asciz "p"

LDIFF_SYM139=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM139
	.byte 3,141,136,1,11
	.asciz "ch"

LDIFF_SYM140=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM140
	.byte 1,104,11
	.asciz "next"

LDIFF_SYM141=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM141
	.byte 3,141,168,1,11
	.asciz "dig"

LDIFF_SYM142=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM142
	.byte 3,141,144,1,11
	.asciz "digCount"

LDIFF_SYM143=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM143
	.byte 3,141,152,1,11
	.asciz "digEnd"

LDIFF_SYM144=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM144
	.byte 3,141,160,1,11
	.asciz "negExp"

LDIFF_SYM145=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM145
	.byte 1,106,11
	.asciz "V_14"

LDIFF_SYM146=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM146
	.byte 0,11
	.asciz "temp"

LDIFF_SYM147=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM147
	.byte 1,105,11
	.asciz "exp"

LDIFF_SYM148=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM148
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM149=Lfde6_end - Lfde6_start
	.long LDIFF_SYM149
Lfde6_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool

LDIFF_SYM150=Lme_6 - System_Globalization_FormatProvider_Number_ParseNumber_char___System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
	.long LDIFF_SYM150
	.long 0
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,147,20,148,19,68,150,18,68,152,17,153,16,68,154,15
	.align 3
Lfde6_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:TrailingZeros"
	.asciz "System_Globalization_FormatProvider_Number_TrailingZeros_string_int"

	.byte 3,164,4
	.quad System_Globalization_FormatProvider_Number_TrailingZeros_string_int
	.quad Lme_7

	.byte 2,118,16,3
	.asciz "s"

LDIFF_SYM151=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM151
	.byte 1,105,3
	.asciz "index"

LDIFF_SYM152=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM152
	.byte 2,141,32,11
	.asciz "i"

LDIFF_SYM153=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM153
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM154=Lfde7_end - Lfde7_start
	.long LDIFF_SYM154
Lfde7_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_TrailingZeros_string_int

LDIFF_SYM155=Lme_7 - System_Globalization_FormatProvider_Number_TrailingZeros_string_int
	.long LDIFF_SYM155
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,152,4,153,3
	.align 3
Lfde7_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:TryStringToNumber"
	.asciz "System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool"

	.byte 3,176,4
	.quad System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
	.quad Lme_8

	.byte 2,118,16,3
	.asciz "str"

LDIFF_SYM156=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM156
	.byte 1,101,3
	.asciz "options"

LDIFF_SYM157=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM157
	.byte 2,141,32,3
	.asciz "number"

LDIFF_SYM158=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM158
	.byte 2,141,40,3
	.asciz "sb"

LDIFF_SYM159=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM159
	.byte 2,141,48,3
	.asciz "numfmt"

LDIFF_SYM160=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM160
	.byte 2,141,56,3
	.asciz "parseDecimal"

LDIFF_SYM161=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM161
	.byte 3,141,192,0,11
	.asciz "stringPointer"

LDIFF_SYM162=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM162
	.byte 1,99,11
	.asciz "V_1"

LDIFF_SYM163=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM163
	.byte 3,141,208,0,11
	.asciz "p"

LDIFF_SYM164=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM164
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM165=Lfde8_end - Lfde8_start
	.long LDIFF_SYM165
Lfde8_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool

LDIFF_SYM166=Lme_8 - System_Globalization_FormatProvider_Number_TryStringToNumber_string_System_Globalization_NumberStyles_System_Globalization_FormatProvider_Number_NumberBuffer__System_Text_StringBuilder_System_Globalization_NumberFormatInfo_bool
	.long LDIFF_SYM166
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29,68,147,10,68,149,9
	.align 3
Lfde8_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_9:

	.byte 5
	.asciz "System_UInt32"

	.byte 20,16
LDIFF_SYM167=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM167
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM168=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM168
	.byte 2,35,16,0,7
	.asciz "System_UInt32"

LDIFF_SYM169=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM169
LTDIE_9_POINTER:

	.byte 13
LDIFF_SYM170=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM170
LTDIE_9_REFERENCE:

	.byte 14
LDIFF_SYM171=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM171
	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:Int32ToDecChars"
	.asciz "System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int"

	.byte 3,0
	.quad System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int
	.quad Lme_9

	.byte 2,118,16,3
	.asciz "buffer"

LDIFF_SYM172=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM172
	.byte 1,103,3
	.asciz "index"

LDIFF_SYM173=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM173
	.byte 1,104,3
	.asciz "value"

LDIFF_SYM174=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM174
	.byte 1,105,3
	.asciz "digits"

LDIFF_SYM175=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM175
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM176=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM176
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM177=Lfde9_end - Lfde9_start
	.long LDIFF_SYM177
Lfde9_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int

LDIFF_SYM178=Lme_9 - System_Globalization_FormatProvider_Number_Int32ToDecChars_char__int__uint_int
	.long LDIFF_SYM178
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,150,6,151,5,68,152,4,153,3,68,154,2
	.align 3
Lfde9_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:ParseFormatSpecifier"
	.asciz "System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_"

	.byte 3,252,4
	.quad System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_
	.quad Lme_a

	.byte 2,118,16,3
	.asciz "format"

LDIFF_SYM179=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM179
	.byte 1,105,3
	.asciz "digits"

LDIFF_SYM180=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM180
	.byte 2,141,48,11
	.asciz "pFormat"

LDIFF_SYM181=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM181
	.byte 1,103,11
	.asciz "V_1"

LDIFF_SYM182=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM182
	.byte 1,104,11
	.asciz "i"

LDIFF_SYM183=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM183
	.byte 1,105,11
	.asciz "ch"

LDIFF_SYM184=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM184
	.byte 1,104,11
	.asciz "n"

LDIFF_SYM185=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM185
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM186=Lfde10_end - Lfde10_start
	.long LDIFF_SYM186
Lfde10_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_

LDIFF_SYM187=Lme_a - System_Globalization_FormatProvider_Number_ParseFormatSpecifier_string_int_
	.long LDIFF_SYM187
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,150,6,151,5,68,152,4,153,3
	.align 3
Lfde10_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:NumberToString"
	.asciz "System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool"

	.byte 3,165,5
	.quad System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool
	.quad Lme_b

	.byte 2,118,16,3
	.asciz "number"

LDIFF_SYM188=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM188
	.byte 1,80,3
	.asciz "format"

LDIFF_SYM189=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM189
	.byte 1,103,3
	.asciz "nMaxDigits"

LDIFF_SYM190=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM190
	.byte 1,104,3
	.asciz "info"

LDIFF_SYM191=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM191
	.byte 1,105,3
	.asciz "isDecimal"

LDIFF_SYM192=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM192
	.byte 1,106,11
	.asciz "nMinDigits"

LDIFF_SYM193=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM193
	.byte 1,101,11
	.asciz "sb"

LDIFF_SYM194=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM194
	.byte 1,102,11
	.asciz "enableRounding"

LDIFF_SYM195=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM195
	.byte 1,100,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM196=Lfde11_end - Lfde11_start
	.long LDIFF_SYM196
Lfde11_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool

LDIFF_SYM197=Lme_b - System_Globalization_FormatProvider_Number_NumberToString_System_Globalization_FormatProvider_Number_NumberBuffer_char_int_System_Globalization_NumberFormatInfo_bool
	.long LDIFF_SYM197
	.long 0
	.byte 12,31,0,84,14,192,4,157,72,158,71,68,13,29,68,148,70,149,69,68,150,68,151,67,68,152,66,153,65,68,154,64
	.align 3
Lfde11_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:FormatCurrency"
	.asciz "System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo"

	.byte 3,175,6
	.quad System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
	.quad Lme_c

	.byte 2,118,16,3
	.asciz "sb"

LDIFF_SYM198=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM198
	.byte 1,103,3
	.asciz "number"

LDIFF_SYM199=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM199
	.byte 1,80,3
	.asciz "nMinDigits"

LDIFF_SYM200=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM200
	.byte 1,104,3
	.asciz "nMaxDigits"

LDIFF_SYM201=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM201
	.byte 1,105,3
	.asciz "info"

LDIFF_SYM202=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM202
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM203=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM203
	.byte 1,101,11
	.asciz "V_1"

LDIFF_SYM204=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM204
	.byte 1,102,11
	.asciz "ch"

LDIFF_SYM205=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM205
	.byte 1,100,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM206=Lfde12_end - Lfde12_start
	.long LDIFF_SYM206
Lfde12_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo

LDIFF_SYM207=Lme_c - System_Globalization_FormatProvider_Number_FormatCurrency_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
	.long LDIFF_SYM207
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,148,14,149,13,68,150,12,151,11,68,152,10,153,9,68,154,8
	.align 3
Lfde12_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:wcslen"
	.asciz "System_Globalization_FormatProvider_Number_wcslen_char_"

	.byte 3,201,6
	.quad System_Globalization_FormatProvider_Number_wcslen_char_
	.quad Lme_d

	.byte 2,118,16,3
	.asciz "s"

LDIFF_SYM208=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM208
	.byte 1,106,11
	.asciz "result"

LDIFF_SYM209=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM209
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM210=Lfde13_end - Lfde13_start
	.long LDIFF_SYM210
Lfde13_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_wcslen_char_

LDIFF_SYM211=Lme_d - System_Globalization_FormatProvider_Number_wcslen_char_
	.long LDIFF_SYM211
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29,68,153,2,154,1
	.align 3
Lfde13_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:FormatFixed"
	.asciz "System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string"

	.byte 3,209,6
	.quad System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string
	.quad Lme_e

	.byte 2,118,16,3
	.asciz "sb"

LDIFF_SYM212=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM212
	.byte 3,141,208,0,3
	.asciz "number"

LDIFF_SYM213=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM213
	.byte 1,80,3
	.asciz "nMinDigits"

LDIFF_SYM214=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM214
	.byte 0,3
	.asciz "nMaxDigits"

LDIFF_SYM215=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM215
	.byte 3,141,232,0,3
	.asciz "info"

LDIFF_SYM216=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM216
	.byte 0,3
	.asciz "groupDigits"

LDIFF_SYM217=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM217
	.byte 1,104,3
	.asciz "sDecimal"

LDIFF_SYM218=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM218
	.byte 1,105,3
	.asciz "sGroup"

LDIFF_SYM219=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM219
	.byte 1,106,11
	.asciz "digPos"

LDIFF_SYM220=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM220
	.byte 1,101,11
	.asciz "dig"

LDIFF_SYM221=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM221
	.byte 3,141,144,1,11
	.asciz "digLength"

LDIFF_SYM222=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM222
	.byte 1,99,11
	.asciz "groupSizeIndex"

LDIFF_SYM223=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM223
	.byte 3,141,152,1,11
	.asciz "groupSizeCount"

LDIFF_SYM224=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM224
	.byte 1,103,11
	.asciz "groupSizeLen"

LDIFF_SYM225=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM225
	.byte 3,141,160,1,11
	.asciz "bufferSize"

LDIFF_SYM226=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM226
	.byte 3,141,168,1,11
	.asciz "groupSeparatorLen"

LDIFF_SYM227=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM227
	.byte 3,141,176,1,11
	.asciz "groupSize"

LDIFF_SYM228=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM228
	.byte 3,141,184,1,11
	.asciz "tmpBuffer"

LDIFF_SYM229=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM229
	.byte 1,103,11
	.asciz "digitCount"

LDIFF_SYM230=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM230
	.byte 1,102,11
	.asciz "digStart"

LDIFF_SYM231=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM231
	.byte 3,141,192,1,11
	.asciz "p"

LDIFF_SYM232=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM232
	.byte 3,141,200,1,11
	.asciz "i"

LDIFF_SYM233=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM233
	.byte 1,100,11
	.asciz "j"

LDIFF_SYM234=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM234
	.byte 1,102,11
	.asciz "digits"

LDIFF_SYM235=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM235
	.byte 1,106,11
	.asciz "zeroes"

LDIFF_SYM236=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM236
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM237=Lfde14_end - Lfde14_start
	.long LDIFF_SYM237
Lfde14_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string

LDIFF_SYM238=Lme_e - System_Globalization_FormatProvider_Number_FormatFixed_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_int___string_string
	.long LDIFF_SYM238
	.long 0
	.byte 12,31,0,68,14,224,1,157,28,158,27,68,13,29,68,147,26,148,25,68,149,24,150,23,68,151,22,152,21,68,153,20
	.byte 154,19
	.align 3
Lfde14_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:FormatNumber"
	.asciz "System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo"

	.byte 3,185,7
	.quad System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
	.quad Lme_f

	.byte 2,118,16,3
	.asciz "sb"

LDIFF_SYM239=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM239
	.byte 1,103,3
	.asciz "number"

LDIFF_SYM240=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM240
	.byte 1,80,3
	.asciz "nMinDigits"

LDIFF_SYM241=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM241
	.byte 1,104,3
	.asciz "nMaxDigits"

LDIFF_SYM242=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM242
	.byte 1,105,3
	.asciz "info"

LDIFF_SYM243=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM243
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM244=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM244
	.byte 1,101,11
	.asciz "V_1"

LDIFF_SYM245=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM245
	.byte 1,102,11
	.asciz "ch"

LDIFF_SYM246=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM246
	.byte 1,100,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM247=Lfde15_end - Lfde15_start
	.long LDIFF_SYM247
Lfde15_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo

LDIFF_SYM248=Lme_f - System_Globalization_FormatProvider_Number_FormatNumber_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
	.long LDIFF_SYM248
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,148,14,149,13,68,150,12,151,11,68,152,10,153,9,68,154,8
	.align 3
Lfde15_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:FormatScientific"
	.asciz "System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char"

	.byte 3,208,7
	.quad System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char
	.quad Lme_10

	.byte 2,118,16,3
	.asciz "sb"

LDIFF_SYM249=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM249
	.byte 1,103,3
	.asciz "number"

LDIFF_SYM250=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM250
	.byte 1,80,3
	.asciz "nMinDigits"

LDIFF_SYM251=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM251
	.byte 0,3
	.asciz "nMaxDigits"

LDIFF_SYM252=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM252
	.byte 1,104,3
	.asciz "info"

LDIFF_SYM253=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM253
	.byte 1,105,3
	.asciz "expChar"

LDIFF_SYM254=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM254
	.byte 3,141,208,0,11
	.asciz "dig"

LDIFF_SYM255=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM255
	.byte 1,102,11
	.asciz "e"

LDIFF_SYM256=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM256
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM257=Lfde16_end - Lfde16_start
	.long LDIFF_SYM257
Lfde16_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char

LDIFF_SYM258=Lme_10 - System_Globalization_FormatProvider_Number_FormatScientific_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char
	.long LDIFF_SYM258
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29,68,148,12,149,11,68,150,10,151,9,68,152,8,153,7
	.align 3
Lfde16_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:FormatExponent"
	.asciz "System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool"

	.byte 3,224,7
	.quad System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool
	.quad Lme_11

	.byte 2,118,16,3
	.asciz "sb"

LDIFF_SYM259=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM259
	.byte 1,101,3
	.asciz "info"

LDIFF_SYM260=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM260
	.byte 1,102,3
	.asciz "value"

LDIFF_SYM261=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM261
	.byte 1,103,3
	.asciz "expChar"

LDIFF_SYM262=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM262
	.byte 1,104,3
	.asciz "minDigits"

LDIFF_SYM263=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM263
	.byte 1,105,3
	.asciz "positiveSign"

LDIFF_SYM264=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM264
	.byte 1,106,11
	.asciz "digits"

LDIFF_SYM265=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM265
	.byte 1,106,11
	.asciz "index"

LDIFF_SYM266=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM266
	.byte 3,141,192,0,11
	.asciz "i"

LDIFF_SYM267=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM267
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM268=Lfde17_end - Lfde17_start
	.long LDIFF_SYM268
Lfde17_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool

LDIFF_SYM269=Lme_11 - System_Globalization_FormatProvider_Number_FormatExponent_System_Text_StringBuilder_System_Globalization_NumberFormatInfo_int_char_int_bool
	.long LDIFF_SYM269
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,149,8,150,7,68,151,6,152,5,68,153,4,154,3
	.align 3
Lfde17_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:FormatGeneral"
	.asciz "System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool"

	.byte 3,247,7
	.quad System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool
	.quad Lme_12

	.byte 2,118,16,3
	.asciz "sb"

LDIFF_SYM270=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM270
	.byte 1,102,3
	.asciz "number"

LDIFF_SYM271=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM271
	.byte 1,80,3
	.asciz "nMinDigits"

LDIFF_SYM272=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM272
	.byte 0,3
	.asciz "nMaxDigits"

LDIFF_SYM273=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM273
	.byte 1,103,3
	.asciz "info"

LDIFF_SYM274=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM274
	.byte 1,104,3
	.asciz "expChar"

LDIFF_SYM275=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM275
	.byte 3,141,216,0,3
	.asciz "bSuppressScientific"

LDIFF_SYM276=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM276
	.byte 1,106,11
	.asciz "digPos"

LDIFF_SYM277=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM277
	.byte 1,101,11
	.asciz "scientific"

LDIFF_SYM278=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM278
	.byte 1,100,11
	.asciz "dig"

LDIFF_SYM279=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM279
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM280=Lfde18_end - Lfde18_start
	.long LDIFF_SYM280
Lfde18_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool

LDIFF_SYM281=Lme_12 - System_Globalization_FormatProvider_Number_FormatGeneral_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo_char_bool
	.long LDIFF_SYM281
	.long 0
	.byte 12,31,0,68,14,144,1,157,18,158,17,68,13,29,68,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,154,10
	.align 3
Lfde18_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:FormatPercent"
	.asciz "System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo"

	.byte 3,166,8
	.quad System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
	.quad Lme_13

	.byte 2,118,16,3
	.asciz "sb"

LDIFF_SYM282=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM282
	.byte 1,103,3
	.asciz "number"

LDIFF_SYM283=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM283
	.byte 1,80,3
	.asciz "nMinDigits"

LDIFF_SYM284=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM284
	.byte 1,104,3
	.asciz "nMaxDigits"

LDIFF_SYM285=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM285
	.byte 1,105,3
	.asciz "info"

LDIFF_SYM286=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM286
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM287=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM287
	.byte 1,101,11
	.asciz "V_1"

LDIFF_SYM288=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM288
	.byte 1,102,11
	.asciz "ch"

LDIFF_SYM289=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM289
	.byte 1,100,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM290=Lfde19_end - Lfde19_start
	.long LDIFF_SYM290
Lfde19_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo

LDIFF_SYM291=Lme_13 - System_Globalization_FormatProvider_Number_FormatPercent_System_Text_StringBuilder_System_Globalization_FormatProvider_Number_NumberBuffer_int_int_System_Globalization_NumberFormatInfo
	.long LDIFF_SYM291
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,148,14,149,13,68,150,12,151,11,68,152,10,153,9,68,154,8
	.align 3
Lfde19_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:RoundNumber"
	.asciz "System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int"

	.byte 3,192,8
	.quad System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int
	.quad Lme_14

	.byte 2,118,16,3
	.asciz "number"

LDIFF_SYM292=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM292
	.byte 1,105,3
	.asciz "pos"

LDIFF_SYM293=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM293
	.byte 1,106,11
	.asciz "dig"

LDIFF_SYM294=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM294
	.byte 1,104,11
	.asciz "i"

LDIFF_SYM295=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM295
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM296=Lfde20_end - Lfde20_start
	.long LDIFF_SYM296
Lfde20_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int

LDIFF_SYM297=Lme_14 - System_Globalization_FormatProvider_Number_RoundNumber_System_Globalization_FormatProvider_Number_NumberBuffer__int
	.long LDIFF_SYM297
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,151,4,152,3,68,153,2,154,1
	.align 3
Lfde20_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:FindSection"
	.asciz "System_Globalization_FormatProvider_Number_FindSection_string_int"

	.byte 3,232,8
	.quad System_Globalization_FormatProvider_Number_FindSection_string_int
	.quad Lme_15

	.byte 2,118,16,3
	.asciz "format"

LDIFF_SYM298=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM298
	.byte 1,105,3
	.asciz "section"

LDIFF_SYM299=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM299
	.byte 1,106,11
	.asciz "src"

LDIFF_SYM300=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM300
	.byte 1,105,11
	.asciz "ch"

LDIFF_SYM301=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM301
	.byte 1,104,11
	.asciz "pFormat"

LDIFF_SYM302=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM302
	.byte 1,103,11
	.asciz "V_3"

LDIFF_SYM303=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM303
	.byte 1,104,11
	.asciz "V_4"

LDIFF_SYM304=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM304
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM305=Lfde21_end - Lfde21_start
	.long LDIFF_SYM305
Lfde21_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_FindSection_string_int

LDIFF_SYM306=Lme_15 - System_Globalization_FormatProvider_Number_FindSection_string_int
	.long LDIFF_SYM306
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,150,6,151,5,68,152,4,153,3,68,154,2
	.align 3
Lfde21_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:NumberToStringFormat"
	.asciz "System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo"

	.byte 3,145,9
	.quad System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo
	.quad Lme_16

	.byte 2,118,16,3
	.asciz "number"

LDIFF_SYM307=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM307
	.byte 1,80,3
	.asciz "format"

LDIFF_SYM308=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM308
	.byte 3,141,216,0,3
	.asciz "info"

LDIFF_SYM309=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM309
	.byte 3,141,224,0,11
	.asciz "digitCount"

LDIFF_SYM310=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM310
	.byte 1,100,11
	.asciz "decimalPos"

LDIFF_SYM311=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM311
	.byte 1,99,11
	.asciz "firstDigit"

LDIFF_SYM312=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM312
	.byte 3,141,168,2,11
	.asciz "lastDigit"

LDIFF_SYM313=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM313
	.byte 3,141,176,2,11
	.asciz "digPos"

LDIFF_SYM314=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM314
	.byte 1,104,11
	.asciz "scientific"

LDIFF_SYM315=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM315
	.byte 3,141,184,2,11
	.asciz "thousandPos"

LDIFF_SYM316=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM316
	.byte 1,101,11
	.asciz "thousandCount"

LDIFF_SYM317=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM317
	.byte 1,104,11
	.asciz "thousandSeps"

LDIFF_SYM318=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM318
	.byte 3,141,192,2,11
	.asciz "scaleAdjust"

LDIFF_SYM319=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM319
	.byte 3,141,200,2,11
	.asciz "adjust"

LDIFF_SYM320=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM320
	.byte 3,141,224,2,11
	.asciz "section"

LDIFF_SYM321=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM321
	.byte 3,141,160,2,11
	.asciz "src"

LDIFF_SYM322=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM322
	.byte 3,141,208,2,11
	.asciz "dig"

LDIFF_SYM323=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM323
	.byte 3,141,152,2,11
	.asciz "ch"

LDIFF_SYM324=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM324
	.byte 1,106,11
	.asciz "thousandsSepPos"

LDIFF_SYM325=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM325
	.byte 3,141,144,2,11
	.asciz "thousandsSepCtr"

LDIFF_SYM326=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM326
	.byte 1,103,11
	.asciz "sb"

LDIFF_SYM327=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM327
	.byte 3,141,232,2,11
	.asciz "decimalWritten"

LDIFF_SYM328=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM328
	.byte 3,141,240,2,11
	.asciz "pFormat"

LDIFF_SYM329=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM329
	.byte 3,141,216,2,11
	.asciz "V_20"

LDIFF_SYM330=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM330
	.byte 1,106,11
	.asciz "pos"

LDIFF_SYM331=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM331
	.byte 1,106,11
	.asciz "groupDigits"

LDIFF_SYM332=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM332
	.byte 1,106,11
	.asciz "groupSizeIndex"

LDIFF_SYM333=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM333
	.byte 1,102,11
	.asciz "groupTotalSizeCount"

LDIFF_SYM334=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM334
	.byte 3,141,136,3,11
	.asciz "groupSizeLen"

LDIFF_SYM335=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM335
	.byte 3,141,144,3,11
	.asciz "groupSize"

LDIFF_SYM336=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM336
	.byte 3,141,152,3,11
	.asciz "totalDigits"

LDIFF_SYM337=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM337
	.byte 3,141,160,3,11
	.asciz "numDigits"

LDIFF_SYM338=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM338
	.byte 1,101,11
	.asciz "pFormat"

LDIFF_SYM339=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM339
	.byte 3,141,248,2,11
	.asciz "cur"

LDIFF_SYM340=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM340
	.byte 3,141,128,3,11
	.asciz "positiveSign"

LDIFF_SYM341=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM341
	.byte 1,102,11
	.asciz "i"

LDIFF_SYM342=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM342
	.byte 1,101,11
	.asciz "exp"

LDIFF_SYM343=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM343
	.byte 3,141,136,3,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM344=Lfde22_end - Lfde22_start
	.long LDIFF_SYM344
Lfde22_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo

LDIFF_SYM345=Lme_16 - System_Globalization_FormatProvider_Number_NumberToStringFormat_System_Globalization_FormatProvider_Number_NumberBuffer_string_System_Globalization_NumberFormatInfo
	.long LDIFF_SYM345
	.long 0
	.byte 12,31,0,68,14,192,3,157,56,158,55,68,13,29,68,147,54,148,53,68,149,52,150,51,68,151,50,152,49,68,153,48
	.byte 154,47
	.align 3
Lfde22_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number:.cctor"
	.asciz "System_Globalization_FormatProvider_Number__cctor"

	.byte 3,207,4
	.quad System_Globalization_FormatProvider_Number__cctor
	.quad Lme_17

	.byte 2,118,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM346=Lfde23_end - Lfde23_start
	.long LDIFF_SYM346
Lfde23_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number__cctor

LDIFF_SYM347=Lme_17 - System_Globalization_FormatProvider_Number__cctor
	.long LDIFF_SYM347
	.long 0
	.byte 12,31,0,68,14,224,2,157,44,158,43,68,13,29
	.align 3
Lfde23_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Globalization.FormatProvider/Number/NumberBuffer:get_digits"
	.asciz "System_Globalization_FormatProvider_Number_NumberBuffer_get_digits"

	.byte 4,26
	.quad System_Globalization_FormatProvider_Number_NumberBuffer_get_digits
	.quad Lme_18

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM348=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM348
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM349=Lfde24_end - Lfde24_start
	.long LDIFF_SYM349
Lfde24_start:

	.long 0
	.align 3
	.quad System_Globalization_FormatProvider_Number_NumberBuffer_get_digits

LDIFF_SYM350=Lme_18 - System_Globalization_FormatProvider_Number_NumberBuffer_get_digits
	.long LDIFF_SYM350
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde24_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_10:

	.byte 5
	.asciz "System_Numerics_BigInteger"

	.byte 32,16
LDIFF_SYM351=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM351
	.byte 2,35,0,6
	.asciz "_sign"

LDIFF_SYM352=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM352
	.byte 2,35,0,6
	.asciz "_bits"

LDIFF_SYM353=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM353
	.byte 2,35,8,0,7
	.asciz "System_Numerics_BigInteger"

LDIFF_SYM354=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM354
LTDIE_10_POINTER:

	.byte 13
LDIFF_SYM355=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM355
LTDIE_10_REFERENCE:

	.byte 14
LDIFF_SYM356=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM356
	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_int"

	.byte 5,36
	.quad System_Numerics_BigInteger__ctor_int
	.quad Lme_19

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM357=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM357
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM358=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM358
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM359=Lfde25_end - Lfde25_start
	.long LDIFF_SYM359
Lfde25_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_int

LDIFF_SYM360=Lme_19 - System_Numerics_BigInteger__ctor_int
	.long LDIFF_SYM360
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde25_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_uint"

	.byte 5,49
	.quad System_Numerics_BigInteger__ctor_uint
	.quad Lme_1a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM361=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM361
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM362=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM362
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM363=Lfde26_end - Lfde26_start
	.long LDIFF_SYM363
Lfde26_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_uint

LDIFF_SYM364=Lme_1a - System_Numerics_BigInteger__ctor_uint
	.long LDIFF_SYM364
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29,68,153,2
	.align 3
Lfde26_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_11:

	.byte 5
	.asciz "System_Int64"

	.byte 24,16
LDIFF_SYM365=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM365
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM366=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM366
	.byte 2,35,16,0,7
	.asciz "System_Int64"

LDIFF_SYM367=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM367
LTDIE_11_POINTER:

	.byte 13
LDIFF_SYM368=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM368
LTDIE_11_REFERENCE:

	.byte 14
LDIFF_SYM369=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM369
LTDIE_12:

	.byte 5
	.asciz "System_UInt64"

	.byte 24,16
LDIFF_SYM370=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM370
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM371=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM371
	.byte 2,35,16,0,7
	.asciz "System_UInt64"

LDIFF_SYM372=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM372
LTDIE_12_POINTER:

	.byte 13
LDIFF_SYM373=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM373
LTDIE_12_REFERENCE:

	.byte 14
LDIFF_SYM374=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM374
	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_long"

	.byte 5,65
	.quad System_Numerics_BigInteger__ctor_long
	.quad Lme_1b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM375=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM375
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM376=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM376
	.byte 1,106,11
	.asciz "x"

LDIFF_SYM377=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM377
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM378=Lfde27_end - Lfde27_start
	.long LDIFF_SYM378
Lfde27_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_long

LDIFF_SYM379=Lme_1b - System_Numerics_BigInteger__ctor_long
	.long LDIFF_SYM379
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,152,6,153,5,68,154,4
	.align 3
Lfde27_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_ulong"

	.byte 5,107
	.quad System_Numerics_BigInteger__ctor_ulong
	.quad Lme_1c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM380=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM380
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM381=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM381
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM382=Lfde28_end - Lfde28_start
	.long LDIFF_SYM382
Lfde28_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_ulong

LDIFF_SYM383=Lme_1c - System_Numerics_BigInteger__ctor_ulong
	.long LDIFF_SYM383
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29,68,153,2,154,1
	.align 3
Lfde28_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_13:

	.byte 5
	.asciz "System_Single"

	.byte 20,16
LDIFF_SYM384=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM384
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM385=LDIE_R4 - Ldebug_info_start
	.long LDIFF_SYM385
	.byte 2,35,16,0,7
	.asciz "System_Single"

LDIFF_SYM386=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM386
LTDIE_13_POINTER:

	.byte 13
LDIFF_SYM387=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM387
LTDIE_13_REFERENCE:

	.byte 14
LDIFF_SYM388=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM388
	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_single"

	.byte 5,129,1
	.quad System_Numerics_BigInteger__ctor_single
	.quad Lme_1d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM389=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM389
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM390=LDIE_R4 - Ldebug_info_start
	.long LDIFF_SYM390
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM391=Lfde29_end - Lfde29_start
	.long LDIFF_SYM391
Lfde29_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_single

LDIFF_SYM392=Lme_1d - System_Numerics_BigInteger__ctor_single
	.long LDIFF_SYM392
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde29_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_14:

	.byte 5
	.asciz "System_Double"

	.byte 24,16
LDIFF_SYM393=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM393
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM394=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM394
	.byte 2,35,16,0,7
	.asciz "System_Double"

LDIFF_SYM395=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM395
LTDIE_14_POINTER:

	.byte 13
LDIFF_SYM396=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM396
LTDIE_14_REFERENCE:

	.byte 14
LDIFF_SYM397=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM397
	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_double"

	.byte 5,135,1
	.quad System_Numerics_BigInteger__ctor_double
	.quad Lme_1e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM398=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM398
	.byte 1,106,3
	.asciz "value"

LDIFF_SYM399=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM399
	.byte 2,141,40,11
	.asciz "sign"

LDIFF_SYM400=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM400
	.byte 3,141,240,0,11
	.asciz "exp"

LDIFF_SYM401=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM401
	.byte 3,141,248,0,11
	.asciz "man"

LDIFF_SYM402=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM402
	.byte 3,141,128,1,11
	.asciz "fFinite"

LDIFF_SYM403=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM403
	.byte 3,141,136,1,11
	.asciz "cu"

LDIFF_SYM404=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM404
	.byte 1,105,11
	.asciz "cbit"

LDIFF_SYM405=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM405
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM406=Lfde30_end - Lfde30_start
	.long LDIFF_SYM406
Lfde30_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_double

LDIFF_SYM407=Lme_1e - System_Numerics_BigInteger__ctor_double
	.long LDIFF_SYM407
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,68,152,22,153,21,68,154,20
	.align 3
Lfde30_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_System_Decimal"

	.byte 5,204,1
	.quad System_Numerics_BigInteger__ctor_System_Decimal
	.quad Lme_1f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM408=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM408
	.byte 1,106,3
	.asciz "value"

LDIFF_SYM409=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM409
	.byte 2,141,48,11
	.asciz "bits"

LDIFF_SYM410=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM410
	.byte 1,105,11
	.asciz "size"

LDIFF_SYM411=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM411
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM412=Lfde31_end - Lfde31_start
	.long LDIFF_SYM412
Lfde31_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_System_Decimal

LDIFF_SYM413=Lme_1f - System_Numerics_BigInteger__ctor_System_Decimal
	.long LDIFF_SYM413
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,151,14,152,13,68,153,12,154,11
	.align 3
Lfde31_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_byte__"

	.byte 5,248,1
	.quad System_Numerics_BigInteger__ctor_byte__
	.quad Lme_20

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM414=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM414
	.byte 3,141,208,0,3
	.asciz "value"

LDIFF_SYM415=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM415
	.byte 3,141,216,0,11
	.asciz "byteCount"

LDIFF_SYM416=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM416
	.byte 1,104,11
	.asciz "isNegative"

LDIFF_SYM417=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM417
	.byte 1,102,11
	.asciz "i"

LDIFF_SYM418=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM418
	.byte 1,106,11
	.asciz "unalignedBytes"

LDIFF_SYM419=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM419
	.byte 1,101,11
	.asciz "dwordCount"

LDIFF_SYM420=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM420
	.byte 1,99,11
	.asciz "isZero"

LDIFF_SYM421=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM421
	.byte 3,141,160,1,11
	.asciz "val"

LDIFF_SYM422=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM422
	.byte 3,141,168,1,11
	.asciz "curDword"

LDIFF_SYM423=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM423
	.byte 3,141,176,1,11
	.asciz "curByte"

LDIFF_SYM424=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM424
	.byte 1,106,11
	.asciz "byteInDword"

LDIFF_SYM425=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM425
	.byte 1,105,11
	.asciz "len"

LDIFF_SYM426=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM426
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM427=Lfde32_end - Lfde32_start
	.long LDIFF_SYM427
Lfde32_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_byte__

LDIFF_SYM428=Lme_20 - System_Numerics_BigInteger__ctor_byte__
	.long LDIFF_SYM428
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,68,147,22,148,21,68,149,20,150,19,68,151,18,152,17,68,153,16
	.byte 154,15
	.align 3
Lfde32_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_int_uint__"

	.byte 5,252,2
	.quad System_Numerics_BigInteger__ctor_int_uint__
	.quad Lme_21

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM429=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM429
	.byte 2,141,16,3
	.asciz "n"

LDIFF_SYM430=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM430
	.byte 2,141,24,3
	.asciz "rgu"

LDIFF_SYM431=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM431
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM432=Lfde33_end - Lfde33_start
	.long LDIFF_SYM432
Lfde33_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_int_uint__

LDIFF_SYM433=Lme_21 - System_Numerics_BigInteger__ctor_int_uint__
	.long LDIFF_SYM433
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde33_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:.ctor"
	.asciz "System_Numerics_BigInteger__ctor_uint___bool"

	.byte 5,137,3
	.quad System_Numerics_BigInteger__ctor_uint___bool
	.quad Lme_22

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM434=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM434
	.byte 1,104,3
	.asciz "value"

LDIFF_SYM435=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM435
	.byte 1,105,3
	.asciz "negative"

LDIFF_SYM436=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM436
	.byte 1,106,11
	.asciz "len"

LDIFF_SYM437=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM437
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM438=Lfde34_end - Lfde34_start
	.long LDIFF_SYM438
Lfde34_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__ctor_uint___bool

LDIFF_SYM439=Lme_22 - System_Numerics_BigInteger__ctor_uint___bool
	.long LDIFF_SYM439
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29,68,150,10,151,9,68,152,8,153,7,68,154,6
	.align 3
Lfde34_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:get_Zero"
	.asciz "System_Numerics_BigInteger_get_Zero"

	.byte 5,146,4
	.quad System_Numerics_BigInteger_get_Zero
	.quad Lme_23

	.byte 2,118,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM440=Lfde35_end - Lfde35_start
	.long LDIFF_SYM440
Lfde35_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_get_Zero

LDIFF_SYM441=Lme_23 - System_Numerics_BigInteger_get_Zero
	.long LDIFF_SYM441
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde35_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:get_IsPowerOfTwo"
	.asciz "System_Numerics_BigInteger_get_IsPowerOfTwo"

	.byte 5,158,4
	.quad System_Numerics_BigInteger_get_IsPowerOfTwo
	.quad Lme_24

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM442=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM442
	.byte 1,106,11
	.asciz "iu"

LDIFF_SYM443=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM443
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM444=Lfde36_end - Lfde36_start
	.long LDIFF_SYM444
Lfde36_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_get_IsPowerOfTwo

LDIFF_SYM445=Lme_24 - System_Numerics_BigInteger_get_IsPowerOfTwo
	.long LDIFF_SYM445
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29,68,153,2,154,1
	.align 3
Lfde36_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:get_IsZero"
	.asciz "System_Numerics_BigInteger_get_IsZero"

	.byte 5,175,4
	.quad System_Numerics_BigInteger_get_IsZero
	.quad Lme_25

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM446=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM446
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM447=Lfde37_end - Lfde37_start
	.long LDIFF_SYM447
Lfde37_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_get_IsZero

LDIFF_SYM448=Lme_25 - System_Numerics_BigInteger_get_IsZero
	.long LDIFF_SYM448
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde37_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:get_IsOne"
	.asciz "System_Numerics_BigInteger_get_IsOne"

	.byte 5,177,4
	.quad System_Numerics_BigInteger_get_IsOne
	.quad Lme_26

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM449=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM449
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM450=Lfde38_end - Lfde38_start
	.long LDIFF_SYM450
Lfde38_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_get_IsOne

LDIFF_SYM451=Lme_26 - System_Numerics_BigInteger_get_IsOne
	.long LDIFF_SYM451
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde38_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:get_IsEven"
	.asciz "System_Numerics_BigInteger_get_IsEven"

	.byte 5,179,4
	.quad System_Numerics_BigInteger_get_IsEven
	.quad Lme_27

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM452=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM452
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM453=Lfde39_end - Lfde39_start
	.long LDIFF_SYM453
Lfde39_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_get_IsEven

LDIFF_SYM454=Lme_27 - System_Numerics_BigInteger_get_IsEven
	.long LDIFF_SYM454
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde39_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:get_Sign"
	.asciz "System_Numerics_BigInteger_get_Sign"

	.byte 5,183,4
	.quad System_Numerics_BigInteger_get_Sign
	.quad Lme_28

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM455=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM455
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM456=Lfde40_end - Lfde40_start
	.long LDIFF_SYM456
Lfde40_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_get_Sign

LDIFF_SYM457=Lme_28 - System_Numerics_BigInteger_get_Sign
	.long LDIFF_SYM457
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde40_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_15:

	.byte 17
	.asciz "System_IFormatProvider"

	.byte 16,7
	.asciz "System_IFormatProvider"

LDIFF_SYM458=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM458
LTDIE_15_POINTER:

	.byte 13
LDIFF_SYM459=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM459
LTDIE_15_REFERENCE:

	.byte 14
LDIFF_SYM460=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM460
	.byte 2
	.asciz "System.Numerics.BigInteger:Parse"
	.asciz "System_Numerics_BigInteger_Parse_string_System_IFormatProvider"

	.byte 5,198,4
	.quad System_Numerics_BigInteger_Parse_string_System_IFormatProvider
	.quad Lme_29

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM461=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM461
	.byte 2,141,32,3
	.asciz "provider"

LDIFF_SYM462=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM462
	.byte 2,141,40,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM463=Lfde41_end - Lfde41_start
	.long LDIFF_SYM463
Lfde41_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_Parse_string_System_IFormatProvider

LDIFF_SYM464=Lme_29 - System_Numerics_BigInteger_Parse_string_System_IFormatProvider
	.long LDIFF_SYM464
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde41_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:Parse"
	.asciz "System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider"

	.byte 5,203,4
	.quad System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider
	.quad Lme_2a

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM465=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM465
	.byte 2,141,32,3
	.asciz "style"

LDIFF_SYM466=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM466
	.byte 2,141,40,3
	.asciz "provider"

LDIFF_SYM467=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM467
	.byte 2,141,48,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM468=Lfde42_end - Lfde42_start
	.long LDIFF_SYM468
Lfde42_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider

LDIFF_SYM469=Lme_2a - System_Numerics_BigInteger_Parse_string_System_Globalization_NumberStyles_System_IFormatProvider
	.long LDIFF_SYM469
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde42_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:GetHashCode"
	.asciz "System_Numerics_BigInteger_GetHashCode"

	.byte 5,229,6
	.quad System_Numerics_BigInteger_GetHashCode
	.quad Lme_2b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM470=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM470
	.byte 1,106,11
	.asciz "hash"

LDIFF_SYM471=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM471
	.byte 1,105,11
	.asciz "iv"

LDIFF_SYM472=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM472
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM473=Lfde43_end - Lfde43_start
	.long LDIFF_SYM473
Lfde43_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_GetHashCode

LDIFF_SYM474=Lme_2b - System_Numerics_BigInteger_GetHashCode
	.long LDIFF_SYM474
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,152,4,153,3,68,154,2
	.align 3
Lfde43_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:Equals"
	.asciz "System_Numerics_BigInteger_Equals_object"

	.byte 5,241,6
	.quad System_Numerics_BigInteger_Equals_object
	.quad Lme_2c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM475=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM475
	.byte 2,141,32,3
	.asciz "obj"

LDIFF_SYM476=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM476
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM477=Lfde44_end - Lfde44_start
	.long LDIFF_SYM477
Lfde44_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_Equals_object

LDIFF_SYM478=Lme_2c - System_Numerics_BigInteger_Equals_object
	.long LDIFF_SYM478
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,152,6,68,154,5
	.align 3
Lfde44_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:Equals"
	.asciz "System_Numerics_BigInteger_Equals_long"

	.byte 5,250,6
	.quad System_Numerics_BigInteger_Equals_long
	.quad Lme_2d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM479=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM479
	.byte 1,105,3
	.asciz "other"

LDIFF_SYM480=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM480
	.byte 1,106,11
	.asciz "cu"

LDIFF_SYM481=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM481
	.byte 2,141,40,11
	.asciz "uu"

LDIFF_SYM482=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM482
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM483=Lfde45_end - Lfde45_start
	.long LDIFF_SYM483
Lfde45_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_Equals_long

LDIFF_SYM484=Lme_2d - System_Numerics_BigInteger_Equals_long
	.long LDIFF_SYM484
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,151,4,68,153,3,154,2
	.align 3
Lfde45_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:Equals"
	.asciz "System_Numerics_BigInteger_Equals_System_Numerics_BigInteger"

	.byte 5,159,7
	.quad System_Numerics_BigInteger_Equals_System_Numerics_BigInteger
	.quad Lme_2e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM485=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM485
	.byte 1,106,3
	.asciz "other"

LDIFF_SYM486=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM486
	.byte 2,141,32,11
	.asciz "cu"

LDIFF_SYM487=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM487
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM488=Lfde46_end - Lfde46_start
	.long LDIFF_SYM488
Lfde46_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_Equals_System_Numerics_BigInteger

LDIFF_SYM489=Lme_2e - System_Numerics_BigInteger_Equals_System_Numerics_BigInteger
	.long LDIFF_SYM489
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,153,14,154,13
	.align 3
Lfde46_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:CompareTo"
	.asciz "System_Numerics_BigInteger_CompareTo_long"

	.byte 5,178,7
	.quad System_Numerics_BigInteger_CompareTo_long
	.quad Lme_2f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM490=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM490
	.byte 1,105,3
	.asciz "other"

LDIFF_SYM491=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM491
	.byte 1,106,11
	.asciz "cu"

LDIFF_SYM492=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM492
	.byte 1,104,11
	.asciz "uu"

LDIFF_SYM493=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM493
	.byte 1,106,11
	.asciz "uuTmp"

LDIFF_SYM494=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM494
	.byte 2,141,48,11
	.asciz "V_3"

LDIFF_SYM495=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM495
	.byte 2,141,56,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM496=Lfde47_end - Lfde47_start
	.long LDIFF_SYM496
Lfde47_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_CompareTo_long

LDIFF_SYM497=Lme_2f - System_Numerics_BigInteger_CompareTo_long
	.long LDIFF_SYM497
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,151,6,152,5,68,153,4,154,3
	.align 3
Lfde47_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:CompareTo"
	.asciz "System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger"

	.byte 5,209,7
	.quad System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger
	.quad Lme_30

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM498=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM498
	.byte 1,106,3
	.asciz "other"

LDIFF_SYM499=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM499
	.byte 2,141,32,11
	.asciz "cuThis"

LDIFF_SYM500=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM500
	.byte 1,105,11
	.asciz "cuOther"

LDIFF_SYM501=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM501
	.byte 3,141,192,1,11
	.asciz "cuDiff"

LDIFF_SYM502=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM502
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM503=Lfde48_end - Lfde48_start
	.long LDIFF_SYM503
Lfde48_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger

LDIFF_SYM504=Lme_30 - System_Numerics_BigInteger_CompareTo_System_Numerics_BigInteger
	.long LDIFF_SYM504
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,68,153,24,154,23
	.align 3
Lfde48_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:CompareTo"
	.asciz "System_Numerics_BigInteger_CompareTo_object"

	.byte 5,236,7
	.quad System_Numerics_BigInteger_CompareTo_object
	.quad Lme_31

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM505=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM505
	.byte 2,141,32,3
	.asciz "obj"

LDIFF_SYM506=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM506
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM507=Lfde49_end - Lfde49_start
	.long LDIFF_SYM507
Lfde49_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_CompareTo_object

LDIFF_SYM508=Lme_31 - System_Numerics_BigInteger_CompareTo_object
	.long LDIFF_SYM508
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,152,8,68,154,7
	.align 3
Lfde49_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_16:

	.byte 5
	.asciz "System_Byte"

	.byte 17,16
LDIFF_SYM509=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM509
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM510=LDIE_U1 - Ldebug_info_start
	.long LDIFF_SYM510
	.byte 2,35,16,0,7
	.asciz "System_Byte"

LDIFF_SYM511=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM511
LTDIE_16_POINTER:

	.byte 13
LDIFF_SYM512=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM512
LTDIE_16_REFERENCE:

	.byte 14
LDIFF_SYM513=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM513
	.byte 2
	.asciz "System.Numerics.BigInteger:ToByteArray"
	.asciz "System_Numerics_BigInteger_ToByteArray"

	.byte 5,251,7
	.quad System_Numerics_BigInteger_ToByteArray
	.quad Lme_32

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM514=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM514
	.byte 1,106,11
	.asciz "sign"

LDIFF_SYM515=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM515
	.byte 3,141,208,0,11
	.asciz "highByte"

LDIFF_SYM516=LDIE_U1 - Ldebug_info_start
	.long LDIFF_SYM516
	.byte 1,106,11
	.asciz "nonZeroDwordIndex"

LDIFF_SYM517=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM517
	.byte 3,141,216,0,11
	.asciz "highDword"

LDIFF_SYM518=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM518
	.byte 1,102,11
	.asciz "bits"

LDIFF_SYM519=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM519
	.byte 1,103,11
	.asciz "msb"

LDIFF_SYM520=LDIE_U1 - Ldebug_info_start
	.long LDIFF_SYM520
	.byte 1,101,11
	.asciz "msbIndex"

LDIFF_SYM521=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM521
	.byte 1,100,11
	.asciz "needExtraByte"

LDIFF_SYM522=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM522
	.byte 1,101,11
	.asciz "bytes"

LDIFF_SYM523=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM523
	.byte 3,141,240,0,11
	.asciz "curByte"

LDIFF_SYM524=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM524
	.byte 1,99,11
	.asciz "i"

LDIFF_SYM525=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM525
	.byte 1,105,11
	.asciz "dword"

LDIFF_SYM526=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM526
	.byte 3,141,248,0,11
	.asciz "j"

LDIFF_SYM527=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM527
	.byte 1,104,11
	.asciz "j"

LDIFF_SYM528=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM528
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM529=Lfde50_end - Lfde50_start
	.long LDIFF_SYM529
Lfde50_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_ToByteArray

LDIFF_SYM530=Lme_32 - System_Numerics_BigInteger_ToByteArray
	.long LDIFF_SYM530
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,147,14,148,13,68,149,12,150,11,68,151,10,152,9,68,153,8
	.byte 154,7
	.align 3
Lfde50_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:ToUInt32Array"
	.asciz "System_Numerics_BigInteger_ToUInt32Array"

	.byte 5,244,8
	.quad System_Numerics_BigInteger_ToUInt32Array
	.quad Lme_33

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM531=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM531
	.byte 1,106,11
	.asciz "dwords"

LDIFF_SYM532=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM532
	.byte 1,105,11
	.asciz "highDWord"

LDIFF_SYM533=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM533
	.byte 1,106,11
	.asciz "msb"

LDIFF_SYM534=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM534
	.byte 1,104,11
	.asciz "needExtraByte"

LDIFF_SYM535=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM535
	.byte 1,103,11
	.asciz "trimmed"

LDIFF_SYM536=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM536
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM537=Lfde51_end - Lfde51_start
	.long LDIFF_SYM537
Lfde51_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_ToUInt32Array

LDIFF_SYM538=Lme_33 - System_Numerics_BigInteger_ToUInt32Array
	.long LDIFF_SYM538
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,149,6,150,5,68,151,4,152,3,68,153,2,154,1
	.align 3
Lfde51_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:ToString"
	.asciz "System_Numerics_BigInteger_ToString"

	.byte 5,157,9
	.quad System_Numerics_BigInteger_ToString
	.quad Lme_34

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM539=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM539
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM540=Lfde52_end - Lfde52_start
	.long LDIFF_SYM540
Lfde52_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_ToString

LDIFF_SYM541=Lme_34 - System_Numerics_BigInteger_ToString
	.long LDIFF_SYM541
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde52_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:ToString"
	.asciz "System_Numerics_BigInteger_ToString_System_IFormatProvider"

	.byte 5,162,9
	.quad System_Numerics_BigInteger_ToString_System_IFormatProvider
	.quad Lme_35

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM542=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM542
	.byte 2,141,16,3
	.asciz "provider"

LDIFF_SYM543=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM543
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM544=Lfde53_end - Lfde53_start
	.long LDIFF_SYM544
Lfde53_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_ToString_System_IFormatProvider

LDIFF_SYM545=Lme_35 - System_Numerics_BigInteger_ToString_System_IFormatProvider
	.long LDIFF_SYM545
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde53_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:ToString"
	.asciz "System_Numerics_BigInteger_ToString_string_System_IFormatProvider"

	.byte 5,172,9
	.quad System_Numerics_BigInteger_ToString_string_System_IFormatProvider
	.quad Lme_36

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM546=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM546
	.byte 2,141,16,3
	.asciz "format"

LDIFF_SYM547=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM547
	.byte 2,141,24,3
	.asciz "provider"

LDIFF_SYM548=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM548
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM549=Lfde54_end - Lfde54_start
	.long LDIFF_SYM549
Lfde54_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_ToString_string_System_IFormatProvider

LDIFF_SYM550=Lme_36 - System_Numerics_BigInteger_ToString_string_System_IFormatProvider
	.long LDIFF_SYM550
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde54_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:Add"
	.asciz "System_Numerics_BigInteger_Add_uint___int_uint___int"

	.byte 5,177,9
	.quad System_Numerics_BigInteger_Add_uint___int_uint___int
	.quad Lme_37

	.byte 2,118,16,3
	.asciz "leftBits"

LDIFF_SYM551=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM551
	.byte 1,103,3
	.asciz "leftSign"

LDIFF_SYM552=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM552
	.byte 1,104,3
	.asciz "rightBits"

LDIFF_SYM553=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM553
	.byte 1,105,3
	.asciz "rightSign"

LDIFF_SYM554=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM554
	.byte 3,141,200,0,11
	.asciz "trivialLeft"

LDIFF_SYM555=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM555
	.byte 1,102,11
	.asciz "trivialRight"

LDIFF_SYM556=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM556
	.byte 1,101,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM557=Lfde55_end - Lfde55_start
	.long LDIFF_SYM557
Lfde55_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_Add_uint___int_uint___int

LDIFF_SYM558=Lme_37 - System_Numerics_BigInteger_Add_uint___int_uint___int
	.long LDIFF_SYM558
	.long 0
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,149,20,150,19,68,151,18,152,17,68,153,16
	.align 3
Lfde55_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Subtraction"
	.asciz "System_Numerics_BigInteger_op_Subtraction_System_Numerics_BigInteger_System_Numerics_BigInteger"

	.byte 5,214,9
	.quad System_Numerics_BigInteger_op_Subtraction_System_Numerics_BigInteger_System_Numerics_BigInteger
	.quad Lme_38

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM559=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM559
	.byte 2,141,32,3
	.asciz "right"

LDIFF_SYM560=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM560
	.byte 2,141,48,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM561=Lfde56_end - Lfde56_start
	.long LDIFF_SYM561
Lfde56_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Subtraction_System_Numerics_BigInteger_System_Numerics_BigInteger

LDIFF_SYM562=Lme_38 - System_Numerics_BigInteger_op_Subtraction_System_Numerics_BigInteger_System_Numerics_BigInteger
	.long LDIFF_SYM562
	.long 0
	.byte 12,31,0,68,14,240,1,157,30,158,29,68,13,29
	.align 3
Lfde56_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:Subtract"
	.asciz "System_Numerics_BigInteger_Subtract_uint___int_uint___int"

	.byte 5,221,9
	.quad System_Numerics_BigInteger_Subtract_uint___int_uint___int
	.quad Lme_39

	.byte 2,118,16,3
	.asciz "leftBits"

LDIFF_SYM563=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM563
	.byte 1,103,3
	.asciz "leftSign"

LDIFF_SYM564=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM564
	.byte 1,104,3
	.asciz "rightBits"

LDIFF_SYM565=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM565
	.byte 1,105,3
	.asciz "rightSign"

LDIFF_SYM566=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM566
	.byte 3,141,200,0,11
	.asciz "trivialLeft"

LDIFF_SYM567=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM567
	.byte 1,102,11
	.asciz "trivialRight"

LDIFF_SYM568=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM568
	.byte 1,101,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM569=Lfde57_end - Lfde57_start
	.long LDIFF_SYM569
Lfde57_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_Subtract_uint___int_uint___int

LDIFF_SYM570=Lme_39 - System_Numerics_BigInteger_Subtract_uint___int_uint___int
	.long LDIFF_SYM570
	.long 0
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,149,20,150,19,68,151,18,152,17,68,153,16
	.align 3
Lfde57_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Implicit"
	.asciz "System_Numerics_BigInteger_op_Implicit_byte"

	.byte 5,255,9
	.quad System_Numerics_BigInteger_op_Implicit_byte
	.quad Lme_3a

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM571=LDIE_U1 - Ldebug_info_start
	.long LDIFF_SYM571
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM572=Lfde58_end - Lfde58_start
	.long LDIFF_SYM572
Lfde58_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Implicit_byte

LDIFF_SYM573=Lme_3a - System_Numerics_BigInteger_op_Implicit_byte
	.long LDIFF_SYM573
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde58_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_17:

	.byte 5
	.asciz "System_SByte"

	.byte 17,16
LDIFF_SYM574=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM574
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM575=LDIE_I1 - Ldebug_info_start
	.long LDIFF_SYM575
	.byte 2,35,16,0,7
	.asciz "System_SByte"

LDIFF_SYM576=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM576
LTDIE_17_POINTER:

	.byte 13
LDIFF_SYM577=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM577
LTDIE_17_REFERENCE:

	.byte 14
LDIFF_SYM578=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM578
	.byte 2
	.asciz "System.Numerics.BigInteger:op_Implicit"
	.asciz "System_Numerics_BigInteger_op_Implicit_sbyte"

	.byte 5,133,10
	.quad System_Numerics_BigInteger_op_Implicit_sbyte
	.quad Lme_3b

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM579=LDIE_I1 - Ldebug_info_start
	.long LDIFF_SYM579
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM580=Lfde59_end - Lfde59_start
	.long LDIFF_SYM580
Lfde59_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Implicit_sbyte

LDIFF_SYM581=Lme_3b - System_Numerics_BigInteger_op_Implicit_sbyte
	.long LDIFF_SYM581
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde59_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_18:

	.byte 5
	.asciz "System_Int16"

	.byte 18,16
LDIFF_SYM582=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM582
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM583=LDIE_I2 - Ldebug_info_start
	.long LDIFF_SYM583
	.byte 2,35,16,0,7
	.asciz "System_Int16"

LDIFF_SYM584=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM584
LTDIE_18_POINTER:

	.byte 13
LDIFF_SYM585=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM585
LTDIE_18_REFERENCE:

	.byte 14
LDIFF_SYM586=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM586
	.byte 2
	.asciz "System.Numerics.BigInteger:op_Implicit"
	.asciz "System_Numerics_BigInteger_op_Implicit_int16"

	.byte 5,138,10
	.quad System_Numerics_BigInteger_op_Implicit_int16
	.quad Lme_3c

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM587=LDIE_I2 - Ldebug_info_start
	.long LDIFF_SYM587
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM588=Lfde60_end - Lfde60_start
	.long LDIFF_SYM588
Lfde60_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Implicit_int16

LDIFF_SYM589=Lme_3c - System_Numerics_BigInteger_op_Implicit_int16
	.long LDIFF_SYM589
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde60_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_19:

	.byte 5
	.asciz "System_UInt16"

	.byte 18,16
LDIFF_SYM590=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM590
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM591=LDIE_U2 - Ldebug_info_start
	.long LDIFF_SYM591
	.byte 2,35,16,0,7
	.asciz "System_UInt16"

LDIFF_SYM592=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM592
LTDIE_19_POINTER:

	.byte 13
LDIFF_SYM593=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM593
LTDIE_19_REFERENCE:

	.byte 14
LDIFF_SYM594=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM594
	.byte 2
	.asciz "System.Numerics.BigInteger:op_Implicit"
	.asciz "System_Numerics_BigInteger_op_Implicit_uint16"

	.byte 5,144,10
	.quad System_Numerics_BigInteger_op_Implicit_uint16
	.quad Lme_3d

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM595=LDIE_U2 - Ldebug_info_start
	.long LDIFF_SYM595
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM596=Lfde61_end - Lfde61_start
	.long LDIFF_SYM596
Lfde61_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Implicit_uint16

LDIFF_SYM597=Lme_3d - System_Numerics_BigInteger_op_Implicit_uint16
	.long LDIFF_SYM597
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde61_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Implicit"
	.asciz "System_Numerics_BigInteger_op_Implicit_int"

	.byte 5,149,10
	.quad System_Numerics_BigInteger_op_Implicit_int
	.quad Lme_3e

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM598=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM598
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM599=Lfde62_end - Lfde62_start
	.long LDIFF_SYM599
Lfde62_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Implicit_int

LDIFF_SYM600=Lme_3e - System_Numerics_BigInteger_op_Implicit_int
	.long LDIFF_SYM600
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde62_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Implicit"
	.asciz "System_Numerics_BigInteger_op_Implicit_uint"

	.byte 5,155,10
	.quad System_Numerics_BigInteger_op_Implicit_uint
	.quad Lme_3f

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM601=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM601
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM602=Lfde63_end - Lfde63_start
	.long LDIFF_SYM602
Lfde63_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Implicit_uint

LDIFF_SYM603=Lme_3f - System_Numerics_BigInteger_op_Implicit_uint
	.long LDIFF_SYM603
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde63_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Implicit"
	.asciz "System_Numerics_BigInteger_op_Implicit_long"

	.byte 5,160,10
	.quad System_Numerics_BigInteger_op_Implicit_long
	.quad Lme_40

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM604=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM604
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM605=Lfde64_end - Lfde64_start
	.long LDIFF_SYM605
Lfde64_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Implicit_long

LDIFF_SYM606=Lme_40 - System_Numerics_BigInteger_op_Implicit_long
	.long LDIFF_SYM606
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde64_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Implicit"
	.asciz "System_Numerics_BigInteger_op_Implicit_ulong"

	.byte 5,166,10
	.quad System_Numerics_BigInteger_op_Implicit_ulong
	.quad Lme_41

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM607=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM607
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM608=Lfde65_end - Lfde65_start
	.long LDIFF_SYM608
Lfde65_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Implicit_ulong

LDIFF_SYM609=Lme_41 - System_Numerics_BigInteger_op_Implicit_ulong
	.long LDIFF_SYM609
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde65_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger"

	.byte 5,186,10
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger
	.quad Lme_42

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM610=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM610
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM611=Lfde66_end - Lfde66_start
	.long LDIFF_SYM611
Lfde66_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger

LDIFF_SYM612=Lme_42 - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger
	.long LDIFF_SYM612
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde66_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0"

	.byte 5,192,10
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0
	.quad Lme_43

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM613=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM613
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM614=Lfde67_end - Lfde67_start
	.long LDIFF_SYM614
Lfde67_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0

LDIFF_SYM615=Lme_43 - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_0
	.long LDIFF_SYM615
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde67_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_1"

	.byte 5,197,10
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_1
	.quad Lme_44

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM616=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM616
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM617=Lfde68_end - Lfde68_start
	.long LDIFF_SYM617
Lfde68_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_1

LDIFF_SYM618=Lme_44 - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_1
	.long LDIFF_SYM618
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde68_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_2"

	.byte 5,203,10
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_2
	.quad Lme_45

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM619=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM619
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM620=Lfde69_end - Lfde69_start
	.long LDIFF_SYM620
Lfde69_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_2

LDIFF_SYM621=Lme_45 - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_2
	.long LDIFF_SYM621
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde69_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_3"

	.byte 5,209,10
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_3
	.quad Lme_46

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM622=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM622
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM623=Lfde70_end - Lfde70_start
	.long LDIFF_SYM623
Lfde70_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_3

LDIFF_SYM624=Lme_46 - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_3
	.long LDIFF_SYM624
	.long 0
	.byte 12,31,0,68,14,144,1,157,18,158,17,68,13,29
	.align 3
Lfde70_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_4"

	.byte 5,234,10
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_4
	.quad Lme_47

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM625=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM625
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM626=Lfde71_end - Lfde71_start
	.long LDIFF_SYM626
Lfde71_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_4

LDIFF_SYM627=Lme_47 - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_4
	.long LDIFF_SYM627
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29
	.align 3
Lfde71_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_5"

	.byte 5,251,10
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_5
	.quad Lme_48

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM628=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM628
	.byte 2,141,32,11
	.asciz "uu"

LDIFF_SYM629=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM629
	.byte 1,106,11
	.asciz "ll"

LDIFF_SYM630=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM630
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM631=Lfde72_end - Lfde72_start
	.long LDIFF_SYM631
Lfde72_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_5

LDIFF_SYM632=Lme_48 - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_5
	.long LDIFF_SYM632
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,68,153,22,154,21
	.align 3
Lfde72_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_6"

	.byte 5,157,11
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_6
	.quad Lme_49

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM633=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM633
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM634=Lfde73_end - Lfde73_start
	.long LDIFF_SYM634
Lfde73_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_6

LDIFF_SYM635=Lme_49 - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_6
	.long LDIFF_SYM635
	.long 0
	.byte 12,31,0,68,14,160,1,157,20,158,19,68,13,29
	.align 3
Lfde73_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_7"

	.byte 5,177,11
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_7
	.quad Lme_4a

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM636=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM636
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM637=Lfde74_end - Lfde74_start
	.long LDIFF_SYM637
Lfde74_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_7

LDIFF_SYM638=Lme_4a - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_7
	.long LDIFF_SYM638
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde74_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_8"

	.byte 5,184,11
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_8
	.quad Lme_4b

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM639=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM639
	.byte 2,141,56,11
	.asciz "sign"

LDIFF_SYM640=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM640
	.byte 1,106,11
	.asciz "bits"

LDIFF_SYM641=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM641
	.byte 1,105,11
	.asciz "length"

LDIFF_SYM642=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM642
	.byte 1,104,11
	.asciz "m"

LDIFF_SYM643=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM643
	.byte 3,141,232,0,11
	.asciz "l"

LDIFF_SYM644=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM644
	.byte 1,105,11
	.asciz "z"

LDIFF_SYM645=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM645
	.byte 1,102,11
	.asciz "exp"

LDIFF_SYM646=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM646
	.byte 1,104,11
	.asciz "man"

LDIFF_SYM647=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM647
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM648=Lfde75_end - Lfde75_start
	.long LDIFF_SYM648
Lfde75_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_8

LDIFF_SYM649=Lme_4b - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_8
	.long LDIFF_SYM649
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29,68,150,12,151,11,68,152,10,153,9,68,154,8
	.align 3
Lfde75_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Explicit"
	.asciz "System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_9"

	.byte 5,220,11
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_9
	.quad Lme_4c

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM650=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM650
	.byte 3,141,192,0,11
	.asciz "lo"

LDIFF_SYM651=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM651
	.byte 1,105,11
	.asciz "mi"

LDIFF_SYM652=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM652
	.byte 1,104,11
	.asciz "hi"

LDIFF_SYM653=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM653
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM654=Lfde76_end - Lfde76_start
	.long LDIFF_SYM654
Lfde76_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_9

LDIFF_SYM655=Lme_4c - System_Numerics_BigInteger_op_Explicit_System_Numerics_BigInteger_9
	.long LDIFF_SYM655
	.long 0
	.byte 12,31,0,68,14,240,1,157,30,158,29,68,13,29,68,151,28,152,27,68,153,26,154,25
	.align 3
Lfde76_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_UnaryNegation"
	.asciz "System_Numerics_BigInteger_op_UnaryNegation_System_Numerics_BigInteger"

	.byte 5,162,13
	.quad System_Numerics_BigInteger_op_UnaryNegation_System_Numerics_BigInteger
	.quad Lme_4d

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM656=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM656
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM657=Lfde77_end - Lfde77_start
	.long LDIFF_SYM657
Lfde77_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_UnaryNegation_System_Numerics_BigInteger

LDIFF_SYM658=Lme_4d - System_Numerics_BigInteger_op_UnaryNegation_System_Numerics_BigInteger
	.long LDIFF_SYM658
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde77_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Addition"
	.asciz "System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger"

	.byte 5,186,13
	.quad System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger
	.quad Lme_4e

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM659=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM659
	.byte 2,141,32,3
	.asciz "right"

LDIFF_SYM660=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM660
	.byte 2,141,48,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM661=Lfde78_end - Lfde78_start
	.long LDIFF_SYM661
Lfde78_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger

LDIFF_SYM662=Lme_4e - System_Numerics_BigInteger_op_Addition_System_Numerics_BigInteger_System_Numerics_BigInteger
	.long LDIFF_SYM662
	.long 0
	.byte 12,31,0,68,14,240,1,157,30,158,29,68,13,29
	.align 3
Lfde78_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Multiply"
	.asciz "System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger"

	.byte 5,196,13
	.quad System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger
	.quad Lme_4f

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM663=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM663
	.byte 2,141,48,3
	.asciz "right"

LDIFF_SYM664=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM664
	.byte 3,141,192,0,11
	.asciz "trivialLeft"

LDIFF_SYM665=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM665
	.byte 1,106,11
	.asciz "trivialRight"

LDIFF_SYM666=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM666
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM667=Lfde79_end - Lfde79_start
	.long LDIFF_SYM667
Lfde79_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger

LDIFF_SYM668=Lme_4f - System_Numerics_BigInteger_op_Multiply_System_Numerics_BigInteger_System_Numerics_BigInteger
	.long LDIFF_SYM668
	.long 0
	.byte 12,31,0,84,14,240,4,157,78,158,77,68,13,29,68,153,76,154,75
	.align 3
Lfde79_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_LessThanOrEqual"
	.asciz "System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_System_Numerics_BigInteger"

	.byte 5,184,14
	.quad System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_System_Numerics_BigInteger
	.quad Lme_50

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM669=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM669
	.byte 2,141,16,3
	.asciz "right"

LDIFF_SYM670=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM670
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM671=Lfde80_end - Lfde80_start
	.long LDIFF_SYM671
Lfde80_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_System_Numerics_BigInteger

LDIFF_SYM672=Lme_50 - System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_System_Numerics_BigInteger
	.long LDIFF_SYM672
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde80_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Inequality"
	.asciz "System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_System_Numerics_BigInteger"

	.byte 5,203,14
	.quad System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_System_Numerics_BigInteger
	.quad Lme_51

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM673=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM673
	.byte 2,141,16,3
	.asciz "right"

LDIFF_SYM674=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM674
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM675=Lfde81_end - Lfde81_start
	.long LDIFF_SYM675
Lfde81_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_System_Numerics_BigInteger

LDIFF_SYM676=Lme_51 - System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_System_Numerics_BigInteger
	.long LDIFF_SYM676
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde81_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_LessThan"
	.asciz "System_Numerics_BigInteger_op_LessThan_System_Numerics_BigInteger_long"

	.byte 5,208,14
	.quad System_Numerics_BigInteger_op_LessThan_System_Numerics_BigInteger_long
	.quad Lme_52

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM677=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM677
	.byte 2,141,16,3
	.asciz "right"

LDIFF_SYM678=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM678
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM679=Lfde82_end - Lfde82_start
	.long LDIFF_SYM679
Lfde82_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_LessThan_System_Numerics_BigInteger_long

LDIFF_SYM680=Lme_52 - System_Numerics_BigInteger_op_LessThan_System_Numerics_BigInteger_long
	.long LDIFF_SYM680
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde82_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_LessThanOrEqual"
	.asciz "System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_long"

	.byte 5,213,14
	.quad System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_long
	.quad Lme_53

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM681=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM681
	.byte 2,141,16,3
	.asciz "right"

LDIFF_SYM682=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM682
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM683=Lfde83_end - Lfde83_start
	.long LDIFF_SYM683
Lfde83_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_long

LDIFF_SYM684=Lme_53 - System_Numerics_BigInteger_op_LessThanOrEqual_System_Numerics_BigInteger_long
	.long LDIFF_SYM684
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde83_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Equality"
	.asciz "System_Numerics_BigInteger_op_Equality_System_Numerics_BigInteger_long"

	.byte 5,228,14
	.quad System_Numerics_BigInteger_op_Equality_System_Numerics_BigInteger_long
	.quad Lme_54

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM685=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM685
	.byte 2,141,16,3
	.asciz "right"

LDIFF_SYM686=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM686
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM687=Lfde84_end - Lfde84_start
	.long LDIFF_SYM687
Lfde84_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Equality_System_Numerics_BigInteger_long

LDIFF_SYM688=Lme_54 - System_Numerics_BigInteger_op_Equality_System_Numerics_BigInteger_long
	.long LDIFF_SYM688
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde84_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_Inequality"
	.asciz "System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_long"

	.byte 5,233,14
	.quad System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_long
	.quad Lme_55

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM689=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM689
	.byte 2,141,16,3
	.asciz "right"

LDIFF_SYM690=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM690
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM691=Lfde85_end - Lfde85_start
	.long LDIFF_SYM691
Lfde85_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_long

LDIFF_SYM692=Lme_55 - System_Numerics_BigInteger_op_Inequality_System_Numerics_BigInteger_long
	.long LDIFF_SYM692
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde85_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_LessThan"
	.asciz "System_Numerics_BigInteger_op_LessThan_long_System_Numerics_BigInteger"

	.byte 5,238,14
	.quad System_Numerics_BigInteger_op_LessThan_long_System_Numerics_BigInteger
	.quad Lme_56

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM693=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM693
	.byte 2,141,16,3
	.asciz "right"

LDIFF_SYM694=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM694
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM695=Lfde86_end - Lfde86_start
	.long LDIFF_SYM695
Lfde86_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_LessThan_long_System_Numerics_BigInteger

LDIFF_SYM696=Lme_56 - System_Numerics_BigInteger_op_LessThan_long_System_Numerics_BigInteger
	.long LDIFF_SYM696
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde86_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:op_LessThanOrEqual"
	.asciz "System_Numerics_BigInteger_op_LessThanOrEqual_long_System_Numerics_BigInteger"

	.byte 5,243,14
	.quad System_Numerics_BigInteger_op_LessThanOrEqual_long_System_Numerics_BigInteger
	.quad Lme_57

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM697=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM697
	.byte 2,141,16,3
	.asciz "right"

LDIFF_SYM698=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM698
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM699=Lfde87_end - Lfde87_start
	.long LDIFF_SYM699
Lfde87_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_op_LessThanOrEqual_long_System_Numerics_BigInteger

LDIFF_SYM700=Lme_57 - System_Numerics_BigInteger_op_LessThanOrEqual_long_System_Numerics_BigInteger
	.long LDIFF_SYM700
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde87_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:GetDiffLength"
	.asciz "System_Numerics_BigInteger_GetDiffLength_uint___uint___int"

	.byte 5,246,15
	.quad System_Numerics_BigInteger_GetDiffLength_uint___uint___int
	.quad Lme_58

	.byte 2,118,16,3
	.asciz "rgu1"

LDIFF_SYM701=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM701
	.byte 1,104,3
	.asciz "rgu2"

LDIFF_SYM702=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM702
	.byte 1,105,3
	.asciz "cu"

LDIFF_SYM703=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM703
	.byte 2,141,40,11
	.asciz "iv"

LDIFF_SYM704=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM704
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM705=Lfde88_end - Lfde88_start
	.long LDIFF_SYM705
Lfde88_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_GetDiffLength_uint___uint___int

LDIFF_SYM706=Lme_58 - System_Numerics_BigInteger_GetDiffLength_uint___uint___int
	.long LDIFF_SYM706
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,151,4,152,3,68,153,2
	.align 3
Lfde88_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:AssertValid"
	.asciz "System_Numerics_BigInteger_AssertValid"

	.byte 5,129,16
	.quad System_Numerics_BigInteger_AssertValid
	.quad Lme_59

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM707=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM707
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM708=Lfde89_end - Lfde89_start
	.long LDIFF_SYM708
Lfde89_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger_AssertValid

LDIFF_SYM709=Lme_59 - System_Numerics_BigInteger_AssertValid
	.long LDIFF_SYM709
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde89_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigInteger:.cctor"
	.asciz "System_Numerics_BigInteger__cctor"

	.byte 5,29
	.quad System_Numerics_BigInteger__cctor
	.quad Lme_5a

	.byte 2,118,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM710=Lfde90_end - Lfde90_start
	.long LDIFF_SYM710
Lfde90_start:

	.long 0
	.align 3
	.quad System_Numerics_BigInteger__cctor

LDIFF_SYM711=Lme_5a - System_Numerics_BigInteger__cctor
	.long LDIFF_SYM711
	.long 0
	.byte 12,31,0,68,14,144,1,157,18,158,17,68,13,29
	.align 3
Lfde90_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Add"
	.asciz "System_Numerics_BigIntegerCalculator_Add_uint___uint"

	.byte 6,21
	.quad System_Numerics_BigIntegerCalculator_Add_uint___uint
	.quad Lme_5b

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM712=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM712
	.byte 1,105,3
	.asciz "right"

LDIFF_SYM713=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM713
	.byte 1,106,11
	.asciz "bits"

LDIFF_SYM714=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM714
	.byte 1,104,11
	.asciz "digit"

LDIFF_SYM715=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM715
	.byte 1,106,11
	.asciz "carry"

LDIFF_SYM716=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM716
	.byte 1,103,11
	.asciz "i"

LDIFF_SYM717=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM717
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM718=Lfde91_end - Lfde91_start
	.long LDIFF_SYM718
Lfde91_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Add_uint___uint

LDIFF_SYM719=Lme_5b - System_Numerics_BigIntegerCalculator_Add_uint___uint
	.long LDIFF_SYM719
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,150,6,151,5,68,152,4,153,3,68,154,2
	.align 3
Lfde91_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Add"
	.asciz "System_Numerics_BigIntegerCalculator_Add_uint___uint__"

	.byte 6,48
	.quad System_Numerics_BigIntegerCalculator_Add_uint___uint__
	.quad Lme_5c

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM720=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM720
	.byte 1,105,3
	.asciz "right"

LDIFF_SYM721=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM721
	.byte 2,141,56,11
	.asciz "bits"

LDIFF_SYM722=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM722
	.byte 1,104,11
	.asciz "l"

LDIFF_SYM723=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM723
	.byte 1,103,11
	.asciz "r"

LDIFF_SYM724=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM724
	.byte 1,102,11
	.asciz "b"

LDIFF_SYM725=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM725
	.byte 1,101,11
	.asciz "V_4"

LDIFF_SYM726=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM726
	.byte 1,103,11
	.asciz "V_5"

LDIFF_SYM727=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM727
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM728=Lfde92_end - Lfde92_start
	.long LDIFF_SYM728
Lfde92_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Add_uint___uint__

LDIFF_SYM729=Lme_5c - System_Numerics_BigIntegerCalculator_Add_uint___uint__
	.long LDIFF_SYM729
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,149,6,150,5,68,151,4,152,3,68,153,2
	.align 3
Lfde92_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Add"
	.asciz "System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int"

	.byte 6,75
	.quad System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int
	.quad Lme_5d

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM730=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM730
	.byte 1,102,3
	.asciz "leftLength"

LDIFF_SYM731=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM731
	.byte 1,103,3
	.asciz "right"

LDIFF_SYM732=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM732
	.byte 1,104,3
	.asciz "rightLength"

LDIFF_SYM733=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM733
	.byte 1,105,3
	.asciz "bits"

LDIFF_SYM734=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM734
	.byte 1,106,3
	.asciz "bitsLength"

LDIFF_SYM735=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM735
	.byte 0,11
	.asciz "i"

LDIFF_SYM736=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM736
	.byte 1,101,11
	.asciz "carry"

LDIFF_SYM737=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM737
	.byte 1,100,11
	.asciz "digit"

LDIFF_SYM738=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM738
	.byte 1,100,11
	.asciz "digit"

LDIFF_SYM739=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM739
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM740=Lfde93_end - Lfde93_start
	.long LDIFF_SYM740
Lfde93_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int

LDIFF_SYM741=Lme_5d - System_Numerics_BigIntegerCalculator_Add_uint__int_uint__int_uint__int
	.long LDIFF_SYM741
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,148,8,149,7,68,150,6,151,5,68,152,4,153,3,68,154,2
	.align 3
Lfde93_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:AddSelf"
	.asciz "System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int"

	.byte 6,105
	.quad System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int
	.quad Lme_5e

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM742=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM742
	.byte 1,103,3
	.asciz "leftLength"

LDIFF_SYM743=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM743
	.byte 1,104,3
	.asciz "right"

LDIFF_SYM744=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM744
	.byte 1,105,3
	.asciz "rightLength"

LDIFF_SYM745=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM745
	.byte 1,106,11
	.asciz "i"

LDIFF_SYM746=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM746
	.byte 1,102,11
	.asciz "carry"

LDIFF_SYM747=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM747
	.byte 1,101,11
	.asciz "digit"

LDIFF_SYM748=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM748
	.byte 1,101,11
	.asciz "digit"

LDIFF_SYM749=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM749
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM750=Lfde94_end - Lfde94_start
	.long LDIFF_SYM750
Lfde94_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int

LDIFF_SYM751=Lme_5e - System_Numerics_BigIntegerCalculator_AddSelf_uint__int_uint__int
	.long LDIFF_SYM751
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,149,6,150,5,68,151,4,152,3,68,153,2,154,1
	.align 3
Lfde94_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Subtract"
	.asciz "System_Numerics_BigIntegerCalculator_Subtract_uint___uint"

	.byte 6,134,1
	.quad System_Numerics_BigIntegerCalculator_Subtract_uint___uint
	.quad Lme_5f

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM752=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM752
	.byte 1,105,3
	.asciz "right"

LDIFF_SYM753=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM753
	.byte 1,106,11
	.asciz "bits"

LDIFF_SYM754=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM754
	.byte 1,104,11
	.asciz "digit"

LDIFF_SYM755=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM755
	.byte 1,106,11
	.asciz "carry"

LDIFF_SYM756=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM756
	.byte 1,103,11
	.asciz "i"

LDIFF_SYM757=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM757
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM758=Lfde95_end - Lfde95_start
	.long LDIFF_SYM758
Lfde95_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Subtract_uint___uint

LDIFF_SYM759=Lme_5f - System_Numerics_BigIntegerCalculator_Subtract_uint___uint
	.long LDIFF_SYM759
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,150,6,151,5,68,152,4,153,3,68,154,2
	.align 3
Lfde95_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Subtract"
	.asciz "System_Numerics_BigIntegerCalculator_Subtract_uint___uint__"

	.byte 6,161,1
	.quad System_Numerics_BigIntegerCalculator_Subtract_uint___uint__
	.quad Lme_60

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM760=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM760
	.byte 1,105,3
	.asciz "right"

LDIFF_SYM761=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM761
	.byte 2,141,56,11
	.asciz "bits"

LDIFF_SYM762=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM762
	.byte 1,104,11
	.asciz "l"

LDIFF_SYM763=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM763
	.byte 1,103,11
	.asciz "r"

LDIFF_SYM764=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM764
	.byte 1,102,11
	.asciz "b"

LDIFF_SYM765=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM765
	.byte 1,101,11
	.asciz "V_4"

LDIFF_SYM766=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM766
	.byte 1,103,11
	.asciz "V_5"

LDIFF_SYM767=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM767
	.byte 1,102,11
	.asciz "V_6"

LDIFF_SYM768=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM768
	.byte 1,101,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM769=Lfde96_end - Lfde96_start
	.long LDIFF_SYM769
Lfde96_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Subtract_uint___uint__

LDIFF_SYM770=Lme_60 - System_Numerics_BigIntegerCalculator_Subtract_uint___uint__
	.long LDIFF_SYM770
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,149,6,150,5,68,151,4,152,3,68,153,2
	.align 3
Lfde96_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Subtract"
	.asciz "System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int"

	.byte 6,189,1
	.quad System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int
	.quad Lme_61

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM771=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM771
	.byte 1,102,3
	.asciz "leftLength"

LDIFF_SYM772=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM772
	.byte 1,103,3
	.asciz "right"

LDIFF_SYM773=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM773
	.byte 1,104,3
	.asciz "rightLength"

LDIFF_SYM774=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM774
	.byte 1,105,3
	.asciz "bits"

LDIFF_SYM775=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM775
	.byte 1,106,3
	.asciz "bitsLength"

LDIFF_SYM776=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM776
	.byte 0,11
	.asciz "i"

LDIFF_SYM777=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM777
	.byte 1,101,11
	.asciz "carry"

LDIFF_SYM778=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM778
	.byte 1,100,11
	.asciz "digit"

LDIFF_SYM779=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM779
	.byte 1,100,11
	.asciz "digit"

LDIFF_SYM780=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM780
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM781=Lfde97_end - Lfde97_start
	.long LDIFF_SYM781
Lfde97_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int

LDIFF_SYM782=Lme_61 - System_Numerics_BigIntegerCalculator_Subtract_uint__int_uint__int_uint__int
	.long LDIFF_SYM782
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,148,8,149,7,68,150,6,151,5,68,152,4,153,3,68,154,2
	.align 3
Lfde97_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Compare"
	.asciz "System_Numerics_BigIntegerCalculator_Compare_uint___uint__"

	.byte 6,245,1
	.quad System_Numerics_BigIntegerCalculator_Compare_uint___uint__
	.quad Lme_62

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM783=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM783
	.byte 1,105,3
	.asciz "right"

LDIFF_SYM784=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM784
	.byte 1,106,11
	.asciz "i"

LDIFF_SYM785=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM785
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM786=Lfde98_end - Lfde98_start
	.long LDIFF_SYM786
Lfde98_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Compare_uint___uint__

LDIFF_SYM787=Lme_62 - System_Numerics_BigIntegerCalculator_Compare_uint___uint__
	.long LDIFF_SYM787
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,152,4,153,3,68,154,2
	.align 3
Lfde98_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Square"
	.asciz "System_Numerics_BigIntegerCalculator_Square_uint__"

	.byte 7,20
	.quad System_Numerics_BigIntegerCalculator_Square_uint__
	.quad Lme_63

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM788=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM788
	.byte 1,106,11
	.asciz "bits"

LDIFF_SYM789=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM789
	.byte 1,105,11
	.asciz "v"

LDIFF_SYM790=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM790
	.byte 1,104,11
	.asciz "b"

LDIFF_SYM791=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM791
	.byte 1,103,11
	.asciz "V_3"

LDIFF_SYM792=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM792
	.byte 1,104,11
	.asciz "V_4"

LDIFF_SYM793=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM793
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM794=Lfde99_end - Lfde99_start
	.long LDIFF_SYM794
Lfde99_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Square_uint__

LDIFF_SYM795=Lme_63 - System_Numerics_BigIntegerCalculator_Square_uint__
	.long LDIFF_SYM795
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,151,4,152,3,68,153,2,154,1
	.align 3
Lfde99_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Square"
	.asciz "System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int"

	.byte 7,51
	.quad System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int
	.quad Lme_64

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM796=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM796
	.byte 1,103,3
	.asciz "valueLength"

LDIFF_SYM797=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM797
	.byte 1,104,3
	.asciz "bits"

LDIFF_SYM798=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM798
	.byte 1,105,3
	.asciz "bitsLength"

LDIFF_SYM799=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM799
	.byte 3,141,208,0,11
	.asciz "i"

LDIFF_SYM800=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM800
	.byte 1,106,11
	.asciz "carry"

LDIFF_SYM801=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM801
	.byte 1,102,11
	.asciz "digits"

LDIFF_SYM802=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM802
	.byte 1,101,11
	.asciz "j"

LDIFF_SYM803=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM803
	.byte 1,101,11
	.asciz "digit1"

LDIFF_SYM804=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM804
	.byte 1,100,11
	.asciz "digit2"

LDIFF_SYM805=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM805
	.byte 1,102,11
	.asciz "n"

LDIFF_SYM806=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM806
	.byte 1,102,11
	.asciz "n2"

LDIFF_SYM807=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM807
	.byte 1,101,11
	.asciz "valueLow"

LDIFF_SYM808=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM808
	.byte 1,100,11
	.asciz "valueLowLength"

LDIFF_SYM809=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM809
	.byte 1,99,11
	.asciz "valueHigh"

LDIFF_SYM810=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM810
	.byte 3,141,216,0,11
	.asciz "valueHighLength"

LDIFF_SYM811=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM811
	.byte 1,106,11
	.asciz "bitsLow"

LDIFF_SYM812=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM812
	.byte 1,104,11
	.asciz "bitsLowLength"

LDIFF_SYM813=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM813
	.byte 3,141,224,0,11
	.asciz "bitsHigh"

LDIFF_SYM814=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM814
	.byte 3,141,232,0,11
	.asciz "bitsHighLength"

LDIFF_SYM815=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM815
	.byte 3,141,240,0,11
	.asciz "foldLength"

LDIFF_SYM816=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM816
	.byte 1,103,11
	.asciz "coreLength"

LDIFF_SYM817=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM817
	.byte 1,101,11
	.asciz "fold"

LDIFF_SYM818=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM818
	.byte 3,141,128,1,11
	.asciz "core"

LDIFF_SYM819=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM819
	.byte 3,141,152,1,11
	.asciz "fold"

LDIFF_SYM820=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM820
	.byte 3,141,128,1,11
	.asciz "core"

LDIFF_SYM821=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM821
	.byte 3,141,144,1,11
	.asciz "V_22"

LDIFF_SYM822=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM822
	.byte 3,141,248,0,11
	.asciz "V_23"

LDIFF_SYM823=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM823
	.byte 3,141,136,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM824=Lfde100_end - Lfde100_start
	.long LDIFF_SYM824
Lfde100_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int

LDIFF_SYM825=Lme_64 - System_Numerics_BigIntegerCalculator_Square_uint__int_uint__int
	.long LDIFF_SYM825
	.long 0
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,147,20,148,19,68,149,18,150,17,68,151,16,152,15,68,153,14
	.byte 154,13
	.align 3
Lfde100_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Multiply"
	.asciz "System_Numerics_BigIntegerCalculator_Multiply_uint___uint"

	.byte 7,173,1
	.quad System_Numerics_BigIntegerCalculator_Multiply_uint___uint
	.quad Lme_65

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM826=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM826
	.byte 1,105,3
	.asciz "right"

LDIFF_SYM827=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM827
	.byte 1,106,11
	.asciz "i"

LDIFF_SYM828=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM828
	.byte 1,104,11
	.asciz "carry"

LDIFF_SYM829=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM829
	.byte 1,103,11
	.asciz "bits"

LDIFF_SYM830=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM830
	.byte 1,102,11
	.asciz "digits"

LDIFF_SYM831=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM831
	.byte 1,101,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM832=Lfde101_end - Lfde101_start
	.long LDIFF_SYM832
Lfde101_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Multiply_uint___uint

LDIFF_SYM833=Lme_65 - System_Numerics_BigIntegerCalculator_Multiply_uint___uint
	.long LDIFF_SYM833
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,149,6,150,5,68,151,4,152,3,68,153,2,154,1
	.align 3
Lfde101_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Multiply"
	.asciz "System_Numerics_BigIntegerCalculator_Multiply_uint___uint__"

	.byte 7,198,1
	.quad System_Numerics_BigIntegerCalculator_Multiply_uint___uint__
	.quad Lme_66

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM834=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM834
	.byte 1,105,3
	.asciz "right"

LDIFF_SYM835=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM835
	.byte 1,106,11
	.asciz "bits"

LDIFF_SYM836=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM836
	.byte 1,104,11
	.asciz "l"

LDIFF_SYM837=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM837
	.byte 1,103,11
	.asciz "r"

LDIFF_SYM838=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM838
	.byte 1,102,11
	.asciz "b"

LDIFF_SYM839=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM839
	.byte 1,101,11
	.asciz "V_4"

LDIFF_SYM840=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM840
	.byte 1,103,11
	.asciz "V_5"

LDIFF_SYM841=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM841
	.byte 1,102,11
	.asciz "V_6"

LDIFF_SYM842=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM842
	.byte 1,101,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM843=Lfde102_end - Lfde102_start
	.long LDIFF_SYM843
Lfde102_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Multiply_uint___uint__

LDIFF_SYM844=Lme_66 - System_Numerics_BigIntegerCalculator_Multiply_uint___uint__
	.long LDIFF_SYM844
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,149,6,150,5,68,151,4,152,3,68,153,2,154,1
	.align 3
Lfde102_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:Multiply"
	.asciz "System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int"

	.byte 7,232,1
	.quad System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int
	.quad Lme_67

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM845=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM845
	.byte 1,101,3
	.asciz "leftLength"

LDIFF_SYM846=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM846
	.byte 1,102,3
	.asciz "right"

LDIFF_SYM847=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM847
	.byte 1,103,3
	.asciz "rightLength"

LDIFF_SYM848=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM848
	.byte 1,104,3
	.asciz "bits"

LDIFF_SYM849=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM849
	.byte 1,105,3
	.asciz "bitsLength"

LDIFF_SYM850=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM850
	.byte 3,141,208,0,11
	.asciz "i"

LDIFF_SYM851=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM851
	.byte 1,106,11
	.asciz "carry"

LDIFF_SYM852=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM852
	.byte 1,100,11
	.asciz "j"

LDIFF_SYM853=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM853
	.byte 1,99,11
	.asciz "digits"

LDIFF_SYM854=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM854
	.byte 1,100,11
	.asciz "n"

LDIFF_SYM855=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM855
	.byte 1,100,11
	.asciz "n2"

LDIFF_SYM856=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM856
	.byte 1,99,11
	.asciz "leftLow"

LDIFF_SYM857=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM857
	.byte 3,141,216,0,11
	.asciz "leftLowLength"

LDIFF_SYM858=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM858
	.byte 3,141,224,0,11
	.asciz "leftHigh"

LDIFF_SYM859=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM859
	.byte 3,141,232,0,11
	.asciz "leftHighLength"

LDIFF_SYM860=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM860
	.byte 1,106,11
	.asciz "rightLow"

LDIFF_SYM861=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM861
	.byte 1,102,11
	.asciz "rightLowLength"

LDIFF_SYM862=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM862
	.byte 3,141,240,0,11
	.asciz "rightHigh"

LDIFF_SYM863=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM863
	.byte 3,141,248,0,11
	.asciz "rightHighLength"

LDIFF_SYM864=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM864
	.byte 3,141,128,1,11
	.asciz "bitsLow"

LDIFF_SYM865=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM865
	.byte 1,104,11
	.asciz "bitsLowLength"

LDIFF_SYM866=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM866
	.byte 3,141,136,1,11
	.asciz "bitsHigh"

LDIFF_SYM867=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM867
	.byte 3,141,144,1,11
	.asciz "bitsHighLength"

LDIFF_SYM868=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM868
	.byte 3,141,152,1,11
	.asciz "leftFoldLength"

LDIFF_SYM869=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM869
	.byte 1,103,11
	.asciz "rightFoldLength"

LDIFF_SYM870=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM870
	.byte 1,101,11
	.asciz "coreLength"

LDIFF_SYM871=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM871
	.byte 1,99,11
	.asciz "leftFold"

LDIFF_SYM872=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM872
	.byte 3,141,168,1,11
	.asciz "rightFold"

LDIFF_SYM873=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM873
	.byte 3,141,200,1,11
	.asciz "core"

LDIFF_SYM874=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM874
	.byte 3,141,216,1,11
	.asciz "leftFold"

LDIFF_SYM875=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM875
	.byte 3,141,168,1,11
	.asciz "rightFold"

LDIFF_SYM876=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM876
	.byte 3,141,184,1,11
	.asciz "core"

LDIFF_SYM877=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM877
	.byte 3,141,200,1,11
	.asciz "V_27"

LDIFF_SYM878=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM878
	.byte 3,141,160,1,11
	.asciz "V_28"

LDIFF_SYM879=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM879
	.byte 3,141,176,1,11
	.asciz "V_29"

LDIFF_SYM880=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM880
	.byte 3,141,192,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM881=Lfde103_end - Lfde103_start
	.long LDIFF_SYM881
Lfde103_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int

LDIFF_SYM882=Lme_67 - System_Numerics_BigIntegerCalculator_Multiply_uint__int_uint__int_uint__int
	.long LDIFF_SYM882
	.long 0
	.byte 12,31,0,68,14,240,1,157,30,158,29,68,13,29,68,147,28,148,27,68,149,26,150,25,68,151,24,152,23,68,153,22
	.byte 154,21
	.align 3
Lfde103_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:SubtractCore"
	.asciz "System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int"

	.byte 7,251,2
	.quad System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int
	.quad Lme_68

	.byte 2,118,16,3
	.asciz "left"

LDIFF_SYM883=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM883
	.byte 1,101,3
	.asciz "leftLength"

LDIFF_SYM884=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM884
	.byte 1,102,3
	.asciz "right"

LDIFF_SYM885=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM885
	.byte 1,103,3
	.asciz "rightLength"

LDIFF_SYM886=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM886
	.byte 1,104,3
	.asciz "core"

LDIFF_SYM887=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM887
	.byte 1,105,3
	.asciz "coreLength"

LDIFF_SYM888=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM888
	.byte 1,106,11
	.asciz "i"

LDIFF_SYM889=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM889
	.byte 1,100,11
	.asciz "carry"

LDIFF_SYM890=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM890
	.byte 1,99,11
	.asciz "digit"

LDIFF_SYM891=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM891
	.byte 1,99,11
	.asciz "digit"

LDIFF_SYM892=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM892
	.byte 1,104,11
	.asciz "digit"

LDIFF_SYM893=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM893
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM894=Lfde104_end - Lfde104_start
	.long LDIFF_SYM894
Lfde104_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int

LDIFF_SYM895=Lme_68 - System_Numerics_BigIntegerCalculator_SubtractCore_uint__int_uint__int_uint__int
	.long LDIFF_SYM895
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,147,8,148,7,68,149,6,150,5,68,151,4,152,3,68,153,2,154
	.byte 1
	.align 3
Lfde104_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigIntegerCalculator:.cctor"
	.asciz "System_Numerics_BigIntegerCalculator__cctor"

	.byte 7,229,1
	.quad System_Numerics_BigIntegerCalculator__cctor
	.quad Lme_69

	.byte 2,118,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM896=Lfde105_end - Lfde105_start
	.long LDIFF_SYM896
Lfde105_start:

	.long 0
	.align 3
	.quad System_Numerics_BigIntegerCalculator__cctor

LDIFF_SYM897=Lme_69 - System_Numerics_BigIntegerCalculator__cctor
	.long LDIFF_SYM897
	.long 0
	.byte 12,31,0,68,14,16,157,2,158,1,68,13,29
	.align 3
Lfde105_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_23:

	.byte 17
	.asciz "System_Collections_IDictionary"

	.byte 16,7
	.asciz "System_Collections_IDictionary"

LDIFF_SYM898=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM898
LTDIE_23_POINTER:

	.byte 13
LDIFF_SYM899=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM899
LTDIE_23_REFERENCE:

	.byte 14
LDIFF_SYM900=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM900
LTDIE_25:

	.byte 17
	.asciz "System_Collections_Generic_IList`1"

	.byte 16,7
	.asciz "System_Collections_Generic_IList`1"

LDIFF_SYM901=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM901
LTDIE_25_POINTER:

	.byte 13
LDIFF_SYM902=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM902
LTDIE_25_REFERENCE:

	.byte 14
LDIFF_SYM903=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM903
LTDIE_28:

	.byte 17
	.asciz "System_Collections_Generic_IEqualityComparer`1"

	.byte 16,7
	.asciz "System_Collections_Generic_IEqualityComparer`1"

LDIFF_SYM904=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM904
LTDIE_28_POINTER:

	.byte 13
LDIFF_SYM905=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM905
LTDIE_28_REFERENCE:

	.byte 14
LDIFF_SYM906=LTDIE_28 - Ldebug_info_start
	.long LDIFF_SYM906
LTDIE_29:

	.byte 5
	.asciz "_KeyCollection"

	.byte 24,16
LDIFF_SYM907=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM907
	.byte 2,35,0,6
	.asciz "dictionary"

LDIFF_SYM908=LTDIE_27_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM908
	.byte 2,35,16,0,7
	.asciz "_KeyCollection"

LDIFF_SYM909=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM909
LTDIE_29_POINTER:

	.byte 13
LDIFF_SYM910=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM910
LTDIE_29_REFERENCE:

	.byte 14
LDIFF_SYM911=LTDIE_29 - Ldebug_info_start
	.long LDIFF_SYM911
LTDIE_30:

	.byte 5
	.asciz "_ValueCollection"

	.byte 24,16
LDIFF_SYM912=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM912
	.byte 2,35,0,6
	.asciz "dictionary"

LDIFF_SYM913=LTDIE_27_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM913
	.byte 2,35,16,0,7
	.asciz "_ValueCollection"

LDIFF_SYM914=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM914
LTDIE_30_POINTER:

	.byte 13
LDIFF_SYM915=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM915
LTDIE_30_REFERENCE:

	.byte 14
LDIFF_SYM916=LTDIE_30 - Ldebug_info_start
	.long LDIFF_SYM916
LTDIE_27:

	.byte 5
	.asciz "System_Collections_Generic_Dictionary`2"

	.byte 72,16
LDIFF_SYM917=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM917
	.byte 2,35,0,6
	.asciz "buckets"

LDIFF_SYM918=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM918
	.byte 2,35,16,6
	.asciz "entries"

LDIFF_SYM919=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM919
	.byte 2,35,24,6
	.asciz "count"

LDIFF_SYM920=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM920
	.byte 2,35,56,6
	.asciz "version"

LDIFF_SYM921=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM921
	.byte 2,35,60,6
	.asciz "freeList"

LDIFF_SYM922=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM922
	.byte 2,35,64,6
	.asciz "freeCount"

LDIFF_SYM923=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM923
	.byte 2,35,68,6
	.asciz "comparer"

LDIFF_SYM924=LTDIE_28_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM924
	.byte 2,35,32,6
	.asciz "keys"

LDIFF_SYM925=LTDIE_29_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM925
	.byte 2,35,40,6
	.asciz "values"

LDIFF_SYM926=LTDIE_30_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM926
	.byte 2,35,48,0,7
	.asciz "System_Collections_Generic_Dictionary`2"

LDIFF_SYM927=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM927
LTDIE_27_POINTER:

	.byte 13
LDIFF_SYM928=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM928
LTDIE_27_REFERENCE:

	.byte 14
LDIFF_SYM929=LTDIE_27 - Ldebug_info_start
	.long LDIFF_SYM929
LTDIE_31:

	.byte 17
	.asciz "System_Runtime_Serialization_IFormatterConverter"

	.byte 16,7
	.asciz "System_Runtime_Serialization_IFormatterConverter"

LDIFF_SYM930=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM930
LTDIE_31_POINTER:

	.byte 13
LDIFF_SYM931=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM931
LTDIE_31_REFERENCE:

	.byte 14
LDIFF_SYM932=LTDIE_31 - Ldebug_info_start
	.long LDIFF_SYM932
LTDIE_33:

	.byte 5
	.asciz "System_Reflection_MemberInfo"

	.byte 16,16
LDIFF_SYM933=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM933
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MemberInfo"

LDIFF_SYM934=LTDIE_33 - Ldebug_info_start
	.long LDIFF_SYM934
LTDIE_33_POINTER:

	.byte 13
LDIFF_SYM935=LTDIE_33 - Ldebug_info_start
	.long LDIFF_SYM935
LTDIE_33_REFERENCE:

	.byte 14
LDIFF_SYM936=LTDIE_33 - Ldebug_info_start
	.long LDIFF_SYM936
LTDIE_32:

	.byte 5
	.asciz "System_Type"

	.byte 24,16
LDIFF_SYM937=LTDIE_33 - Ldebug_info_start
	.long LDIFF_SYM937
	.byte 2,35,0,6
	.asciz "_impl"

LDIFF_SYM938=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM938
	.byte 2,35,16,0,7
	.asciz "System_Type"

LDIFF_SYM939=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM939
LTDIE_32_POINTER:

	.byte 13
LDIFF_SYM940=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM940
LTDIE_32_REFERENCE:

	.byte 14
LDIFF_SYM941=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM941
LTDIE_26:

	.byte 5
	.asciz "System_Runtime_Serialization_SerializationInfo"

	.byte 88,16
LDIFF_SYM942=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM942
	.byte 2,35,0,6
	.asciz "m_members"

LDIFF_SYM943=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM943
	.byte 2,35,16,6
	.asciz "m_data"

LDIFF_SYM944=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM944
	.byte 2,35,24,6
	.asciz "m_types"

LDIFF_SYM945=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM945
	.byte 2,35,32,6
	.asciz "m_nameToIndex"

LDIFF_SYM946=LTDIE_27_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM946
	.byte 2,35,40,6
	.asciz "m_currMember"

LDIFF_SYM947=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM947
	.byte 2,35,80,6
	.asciz "m_converter"

LDIFF_SYM948=LTDIE_31_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM948
	.byte 2,35,48,6
	.asciz "m_fullTypeName"

LDIFF_SYM949=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM949
	.byte 2,35,56,6
	.asciz "m_assemName"

LDIFF_SYM950=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM950
	.byte 2,35,64,6
	.asciz "objectType"

LDIFF_SYM951=LTDIE_32_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM951
	.byte 2,35,72,6
	.asciz "isFullTypeNameSetExplicit"

LDIFF_SYM952=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM952
	.byte 2,35,84,6
	.asciz "isAssemblyNameSetExplicit"

LDIFF_SYM953=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM953
	.byte 2,35,85,6
	.asciz "requireSameTokenInPartialTrust"

LDIFF_SYM954=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM954
	.byte 2,35,86,0,7
	.asciz "System_Runtime_Serialization_SerializationInfo"

LDIFF_SYM955=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM955
LTDIE_26_POINTER:

	.byte 13
LDIFF_SYM956=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM956
LTDIE_26_REFERENCE:

	.byte 14
LDIFF_SYM957=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM957
LTDIE_35:

	.byte 5
	.asciz "System_Reflection_TypeInfo"

	.byte 24,16
LDIFF_SYM958=LTDIE_32 - Ldebug_info_start
	.long LDIFF_SYM958
	.byte 2,35,0,0,7
	.asciz "System_Reflection_TypeInfo"

LDIFF_SYM959=LTDIE_35 - Ldebug_info_start
	.long LDIFF_SYM959
LTDIE_35_POINTER:

	.byte 13
LDIFF_SYM960=LTDIE_35 - Ldebug_info_start
	.long LDIFF_SYM960
LTDIE_35_REFERENCE:

	.byte 14
LDIFF_SYM961=LTDIE_35 - Ldebug_info_start
	.long LDIFF_SYM961
LTDIE_40:

	.byte 5
	.asciz "System_Reflection_MethodBase"

	.byte 16,16
LDIFF_SYM962=LTDIE_33 - Ldebug_info_start
	.long LDIFF_SYM962
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MethodBase"

LDIFF_SYM963=LTDIE_40 - Ldebug_info_start
	.long LDIFF_SYM963
LTDIE_40_POINTER:

	.byte 13
LDIFF_SYM964=LTDIE_40 - Ldebug_info_start
	.long LDIFF_SYM964
LTDIE_40_REFERENCE:

	.byte 14
LDIFF_SYM965=LTDIE_40 - Ldebug_info_start
	.long LDIFF_SYM965
LTDIE_39:

	.byte 5
	.asciz "System_Reflection_ConstructorInfo"

	.byte 16,16
LDIFF_SYM966=LTDIE_40 - Ldebug_info_start
	.long LDIFF_SYM966
	.byte 2,35,0,0,7
	.asciz "System_Reflection_ConstructorInfo"

LDIFF_SYM967=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM967
LTDIE_39_POINTER:

	.byte 13
LDIFF_SYM968=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM968
LTDIE_39_REFERENCE:

	.byte 14
LDIFF_SYM969=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM969
LTDIE_38:

	.byte 5
	.asciz "System_Reflection_RuntimeConstructorInfo"

	.byte 16,16
LDIFF_SYM970=LTDIE_39 - Ldebug_info_start
	.long LDIFF_SYM970
	.byte 2,35,0,0,7
	.asciz "System_Reflection_RuntimeConstructorInfo"

LDIFF_SYM971=LTDIE_38 - Ldebug_info_start
	.long LDIFF_SYM971
LTDIE_38_POINTER:

	.byte 13
LDIFF_SYM972=LTDIE_38 - Ldebug_info_start
	.long LDIFF_SYM972
LTDIE_38_REFERENCE:

	.byte 14
LDIFF_SYM973=LTDIE_38 - Ldebug_info_start
	.long LDIFF_SYM973
LTDIE_37:

	.byte 5
	.asciz "System_Reflection_MonoCMethod"

	.byte 40,16
LDIFF_SYM974=LTDIE_38 - Ldebug_info_start
	.long LDIFF_SYM974
	.byte 2,35,0,6
	.asciz "mhandle"

LDIFF_SYM975=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM975
	.byte 2,35,16,6
	.asciz "name"

LDIFF_SYM976=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM976
	.byte 2,35,24,6
	.asciz "reftype"

LDIFF_SYM977=LTDIE_32_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM977
	.byte 2,35,32,0,7
	.asciz "System_Reflection_MonoCMethod"

LDIFF_SYM978=LTDIE_37 - Ldebug_info_start
	.long LDIFF_SYM978
LTDIE_37_POINTER:

	.byte 13
LDIFF_SYM979=LTDIE_37 - Ldebug_info_start
	.long LDIFF_SYM979
LTDIE_37_REFERENCE:

	.byte 14
LDIFF_SYM980=LTDIE_37 - Ldebug_info_start
	.long LDIFF_SYM980
LTDIE_36:

	.byte 5
	.asciz "System_MonoTypeInfo"

	.byte 32,16
LDIFF_SYM981=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM981
	.byte 2,35,0,6
	.asciz "full_name"

LDIFF_SYM982=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM982
	.byte 2,35,16,6
	.asciz "default_ctor"

LDIFF_SYM983=LTDIE_37_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM983
	.byte 2,35,24,0,7
	.asciz "System_MonoTypeInfo"

LDIFF_SYM984=LTDIE_36 - Ldebug_info_start
	.long LDIFF_SYM984
LTDIE_36_POINTER:

	.byte 13
LDIFF_SYM985=LTDIE_36 - Ldebug_info_start
	.long LDIFF_SYM985
LTDIE_36_REFERENCE:

	.byte 14
LDIFF_SYM986=LTDIE_36 - Ldebug_info_start
	.long LDIFF_SYM986
LTDIE_34:

	.byte 5
	.asciz "System_RuntimeType"

	.byte 48,16
LDIFF_SYM987=LTDIE_35 - Ldebug_info_start
	.long LDIFF_SYM987
	.byte 2,35,0,6
	.asciz "type_info"

LDIFF_SYM988=LTDIE_36_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM988
	.byte 2,35,24,6
	.asciz "GenericCache"

LDIFF_SYM989=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM989
	.byte 2,35,32,6
	.asciz "m_serializationCtor"

LDIFF_SYM990=LTDIE_38_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM990
	.byte 2,35,40,0,7
	.asciz "System_RuntimeType"

LDIFF_SYM991=LTDIE_34 - Ldebug_info_start
	.long LDIFF_SYM991
LTDIE_34_POINTER:

	.byte 13
LDIFF_SYM992=LTDIE_34 - Ldebug_info_start
	.long LDIFF_SYM992
LTDIE_34_REFERENCE:

	.byte 14
LDIFF_SYM993=LTDIE_34 - Ldebug_info_start
	.long LDIFF_SYM993
LTDIE_44:

	.byte 5
	.asciz "System_Reflection_MethodInfo"

	.byte 16,16
LDIFF_SYM994=LTDIE_40 - Ldebug_info_start
	.long LDIFF_SYM994
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MethodInfo"

LDIFF_SYM995=LTDIE_44 - Ldebug_info_start
	.long LDIFF_SYM995
LTDIE_44_POINTER:

	.byte 13
LDIFF_SYM996=LTDIE_44 - Ldebug_info_start
	.long LDIFF_SYM996
LTDIE_44_REFERENCE:

	.byte 14
LDIFF_SYM997=LTDIE_44 - Ldebug_info_start
	.long LDIFF_SYM997
LTDIE_45:

	.byte 5
	.asciz "System_DelegateData"

	.byte 40,16
LDIFF_SYM998=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM998
	.byte 2,35,0,6
	.asciz "target_type"

LDIFF_SYM999=LTDIE_32_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM999
	.byte 2,35,16,6
	.asciz "method_name"

LDIFF_SYM1000=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1000
	.byte 2,35,24,6
	.asciz "curried_first_arg"

LDIFF_SYM1001=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM1001
	.byte 2,35,32,0,7
	.asciz "System_DelegateData"

LDIFF_SYM1002=LTDIE_45 - Ldebug_info_start
	.long LDIFF_SYM1002
LTDIE_45_POINTER:

	.byte 13
LDIFF_SYM1003=LTDIE_45 - Ldebug_info_start
	.long LDIFF_SYM1003
LTDIE_45_REFERENCE:

	.byte 14
LDIFF_SYM1004=LTDIE_45 - Ldebug_info_start
	.long LDIFF_SYM1004
LTDIE_43:

	.byte 5
	.asciz "System_Delegate"

	.byte 104,16
LDIFF_SYM1005=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM1005
	.byte 2,35,0,6
	.asciz "method_ptr"

LDIFF_SYM1006=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1006
	.byte 2,35,16,6
	.asciz "invoke_impl"

LDIFF_SYM1007=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1007
	.byte 2,35,24,6
	.asciz "m_target"

LDIFF_SYM1008=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1008
	.byte 2,35,32,6
	.asciz "method"

LDIFF_SYM1009=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1009
	.byte 2,35,40,6
	.asciz "delegate_trampoline"

LDIFF_SYM1010=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1010
	.byte 2,35,48,6
	.asciz "extra_arg"

LDIFF_SYM1011=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1011
	.byte 2,35,56,6
	.asciz "method_code"

LDIFF_SYM1012=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1012
	.byte 2,35,64,6
	.asciz "method_info"

LDIFF_SYM1013=LTDIE_44_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1013
	.byte 2,35,72,6
	.asciz "original_method_info"

LDIFF_SYM1014=LTDIE_44_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1014
	.byte 2,35,80,6
	.asciz "data"

LDIFF_SYM1015=LTDIE_45_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1015
	.byte 2,35,88,6
	.asciz "method_is_virtual"

LDIFF_SYM1016=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM1016
	.byte 2,35,96,0,7
	.asciz "System_Delegate"

LDIFF_SYM1017=LTDIE_43 - Ldebug_info_start
	.long LDIFF_SYM1017
LTDIE_43_POINTER:

	.byte 13
LDIFF_SYM1018=LTDIE_43 - Ldebug_info_start
	.long LDIFF_SYM1018
LTDIE_43_REFERENCE:

	.byte 14
LDIFF_SYM1019=LTDIE_43 - Ldebug_info_start
	.long LDIFF_SYM1019
LTDIE_42:

	.byte 5
	.asciz "System_MulticastDelegate"

	.byte 112,16
LDIFF_SYM1020=LTDIE_43 - Ldebug_info_start
	.long LDIFF_SYM1020
	.byte 2,35,0,6
	.asciz "delegates"

LDIFF_SYM1021=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1021
	.byte 2,35,104,0,7
	.asciz "System_MulticastDelegate"

LDIFF_SYM1022=LTDIE_42 - Ldebug_info_start
	.long LDIFF_SYM1022
LTDIE_42_POINTER:

	.byte 13
LDIFF_SYM1023=LTDIE_42 - Ldebug_info_start
	.long LDIFF_SYM1023
LTDIE_42_REFERENCE:

	.byte 14
LDIFF_SYM1024=LTDIE_42 - Ldebug_info_start
	.long LDIFF_SYM1024
LTDIE_41:

	.byte 5
	.asciz "System_EventHandler`1"

	.byte 112,16
LDIFF_SYM1025=LTDIE_42 - Ldebug_info_start
	.long LDIFF_SYM1025
	.byte 2,35,0,0,7
	.asciz "System_EventHandler`1"

LDIFF_SYM1026=LTDIE_41 - Ldebug_info_start
	.long LDIFF_SYM1026
LTDIE_41_POINTER:

	.byte 13
LDIFF_SYM1027=LTDIE_41 - Ldebug_info_start
	.long LDIFF_SYM1027
LTDIE_41_REFERENCE:

	.byte 14
LDIFF_SYM1028=LTDIE_41 - Ldebug_info_start
	.long LDIFF_SYM1028
LTDIE_24:

	.byte 5
	.asciz "System_Runtime_Serialization_SafeSerializationManager"

	.byte 56,16
LDIFF_SYM1029=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM1029
	.byte 2,35,0,6
	.asciz "m_serializedStates"

LDIFF_SYM1030=LTDIE_25_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1030
	.byte 2,35,16,6
	.asciz "m_savedSerializationInfo"

LDIFF_SYM1031=LTDIE_26_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1031
	.byte 2,35,24,6
	.asciz "m_realObject"

LDIFF_SYM1032=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1032
	.byte 2,35,32,6
	.asciz "m_realType"

LDIFF_SYM1033=LTDIE_34_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1033
	.byte 2,35,40,6
	.asciz "SerializeObjectState"

LDIFF_SYM1034=LTDIE_41_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1034
	.byte 2,35,48,0,7
	.asciz "System_Runtime_Serialization_SafeSerializationManager"

LDIFF_SYM1035=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM1035
LTDIE_24_POINTER:

	.byte 13
LDIFF_SYM1036=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM1036
LTDIE_24_REFERENCE:

	.byte 14
LDIFF_SYM1037=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM1037
LTDIE_22:

	.byte 5
	.asciz "System_Exception"

	.byte 136,1,16
LDIFF_SYM1038=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM1038
	.byte 2,35,0,6
	.asciz "_className"

LDIFF_SYM1039=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1039
	.byte 2,35,16,6
	.asciz "_message"

LDIFF_SYM1040=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1040
	.byte 2,35,24,6
	.asciz "_data"

LDIFF_SYM1041=LTDIE_23_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1041
	.byte 2,35,32,6
	.asciz "_innerException"

LDIFF_SYM1042=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1042
	.byte 2,35,40,6
	.asciz "_helpURL"

LDIFF_SYM1043=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1043
	.byte 2,35,48,6
	.asciz "_stackTrace"

LDIFF_SYM1044=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1044
	.byte 2,35,56,6
	.asciz "_stackTraceString"

LDIFF_SYM1045=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1045
	.byte 2,35,64,6
	.asciz "_remoteStackTraceString"

LDIFF_SYM1046=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1046
	.byte 2,35,72,6
	.asciz "_remoteStackIndex"

LDIFF_SYM1047=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1047
	.byte 2,35,80,6
	.asciz "_dynamicMethods"

LDIFF_SYM1048=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1048
	.byte 2,35,88,6
	.asciz "_HResult"

LDIFF_SYM1049=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1049
	.byte 2,35,96,6
	.asciz "_source"

LDIFF_SYM1050=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1050
	.byte 2,35,104,6
	.asciz "_safeSerializationManager"

LDIFF_SYM1051=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1051
	.byte 2,35,112,6
	.asciz "captured_traces"

LDIFF_SYM1052=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1052
	.byte 2,35,120,6
	.asciz "native_trace_ips"

LDIFF_SYM1053=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1053
	.byte 3,35,128,1,0,7
	.asciz "System_Exception"

LDIFF_SYM1054=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM1054
LTDIE_22_POINTER:

	.byte 13
LDIFF_SYM1055=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM1055
LTDIE_22_REFERENCE:

	.byte 14
LDIFF_SYM1056=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM1056
LTDIE_21:

	.byte 5
	.asciz "System_SystemException"

	.byte 136,1,16
LDIFF_SYM1057=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM1057
	.byte 2,35,0,0,7
	.asciz "System_SystemException"

LDIFF_SYM1058=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM1058
LTDIE_21_POINTER:

	.byte 13
LDIFF_SYM1059=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM1059
LTDIE_21_REFERENCE:

	.byte 14
LDIFF_SYM1060=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM1060
LTDIE_20:

	.byte 5
	.asciz "System_ArgumentException"

	.byte 144,1,16
LDIFF_SYM1061=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM1061
	.byte 2,35,0,6
	.asciz "m_paramName"

LDIFF_SYM1062=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1062
	.byte 3,35,136,1,0,7
	.asciz "System_ArgumentException"

LDIFF_SYM1063=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM1063
LTDIE_20_POINTER:

	.byte 13
LDIFF_SYM1064=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM1064
LTDIE_20_REFERENCE:

	.byte 14
LDIFF_SYM1065=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM1065
	.byte 2
	.asciz "System.Numerics.BigNumber:TryValidateParseStyleInteger"
	.asciz "System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_"

	.byte 8,180,2
	.quad System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_
	.quad Lme_6a

	.byte 2,118,16,3
	.asciz "style"

LDIFF_SYM1066=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM1066
	.byte 2,141,24,3
	.asciz "e"

LDIFF_SYM1067=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1067
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1068=Lfde106_end - Lfde106_start
	.long LDIFF_SYM1068
Lfde106_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_

LDIFF_SYM1069=Lme_6a - System_Numerics_BigNumber_TryValidateParseStyleInteger_System_Globalization_NumberStyles_System_ArgumentException_
	.long LDIFF_SYM1069
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,154,8
	.align 3
Lfde106_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigNumber:TryParseBigInteger"
	.asciz "System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_"

	.byte 8,202,2
	.quad System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_
	.quad Lme_6b

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM1070=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1070
	.byte 2,141,16,3
	.asciz "style"

LDIFF_SYM1071=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM1071
	.byte 2,141,24,3
	.asciz "info"

LDIFF_SYM1072=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1072
	.byte 2,141,32,3
	.asciz "result"

LDIFF_SYM1073=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1073
	.byte 2,141,40,11
	.asciz "e"

LDIFF_SYM1074=LTDIE_20_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1074
	.byte 3,141,240,0,11
	.asciz "bignumber"

LDIFF_SYM1075=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1075
	.byte 3,141,216,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1076=Lfde107_end - Lfde107_start
	.long LDIFF_SYM1076
Lfde107_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_

LDIFF_SYM1077=Lme_6b - System_Numerics_BigNumber_TryParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo_System_Numerics_BigInteger_
	.long LDIFF_SYM1077
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29
	.align 3
Lfde107_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigNumber:ParseBigInteger"
	.asciz "System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo"

	.byte 8,231,2
	.quad System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo
	.quad Lme_6c

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM1078=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1078
	.byte 2,141,32,3
	.asciz "style"

LDIFF_SYM1079=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM1079
	.byte 2,141,40,3
	.asciz "info"

LDIFF_SYM1080=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1080
	.byte 2,141,48,11
	.asciz "e"

LDIFF_SYM1081=LTDIE_20_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1081
	.byte 3,141,200,0,11
	.asciz "result"

LDIFF_SYM1082=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1082
	.byte 2,141,56,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1083=Lfde108_end - Lfde108_start
	.long LDIFF_SYM1083
Lfde108_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo

LDIFF_SYM1084=Lme_6c - System_Numerics_BigNumber_ParseBigInteger_string_System_Globalization_NumberStyles_System_Globalization_NumberFormatInfo
	.long LDIFF_SYM1084
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29
	.align 3
Lfde108_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_46:

	.byte 5
	.asciz "_BigNumberBuffer"

	.byte 40,16
LDIFF_SYM1085=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM1085
	.byte 2,35,0,6
	.asciz "digits"

LDIFF_SYM1086=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1086
	.byte 2,35,0,6
	.asciz "precision"

LDIFF_SYM1087=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1087
	.byte 2,35,8,6
	.asciz "scale"

LDIFF_SYM1088=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1088
	.byte 2,35,12,6
	.asciz "sign"

LDIFF_SYM1089=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM1089
	.byte 2,35,16,0,7
	.asciz "_BigNumberBuffer"

LDIFF_SYM1090=LTDIE_46 - Ldebug_info_start
	.long LDIFF_SYM1090
LTDIE_46_POINTER:

	.byte 13
LDIFF_SYM1091=LTDIE_46 - Ldebug_info_start
	.long LDIFF_SYM1091
LTDIE_46_REFERENCE:

	.byte 14
LDIFF_SYM1092=LTDIE_46 - Ldebug_info_start
	.long LDIFF_SYM1092
	.byte 2
	.asciz "System.Numerics.BigNumber:HexNumberToBigInteger"
	.asciz "System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_"

	.byte 8,248,2
	.quad System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
	.quad Lme_6d

	.byte 2,118,16,3
	.asciz "number"

LDIFF_SYM1093=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1093
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM1094=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1094
	.byte 3,141,208,0,11
	.asciz "len"

LDIFF_SYM1095=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1095
	.byte 1,104,11
	.asciz "bits"

LDIFF_SYM1096=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1096
	.byte 1,103,11
	.asciz "shift"

LDIFF_SYM1097=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM1097
	.byte 1,102,11
	.asciz "isNegative"

LDIFF_SYM1098=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM1098
	.byte 1,101,11
	.asciz "bitIndex"

LDIFF_SYM1099=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1099
	.byte 1,100,11
	.asciz "i"

LDIFF_SYM1100=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1100
	.byte 1,99,11
	.asciz "c"

LDIFF_SYM1101=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM1101
	.byte 1,104,11
	.asciz "b"

LDIFF_SYM1102=LDIE_U1 - Ldebug_info_start
	.long LDIFF_SYM1102
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1103=Lfde109_end - Lfde109_start
	.long LDIFF_SYM1103
Lfde109_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_

LDIFF_SYM1104=Lme_6d - System_Numerics_BigNumber_HexNumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
	.long LDIFF_SYM1104
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,147,14,148,13,68,149,12,150,11,68,151,10,152,9,68,153,8
	.byte 154,7
	.align 3
Lfde109_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigNumber:NumberToBigInteger"
	.asciz "System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_"

	.byte 8,174,3
	.quad System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
	.quad Lme_6e

	.byte 2,118,16,3
	.asciz "number"

LDIFF_SYM1105=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1105
	.byte 1,105,3
	.asciz "value"

LDIFF_SYM1106=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1106
	.byte 1,106,11
	.asciz "i"

LDIFF_SYM1107=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1107
	.byte 1,104,11
	.asciz "cur"

LDIFF_SYM1108=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1108
	.byte 1,103,11
	.asciz "ten"

LDIFF_SYM1109=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1109
	.byte 3,141,224,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1110=Lfde110_end - Lfde110_start
	.long LDIFF_SYM1110
Lfde110_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_

LDIFF_SYM1111=Lme_6e - System_Numerics_BigNumber_NumberToBigInteger_System_Numerics_BigNumber_BigNumberBuffer__System_Numerics_BigInteger_
	.long LDIFF_SYM1111
	.long 0
	.byte 12,31,0,68,14,176,2,157,38,158,37,68,13,29,68,151,36,152,35,68,153,34,154,33
	.align 3
Lfde110_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigNumber:ParseFormatSpecifier"
	.asciz "System_Numerics_BigNumber_ParseFormatSpecifier_string_int_"

	.byte 8,201,3
	.quad System_Numerics_BigNumber_ParseFormatSpecifier_string_int_
	.quad Lme_6f

	.byte 2,118,16,3
	.asciz "format"

LDIFF_SYM1112=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1112
	.byte 1,105,3
	.asciz "digits"

LDIFF_SYM1113=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1113
	.byte 2,141,48,11
	.asciz "i"

LDIFF_SYM1114=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1114
	.byte 1,104,11
	.asciz "ch"

LDIFF_SYM1115=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM1115
	.byte 1,103,11
	.asciz "n"

LDIFF_SYM1116=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1116
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1117=Lfde111_end - Lfde111_start
	.long LDIFF_SYM1117
Lfde111_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_ParseFormatSpecifier_string_int_

LDIFF_SYM1118=Lme_6f - System_Numerics_BigNumber_ParseFormatSpecifier_string_int_
	.long LDIFF_SYM1118
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,150,6,151,5,68,152,4,153,3
	.align 3
Lfde111_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigNumber:FormatBigIntegerToHexString"
	.asciz "System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo"

	.byte 8,235,3
	.quad System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo
	.quad Lme_70

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM1119=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1119
	.byte 3,141,192,0,3
	.asciz "format"

LDIFF_SYM1120=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM1120
	.byte 1,104,3
	.asciz "digits"

LDIFF_SYM1121=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1121
	.byte 3,141,208,0,3
	.asciz "info"

LDIFF_SYM1122=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1122
	.byte 1,106,11
	.asciz "sb"

LDIFF_SYM1123=LTDIE_7_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1123
	.byte 1,103,11
	.asciz "bits"

LDIFF_SYM1124=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1124
	.byte 1,102,11
	.asciz "fmt"

LDIFF_SYM1125=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1125
	.byte 1,100,11
	.asciz "cur"

LDIFF_SYM1126=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1126
	.byte 1,101,11
	.asciz "clearHighF"

LDIFF_SYM1127=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM1127
	.byte 1,100,11
	.asciz "head"

LDIFF_SYM1128=LDIE_U1 - Ldebug_info_start
	.long LDIFF_SYM1128
	.byte 3,141,232,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1129=Lfde112_end - Lfde112_start
	.long LDIFF_SYM1129
Lfde112_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo

LDIFF_SYM1130=Lme_70 - System_Numerics_BigNumber_FormatBigIntegerToHexString_System_Numerics_BigInteger_char_int_System_Globalization_NumberFormatInfo
	.long LDIFF_SYM1130
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,68,148,22,149,21,68,150,20,151,19,68,152,18,68,154,17
	.align 3
Lfde112_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_48:

	.byte 5
	.asciz "System_ArithmeticException"

	.byte 136,1,16
LDIFF_SYM1131=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM1131
	.byte 2,35,0,0,7
	.asciz "System_ArithmeticException"

LDIFF_SYM1132=LTDIE_48 - Ldebug_info_start
	.long LDIFF_SYM1132
LTDIE_48_POINTER:

	.byte 13
LDIFF_SYM1133=LTDIE_48 - Ldebug_info_start
	.long LDIFF_SYM1133
LTDIE_48_REFERENCE:

	.byte 14
LDIFF_SYM1134=LTDIE_48 - Ldebug_info_start
	.long LDIFF_SYM1134
LTDIE_47:

	.byte 5
	.asciz "System_OverflowException"

	.byte 136,1,16
LDIFF_SYM1135=LTDIE_48 - Ldebug_info_start
	.long LDIFF_SYM1135
	.byte 2,35,0,0,7
	.asciz "System_OverflowException"

LDIFF_SYM1136=LTDIE_47 - Ldebug_info_start
	.long LDIFF_SYM1136
LTDIE_47_POINTER:

	.byte 13
LDIFF_SYM1137=LTDIE_47 - Ldebug_info_start
	.long LDIFF_SYM1137
LTDIE_47_REFERENCE:

	.byte 14
LDIFF_SYM1138=LTDIE_47 - Ldebug_info_start
	.long LDIFF_SYM1138
	.byte 2
	.asciz "System.Numerics.BigNumber:FormatBigInteger"
	.asciz "System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo"

	.byte 8,152,4
	.quad System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo
	.quad Lme_71

	.byte 2,118,16,3
	.asciz "value"

LDIFF_SYM1139=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1139
	.byte 3,141,208,0,3
	.asciz "format"

LDIFF_SYM1140=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM1140
	.byte 1,105,3
	.asciz "info"

LDIFF_SYM1141=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1141
	.byte 3,141,224,0,11
	.asciz "digits"

LDIFF_SYM1142=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1142
	.byte 3,141,216,1,11
	.asciz "fmt"

LDIFF_SYM1143=LDIE_CHAR - Ldebug_info_start
	.long LDIFF_SYM1143
	.byte 1,104,11
	.asciz "decimalFmt"

LDIFF_SYM1144=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM1144
	.byte 1,102,11
	.asciz "cuSrc"

LDIFF_SYM1145=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1145
	.byte 1,104,11
	.asciz "cuMax"

LDIFF_SYM1146=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1146
	.byte 1,103,11
	.asciz "rguDst"

LDIFF_SYM1147=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1147
	.byte 1,103,11
	.asciz "cuDst"

LDIFF_SYM1148=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1148
	.byte 1,101,11
	.asciz "cchMax"

LDIFF_SYM1149=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1149
	.byte 1,104,11
	.asciz "rgchBufSize"

LDIFF_SYM1150=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1150
	.byte 1,100,11
	.asciz "rgch"

LDIFF_SYM1151=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1151
	.byte 1,100,11
	.asciz "ichDst"

LDIFF_SYM1152=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1152
	.byte 1,99,11
	.asciz "numDigitsPrinted"

LDIFF_SYM1153=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1153
	.byte 1,106,11
	.asciz "V_12"

LDIFF_SYM1154=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1154
	.byte 3,141,224,1,11
	.asciz "e"

LDIFF_SYM1155=LTDIE_47_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1155
	.byte 3,141,232,1,11
	.asciz "iuSrc"

LDIFF_SYM1156=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1156
	.byte 1,100,11
	.asciz "uCarry"

LDIFF_SYM1157=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1157
	.byte 1,104,11
	.asciz "iuDst"

LDIFF_SYM1158=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1158
	.byte 1,99,11
	.asciz "uuRes"

LDIFF_SYM1159=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM1159
	.byte 1,104,11
	.asciz "e"

LDIFF_SYM1160=LTDIE_47_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1160
	.byte 3,141,240,1,11
	.asciz "e"

LDIFF_SYM1161=LTDIE_47_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1161
	.byte 3,141,248,1,11
	.asciz "e"

LDIFF_SYM1162=LTDIE_47_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM1162
	.byte 3,141,128,2,11
	.asciz "iuDst"

LDIFF_SYM1163=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1163
	.byte 3,141,136,2,11
	.asciz "uDig"

LDIFF_SYM1164=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1164
	.byte 3,141,144,2,11
	.asciz "cch"

LDIFF_SYM1165=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1165
	.byte 1,106,11
	.asciz "uDig"

LDIFF_SYM1166=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1166
	.byte 1,106,11
	.asciz "sign"

LDIFF_SYM1167=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM1167
	.byte 1,106,11
	.asciz "scale"

LDIFF_SYM1168=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1168
	.byte 1,103,11
	.asciz "i"

LDIFF_SYM1169=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1169
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1170=Lfde113_end - Lfde113_start
	.long LDIFF_SYM1170
Lfde113_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo

LDIFF_SYM1171=Lme_71 - System_Numerics_BigNumber_FormatBigInteger_System_Numerics_BigInteger_string_System_Globalization_NumberFormatInfo
	.long LDIFF_SYM1171
	.long 0
	.byte 12,31,0,68,14,192,3,157,56,158,55,68,13,29,68,147,54,148,53,68,149,52,150,51,68,151,50,152,49,68,153,48
	.byte 154,47
	.align 3
Lfde113_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.BigNumber/BigNumberBuffer:Create"
	.asciz "System_Numerics_BigNumber_BigNumberBuffer_Create"

	.byte 8,170,2
	.quad System_Numerics_BigNumber_BigNumberBuffer_Create
	.quad Lme_72

	.byte 2,118,16,11
	.asciz "number"

LDIFF_SYM1172=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1172
	.byte 2,141,48,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1173=Lfde114_end - Lfde114_start
	.long LDIFF_SYM1173
Lfde114_start:

	.long 0
	.align 3
	.quad System_Numerics_BigNumber_BigNumberBuffer_Create

LDIFF_SYM1174=Lme_72 - System_Numerics_BigNumber_BigNumberBuffer_Create
	.long LDIFF_SYM1174
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde114_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:GetDoubleParts"
	.asciz "System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_"

	.byte 9,29
	.quad System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_
	.quad Lme_73

	.byte 2,118,16,3
	.asciz "dbl"

LDIFF_SYM1175=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM1175
	.byte 2,141,32,3
	.asciz "sign"

LDIFF_SYM1176=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1176
	.byte 2,141,40,3
	.asciz "exp"

LDIFF_SYM1177=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1177
	.byte 1,104,3
	.asciz "man"

LDIFF_SYM1178=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1178
	.byte 1,105,3
	.asciz "fFinite"

LDIFF_SYM1179=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1179
	.byte 2,141,48,11
	.asciz "du"

LDIFF_SYM1180=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1180
	.byte 3,141,208,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1181=Lfde115_end - Lfde115_start
	.long LDIFF_SYM1181
Lfde115_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_

LDIFF_SYM1182=Lme_73 - System_Numerics_NumericsHelpers_GetDoubleParts_double_int__int__ulong__bool_
	.long LDIFF_SYM1182
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29,68,152,10,153,9
	.align 3
Lfde115_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:GetDoubleFromParts"
	.asciz "System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong"

	.byte 9,59
	.quad System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong
	.quad Lme_74

	.byte 2,118,16,3
	.asciz "sign"

LDIFF_SYM1183=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1183
	.byte 2,141,40,3
	.asciz "exp"

LDIFF_SYM1184=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1184
	.byte 1,105,3
	.asciz "man"

LDIFF_SYM1185=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM1185
	.byte 1,106,11
	.asciz "du"

LDIFF_SYM1186=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1186
	.byte 2,141,56,11
	.asciz "cbitShift"

LDIFF_SYM1187=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1187
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1188=Lfde116_end - Lfde116_start
	.long LDIFF_SYM1188
Lfde116_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong

LDIFF_SYM1189=Lme_74 - System_Numerics_NumericsHelpers_GetDoubleFromParts_int_int_ulong
	.long LDIFF_SYM1189
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,151,6,68,153,5,154,4
	.align 3
Lfde116_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:DangerousMakeTwosComplement"
	.asciz "System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__"

	.byte 9,115
	.quad System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__
	.quad Lme_75

	.byte 2,118,16,3
	.asciz "d"

LDIFF_SYM1190=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1190
	.byte 1,106,11
	.asciz "i"

LDIFF_SYM1191=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1191
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1192=Lfde117_end - Lfde117_start
	.long LDIFF_SYM1192
Lfde117_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__

LDIFF_SYM1193=Lme_75 - System_Numerics_NumericsHelpers_DangerousMakeTwosComplement_uint__
	.long LDIFF_SYM1193
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29,68,153,2,154,1
	.align 3
Lfde117_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:MakeUlong"
	.asciz "System_Numerics_NumericsHelpers_MakeUlong_uint_uint"

	.byte 9,135,1
	.quad System_Numerics_NumericsHelpers_MakeUlong_uint_uint
	.quad Lme_76

	.byte 2,118,16,3
	.asciz "uHi"

LDIFF_SYM1194=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1194
	.byte 2,141,16,3
	.asciz "uLo"

LDIFF_SYM1195=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1195
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1196=Lfde118_end - Lfde118_start
	.long LDIFF_SYM1196
Lfde118_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_MakeUlong_uint_uint

LDIFF_SYM1197=Lme_76 - System_Numerics_NumericsHelpers_MakeUlong_uint_uint
	.long LDIFF_SYM1197
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde118_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:Abs"
	.asciz "System_Numerics_NumericsHelpers_Abs_int"

	.byte 9,142,1
	.quad System_Numerics_NumericsHelpers_Abs_int
	.quad Lme_77

	.byte 2,118,16,3
	.asciz "a"

LDIFF_SYM1198=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1198
	.byte 2,141,16,11
	.asciz "mask"

LDIFF_SYM1199=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1199
	.byte 0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1200=Lfde119_end - Lfde119_start
	.long LDIFF_SYM1200
Lfde119_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_Abs_int

LDIFF_SYM1201=Lme_77 - System_Numerics_NumericsHelpers_Abs_int
	.long LDIFF_SYM1201
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde119_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:CombineHash"
	.asciz "System_Numerics_NumericsHelpers_CombineHash_uint_uint"

	.byte 9,149,1
	.quad System_Numerics_NumericsHelpers_CombineHash_uint_uint
	.quad Lme_78

	.byte 2,118,16,3
	.asciz "u1"

LDIFF_SYM1202=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1202
	.byte 2,141,16,3
	.asciz "u2"

LDIFF_SYM1203=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1203
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1204=Lfde120_end - Lfde120_start
	.long LDIFF_SYM1204
Lfde120_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_CombineHash_uint_uint

LDIFF_SYM1205=Lme_78 - System_Numerics_NumericsHelpers_CombineHash_uint_uint
	.long LDIFF_SYM1205
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde120_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:CombineHash"
	.asciz "System_Numerics_NumericsHelpers_CombineHash_int_int"

	.byte 9,154,1
	.quad System_Numerics_NumericsHelpers_CombineHash_int_int
	.quad Lme_79

	.byte 2,118,16,3
	.asciz "n1"

LDIFF_SYM1206=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1206
	.byte 2,141,16,3
	.asciz "n2"

LDIFF_SYM1207=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1207
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1208=Lfde121_end - Lfde121_start
	.long LDIFF_SYM1208
Lfde121_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_CombineHash_int_int

LDIFF_SYM1209=Lme_79 - System_Numerics_NumericsHelpers_CombineHash_int_int
	.long LDIFF_SYM1209
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde121_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:CbitHighZero"
	.asciz "System_Numerics_NumericsHelpers_CbitHighZero_uint"

	.byte 9,159,1
	.quad System_Numerics_NumericsHelpers_CbitHighZero_uint
	.quad Lme_7a

	.byte 2,118,16,3
	.asciz "u"

LDIFF_SYM1210=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1210
	.byte 1,106,11
	.asciz "cbit"

LDIFF_SYM1211=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1211
	.byte 1,105,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1212=Lfde122_end - Lfde122_start
	.long LDIFF_SYM1212
Lfde122_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_CbitHighZero_uint

LDIFF_SYM1213=Lme_7a - System_Numerics_NumericsHelpers_CbitHighZero_uint
	.long LDIFF_SYM1213
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29,68,153,2,154,1
	.align 3
Lfde122_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Numerics.NumericsHelpers:CbitHighZero"
	.asciz "System_Numerics_NumericsHelpers_CbitHighZero_ulong"

	.byte 9,190,1
	.quad System_Numerics_NumericsHelpers_CbitHighZero_ulong
	.quad Lme_7b

	.byte 2,118,16,3
	.asciz "uu"

LDIFF_SYM1214=LDIE_U8 - Ldebug_info_start
	.long LDIFF_SYM1214
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1215=Lfde123_end - Lfde123_start
	.long LDIFF_SYM1215
Lfde123_start:

	.long 0
	.align 3
	.quad System_Numerics_NumericsHelpers_CbitHighZero_ulong

LDIFF_SYM1216=Lme_7b - System_Numerics_NumericsHelpers_CbitHighZero_ulong
	.long LDIFF_SYM1216
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde123_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_uint[]:Get"
	.asciz "wrapper_unknown_uint___Get_int"

	.byte 0,0
	.quad wrapper_unknown_uint___Get_int
	.quad Lme_7f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM1217=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1217
	.byte 2,141,16,3
	.asciz "param0"

LDIFF_SYM1218=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1218
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1219=Lfde124_end - Lfde124_start
	.long LDIFF_SYM1219
Lfde124_start:

	.long 0
	.align 3
	.quad wrapper_unknown_uint___Get_int

LDIFF_SYM1220=Lme_7f - wrapper_unknown_uint___Get_int
	.long LDIFF_SYM1220
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde124_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_runtime-invoke)_<Module>:runtime_invoke_uint__this___int"
	.asciz "wrapper_runtime_invoke__Module_runtime_invoke_uint__this___int_object_intptr_intptr_intptr"

	.byte 0,0
	.quad wrapper_runtime_invoke__Module_runtime_invoke_uint__this___int_object_intptr_intptr_intptr
	.quad Lme_80

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM1221=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1221
	.byte 2,141,24,3
	.asciz "params"

LDIFF_SYM1222=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1222
	.byte 2,141,32,3
	.asciz "exc"

LDIFF_SYM1223=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1223
	.byte 2,141,40,3
	.asciz "method"

LDIFF_SYM1224=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1224
	.byte 2,141,48,11
	.asciz "V_0"

LDIFF_SYM1225=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1225
	.byte 2,141,56,11
	.asciz "V_1"

LDIFF_SYM1226=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1226
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1227=Lfde125_end - Lfde125_start
	.long LDIFF_SYM1227
Lfde125_start:

	.long 0
	.align 3
	.quad wrapper_runtime_invoke__Module_runtime_invoke_uint__this___int_object_intptr_intptr_intptr

LDIFF_SYM1228=Lme_80 - wrapper_runtime_invoke__Module_runtime_invoke_uint__this___int_object_intptr_intptr_intptr
	.long LDIFF_SYM1228
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,151,14
	.align 3
Lfde125_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_uint[]:Set"
	.asciz "wrapper_unknown_uint___Set_int_uint"

	.byte 0,0
	.quad wrapper_unknown_uint___Set_int_uint
	.quad Lme_81

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM1229=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1229
	.byte 2,141,16,3
	.asciz "param0"

LDIFF_SYM1230=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1230
	.byte 2,141,24,3
	.asciz "param1"

LDIFF_SYM1231=LDIE_U4 - Ldebug_info_start
	.long LDIFF_SYM1231
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1232=Lfde126_end - Lfde126_start
	.long LDIFF_SYM1232
Lfde126_start:

	.long 0
	.align 3
	.quad wrapper_unknown_uint___Set_int_uint

LDIFF_SYM1233=Lme_81 - wrapper_unknown_uint___Set_int_uint
	.long LDIFF_SYM1233
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde126_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_runtime-invoke)_<Module>:runtime_invoke_void__this___int_uint"
	.asciz "wrapper_runtime_invoke__Module_runtime_invoke_void__this___int_uint_object_intptr_intptr_intptr"

	.byte 0,0
	.quad wrapper_runtime_invoke__Module_runtime_invoke_void__this___int_uint_object_intptr_intptr_intptr
	.quad Lme_82

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM1234=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1234
	.byte 2,141,32,3
	.asciz "params"

LDIFF_SYM1235=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1235
	.byte 1,105,3
	.asciz "exc"

LDIFF_SYM1236=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1236
	.byte 2,141,40,3
	.asciz "method"

LDIFF_SYM1237=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1237
	.byte 2,141,48,11
	.asciz "V_0"

LDIFF_SYM1238=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1238
	.byte 2,141,56,11
	.asciz "V_1"

LDIFF_SYM1239=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1239
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1240=Lfde127_end - Lfde127_start
	.long LDIFF_SYM1240
Lfde127_start:

	.long 0
	.align 3
	.quad wrapper_runtime_invoke__Module_runtime_invoke_void__this___int_uint_object_intptr_intptr_intptr

LDIFF_SYM1241=Lme_82 - wrapper_runtime_invoke__Module_runtime_invoke_void__this___int_uint_object_intptr_intptr_intptr
	.long LDIFF_SYM1241
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29,68,151,12,68,153,11
	.align 3
Lfde127_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_System.Globalization.FormatProvider/Number/NumberBuffer:StructureToPtr"
	.asciz "wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_StructureToPtr_object_intptr_bool"

	.byte 0,0
	.quad wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_StructureToPtr_object_intptr_bool
	.quad Lme_83

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM1242=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1242
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM1243=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1243
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM1244=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1244
	.byte 0,11
	.asciz "V_0"

LDIFF_SYM1245=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1245
	.byte 0,11
	.asciz "V_1"

LDIFF_SYM1246=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1246
	.byte 0,11
	.asciz "V_2"

LDIFF_SYM1247=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1247
	.byte 0,11
	.asciz "V_3"

LDIFF_SYM1248=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1248
	.byte 0,11
	.asciz "V_4"

LDIFF_SYM1249=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1249
	.byte 0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1250=Lfde128_end - Lfde128_start
	.long LDIFF_SYM1250
Lfde128_start:

	.long 0
	.align 3
	.quad wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_StructureToPtr_object_intptr_bool

LDIFF_SYM1251=Lme_83 - wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_StructureToPtr_object_intptr_bool
	.long LDIFF_SYM1251
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde128_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_System.Globalization.FormatProvider/Number/NumberBuffer:PtrToStructure"
	.asciz "wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_PtrToStructure_intptr_object"

	.byte 0,0
	.quad wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_PtrToStructure_intptr_object
	.quad Lme_84

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM1252=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1252
	.byte 1,105,3
	.asciz "param1"

LDIFF_SYM1253=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1253
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM1254=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1254
	.byte 1,106,11
	.asciz "V_1"

LDIFF_SYM1255=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1255
	.byte 1,104,11
	.asciz "V_2"

LDIFF_SYM1256=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1256
	.byte 1,105,11
	.asciz "V_3"

LDIFF_SYM1257=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1257
	.byte 2,141,40,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1258=Lfde129_end - Lfde129_start
	.long LDIFF_SYM1258
Lfde129_start:

	.long 0
	.align 3
	.quad wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_PtrToStructure_intptr_object

LDIFF_SYM1259=Lme_84 - wrapper_unknown_System_Globalization_FormatProvider_Number_NumberBuffer_PtrToStructure_intptr_object
	.long LDIFF_SYM1259
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29,68,152,4,153,3,68,154,2
	.align 3
Lfde129_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_System.Numerics.DoubleUlong:StructureToPtr"
	.asciz "wrapper_unknown_System_Numerics_DoubleUlong_StructureToPtr_object_intptr_bool"

	.byte 0,0
	.quad wrapper_unknown_System_Numerics_DoubleUlong_StructureToPtr_object_intptr_bool
	.quad Lme_85

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM1260=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1260
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM1261=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1261
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM1262=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1262
	.byte 0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1263=Lfde130_end - Lfde130_start
	.long LDIFF_SYM1263
Lfde130_start:

	.long 0
	.align 3
	.quad wrapper_unknown_System_Numerics_DoubleUlong_StructureToPtr_object_intptr_bool

LDIFF_SYM1264=Lme_85 - wrapper_unknown_System_Numerics_DoubleUlong_StructureToPtr_object_intptr_bool
	.long LDIFF_SYM1264
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde130_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_System.Numerics.DoubleUlong:PtrToStructure"
	.asciz "wrapper_unknown_System_Numerics_DoubleUlong_PtrToStructure_intptr_object"

	.byte 0,0
	.quad wrapper_unknown_System_Numerics_DoubleUlong_PtrToStructure_intptr_object
	.quad Lme_86

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM1265=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1265
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM1266=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM1266
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1267=Lfde131_end - Lfde131_start
	.long LDIFF_SYM1267
Lfde131_start:

	.long 0
	.align 3
	.quad wrapper_unknown_System_Numerics_DoubleUlong_PtrToStructure_intptr_object

LDIFF_SYM1268=Lme_86 - wrapper_unknown_System_Numerics_DoubleUlong_PtrToStructure_intptr_object
	.long LDIFF_SYM1268
	.long 0
	.byte 12,31,0,68,14,32,157,4,158,3,68,13,29
	.align 3
Lfde131_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "System.Array:Resize<T_INT>"
	.asciz "System_Array_Resize_T_INT_T_INT____int"

	.byte 10,55
	.quad System_Array_Resize_T_INT_T_INT____int
	.quad Lme_87

	.byte 2,118,16,3
	.asciz "array"

LDIFF_SYM1269=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM1269
	.byte 1,105,3
	.asciz "newSize"

LDIFF_SYM1270=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM1270
	.byte 1,106,11
	.asciz "larray"

LDIFF_SYM1271=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1271
	.byte 1,104,11
	.asciz "newArray"

LDIFF_SYM1272=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM1272
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM1273=Lfde132_end - Lfde132_start
	.long LDIFF_SYM1273
Lfde132_start:

	.long 0
	.align 3
	.quad System_Array_Resize_T_INT_T_INT____int

LDIFF_SYM1274=Lme_87 - System_Array_Resize_T_INT_T_INT____int
	.long LDIFF_SYM1274
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29,68,151,12,152,11,68,153,10,154,9
	.align 3
Lfde132_end:

.section __DWARF, __debug_info,regular,debug

	.byte 0
Ldebug_info_end:
.text
	.align 3
mem_end:
