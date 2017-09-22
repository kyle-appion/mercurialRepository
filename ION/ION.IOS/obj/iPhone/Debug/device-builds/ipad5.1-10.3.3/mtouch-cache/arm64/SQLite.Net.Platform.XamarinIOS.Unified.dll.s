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
	.asciz "SQLite.Net.Platform.XamarinIOS.Unified.dll"
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
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetPublicInstanceProperties_System_Type
SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetPublicInstanceProperties_System_Type:
.file 1 "<unknown>"
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #200]
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
.word 0xf9400fa2
.word 0xd2840280
.word 0xaa0203e0
.word 0xd2840281
.word 0xf9400042
.word 0xf9416c50
.word 0xd63f0200
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_0:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetMemberValue_object_System_Linq_Expressions_Expression_System_Reflection_MemberInfo
SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetMemberValue_object_System_Linq_Expressions_Expression_System_Reflection_MemberInfo:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xf9001ba0
.word 0xaa0103f9
.word 0xf9001fa2
.word 0xaa0303fa

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #208]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xf94023b1
.word 0xf9404631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9405c30
.word 0xd63f0200
.word 0x93407c00
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xd2800201
.word 0xd280021e
.word 0x6b1e001f
.word 0x54000521
.word 0xf94023b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03f7
.word 0xb400017a
.word 0xf94002e0
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x1, [x16, #216]
.word 0xeb01001f
.word 0x10000011
.word 0x540010c1
.word 0xaa1703e0
.word 0xaa1903e0
.word 0xd2800000
.word 0xaa1703e0
.word 0xaa1903e1
.word 0xd2800002
.word 0xf94002e3
.word 0xf9406c70
.word 0xd63f0200
.word 0xf90033a0
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
.word 0xf94033a0
.word 0x14000065
.word 0xf94023b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9405c30
.word 0xd63f0200
.word 0x93407c00
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xd2800081
.word 0xd280009e
.word 0x6b1e001f
.word 0x540004e1
.word 0xf94023b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03f8
.word 0xb400017a
.word 0xf9400300
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x1, [x16, #224]
.word 0xeb01001f
.word 0x10000011
.word 0x54000941
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xaa1903e1
.word 0xf9400302
.word 0xf9407050
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x1400002b
.word 0xf94023b1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2800021
.word 0xd2800021
bl _p_1
.word 0xf90037a0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9405c30
.word 0xd63f0200
.word 0x93407c00
.word 0xf9003ba0
.word 0xf94023b1
.word 0xf9428631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2803a60
.word 0xd2803a60
bl _p_2
.word 0xaa0003e1
.word 0xf94037a0
.word 0xf9403ba2
.word 0xb9001022
bl _p_3
.word 0xf90033a0
.word 0xf94023b1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_4
.word 0xf94023b1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_1:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor
SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #232]
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

Lme_2:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Open_byte___SQLite_Net_Interop_IDbHandle__int_intptr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Open_byte___SQLite_Net_Interop_IDbHandle__int_intptr:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9001ba4

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #240]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf90033bf
.word 0xf9401fb1
.word 0xf9404631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0x910183a1
.word 0xb9802ba2
.word 0xf9401ba3
bl _p_6
.word 0x93407c00
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf9003fa0
.word 0xf94033a1
.word 0x910163a0
.word 0xf9002fbf
.word 0x910163a0
bl _p_7
.word 0x910163a0
.word 0x910143a0
.word 0xf9402fa0
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #248]
.word 0xd2800301
.word 0xd2800301
bl _p_8
.word 0xf9403fa1
.word 0x910143a2
.word 0x91004002
.word 0xf9402ba3
.word 0xf9000043
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9401fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_3:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ExtendedErrCode_SQLite_Net_Interop_IDbHandle
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ExtendedErrCode_SQLite_Net_Interop_IDbHandle:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #256]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #264]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_9
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_10
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_4:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Close_SQLite_Net_Interop_IDbHandle
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Close_SQLite_Net_Interop_IDbHandle:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #272]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #264]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_9
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_11
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_5:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BusyTimeout_SQLite_Net_Interop_IDbHandle_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BusyTimeout_SQLite_Net_Interop_IDbHandle_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #280]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x540006a1
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #264]
.word 0xeb02003f
.word 0x10000011
.word 0x540005a1
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_9
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb98023a1
bl _p_12
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_6:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Changes_SQLite_Net_Interop_IDbHandle
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Changes_SQLite_Net_Interop_IDbHandle:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #288]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #264]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_9
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_13
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_7:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Prepare2_SQLite_Net_Interop_IDbHandle_string
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Prepare2_SQLite_Net_Interop_IDbHandle_string:
.loc 1 1 0
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xf90013a0
.word 0xf90017a1
.word 0xf9001ba2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #296]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0x9101c3a0
.word 0xf9003bbf
.word 0xf9003fbf
.word 0xd2800017
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
.word 0xf94017a0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54001281
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #264]
.word 0xeb02003f
.word 0x10000011
.word 0x54001181
.word 0x91004000
.word 0x910183a1
.word 0xf9400000
.word 0xf90033a0
.word 0x910183a0
.word 0x9101c3a0
.word 0xf94033a0
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0x9101c3a0
bl _p_9
.word 0xf9004ba0
.word 0xf9401fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xf9004fa0
.word 0xaa1603e0
.word 0xb98012c0
.word 0xf90053a0
.word 0xf9401fb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xf9404fa1
.word 0xf94053a2
.word 0x9101e3a3

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x4, [x16, #304]
.word 0xf9400084
bl _p_14
.word 0x93407c00
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf90043a0
.word 0xaa0003f7
.word 0xf9401fb1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xaa0003e1
.word 0x34000500
.word 0xf9401fb1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013a0
.word 0xf9004ba0
.word 0x9101c3a0
.word 0x910163a0
.word 0xf9403ba0
.word 0xf9002fa0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #248]
.word 0xd2800301
.word 0xd2800301
bl _p_8
.word 0xaa0003e1
.word 0xf9404ba0
.word 0x910163a2
.word 0x91004022
.word 0xf9402fa3
.word 0xf9000043
bl _p_15
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa1703e0
bl _p_16
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
bl _p_4
.word 0xf9401fb1
.word 0xf9422e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa1
.word 0x9101a3a0
.word 0xf90037bf
.word 0x9101a3a0
bl _p_17
.word 0x9101a3a0
.word 0x910143a0
.word 0xf94037a0
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9426231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #312]
.word 0xd2800301
.word 0xd2800301
bl _p_8
.word 0x910143a1
.word 0xf90043a0
.word 0x91004000
.word 0xf9402ba1
.word 0xf9000001
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf9401fb1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_8:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Step_SQLite_Net_Interop_IDbStatement
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Step_SQLite_Net_Interop_IDbStatement:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #320]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_18
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_19
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_9:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Reset_SQLite_Net_Interop_IDbStatement
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Reset_SQLite_Net_Interop_IDbStatement:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #336]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_18
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_20
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_a:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Finalize_SQLite_Net_Interop_IDbStatement
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Finalize_SQLite_Net_Interop_IDbStatement:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #344]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_18
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_21
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_b:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_LastInsertRowid_SQLite_Net_Interop_IDbHandle
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_LastInsertRowid_SQLite_Net_Interop_IDbHandle:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #352]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000661
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #264]
.word 0xeb02003f
.word 0x10000011
.word 0x54000561
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_9
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_22
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_c:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #360]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000741
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #264]
.word 0xeb02003f
.word 0x10000011
.word 0x54000641
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_9
.word 0xf90033a0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_23
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_24
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9414e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_d:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindParameterIndex_SQLite_Net_Interop_IDbStatement_string
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindParameterIndex_SQLite_Net_Interop_IDbStatement_string:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #368]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x540006a1
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x540005a1
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf94013a1
bl _p_25
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_e:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindNull_SQLite_Net_Interop_IDbStatement_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindNull_SQLite_Net_Interop_IDbStatement_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #376]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x540006a1
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x540005a1
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb98023a1
bl _p_26
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_f:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt_SQLite_Net_Interop_IDbStatement_int_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt_SQLite_Net_Interop_IDbStatement_int_int:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #384]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0x910143a0
.word 0xf9002bbf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x540006c1
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x540005c1
.word 0x91004000
.word 0x910123a1
.word 0xf9400000
.word 0xf90027a0
.word 0x910123a0
.word 0x910143a0
.word 0xf94027a0
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0x910143a0
bl _p_18
.word 0xf90037a0
.word 0xf9401bb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xb98023a1
.word 0xb9802ba2
bl _p_27
.word 0x93407c00
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9401bb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_10:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt64_SQLite_Net_Interop_IDbStatement_int_long
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt64_SQLite_Net_Interop_IDbStatement_int_long:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #392]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0x910143a0
.word 0xf9002bbf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x540006c1
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x540005c1
.word 0x91004000
.word 0x910123a1
.word 0xf9400000
.word 0xf90027a0
.word 0x910123a0
.word 0x910143a0
.word 0xf94027a0
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0x910143a0
bl _p_18
.word 0xf90037a0
.word 0xf9401bb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xb98023a1
.word 0xf94017a2
bl _p_28
.word 0x93407c00
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9401bb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_11:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindDouble_SQLite_Net_Interop_IDbStatement_int_double
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindDouble_SQLite_Net_Interop_IDbStatement_int_double:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xfd0017a0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #400]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0x910143a0
.word 0xf9002bbf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x540006c1
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x540005c1
.word 0x91004000
.word 0x910123a1
.word 0xf9400000
.word 0xf90027a0
.word 0x910123a0
.word 0x910143a0
.word 0xf94027a0
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0x910143a0
bl _p_18
.word 0xf90037a0
.word 0xf9401bb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xb98023a1
.word 0xfd4017a0
bl _p_29
.word 0x93407c00
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9401bb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_12:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindText16_SQLite_Net_Interop_IDbStatement_int_string_int_intptr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindText16_SQLite_Net_Interop_IDbStatement_int_string_int_intptr:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9001ba4
.word 0xf9001fa5

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #408]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0x910183a0
.word 0xf90033bf
.word 0xf94023b1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000701
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000601
.word 0x91004000
.word 0x910163a1
.word 0xf9400000
.word 0xf9002fa0
.word 0x910163a0
.word 0x910183a0
.word 0xf9402fa0
.word 0xf90033a0
.word 0xf94023b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0x910183a0
bl _p_18
.word 0xf9003fa0
.word 0xf94023b1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xb98023a1
.word 0xf94017a2
.word 0xb98033a3
.word 0xf9401fa4
bl _p_30
.word 0x93407c00
.word 0xf9003ba0
.word 0xf94023b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf94023b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_13:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindBlob_SQLite_Net_Interop_IDbStatement_int_byte___int_intptr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindBlob_SQLite_Net_Interop_IDbStatement_int_byte___int_intptr:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9001ba4
.word 0xf9001fa5

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #416]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0x910183a0
.word 0xf90033bf
.word 0xf94023b1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000701
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000601
.word 0x91004000
.word 0x910163a1
.word 0xf9400000
.word 0xf9002fa0
.word 0x910163a0
.word 0x910183a0
.word 0xf9402fa0
.word 0xf90033a0
.word 0xf94023b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0x910183a0
bl _p_18
.word 0xf9003fa0
.word 0xf94023b1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xb98023a1
.word 0xf94017a2
.word 0xb98033a3
.word 0xf9401fa4
bl _p_31
.word 0x93407c00
.word 0xf9003ba0
.word 0xf94023b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf94023b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_14:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnCount_SQLite_Net_Interop_IDbStatement
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnCount_SQLite_Net_Interop_IDbStatement:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #424]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0x910103a0
.word 0xf90023bf
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
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x9100e3a1
.word 0xf9400000
.word 0xf9001fa0
.word 0x9100e3a0
.word 0x910103a0
.word 0xf9401fa0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
bl _p_18
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_32
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_15:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnName16_SQLite_Net_Interop_IDbStatement_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnName16_SQLite_Net_Interop_IDbStatement_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #432]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb98023a1
bl _p_33
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_16:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnType_SQLite_Net_Interop_IDbStatement_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnType_SQLite_Net_Interop_IDbStatement_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #440]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x540006a1
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x540005a1
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb98023a1
bl _p_34
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_17:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt_SQLite_Net_Interop_IDbStatement_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt_SQLite_Net_Interop_IDbStatement_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #448]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x540006a1
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x540005a1
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb98023a1
bl _p_35
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_18:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt64_SQLite_Net_Interop_IDbStatement_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt64_SQLite_Net_Interop_IDbStatement_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #456]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb98023a1
bl _p_36
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_19:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnDouble_SQLite_Net_Interop_IDbStatement_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnDouble_SQLite_Net_Interop_IDbStatement_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #464]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb98023a1
bl _p_37
.word 0xfd002ba0
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd402ba0
.word 0xf94017b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_1a:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnText16_SQLite_Net_Interop_IDbStatement_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnText16_SQLite_Net_Interop_IDbStatement_int:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #472]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000761
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000661
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf90033a0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xb98023a1
bl _p_38
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_24
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_1b:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnByteArray_SQLite_Net_Interop_IDbStatement_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnByteArray_SQLite_Net_Interop_IDbStatement_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #480]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0x910123a0
.word 0xf90027bf
.word 0xf94017b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x54000681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #328]
.word 0xeb02003f
.word 0x10000011
.word 0x54000581
.word 0x91004000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0x910103a0
.word 0x910123a0
.word 0xf94023a0
.word 0xf90027a0
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
bl _p_18
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb98023a1
bl _p_39
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_5

Lme_1c:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
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

Lme_1d:
.text
ut_30:
add x0, x0, 16
b SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr
ut_end:
.section __TEXT, __const
_unbox_trampoline_p:

	.long 0
LDIFF_SYM3=ut_end - ut_30
	.long LDIFF_SYM3
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #496]
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
.word 0xf900033f
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa1
.word 0xaa1903e0
bl _p_40
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_1e:
.text
ut_31:
add x0, x0, 16
b SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #504]
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
.word 0xf9400000
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

Lme_1f:
.text
ut_32:
add x0, x0, 16
b SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #512]
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
.word 0xf9400fa1
.word 0xf9000001
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

Lme_20:
.text
ut_33:
add x0, x0, 16
b SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #520]
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
.word 0xf900033f
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa1
.word 0xaa1903e0
bl _p_41
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_21:
.text
ut_34:
add x0, x0, 16
b SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #528]
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
.word 0xf9400000
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

Lme_22:
.text
ut_35:
add x0, x0, 16
b SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #536]
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
.word 0xf9400fa1
.word 0xf9000001
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

Lme_23:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #544]
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
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0xaa1a03e1
bl _p_42
.word 0x93407c00
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9002ba0
.word 0xaa0003f8
.word 0xf9401bb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba1
.word 0xaa0103e0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #552]
bl _p_43
.word 0xaa0003f7
.word 0xf9401bb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xd2800001
.word 0x6b1f001f
.word 0x5400036d
.word 0xf9401bb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0xaa1a03e1
bl _p_44
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa1703e1
.word 0xd2800001
.word 0xaa1803e1
.word 0xaa1703e1
.word 0xd2800002
.word 0xaa1803e3
bl _p_45
.word 0xf9401bb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9401bb1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_24:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int
SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #560]
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
.word 0xb9801ba1
bl _p_46
.word 0xf90027a0
.word 0xf94013b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
bl _p_24
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_25:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS__ctor
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS__ctor:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #568]
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
.word 0xf9400fb1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #576]
.word 0xd2800201
.word 0xd2800201
bl _p_8
.word 0xf90027a0
bl _p_47
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a1
.word 0xaa1a03e0
bl _p_48
.word 0xf9400fb1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #584]
.word 0xd2800201
.word 0xd2800201
bl _p_8
.word 0xf90023a0
bl _p_49
.word 0xf9400fb1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa1a03e0
bl _p_50
.word 0xf9400fb1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #592]
.word 0xd2800201
.word 0xd2800201
bl _p_8
.word 0xf9001fa0
bl _p_51
.word 0xf9400fb1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa1a03e0
bl _p_52
.word 0xf9400fb1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #600]
.word 0xd2800201
.word 0xd2800201
bl _p_8
.word 0xf9001ba0
bl _p_53
.word 0xf9400fb1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1
.word 0xaa1a03e0
bl _p_54
.word 0xf9400fb1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_41:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_SQLiteApi
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_SQLiteApi:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #608]
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
.word 0xf9400800
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

Lme_42:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #616]
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
.word 0xf9000820
.word 0x91004021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
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

Lme_43:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_StopwatchFactory
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_StopwatchFactory:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #624]
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
.word 0xf9400c00
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

Lme_44:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #632]
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
.word 0xf9000c20
.word 0x91006021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
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

Lme_45:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_ReflectionService
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_ReflectionService:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #640]
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
.word 0xf9401000
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

Lme_46:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #648]
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
.word 0xf9001020
.word 0x91008021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
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

Lme_47:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_VolatileService
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_VolatileService:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #656]
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
.word 0xf9401400
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

Lme_48:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService
SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #664]
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
.word 0xf9001420
.word 0x9100a021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
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

Lme_49:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_Create
SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_Create:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #672]
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

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #680]
.word 0xd2800301
.word 0xd2800301
bl _p_8
.word 0xf9001ba0
bl _p_55
.word 0xf9400fb1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_4a:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor
SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #688]
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

Lme_4b:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor
SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #696]
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
.word 0xf9400fb1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #704]
.word 0xd2800501
.word 0xd2800501
bl _p_8
.word 0xf9001ba0
bl _p_56
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9000b40
.word 0x91004341
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9400fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_4c:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Stop
SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Stop:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
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
.word 0xf9400ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf940003e
bl _p_57
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_4d:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Reset
SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Reset:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #720]
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
.word 0xf940003e
bl _p_58
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_4e:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Start
SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Start:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #728]
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
.word 0xf940003e
bl _p_59
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_4f:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_get_ElapsedMilliseconds
SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_get_ElapsedMilliseconds:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #736]
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
.word 0xf940003e
bl _p_60
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

Lme_50:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS_Write_int__int
SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS_Write_int__int:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #744]
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
.word 0xf9400fa0
.word 0xb98023a1
.word 0x9100001e
.word 0x889fffc1
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

Lme_51:
.text
	.align 4
	.no_dead_strip SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor
SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #752]
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

Lme_52:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr:
.loc 1 1 0
.word 0xa9b17bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90033be
.word 0xa906d3b3
.word 0xa907dbb5
.word 0xa908e3b7
.word 0xa909ebb9
.word 0xa90af3bb
.word 0xf9005fbd
.word 0x910003f1
.word 0xf90063b1
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9001ba4

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #760]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf90067bf
.word 0xf9006bbf
.word 0x390363bf
.word 0xd2800014
.word 0xd2800013

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f8
.word 0x910143a0
.word 0xf9400301
.word 0xf9002ba1
.word 0xf9000300
.word 0xf9401fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0x390363bf
.word 0xf9401fb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
bl _p_61
.word 0xaa0003f3
.word 0xf9401fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
.word 0xaa1303e2
.word 0xb9802ba3
.word 0xf9401ba4
.word 0xaa1303e2
bl _p_62
.word 0x93407c00
.word 0xaa0003f4
.word 0xf9401fb1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000240
.word 0xf9401fb1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xf90073a0
.word 0xf94073a1
.word 0xf94073a0
.word 0xf90077a1
.word 0xb4000060
.word 0xf94077a0
bl _p_4
.word 0xf94077a0
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xaa1303e1
.word 0xaa1303e1
bl _p_64
.word 0xf9401fb1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0x910143a0
.word 0xf9402ba0
.word 0xf9000300
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xf9401fb1
.word 0xf9420231
.word 0xb4000051
.word 0xd63f0220
.word 0xa946d3b3
.word 0xf9404bb8
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0

Lme_54:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf9002bbe
.word 0xa905d3b3
.word 0xa906dbb5
.word 0xa907e3b7
.word 0xa908ebb9
.word 0xa909f3bb
.word 0xf90057bd
.word 0x910003f1
.word 0xf9005bb1
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xfd0013a0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #776]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf9005fbf
.word 0xf90063bf
.word 0x390323bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x910103a0
.word 0xf94002c1
.word 0xf90023a1
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0x390323bf
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
.word 0xfd4013a0
bl _p_65
.word 0x93407c00
.word 0xaa0003f7
.word 0xf94017b1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94017b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x910103a0
.word 0xf94023a0
.word 0xf90002c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94017b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94657b4
.word 0xa9475fb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_55:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf9002bbe
.word 0xa905d3b3
.word 0xa906dbb5
.word 0xa907e3b7
.word 0xa908ebb9
.word 0xa909f3bb
.word 0xf90057bd
.word 0x910003f1
.word 0xf9005bb1
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #784]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf9005fbf
.word 0xf90063bf
.word 0x390323bf
.word 0xd2800016

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f5
.word 0x910103a0
.word 0xf94002a1
.word 0xf90023a1
.word 0xf90002a0
.word 0xf94017b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0x390323bf
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
.word 0xb98023a2
bl _p_66
.word 0x93407c00
.word 0xaa0003f6
.word 0xf94017b1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94017b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xaa1403e1
.word 0xaa0003f3
.word 0xb4000094
.word 0xaa1303e0
.word 0xaa1303e0
bl _p_4
.word 0xaa1303e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0x910103a0
.word 0xf94023a0
.word 0xf90002a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94017b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d3b3
.word 0xa946dbb5
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_56:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf9002bbe
.word 0xa905d3b3
.word 0xa906dbb5
.word 0xa907e3b7
.word 0xa908ebb9
.word 0xa909f3bb
.word 0xf90057bd
.word 0x910003f1
.word 0xf9005bb1
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #792]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf9005fbf
.word 0xf90063bf
.word 0x390323bf
.word 0xd2800016

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f5
.word 0x910103a0
.word 0xf94002a1
.word 0xf90023a1
.word 0xf90002a0
.word 0xf94017b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0x390323bf
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
.word 0xf94013a2
bl _p_67
.word 0x93407c00
.word 0xaa0003f6
.word 0xf94017b1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94017b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xaa1403e1
.word 0xaa0003f3
.word 0xb4000094
.word 0xaa1303e0
.word 0xaa1303e0
bl _p_4
.word 0xaa1303e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0x910103a0
.word 0xf94023a0
.word 0xf90002a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94017b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d3b3
.word 0xa946dbb5
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_57:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #800]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_68
.word 0x93407c00
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_58:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #808]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017
.word 0xd2800016

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f5
.word 0x9100e3a0
.word 0xf94002a1
.word 0xf9001fa1
.word 0xf90002a0
.word 0xf94013b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
bl _p_69
.word 0xaa0003f6
.word 0xf94013b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xaa1603e1
.word 0xaa1603e1
bl _p_70
.word 0x93407c00
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xaa1403e1
.word 0xaa0003f3
.word 0xb4000094
.word 0xaa1303e0
.word 0xaa1303e0
bl _p_4
.word 0xaa1303e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1603e0
bl _p_71
.word 0xf94013b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94553b3
.word 0xa9465bb5
.word 0xf9403bb7
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_59:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr:
.loc 1 1 0
.word 0xa9b07bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90033be
.word 0xa906d3b3
.word 0xa907dbb5
.word 0xa908e3b7
.word 0xa909ebb9
.word 0xa90af3bb
.word 0xf9005fbd
.word 0x910003f1
.word 0xf90063b1
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9001ba4

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #816]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf90067bf
.word 0xf9006bbf
.word 0x390363bf
.word 0xb900e3bf
.word 0xd2800013

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f4
.word 0x910143a0
.word 0xf9400281
.word 0xf9002ba1
.word 0xf9000280
.word 0xf9401fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0x390363bf
.word 0xf9401fb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
bl _p_72
.word 0xaa0003f3
.word 0xf9401fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
.word 0xaa1303e2
.word 0xb9802ba3
.word 0xf9401ba4
.word 0xaa1303e2
bl _p_73
.word 0x93407c00
.word 0xb900e3a0
.word 0xf9401fb1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9401fb1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003fa
.word 0xaa1a03e0
.word 0xaa1a03e1
.word 0xaa0003f9
.word 0xb400009a
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_4
.word 0xaa1903e0
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xb980e3a0
.word 0xf9007ba0
.word 0x910143a0
.word 0xf9402ba0
.word 0xf9000280
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba0
.word 0xf9401fb1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xa946d3b3
.word 0xa949ebb9
.word 0x910003bf
.word 0xa8d07bfd
.word 0xd65f03c0

Lme_5a:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #824]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_74
.word 0x93407c00
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_5b:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #832]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_75
.word 0x93407c00
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_5c:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #840]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_76
.word 0x93407c00
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_5d:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #848]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_77
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_5e:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #856]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_78
.word 0x93407c00
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_5f:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #864]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_79
.word 0x93407c00
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_60:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int:
.loc 1 1 0
.word 0xa9b27bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #872]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0x9e6703e0
.word 0xfd0067a0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100e3a0
.word 0xf94002e1
.word 0xf9001fa1
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_80
.word 0xfd0067a0
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4067a0
.word 0xfd006ba0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd406ba0
.word 0xf94013b1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9465bb5
.word 0xf9403bb7
.word 0x910003bf
.word 0xa8ce7bfd
.word 0xd65f03c0

Lme_61:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #880]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_81
.word 0x93407c00
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_62:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #888]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_82
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_63:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #896]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_83
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_64:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #904]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_84
.word 0x93407c00
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_65:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #912]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_85
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_66:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #920]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_86
.word 0x93407c00
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_67:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #928]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_87
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_68:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr:
.loc 1 1 0
.word 0xa9b27bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf9002fbe
.word 0xa90653b3
.word 0xa9075bb5
.word 0xa90863b7
.word 0xa9096bb9
.word 0xa90a73bb
.word 0xf9005bbd
.word 0x910003f1
.word 0xf9005fb1
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #936]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf90063bf
.word 0xf90067bf
.word 0x390343bf
.word 0xd2800015
.word 0xd2800014

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f3
.word 0x910123a0
.word 0xf9400261
.word 0xf90027a1
.word 0xf9000260
.word 0xf9401bb1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0x390343bf
.word 0xf9401bb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_61
.word 0xaa0003f4
.word 0xf9401bb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xf9400fa1
.word 0xb98023a2
.word 0xf94017a3
.word 0xaa1403e0
bl _p_88
.word 0x93407c00
.word 0xaa0003f5
.word 0xf9401bb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000240
.word 0xf9401bb1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f7
.word 0xaa1703e0
.word 0xaa1703e1
.word 0xf9006fa0
.word 0xb4000077
.word 0xf9406fa0
bl _p_4
.word 0xf9406fa0
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xaa1403e1
.word 0xaa1403e1
bl _p_64
.word 0xf9401bb1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0x910123a0
.word 0xf94027a0
.word 0xf9000260
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf941e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xf9401bb1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94653b3
.word 0xf9403bb5
.word 0xf94043b7
.word 0x910003bf
.word 0xa8ce7bfd
.word 0xd65f03c0

Lme_69:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr:
.loc 1 1 0
.word 0xa9af7bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90033be
.word 0xa906d3b3
.word 0xa907dbb5
.word 0xa908e3b7
.word 0xa909ebb9
.word 0xa90af3bb
.word 0xf9005fbd
.word 0x910003f1
.word 0xf90063b1
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9001ba4

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #944]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf90067bf
.word 0xf9006bbf
.word 0x390363bf
.word 0xb900e3bf
.word 0xd2800013

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f4
.word 0x910143a0
.word 0xf9400281
.word 0xf9002ba1
.word 0xf9000280
.word 0xf9401fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0x390363bf
.word 0xf9401fb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
bl _p_69
.word 0xaa0003f3
.word 0xf9401fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xaa1303e1
.word 0xb98023a2
.word 0xf94017a3
.word 0xf9401ba4
.word 0xaa1303e1
bl _p_89
.word 0x93407c00
.word 0xb900e3a0
.word 0xf9401fb1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000240
.word 0xf9401fb1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xf90077a0
.word 0xf94077a1
.word 0xf94077a0
.word 0xf9007ba1
.word 0xb4000060
.word 0xf9407ba0
bl _p_4
.word 0xf9407ba0
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0xaa1303e0
bl _p_71
.word 0xf9401fb1
.word 0xf941ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xb980e3a0
.word 0xf90083a0
.word 0x910143a0
.word 0xf9402ba0
.word 0xf9000280
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xf9401fb1
.word 0xf9420231
.word 0xb4000051
.word 0xd63f0220
.word 0xa946d3b3
.word 0x910003bf
.word 0xa8d17bfd
.word 0xd65f03c0

Lme_6a:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #952]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_90
.word 0x93407c00
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_6b:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #960]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_91
.word 0x93407c00
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_6c:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90027be
.word 0xa90553b3
.word 0xa9065bb5
.word 0xa90763b7
.word 0xa9086bb9
.word 0xa90973bb
.word 0xf90053bd
.word 0x910003f1
.word 0xf90057b1
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #968]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9005bbf
.word 0xf9005fbf
.word 0x390303bf
.word 0xd2800017

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f6
.word 0x9100e3a0
.word 0xf94002c1
.word 0xf9001fa1
.word 0xf90002c0
.word 0xf94013b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x390303bf
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
bl _p_92
.word 0xaa0003f7
.word 0xf94013b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf94013b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503e1
.word 0xaa0003f4
.word 0xb4000095
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_4
.word 0xaa1403e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9100e3a0
.word 0xf9401fa0
.word 0xf90002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94013b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xa945d7b4
.word 0xa946dfb6
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_6d:
.text
	.align 4
	.no_dead_strip wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr
wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0x1000001e
.word 0xf90023be
.word 0xa904d3b3
.word 0xa905dbb5
.word 0xa906e3b7
.word 0xa907ebb9
.word 0xa908f3bb
.word 0xf9004fbd
.word 0x910003f1
.word 0xf90053b1
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #976]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf90057bf
.word 0xf9005bbf
.word 0x3902e3bf
.word 0xd2800018

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #112]
.word 0xd63f0000
.word 0xaa0003f7
.word 0x9100c3a0
.word 0xf94002e1
.word 0xf9001ba1
.word 0xf90002e0
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x3902e3bf
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_93
.word 0x93407c00
.word 0xaa0003f8
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xb9400000
.word 0x34000260
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
bl _p_63
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa0003f5
.word 0xb4000096
.word 0xaa1503e0
.word 0xaa1503e0
bl _p_4
.word 0xaa1503e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9100c3a0
.word 0xf9401ba0
.word 0xf90002e0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa945dbb5
.word 0xa946e3b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_6e:
.text
ut_111:
add x0, x0, 16
b wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_StructureToPtr_object_intptr_bool
.text
	.align 4
	.no_dead_strip wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_StructureToPtr_object_intptr_bool
wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_StructureToPtr_object_intptr_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #984]
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
.word 0xf9400fa0
.word 0xf9400ba1
.word 0x91004021
.word 0xd2800102
.word 0xf9400021
.word 0xf9000001
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

Lme_6f:
.text
ut_112:
add x0, x0, 16
b wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_PtrToStructure_intptr_object
.text
	.align 4
	.no_dead_strip wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_PtrToStructure_intptr_object
wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_PtrToStructure_intptr_object:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #992]
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
.word 0xf9400fa0
.word 0x91004000
.word 0xf9400ba1
.word 0xd2800102
.word 0xf9400021
.word 0xf9000001
.word 0xf94013b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_70:
.text
ut_113:
add x0, x0, 16
b wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_StructureToPtr_object_intptr_bool
.text
	.align 4
	.no_dead_strip wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_StructureToPtr_object_intptr_bool
wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_StructureToPtr_object_intptr_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1000]
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
.word 0xf9400fa0
.word 0xf9400ba1
.word 0x91004021
.word 0xd2800102
.word 0xf9400021
.word 0xf9000001
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

Lme_71:
.text
ut_114:
add x0, x0, 16
b wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_PtrToStructure_intptr_object
.text
	.align 4
	.no_dead_strip wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_PtrToStructure_intptr_object
wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_PtrToStructure_intptr_object:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1008]
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
.word 0xf9400fa0
.word 0x91004000
.word 0xf9400ba1
.word 0xd2800102
.word 0xf9400021
.word 0xf9000001
.word 0xf94013b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_72:
.text
	.align 3
jit_code_end:

	.byte 0,0,0,0
.text
	.align 3
method_addresses:
	.no_dead_strip method_addresses
bl SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetPublicInstanceProperties_System_Type
bl SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetMemberValue_object_System_Linq_Expressions_Expression_System_Reflection_MemberInfo
bl SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Open_byte___SQLite_Net_Interop_IDbHandle__int_intptr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ExtendedErrCode_SQLite_Net_Interop_IDbHandle
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Close_SQLite_Net_Interop_IDbHandle
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BusyTimeout_SQLite_Net_Interop_IDbHandle_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Changes_SQLite_Net_Interop_IDbHandle
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Prepare2_SQLite_Net_Interop_IDbHandle_string
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Step_SQLite_Net_Interop_IDbStatement
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Reset_SQLite_Net_Interop_IDbStatement
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Finalize_SQLite_Net_Interop_IDbStatement
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_LastInsertRowid_SQLite_Net_Interop_IDbHandle
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindParameterIndex_SQLite_Net_Interop_IDbStatement_string
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindNull_SQLite_Net_Interop_IDbStatement_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt_SQLite_Net_Interop_IDbStatement_int_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt64_SQLite_Net_Interop_IDbStatement_int_long
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindDouble_SQLite_Net_Interop_IDbStatement_int_double
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindText16_SQLite_Net_Interop_IDbStatement_int_string_int_intptr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindBlob_SQLite_Net_Interop_IDbStatement_int_byte___int_intptr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnCount_SQLite_Net_Interop_IDbStatement
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnName16_SQLite_Net_Interop_IDbStatement_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnType_SQLite_Net_Interop_IDbStatement_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt_SQLite_Net_Interop_IDbStatement_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt64_SQLite_Net_Interop_IDbStatement_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnDouble_SQLite_Net_Interop_IDbStatement_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnText16_SQLite_Net_Interop_IDbStatement_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnByteArray_SQLite_Net_Interop_IDbStatement_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int
bl SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl method_addresses
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS__ctor
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_SQLiteApi
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_StopwatchFactory
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_ReflectionService
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_VolatileService
bl SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService
bl SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_Create
bl SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor
bl SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor
bl SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Stop
bl SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Reset
bl SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Start
bl SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_get_ElapsedMilliseconds
bl SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS_Write_int__int
bl SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor
bl method_addresses
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int
bl wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr
bl wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_StructureToPtr_object_intptr_bool
bl wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_PtrToStructure_intptr_object
bl wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_StructureToPtr_object_intptr_bool
bl wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_PtrToStructure_intptr_object
method_addresses_end:

.section __TEXT, __const
	.align 3
unbox_trampolines:

	.long 30,31,32,33,34,35,111,112
	.long 113,114
unbox_trampolines_end:

	.long 0
.text
	.align 3
unbox_trampoline_addresses:
bl ut_30
bl ut_31
bl ut_32
bl ut_33
bl ut_34
bl ut_35
bl ut_111
bl ut_112
bl ut_113
bl ut_114

	.long 0
.section __TEXT, __const
	.align 3
unwind_info:

	.byte 0,13,12,31,0,68,14,80,157,10,158,9,68,13,29,24,12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,151
	.byte 14,152,13,68,153,12,154,11,13,12,31,0,68,14,48,157,6,158,5,68,13,29,14,12,31,0,68,14,128,1,157,16
	.byte 158,15,68,13,29,13,12,31,0,68,14,96,157,12,158,11,68,13,29,19,12,31,0,68,14,176,1,157,22,158,21,68
	.byte 13,29,68,150,20,151,19,13,12,31,0,68,14,112,157,14,158,13,68,13,29,16,12,31,0,68,14,64,157,8,158,7
	.byte 68,13,29,68,153,6,13,12,31,0,68,14,64,157,8,158,7,68,13,29,23,12,31,0,68,14,96,157,12,158,11,68
	.byte 13,29,68,151,10,152,9,68,153,8,154,7,16,12,31,0,68,14,80,157,10,158,9,68,13,29,68,154,8,16,12,31
	.byte 0,68,14,64,157,8,158,7,68,13,29,68,154,6,39,12,31,0,68,14,240,1,157,30,158,29,68,13,29,76,147,17
	.byte 148,16,68,149,15,150,14,68,151,13,152,12,68,153,11,154,10,68,155,9,156,8,39,12,31,0,68,14,208,1,157,26
	.byte 158,25,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9,154,8,68,155,7,156,6,39,12
	.byte 31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10,154
	.byte 9,68,155,8,156,7,39,12,31,0,68,14,128,2,157,32,158,31,68,13,29,76,147,19,148,18,68,149,17,150,16,68
	.byte 151,15,152,14,68,153,13,154,12,68,155,11,156,10,39,12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15
	.byte 148,14,68,149,13,150,12,68,151,11,152,10,68,153,9,154,8,68,155,7,156,6,39,12,31,0,68,14,224,1,157,28
	.byte 158,27,68,13,29,76,147,18,148,17,68,149,16,150,15,68,151,14,152,13,68,153,12,154,11,68,155,10,156,9,39,12
	.byte 31,0,68,14,224,1,157,28,158,27,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10,154
	.byte 9,68,155,8,156,7,39,12,31,0,68,14,144,2,157,34,158,33,68,13,29,76,147,21,148,20,68,149,19,150,18,68
	.byte 151,17,152,16,68,153,15,154,14,68,155,13,156,12

.text
	.align 4
plt:
mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_plt:
	.no_dead_strip plt__jit_icall_mono_helper_ldstr
plt__jit_icall_mono_helper_ldstr:
_p_1:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1024]
br x16
.word 1033
	.no_dead_strip plt__jit_icall_mono_helper_newobj_mscorlib
plt__jit_icall_mono_helper_newobj_mscorlib:
_p_2:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1032]
br x16
.word 1053
	.no_dead_strip plt_string_Concat_object_object
plt_string_Concat_object_object:
_p_3:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1040]
br x16
.word 1083
	.no_dead_strip plt__jit_icall_mono_arch_throw_exception
plt__jit_icall_mono_arch_throw_exception:
_p_4:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1048]
br x16
.word 1088
	.no_dead_strip plt__jit_icall_mono_arch_throw_corlib_exception
plt__jit_icall_mono_arch_throw_corlib_exception:
_p_5:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1056]
br x16
.word 1116
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr:
_p_6:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1064]
br x16
.word 1151
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr:
_p_7:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1072]
br x16
.word 1153
	.no_dead_strip plt_wrapper_alloc_object_AllocSmall_intptr_intptr
plt_wrapper_alloc_object_AllocSmall_intptr_intptr:
_p_8:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1080]
br x16
.word 1155
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr:
_p_9:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1088]
br x16
.word 1163
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr:
_p_10:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1096]
br x16
.word 1165
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr:
_p_11:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1104]
br x16
.word 1167
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int:
_p_12:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1112]
br x16
.word 1169
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr:
_p_13:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1120]
br x16
.word 1171
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr:
_p_14:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1128]
br x16
.word 1173
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle:
_p_15:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1136]
br x16
.word 1175
	.no_dead_strip plt_SQLite_Net_SQLiteException_New_SQLite_Net_Interop_Result_string
plt_SQLite_Net_SQLiteException_New_SQLite_Net_Interop_Result_string:
_p_16:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1144]
br x16
.word 1177
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr:
_p_17:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1152]
br x16
.word 1182
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr:
_p_18:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1160]
br x16
.word 1184
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr:
_p_19:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1168]
br x16
.word 1186
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr:
_p_20:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1176]
br x16
.word 1188
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr:
_p_21:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1184]
br x16
.word 1190
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr:
_p_22:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1192]
br x16
.word 1192
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr:
_p_23:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1200]
br x16
.word 1194
	.no_dead_strip plt_System_Runtime_InteropServices_Marshal_PtrToStringUni_intptr
plt_System_Runtime_InteropServices_Marshal_PtrToStringUni_intptr:
_p_24:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1208]
br x16
.word 1196
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string:
_p_25:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1216]
br x16
.word 1201
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int:
_p_26:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1224]
br x16
.word 1203
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int:
_p_27:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1232]
br x16
.word 1205
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long:
_p_28:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1240]
br x16
.word 1207
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double:
_p_29:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1248]
br x16
.word 1209
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr:
_p_30:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1256]
br x16
.word 1211
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr:
_p_31:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1264]
br x16
.word 1213
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr:
_p_32:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1272]
br x16
.word 1215
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int:
_p_33:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1280]
br x16
.word 1217
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int:
_p_34:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1288]
br x16
.word 1219
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int:
_p_35:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1296]
br x16
.word 1221
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int:
_p_36:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1304]
br x16
.word 1223
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int:
_p_37:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1312]
br x16
.word 1225
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int:
_p_38:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1320]
br x16
.word 1227
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int:
_p_39:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1328]
br x16
.word 1229
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr:
_p_40:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1336]
br x16
.word 1231
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr:
_p_41:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1344]
br x16
.word 1233
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int:
_p_42:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1352]
br x16
.word 1235
	.no_dead_strip plt_wrapper_alloc_object_AllocVector_intptr_intptr
plt_wrapper_alloc_object_AllocVector_intptr_intptr:
_p_43:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1360]
br x16
.word 1237
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int:
_p_44:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1368]
br x16
.word 1245
	.no_dead_strip plt_System_Runtime_InteropServices_Marshal_Copy_intptr_byte___int_int
plt_System_Runtime_InteropServices_Marshal_Copy_intptr_byte___int_int:
_p_45:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1376]
br x16
.word 1247
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int:
_p_46:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1384]
br x16
.word 1252
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor
plt_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor:
_p_47:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1392]
br x16
.word 1254
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi
plt_SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi:
_p_48:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1400]
br x16
.word 1256
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor
plt_SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor:
_p_49:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1408]
br x16
.word 1258
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory
plt_SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory:
_p_50:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1416]
br x16
.word 1260
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor
plt_SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor:
_p_51:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1424]
br x16
.word 1262
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService
plt_SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService:
_p_52:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1432]
br x16
.word 1264
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor
plt_SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor:
_p_53:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1440]
br x16
.word 1266
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService
plt_SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService:
_p_54:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1448]
br x16
.word 1268
	.no_dead_strip plt_SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor
plt_SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor:
_p_55:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1456]
br x16
.word 1270
	.no_dead_strip plt_System_Diagnostics_Stopwatch__ctor
plt_System_Diagnostics_Stopwatch__ctor:
_p_56:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1464]
br x16
.word 1272
	.no_dead_strip plt_System_Diagnostics_Stopwatch_Stop
plt_System_Diagnostics_Stopwatch_Stop:
_p_57:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1472]
br x16
.word 1277
	.no_dead_strip plt_System_Diagnostics_Stopwatch_Reset
plt_System_Diagnostics_Stopwatch_Reset:
_p_58:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1480]
br x16
.word 1282
	.no_dead_strip plt_System_Diagnostics_Stopwatch_Start
plt_System_Diagnostics_Stopwatch_Start:
_p_59:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1488]
br x16
.word 1287
	.no_dead_strip plt_System_Diagnostics_Stopwatch_get_ElapsedMilliseconds
plt_System_Diagnostics_Stopwatch_get_ElapsedMilliseconds:
_p_60:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1496]
br x16
.word 1292
	.no_dead_strip plt__jit_icall_mono_array_to_lparray
plt__jit_icall_mono_array_to_lparray:
_p_61:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1504]
br x16
.word 1297
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr:
_p_62:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1512]
br x16
.word 1321
	.no_dead_strip plt__jit_icall_mono_thread_interruption_checkpoint
plt__jit_icall_mono_thread_interruption_checkpoint:
_p_63:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1520]
br x16
.word 1323
	.no_dead_strip plt__jit_icall_mono_free_lparray
plt__jit_icall_mono_free_lparray:
_p_64:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1528]
br x16
.word 1361
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double:
_p_65:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1536]
br x16
.word 1381
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int:
_p_66:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1544]
br x16
.word 1383
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long:
_p_67:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1552]
br x16
.word 1385
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int:
_p_68:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1560]
br x16
.word 1387
	.no_dead_strip plt__jit_icall_mono_string_to_utf8str
plt__jit_icall_mono_string_to_utf8str:
_p_69:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1568]
br x16
.word 1389
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string:
_p_70:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1576]
br x16
.word 1414
	.no_dead_strip plt__jit_icall_mono_marshal_free
plt__jit_icall_mono_marshal_free:
_p_71:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1584]
br x16
.word 1416
	.no_dead_strip plt__jit_icall_mono_marshal_string_to_utf16
plt__jit_icall_mono_marshal_string_to_utf16:
_p_72:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1592]
br x16
.word 1436
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr:
_p_73:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1600]
br x16
.word 1467
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int:
_p_74:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1608]
br x16
.word 1469
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr:
_p_75:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1616]
br x16
.word 1471
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr:
_p_76:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1624]
br x16
.word 1473
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int:
_p_77:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1632]
br x16
.word 1475
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int:
_p_78:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1640]
br x16
.word 1477
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr:
_p_79:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1648]
br x16
.word 1479
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int:
_p_80:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1656]
br x16
.word 1481
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int:
_p_81:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1664]
br x16
.word 1483
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int:
_p_82:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1672]
br x16
.word 1485
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int:
_p_83:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1680]
br x16
.word 1487
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int:
_p_84:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1688]
br x16
.word 1489
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr:
_p_85:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1696]
br x16
.word 1491
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr:
_p_86:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1704]
br x16
.word 1493
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr:
_p_87:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1712]
br x16
.word 1495
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr:
_p_88:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1720]
br x16
.word 1497
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr:
_p_89:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1728]
br x16
.word 1499
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr:
_p_90:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1736]
br x16
.word 1501
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr:
_p_91:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1744]
br x16
.word 1503
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int:
_p_92:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1752]
br x16
.word 1505
	.no_dead_strip plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr
plt__icall_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr:
_p_93:
adrp x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got@PAGEOFF
ldr x16, [x16, #1760]
br x16
.word 1507
plt_end:
.section __DATA, __bss
	.align 3
.lcomm mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got, 1768
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
	.asciz "3FC74285-B49A-4691-8268-5A92B63B533B"
.section __TEXT, __const
	.align 2
assembly_name:
	.asciz "SQLite.Net.Platform.XamarinIOS.Unified"
.data
	.align 3
_mono_aot_file_info:

	.long 139,0
	.align 3
	.quad mono_aot_SQLite_Net_Platform_XamarinIOS_Unified_got
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

	.long 127,1768,94,115,70,387000831,0,11625
	.long 128,8,8,10,0,25,13920,2288
	.long 1928,1416,0,1688,1896,1504,0,1160
	.long 176,0,0,0,0,0,0,0
	.long 0,0,0,0,0,0,0,0
	.long 0
	.byte 242,0,76,70,13,163,19,73,250,203,147,244,106,225,225,1
	.globl _mono_aot_module_SQLite_Net_Platform_XamarinIOS_Unified_info
	.align 3
_mono_aot_module_SQLite_Net_Platform_XamarinIOS_Unified_info:
	.align 3
	.quad _mono_aot_file_info
.section __DWARF, __debug_info,regular,debug
LTDIE_1:

	.byte 17
	.asciz "System_Object"

	.byte 16,7
	.asciz "System_Object"

LDIFF_SYM4=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM4
LTDIE_1_POINTER:

	.byte 13
LDIFF_SYM5=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM5
LTDIE_1_REFERENCE:

	.byte 14
LDIFF_SYM6=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM6
LTDIE_0:

	.byte 5
	.asciz "SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS"

	.byte 16,16
LDIFF_SYM7=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM7
	.byte 2,35,0,0,7
	.asciz "SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS"

LDIFF_SYM8=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM8
LTDIE_0_POINTER:

	.byte 13
LDIFF_SYM9=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM9
LTDIE_0_REFERENCE:

	.byte 14
LDIFF_SYM10=LTDIE_0 - Ldebug_info_start
	.long LDIFF_SYM10
LTDIE_3:

	.byte 5
	.asciz "System_Reflection_MemberInfo"

	.byte 16,16
LDIFF_SYM11=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM11
	.byte 2,35,0,0,7
	.asciz "System_Reflection_MemberInfo"

LDIFF_SYM12=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM12
LTDIE_3_POINTER:

	.byte 13
LDIFF_SYM13=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM13
LTDIE_3_REFERENCE:

	.byte 14
LDIFF_SYM14=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM14
LTDIE_2:

	.byte 5
	.asciz "System_Type"

	.byte 24,16
LDIFF_SYM15=LTDIE_3 - Ldebug_info_start
	.long LDIFF_SYM15
	.byte 2,35,0,6
	.asciz "_impl"

LDIFF_SYM16=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM16
	.byte 2,35,16,0,7
	.asciz "System_Type"

LDIFF_SYM17=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM17
LTDIE_2_POINTER:

	.byte 13
LDIFF_SYM18=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM18
LTDIE_2_REFERENCE:

	.byte 14
LDIFF_SYM19=LTDIE_2 - Ldebug_info_start
	.long LDIFF_SYM19
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.ReflectionServiceIOS:GetPublicInstanceProperties"
	.asciz "SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetPublicInstanceProperties_System_Type"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetPublicInstanceProperties_System_Type
	.quad Lme_0

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM20=LTDIE_0_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM20
	.byte 2,141,16,3
	.asciz "mappedType"

LDIFF_SYM21=LTDIE_2_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM21
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM22=Lfde0_end - Lfde0_start
	.long LDIFF_SYM22
Lfde0_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetPublicInstanceProperties_System_Type

LDIFF_SYM23=Lme_0 - SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetPublicInstanceProperties_System_Type
	.long LDIFF_SYM23
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29
	.align 3
Lfde0_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_4:

	.byte 5
	.asciz "System_Linq_Expressions_Expression"

	.byte 16,16
LDIFF_SYM24=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM24
	.byte 2,35,0,0,7
	.asciz "System_Linq_Expressions_Expression"

LDIFF_SYM25=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM25
LTDIE_4_POINTER:

	.byte 13
LDIFF_SYM26=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM26
LTDIE_4_REFERENCE:

	.byte 14
LDIFF_SYM27=LTDIE_4 - Ldebug_info_start
	.long LDIFF_SYM27
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.ReflectionServiceIOS:GetMemberValue"
	.asciz "SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetMemberValue_object_System_Linq_Expressions_Expression_System_Reflection_MemberInfo"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetMemberValue_object_System_Linq_Expressions_Expression_System_Reflection_MemberInfo
	.quad Lme_1

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM28=LTDIE_0_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM28
	.byte 2,141,48,3
	.asciz "obj"

LDIFF_SYM29=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM29
	.byte 1,105,3
	.asciz "expr"

LDIFF_SYM30=LTDIE_4_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM30
	.byte 2,141,56,3
	.asciz "member"

LDIFF_SYM31=LTDIE_3_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM31
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM32=Lfde1_end - Lfde1_start
	.long LDIFF_SYM32
Lfde1_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetMemberValue_object_System_Linq_Expressions_Expression_System_Reflection_MemberInfo

LDIFF_SYM33=Lme_1 - SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS_GetMemberValue_object_System_Linq_Expressions_Expression_System_Reflection_MemberInfo
	.long LDIFF_SYM33
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29,68,151,14,152,13,68,153,12,154,11
	.align 3
Lfde1_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.ReflectionServiceIOS:.ctor"
	.asciz "SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor
	.quad Lme_2

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM34=LTDIE_0_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM34
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM35=Lfde2_end - Lfde2_start
	.long LDIFF_SYM35
Lfde2_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor

LDIFF_SYM36=Lme_2 - SQLite_Net_Platform_XamarinIOS_ReflectionServiceIOS__ctor
	.long LDIFF_SYM36
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde2_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_5:

	.byte 5
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS"

	.byte 16,16
LDIFF_SYM37=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM37
	.byte 2,35,0,0,7
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS"

LDIFF_SYM38=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM38
LTDIE_5_POINTER:

	.byte 13
LDIFF_SYM39=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM39
LTDIE_5_REFERENCE:

	.byte 14
LDIFF_SYM40=LTDIE_5 - Ldebug_info_start
	.long LDIFF_SYM40
LTDIE_6:

	.byte 17
	.asciz "SQLite_Net_Interop_IDbHandle"

	.byte 16,7
	.asciz "SQLite_Net_Interop_IDbHandle"

LDIFF_SYM41=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM41
LTDIE_6_POINTER:

	.byte 13
LDIFF_SYM42=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM42
LTDIE_6_REFERENCE:

	.byte 14
LDIFF_SYM43=LTDIE_6 - Ldebug_info_start
	.long LDIFF_SYM43
LTDIE_8:

	.byte 5
	.asciz "System_ValueType"

	.byte 16,16
LDIFF_SYM44=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM44
	.byte 2,35,0,0,7
	.asciz "System_ValueType"

LDIFF_SYM45=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM45
LTDIE_8_POINTER:

	.byte 13
LDIFF_SYM46=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM46
LTDIE_8_REFERENCE:

	.byte 14
LDIFF_SYM47=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM47
LTDIE_7:

	.byte 5
	.asciz "System_Int32"

	.byte 20,16
LDIFF_SYM48=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM48
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM49=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM49
	.byte 2,35,16,0,7
	.asciz "System_Int32"

LDIFF_SYM50=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM50
LTDIE_7_POINTER:

	.byte 13
LDIFF_SYM51=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM51
LTDIE_7_REFERENCE:

	.byte 14
LDIFF_SYM52=LTDIE_7 - Ldebug_info_start
	.long LDIFF_SYM52
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:Open"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Open_byte___SQLite_Net_Interop_IDbHandle__int_intptr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Open_byte___SQLite_Net_Interop_IDbHandle__int_intptr
	.quad Lme_3

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM53=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM53
	.byte 2,141,16,3
	.asciz "filename"

LDIFF_SYM54=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM54
	.byte 2,141,24,3
	.asciz "db"

LDIFF_SYM55=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM55
	.byte 2,141,32,3
	.asciz "flags"

LDIFF_SYM56=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM56
	.byte 2,141,40,3
	.asciz "zvfs"

LDIFF_SYM57=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM57
	.byte 2,141,48,11
	.asciz "V_0"

LDIFF_SYM58=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM58
	.byte 3,141,224,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM59=Lfde3_end - Lfde3_start
	.long LDIFF_SYM59
Lfde3_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Open_byte___SQLite_Net_Interop_IDbHandle__int_intptr

LDIFF_SYM60=Lme_3 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Open_byte___SQLite_Net_Interop_IDbHandle__int_intptr
	.long LDIFF_SYM60
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29
	.align 3
Lfde3_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ExtendedErrCode"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ExtendedErrCode_SQLite_Net_Interop_IDbHandle"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ExtendedErrCode_SQLite_Net_Interop_IDbHandle
	.quad Lme_4

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM61=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM61
	.byte 2,141,16,3
	.asciz "db"

LDIFF_SYM62=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM62
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM63=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM63
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM64=Lfde4_end - Lfde4_start
	.long LDIFF_SYM64
Lfde4_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ExtendedErrCode_SQLite_Net_Interop_IDbHandle

LDIFF_SYM65=Lme_4 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ExtendedErrCode_SQLite_Net_Interop_IDbHandle
	.long LDIFF_SYM65
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde4_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:Close"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Close_SQLite_Net_Interop_IDbHandle"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Close_SQLite_Net_Interop_IDbHandle
	.quad Lme_5

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM66=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM66
	.byte 2,141,16,3
	.asciz "db"

LDIFF_SYM67=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM67
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM68=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM68
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM69=Lfde5_end - Lfde5_start
	.long LDIFF_SYM69
Lfde5_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Close_SQLite_Net_Interop_IDbHandle

LDIFF_SYM70=Lme_5 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Close_SQLite_Net_Interop_IDbHandle
	.long LDIFF_SYM70
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde5_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:BusyTimeout"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BusyTimeout_SQLite_Net_Interop_IDbHandle_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BusyTimeout_SQLite_Net_Interop_IDbHandle_int
	.quad Lme_6

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM71=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM71
	.byte 2,141,16,3
	.asciz "db"

LDIFF_SYM72=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM72
	.byte 2,141,24,3
	.asciz "milliseconds"

LDIFF_SYM73=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM73
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM74=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM74
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM75=Lfde6_end - Lfde6_start
	.long LDIFF_SYM75
Lfde6_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BusyTimeout_SQLite_Net_Interop_IDbHandle_int

LDIFF_SYM76=Lme_6 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BusyTimeout_SQLite_Net_Interop_IDbHandle_int
	.long LDIFF_SYM76
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde6_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:Changes"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Changes_SQLite_Net_Interop_IDbHandle"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Changes_SQLite_Net_Interop_IDbHandle
	.quad Lme_7

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM77=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM77
	.byte 2,141,16,3
	.asciz "db"

LDIFF_SYM78=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM78
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM79=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM79
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM80=Lfde7_end - Lfde7_start
	.long LDIFF_SYM80
Lfde7_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Changes_SQLite_Net_Interop_IDbHandle

LDIFF_SYM81=Lme_7 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Changes_SQLite_Net_Interop_IDbHandle
	.long LDIFF_SYM81
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde7_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_9:

	.byte 8
	.asciz "SQLite_Net_Interop_Result"

	.byte 4
LDIFF_SYM82=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM82
	.byte 9
	.asciz "OK"

	.byte 0,9
	.asciz "Error"

	.byte 1,9
	.asciz "Internal"

	.byte 2,9
	.asciz "Perm"

	.byte 3,9
	.asciz "Abort"

	.byte 4,9
	.asciz "Busy"

	.byte 5,9
	.asciz "Locked"

	.byte 6,9
	.asciz "NoMem"

	.byte 7,9
	.asciz "ReadOnly"

	.byte 8,9
	.asciz "Interrupt"

	.byte 9,9
	.asciz "IOError"

	.byte 10,9
	.asciz "Corrupt"

	.byte 11,9
	.asciz "NotFound"

	.byte 12,9
	.asciz "Full"

	.byte 13,9
	.asciz "CannotOpen"

	.byte 14,9
	.asciz "LockErr"

	.byte 15,9
	.asciz "Empty"

	.byte 16,9
	.asciz "SchemaChngd"

	.byte 17,9
	.asciz "TooBig"

	.byte 18,9
	.asciz "Constraint"

	.byte 19,9
	.asciz "Mismatch"

	.byte 20,9
	.asciz "Misuse"

	.byte 21,9
	.asciz "NotImplementedLFS"

	.byte 22,9
	.asciz "AccessDenied"

	.byte 23,9
	.asciz "Format"

	.byte 24,9
	.asciz "Range"

	.byte 25,9
	.asciz "NonDBFile"

	.byte 26,9
	.asciz "Notice"

	.byte 27,9
	.asciz "Warning"

	.byte 28,9
	.asciz "Row"

	.byte 228,0,9
	.asciz "Done"

	.byte 229,0,0,7
	.asciz "SQLite_Net_Interop_Result"

LDIFF_SYM83=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM83
LTDIE_9_POINTER:

	.byte 13
LDIFF_SYM84=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM84
LTDIE_9_REFERENCE:

	.byte 14
LDIFF_SYM85=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM85
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:Prepare2"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Prepare2_SQLite_Net_Interop_IDbHandle_string"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Prepare2_SQLite_Net_Interop_IDbHandle_string
	.quad Lme_8

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM86=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM86
	.byte 2,141,32,3
	.asciz "db"

LDIFF_SYM87=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM87
	.byte 2,141,40,3
	.asciz "query"

LDIFF_SYM88=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM88
	.byte 2,141,48,11
	.asciz "V_0"

LDIFF_SYM89=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM89
	.byte 3,141,240,0,11
	.asciz "V_1"

LDIFF_SYM90=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM90
	.byte 3,141,248,0,11
	.asciz "V_2"

LDIFF_SYM91=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM91
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM92=Lfde8_end - Lfde8_start
	.long LDIFF_SYM92
Lfde8_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Prepare2_SQLite_Net_Interop_IDbHandle_string

LDIFF_SYM93=Lme_8 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Prepare2_SQLite_Net_Interop_IDbHandle_string
	.long LDIFF_SYM93
	.long 0
	.byte 12,31,0,68,14,176,1,157,22,158,21,68,13,29,68,150,20,151,19
	.align 3
Lfde8_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_10:

	.byte 17
	.asciz "SQLite_Net_Interop_IDbStatement"

	.byte 16,7
	.asciz "SQLite_Net_Interop_IDbStatement"

LDIFF_SYM94=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM94
LTDIE_10_POINTER:

	.byte 13
LDIFF_SYM95=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM95
LTDIE_10_REFERENCE:

	.byte 14
LDIFF_SYM96=LTDIE_10 - Ldebug_info_start
	.long LDIFF_SYM96
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:Step"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Step_SQLite_Net_Interop_IDbStatement"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Step_SQLite_Net_Interop_IDbStatement
	.quad Lme_9

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM97=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM97
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM98=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM98
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM99=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM99
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM100=Lfde9_end - Lfde9_start
	.long LDIFF_SYM100
Lfde9_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Step_SQLite_Net_Interop_IDbStatement

LDIFF_SYM101=Lme_9 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Step_SQLite_Net_Interop_IDbStatement
	.long LDIFF_SYM101
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde9_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:Reset"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Reset_SQLite_Net_Interop_IDbStatement"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Reset_SQLite_Net_Interop_IDbStatement
	.quad Lme_a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM102=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM102
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM103=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM103
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM104=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM104
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM105=Lfde10_end - Lfde10_start
	.long LDIFF_SYM105
Lfde10_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Reset_SQLite_Net_Interop_IDbStatement

LDIFF_SYM106=Lme_a - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Reset_SQLite_Net_Interop_IDbStatement
	.long LDIFF_SYM106
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde10_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:Finalize"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Finalize_SQLite_Net_Interop_IDbStatement"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Finalize_SQLite_Net_Interop_IDbStatement
	.quad Lme_b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM107=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM107
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM108=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM108
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM109=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM109
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM110=Lfde11_end - Lfde11_start
	.long LDIFF_SYM110
Lfde11_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Finalize_SQLite_Net_Interop_IDbStatement

LDIFF_SYM111=Lme_b - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Finalize_SQLite_Net_Interop_IDbStatement
	.long LDIFF_SYM111
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde11_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:LastInsertRowid"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_LastInsertRowid_SQLite_Net_Interop_IDbHandle"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_LastInsertRowid_SQLite_Net_Interop_IDbHandle
	.quad Lme_c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM112=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM112
	.byte 2,141,16,3
	.asciz "db"

LDIFF_SYM113=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM113
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM114=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM114
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM115=Lfde12_end - Lfde12_start
	.long LDIFF_SYM115
Lfde12_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_LastInsertRowid_SQLite_Net_Interop_IDbHandle

LDIFF_SYM116=Lme_c - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_LastInsertRowid_SQLite_Net_Interop_IDbHandle
	.long LDIFF_SYM116
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde12_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:Errmsg16"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle
	.quad Lme_d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM117=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM117
	.byte 2,141,16,3
	.asciz "db"

LDIFF_SYM118=LTDIE_6_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM118
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM119=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM119
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM120=Lfde13_end - Lfde13_start
	.long LDIFF_SYM120
Lfde13_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle

LDIFF_SYM121=Lme_d - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_Errmsg16_SQLite_Net_Interop_IDbHandle
	.long LDIFF_SYM121
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29
	.align 3
Lfde13_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:BindParameterIndex"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindParameterIndex_SQLite_Net_Interop_IDbStatement_string"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindParameterIndex_SQLite_Net_Interop_IDbStatement_string
	.quad Lme_e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM122=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM122
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM123=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM123
	.byte 2,141,24,3
	.asciz "name"

LDIFF_SYM124=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM124
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM125=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM125
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM126=Lfde14_end - Lfde14_start
	.long LDIFF_SYM126
Lfde14_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindParameterIndex_SQLite_Net_Interop_IDbStatement_string

LDIFF_SYM127=Lme_e - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindParameterIndex_SQLite_Net_Interop_IDbStatement_string
	.long LDIFF_SYM127
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde14_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:BindNull"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindNull_SQLite_Net_Interop_IDbStatement_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindNull_SQLite_Net_Interop_IDbStatement_int
	.quad Lme_f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM128=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM128
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM129=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM129
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM130=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM130
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM131=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM131
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM132=Lfde15_end - Lfde15_start
	.long LDIFF_SYM132
Lfde15_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindNull_SQLite_Net_Interop_IDbStatement_int

LDIFF_SYM133=Lme_f - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindNull_SQLite_Net_Interop_IDbStatement_int
	.long LDIFF_SYM133
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde15_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:BindInt"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt_SQLite_Net_Interop_IDbStatement_int_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt_SQLite_Net_Interop_IDbStatement_int_int
	.quad Lme_10

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM134=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM134
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM135=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM135
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM136=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM136
	.byte 2,141,32,3
	.asciz "val"

LDIFF_SYM137=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM137
	.byte 2,141,40,11
	.asciz "V_0"

LDIFF_SYM138=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM138
	.byte 3,141,208,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM139=Lfde16_end - Lfde16_start
	.long LDIFF_SYM139
Lfde16_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt_SQLite_Net_Interop_IDbStatement_int_int

LDIFF_SYM140=Lme_10 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt_SQLite_Net_Interop_IDbStatement_int_int
	.long LDIFF_SYM140
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29
	.align 3
Lfde16_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_11:

	.byte 5
	.asciz "System_Int64"

	.byte 24,16
LDIFF_SYM141=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM141
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM142=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM142
	.byte 2,35,16,0,7
	.asciz "System_Int64"

LDIFF_SYM143=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM143
LTDIE_11_POINTER:

	.byte 13
LDIFF_SYM144=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM144
LTDIE_11_REFERENCE:

	.byte 14
LDIFF_SYM145=LTDIE_11 - Ldebug_info_start
	.long LDIFF_SYM145
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:BindInt64"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt64_SQLite_Net_Interop_IDbStatement_int_long"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt64_SQLite_Net_Interop_IDbStatement_int_long
	.quad Lme_11

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM146=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM146
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM147=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM147
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM148=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM148
	.byte 2,141,32,3
	.asciz "val"

LDIFF_SYM149=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM149
	.byte 2,141,40,11
	.asciz "V_0"

LDIFF_SYM150=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM150
	.byte 3,141,208,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM151=Lfde17_end - Lfde17_start
	.long LDIFF_SYM151
Lfde17_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt64_SQLite_Net_Interop_IDbStatement_int_long

LDIFF_SYM152=Lme_11 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindInt64_SQLite_Net_Interop_IDbStatement_int_long
	.long LDIFF_SYM152
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29
	.align 3
Lfde17_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_12:

	.byte 5
	.asciz "System_Double"

	.byte 24,16
LDIFF_SYM153=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM153
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM154=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM154
	.byte 2,35,16,0,7
	.asciz "System_Double"

LDIFF_SYM155=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM155
LTDIE_12_POINTER:

	.byte 13
LDIFF_SYM156=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM156
LTDIE_12_REFERENCE:

	.byte 14
LDIFF_SYM157=LTDIE_12 - Ldebug_info_start
	.long LDIFF_SYM157
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:BindDouble"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindDouble_SQLite_Net_Interop_IDbStatement_int_double"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindDouble_SQLite_Net_Interop_IDbStatement_int_double
	.quad Lme_12

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM158=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM158
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM159=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM159
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM160=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM160
	.byte 2,141,32,3
	.asciz "val"

LDIFF_SYM161=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM161
	.byte 2,141,40,11
	.asciz "V_0"

LDIFF_SYM162=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM162
	.byte 3,141,208,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM163=Lfde18_end - Lfde18_start
	.long LDIFF_SYM163
Lfde18_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindDouble_SQLite_Net_Interop_IDbStatement_int_double

LDIFF_SYM164=Lme_12 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindDouble_SQLite_Net_Interop_IDbStatement_int_double
	.long LDIFF_SYM164
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29
	.align 3
Lfde18_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:BindText16"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindText16_SQLite_Net_Interop_IDbStatement_int_string_int_intptr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindText16_SQLite_Net_Interop_IDbStatement_int_string_int_intptr
	.quad Lme_13

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM165=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM165
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM166=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM166
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM167=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM167
	.byte 2,141,32,3
	.asciz "val"

LDIFF_SYM168=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM168
	.byte 2,141,40,3
	.asciz "n"

LDIFF_SYM169=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM169
	.byte 2,141,48,3
	.asciz "free"

LDIFF_SYM170=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM170
	.byte 2,141,56,11
	.asciz "V_0"

LDIFF_SYM171=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM171
	.byte 3,141,224,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM172=Lfde19_end - Lfde19_start
	.long LDIFF_SYM172
Lfde19_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindText16_SQLite_Net_Interop_IDbStatement_int_string_int_intptr

LDIFF_SYM173=Lme_13 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindText16_SQLite_Net_Interop_IDbStatement_int_string_int_intptr
	.long LDIFF_SYM173
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29
	.align 3
Lfde19_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:BindBlob"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindBlob_SQLite_Net_Interop_IDbStatement_int_byte___int_intptr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindBlob_SQLite_Net_Interop_IDbStatement_int_byte___int_intptr
	.quad Lme_14

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM174=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM174
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM175=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM175
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM176=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM176
	.byte 2,141,32,3
	.asciz "val"

LDIFF_SYM177=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM177
	.byte 2,141,40,3
	.asciz "n"

LDIFF_SYM178=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM178
	.byte 2,141,48,3
	.asciz "free"

LDIFF_SYM179=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM179
	.byte 2,141,56,11
	.asciz "V_0"

LDIFF_SYM180=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM180
	.byte 3,141,224,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM181=Lfde20_end - Lfde20_start
	.long LDIFF_SYM181
Lfde20_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindBlob_SQLite_Net_Interop_IDbStatement_int_byte___int_intptr

LDIFF_SYM182=Lme_14 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_BindBlob_SQLite_Net_Interop_IDbStatement_int_byte___int_intptr
	.long LDIFF_SYM182
	.long 0
	.byte 12,31,0,68,14,128,1,157,16,158,15,68,13,29
	.align 3
Lfde20_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ColumnCount"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnCount_SQLite_Net_Interop_IDbStatement"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnCount_SQLite_Net_Interop_IDbStatement
	.quad Lme_15

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM183=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM183
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM184=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM184
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM185=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM185
	.byte 3,141,192,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM186=Lfde21_end - Lfde21_start
	.long LDIFF_SYM186
Lfde21_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnCount_SQLite_Net_Interop_IDbStatement

LDIFF_SYM187=Lme_15 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnCount_SQLite_Net_Interop_IDbStatement
	.long LDIFF_SYM187
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde21_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ColumnName16"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnName16_SQLite_Net_Interop_IDbStatement_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnName16_SQLite_Net_Interop_IDbStatement_int
	.quad Lme_16

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM188=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM188
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM189=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM189
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM190=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM190
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM191=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM191
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM192=Lfde22_end - Lfde22_start
	.long LDIFF_SYM192
Lfde22_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnName16_SQLite_Net_Interop_IDbStatement_int

LDIFF_SYM193=Lme_16 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnName16_SQLite_Net_Interop_IDbStatement_int
	.long LDIFF_SYM193
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde22_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ColumnType"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnType_SQLite_Net_Interop_IDbStatement_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnType_SQLite_Net_Interop_IDbStatement_int
	.quad Lme_17

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM194=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM194
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM195=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM195
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM196=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM196
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM197=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM197
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM198=Lfde23_end - Lfde23_start
	.long LDIFF_SYM198
Lfde23_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnType_SQLite_Net_Interop_IDbStatement_int

LDIFF_SYM199=Lme_17 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnType_SQLite_Net_Interop_IDbStatement_int
	.long LDIFF_SYM199
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde23_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ColumnInt"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt_SQLite_Net_Interop_IDbStatement_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt_SQLite_Net_Interop_IDbStatement_int
	.quad Lme_18

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM200=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM200
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM201=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM201
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM202=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM202
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM203=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM203
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM204=Lfde24_end - Lfde24_start
	.long LDIFF_SYM204
Lfde24_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt_SQLite_Net_Interop_IDbStatement_int

LDIFF_SYM205=Lme_18 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt_SQLite_Net_Interop_IDbStatement_int
	.long LDIFF_SYM205
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde24_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ColumnInt64"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt64_SQLite_Net_Interop_IDbStatement_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt64_SQLite_Net_Interop_IDbStatement_int
	.quad Lme_19

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM206=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM206
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM207=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM207
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM208=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM208
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM209=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM209
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM210=Lfde25_end - Lfde25_start
	.long LDIFF_SYM210
Lfde25_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt64_SQLite_Net_Interop_IDbStatement_int

LDIFF_SYM211=Lme_19 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnInt64_SQLite_Net_Interop_IDbStatement_int
	.long LDIFF_SYM211
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde25_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ColumnDouble"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnDouble_SQLite_Net_Interop_IDbStatement_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnDouble_SQLite_Net_Interop_IDbStatement_int
	.quad Lme_1a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM212=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM212
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM213=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM213
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM214=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM214
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM215=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM215
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM216=Lfde26_end - Lfde26_start
	.long LDIFF_SYM216
Lfde26_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnDouble_SQLite_Net_Interop_IDbStatement_int

LDIFF_SYM217=Lme_1a - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnDouble_SQLite_Net_Interop_IDbStatement_int
	.long LDIFF_SYM217
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde26_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ColumnText16"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnText16_SQLite_Net_Interop_IDbStatement_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnText16_SQLite_Net_Interop_IDbStatement_int
	.quad Lme_1b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM218=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM218
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM219=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM219
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM220=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM220
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM221=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM221
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM222=Lfde27_end - Lfde27_start
	.long LDIFF_SYM222
Lfde27_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnText16_SQLite_Net_Interop_IDbStatement_int

LDIFF_SYM223=Lme_1b - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnText16_SQLite_Net_Interop_IDbStatement_int
	.long LDIFF_SYM223
	.long 0
	.byte 12,31,0,68,14,112,157,14,158,13,68,13,29
	.align 3
Lfde27_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:ColumnByteArray"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnByteArray_SQLite_Net_Interop_IDbStatement_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnByteArray_SQLite_Net_Interop_IDbStatement_int
	.quad Lme_1c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM224=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM224
	.byte 2,141,16,3
	.asciz "stmt"

LDIFF_SYM225=LTDIE_10_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM225
	.byte 2,141,24,3
	.asciz "index"

LDIFF_SYM226=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM226
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM227=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM227
	.byte 3,141,200,0,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM228=Lfde28_end - Lfde28_start
	.long LDIFF_SYM228
Lfde28_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnByteArray_SQLite_Net_Interop_IDbStatement_int

LDIFF_SYM229=Lme_1c - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_ColumnByteArray_SQLite_Net_Interop_IDbStatement_int
	.long LDIFF_SYM229
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29
	.align 3
Lfde28_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS:.ctor"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor
	.quad Lme_1d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM230=LTDIE_5_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM230
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM231=Lfde29_end - Lfde29_start
	.long LDIFF_SYM231
Lfde29_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor

LDIFF_SYM232=Lme_1d - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS__ctor
	.long LDIFF_SYM232
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde29_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_13:

	.byte 5
	.asciz "_DbHandle"

	.byte 24,16
LDIFF_SYM233=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM233
	.byte 2,35,0,6
	.asciz "<DbPtr>k__BackingField"

LDIFF_SYM234=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM234
	.byte 2,35,0,0,7
	.asciz "_DbHandle"

LDIFF_SYM235=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM235
LTDIE_13_POINTER:

	.byte 13
LDIFF_SYM236=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM236
LTDIE_13_REFERENCE:

	.byte 14
LDIFF_SYM237=LTDIE_13 - Ldebug_info_start
	.long LDIFF_SYM237
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbHandle:.ctor"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr
	.quad Lme_1e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM238=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM238
	.byte 1,105,3
	.asciz "dbPtr"

LDIFF_SYM239=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM239
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM240=Lfde30_end - Lfde30_start
	.long LDIFF_SYM240
Lfde30_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr

LDIFF_SYM241=Lme_1e - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle__ctor_intptr
	.long LDIFF_SYM241
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,153,6
	.align 3
Lfde30_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbHandle:get_DbPtr"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr
	.quad Lme_1f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM242=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM242
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM243=Lfde31_end - Lfde31_start
	.long LDIFF_SYM243
Lfde31_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr

LDIFF_SYM244=Lme_1f - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_get_DbPtr
	.long LDIFF_SYM244
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde31_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbHandle:set_DbPtr"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr
	.quad Lme_20

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM245=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM245
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM246=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM246
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM247=Lfde32_end - Lfde32_start
	.long LDIFF_SYM247
Lfde32_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr

LDIFF_SYM248=Lme_20 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_set_DbPtr_intptr
	.long LDIFF_SYM248
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde32_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_14:

	.byte 5
	.asciz "_DbStatement"

	.byte 24,16
LDIFF_SYM249=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM249
	.byte 2,35,0,6
	.asciz "<StmtPtr>k__BackingField"

LDIFF_SYM250=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM250
	.byte 2,35,0,0,7
	.asciz "_DbStatement"

LDIFF_SYM251=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM251
LTDIE_14_POINTER:

	.byte 13
LDIFF_SYM252=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM252
LTDIE_14_REFERENCE:

	.byte 14
LDIFF_SYM253=LTDIE_14 - Ldebug_info_start
	.long LDIFF_SYM253
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbStatement:.ctor"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr
	.quad Lme_21

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM254=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM254
	.byte 1,105,3
	.asciz "stmtPtr"

LDIFF_SYM255=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM255
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM256=Lfde33_end - Lfde33_start
	.long LDIFF_SYM256
Lfde33_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr

LDIFF_SYM257=Lme_21 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement__ctor_intptr
	.long LDIFF_SYM257
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,153,6
	.align 3
Lfde33_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbStatement:get_StmtPtr"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr
	.quad Lme_22

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM258=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM258
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM259=Lfde34_end - Lfde34_start
	.long LDIFF_SYM259
Lfde34_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr

LDIFF_SYM260=Lme_22 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_get_StmtPtr
	.long LDIFF_SYM260
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde34_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbStatement:set_StmtPtr"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr
	.quad Lme_23

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM261=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM261
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM262=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM262
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM263=Lfde35_end - Lfde35_start
	.long LDIFF_SYM263
Lfde35_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr

LDIFF_SYM264=Lme_23 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_set_StmtPtr_intptr
	.long LDIFF_SYM264
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde35_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:ColumnByteArray"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int
	.quad Lme_24

	.byte 2,118,16,3
	.asciz "stmt"

LDIFF_SYM265=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM265
	.byte 1,105,3
	.asciz "index"

LDIFF_SYM266=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM266
	.byte 1,106,11
	.asciz "V_0"

LDIFF_SYM267=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM267
	.byte 1,104,11
	.asciz "V_1"

LDIFF_SYM268=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM268
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM269=Lfde36_end - Lfde36_start
	.long LDIFF_SYM269
Lfde36_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int

LDIFF_SYM270=Lme_24 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnByteArray_intptr_int
	.long LDIFF_SYM270
	.long 0
	.byte 12,31,0,68,14,96,157,12,158,11,68,13,29,68,151,10,152,9,68,153,8,154,7
	.align 3
Lfde36_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:ColumnName16"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int
	.quad Lme_25

	.byte 2,118,16,3
	.asciz "stmt"

LDIFF_SYM271=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM271
	.byte 2,141,16,3
	.asciz "index"

LDIFF_SYM272=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM272
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM273=Lfde37_end - Lfde37_start
	.long LDIFF_SYM273
Lfde37_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int

LDIFF_SYM274=Lme_25 - SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_ColumnName16_intptr_int
	.long LDIFF_SYM274
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29
	.align 3
Lfde37_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_16:

	.byte 17
	.asciz "SQLite_Net_Interop_ISQLiteApi"

	.byte 16,7
	.asciz "SQLite_Net_Interop_ISQLiteApi"

LDIFF_SYM275=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM275
LTDIE_16_POINTER:

	.byte 13
LDIFF_SYM276=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM276
LTDIE_16_REFERENCE:

	.byte 14
LDIFF_SYM277=LTDIE_16 - Ldebug_info_start
	.long LDIFF_SYM277
LTDIE_17:

	.byte 17
	.asciz "SQLite_Net_Interop_IStopwatchFactory"

	.byte 16,7
	.asciz "SQLite_Net_Interop_IStopwatchFactory"

LDIFF_SYM278=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM278
LTDIE_17_POINTER:

	.byte 13
LDIFF_SYM279=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM279
LTDIE_17_REFERENCE:

	.byte 14
LDIFF_SYM280=LTDIE_17 - Ldebug_info_start
	.long LDIFF_SYM280
LTDIE_18:

	.byte 17
	.asciz "SQLite_Net_Interop_IReflectionService"

	.byte 16,7
	.asciz "SQLite_Net_Interop_IReflectionService"

LDIFF_SYM281=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM281
LTDIE_18_POINTER:

	.byte 13
LDIFF_SYM282=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM282
LTDIE_18_REFERENCE:

	.byte 14
LDIFF_SYM283=LTDIE_18 - Ldebug_info_start
	.long LDIFF_SYM283
LTDIE_19:

	.byte 17
	.asciz "SQLite_Net_Interop_IVolatileService"

	.byte 16,7
	.asciz "SQLite_Net_Interop_IVolatileService"

LDIFF_SYM284=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM284
LTDIE_19_POINTER:

	.byte 13
LDIFF_SYM285=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM285
LTDIE_19_REFERENCE:

	.byte 14
LDIFF_SYM286=LTDIE_19 - Ldebug_info_start
	.long LDIFF_SYM286
LTDIE_15:

	.byte 5
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS"

	.byte 48,16
LDIFF_SYM287=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM287
	.byte 2,35,0,6
	.asciz "<SQLiteApi>k__BackingField"

LDIFF_SYM288=LTDIE_16_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM288
	.byte 2,35,16,6
	.asciz "<StopwatchFactory>k__BackingField"

LDIFF_SYM289=LTDIE_17_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM289
	.byte 2,35,24,6
	.asciz "<ReflectionService>k__BackingField"

LDIFF_SYM290=LTDIE_18_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM290
	.byte 2,35,32,6
	.asciz "<VolatileService>k__BackingField"

LDIFF_SYM291=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM291
	.byte 2,35,40,0,7
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS"

LDIFF_SYM292=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM292
LTDIE_15_POINTER:

	.byte 13
LDIFF_SYM293=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM293
LTDIE_15_REFERENCE:

	.byte 14
LDIFF_SYM294=LTDIE_15 - Ldebug_info_start
	.long LDIFF_SYM294
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:.ctor"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS__ctor"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS__ctor
	.quad Lme_41

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM295=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM295
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM296=Lfde38_end - Lfde38_start
	.long LDIFF_SYM296
Lfde38_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS__ctor

LDIFF_SYM297=Lme_41 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS__ctor
	.long LDIFF_SYM297
	.long 0
	.byte 12,31,0,68,14,80,157,10,158,9,68,13,29,68,154,8
	.align 3
Lfde38_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:get_SQLiteApi"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_SQLiteApi"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_SQLiteApi
	.quad Lme_42

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM298=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM298
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM299=Lfde39_end - Lfde39_start
	.long LDIFF_SYM299
Lfde39_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_SQLiteApi

LDIFF_SYM300=Lme_42 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_SQLiteApi
	.long LDIFF_SYM300
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde39_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:set_SQLiteApi"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi
	.quad Lme_43

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM301=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM301
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM302=LTDIE_16_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM302
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM303=Lfde40_end - Lfde40_start
	.long LDIFF_SYM303
Lfde40_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi

LDIFF_SYM304=Lme_43 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_SQLiteApi_SQLite_Net_Interop_ISQLiteApi
	.long LDIFF_SYM304
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde40_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:get_StopwatchFactory"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_StopwatchFactory"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_StopwatchFactory
	.quad Lme_44

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM305=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM305
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM306=Lfde41_end - Lfde41_start
	.long LDIFF_SYM306
Lfde41_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_StopwatchFactory

LDIFF_SYM307=Lme_44 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_StopwatchFactory
	.long LDIFF_SYM307
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde41_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:set_StopwatchFactory"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory
	.quad Lme_45

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM308=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM308
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM309=LTDIE_17_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM309
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM310=Lfde42_end - Lfde42_start
	.long LDIFF_SYM310
Lfde42_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory

LDIFF_SYM311=Lme_45 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_StopwatchFactory_SQLite_Net_Interop_IStopwatchFactory
	.long LDIFF_SYM311
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde42_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:get_ReflectionService"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_ReflectionService"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_ReflectionService
	.quad Lme_46

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM312=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM312
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM313=Lfde43_end - Lfde43_start
	.long LDIFF_SYM313
Lfde43_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_ReflectionService

LDIFF_SYM314=Lme_46 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_ReflectionService
	.long LDIFF_SYM314
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde43_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:set_ReflectionService"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService
	.quad Lme_47

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM315=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM315
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM316=LTDIE_18_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM316
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM317=Lfde44_end - Lfde44_start
	.long LDIFF_SYM317
Lfde44_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService

LDIFF_SYM318=Lme_47 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_ReflectionService_SQLite_Net_Interop_IReflectionService
	.long LDIFF_SYM318
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde44_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:get_VolatileService"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_VolatileService"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_VolatileService
	.quad Lme_48

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM319=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM319
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM320=Lfde45_end - Lfde45_start
	.long LDIFF_SYM320
Lfde45_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_VolatileService

LDIFF_SYM321=Lme_48 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_get_VolatileService
	.long LDIFF_SYM321
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde45_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS:set_VolatileService"
	.asciz "SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService
	.quad Lme_49

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM322=LTDIE_15_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM322
	.byte 2,141,16,3
	.asciz "value"

LDIFF_SYM323=LTDIE_19_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM323
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM324=Lfde46_end - Lfde46_start
	.long LDIFF_SYM324
Lfde46_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService

LDIFF_SYM325=Lme_49 - SQLite_Net_Platform_XamarinIOS_SQLitePlatformIOS_set_VolatileService_SQLite_Net_Interop_IVolatileService
	.long LDIFF_SYM325
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde46_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_20:

	.byte 5
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS"

	.byte 16,16
LDIFF_SYM326=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM326
	.byte 2,35,0,0,7
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS"

LDIFF_SYM327=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM327
LTDIE_20_POINTER:

	.byte 13
LDIFF_SYM328=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM328
LTDIE_20_REFERENCE:

	.byte 14
LDIFF_SYM329=LTDIE_20 - Ldebug_info_start
	.long LDIFF_SYM329
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.StopwatchFactoryIOS:Create"
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_Create"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_Create
	.quad Lme_4a

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM330=LTDIE_20_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM330
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM331=Lfde47_end - Lfde47_start
	.long LDIFF_SYM331
Lfde47_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_Create

LDIFF_SYM332=Lme_4a - SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_Create
	.long LDIFF_SYM332
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde47_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.StopwatchFactoryIOS:.ctor"
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor
	.quad Lme_4b

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM333=LTDIE_20_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM333
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM334=Lfde48_end - Lfde48_start
	.long LDIFF_SYM334
Lfde48_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor

LDIFF_SYM335=Lme_4b - SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS__ctor
	.long LDIFF_SYM335
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde48_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_23:

	.byte 5
	.asciz "System_Boolean"

	.byte 17,16
LDIFF_SYM336=LTDIE_8 - Ldebug_info_start
	.long LDIFF_SYM336
	.byte 2,35,0,6
	.asciz "m_value"

LDIFF_SYM337=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM337
	.byte 2,35,16,0,7
	.asciz "System_Boolean"

LDIFF_SYM338=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM338
LTDIE_23_POINTER:

	.byte 13
LDIFF_SYM339=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM339
LTDIE_23_REFERENCE:

	.byte 14
LDIFF_SYM340=LTDIE_23 - Ldebug_info_start
	.long LDIFF_SYM340
LTDIE_22:

	.byte 5
	.asciz "System_Diagnostics_Stopwatch"

	.byte 40,16
LDIFF_SYM341=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM341
	.byte 2,35,0,6
	.asciz "elapsed"

LDIFF_SYM342=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM342
	.byte 2,35,16,6
	.asciz "started"

LDIFF_SYM343=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM343
	.byte 2,35,24,6
	.asciz "is_running"

LDIFF_SYM344=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM344
	.byte 2,35,32,0,7
	.asciz "System_Diagnostics_Stopwatch"

LDIFF_SYM345=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM345
LTDIE_22_POINTER:

	.byte 13
LDIFF_SYM346=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM346
LTDIE_22_REFERENCE:

	.byte 14
LDIFF_SYM347=LTDIE_22 - Ldebug_info_start
	.long LDIFF_SYM347
LTDIE_21:

	.byte 5
	.asciz "_StopwatchIOS"

	.byte 24,16
LDIFF_SYM348=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM348
	.byte 2,35,0,6
	.asciz "_stopWatch"

LDIFF_SYM349=LTDIE_22_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM349
	.byte 2,35,16,0,7
	.asciz "_StopwatchIOS"

LDIFF_SYM350=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM350
LTDIE_21_POINTER:

	.byte 13
LDIFF_SYM351=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM351
LTDIE_21_REFERENCE:

	.byte 14
LDIFF_SYM352=LTDIE_21 - Ldebug_info_start
	.long LDIFF_SYM352
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.StopwatchFactoryIOS/StopwatchIOS:.ctor"
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor
	.quad Lme_4c

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM353=LTDIE_21_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM353
	.byte 1,106,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM354=Lfde49_end - Lfde49_start
	.long LDIFF_SYM354
Lfde49_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor

LDIFF_SYM355=Lme_4c - SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS__ctor
	.long LDIFF_SYM355
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29,68,154,6
	.align 3
Lfde49_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.StopwatchFactoryIOS/StopwatchIOS:Stop"
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Stop"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Stop
	.quad Lme_4d

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM356=LTDIE_21_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM356
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM357=Lfde50_end - Lfde50_start
	.long LDIFF_SYM357
Lfde50_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Stop

LDIFF_SYM358=Lme_4d - SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Stop
	.long LDIFF_SYM358
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde50_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.StopwatchFactoryIOS/StopwatchIOS:Reset"
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Reset"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Reset
	.quad Lme_4e

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM359=LTDIE_21_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM359
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM360=Lfde51_end - Lfde51_start
	.long LDIFF_SYM360
Lfde51_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Reset

LDIFF_SYM361=Lme_4e - SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Reset
	.long LDIFF_SYM361
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde51_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.StopwatchFactoryIOS/StopwatchIOS:Start"
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Start"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Start
	.quad Lme_4f

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM362=LTDIE_21_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM362
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM363=Lfde52_end - Lfde52_start
	.long LDIFF_SYM363
Lfde52_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Start

LDIFF_SYM364=Lme_4f - SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_Start
	.long LDIFF_SYM364
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde52_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.StopwatchFactoryIOS/StopwatchIOS:get_ElapsedMilliseconds"
	.asciz "SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_get_ElapsedMilliseconds"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_get_ElapsedMilliseconds
	.quad Lme_50

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM365=LTDIE_21_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM365
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM366=Lfde53_end - Lfde53_start
	.long LDIFF_SYM366
Lfde53_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_get_ElapsedMilliseconds

LDIFF_SYM367=Lme_50 - SQLite_Net_Platform_XamarinIOS_StopwatchFactoryIOS_StopwatchIOS_get_ElapsedMilliseconds
	.long LDIFF_SYM367
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde53_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_24:

	.byte 5
	.asciz "SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS"

	.byte 16,16
LDIFF_SYM368=LTDIE_1 - Ldebug_info_start
	.long LDIFF_SYM368
	.byte 2,35,0,0,7
	.asciz "SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS"

LDIFF_SYM369=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM369
LTDIE_24_POINTER:

	.byte 13
LDIFF_SYM370=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM370
LTDIE_24_REFERENCE:

	.byte 14
LDIFF_SYM371=LTDIE_24 - Ldebug_info_start
	.long LDIFF_SYM371
	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.VolatileServiceIOS:Write"
	.asciz "SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS_Write_int__int"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS_Write_int__int
	.quad Lme_51

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM372=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM372
	.byte 2,141,16,3
	.asciz "transactionDepth"

LDIFF_SYM373=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM373
	.byte 2,141,24,3
	.asciz "depth"

LDIFF_SYM374=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM374
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM375=Lfde54_end - Lfde54_start
	.long LDIFF_SYM375
Lfde54_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS_Write_int__int

LDIFF_SYM376=Lme_51 - SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS_Write_int__int
	.long LDIFF_SYM376
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde54_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "SQLite.Net.Platform.XamarinIOS.VolatileServiceIOS:.ctor"
	.asciz "SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor"

	.byte 0,0
	.quad SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor
	.quad Lme_52

	.byte 2,118,16,3
	.asciz "this"

LDIFF_SYM377=LTDIE_24_REFERENCE - Ldebug_info_start
	.long LDIFF_SYM377
	.byte 2,141,16,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM378=Lfde55_end - Lfde55_start
	.long LDIFF_SYM378
Lfde55_start:

	.long 0
	.align 3
	.quad SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor

LDIFF_SYM379=Lme_52 - SQLite_Net_Platform_XamarinIOS_VolatileServiceIOS__ctor
	.long LDIFF_SYM379
	.long 0
	.byte 12,31,0,68,14,48,157,6,158,5,68,13,29
	.align 3
Lfde55_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_bind_blob"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr
	.quad Lme_54

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM380=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM380
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM381=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM381
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM382=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM382
	.byte 2,141,32,3
	.asciz "param3"

LDIFF_SYM383=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM383
	.byte 2,141,40,3
	.asciz "param4"

LDIFF_SYM384=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM384
	.byte 2,141,48,11
	.asciz "V_0"

LDIFF_SYM385=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM385
	.byte 3,141,200,1,11
	.asciz "V_1"

LDIFF_SYM386=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM386
	.byte 3,141,208,1,11
	.asciz "V_2"

LDIFF_SYM387=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM387
	.byte 3,141,216,1,11
	.asciz "V_3"

LDIFF_SYM388=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM388
	.byte 1,100,11
	.asciz "V_4"

LDIFF_SYM389=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM389
	.byte 1,99,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM390=Lfde56_end - Lfde56_start
	.long LDIFF_SYM390
Lfde56_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr

LDIFF_SYM391=Lme_54 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_blob_intptr_int_byte___int_intptr
	.long LDIFF_SYM391
	.long 0
	.byte 12,31,0,68,14,240,1,157,30,158,29,68,13,29,76,147,17,148,16,68,149,15,150,14,68,151,13,152,12,68,153,11
	.byte 154,10,68,155,9,156,8
	.align 3
Lfde56_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_bind_double"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double
	.quad Lme_55

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM392=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM392
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM393=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM393
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM394=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM394
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM395=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM395
	.byte 3,141,184,1,11
	.asciz "V_1"

LDIFF_SYM396=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM396
	.byte 3,141,192,1,11
	.asciz "V_2"

LDIFF_SYM397=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM397
	.byte 3,141,200,1,11
	.asciz "V_3"

LDIFF_SYM398=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM398
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM399=Lfde57_end - Lfde57_start
	.long LDIFF_SYM399
Lfde57_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double

LDIFF_SYM400=Lme_55 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_double_intptr_int_double
	.long LDIFF_SYM400
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde57_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_bind_int"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int
	.quad Lme_56

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM401=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM401
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM402=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM402
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM403=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM403
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM404=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM404
	.byte 3,141,184,1,11
	.asciz "V_1"

LDIFF_SYM405=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM405
	.byte 3,141,192,1,11
	.asciz "V_2"

LDIFF_SYM406=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM406
	.byte 3,141,200,1,11
	.asciz "V_3"

LDIFF_SYM407=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM407
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM408=Lfde58_end - Lfde58_start
	.long LDIFF_SYM408
Lfde58_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int

LDIFF_SYM409=Lme_56 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int_intptr_int_int
	.long LDIFF_SYM409
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde58_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_bind_int64"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long
	.quad Lme_57

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM410=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM410
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM411=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM411
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM412=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM412
	.byte 2,141,32,11
	.asciz "V_0"

LDIFF_SYM413=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM413
	.byte 3,141,184,1,11
	.asciz "V_1"

LDIFF_SYM414=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM414
	.byte 3,141,192,1,11
	.asciz "V_2"

LDIFF_SYM415=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM415
	.byte 3,141,200,1,11
	.asciz "V_3"

LDIFF_SYM416=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM416
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM417=Lfde59_end - Lfde59_start
	.long LDIFF_SYM417
Lfde59_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long

LDIFF_SYM418=Lme_57 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_int64_intptr_int_long
	.long LDIFF_SYM418
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde59_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_bind_null"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int
	.quad Lme_58

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM419=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM419
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM420=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM420
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM421=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM421
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM422=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM422
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM423=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM423
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM424=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM424
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM425=Lfde60_end - Lfde60_start
	.long LDIFF_SYM425
Lfde60_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int

LDIFF_SYM426=Lme_58 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_null_intptr_int
	.long LDIFF_SYM426
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde60_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_bind_parameter_index"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string
	.quad Lme_59

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM427=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM427
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM428=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM428
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM429=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM429
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM430=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM430
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM431=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM431
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM432=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM432
	.byte 1,103,11
	.asciz "V_4"

LDIFF_SYM433=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM433
	.byte 1,102,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM434=Lfde61_end - Lfde61_start
	.long LDIFF_SYM434
Lfde61_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string

LDIFF_SYM435=Lme_59 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_parameter_index_intptr_string
	.long LDIFF_SYM435
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde61_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_bind_text16"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr
	.quad Lme_5a

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM436=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM436
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM437=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM437
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM438=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM438
	.byte 2,141,32,3
	.asciz "param3"

LDIFF_SYM439=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM439
	.byte 2,141,40,3
	.asciz "param4"

LDIFF_SYM440=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM440
	.byte 2,141,48,11
	.asciz "V_0"

LDIFF_SYM441=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM441
	.byte 3,141,200,1,11
	.asciz "V_1"

LDIFF_SYM442=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM442
	.byte 3,141,208,1,11
	.asciz "V_2"

LDIFF_SYM443=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM443
	.byte 3,141,216,1,11
	.asciz "V_3"

LDIFF_SYM444=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM444
	.byte 3,141,224,1,11
	.asciz "V_4"

LDIFF_SYM445=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM445
	.byte 1,99,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM446=Lfde62_end - Lfde62_start
	.long LDIFF_SYM446
Lfde62_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr

LDIFF_SYM447=Lme_5a - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_bind_text16_intptr_int_string_int_intptr
	.long LDIFF_SYM447
	.long 0
	.byte 12,31,0,68,14,128,2,157,32,158,31,68,13,29,76,147,19,148,18,68,149,17,150,16,68,151,15,152,14,68,153,13
	.byte 154,12,68,155,11,156,10
	.align 3
Lfde62_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_busy_timeout"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int
	.quad Lme_5b

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM448=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM448
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM449=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM449
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM450=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM450
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM451=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM451
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM452=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM452
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM453=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM453
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM454=Lfde63_end - Lfde63_start
	.long LDIFF_SYM454
Lfde63_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int

LDIFF_SYM455=Lme_5b - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_busy_timeout_intptr_int
	.long LDIFF_SYM455
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde63_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_changes"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr
	.quad Lme_5c

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM456=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM456
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM457=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM457
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM458=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM458
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM459=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM459
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM460=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM460
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM461=Lfde64_end - Lfde64_start
	.long LDIFF_SYM461
Lfde64_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr

LDIFF_SYM462=Lme_5c - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_changes_intptr
	.long LDIFF_SYM462
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde64_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_close"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr
	.quad Lme_5d

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM463=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM463
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM464=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM464
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM465=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM465
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM466=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM466
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM467=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM467
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM468=Lfde65_end - Lfde65_start
	.long LDIFF_SYM468
Lfde65_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr

LDIFF_SYM469=Lme_5d - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_close_intptr
	.long LDIFF_SYM469
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde65_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_blob"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int
	.quad Lme_5e

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM470=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM470
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM471=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM471
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM472=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM472
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM473=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM473
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM474=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM474
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM475=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM475
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM476=Lfde66_end - Lfde66_start
	.long LDIFF_SYM476
Lfde66_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int

LDIFF_SYM477=Lme_5e - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_blob_intptr_int
	.long LDIFF_SYM477
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde66_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_bytes"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int
	.quad Lme_5f

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM478=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM478
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM479=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM479
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM480=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM480
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM481=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM481
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM482=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM482
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM483=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM483
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM484=Lfde67_end - Lfde67_start
	.long LDIFF_SYM484
Lfde67_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int

LDIFF_SYM485=Lme_5f - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_bytes_intptr_int
	.long LDIFF_SYM485
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde67_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_count"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr
	.quad Lme_60

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM486=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM486
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM487=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM487
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM488=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM488
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM489=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM489
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM490=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM490
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM491=Lfde68_end - Lfde68_start
	.long LDIFF_SYM491
Lfde68_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr

LDIFF_SYM492=Lme_60 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_count_intptr
	.long LDIFF_SYM492
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde68_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_double"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int
	.quad Lme_61

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM493=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM493
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM494=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM494
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM495=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM495
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM496=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM496
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM497=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM497
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM498=LDIE_R8 - Ldebug_info_start
	.long LDIFF_SYM498
	.byte 3,141,200,1,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM499=Lfde69_end - Lfde69_start
	.long LDIFF_SYM499
Lfde69_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int

LDIFF_SYM500=Lme_61 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_double_intptr_int
	.long LDIFF_SYM500
	.long 0
	.byte 12,31,0,68,14,224,1,157,28,158,27,68,13,29,76,147,18,148,17,68,149,16,150,15,68,151,14,152,13,68,153,12
	.byte 154,11,68,155,10,156,9
	.align 3
Lfde69_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_int"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int
	.quad Lme_62

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM501=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM501
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM502=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM502
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM503=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM503
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM504=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM504
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM505=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM505
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM506=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM506
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM507=Lfde70_end - Lfde70_start
	.long LDIFF_SYM507
Lfde70_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int

LDIFF_SYM508=Lme_62 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int_intptr_int
	.long LDIFF_SYM508
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde70_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_int64"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int
	.quad Lme_63

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM509=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM509
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM510=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM510
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM511=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM511
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM512=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM512
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM513=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM513
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM514=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM514
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM515=Lfde71_end - Lfde71_start
	.long LDIFF_SYM515
Lfde71_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int

LDIFF_SYM516=Lme_63 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_int64_intptr_int
	.long LDIFF_SYM516
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde71_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_text16"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int
	.quad Lme_64

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM517=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM517
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM518=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM518
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM519=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM519
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM520=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM520
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM521=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM521
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM522=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM522
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM523=Lfde72_end - Lfde72_start
	.long LDIFF_SYM523
Lfde72_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int

LDIFF_SYM524=Lme_64 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_text16_intptr_int
	.long LDIFF_SYM524
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde72_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_25:

	.byte 8
	.asciz "SQLite_Net_Interop_ColType"

	.byte 4
LDIFF_SYM525=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM525
	.byte 9
	.asciz "Integer"

	.byte 1,9
	.asciz "Float"

	.byte 2,9
	.asciz "Text"

	.byte 3,9
	.asciz "Blob"

	.byte 4,9
	.asciz "Null"

	.byte 5,0,7
	.asciz "SQLite_Net_Interop_ColType"

LDIFF_SYM526=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM526
LTDIE_25_POINTER:

	.byte 13
LDIFF_SYM527=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM527
LTDIE_25_REFERENCE:

	.byte 14
LDIFF_SYM528=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM528
	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_type"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int
	.quad Lme_65

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM529=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM529
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM530=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM530
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM531=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM531
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM532=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM532
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM533=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM533
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM534=LTDIE_25 - Ldebug_info_start
	.long LDIFF_SYM534
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM535=Lfde73_end - Lfde73_start
	.long LDIFF_SYM535
Lfde73_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int

LDIFF_SYM536=Lme_65 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_type_intptr_int
	.long LDIFF_SYM536
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde73_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_errmsg16"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr
	.quad Lme_66

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM537=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM537
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM538=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM538
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM539=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM539
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM540=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM540
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM541=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM541
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM542=Lfde74_end - Lfde74_start
	.long LDIFF_SYM542
Lfde74_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr

LDIFF_SYM543=Lme_66 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_errmsg16_intptr
	.long LDIFF_SYM543
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde74_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_finalize"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr
	.quad Lme_67

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM544=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM544
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM545=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM545
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM546=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM546
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM547=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM547
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM548=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM548
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM549=Lfde75_end - Lfde75_start
	.long LDIFF_SYM549
Lfde75_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr

LDIFF_SYM550=Lme_67 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_finalize_intptr
	.long LDIFF_SYM550
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde75_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_last_insert_rowid"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr
	.quad Lme_68

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM551=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM551
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM552=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM552
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM553=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM553
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM554=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM554
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM555=LDIE_I8 - Ldebug_info_start
	.long LDIFF_SYM555
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM556=Lfde76_end - Lfde76_start
	.long LDIFF_SYM556
Lfde76_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr

LDIFF_SYM557=Lme_68 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_last_insert_rowid_intptr
	.long LDIFF_SYM557
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde76_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_open_v2"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr
	.quad Lme_69

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM558=LDIE_SZARRAY - Ldebug_info_start
	.long LDIFF_SYM558
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM559=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM559
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM560=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM560
	.byte 2,141,32,3
	.asciz "param3"

LDIFF_SYM561=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM561
	.byte 2,141,40,11
	.asciz "V_0"

LDIFF_SYM562=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM562
	.byte 3,141,192,1,11
	.asciz "V_1"

LDIFF_SYM563=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM563
	.byte 3,141,200,1,11
	.asciz "V_2"

LDIFF_SYM564=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM564
	.byte 3,141,208,1,11
	.asciz "V_3"

LDIFF_SYM565=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM565
	.byte 1,101,11
	.asciz "V_4"

LDIFF_SYM566=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM566
	.byte 1,100,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM567=Lfde77_end - Lfde77_start
	.long LDIFF_SYM567
Lfde77_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr

LDIFF_SYM568=Lme_69 - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_open_v2_byte___intptr__int_intptr
	.long LDIFF_SYM568
	.long 0
	.byte 12,31,0,68,14,224,1,157,28,158,27,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde77_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_prepare_v2"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr
	.quad Lme_6a

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM569=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM569
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM570=LDIE_STRING - Ldebug_info_start
	.long LDIFF_SYM570
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM571=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM571
	.byte 2,141,32,3
	.asciz "param3"

LDIFF_SYM572=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM572
	.byte 2,141,40,3
	.asciz "param4"

LDIFF_SYM573=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM573
	.byte 2,141,48,11
	.asciz "V_0"

LDIFF_SYM574=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM574
	.byte 3,141,200,1,11
	.asciz "V_1"

LDIFF_SYM575=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM575
	.byte 3,141,208,1,11
	.asciz "V_2"

LDIFF_SYM576=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM576
	.byte 3,141,216,1,11
	.asciz "V_3"

LDIFF_SYM577=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM577
	.byte 3,141,224,1,11
	.asciz "V_4"

LDIFF_SYM578=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM578
	.byte 1,99,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM579=Lfde78_end - Lfde78_start
	.long LDIFF_SYM579
Lfde78_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr

LDIFF_SYM580=Lme_6a - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_prepare_v2_intptr_string_int_intptr__intptr
	.long LDIFF_SYM580
	.long 0
	.byte 12,31,0,68,14,144,2,157,34,158,33,68,13,29,76,147,21,148,20,68,149,19,150,18,68,151,17,152,16,68,153,15
	.byte 154,14,68,155,13,156,12
	.align 3
Lfde78_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_reset"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr
	.quad Lme_6b

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM581=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM581
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM582=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM582
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM583=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM583
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM584=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM584
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM585=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM585
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM586=Lfde79_end - Lfde79_start
	.long LDIFF_SYM586
Lfde79_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr

LDIFF_SYM587=Lme_6b - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_reset_intptr
	.long LDIFF_SYM587
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde79_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_step"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr
	.quad Lme_6c

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM588=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM588
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM589=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM589
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM590=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM590
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM591=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM591
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM592=LTDIE_9 - Ldebug_info_start
	.long LDIFF_SYM592
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM593=Lfde80_end - Lfde80_start
	.long LDIFF_SYM593
Lfde80_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr

LDIFF_SYM594=Lme_6c - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_step_intptr
	.long LDIFF_SYM594
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde80_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_column_name16"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int
	.quad Lme_6d

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM595=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM595
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM596=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM596
	.byte 2,141,24,11
	.asciz "V_0"

LDIFF_SYM597=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM597
	.byte 3,141,176,1,11
	.asciz "V_1"

LDIFF_SYM598=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM598
	.byte 3,141,184,1,11
	.asciz "V_2"

LDIFF_SYM599=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM599
	.byte 3,141,192,1,11
	.asciz "V_3"

LDIFF_SYM600=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM600
	.byte 1,103,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM601=Lfde81_end - Lfde81_start
	.long LDIFF_SYM601
Lfde81_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int

LDIFF_SYM602=Lme_6d - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_column_name16_intptr_int
	.long LDIFF_SYM602
	.long 0
	.byte 12,31,0,68,14,208,1,157,26,158,25,68,13,29,76,147,16,148,15,68,149,14,150,13,68,151,12,152,11,68,153,10
	.byte 154,9,68,155,8,156,7
	.align 3
Lfde81_end:

.section __DWARF, __debug_info,regular,debug
LTDIE_26:

	.byte 8
	.asciz "SQLite_Net_Interop_ExtendedResult"

	.byte 4
LDIFF_SYM603=LDIE_I4 - Ldebug_info_start
	.long LDIFF_SYM603
	.byte 9
	.asciz "IOErrorRead"

	.byte 138,2,9
	.asciz "IOErrorShortRead"

	.byte 138,4,9
	.asciz "IOErrorWrite"

	.byte 138,6,9
	.asciz "IOErrorFsync"

	.byte 138,8,9
	.asciz "IOErrorDirFSync"

	.byte 138,10,9
	.asciz "IOErrorTruncate"

	.byte 138,12,9
	.asciz "IOErrorFStat"

	.byte 138,14,9
	.asciz "IOErrorUnlock"

	.byte 138,16,9
	.asciz "IOErrorRdlock"

	.byte 138,18,9
	.asciz "IOErrorDelete"

	.byte 138,20,9
	.asciz "IOErrorBlocked"

	.byte 138,22,9
	.asciz "IOErrorNoMem"

	.byte 138,24,9
	.asciz "IOErrorAccess"

	.byte 138,26,9
	.asciz "IOErrorCheckReservedLock"

	.byte 138,28,9
	.asciz "IOErrorLock"

	.byte 138,30,9
	.asciz "IOErrorClose"

	.byte 138,32,9
	.asciz "IOErrorDirClose"

	.byte 138,34,9
	.asciz "IOErrorSHMOpen"

	.byte 138,36,9
	.asciz "IOErrorSHMSize"

	.byte 138,38,9
	.asciz "IOErrorSHMLock"

	.byte 138,40,9
	.asciz "IOErrorSHMMap"

	.byte 138,42,9
	.asciz "IOErrorSeek"

	.byte 138,44,9
	.asciz "IOErrorDeleteNoEnt"

	.byte 138,46,9
	.asciz "IOErrorMMap"

	.byte 138,48,9
	.asciz "LockedSharedcache"

	.byte 134,2,9
	.asciz "BusyRecovery"

	.byte 133,2,9
	.asciz "CannottOpenNoTempDir"

	.byte 142,2,9
	.asciz "CannotOpenIsDir"

	.byte 142,4,9
	.asciz "CannotOpenFullPath"

	.byte 142,6,9
	.asciz "CorruptVTab"

	.byte 139,2,9
	.asciz "ReadonlyRecovery"

	.byte 136,2,9
	.asciz "ReadonlyCannotLock"

	.byte 136,4,9
	.asciz "ReadonlyRollback"

	.byte 136,6,9
	.asciz "AbortRollback"

	.byte 132,4,9
	.asciz "ConstraintCheck"

	.byte 147,2,9
	.asciz "ConstraintCommitHook"

	.byte 147,4,9
	.asciz "ConstraintForeignKey"

	.byte 147,6,9
	.asciz "ConstraintFunction"

	.byte 147,8,9
	.asciz "ConstraintNotNull"

	.byte 147,10,9
	.asciz "ConstraintPrimaryKey"

	.byte 147,12,9
	.asciz "ConstraintTrigger"

	.byte 147,14,9
	.asciz "ConstraintUnique"

	.byte 147,16,9
	.asciz "ConstraintVTab"

	.byte 147,18,9
	.asciz "NoticeRecoverWAL"

	.byte 155,2,9
	.asciz "NoticeRecoverRollback"

	.byte 155,4,0,7
	.asciz "SQLite_Net_Interop_ExtendedResult"

LDIFF_SYM604=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM604
LTDIE_26_POINTER:

	.byte 13
LDIFF_SYM605=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM605
LTDIE_26_REFERENCE:

	.byte 14
LDIFF_SYM606=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM606
	.byte 2
	.asciz "(wrapper_managed-to-native)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOSInternal:sqlite3_extended_errcode"
	.asciz "wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr"

	.byte 0,0
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr
	.quad Lme_6e

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM607=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM607
	.byte 2,141,16,11
	.asciz "V_0"

LDIFF_SYM608=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM608
	.byte 3,141,168,1,11
	.asciz "V_1"

LDIFF_SYM609=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM609
	.byte 3,141,176,1,11
	.asciz "V_2"

LDIFF_SYM610=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM610
	.byte 3,141,184,1,11
	.asciz "V_3"

LDIFF_SYM611=LTDIE_26 - Ldebug_info_start
	.long LDIFF_SYM611
	.byte 1,104,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM612=Lfde82_end - Lfde82_start
	.long LDIFF_SYM612
Lfde82_start:

	.long 0
	.align 3
	.quad wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr

LDIFF_SYM613=Lme_6e - wrapper_managed_to_native_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOSInternal_sqlite3_extended_errcode_intptr
	.long LDIFF_SYM613
	.long 0
	.byte 12,31,0,68,14,192,1,157,24,158,23,68,13,29,76,147,15,148,14,68,149,13,150,12,68,151,11,152,10,68,153,9
	.byte 154,8,68,155,7,156,6
	.align 3
Lfde82_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbHandle:StructureToPtr"
	.asciz "wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_StructureToPtr_object_intptr_bool"

	.byte 0,0
	.quad wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_StructureToPtr_object_intptr_bool
	.quad Lme_6f

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM614=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM614
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM615=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM615
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM616=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM616
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM617=Lfde83_end - Lfde83_start
	.long LDIFF_SYM617
Lfde83_start:

	.long 0
	.align 3
	.quad wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_StructureToPtr_object_intptr_bool

LDIFF_SYM618=Lme_6f - wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_StructureToPtr_object_intptr_bool
	.long LDIFF_SYM618
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde83_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbHandle:PtrToStructure"
	.asciz "wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_PtrToStructure_intptr_object"

	.byte 0,0
	.quad wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_PtrToStructure_intptr_object
	.quad Lme_70

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM619=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM619
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM620=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM620
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM621=Lfde84_end - Lfde84_start
	.long LDIFF_SYM621
Lfde84_start:

	.long 0
	.align 3
	.quad wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_PtrToStructure_intptr_object

LDIFF_SYM622=Lme_70 - wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbHandle_PtrToStructure_intptr_object
	.long LDIFF_SYM622
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde84_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbStatement:StructureToPtr"
	.asciz "wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_StructureToPtr_object_intptr_bool"

	.byte 0,0
	.quad wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_StructureToPtr_object_intptr_bool
	.quad Lme_71

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM623=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM623
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM624=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM624
	.byte 2,141,24,3
	.asciz "param2"

LDIFF_SYM625=LDIE_BOOLEAN - Ldebug_info_start
	.long LDIFF_SYM625
	.byte 2,141,32,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM626=Lfde85_end - Lfde85_start
	.long LDIFF_SYM626
Lfde85_start:

	.long 0
	.align 3
	.quad wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_StructureToPtr_object_intptr_bool

LDIFF_SYM627=Lme_71 - wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_StructureToPtr_object_intptr_bool
	.long LDIFF_SYM627
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde85_end:

.section __DWARF, __debug_info,regular,debug

	.byte 2
	.asciz "(wrapper_unknown)_SQLite.Net.Platform.XamarinIOS.SQLiteApiIOS/DbStatement:PtrToStructure"
	.asciz "wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_PtrToStructure_intptr_object"

	.byte 0,0
	.quad wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_PtrToStructure_intptr_object
	.quad Lme_72

	.byte 2,118,16,3
	.asciz "param0"

LDIFF_SYM628=LDIE_I - Ldebug_info_start
	.long LDIFF_SYM628
	.byte 2,141,16,3
	.asciz "param1"

LDIFF_SYM629=LDIE_OBJECT - Ldebug_info_start
	.long LDIFF_SYM629
	.byte 2,141,24,0

.section __DWARF, __debug_frame,regular,debug

LDIFF_SYM630=Lfde86_end - Lfde86_start
	.long LDIFF_SYM630
Lfde86_start:

	.long 0
	.align 3
	.quad wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_PtrToStructure_intptr_object

LDIFF_SYM631=Lme_72 - wrapper_unknown_SQLite_Net_Platform_XamarinIOS_SQLiteApiIOS_DbStatement_PtrToStructure_intptr_object
	.long LDIFF_SYM631
	.long 0
	.byte 12,31,0,68,14,64,157,8,158,7,68,13,29
	.align 3
Lfde86_end:

.section __DWARF, __debug_info,regular,debug

	.byte 0
Ldebug_info_end:
.text
	.align 3
mem_end:
