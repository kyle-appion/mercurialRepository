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
	.asciz "SQLite.Net.dll"
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
	.no_dead_strip JetBrains_Annotations_CanBeNullAttribute__ctor
JetBrains_Annotations_CanBeNullAttribute__ctor:
.file 1 "<unknown>"
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #200]
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
bl _p_1
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

Lme_0:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_NotNullAttribute__ctor
JetBrains_Annotations_NotNullAttribute__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #208]
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
bl _p_1
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

Lme_1:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_UsedImplicitlyAttribute__ctor
JetBrains_Annotations_UsedImplicitlyAttribute__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #216]
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
.word 0xd28000e1
.word 0xd2800021
.word 0xd28000e1
.word 0xd2800022
bl _p_2
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
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_2:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_UsedImplicitlyAttribute__ctor_JetBrains_Annotations_ImplicitUseKindFlags_JetBrains_Annotations_ImplicitUseTargetFlags
JetBrains_Annotations_UsedImplicitlyAttribute__ctor_JetBrains_Annotations_ImplicitUseKindFlags_JetBrains_Annotations_ImplicitUseTargetFlags:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xaa0003f8
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #224]
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
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_1
.word 0xf94017b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb9801ba1
.word 0xaa1803e0
bl _p_3
.word 0xf94017b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb98023a1
.word 0xaa1803e0
bl _p_4
.word 0xf94017b1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_3:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_UsedImplicitlyAttribute_set_UseKindFlags_JetBrains_Annotations_ImplicitUseKindFlags
JetBrains_Annotations_UsedImplicitlyAttribute_set_UseKindFlags_JetBrains_Annotations_ImplicitUseKindFlags:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #232]
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
.word 0xb9001001
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

Lme_4:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_UsedImplicitlyAttribute_set_TargetFlags_JetBrains_Annotations_ImplicitUseTargetFlags
JetBrains_Annotations_UsedImplicitlyAttribute_set_TargetFlags_JetBrains_Annotations_ImplicitUseTargetFlags:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #240]
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
.word 0xb9001401
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

Lme_5:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_MeansImplicitUseAttribute__ctor_JetBrains_Annotations_ImplicitUseTargetFlags
JetBrains_Annotations_MeansImplicitUseAttribute__ctor_JetBrains_Annotations_ImplicitUseTargetFlags:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #248]
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
.word 0xd28000e1
.word 0xb9801ba2
.word 0xd28000e1
bl _p_5
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_6:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_MeansImplicitUseAttribute__ctor_JetBrains_Annotations_ImplicitUseKindFlags_JetBrains_Annotations_ImplicitUseTargetFlags
JetBrains_Annotations_MeansImplicitUseAttribute__ctor_JetBrains_Annotations_ImplicitUseKindFlags_JetBrains_Annotations_ImplicitUseTargetFlags:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xaa0003f8
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #256]
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
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_1
.word 0xf94017b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb9801ba1
.word 0xaa1803e0
bl _p_6
.word 0xf94017b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb98023a1
.word 0xaa1803e0
bl _p_7
.word 0xf94017b1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_7:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_MeansImplicitUseAttribute_set_UseKindFlags_JetBrains_Annotations_ImplicitUseKindFlags
JetBrains_Annotations_MeansImplicitUseAttribute_set_UseKindFlags_JetBrains_Annotations_ImplicitUseKindFlags:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #264]
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
.word 0xb9001001
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

Lme_8:
.text
	.align 4
	.no_dead_strip JetBrains_Annotations_MeansImplicitUseAttribute_set_TargetFlags_JetBrains_Annotations_ImplicitUseTargetFlags
JetBrains_Annotations_MeansImplicitUseAttribute_set_TargetFlags_JetBrains_Annotations_ImplicitUseTargetFlags:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #272]
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
.word 0xb9001401
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
	.no_dead_strip JetBrains_Annotations_PublicAPIAttribute__ctor
JetBrains_Annotations_PublicAPIAttribute__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #280]
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
bl _p_1
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

Lme_a:
.text
	.align 4
	.no_dead_strip SQLite_Net_ActiveInsertCommand__ctor_SQLite_Net_TableMapping
SQLite_Net_ActiveInsertCommand__ctor_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #288]
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
.word 0xf94013b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa0
.word 0xf9000b20
.word 0x91004321
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_b:
.text
	.align 4
	.no_dead_strip SQLite_Net_ActiveInsertCommand_GetCommand_SQLite_Net_SQLiteConnection_string
SQLite_Net_ActiveInsertCommand_GetCommand_SQLite_Net_SQLiteConnection_string:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xaa0203fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #296]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
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
.word 0xaa1803e0
.word 0xf9400f00
.word 0xb50006c0
.word 0xf94017b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1903e1
.word 0xaa1a03e2
bl _p_8
.word 0xf90023a0
.word 0xf94017b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9000f00
.word 0x91006301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1a03e0
.word 0xf900131a
.word 0x91008300
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94017b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400004f
.word 0xf94017b1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9401300
.word 0xaa1a03e1
.word 0xaa1a03e1
bl _p_9
.word 0x53001c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x340007c0
.word 0xf94017b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400f01
.word 0xaa0103e0
.word 0xf940003e
bl _p_10
.word 0xf94017b1
.word 0xf941ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1903e1
.word 0xaa1a03e2
bl _p_8
.word 0xf90023a0
.word 0xf94017b1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9000f00
.word 0x91006301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1a03e0
.word 0xf900131a
.word 0x91008300
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9429a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400f00
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf942c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94017b1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_c:
.text
	.align 4
	.no_dead_strip SQLite_Net_ActiveInsertCommand_Dispose
SQLite_Net_ActiveInsertCommand_Dispose:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #304]
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
.word 0xb40002a0
.word 0xf9400fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f41
.word 0xaa0103e0
.word 0xf940003e
bl _p_10
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800000
.word 0xf9000f5f
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_d:
.text
	.align 4
	.no_dead_strip SQLite_Net_ActiveInsertCommand_CreateCommand_SQLite_Net_SQLiteConnection_string
SQLite_Net_ActiveInsertCommand_CreateCommand_SQLite_Net_SQLiteConnection_string:
.loc 1 1 0
.word 0xa9af7bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xf90033a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #312]
.word 0xf90037b0
.word 0xf9400a11
.word 0xf9003bb1
.word 0xf90043bf
.word 0xf90047bf
.word 0xf94037b1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf940003e
bl _p_11
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf90043a0
.word 0xf94037b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #320]
bl _p_12
.word 0x53001c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0x35000fe0
.word 0xf94037b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf940003e
bl _p_13
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #328]
bl _p_14
.word 0x93407c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000c21
.word 0xf94037b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf940003e
bl _p_13
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xd2800001
.word 0xb9801801
.word 0xeb1f003f
.word 0x10000011
.word 0x54003ce9
.word 0xf9401001
.word 0xaa0103e0
.word 0xf940003e
bl _p_15
.word 0x53001c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0x34000820
.word 0xf94037b1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #336]
.word 0xf90077a0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800041
bl _p_16
.word 0xf9006ba0
.word 0xf9406ba0
.word 0xf9007fa0
.word 0xf9406ba0
.word 0xf90087a0
.word 0xd2800000
.word 0xf9402ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf90083a0
.word 0xf94037b1
.word 0xf9425631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a2
.word 0xf94087a3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407fa0
.word 0xf9006fa0
.word 0xf9406fa0
.word 0xf9007ba0
.word 0xf9406fa3
.word 0xd2800020
.word 0xf94033a2
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94077a0
.word 0xf9407ba1
bl _p_18
.word 0xf90073a0
.word 0xf94037b1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf90047a0
.word 0xf94037b1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000162
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf942fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #352]
.word 0xd28000a2
.word 0xd28000a2
bl _p_19
.word 0x93407c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9433231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x34000220
.word 0xf94037b1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf940003e
bl _p_13
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9437e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf90043a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf943a631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #360]
.word 0xf90077a0
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800081
bl _p_16
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xf90073a0
.word 0xaa1503e0
.word 0xf9007fa0
.word 0xd2800000
.word 0xf9402ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf9440e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba2
.word 0xf9407fa3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94073a0
.word 0xf94077a6
.word 0xf9004ba0
.word 0xf9404ba5
.word 0xf9404ba4
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #368]
.word 0xf94043a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #376]
.word 0xf9400000
.word 0xf9004fa0
.word 0xf9404fa1
.word 0xf9404fa0
.word 0xaa0603f9
.word 0xaa0503f6
.word 0xaa0403fa
.word 0xd2800038
.word 0xaa0303f7
.word 0xaa0203f3
.word 0xaa0103f4
.word 0xb5000780
.word 0xaa1903e0
.word 0xaa1603e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xaa1303e0
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #384]
.word 0xf9400000
.word 0xf90073a0
.word 0xeb1f001f
.word 0x10000011
.word 0x540023a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #392]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xf94073a1
.word 0xf9001001
.word 0x91008002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #400]
.word 0xf9001401

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #408]
.word 0xf9002001

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #416]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf90067a0
.word 0xf94067a0
.word 0xf94067a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #376]
.word 0xf9000022
.word 0xaa0003f4
.word 0xaa1903e0
.word 0xaa1603e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xaa1303e0
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #424]
.word 0xaa1303e0
.word 0xaa1403e1
bl _p_21
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf945c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #432]
bl _p_22
.word 0xf90077a0
.word 0xf94037b1
.word 0xf945ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a1
.word 0xaa1703e0
bl _p_23
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9460a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a2
.word 0xaa1a03e0
.word 0xaa1803e1
.word 0xf9400343
.word 0xf9407870
.word 0xd63f0200
.word 0xf90053b6
.word 0xf94053a5
.word 0xf94053a4
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #368]
.word 0xf94043a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #440]
.word 0xf9400000
.word 0xf90057a0
.word 0xf94057a1
.word 0xf94057a0
.word 0xaa0503f6
.word 0xaa0403fa
.word 0xd2800058
.word 0xaa0303f7
.word 0xaa0203f3
.word 0xaa0103f4
.word 0xb5000780
.word 0xaa1903e0
.word 0xaa1603e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xaa1303e0
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #384]
.word 0xf9400000
.word 0xf90073a0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001440

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #392]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xf94073a1
.word 0xf9001001
.word 0x91008002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #448]
.word 0xf9001401

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #456]
.word 0xf9002001

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #464]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf90063a0
.word 0xf94063a0
.word 0xf94063a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #440]
.word 0xf9000022
.word 0xaa0003f4
.word 0xaa1903e0
.word 0xaa1603e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xaa1303e0
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #424]
.word 0xaa1303e0
.word 0xaa1403e1
bl _p_21
.word 0xf90083a0
.word 0xf94037b1
.word 0xf947ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #432]
bl _p_22
.word 0xf9007fa0
.word 0xf94037b1
.word 0xf947d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa1
.word 0xaa1703e0
bl _p_23
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf947f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba2
.word 0xaa1a03e0
.word 0xaa1803e1
.word 0xf9400343
.word 0xf9407870
.word 0xd63f0200
.word 0xf9005bb6
.word 0xf9405ba0
.word 0xf90077a0
.word 0xf9405ba3
.word 0xd2800060
.word 0xf94033a2
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94077a1
.word 0xaa1903e0
bl _p_18
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9485a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf90047a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9488231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9007ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #472]
bl _p_24
.word 0xf9407ba1
.word 0xf90077a0
bl _p_25
.word 0xf94037b1
.word 0xf948b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf9005fa0
.word 0xf9405fa0
.word 0xf90073a0
.word 0xf9405fa2
.word 0xf94047a1
.word 0xaa0203e0
.word 0xf940005e
bl _p_26
.word 0xf94037b1
.word 0xf948ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9490a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf94037b1
.word 0xf9491e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d17bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_e:
.text
	.align 4
	.no_dead_strip SQLite_Net_ActiveInsertCommand__c__cctor
SQLite_Net_ActiveInsertCommand__c__cctor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #480]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #488]
.word 0xd2800201
.word 0xd2800201
bl _p_20
.word 0xf9001ba0
bl _p_28
.word 0xf9400bb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #384]
.word 0xf9000001
.word 0xf9400bb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_f:
.text
	.align 4
	.no_dead_strip SQLite_Net_ActiveInsertCommand__c__ctor
SQLite_Net_ActiveInsertCommand__c__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #496]
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

Lme_10:
.text
	.align 4
	.no_dead_strip SQLite_Net_ActiveInsertCommand__c__CreateCommandb__6_0_SQLite_Net_TableMapping_Column
SQLite_Net_ActiveInsertCommand__c__CreateCommandb__6_0_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #504]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #512]
.word 0xf90027a0
.word 0xf9400fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9402ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #512]
bl _p_30
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_11:
.text
	.align 4
	.no_dead_strip SQLite_Net_ActiveInsertCommand__c__CreateCommandb__6_1_SQLite_Net_TableMapping_Column
SQLite_Net_ActiveInsertCommand__c__CreateCommandb__6_1_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #520]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #528]
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_12:
.text
	.align 4
	.no_dead_strip SQLite_Net_BaseTableQuery__ctor
SQLite_Net_BaseTableQuery__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #536]
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

Lme_13:
.text
	.align 4
	.no_dead_strip SQLite_Net_BaseTableQuery_Ordering_get_ColumnName
SQLite_Net_BaseTableQuery_Ordering_get_ColumnName:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #544]
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

Lme_14:
.text
	.align 4
	.no_dead_strip SQLite_Net_BaseTableQuery_Ordering_set_ColumnName_string
SQLite_Net_BaseTableQuery_Ordering_set_ColumnName_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #552]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_15:
.text
	.align 4
	.no_dead_strip SQLite_Net_BaseTableQuery_Ordering_get_Ascending
SQLite_Net_BaseTableQuery_Ordering_get_Ascending:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #560]
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
.word 0x39406000
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
	.no_dead_strip SQLite_Net_BaseTableQuery_Ordering_set_Ascending_bool
SQLite_Net_BaseTableQuery_Ordering_set_Ascending_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #568]
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
.word 0x39006001
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

Lme_17:
.text
	.align 4
	.no_dead_strip SQLite_Net_BaseTableQuery_Ordering__ctor
SQLite_Net_BaseTableQuery_Ordering__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_18:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver__ctor
SQLite_Net_ContractResolver__ctor:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #584]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf9401fb1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #592]
.word 0xf9400000
.word 0xaa0003f9
.word 0xaa1903e0
.word 0xaa1903e2
.word 0xaa0103f8
.word 0xaa0003f7
.word 0xb50006f9
.word 0xaa1803e0
.word 0xaa1703e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #600]
.word 0xf9400000
.word 0xf9002ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000b80

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #608]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xf9402ba1
.word 0xf9001001
.word 0x91008002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #616]
.word 0xf9001401

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #624]
.word 0xf9002001

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #632]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xaa0003f6
.word 0xaa0003e1
.word 0xaa0003e1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #592]
.word 0xf9000020
.word 0xaa0003f7
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #640]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #648]
.word 0xf9001440

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #656]
.word 0xf9002040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #664]
.word 0xf9401401
.word 0xf9000c41
.word 0xf9401000
.word 0xf9000840
.word 0xd2800000
.word 0x3901805f
.word 0xaa1803e0
.word 0xaa1703e1
bl _p_31
.word 0xf9401fb1
.word 0xf941e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xa94267b8
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_19:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver__ctor_System_Func_2_System_Type_bool_System_Func_3_System_Type_object___object
SQLite_Net_ContractResolver__ctor_System_Func_2_System_Type_bool_System_Func_3_System_Type_object___object:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xaa0203fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #672]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
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
.word 0xaa1803e0
.word 0xf94017b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xb5000259
.word 0xf94017b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28017a1
.word 0xd28017a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94017b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb500025a
.word 0xf94017b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2801a21
.word 0xd2801a21
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94017b1
.word 0xf9413e31
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94017b1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1a03e0
.word 0xf9000f1a
.word 0x91006300
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94017b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_1a:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver_get_Current
SQLite_Net_ContractResolver_get_Current:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #680]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94017b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #688]
.word 0xf9400000
.word 0xaa0003fa
.word 0xaa1a03e0
.word 0xaa1a03e1
.word 0xaa0003f9
.word 0xb500037a
.word 0xaa1903e0
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #696]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90023a0
bl _p_34
.word 0xf94017b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xaa0003f8
.word 0xaa1803e0
.word 0xaa1803e1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #688]
.word 0xf9000038
.word 0xaa0003f9
.word 0xaa1903e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94017b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_1b:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver_get_CanCreate
SQLite_Net_ContractResolver_get_CanCreate:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #704]
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

Lme_1c:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver_get_Create
SQLite_Net_ContractResolver_get_Create:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_1d:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver_CreateObject_System_Type_object__
SQLite_Net_ContractResolver_CreateObject_System_Type_object__:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #720]
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
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_35
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xb40003a0
.word 0xf94017b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_35
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba2
.word 0xaa1903e0
.word 0xaa0203e0
.word 0xaa1903e1
.word 0xf90027a2
.word 0xf9400c50
.word 0xd63f0200
.word 0xf94027a1
.word 0x53001c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x34000540
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_36
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba3
.word 0xaa1903e0
.word 0xf94013a2
.word 0xaa0303e0
.word 0xaa1903e1
.word 0xf90027a3
.word 0xf9400c70
.word 0xd63f0200
.word 0xaa0003e1
.word 0xf94027a0
.word 0xf90023a1
.word 0xf94017b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x1400000f
.word 0xf94017b1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf94017b1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_1e:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver__c__cctor
SQLite_Net_ContractResolver__c__cctor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #728]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #736]
.word 0xd2800201
.word 0xd2800201
bl _p_20
.word 0xf9001ba0
bl _p_37
.word 0xf9400bb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #600]
.word 0xf9000001
.word 0xf9400bb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_1f:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver__c__ctor
SQLite_Net_ContractResolver__c__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #744]
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

Lme_20:
.text
	.align 4
	.no_dead_strip SQLite_Net_ContractResolver__c___ctorb__3_0_System_Type
SQLite_Net_ContractResolver__c___ctorb__3_0_System_Type:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #752]
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
.word 0xd2800020
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xf94013b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_21:
.text
	.align 4
	.no_dead_strip SQLite_Net_NotNullConstraintViolationException__ctor_SQLite_Net_Interop_Result_string_SQLite_Net_TableMapping_object
SQLite_Net_NotNullConstraintViolationException__ctor_SQLite_Net_Interop_Result_string_SQLite_Net_TableMapping_object:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xf90013b9
.word 0xaa0003f6
.word 0xf90017a1
.word 0xf9001ba2
.word 0xaa0303f9
.word 0xf9001fa4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #760]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800015
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #768]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xf90033a0
bl _p_38
.word 0xf94023b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f5
.word 0xf94023b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xf9401fa0
.word 0xf9000aa0
.word 0x910042a1
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94023b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xb9802ba1
.word 0xf9401ba2
.word 0xaa1603e0
bl _p_39
.word 0xf94023b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xb40009f9
.word 0xf94023b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xf9400aa0
.word 0xb4000900
.word 0xf94023b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf940033e
bl _p_13
.word 0xf90037a0
.word 0xf94023b1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xeb1f02bf
.word 0x10000011
.word 0x540008e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #776]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf94037a0
.word 0xf9001035
.word 0x91008022
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #784]
.word 0xf9001422

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #792]
.word 0xf9002022

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #800]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #808]
bl _p_40
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9425631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa1603e0
bl _p_41
.word 0xf94023b1
.word 0xf9427231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xf94013b9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_26:
.text
	.align 4
	.no_dead_strip SQLite_Net_NotNullConstraintViolationException_set_Columns_System_Collections_Generic_IEnumerable_1_SQLite_Net_TableMapping_Column
SQLite_Net_NotNullConstraintViolationException_set_Columns_System_Collections_Generic_IEnumerable_1_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #816]
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
.word 0xf9004820
.word 0x91024021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_27:
.text
	.align 4
	.no_dead_strip SQLite_Net_NotNullConstraintViolationException_New_SQLite_Net_Interop_Result_string
SQLite_Net_NotNullConstraintViolationException_New_SQLite_Net_Interop_Result_string:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #824]
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
.word 0xb98013a0
.word 0xf90027a0
.word 0xf9400fa0
.word 0xf9002ba0
.word 0xd2800000
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #832]
.word 0xd2801301
.word 0xd2801301
bl _p_20
.word 0xf94027a1
.word 0xf9402ba2
.word 0xf90023a0
.word 0xd2800003
.word 0xd2800004
bl _p_42
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_28:
.text
	.align 4
	.no_dead_strip SQLite_Net_NotNullConstraintViolationException_New_SQLite_Net_Interop_Result_string_SQLite_Net_TableMapping_object
SQLite_Net_NotNullConstraintViolationException_New_SQLite_Net_Interop_Result_string_SQLite_Net_TableMapping_object:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #840]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf9401bb1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xb98013a0
.word 0xf9002fa0
.word 0xf9400fa0
.word 0xf90033a0
.word 0xf94013a0
.word 0xf90037a0
.word 0xf94017a0
.word 0xf9003ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #832]
.word 0xd2801301
.word 0xd2801301
bl _p_20
.word 0xf9402fa1
.word 0xf94033a2
.word 0xf94037a3
.word 0xf9403ba4
.word 0xf9002ba0
bl _p_42
.word 0xf9401bb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401bb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_29:
.text
	.align 4
	.no_dead_strip SQLite_Net_NotNullConstraintViolationException_New_SQLite_Net_SQLiteException_SQLite_Net_TableMapping_object
SQLite_Net_NotNullConstraintViolationException_New_SQLite_Net_SQLiteException_SQLite_Net_TableMapping_object:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xaa0003f8
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #848]
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
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf940031e
bl _p_43
.word 0x93407c00
.word 0xf90027a0
.word 0xf94017b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf9405030
.word 0xd63f0200
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9002fa0
.word 0xf94013a0
.word 0xf90033a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #832]
.word 0xd2801301
.word 0xd2801301
bl _p_20
.word 0xf94027a1
.word 0xf9402ba2
.word 0xf9402fa3
.word 0xf94033a4
.word 0xf90023a0
bl _p_42
.word 0xf94017b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94017b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_2a:
.text
	.align 4
	.no_dead_strip SQLite_Net_NotNullConstraintViolationException__c__DisplayClass0_0__ctor
SQLite_Net_NotNullConstraintViolationException__c__DisplayClass0_0__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #856]
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

Lme_2b:
.text
	.align 4
	.no_dead_strip SQLite_Net_NotNullConstraintViolationException__c__DisplayClass0_0___ctorb__0_SQLite_Net_TableMapping_Column
SQLite_Net_NotNullConstraintViolationException__c__DisplayClass0_0___ctorb__0_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #864]
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
.word 0xaa1a03e0
.word 0xf940035e
bl _p_44
.word 0x53001c00
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x350003e0
.word 0xf94013b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400fa0
.word 0xf9400801
.word 0xaa1a03e0
.word 0xf940035e
bl _p_45
.word 0xf90027a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xd2800001
.word 0xeb1f001f
.word 0x9a9f17e0
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x1400000f
.word 0xf94013b1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf94013b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_2c:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_SqlDecl_SQLite_Net_TableMapping_Column_bool_SQLite_Net_IBlobSerializer_System_Collections_Generic_IDictionary_2_System_Type_string
SQLite_Net_Orm_SqlDecl_SQLite_Net_TableMapping_Column_bool_SQLite_Net_IBlobSerializer_System_Collections_Generic_IDictionary_2_System_Type_string:
.loc 1 1 0
.word 0xa9b07bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xf9001bb7
.word 0xaa0003f7
.word 0xf9001fa1
.word 0xf90023a2
.word 0xf90027a3

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #872]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800016
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
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xf9007ba0
.word 0xaa1503e0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #512]
.word 0xaa1503e0
.word 0xd2800001
.word 0xf94002a3
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407ba0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf9006fa0
.word 0xaa1403e0
.word 0xf90077a0
.word 0xd2800020
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_29
.word 0xf90073a0
.word 0xf9402bb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a2
.word 0xf94077a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406fa0
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xf9006ba0
.word 0xaa1303e0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #888]
.word 0xaa1303e0
.word 0xd2800041
.word 0xf9400263
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406ba0
.word 0xf90037a0
.word 0xf94037a0
.word 0xf9005fa0
.word 0xf94037a0
.word 0xf90067a0
.word 0xd2800060
.word 0xaa1703e0
.word 0x3940e3a1
.word 0xf94023a2
.word 0xf94027a3
.word 0xaa1703e0
bl _p_46
.word 0xf90063a0
.word 0xf9402bb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a2
.word 0xf94067a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9405fa0
.word 0xf9003ba0
.word 0xf9403ba0
.word 0xf9005ba0
.word 0xf9403ba3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #896]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9405ba0
bl _p_47
.word 0xf90057a0
.word 0xf9402bb1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
.word 0xaa0003f6
.word 0xf9402bb1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_48
.word 0x53001c00
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0x34000240
.word 0xf9402bb1
.word 0xf9425e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #904]
.word 0xaa1603e0
bl _p_49
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_15
.word 0x53001c00
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0x34000240
.word 0xf9402bb1
.word 0xf942f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #912]
.word 0xaa1603e0
bl _p_49
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf9431e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9434631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_44
.word 0x53001c00
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf9436e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0x35000240
.word 0xf9402bb1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #920]
.word 0xaa1603e0
bl _p_49
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf943b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_50
.word 0xf90057a0
.word 0xf9402bb1
.word 0xf943fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
bl _p_51
.word 0x53001c00
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf9441e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0x35000420
.word 0xf9402bb1
.word 0xf9443631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #928]
.word 0xf90057a0
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_50
.word 0xf9005ba0
.word 0xf9402bb1
.word 0xf9446e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a1
.word 0xf9405ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #896]
.word 0xaa1603e0
bl _p_52
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf944c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_53
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf944ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xb4000a60
.word 0xf9402bb1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800081
bl _p_16
.word 0xf9003fa0
.word 0xf9403fa0
.word 0xf9006ba0
.word 0xf9403fa3
.word 0xd2800000
.word 0xaa1603e0
.word 0xaa0303e0
.word 0xd2800001
.word 0xaa1603e2
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406ba0
.word 0xf90043a0
.word 0xf94043a0
.word 0xf90067a0
.word 0xf94043a3
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #936]
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94067a0
.word 0xf90047a0
.word 0xf94047a0
.word 0xf9005ba0
.word 0xf94047a0
.word 0xf90063a0
.word 0xd2800040
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_53
.word 0xf9005fa0
.word 0xf9402bb1
.word 0xf945c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa2
.word 0xf94063a3
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9405ba0
.word 0xf9004ba0
.word 0xf9404ba0
.word 0xf90057a0
.word 0xf9404ba3
.word 0xd2800060

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #944]
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94057a0
bl _p_54
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf9463231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9465a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9467e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9402bb1
.word 0xf9469231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xf9401bb7
.word 0x910003bf
.word 0xa8d07bfd
.word 0xd65f03c0

Lme_2d:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_SqlType_SQLite_Net_TableMapping_Column_bool_SQLite_Net_IBlobSerializer_System_Collections_Generic_IDictionary_2_System_Type_string
SQLite_Net_Orm_SqlType_SQLite_Net_TableMapping_Column_bool_SQLite_Net_IBlobSerializer_System_Collections_Generic_IDictionary_2_System_Type_string:
.loc 1 1 0
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xf90013b7
.word 0xf90017b9
.word 0xaa0003f7
.word 0xf9001ba1
.word 0xaa0203f9
.word 0xf9001fa3

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #952]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800016
.word 0xd2800015
.word 0xf90037bf
.word 0x910183a0
.word 0xd2800000
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_55
.word 0xf90057a0
.word 0xf94023b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
.word 0xf90053a0
.word 0xaa0003f6
.word 0xf94023b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xaa0003e1
bl _p_56
.word 0xf9004fa0
.word 0xf94023b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421430
.word 0xd63f0200
.word 0xf9004ba0
.word 0xf94023b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #960]
bl _p_57
.word 0xf90047a0
.word 0xf94023b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xaa0003f5
.word 0xf94023b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa3
.word 0xaa1603e1
.word 0x9101a3a2
.word 0xaa0303e0
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #968]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x34000220
.word 0xf94023b1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf941d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x1400037f
.word 0xf94023b1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #976]
.word 0xeb0002df
.word 0x54001e40
.word 0xf94023b1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #984]
.word 0xeb0002df
.word 0x54001d00
.word 0xf94023b1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #992]
.word 0xeb0002df
.word 0x54001bc0
.word 0xf94023b1
.word 0xf9426231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1000]
.word 0xeb0002df
.word 0x54001a80
.word 0xf94023b1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1008]
.word 0xeb0002df
.word 0x54001940
.word 0xf94023b1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1016]
.word 0xeb0002df
.word 0x54001800
.word 0xf94023b1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1024]
.word 0xeb0002df
.word 0x540016c0
.word 0xf94023b1
.word 0xf9430231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1032]
.word 0xeb0002df
.word 0x54001580
.word 0xf94023b1
.word 0xf9432a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1040]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9435e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35001320
.word 0xf94023b1
.word 0xf9437631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1048]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf943aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x350010c0
.word 0xf94023b1
.word 0xf943c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1056]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf943f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35000e60
.word 0xf94023b1
.word 0xf9440e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1064]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9444231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35000c00
.word 0xf94023b1
.word 0xf9445a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1072]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9448e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x350009a0
.word 0xf94023b1
.word 0xf944a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1080]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf944da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35000740
.word 0xf94023b1
.word 0xf944f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1088]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9452631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x350004e0
.word 0xf94023b1
.word 0xf9453e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1096]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9457231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35000280
.word 0xf94023b1
.word 0xf9458a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1104]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf945be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x340002e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf945e631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1112]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9461631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x1400026e
.word 0xf94023b1
.word 0xf9462e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1120]
.word 0xeb0002df
.word 0x540009c0
.word 0xf94023b1
.word 0xf9465631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1128]
.word 0xeb0002df
.word 0x54000880
.word 0xf94023b1
.word 0xf9467e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1136]
.word 0xeb0002df
.word 0x54000740
.word 0xf94023b1
.word 0xf946a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1144]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf946da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x350004e0
.word 0xf94023b1
.word 0xf946f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1152]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9472631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35000280
.word 0xf94023b1
.word 0xf9473e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1160]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9477231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x340002e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9479a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1168]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf947ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x14000201
.word 0xf94023b1
.word 0xf947e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1176]
.word 0xeb0002df
.word 0x54000280
.word 0xf94023b1
.word 0xf9480a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1184]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9483e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x34000d80
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9486631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x910163a0
.word 0xf9003ba0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_59
.word 0xf9403bbe
.word 0xf90003c0
.word 0xf94023b1
.word 0xf9489631
.word 0xb4000051
.word 0xd63f0220
.word 0x910163a0
.word 0x910183a0
.word 0xf9402fa0
.word 0xf90033a0
.word 0xf94023b1
.word 0xf948b631
.word 0xb4000051
.word 0xd63f0220
.word 0x910183a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf948e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x34000620
.word 0xf94023b1
.word 0xf948fa31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1200]
.word 0xf90047a0
.word 0x910183a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_61
.word 0x93407c00
.word 0xf9004ba0
.word 0xf94023b1
.word 0xf9493631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e1
.word 0xf94047a0
.word 0xf9404ba2
.word 0xb9001022

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
bl _p_62
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9498231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf949a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x1400018b
.word 0xf94023b1
.word 0xf949ba31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1224]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf949ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x14000179
.word 0xf94023b1
.word 0xf94a0231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1232]
.word 0xeb0002df
.word 0x54000280
.word 0xf94023b1
.word 0xf94a2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1240]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf94a5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x340002e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94a8631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1248]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94ab631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x14000146
.word 0xf94023b1
.word 0xf94ace31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1256]
.word 0xeb0002df
.word 0x54000280
.word 0xf94023b1
.word 0xf94af631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1264]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf94b2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x340005e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94b5231
.word 0xb4000051
.word 0xd63f0220
.word 0x3940c3a0
.word 0x35000260
.word 0xf94023b1
.word 0xf94b6a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1272]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94b9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x1400010d
.word 0xf94023b1
.word 0xf94bb231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1248]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94be231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x140000fb
.word 0xf94023b1
.word 0xf94bfa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1280]
.word 0xeb0002df
.word 0x54000261
.word 0xf94023b1
.word 0xf94c2231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1248]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94c5231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x140000df
.word 0xf94023b1
.word 0xf94c6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1603e0
bl _p_56
.word 0xf90047a0
.word 0xf94023b1
.word 0xf94c8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9411030
.word 0xd63f0200
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf94cb631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x34000260
.word 0xf94023b1
.word 0xf94cce31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1112]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94cfe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x140000b4
.word 0xf94023b1
.word 0xf94d1631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1288]
.word 0xeb0002df
.word 0x54000280
.word 0xf94023b1
.word 0xf94d3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1296]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf94d7231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x340002e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94d9a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1304]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94dca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x14000081
.word 0xf94023b1
.word 0xf94de231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1312]
.word 0xeb0002df
.word 0x54000280
.word 0xf94023b1
.word 0xf94e0a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1320]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf94e3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x340002e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94e6631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1328]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94e9631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x1400004e
.word 0xf94023b1
.word 0xf94eae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xb4000579
.word 0xf94023b1
.word 0xf94ec631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1603e0
.word 0xaa1903e0
.word 0xaa1603e1
.word 0xf9400322

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1336]
.word 0x928005f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf94f0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x34000260
.word 0xf94023b1
.word 0xf94f2631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1304]
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94f5631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x1400001e
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf94f7e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2803ce1
.word 0xd2803ce1
bl _p_32
.word 0xaa1603e1
.word 0xaa1603e1
bl _p_63
.word 0xf90043a0
.word 0xf94023b1
.word 0xf94fb631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94023b1
.word 0xf94fe231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xf94013b7
.word 0xf94017b9
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_2e:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_IsPK_System_Reflection_MemberInfo
SQLite_Net_Orm_IsPK_System_Reflection_MemberInfo:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1344]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1352]
bl _p_64
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1360]
bl _p_65
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_2f:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_Collation_System_Reflection_MemberInfo
SQLite_Net_Orm_Collation_System_Reflection_MemberInfo:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xf9000fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1368]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9001fbf
.word 0xd2800019
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1376]
bl _p_66
.word 0xf90037a0
.word 0xf94013b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1384]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94013b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9001fa0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000029
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1392]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90037a0
.word 0xf94013b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_67
.word 0xf90033a0
.word 0xf94013b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f9
.word 0xf94013b1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0x94000026
.word 0x14000063
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf90033a0
.word 0xf94013b1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x35fff7e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000028
.word 0xf9002bbe
.word 0xf94013b1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
.word 0xb40002e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94013b1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bbe
.word 0xd61f03c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1416]
.word 0xf9400000
.word 0xf90033a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9430e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x14000013
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9433631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9435a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94013b1
.word 0xf9436e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_30:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_IsAutoInc_System_Reflection_MemberInfo
SQLite_Net_Orm_IsAutoInc_System_Reflection_MemberInfo:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1424]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1432]
bl _p_68
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1440]
bl _p_69
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_31:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_GetIndices_System_Reflection_MemberInfo
SQLite_Net_Orm_GetIndices_System_Reflection_MemberInfo:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1448]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1456]
bl _p_70
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

Lme_32:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_MaxStringLength_System_Reflection_PropertyInfo
SQLite_Net_Orm_MaxStringLength_System_Reflection_PropertyInfo:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xf90013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1464]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf9003bbf
.word 0x9101a3a0
.word 0xd2800000
.word 0xf90037a0
.word 0x910183a0
.word 0xd2800000
.word 0xf90033a0
.word 0xf94017b1
.word 0xf9404e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1472]
bl _p_71
.word 0xf9004fa0
.word 0xf94017b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1480]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xf9003ba0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400003d
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1488]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf94017b1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_72
.word 0x93407c00
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0x910163a0
.word 0xd2800000
.word 0xf9002fa0
.word 0x910163a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_73
.word 0x910163a0
.word 0x910143a0
.word 0xf9402fa0
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf941c631
.word 0xb4000051
.word 0xd63f0220
.word 0x910143a0
.word 0x9101a3a0
.word 0xf9402ba0
.word 0xf90037a0
.word 0xf94017b1
.word 0xf941e631
.word 0xb4000051
.word 0xd63f0220
.word 0x94000026
.word 0x1400006c
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf9424e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x35fff560
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000028
.word 0xf90047be
.word 0xf94017b1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xb40002e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94017b1
.word 0xf942f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047be
.word 0xd61f03c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9433a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910183a0
.word 0xd2800000
.word 0xf90033a0
.word 0xf94017b1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0x910183a0
.word 0x910123a0
.word 0xf94033a0
.word 0xf90027a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0x910123a0
.word 0x910043a0
.word 0xf94027a0
.word 0xf9000ba0
.word 0x14000019
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf943ba31
.word 0xb4000051
.word 0xd63f0220
.word 0x9101a3a0
.word 0x910103a0
.word 0xf94037a0
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf943ea31
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
.word 0x910043a0
.word 0xf94023a0
.word 0xf9000ba0
.word 0xf94017b1
.word 0xf9440a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0

Lme_33:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_GetDefaultValue_System_Reflection_PropertyInfo
SQLite_Net_Orm_GetDefaultValue_System_Reflection_PropertyInfo:
.loc 1 1 0
.word 0xa9ae7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1496]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf90023bf
.word 0xf90027bf
.word 0xd280001a
.word 0xd2800019
.word 0xf9002bbf
.word 0xf94017b1
.word 0xf9404a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1504]
bl _p_74
.word 0xf9005fa0
.word 0xf94017b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1512]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9005ba0
.word 0xf94017b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000105
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1520]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9005ba0
.word 0xf94017b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf90027a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_75
.word 0x53001c00
.word 0xf9005ba0
.word 0xf94017b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0x35000580
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_76
.word 0xf9005fa0
.word 0xf94017b1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9407c30
.word 0xd63f0200
.word 0xf90063a0
.word 0xf94017b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xf94063a1
bl _p_77
.word 0xf9005ba0
.word 0xf94017b1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf9425231
.word 0xb4000051
.word 0xd63f0220
.word 0x940000d2
.word 0x1400010b
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405430
.word 0xd63f0200
.word 0xf90067a0
.word 0xf94017b1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a0
bl _p_78
.word 0xf90063a0
.word 0xf94017b1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf9005fa0
.word 0xaa0003fa
.word 0xf94017b1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa1
.word 0xf94013a2
.word 0xaa0103e0
.word 0xaa0203e0
.word 0xf940005e
bl _p_79
.word 0xf9005ba0
.word 0xf94017b1
.word 0xf9430631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf9431e31
.word 0xb4000051
.word 0xd63f0220
.word 0x9400009f
.word 0x140000d8
.word 0xf9002fa0
.word 0xf9402fa0
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9434231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800081
bl _p_16
.word 0xf90033a0
.word 0xf94033a0
.word 0xf90087a0
.word 0xf94033a0
.word 0xf9008ba0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2804161
.word 0xd2804161
bl _p_32
.word 0xaa0003e2
.word 0xf9408ba3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94087a0
.word 0xf90037a0
.word 0xf94037a0
.word 0xf9007ba0
.word 0xf94037a0
.word 0xf90083a0
.word 0xd2800020
.word 0xf94027a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_76
.word 0xf9007fa0
.word 0xf94017b1
.word 0xf943e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa2
.word 0xf94083a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407ba0
.word 0xf9003ba0
.word 0xf9403ba0
.word 0xf90073a0
.word 0xf9403ba0
.word 0xf90077a0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2804621
.word 0xd2804621
bl _p_32
.word 0xaa0003e2
.word 0xf94077a3
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94073a0
.word 0xf9003fa0
.word 0xf9403fa0
.word 0xf90067a0
.word 0xf9403fa0
.word 0xf9006fa0
.word 0xd2800060
.word 0xf94013a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9407c30
.word 0xd63f0200
.word 0xf9006ba0
.word 0xf94017b1
.word 0xf9449631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba2
.word 0xf9406fa3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94067a0
bl _p_54
.word 0xf9005fa0
.word 0xf94017b1
.word 0xf944ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90063a0
.word 0xd2801700
.word 0xd2801700
bl _p_80
.word 0xf9405fa1
.word 0xf94063a2
.word 0xf9005ba0
bl _p_81
.word 0xf94017b1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
bl _p_33
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9452a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf9005ba0
.word 0xf94017b1
.word 0xf9456a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0x35ffdc60
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9459231
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000028
.word 0xf90053be
.word 0xf94017b1
.word 0xf945ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xb40002e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf945d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94017b1
.word 0xf9460e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9462e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053be
.word 0xd61f03c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9465631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9467a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x14000013
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf946a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf946c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94017b1
.word 0xf946da31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8d27bfd
.word 0xd65f03c0

Lme_34:
.text
	.align 4
	.no_dead_strip SQLite_Net_Orm_IsMarkedNotNull_System_Reflection_MemberInfo
SQLite_Net_Orm_IsMarkedNotNull_System_Reflection_MemberInfo:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
.word 0xd2800021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1536]
.word 0xd2800021
bl _p_82
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1544]
bl _p_83
.word 0x53001c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_35:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand__ctor_SQLite_Net_SQLiteConnection
SQLite_Net_PreparedSqlLiteInsertCommand__ctor_SQLite_Net_SQLiteConnection:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1552]
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
.word 0xf94013b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa1
.word 0xaa1903e0
bl _p_84
.word 0xf94013b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_36:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_get_Initialized
SQLite_Net_PreparedSqlLiteInsertCommand_get_Initialized:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1560]
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
.word 0x3940a000
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

Lme_37:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_set_Initialized_bool
SQLite_Net_PreparedSqlLiteInsertCommand_set_Initialized_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1568]
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
.word 0x3900a001
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

Lme_38:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_get_CommandText
SQLite_Net_PreparedSqlLiteInsertCommand_get_CommandText:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1576]
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

Lme_39:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_set_CommandText_string
SQLite_Net_PreparedSqlLiteInsertCommand_set_CommandText_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1584]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_3a:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_get_Connection
SQLite_Net_PreparedSqlLiteInsertCommand_get_Connection:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1592]
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

Lme_3b:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_set_Connection_SQLite_Net_SQLiteConnection
SQLite_Net_PreparedSqlLiteInsertCommand_set_Connection_SQLite_Net_SQLiteConnection:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1600]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_3c:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_get_Statement
SQLite_Net_PreparedSqlLiteInsertCommand_get_Statement:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_3d:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_set_Statement_SQLite_Net_Interop_IDbStatement
SQLite_Net_PreparedSqlLiteInsertCommand_set_Statement_SQLite_Net_Interop_IDbStatement:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1616]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_3e:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_Dispose
SQLite_Net_PreparedSqlLiteInsertCommand_Dispose:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1624]
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
.word 0xd2800020
.word 0xaa1a03e0
.word 0xd2800021
bl _p_85
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_86
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_3f:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_Finalize
SQLite_Net_PreparedSqlLiteInsertCommand_Finalize:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1632]
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
.word 0xd2800001
.word 0xd2800001
bl _p_85
.word 0xf9400fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000012
.word 0xf90023be
.word 0xf9400fb1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_87
.word 0xf9400fb1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023be
.word 0xd61f03c0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000001
.word 0xf9400fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_40:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_ExecuteNonQuery_object__
SQLite_Net_PreparedSqlLiteInsertCommand_ExecuteNonQuery_object__:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1640]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xd2800018
.word 0xb90053bf
.word 0xd2800017
.word 0xd2800016
.word 0xf9401fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_88
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_89
.word 0xf90037a0
.word 0xf9401fb1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1648]
.word 0xf9003ba0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_90
.word 0xf9003fa0
.word 0xf9401fb1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf9403ba1
.word 0xf9403fa2
bl _p_91
.word 0xf9401fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_92
.word 0x53001c00
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x35000480
.word 0xf9401fb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9403430
.word 0xd63f0200
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa1903e0
bl _p_93
.word 0xf9401fb1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020
.word 0xaa1903e0
.word 0xd2800021
bl _p_94
.word 0xf9401fb1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_88
.word 0xf90037a0
.word 0xf9401fb1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_95
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f8
.word 0xf9401fb1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb4000efa
.word 0xf9401fb1
.word 0xf9426231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800017
.word 0xf9401fb1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000060
.word 0xf9401fb1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf942c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_96
.word 0xf90037a0
.word 0xf9401fb1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x110006e0
.word 0xf9003ba0
.word 0xaa1a03e0
.word 0xaa1703e0
.word 0x93407ee0
.word 0xb9801b41
.word 0xeb00003f
.word 0x10000011
.word 0x54004dc9
.word 0xd37df000
.word 0x8b000340
.word 0x91008000
.word 0xf9400000
.word 0xf9003fa0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_88
.word 0xf9004fa0
.word 0xf9401fb1
.word 0xf9434231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_97
.word 0x53001c00
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9436a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_88
.word 0xf9004ba0
.word 0xf9401fb1
.word 0xf9438a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_98
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf943ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf94037a1
.word 0xf9403ba2
.word 0xf9403fa3
.word 0xf94043a4
.word 0xf94047a5
bl _p_99
.word 0xf9401fb1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf943ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x110006e0
.word 0xaa0003f7
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9441631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1a03e0
.word 0xb9801b40
.word 0x6b0002ff
.word 0x54fff2ab
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9444a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf9448631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_96
.word 0xf90037a0
.word 0xf9401fb1
.word 0xf944a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xf9403ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1664]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf944ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xb90053a0
.word 0xf9401fb1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0xb98053a0
.word 0xd2800ca1
.word 0xd2800cbe
.word 0x6b1e001f
.word 0x54000ce1
.word 0xf9401fb1
.word 0xf9452631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9456231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_88
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf9458231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf9003fa0
.word 0xf9401fb1
.word 0xf945a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa1
.word 0xf94043a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1672]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf945ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf9462631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_96
.word 0xf90037a0
.word 0xf9401fb1
.word 0xf9464631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xf9403ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1680]
.word 0x92800cf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9401fb1
.word 0xf9468631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf946a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x1400017c
.word 0xf9401fb1
.word 0xf946be31
.word 0xb4000051
.word 0xd63f0220
.word 0xb98053a0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000e01
.word 0xf9401fb1
.word 0xf946e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf9471e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_88
.word 0xf9004ba0
.word 0xf9401fb1
.word 0xf9473e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9476231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xf94047a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1688]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf9003fa0
.word 0xf9401fb1
.word 0xf947a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xaa0003f6
.word 0xf9401fb1
.word 0xf947ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf947f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_96
.word 0xf90037a0
.word 0xf9401fb1
.word 0xf9481631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xf9403ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1680]
.word 0x92800cf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9401fb1
.word 0xf9485631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9486631
.word 0xb4000051
.word 0xd63f0220
.word 0xb98053a0
.word 0xaa1603e1
bl _p_101
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf9488631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_33
.word 0xf9401fb1
.word 0xf9489e31
.word 0xb4000051
.word 0xd63f0220
.word 0xb98053a0
.word 0xd2800261
.word 0xd280027e
.word 0x6b1e001f
.word 0x540014e1
.word 0xf9401fb1
.word 0xf948c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf948fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_88
.word 0xf9003fa0
.word 0xf9401fb1
.word 0xf9491e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf90037a0
.word 0xf9401fb1
.word 0xf9494231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xf9403ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1696]
.word 0x928006f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf9498631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xd280a261
.word 0xd280a27e
.word 0x6b1e001f
.word 0x54000da1
.word 0xf9401fb1
.word 0xf949aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf9401fb1
.word 0xf949e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_96
.word 0xf9004ba0
.word 0xf9401fb1
.word 0xf94a0631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xf9404fa2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1680]
.word 0x92800cf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9401fb1
.word 0xf94a4631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf94a5631
.word 0xb4000051
.word 0xd63f0220
.word 0xb98053a0
.word 0xf90037a0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf94a9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_88
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf94aba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf9003fa0
.word 0xf9401fb1
.word 0xf94ade31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa1
.word 0xf94043a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1688]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf94b1e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf9403ba1
bl _p_102
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf94b3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_33
.word 0x14000001
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf94b6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf94ba631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_96
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf94bc631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xf94047a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1680]
.word 0x92800cf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9401fb1
.word 0xf94c0631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf94c1631
.word 0xb4000051
.word 0xd63f0220
.word 0xb98053a0
.word 0xf90037a0
.word 0x910143a0
.word 0xb98053a0
.word 0xf9003fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1704]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e1
.word 0xf9403fa0
.word 0xb9001020
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9402030
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf94c7231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf9403ba1
bl _p_101
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf94c9231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_33
.word 0xf9401fb1
.word 0xf94caa31
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
bl _p_27

Lme_41:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_Prepare
SQLite_Net_PreparedSqlLiteInsertCommand_Prepare:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1712]
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
bl _p_88
.word 0xf90033a0
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_95
.word 0xf9002fa0
.word 0xf9400fb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90027a0
.word 0xf9400fb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_88
.word 0xf9002ba0
.word 0xf9400fb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_90
.word 0xf90023a0
.word 0xf9400fb1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xf94023a2
.word 0xf94027a3
.word 0xaa0303e0
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1720]
.word 0x928007f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_42:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand_Dispose_bool
SQLite_Net_PreparedSqlLiteInsertCommand_Dispose_bool:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1728]
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
bl _p_96
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1736]
.word 0xf9400021
.word 0xeb01001f
.word 0x54000cc0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_88
.word 0xf90037a0
.word 0xf94013b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_95
.word 0xf90033a0
.word 0xf94013b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_96
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba1
.word 0xf9402fa2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1744]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf94013b1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000022
.word 0xf90027be
.word 0xf94013b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1736]
.word 0xf9400021
bl _p_93
.word 0xf94013b1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xd2800001
.word 0xd2800001
bl _p_84
.word 0xf94013b1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027be
.word 0xd61f03c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9425231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_43:
.text
	.align 4
	.no_dead_strip SQLite_Net_PreparedSqlLiteInsertCommand__cctor
SQLite_Net_PreparedSqlLiteInsertCommand__cctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1752]
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
.word 0xf9400bb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_44:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteException__ctor_SQLite_Net_Interop_Result_string
SQLite_Net_SQLiteException__ctor_SQLite_Net_Interop_Result_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xaa0003f8
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1760]
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
.word 0xaa1803e0
.word 0xf94013a1
.word 0xaa1803e0
bl _p_103
.word 0xf94017b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb9801ba1
.word 0xaa1803e0
bl _p_104
.word 0xf94017b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_45:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteException_get_Result
SQLite_Net_SQLiteException_get_Result:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1768]
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
.word 0xb9808800
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
	.no_dead_strip SQLite_Net_SQLiteException_set_Result_SQLite_Net_Interop_Result
SQLite_Net_SQLiteException_set_Result_SQLite_Net_Interop_Result:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1776]
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
.word 0xb9008801
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

Lme_47:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteException_New_SQLite_Net_Interop_Result_string
SQLite_Net_SQLiteException_New_SQLite_Net_Interop_Result_string:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1784]
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
.word 0xb98013a0
.word 0xf90027a0
.word 0xf9400fa0
.word 0xf9002ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1792]
.word 0xd2801201
.word 0xd2801201
bl _p_20
.word 0xf94027a1
.word 0xf9402ba2
.word 0xf90023a0
bl _p_39
.word 0xf94013b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_48:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection
SQLite_Net_SQLiteCommand__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xaa0003f8
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1800]
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
.word 0xaa1803e0
.word 0xf94017b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400fa0
.word 0xf9001300
.word 0x91008301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94013a0
.word 0xf9000f00
.word 0x91006301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1808]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90023a0
bl _p_105
.word 0xf94017b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9000b00
.word 0x91004301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1816]
.word 0xaa1803e0
bl _p_106
.word 0xf94017b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_49:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_get_CommandText
SQLite_Net_SQLiteCommand_get_CommandText:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1824]
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

Lme_4a:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_set_CommandText_string
SQLite_Net_SQLiteCommand_set_CommandText_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1832]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_4b:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteNonQuery
SQLite_Net_SQLiteCommand_ExecuteNonQuery:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1840]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800019
.word 0xb90043bf
.word 0xd2800018
.word 0xf94017b1
.word 0xf9404631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f41
.word 0xaa0103e0
.word 0xf940003e
bl _p_89
.word 0xf90037a0
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1648]
.word 0xaa1a03e2
.word 0xaa1a03e2
bl _p_91
.word 0xf94017b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_107
.word 0xf90033a0
.word 0xf94017b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9401341
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa2
.word 0xaa1903e1
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1664]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xb90043a0
.word 0xf94017b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1903e1
.word 0xaa1a03e0
bl _p_108
.word 0xf94017b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xb98043a0
.word 0xd2800ca1
.word 0xd2800cbe
.word 0x6b1e001f
.word 0x54000741
.word 0xf94017b1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9401341
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94017b1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f41
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xf94033a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1672]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9429e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x140000eb
.word 0xf94017b1
.word 0xf942d631
.word 0xb4000051
.word 0xd63f0220
.word 0xb98043a0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000821
.word 0xf94017b1
.word 0xf942fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9401341
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf9433a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f41
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf90037a0
.word 0xf94017b1
.word 0xf9436231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xf9403ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1688]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94017b1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9002fa0
.word 0xaa0003f8
.word 0xf94017b1
.word 0xf943be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xb98043a0
.word 0xaa0103e2
bl _p_101
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf943e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_33
.word 0xf94017b1
.word 0xf943fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xb98043a0
.word 0xd2800261
.word 0xd280027e
.word 0x6b1e001f
.word 0x54000e01
.word 0xf94017b1
.word 0xf9441e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9401341
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94017b1
.word 0xf9445e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f41
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf9448631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xf94033a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1696]
.word 0x928006f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf944ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xd280a261
.word 0xd280a27e
.word 0x6b1e001f
.word 0x54000781
.word 0xf94017b1
.word 0xf944ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xb98043a0
.word 0xf9002fa0
.word 0xaa1a03e0
.word 0xf9401341
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf9453631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f41
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf90037a0
.word 0xf94017b1
.word 0xf9455e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xf9403ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1688]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94017b1
.word 0xf9459e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf94033a1
bl _p_102
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf945be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_33
.word 0x14000001
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf945ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xb98043a0
.word 0xf9002fa0
.word 0x910103a0
.word 0xb98043a0
.word 0xf90037a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1704]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e1
.word 0xf94037a0
.word 0xb9001020
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9402030
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94017b1
.word 0xf9464631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf94033a1
bl _p_101
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9466631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_33
.word 0xf94017b1
.word 0xf9467e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_4c:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteDeferredQuery_T_REF
SQLite_Net_SQLiteCommand_ExecuteDeferredQuery_T_REF:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9001baf
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1848]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xf9002ba0
.word 0xf9401ba0
bl _p_109
.word 0xaa0003e1
.word 0xf9402ba3
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf940007e
bl _p_110
.word 0xf90027a0
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
bl _p_111
.word 0xaa0003ef
.word 0xf94027a1
.word 0xaa1a03e0
bl _p_112
.word 0xf90023a0
.word 0xf9400fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_4d:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteQuery_T_REF
SQLite_Net_SQLiteCommand_ExecuteQuery_T_REF:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9001baf
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1856]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xf9002fa0
.word 0xf9401ba0
bl _p_113
.word 0xaa0003e1
.word 0xf9402fa3
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf940007e
bl _p_110
.word 0xf9002ba0
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
bl _p_114
.word 0xaa0003ef
.word 0xf9402ba1
.word 0xaa1a03e0
bl _p_115
.word 0xf90027a0
.word 0xf9400fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
bl _p_116
.word 0xaa0003ef
.word 0xf94027a0
bl _p_117
.word 0xf90023a0
.word 0xf9400fb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400fb1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_4e:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_OnInstanceCreated_object
SQLite_Net_SQLiteCommand_OnInstanceCreated_object:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1864]
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
.word 0xf94013b1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_4f:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteDeferredQuery_T_REF_SQLite_Net_TableMapping
SQLite_Net_SQLiteCommand_ExecuteDeferredQuery_T_REF_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xf90027af
.word 0xf90013a0
.word 0xf90017a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1872]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf9401bb1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0x92800020
.word 0xf2bfffe0
.word 0xf94027a0
bl _p_118
.word 0xd2800901
.word 0xd2800901
bl _p_20
.word 0xf9002fa0
.word 0x92800021
.word 0xf2bfffe1
bl _p_119
.word 0xf9401bb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xaa0003f8
.word 0xaa1803e1
.word 0xaa1803e0
.word 0xf94013a0
.word 0xf9000f00
.word 0x91006302
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xaa0103f7
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf94017a0
.word 0xf9001420
.word 0xf9002ba1
.word 0x9100a021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401bb1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_50:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteScalar_T_REF
SQLite_Net_SQLiteCommand_ExecuteScalar_T_REF:
.loc 1 1 0
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xf90033af
.word 0xf90023a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1880]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xd280001a
.word 0xf90037bf
.word 0xd2800019
.word 0xd2800018
.word 0xd2800017
.word 0xf94027b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_89
.word 0xf9004fa0
.word 0xf94027b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1888]
.word 0xf94023a2
bl _p_91
.word 0xf94027b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xd280001a
.word 0xf94027b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
bl _p_107
.word 0xf9004ba0
.word 0xf94027b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xf90037a0
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90053a0
.word 0xf94027b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a2
.word 0xf94037a1
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1664]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9004fa0
.word 0xf94027b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf9004ba0
.word 0xaa0003f9
.word 0xf94027b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003e1
.word 0xd2800c81
.word 0xd2800c9e
.word 0x6b1e001f
.word 0x54000ec1
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90053a0
.word 0xf94027b1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a3
.word 0xf94037a1
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1896]
.word 0x928007f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9004fa0
.word 0xf94027b1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xaa0003f8
.word 0xf94027b1
.word 0xf942aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_120
bl _p_121
.word 0xf9004ba0
.word 0xf94027b1
.word 0xf942ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f6
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa0003f5
.word 0xb5000120
.word 0xaa1503e0
.word 0xf94027b1
.word 0xf942f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_120
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xaa1503f7
.word 0xf94027b1
.word 0xf9431a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xd28000a0
.word 0xd28000be
.word 0x6b1e031f
.word 0x54000f00
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9434e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94037a1
.word 0xd2800002
.word 0xaa1803e2
.word 0xaa1703e2
.word 0xd2800002
.word 0xaa1803e3
.word 0xaa1703e4
bl _p_122
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_123
.word 0xaa0003e2
.word 0xf9400441
.word 0xf9403ba0
bl _p_124
.word 0xaa0003fa
.word 0xf94027b1
.word 0xf943b231
.word 0xb4000051
.word 0xd63f0220
.word 0x94000060
.word 0x14000071
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800ca0
.word 0xd2800cbe
.word 0x6b1e033f
.word 0x54000161
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9440e31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000049
.word 0x1400005a
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9443631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94023a0
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90057a0
.word 0xf94027b1
.word 0xf9447a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf90053a0
.word 0xf94027b1
.word 0xf944a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xf94057a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1688]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf94027b1
.word 0xf944e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa1903e0
bl _p_101
.word 0xf9004ba0
.word 0xf94027b1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
bl _p_33
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9452a31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000013
.word 0xf90047be
.word 0xf94027b1
.word 0xf9454631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94037a1
bl _p_108
.word 0xf94027b1
.word 0xf9456231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9457231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047be
.word 0xd61f03c0
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9459a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf945be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf94027b1
.word 0xf945d231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_51:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Bind_string_object
SQLite_Net_SQLiteCommand_Bind_string_object:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xf90013a0
.word 0xf90017a1
.word 0xf9001ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1904]
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
.word 0xf94013a0
.word 0xf9400800
.word 0xf9002fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1912]
.word 0xd2800501
.word 0xd2800501
bl _p_20
.word 0xf90037a0
bl _p_125
.word 0xf9401fb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xaa0003f7
.word 0xaa1703e0
.word 0xf90033a0
.word 0xaa1703e0
.word 0xf94017a1
.word 0xaa1703e0
.word 0xf94002fe
bl _p_126
.word 0xf9401fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xf9002ba0
.word 0xaa1603e0
.word 0xf9401ba1
.word 0xaa1603e0
.word 0xf94002de
bl _p_127
.word 0xf9401fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba1
.word 0xf9402fa2
.word 0xaa0203e0
.word 0xf940005e
bl _p_128
.word 0xf9401fb1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_52:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Bind_object
SQLite_Net_SQLiteCommand_Bind_object:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1920]
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
.word 0xd2800001
.word 0xf9400fa2
.word 0xd2800001
bl _p_129
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_53:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ToString
SQLite_Net_SQLiteCommand_ToString:
.loc 1 1 0
.word 0xa9b17bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1928]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800019
.word 0xd2800018
.word 0x9101c3a0
.word 0xd2800000
.word 0xf9003ba0
.word 0xf9003fa0
.word 0xf90043a0
.word 0xd2800017
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
.word 0xd2800020
.word 0xaa1a03e0
.word 0xf9400b41
.word 0xaa0103e0
.word 0xf940003e
bl _p_130
.word 0x93407c00
.word 0xf9005ba0
.word 0xf94023b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0x11000401

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
bl _p_16
.word 0xaa0003f9
.word 0xf94023b1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf90057a0
.word 0xd2800000
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_131
.word 0xf90053a0
.word 0xf94023b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a2
.word 0xf94057a3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94023b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800038
.word 0xf94023b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400b41
.word 0x910163a0
.word 0xaa0003e8
.word 0xaa0103e0
.word 0xf940003e
bl _p_132
.word 0xf94023b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0x910163a0
.word 0x9101c3a0
.word 0xf9402fa0
.word 0xf9003ba0
.word 0xf94033a0
.word 0xf9003fa0
.word 0xf94037a0
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000069
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0x9101c3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1936]
bl _p_133
.word 0xf90073a0
.word 0xf94023b1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xaa0003f7
.word 0xf94023b1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1944]
.word 0xf90057a0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800041
bl _p_16
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xf90067a0
.word 0xaa1603e0
.word 0xf9006fa0
.word 0xd2800000
.word 0xaa1803e0
.word 0x51000700
.word 0xf9006ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf9406ba0
.word 0xf9406fa3
.word 0xb9001040
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94067a0
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xf9005ba0
.word 0xaa1503e0
.word 0xf90063a0
.word 0xd2800020
.word 0xaa1703e1
.word 0xaa0103e0
.word 0xf940003e
bl _p_134
.word 0xf9005fa0
.word 0xf94023b1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa2
.word 0xf94063a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94057a0
.word 0xf9405ba1
bl _p_18
.word 0xf90053a0
.word 0xf94023b1
.word 0xf9432231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a2
.word 0xaa1903e0
.word 0xaa1803e1
.word 0xf9400323
.word 0xf9407870
.word 0xd63f0200
.word 0xf94023b1
.word 0xf9434a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x11000700
.word 0xaa0003f8
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9437631
.word 0xb4000051
.word 0xd63f0220
.word 0x9101c3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1936]
bl _p_135
.word 0x53001c00
.word 0xf90053a0
.word 0xf94023b1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0x35fff080
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf943ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000015
.word 0xf9004fbe
.word 0xf94023b1
.word 0xf943e631
.word 0xb4000051
.word 0xd63f0220
.word 0x9101c3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1936]
bl _p_136
.word 0xf94023b1
.word 0xf9440a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9441a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fbe
.word 0xd61f03c0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9444231
.word 0xb4000051
.word 0xd63f0220
bl _p_137
.word 0xf90057a0
.word 0xf94023b1
.word 0xf9445a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
.word 0xaa1903e1
.word 0xaa1903e1
bl _p_23
.word 0xf90053a0
.word 0xf94023b1
.word 0xf9447e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf94023b1
.word 0xf944b231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0

Lme_54:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Prepare
SQLite_Net_SQLiteCommand_Prepare:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1952]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xd2800019
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
.word 0xf9401341
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90037a0
.word 0xf94013b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f41
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_131
.word 0xf90033a0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xf94033a2
.word 0xf94037a3
.word 0xaa0303e0
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1720]
.word 0x928007f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90027a0
.word 0xaa0003f9
.word 0xf94013b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a1
.word 0xaa1a03e0
.word 0xaa0103e0
.word 0xaa1a03e0
bl _p_138
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_55:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Finalize_SQLite_Net_Interop_IDbStatement
SQLite_Net_SQLiteCommand_Finalize_SQLite_Net_Interop_IDbStatement:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1960]
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
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a2
.word 0xf9400fa1
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1744]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf94013b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_56:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_BindAll_SQLite_Net_Interop_IDbStatement
SQLite_Net_SQLiteCommand_BindAll_SQLite_Net_Interop_IDbStatement:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1968]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xd2800018
.word 0x9101a3a0
.word 0xd2800000
.word 0xf90037a0
.word 0xf9003ba0
.word 0xf9003fa0
.word 0xd2800017
.word 0xf9401fb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800038
.word 0xf9401fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400b21
.word 0x910143a0
.word 0xaa0003e8
.word 0xaa0103e0
.word 0xf940003e
bl _p_132
.word 0xf9401fb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0x910143a0
.word 0x9101a3a0
.word 0xf9402ba0
.word 0xf90037a0
.word 0xf9402fa0
.word 0xf9003ba0
.word 0xf94033a0
.word 0xf9003fa0
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x140000ca
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0x9101a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1936]
bl _p_133
.word 0xf9005ba0
.word 0xf9401fb1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf90057a0
.word 0xaa0003f7
.word 0xf9401fb1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a1
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf940003e
bl _p_139
.word 0xf90053a0
.word 0xf9401fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xb4000860
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1903e0
.word 0xf9401321
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9005ba0
.word 0xf9401fb1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_139
.word 0xf90057a0
.word 0xf9401fb1
.word 0xf9422231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a2
.word 0xf9405ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1976]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90053a0
.word 0xf9401fb1
.word 0xf9426a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xaa1703e0
.word 0xf94002fe
bl _p_140
.word 0xf9401fb1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9429a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000017
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa1803f6
.word 0xaa1803e1
.word 0xaa1803e0
.word 0x11000700
.word 0xaa0003f8
.word 0xaa1703e0
.word 0xf94002fe
bl _p_140
.word 0xf9401fb1
.word 0xf942f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9431631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401321
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90053a0
.word 0xf9401fb1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_141
.word 0x93407c00
.word 0xf90057a0
.word 0xf9401fb1
.word 0xf9438231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_134
.word 0xf9005ba0
.word 0xf9401fb1
.word 0xf943a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400f21
.word 0xaa0103e0
.word 0xf940003e
bl _p_97
.word 0x53001c00
.word 0xf9005fa0
.word 0xf9401fb1
.word 0xf943d231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400f21
.word 0xaa0103e0
.word 0xf940003e
bl _p_98
.word 0xf90063a0
.word 0xf9401fb1
.word 0xf943fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf94057a2
.word 0xf9405ba3
.word 0xf9405fa4
.word 0xf94063a5
.word 0xaa1a03e1
bl _p_99
.word 0xf9401fb1
.word 0xf9442631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9444631
.word 0xb4000051
.word 0xd63f0220
.word 0x9101a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1936]
bl _p_135
.word 0x53001c00
.word 0xf90053a0
.word 0xf9401fb1
.word 0xf9447231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0x35ffe460
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9449a31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000015
.word 0xf9004bbe
.word 0xf9401fb1
.word 0xf944b631
.word 0xb4000051
.word 0xd63f0220
.word 0x9101a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1936]
bl _p_136
.word 0xf9401fb1
.word 0xf944da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf944ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bbe
.word 0xd61f03c0
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9451231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9452231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_57:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_BindParameter_SQLite_Net_Interop_ISQLiteApi_SQLite_Net_Interop_IDbStatement_int_object_bool_SQLite_Net_IBlobSerializer
SQLite_Net_SQLiteCommand_BindParameter_SQLite_Net_Interop_ISQLiteApi_SQLite_Net_Interop_IDbStatement_int_object_bool_SQLite_Net_IBlobSerializer:
.loc 1 1 0
.word 0xd280d410
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1984]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0x910483a0
.word 0xf90093bf
.word 0xd2800014
.word 0x910463a0
.word 0xf9008fbf
.word 0xd2800013
.word 0x910423a0
.word 0xd2800000
.word 0xf90087a0
.word 0xf9008ba0
.word 0xf90097bf
.word 0xf9009bbf
.word 0x9103e3a0
.word 0xd2800000
.word 0xf9007fa0
.word 0xf90083a0
.word 0xf9009fbf
.word 0xf9402bb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb50003b8
.word 0xf9402bb1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a3

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1992]
.word 0x928009f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000d9e
.word 0xf9402bb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf900a3b8
.word 0xf940a3a0
.word 0xf900a7a0
.word 0xf940a3a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf940a3a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2000]
.word 0xeb01001f
.word 0x54000040
.word 0xf900a7bf
.word 0xf940a7a0
.word 0xb40005a0
.word 0xf9402bb1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xf9400300
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x5401b121
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2000]
.word 0xeb01001f
.word 0x10000011
.word 0x5401b021
.word 0x91004300
.word 0xb9801303
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000d5b
.word 0xf9402bb1
.word 0xf9425e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf900abb8
.word 0xf940aba0
.word 0xf900afa0
.word 0xf940aba0
.word 0xeb1f001f
.word 0x54000320
.word 0xf940aba0
.word 0xf9400000
.word 0xf900b3a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2016]
.word 0xeb01001f
.word 0x540001e3
.word 0xf940b3a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2016]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf900afbf
.word 0xf940afa0
.word 0xb4000940
.word 0xf9402bb1
.word 0xf942ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9031fb5
.word 0xf90323b6
.word 0xf90327b7
.word 0xf9032bb8
.word 0xf9432ba0
.word 0xb4000320
.word 0xf9432ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2016]
.word 0xeb02003f
.word 0x10000011
.word 0x5401a663
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2016]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x5401a480
.word 0xf9432ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2024]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x93407c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf943a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xf9431fa0
.word 0xf94323a1
.word 0xf94327a2
.word 0xf9431fa4
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf943ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf943fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000cee
.word 0xf9402bb1
.word 0xf9441231
.word 0xb4000051
.word 0xd63f0220
.word 0xf900b7b8
.word 0xf940b7a0
.word 0xf900bba0
.word 0xf940b7a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf940b7a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400400

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2032]
.word 0xeb01001f
.word 0x54000040
.word 0xf900bbbf
.word 0xf940bba0
.word 0xb40006a0
.word 0xf9402bb1
.word 0xf9446e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9030fb5
.word 0xf90313b6
.word 0xf90317b7
.word 0xf9031bb8
.word 0xf9431ba0
.word 0xb4000180
.word 0xf9431ba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400400

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2032]
.word 0xeb01001f
.word 0x10000011
.word 0x54019a21
.word 0xf9431ba3
.word 0x92800000
.word 0xf2bfffe0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xf9430fa0
.word 0xf94313a1
.word 0xf94317a2
.word 0x92800004
.word 0xf2bfffe4
.word 0xf9430fa6
.word 0xf94000c6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2048]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9451a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9452a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000ca3
.word 0xf9402bb1
.word 0xf9453e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf900bfb8
.word 0xf940bfa0
.word 0xf900c3a0
.word 0xf940bfa0
.word 0xeb1f001f
.word 0x54000320
.word 0xf940bfa0
.word 0xf9400000
.word 0xf900c7a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2056]
.word 0xeb01001f
.word 0x540001e3
.word 0xf940c7a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2056]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf900c3bf
.word 0xf940c3a0
.word 0xb4000a20
.word 0xf9402bb1
.word 0xf945ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf902ffb5
.word 0xf90303b6
.word 0xf90307b7
.word 0xf9030bb8
.word 0xf9430ba0
.word 0xb4000320
.word 0xf9430ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2056]
.word 0xeb02003f
.word 0x10000011
.word 0x54018f63
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2056]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54018d80
.word 0xf9430ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2064]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9468231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0x92800000
.word 0xf2bfffe0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xf942ffa0
.word 0xf94303a1
.word 0xf94307a2
.word 0x92800004
.word 0xf2bfffe4
.word 0xf942ffa6
.word 0xf94000c6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2048]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf946ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf946fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000c2f
.word 0xf9402bb1
.word 0xf9470e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf900cbb8
.word 0xf940cba0
.word 0xf900cfa0
.word 0xf940cba0
.word 0xeb1f001f
.word 0x54000180
.word 0xf940cba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2072]
.word 0xeb01001f
.word 0x54000040
.word 0xf900cfbf
.word 0xf940cfa0
.word 0xb50008c0
.word 0xf9402bb1
.word 0xf9476a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf900d3b8
.word 0xf940d3a0
.word 0xf900d7a0
.word 0xf940d3a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf940d3a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2080]
.word 0xeb01001f
.word 0x54000040
.word 0xf900d7bf
.word 0xf940d7a0
.word 0xb50005e0
.word 0xf9402bb1
.word 0xf947c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf900dbb8
.word 0xf940dba0
.word 0xf900dfa0
.word 0xf940dba0
.word 0xeb1f001f
.word 0x54000180
.word 0xf940dba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2088]
.word 0xeb01001f
.word 0x54000040
.word 0xf900dfbf
.word 0xf940dfa0
.word 0xb5000300
.word 0xf9402bb1
.word 0xf9482231
.word 0xb4000051
.word 0xd63f0220
.word 0xf900e3b8
.word 0xf940e3a0
.word 0xf900e7a0
.word 0xf940e3a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf940e3a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2096]
.word 0xeb01001f
.word 0x54000040
.word 0xf900e7bf
.word 0xf940e7a0
.word 0xb4000560
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9488e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_142
.word 0x93407c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf948be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9490231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9491231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000ba9
.word 0xf9402bb1
.word 0xf9492631
.word 0xb4000051
.word 0xd63f0220
.word 0xf900ebb8
.word 0xf940eba0
.word 0xf900efa0
.word 0xf940eba0
.word 0xeb1f001f
.word 0x54000320
.word 0xf940eba0
.word 0xf9400000
.word 0xf900f3a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2104]
.word 0xeb01001f
.word 0x540001e3
.word 0xf940f3a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2104]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf900efbf
.word 0xf940efa0
.word 0xb4000a40
.word 0xf9402bb1
.word 0xf949b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf902efb5
.word 0xf902f3b6
.word 0xf902f7b7
.word 0xf902fbb8
.word 0xf942fba0
.word 0xb4000320
.word 0xf942fba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2104]
.word 0xeb02003f
.word 0x10000011
.word 0x54017023
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2104]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54016e40
.word 0xf942fba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2112]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf94a6e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
bl _p_143
.word 0x93407c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf94a8e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xf942efa0
.word 0xf942f3a1
.word 0xf942f7a2
.word 0xf942efa4
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf94ad631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94ae631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000b34
.word 0xf9402bb1
.word 0xf94afa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf900f7b8
.word 0xf940f7a0
.word 0xf900fba0
.word 0xf940f7a0
.word 0xeb1f001f
.word 0x54000320
.word 0xf940f7a0
.word 0xf9400000
.word 0xf900ffa0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2120]
.word 0xeb01001f
.word 0x540001e3
.word 0xf940ffa0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2120]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf900fbbf
.word 0xf940fba0
.word 0xb4000a40
.word 0xf9402bb1
.word 0xf94b8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf902dfb5
.word 0xf902e3b6
.word 0xf902e7b7
.word 0xf902ebb8
.word 0xf942eba0
.word 0xb4000320
.word 0xf942eba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2120]
.word 0xeb02003f
.word 0x10000011
.word 0x54016183
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2120]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54015fa0
.word 0xf942eba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2128]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53003c00
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf94c4231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
bl _p_144
.word 0x93407c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf94c6231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xf942dfa0
.word 0xf942e3a1
.word 0xf942e7a2
.word 0xf942dfa4
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf94caa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94cba31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000abf
.word 0xf9402bb1
.word 0xf94cce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf90103b8
.word 0xf94103a0
.word 0xf90107a0
.word 0xf94103a0
.word 0xeb1f001f
.word 0x54000320
.word 0xf94103a0
.word 0xf9400000
.word 0xf9010ba0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2136]
.word 0xeb01001f
.word 0x540001e3
.word 0xf9410ba0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2136]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf90107bf
.word 0xf94107a0
.word 0xb4000a40
.word 0xf9402bb1
.word 0xf94d5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf902cfb5
.word 0xf902d3b6
.word 0xf902d7b7
.word 0xf902dbb8
.word 0xf942dba0
.word 0xb4000320
.word 0xf942dba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2136]
.word 0xeb02003f
.word 0x10000011
.word 0x540152e3
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2136]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54015100
.word 0xf942dba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2144]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x93401c00
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf94e1631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
bl _p_145
.word 0x93407c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf94e3631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xf942cfa0
.word 0xf942d3a1
.word 0xf942d7a2
.word 0xf942cfa4
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf94e7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94e8e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000a4a
.word 0xf9402bb1
.word 0xf94ea231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9010fb8
.word 0xf9410fa0
.word 0xf90113a0
.word 0xf9410fa0
.word 0xeb1f001f
.word 0x54000320
.word 0xf9410fa0
.word 0xf9400000
.word 0xf90117a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2152]
.word 0xeb01001f
.word 0x540001e3
.word 0xf94117a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2152]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf90113bf
.word 0xf94113a0
.word 0xb4000a40
.word 0xf9402bb1
.word 0xf94f3231
.word 0xb4000051
.word 0xd63f0220
.word 0xf902bfb5
.word 0xf902c3b6
.word 0xf902c7b7
.word 0xf902cbb8
.word 0xf942cba0
.word 0xb4000320
.word 0xf942cba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2152]
.word 0xeb02003f
.word 0x10000011
.word 0x54014443
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2152]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54014260
.word 0xf942cba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2160]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x93403c00
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf94fea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
bl _p_146
.word 0x93407c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9500a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xf942bfa0
.word 0xf942c3a1
.word 0xf942c7a2
.word 0xf942bfa4
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9505231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9506231
.word 0xb4000051
.word 0xd63f0220
.word 0x140009d5
.word 0xf9402bb1
.word 0xf9507631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9011bb8
.word 0xf9411ba0
.word 0xf9011fa0
.word 0xf9411ba0
.word 0xeb1f001f
.word 0x54000180
.word 0xf9411ba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2168]
.word 0xeb01001f
.word 0x54000040
.word 0xf9011fbf
.word 0xf9411fa0
.word 0xb40008a0
.word 0xf9402bb1
.word 0xf950d231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xf9400300
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x54013801
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2168]
.word 0xeb01001f
.word 0x10000011
.word 0x54013701
.word 0x91004300
.word 0x39404300
.word 0xf902afb5
.word 0xf902b3b6
.word 0xb9056bb7
.word 0x35000140
.word 0xf942afa2
.word 0xf942b3a1
.word 0xb9856ba0
.word 0xd2800003
.word 0xf902afa2
.word 0xf902b3a1
.word 0xb9056ba0
.word 0xb90573bf
.word 0x1400000a
.word 0xf942afa2
.word 0xf942b3a1
.word 0xb9856ba0
.word 0xd2800023
.word 0xf902afa2
.word 0xf902b3a1
.word 0xb9056ba0
.word 0xd280003e
.word 0xb90573be
.word 0xf942afa4
.word 0xf942b3a1
.word 0xb9856ba2
.word 0xb98573a3
.word 0xaa0403e0
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf951be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf951ce31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400097a
.word 0xf9402bb1
.word 0xf951e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf90123b8
.word 0xf94123a0
.word 0xf90127a0
.word 0xf94123a0
.word 0xeb1f001f
.word 0x54000320
.word 0xf94123a0
.word 0xf9400000
.word 0xf9012ba0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2176]
.word 0xeb01001f
.word 0x540001e3
.word 0xf9412ba0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2176]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf90127bf
.word 0xf94127a0
.word 0xb4000c80
.word 0xf9402bb1
.word 0xf9527231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9029fb5
.word 0xf902a3b6
.word 0xf902a7b7
.word 0xf902abb8
.word 0xf942aba0
.word 0xb4000320
.word 0xf942aba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2176]
.word 0xeb02003f
.word 0x10000011
.word 0x54012a43
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2176]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54012860
.word 0xf942aba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2184]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9532a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a0
.word 0xf9429fa1
.word 0xf902afa1
.word 0xf942a3a1
.word 0xf902b3a1
.word 0xf942a7a1
.word 0xb9056ba1
.word 0x35000140
.word 0xf942afa2
.word 0xf942b3a1
.word 0xb9856ba0
.word 0xd2800003
.word 0xf902afa2
.word 0xf902b3a1
.word 0xb9056ba0
.word 0xb90573bf
.word 0x1400000a
.word 0xf942afa2
.word 0xf942b3a1
.word 0xb9856ba0
.word 0xd2800023
.word 0xf902afa2
.word 0xf902b3a1
.word 0xb9056ba0
.word 0xd280003e
.word 0xb90573be
.word 0xf942afa4
.word 0xf942b3a1
.word 0xb9856ba2
.word 0xb98573a3
.word 0xaa0403e0
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf953da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf953ea31
.word 0xb4000051
.word 0xd63f0220
.word 0x140008f3
.word 0xf9402bb1
.word 0xf953fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9012fb8
.word 0xf9412fa0
.word 0xf90133a0
.word 0xf9412fa0
.word 0xeb1f001f
.word 0x54000180
.word 0xf9412fa0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2192]
.word 0xeb01001f
.word 0x54000040
.word 0xf90133bf
.word 0xf94133a0
.word 0xb5000300
.word 0xf9402bb1
.word 0xf9545a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf90137b8
.word 0xf94137a0
.word 0xf9013ba0
.word 0xf94137a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf94137a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2200]
.word 0xeb01001f
.word 0x54000040
.word 0xf9013bbf
.word 0xf9413ba0
.word 0xb4000540
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf954c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_147
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf954f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2208]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9553631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9554631
.word 0xb4000051
.word 0xd63f0220
.word 0x1400089c
.word 0xf9402bb1
.word 0xf9555a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9013fb8
.word 0xf9413fa0
.word 0xf90143a0
.word 0xf9413fa0
.word 0xeb1f001f
.word 0x54000320
.word 0xf9413fa0
.word 0xf9400000
.word 0xf90147a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2216]
.word 0xeb01001f
.word 0x540001e3
.word 0xf94147a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2216]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf90143bf
.word 0xf94143a0
.word 0xb4000a00
.word 0xf9402bb1
.word 0xf955ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9028fb5
.word 0xf90293b6
.word 0xf90297b7
.word 0xf9029bb8
.word 0xf9429ba0
.word 0xb4000320
.word 0xf9429ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2216]
.word 0xeb02003f
.word 0x10000011
.word 0x54010e83
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2216]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54010ca0
.word 0xf9429ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2224]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf9569e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
bl _p_148
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf956ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xf9428fa0
.word 0xf94293a1
.word 0xf94297a2
.word 0xf9428fa4
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2208]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9570231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9571231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000829
.word 0xf9402bb1
.word 0xf9572631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9014bb8
.word 0xf9414ba0
.word 0xf9014fa0
.word 0xf9414ba0
.word 0xeb1f001f
.word 0x54000320
.word 0xf9414ba0
.word 0xf9400000
.word 0xf90153a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2232]
.word 0xeb01001f
.word 0x540001e3
.word 0xf94153a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2232]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf9014fbf
.word 0xf9414fa0
.word 0xb4000a00
.word 0xf9402bb1
.word 0xf957b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9027fb5
.word 0xf90283b6
.word 0xf90287b7
.word 0xf9028bb8
.word 0xf9428ba0
.word 0xb4000320
.word 0xf9428ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2232]
.word 0xeb02003f
.word 0x10000011
.word 0x54010023
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2232]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x5400fe40
.word 0xf9428ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2240]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf9586a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
bl _p_149
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9588631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xf9427fa0
.word 0xf94283a1
.word 0xf94287a2
.word 0xf9427fa4
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2208]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf958ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf958de31
.word 0xb4000051
.word 0xd63f0220
.word 0x140007b6
.word 0xf9402bb1
.word 0xf958f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf90157b8
.word 0xf94157a0
.word 0xf9015ba0
.word 0xf94157a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf94157a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2248]
.word 0xeb01001f
.word 0x54000040
.word 0xf9015bbf
.word 0xf9415ba0
.word 0xb50005e0
.word 0xf9402bb1
.word 0xf9594e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9015fb8
.word 0xf9415fa0
.word 0xf90163a0
.word 0xf9415fa0
.word 0xeb1f001f
.word 0x54000180
.word 0xf9415fa0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2256]
.word 0xeb01001f
.word 0x54000040
.word 0xf90163bf
.word 0xf94163a0
.word 0xb5000300
.word 0xf9402bb1
.word 0xf959aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf90167b8
.word 0xf94167a0
.word 0xf9016ba0
.word 0xf94167a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf94167a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2264]
.word 0xeb01001f
.word 0x54000040
.word 0xf9016bbf
.word 0xf9416ba0
.word 0xb4000540
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95a1631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_150
.word 0xfd033ba0
.word 0xf9402bb1
.word 0xf95a4231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd433ba0
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a3

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2272]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf95a8631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95a9631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000748
.word 0xf9402bb1
.word 0xf95aaa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9016fb8
.word 0xf9416fa0
.word 0xf90173a0
.word 0xf9416fa0
.word 0xeb1f001f
.word 0x54000320
.word 0xf9416fa0
.word 0xf9400000
.word 0xf90177a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2280]
.word 0xeb01001f
.word 0x540001e3
.word 0xf94177a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2280]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf90173bf
.word 0xf94173a0
.word 0xb4000a40
.word 0xf9402bb1
.word 0xf95b3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9026fb5
.word 0xf90273b6
.word 0xf90277b7
.word 0xf9027bb8
.word 0xf9427ba0
.word 0xb4000320
.word 0xf9427ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2280]
.word 0xeb02003f
.word 0x10000011
.word 0x5400e403
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2280]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x5400e220
.word 0xf9427ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2288]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x1e22c000
.word 0xfd033fa0
.word 0xf9402bb1
.word 0xf95bf231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd433fa0
.word 0x1e624000
bl _p_151
.word 0xfd033ba0
.word 0xf9402bb1
.word 0xf95c1231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd433ba0
.word 0xf9426fa0
.word 0xf94273a1
.word 0xf94277a2
.word 0xf9426fa3
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2272]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf95c5a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95c6a31
.word 0xb4000051
.word 0xd63f0220
.word 0x140006d3
.word 0xf9402bb1
.word 0xf95c7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9017bb8
.word 0xf9417ba0
.word 0xf9017fa0
.word 0xf9417ba0
.word 0xeb1f001f
.word 0x54000320
.word 0xf9417ba0
.word 0xf9400000
.word 0xf90183a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2296]
.word 0xeb01001f
.word 0x540001e3
.word 0xf94183a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2296]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf9017fbf
.word 0xf9417fa0
.word 0xb4000a00
.word 0xf9402bb1
.word 0xf95d0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9025fb5
.word 0xf90263b6
.word 0xf90267b7
.word 0xf9026bb8
.word 0xf9426ba0
.word 0xb4000320
.word 0xf9426ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2296]
.word 0xeb02003f
.word 0x10000011
.word 0x5400d563
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2296]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x5400d380
.word 0xf9426ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2304]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xfd033fa0
.word 0xf9402bb1
.word 0xf95dc231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd433fa0
bl _p_152
.word 0xfd033ba0
.word 0xf9402bb1
.word 0xf95dde31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd433ba0
.word 0xf9425fa0
.word 0xf94263a1
.word 0xf94267a2
.word 0xf9425fa3
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2272]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf95e2631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95e3631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000660
.word 0xf9402bb1
.word 0xf95e4a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf90187b8
.word 0xf94187a0
.word 0xf9018ba0
.word 0xf94187a0
.word 0xeb1f001f
.word 0x54000320
.word 0xf94187a0
.word 0xf9400000
.word 0xf9018fa0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2312]
.word 0xeb01001f
.word 0x540001e3
.word 0xf9418fa0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2312]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf9018bbf
.word 0xf9418ba0
.word 0xb4000ac0
.word 0xf9402bb1
.word 0xf95eda31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9024fb5
.word 0xf90253b6
.word 0xf90257b7
.word 0xf9025bb8
.word 0xf9425ba0
.word 0xb4000320
.word 0xf9425ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2312]
.word 0xeb02003f
.word 0x10000011
.word 0x5400c703
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2312]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x5400c520
.word 0xf9425ba1
.word 0x9103a3a0
.word 0xf90203a0
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2320]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94203be
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9402bb1
.word 0xf95f9e31
.word 0xb4000051
.word 0xd63f0220
.word 0x9103a3a0
.word 0xf94077a0
.word 0xf9407ba1
bl _p_153
.word 0xfd033ba0
.word 0xf9402bb1
.word 0xf95fc231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd433ba0
.word 0xf9424fa0
.word 0xf94253a1
.word 0xf94257a2
.word 0xf9424fa3
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2272]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9600a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9601a31
.word 0xb4000051
.word 0xd63f0220
.word 0x140005e7
.word 0xf9402bb1
.word 0xf9602e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf90193b8
.word 0xf94193a0
.word 0xf90197a0
.word 0xf94193a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf94193a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2328]
.word 0xeb01001f
.word 0x54000040
.word 0xf90197bf
.word 0xf94197a0
.word 0xb4000760
.word 0xf9402bb1
.word 0xf9608a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xf9400300
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x5400ba41
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2328]
.word 0xeb01001f
.word 0x10000011
.word 0x5400b941
.word 0x91004300
.word 0x910263a1
.word 0xf9400000
.word 0xf9004fa0
.word 0x910263a0
.word 0x910483a0
.word 0xf9404fa0
.word 0xf90093a0
.word 0x910483a0
bl _p_154
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9610a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2208]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9614e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9615e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000596
.word 0xf9402bb1
.word 0xf9617231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9019bb8
.word 0xf9419ba0
.word 0xf9019fa0
.word 0xf9419ba0
.word 0xeb1f001f
.word 0x54000320
.word 0xf9419ba0
.word 0xf9400000
.word 0xf901a3a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2336]
.word 0xeb01001f
.word 0x540001e3
.word 0xf941a3a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2336]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf9019fbf
.word 0xf9419fa0
.word 0xb4000ae0
.word 0xf9402bb1
.word 0xf9620231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9023fb5
.word 0xf90243b6
.word 0xf90247b7
.word 0xf9024bb8
.word 0xf9424ba0
.word 0xb4000320
.word 0xf9424ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2336]
.word 0xeb02003f
.word 0x10000011
.word 0x5400adc3
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2336]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x5400abe0
.word 0xf9424ba1
.word 0x910383a0
.word 0xf90203a0
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2344]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94203be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf962c231
.word 0xb4000051
.word 0xd63f0220
.word 0x910383a0
.word 0x910483a0
.word 0xf94073a0
.word 0xf90093a0
.word 0x910483a0
bl _p_154
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf962ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xf9423fa0
.word 0xf94243a1
.word 0xf94247a2
.word 0xf9423fa4
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2208]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9633631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9634631
.word 0xb4000051
.word 0xd63f0220
.word 0x1400051c
.word 0xf9402bb1
.word 0xf9635a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf901a7b8
.word 0xf941a7a0
.word 0xf901aba0
.word 0xf941a7a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf941a7a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2352]
.word 0xeb01001f
.word 0x54000040
.word 0xf901abbf
.word 0xf941aba0
.word 0xb40019a0
.word 0xf9402bb1
.word 0xf963b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x34000b39
.word 0xf9402bb1
.word 0xf963ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400300
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x5400a081
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2352]
.word 0xeb01001f
.word 0x10000011
.word 0x54009f81
.word 0x91004300
.word 0x910243a1
.word 0xf9400000
.word 0xf9004ba0
.word 0x910243a0
.word 0x910463a0
.word 0xf9404ba0
.word 0xf9008fa0
.word 0xf9402bb1
.word 0xf9643631
.word 0xb4000051
.word 0xd63f0220
.word 0x910463a0
.word 0x910363a1
.word 0xf90203a1
bl _p_155
.word 0xf94203be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf9645e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910363a0
.word 0x910463a0
.word 0xf9406fa0
.word 0xf9008fa0
.word 0xf9402bb1
.word 0xf9647e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910463a0
bl _p_156
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf9649a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
.word 0xf90333a0
.word 0xaa0003f4
.word 0xf9402bb1
.word 0xf964b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa0303e0
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2208]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9650a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9651a31
.word 0xb4000051
.word 0xd63f0220
.word 0x140004a7
.word 0xf9402bb1
.word 0xf9652e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400300
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x54009581
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2352]
.word 0xeb01001f
.word 0x10000011
.word 0x54009481
.word 0x91004300
.word 0x910223a1
.word 0xf9400000
.word 0xf90047a0
.word 0x910223a0
.word 0x910463a0
.word 0xf94047a0
.word 0xf9008fa0
.word 0xf9402bb1
.word 0xf9659631
.word 0xb4000051
.word 0xd63f0220
.word 0x910463a0
.word 0x910343a1
.word 0xf90203a1
bl _p_155
.word 0xf94203be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf965be31
.word 0xb4000051
.word 0xd63f0220
.word 0x910343a0
.word 0x910463a0
.word 0xf9406ba0
.word 0xf9008fa0
.word 0xf9402bb1
.word 0xf965de31
.word 0xb4000051
.word 0xd63f0220
.word 0x910463a0
.word 0xf90343a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2360]
.word 0xf90347a0
bl _p_157
.word 0xf9034ba0
.word 0xf9402bb1
.word 0xf9660e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94343a0
.word 0xf94347a1
.word 0xf9434ba2
bl _p_158
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf9663231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
.word 0xf90333a0
.word 0xaa0003f3
.word 0xf9402bb1
.word 0xf9664e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa0303e0
.word 0x92800000
.word 0xf2bfffe0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0x92800004
.word 0xf2bfffe4
.word 0xf94002a6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2048]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf966c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf966d231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000439
.word 0xf9402bb1
.word 0xf966e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf901afb8
.word 0xf941afa0
.word 0xf901b3a0
.word 0xf941afa0
.word 0xeb1f001f
.word 0x54000180
.word 0xf941afa0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2368]
.word 0xeb01001f
.word 0x54000040
.word 0xf901b3bf
.word 0xf941b3a0
.word 0xb40007e0
.word 0xf9402bb1
.word 0xf9674231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xf9400300
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x54008481
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2368]
.word 0xeb01001f
.word 0x10000011
.word 0x54008381
.word 0x91004300
.word 0x9101e3a1
.word 0xf9400001
.word 0xf9003fa1
.word 0xf9400400
.word 0xf90043a0
.word 0x9101e3a0
.word 0x910423a0
.word 0xf9403fa0
.word 0xf90087a0
.word 0xf94043a0
.word 0xf9008ba0
.word 0x910423a0
bl _p_159
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf967d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2208]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9681631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9682631
.word 0xb4000051
.word 0xd63f0220
.word 0x140003e4
.word 0xf9402bb1
.word 0xf9683a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf901b7b8
.word 0xf941b7a0
.word 0xf901bba0
.word 0xf941b7a0
.word 0xeb1f001f
.word 0x54000320
.word 0xf941b7a0
.word 0xf9400000
.word 0xf901bfa0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2376]
.word 0xeb01001f
.word 0x540001e3
.word 0xf941bfa0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2376]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf901bbbf
.word 0xf941bba0
.word 0xb4001fe0
.word 0xf9402bb1
.word 0xf968ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x34000e59
.word 0xf9402bb1
.word 0xf968e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9023bb8
.word 0xf9423ba0
.word 0xb4000320
.word 0xf9423ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2376]
.word 0xeb02003f
.word 0x10000011
.word 0x54007723
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2376]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54007540
.word 0xf9423ba1
.word 0x910323a0
.word 0xf90203a0
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2384]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94203be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf9699631
.word 0xb4000051
.word 0xd63f0220
.word 0x910323a0
.word 0x910463a0
.word 0xf94067a0
.word 0xf9008fa0
.word 0xf9402bb1
.word 0xf969b631
.word 0xb4000051
.word 0xd63f0220
.word 0x910463a0
.word 0x910303a1
.word 0xf90203a1
bl _p_155
.word 0xf94203be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf969de31
.word 0xb4000051
.word 0xd63f0220
.word 0x910303a0
.word 0x910463a0
.word 0xf94063a0
.word 0xf9008fa0
.word 0xf9402bb1
.word 0xf969fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x910463a0
bl _p_156
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf96a1a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a0
.word 0xf90097a0
.word 0xf9402bb1
.word 0xf96a3231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xf94097a3
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2208]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf96a8231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf96a9231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000349
.word 0xf9402bb1
.word 0xf96aa631
.word 0xb4000051
.word 0xd63f0220
.word 0xf90237b8
.word 0xf94237a0
.word 0xb4000320
.word 0xf94237a0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2376]
.word 0xeb02003f
.word 0x10000011
.word 0x54006903
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2376]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54006720
.word 0xf94237a1
.word 0x9102e3a0
.word 0xf90203a0
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2384]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94203be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf96b5a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9102e3a0
.word 0x910463a0
.word 0xf9405fa0
.word 0xf9008fa0
.word 0xf9402bb1
.word 0xf96b7a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910463a0
.word 0x9102c3a1
.word 0xf90203a1
bl _p_155
.word 0xf94203be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf96ba231
.word 0xb4000051
.word 0xd63f0220
.word 0x9102c3a0
.word 0x910463a0
.word 0xf9405ba0
.word 0xf9008fa0
.word 0xf9402bb1
.word 0xf96bc231
.word 0xb4000051
.word 0xd63f0220
.word 0x910463a0
.word 0xf90337a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2360]
.word 0xf90343a0
bl _p_157
.word 0xf90347a0
.word 0xf9402bb1
.word 0xf96bf231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
.word 0xf94343a1
.word 0xf94347a2
bl _p_158
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf96c1631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a0
.word 0xf9009ba0
.word 0xf9402bb1
.word 0xf96c2e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xf9409ba3
.word 0x92800000
.word 0xf2bfffe0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0x92800004
.word 0xf2bfffe4
.word 0xf94002a6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2048]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf96c9e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf96cae31
.word 0xb4000051
.word 0xd63f0220
.word 0x140002c2
.word 0xf9402bb1
.word 0xf96cc231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400300
.word 0xf9400c00
.word 0xf90343a0
.word 0xf9402bb1
.word 0xf96ce231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94343a0
bl _p_56
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf96cfe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9411030
.word 0xd63f0200
.word 0x53001c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf96d2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a0
.word 0x340004e0
.word 0xf9402bb1
.word 0xf96d4231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_142
.word 0x93407c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf96d7231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2008]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf96db631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf96dc631
.word 0xb4000051
.word 0xd63f0220
.word 0x1400027c
.word 0xf9402bb1
.word 0xf96dda31
.word 0xb4000051
.word 0xd63f0220
.word 0xf901c3b8
.word 0xf941c3a0
.word 0xf901c7a0
.word 0xf941c3a0
.word 0xeb1f001f
.word 0x540002c0
.word 0xf941c3a0
.word 0xf9400001
.word 0xf901cba1
.word 0xf9400800
.word 0xb5000200
.word 0xf941cba0
.word 0x3940b000
.word 0xd280003e
.word 0xeb1e001f
.word 0x54000161
.word 0xf941cba0
.word 0xf9400000
.word 0xf9400400
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2072]
.word 0xeb01001f
.word 0x54000040
.word 0xf901c7bf
.word 0xf941c7a0
.word 0xb4000ae0
.word 0xf9402bb1
.word 0xf96e5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9021fb5
.word 0xf90223b6
.word 0xf90227b7
.word 0xf9022bb8
.word 0xf9422ba0
.word 0xb40002c0
.word 0xf9422ba0
.word 0xf9400001
.word 0xf9400021
.word 0x39406822
.word 0xd280003e
.word 0xeb1e005f
.word 0x10000011
.word 0x54004b01
.word 0xf9400421
.word 0xf9400821
.word 0xf9400821

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2072]
.word 0xeb02003f
.word 0x10000011
.word 0x540049e1
.word 0xf9400800
.word 0xeb1f001f
.word 0x10000011
.word 0x54004961
.word 0xf9422ba0
.word 0xf9022fa0
.word 0xf90233b8
.word 0xf94233a0
.word 0xb40002c0
.word 0xf94233a0
.word 0xf9400001
.word 0xf9400021
.word 0x39406822
.word 0xd280003e
.word 0xeb1e005f
.word 0x10000011
.word 0x540047c1
.word 0xf9400421
.word 0xf9400821
.word 0xf9400821

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2072]
.word 0xeb02003f
.word 0x10000011
.word 0x540046a1
.word 0xf9400800
.word 0xeb1f001f
.word 0x10000011
.word 0x54004621
.word 0xf94233a0
.word 0xb9801804

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xf9421fa0
.word 0xf94223a1
.word 0xf94227a2
.word 0xf9422fa3
.word 0xf9421fa6
.word 0xf94000c6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2392]
.word 0x928009f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf96f9231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf96fa231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000205
.word 0xf9402bb1
.word 0xf96fb631
.word 0xb4000051
.word 0xd63f0220
.word 0xf901cfb8
.word 0xf941cfa0
.word 0xf901d3a0
.word 0xf941cfa0
.word 0xeb1f001f
.word 0x54000320
.word 0xf941cfa0
.word 0xf9400000
.word 0xf901d7a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2400]
.word 0xeb01001f
.word 0x540001e3
.word 0xf941d7a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2400]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf901d3bf
.word 0xf941d3a0
.word 0xb4000f20
.word 0xf9402bb1
.word 0xf9704631
.word 0xb4000051
.word 0xd63f0220
.word 0xf90207b5
.word 0xf9020bb6
.word 0xf9020fb7
.word 0xf90213b8
.word 0xf94213a0
.word 0xb4000320
.word 0xf94213a0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2400]
.word 0xeb02003f
.word 0x10000011
.word 0x54003ba3
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2400]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x540039c0
.word 0xf94213a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2408]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90217a0
.word 0xf9402bb1
.word 0xf970fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9021bb8
.word 0xf9421ba0
.word 0xb4000320
.word 0xf9421ba0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2400]
.word 0xeb02003f
.word 0x10000011
.word 0x54003663
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2400]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54003480
.word 0xf9421ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2408]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf971a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a0
.word 0xb9801804

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xf94207a0
.word 0xf9420ba1
.word 0xf9420fa2
.word 0xf94217a3
.word 0xf94207a6
.word 0xf94000c6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2392]
.word 0x928009f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9720231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9721231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000169
.word 0xf9402bb1
.word 0xf9722631
.word 0xb4000051
.word 0xd63f0220
.word 0xf901dbb8
.word 0xf941dba0
.word 0xf901dfa0
.word 0xf941dba0
.word 0xeb1f001f
.word 0x54000180
.word 0xf941dba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2416]
.word 0xeb01001f
.word 0x54000040
.word 0xf901dfbf
.word 0xf941dfa0
.word 0xb40008a0
.word 0xf9402bb1
.word 0xf9728231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xf9400300
.word 0x3940b001
.word 0xeb1f003f
.word 0x10000011
.word 0x54002a81
.word 0xf9400000
.word 0xf9400000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2416]
.word 0xeb01001f
.word 0x10000011
.word 0x54002981
.word 0x91004300
.word 0x9101a3a1
.word 0xf9400001
.word 0xf90037a1
.word 0xf9400400
.word 0xf9003ba0
.word 0x9101a3a0
.word 0x9103e3a0
.word 0xf94037a0
.word 0xf9007fa0
.word 0xf9403ba0
.word 0xf90083a0
.word 0x9103e3a0
bl _p_160
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9731231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xd2800900

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xd2800904
.word 0xf94002a6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2048]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9736e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9737e31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400010e
.word 0xf9402bb1
.word 0xf9739231
.word 0xb4000051
.word 0xd63f0220
.word 0xf901e3b8
.word 0xf941e3a0
.word 0xf901e7a0
.word 0xf941e3a0
.word 0xeb1f001f
.word 0x54000320
.word 0xf941e3a0
.word 0xf9400000
.word 0xf901eba0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2424]
.word 0xeb01001f
.word 0x540001e3
.word 0xf941eba0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2424]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf901e7bf
.word 0xf941e7a0
.word 0xb4000c00
.word 0xf9402bb1
.word 0xf9742231
.word 0xb4000051
.word 0xd63f0220
.word 0xf901f3b5
.word 0xf901f7b6
.word 0xf901fbb7
.word 0xf901ffb8
.word 0xf941ffa0
.word 0xb4000320
.word 0xf941ffa0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2424]
.word 0xeb02003f
.word 0x10000011
.word 0x54001cc3
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2424]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x54001ae0
.word 0xf941ffa1
.word 0x910283a0
.word 0xf90203a0
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2432]
.word 0x92800ff0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94203be
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9402bb1
.word 0xf974e631
.word 0xb4000051
.word 0xd63f0220
.word 0x910283a0
.word 0x9103e3a0
.word 0xf94053a0
.word 0xf9007fa0
.word 0xf94057a0
.word 0xf90083a0
.word 0x9103e3a0
bl _p_160
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9751a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a3
.word 0xd2800900

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xf941f3a0
.word 0xf941f7a1
.word 0xf941fba2
.word 0xd2800904
.word 0xf941f3a6
.word 0xf94000c6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2048]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9757a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9758a31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400008b
.word 0xf9402bb1
.word 0xf9759e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb4000bfa
.word 0xf9402bb1
.word 0xf975b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xf9400300
.word 0xf9400c00
.word 0xf90337a0
.word 0xf9402bb1
.word 0xf975da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a1
.word 0xaa1a03e0
.word 0xf9400342

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1336]
.word 0x928005f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x53001c00
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9761a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a0
.word 0x34000800
.word 0xf9402bb1
.word 0xf9763231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2440]

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2448]
.word 0xaa1a03e0
.word 0xaa1803e1
.word 0xf9400342
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9768231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a0
.word 0xf9009fa0
.word 0xf9402bb1
.word 0xf9769a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xf9409fa0
.word 0xf901efa0
.word 0xf941efa3
.word 0xf941efa0
.word 0xb9801804

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9400005
.word 0xaa1503e0
.word 0xaa1603e1
.word 0xaa1703e2
.word 0xf94002a6

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2392]
.word 0x928009f0
.word 0xf2bffff0
.word 0xf87068d0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9402bb1
.word 0xf9770a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9771a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000027
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9773e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2805be1
.word 0xd2805be1
bl _p_32
.word 0xf90337a0
.word 0xaa1803e0
.word 0xf9400300
.word 0xf9400c00
.word 0xf90343a0
.word 0xf9402bb1
.word 0xf9777a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94337a0
.word 0xf94343a1
bl _p_63
.word 0xf90333a0
.word 0xf9402bb1
.word 0xf9779a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94333a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402bb1
.word 0xf977c631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa9407bfd
.word 0xd280d410
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_27

Lme_58:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ReadCol_SQLite_Net_Interop_IDbStatement_int_SQLite_Net_Interop_ColType_System_Type
SQLite_Net_SQLiteCommand_ReadCol_SQLite_Net_Interop_IDbStatement_int_SQLite_Net_Interop_ColType_System_Type:
.loc 1 1 0
.word 0xd2805410
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xf90023ba
.word 0xaa0003f6
.word 0xaa0103f7
.word 0xaa0203f8
.word 0xf90027a3
.word 0xaa0403fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2456]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800015
.word 0xd2800014
.word 0xd2800013
.word 0x390583bf
.word 0x9e6703e0
.word 0xfd00b7a0
.word 0x9e6703e0
.word 0x1e624010
.word 0xbd0173b0
.word 0x910563a0
.word 0xf900afbf
.word 0x910543a0
.word 0xf900abbf
.word 0xf900bfbf
.word 0xb90183bf
.word 0x910503a0
.word 0xd2800000
.word 0xf900a3a0
.word 0xf900a7a0
.word 0x390623bf
.word 0x790323bf
.word 0x790333bf
.word 0x390683bf
.word 0xf900d7bf
.word 0x9104c3a0
.word 0xd2800000
.word 0xf9009ba0
.word 0xf9009fa0
.word 0xf900dbbf
.word 0xf9402bb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_56
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9421430
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #960]
bl _p_57
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0xaa0003f5
.word 0xf9402bb1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xb9804ba0
.word 0xd28000a1
.word 0xd28000be
.word 0x6b1e001f
.word 0x54000201
.word 0xf9402bb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf941c631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x14000ef5
.word 0xf9402bb1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1176]
.word 0xeb00035f
.word 0x54000641
.word 0xf9402bb1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2464]
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000eba
.word 0xf9402bb1
.word 0xf942ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1184]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf942fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000ca0
.word 0xf9402bb1
.word 0xf9431631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90133a0
.word 0xf9402bb1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2464]
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa0
.word 0xaa0003f4
.word 0xf9402bb1
.word 0xf943ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf943e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf9011fa0
.word 0xf9411fa0
.word 0xf90127a0
.word 0xf9411fa3
.word 0xd2800000
.word 0xaa1403e2
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9446e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9448e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000e43
.word 0xf9402bb1
.word 0xf944a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1016]
.word 0xeb00035f
.word 0x54000781
.word 0xf9402bb1
.word 0xf944ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9450e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9455e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xf94127a1
.word 0xb9001001
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf945a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000dfe
.word 0xf9402bb1
.word 0xf945ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1080]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf945ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000e40
.word 0xf9402bb1
.word 0xf9460631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf9464631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf9469631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a0
.word 0xaa0003f3
.word 0xf9402bb1
.word 0xf946ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf946d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf9011ba0
.word 0xf9411ba0
.word 0xf90127a0
.word 0xf9411ba0
.word 0xf90133a0
.word 0xd2800000
.word 0xaa1303e0
.word 0xf9012fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa0
.word 0xf94133a3
.word 0xb9001040
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9479231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf947b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000d7a
.word 0xf9402bb1
.word 0xf947ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #976]
.word 0xeb00035f
.word 0x54000841
.word 0xf9402bb1
.word 0xf947f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf9483231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9488231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x9a9f17e0
.word 0xf90127a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2488]
.word 0xd2800221
.word 0xd2800221
bl _p_20
.word 0xf94127a1
.word 0x39004001
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf948de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000d2f
.word 0xf9402bb1
.word 0xf948f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1040]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9492a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000ec0
.word 0xf9402bb1
.word 0xf9494231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf9498231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf949d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x9a9f17e0
.word 0x390583a0
.word 0xf9402bb1
.word 0xf949fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf94a2231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf90117a0
.word 0xf94117a0
.word 0xf90127a0
.word 0xf94117a0
.word 0xf90133a0
.word 0xd2800000
.word 0x394583a0
.word 0xf9012fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2488]
.word 0xd2800221
.word 0xd2800221
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa0
.word 0xf94133a3
.word 0x39004040
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf94ade31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94afe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000ca7
.word 0xf9402bb1
.word 0xf94b1631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1128]
.word 0xeb00035f
.word 0x54000761
.word 0xf9402bb1
.word 0xf94b3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf94b7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2496]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xfd013fa0
.word 0xf9402bb1
.word 0xf94bca31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2504]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xfd413fa0
.word 0xfd000800
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94c0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000c63
.word 0xf9402bb1
.word 0xf94c2631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1152]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf94c5a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000e20
.word 0xf9402bb1
.word 0xf94c7231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf94cb231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2496]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xfd0147a0
.word 0xf9402bb1
.word 0xf94cfe31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4147a0
.word 0xfd00b7a0
.word 0xf9402bb1
.word 0xf94d1631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf94d3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf90113a0
.word 0xf94113a0
.word 0xf90127a0
.word 0xf94113a0
.word 0xf9012fa0
.word 0xd2800000
.word 0xfd40b7a0
.word 0xfd0143a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2504]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa3
.word 0xfd4143a0
.word 0xfd000840
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf94dfa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94e1a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000be0
.word 0xf9402bb1
.word 0xf94e3231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1120]
.word 0xeb00035f
.word 0x54000801
.word 0xf9402bb1
.word 0xf94e5a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf94e9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2496]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xfd014ba0
.word 0xf9402bb1
.word 0xf94ee631
.word 0xb4000051
.word 0xd63f0220
.word 0xfd414ba0
.word 0x1e624010
.word 0x1e22c200
.word 0xfd013fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2512]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xfd413fa0
.word 0x1e624010
.word 0xbd001010
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf94f3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000b97
.word 0xf9402bb1
.word 0xf94f5631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1144]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf94f8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000ec0
.word 0xf9402bb1
.word 0xf94fa231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf94fe231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2496]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xfd0147a0
.word 0xf9402bb1
.word 0xf9502e31
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4147a0
.word 0x1e624010
.word 0x1e22c200
.word 0x1e624010
.word 0xbd0173b0
.word 0xf9402bb1
.word 0xf9505231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9507a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf9010fa0
.word 0xf9410fa0
.word 0xf90127a0
.word 0xf9410fa0
.word 0xf9012fa0
.word 0xd2800000
.word 0xbd4173b0
.word 0x1e22c200
.word 0xfd0143a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2512]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa3
.word 0xfd4143a0
.word 0x1e624010
.word 0xbd001050
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9513e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9515e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000b0f
.word 0xf9402bb1
.word 0xf9517631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1232]
.word 0xeb00035f
.word 0x54000941
.word 0xf9402bb1
.word 0xf9519e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf951de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9522a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a1
.word 0x9104a3a0
.word 0xf90097bf
.word 0x9104a3a0
bl _p_162
.word 0x9104a3a0
.word 0x910323a0
.word 0xf94097a0
.word 0xf90067a0
.word 0xf9402bb1
.word 0xf9525e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2528]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0x910323a1
.word 0xf90123a0
.word 0x91004000
.word 0xf94067a1
.word 0xf9000001
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf952aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000abc
.word 0xf9402bb1
.word 0xf952c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1240]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf952f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000f80
.word 0xf9402bb1
.word 0xf9530e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910563a0
.word 0xf90133a0
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf9535631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf953a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a0
.word 0xf94137a1
bl _p_162
.word 0xf9402bb1
.word 0xf953be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf953ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf953f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf9010ba0
.word 0xf9410ba0
.word 0xf90127a0
.word 0xf9410ba0
.word 0xf9012fa0
.word 0xd2800000
.word 0x910563a0
.word 0x910303a0
.word 0xf940afa0
.word 0xf90063a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2528]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa3
.word 0x910303a0
.word 0x91004040
.word 0xf94063a1
.word 0xf9000001
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf954c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf954e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000a2e
.word 0xf9402bb1
.word 0xf954fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1256]
.word 0xeb00035f
.word 0x54001541
.word 0xf9402bb1
.word 0xf9552231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_97
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9554e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000980
.word 0xf9402bb1
.word 0xf9556631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf955a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf955f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a1
.word 0xd2800020
.word 0x910483a0
.word 0xf90093bf
.word 0x910483a0
.word 0xd2800022
bl _p_163
.word 0x910483a0
.word 0x9102e3a0
.word 0xf94093a0
.word 0xf9005fa0
.word 0xf9402bb1
.word 0xf9562e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2536]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0x9102e3a1
.word 0xf90123a0
.word 0x91004000
.word 0xf9405fa1
.word 0xf9000001
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9567a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x140009c8
.word 0xf9402bb1
.word 0xf9569231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf956d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2464]
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9571e31
.word 0xb4000051
.word 0xd63f0220
bl _p_157
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9573631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a0
.word 0xf9412ba1
.word 0x910463a2
.word 0xf900f7a2
bl _p_164
.word 0xf940f7be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf9576231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2536]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0x910463a1
.word 0xf90123a0
.word 0x91004000
.word 0xf9408fa1
.word 0xf9000001
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf957ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400097b
.word 0xf9402bb1
.word 0xf957c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1280]
.word 0xeb00035f
.word 0x54000b01
.word 0xf9402bb1
.word 0xf957ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9582e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9587a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2544]
.word 0x9102c3a2
.word 0xf9400000
.word 0xf9005ba0
.word 0x910423a0
.word 0xd2800000
.word 0xf90087a0
.word 0xf9008ba0
.word 0x910423a0
.word 0x9102c3a2
.word 0xf9405ba2
bl _p_165
.word 0x910423a0
.word 0x910283a0
.word 0xf94087a0
.word 0xf90053a0
.word 0xf9408ba0
.word 0xf90057a0
.word 0xf9402bb1
.word 0xf958de31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2552]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0x910283a1
.word 0xf90123a0
.word 0x91004000
.word 0xf94053a1
.word 0xf9000001
.word 0xf94057a1
.word 0xf9000401
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9593231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400091a
.word 0xf9402bb1
.word 0xf9594a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1264]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9597e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34001820
.word 0xf9402bb1
.word 0xf9599631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_97
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf959c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000700
.word 0xf9402bb1
.word 0xf959da31
.word 0xb4000051
.word 0xd63f0220
.word 0x910543a0
.word 0xf90123a0
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf95a2231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf95a6e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0xf94127a1
.word 0xd2800022
.word 0xd2800022
bl _p_163
.word 0xf9402bb1
.word 0xf95a9231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95aa231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400003d
.word 0xf9402bb1
.word 0xf95ab631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf95af631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2464]
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf95b4231
.word 0xb4000051
.word 0xd63f0220
bl _p_157
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf95b5a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0xf94127a1
.word 0x910403a2
.word 0xf900f7a2
bl _p_164
.word 0xf940f7be
.word 0xf90003c0
.word 0xf9402bb1
.word 0xf95b8631
.word 0xb4000051
.word 0xd63f0220
.word 0x910403a0
.word 0x910543a0
.word 0xf94083a0
.word 0xf900aba0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95bb631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf90107a0
.word 0xf94107a0
.word 0xf90127a0
.word 0xf94107a0
.word 0xf9012ba0
.word 0xd2800000
.word 0x910543a0
.word 0x910263a0
.word 0xf940aba0
.word 0xf9004fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2536]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xaa0003e2
.word 0xf9412ba3
.word 0x910263a0
.word 0x91004040
.word 0xf9404fa1
.word 0xf9000001
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a1
.word 0xaa1a03e0
bl _p_166
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf95c5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95c7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000847
.word 0xf9402bb1
.word 0xf95c9631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_56
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf95cb631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9411030
.word 0xd63f0200
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf95ce231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000780
.word 0xf9402bb1
.word 0xf95cfa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf95d3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf95d8a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xf94127a1
.word 0xb9001001
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95dce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x140007f3
.word 0xf9402bb1
.word 0xf95de631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1032]
.word 0xeb00035f
.word 0x54000761
.word 0xf9402bb1
.word 0xf95e0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf95e4e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf95e9a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2560]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xf94127a1
.word 0xf9000801
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf95ede31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x140007af
.word 0xf9402bb1
.word 0xf95ef631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1096]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf95f2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000e20
.word 0xf9402bb1
.word 0xf95f4231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf95f8231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf95fce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a0
.word 0xf900bfa0
.word 0xf9402bb1
.word 0xf95fe631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9600e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf90103a0
.word 0xf94103a0
.word 0xf90127a0
.word 0xf94103a0
.word 0xf90133a0
.word 0xd2800000
.word 0xf940bfa0
.word 0xf9012fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2560]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa0
.word 0xf94133a3
.word 0xf9000840
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf960ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf960ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400072c
.word 0xf9402bb1
.word 0xf9610231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1024]
.word 0xeb00035f
.word 0x540007c1
.word 0xf9402bb1
.word 0xf9612a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf9616a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf961b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba0
.word 0xaa0003e0
.word 0xf90127a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2568]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xf94127a1
.word 0xb9001001
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9620631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x140006e5
.word 0xf9402bb1
.word 0xf9621e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1096]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9625231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000e40
.word 0xf9402bb1
.word 0xf9626a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf962aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2520]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf962f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a0
.word 0xaa0003e0
.word 0xb90183a0
.word 0xf9402bb1
.word 0xf9631231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9633a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf900ffa0
.word 0xf940ffa0
.word 0xf90127a0
.word 0xf940ffa0
.word 0xf90133a0
.word 0xd2800000
.word 0xb94183a0
.word 0xf9012fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2568]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa0
.word 0xf94133a3
.word 0xb9001040
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf963f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9641631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000661
.word 0xf9402bb1
.word 0xf9642e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1136]
.word 0xeb00035f
.word 0x54000941
.word 0xf9402bb1
.word 0xf9645631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9649631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2496]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xfd013fa0
.word 0xf9402bb1
.word 0xf964e231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd413fa0
.word 0x9103c3a0
.word 0xf900f7a0
bl _p_167
.word 0xf940f7be
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9402bb1
.word 0xf9650e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2576]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0x9103c3a1
.word 0xf90123a0
.word 0x91004000
.word 0xf9407ba1
.word 0xf9000001
.word 0xf9407fa1
.word 0xf9000401
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9656231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400060e
.word 0xf9402bb1
.word 0xf9657a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1160]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf965ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34001100
.word 0xf9402bb1
.word 0xf965c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf9660631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2496]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xfd0143a0
.word 0xf9402bb1
.word 0xf9665231
.word 0xb4000051
.word 0xd63f0220
.word 0xfd4143a0
.word 0x910383a0
.word 0xf900f7a0
bl _p_167
.word 0xf940f7be
.word 0xf90003c0
.word 0xf90007c1
.word 0xf9402bb1
.word 0xf9667e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910383a0
.word 0x910503a0
.word 0xf94073a0
.word 0xf900a3a0
.word 0xf94077a0
.word 0xf900a7a0
.word 0xf9402bb1
.word 0xf966a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf966ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf900fba0
.word 0xf940fba0
.word 0xf90127a0
.word 0xf940fba0
.word 0xf9012fa0
.word 0xd2800000
.word 0x910503a0
.word 0x910223a0
.word 0xf940a3a0
.word 0xf90047a0
.word 0xf940a7a0
.word 0xf9004ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2576]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa3
.word 0x910223a0
.word 0x91004040
.word 0xf94047a1
.word 0xf9000001
.word 0xf9404ba1
.word 0xf9000401
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf967aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf967ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000574
.word 0xf9402bb1
.word 0xf967e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #984]
.word 0xeb00035f
.word 0x540007e1
.word 0xf9402bb1
.word 0xf9680a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf9684a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9689a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba0
.word 0xf90127a0
.word 0x53001c00

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2584]
.word 0xd2800221
.word 0xd2800221
bl _p_20
.word 0xf94127a1
.word 0x39004001
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf968ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400052c
.word 0xf9402bb1
.word 0xf9690231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1048]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9693631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000e60
.word 0xf9402bb1
.word 0xf9694e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf9698e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf969de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a0
.word 0x53001c01
.word 0x390623a0
.word 0xf9402bb1
.word 0xf969fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf96a2231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf900f3a0
.word 0xf940f3a0
.word 0xf90127a0
.word 0xf940f3a0
.word 0xf90133a0
.word 0xd2800000
.word 0x394623a0
.word 0xf9012fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2584]
.word 0xd2800221
.word 0xd2800221
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa0
.word 0xf94133a3
.word 0x39004040
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf96ade31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf96afe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x140004a7
.word 0xf9402bb1
.word 0xf96b1631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #992]
.word 0xeb00035f
.word 0x540007e1
.word 0xf9402bb1
.word 0xf96b3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf96b7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf96bce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba0
.word 0xf90127a0
.word 0x53003c00

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2592]
.word 0xd2800241
.word 0xd2800241
bl _p_20
.word 0xf94127a1
.word 0x79002001
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf96c1e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400045f
.word 0xf9402bb1
.word 0xf96c3631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1056]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf96c6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000e60
.word 0xf9402bb1
.word 0xf96c8231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf96cc231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf96d1231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a0
.word 0x53003c01
.word 0x790323a0
.word 0xf9402bb1
.word 0xf96d2e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf96d5631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf900efa0
.word 0xf940efa0
.word 0xf90127a0
.word 0xf940efa0
.word 0xf90133a0
.word 0xd2800000
.word 0x794323a0
.word 0xf9012fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2592]
.word 0xd2800241
.word 0xd2800241
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa0
.word 0xf94133a3
.word 0x79002040
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf96e1231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf96e3231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x140003da
.word 0xf9402bb1
.word 0xf96e4a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1008]
.word 0xeb00035f
.word 0x540007e1
.word 0xf9402bb1
.word 0xf96e7231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf96eb231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf96f0231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba0
.word 0xf90127a0
.word 0x93403c00

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2600]
.word 0xd2800241
.word 0xd2800241
bl _p_20
.word 0xf94127a1
.word 0x79002001
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf96f5231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000392
.word 0xf9402bb1
.word 0xf96f6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1072]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf96f9e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000e60
.word 0xf9402bb1
.word 0xf96fb631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf96ff631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf9704631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a0
.word 0x93403c01
.word 0x790333a0
.word 0xf9402bb1
.word 0xf9706231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9708a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf900eba0
.word 0xf940eba0
.word 0xf90127a0
.word 0xf940eba0
.word 0xf90133a0
.word 0xd2800000
.word 0x798333a0
.word 0xf9012fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2600]
.word 0xd2800241
.word 0xd2800241
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa0
.word 0xf94133a3
.word 0x79002040
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9714631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9716631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400030d
.word 0xf9402bb1
.word 0xf9717e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1000]
.word 0xeb00035f
.word 0x540007e1
.word 0xf9402bb1
.word 0xf971a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf971e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf9723631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba0
.word 0xf90127a0
.word 0x93401c00

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2608]
.word 0xd2800221
.word 0xd2800221
bl _p_20
.word 0xf94127a1
.word 0x39004001
.word 0xf90123a0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9728631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x140002c5
.word 0xf9402bb1
.word 0xf9729e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1064]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf972d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000e60
.word 0xf9402bb1
.word 0xf972ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf9732a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2480]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf9737a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94137a0
.word 0x93401c01
.word 0x390683a0
.word 0xf9402bb1
.word 0xf9739631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf973be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf900e7a0
.word 0xf940e7a0
.word 0xf90127a0
.word 0xf940e7a0
.word 0xf90133a0
.word 0xd2800000
.word 0x398683a0
.word 0xf9012fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2608]
.word 0xd2800221
.word 0xd2800221
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa0
.word 0xf94133a3
.word 0x39004040
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9747a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9749a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000240
.word 0xf9402bb1
.word 0xf974b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1288]
.word 0xeb00035f
.word 0x54000641
.word 0xf9402bb1
.word 0xf974da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9751a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2616]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9756631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9758631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000205
.word 0xf9402bb1
.word 0xf9759e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1296]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf975d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000ca0
.word 0xf9402bb1
.word 0xf975ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90133a0
.word 0xf9402bb1
.word 0xf9762a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2616]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf9767631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa0
.word 0xf900d7a0
.word 0xf9402bb1
.word 0xf9768e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf976b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf900e3a0
.word 0xf940e3a0
.word 0xf90127a0
.word 0xf940e3a3
.word 0xd2800000
.word 0xf940d7a2
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9774231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9776231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400018e
.word 0xf9402bb1
.word 0xf9777a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1312]
.word 0xeb00035f
.word 0x54000a01
.word 0xf9402bb1
.word 0xf977a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf977e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2464]
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf9782e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a1
.word 0x910343a0
.word 0xd2800000
.word 0xf9006ba0
.word 0xf9006fa0
.word 0x910343a0
bl _p_168
.word 0x910343a0
.word 0x9101e3a0
.word 0xf9406ba0
.word 0xf9003fa0
.word 0xf9406fa0
.word 0xf90043a0
.word 0xf9402bb1
.word 0xf9787231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2624]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0x9101e3a1
.word 0xf90123a0
.word 0x91004000
.word 0xf9403fa1
.word 0xf9000001
.word 0xf94043a1
.word 0xf9000401
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf978c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x14000135
.word 0xf9402bb1
.word 0xf978de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1320]
.word 0xaa1503e0
.word 0xf94002be
bl _p_58
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf9791231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34001000
.word 0xf9402bb1
.word 0xf9792a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9104c3a0
.word 0xf90133a0
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9013ba0
.word 0xf9402bb1
.word 0xf9797231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9413ba3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2464]
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90137a0
.word 0xf9402bb1
.word 0xf979be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a0
.word 0xf94137a1
bl _p_168
.word 0xf9402bb1
.word 0xf979da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf979ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf97a1231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf900dfa0
.word 0xf940dfa0
.word 0xf90127a0
.word 0xf940dfa0
.word 0xf9012fa0
.word 0xd2800000
.word 0x9104c3a0
.word 0x9101a3a0
.word 0xf9409ba0
.word 0xf90037a0
.word 0xf9409fa0
.word 0xf9003ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2624]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xaa0003e2
.word 0xf9412fa3
.word 0x9101a3a0
.word 0x91004040
.word 0xf94037a1
.word 0xf9000001
.word 0xf9403ba1
.word 0xf9000401
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94127a2
.word 0xf9412ba3
.word 0xaa0303e0
.word 0xaa1a03e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf97aee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf97b0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x140000a3
.word 0xf9402bb1
.word 0xf97b2631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_98
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf97b4e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0xb4000ec0
.word 0xf9402bb1
.word 0xf97b6631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_98
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf97b8e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a2
.word 0xaa1a03e0
.word 0xaa0203e0
.word 0xaa1a03e1
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1336]
.word 0x928005f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x53001c00
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf97bd631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x34000a80
.word 0xf9402bb1
.word 0xf97bee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94012c1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9012fa0
.word 0xf9402bb1
.word 0xf97c2e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa3
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1703e1
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2616]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9012ba0
.word 0xf9402bb1
.word 0xf97c7a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba0
.word 0xf900dba0
.word 0xf9402bb1
.word 0xf97c9231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9400ec1
.word 0xaa0103e0
.word 0xf940003e
bl _p_98
.word 0xf90127a0
.word 0xf9402bb1
.word 0xf97cba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94127a3
.word 0xf940dba1
.word 0xaa1a03e0
.word 0xaa0303e0
.word 0xaa1a03e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2632]
.word 0x92800af0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf97d0231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf97d2231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a0
.word 0x1400001e
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf97d4a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28060e1
.word 0xd28060e1
bl _p_32
.word 0xaa1a03e1
.word 0xaa1a03e1
bl _p_63
.word 0xf90123a0
.word 0xf9402bb1
.word 0xf97d8231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94123a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402bb1
.word 0xf97dae31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xf94023ba
.word 0x910003bf
.word 0xa9407bfd
.word 0xd2805410
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0

Lme_59:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__cctor
SQLite_Net_SQLiteCommand__cctor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2640]
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
.word 0x92800000
.word 0xf2bfffe0
.word 0xf90017bf
.word 0x9100a3a0
.word 0x92800001
.word 0xf2bfffe1
bl _p_169
.word 0xf94017a0
.word 0xf9001ba0
.word 0xf9400bb1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2040]
.word 0xf9000001
.word 0xf9400bb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_5a:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Binding_get_Name
SQLite_Net_SQLiteCommand_Binding_get_Name:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2648]
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

Lme_5b:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Binding_set_Name_string
SQLite_Net_SQLiteCommand_Binding_set_Name_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2656]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_5c:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Binding_get_Value
SQLite_Net_SQLiteCommand_Binding_get_Value:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2664]
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

Lme_5d:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Binding_set_Value_object
SQLite_Net_SQLiteCommand_Binding_set_Value_object:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2672]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_5e:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Binding_get_Index
SQLite_Net_SQLiteCommand_Binding_get_Index:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2680]
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
.word 0xb9802000
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

Lme_5f:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Binding_set_Index_int
SQLite_Net_SQLiteCommand_Binding_set_Index_int:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2688]
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
.word 0xb9002001
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

Lme_60:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_Binding__ctor
SQLite_Net_SQLiteCommand_Binding__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2696]
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

Lme_61:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF__ctor_int
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF__ctor_int:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2704]
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
.word 0xf94013b1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xb9801ba1
.word 0xb9004001
.word 0xf94013b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf90023a0
bl _p_170
.word 0x93407c00
.word 0xf90027a0
.word 0xf94013b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94027a1
.word 0xb9004401
.word 0xf94013b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_62:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_IDisposable_Dispose
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_IDisposable_Dispose:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2712]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xd280001a
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
.word 0xf9400fa0
.word 0xb9804000
.word 0xaa0003fa
.word 0xf94013b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x92800040
.word 0xf2bfffe0
.word 0x9280005e
.word 0xf2bffffe
.word 0x6b1e035f
.word 0x540001c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020
.word 0xd280003e
.word 0x6b1e035f
.word 0x54000481
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000012
.word 0xf90027be
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
bl _p_171
.word 0xf94013b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027be
.word 0xd61f03c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_63:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_MoveNext
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_MoveNext:
.loc 1 1 0
.word 0xa9b27bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2720]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0xd280001a
.word 0xd2800019
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xd2800013
.word 0xf9402fb1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xb9804000
.word 0xaa0003f9
.word 0xf9402fb1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x34000399
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020
.word 0xd280003e
.word 0x6b1e033f
.word 0x54004640
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xd280001a
.word 0xf9402fb1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400029a
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x92800001
.word 0xf2bfffe1
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900401e
.word 0xf9402fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400c00
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_89
.word 0xf9005fa0
.word 0xf9402fb1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1888]
.word 0xf9402ba2
.word 0xf9400c42
bl _p_91
.word 0xf9402fb1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9005ba0
.word 0xf9402ba0
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_107
.word 0xf90057a0
.word 0xf9402fb1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
.word 0xf9405ba1
.word 0xf9001c20
.word 0x9100e021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x92800041
.word 0xf2bfffe1
.word 0x9280005e
.word 0xf2bffffe
.word 0xb900401e
.word 0xf9402fb1
.word 0xf9426231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9004ba0
.word 0xf9402ba0
.word 0xf9400c00
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90053a0
.word 0xf9402fb1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a2
.word 0xf9402ba0
.word 0xf9401c01
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2728]
.word 0x92800cf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf942f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2736]
bl _p_16
.word 0xf9404ba1
.word 0xf9001820
.word 0x9100c021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9434e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800018
.word 0xf9402fb1
.word 0xf9436231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000052
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400c00
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9005ba0
.word 0xf9402fb1
.word 0xf943ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba3
.word 0xf9402ba0
.word 0xf9401c01
.word 0xaa1803e0
.word 0xaa0303e0
.word 0xaa1803e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2744]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90057a0
.word 0xf9402fb1
.word 0xf9441631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
.word 0xf90053a0
.word 0xaa0003f7
.word 0xf9402fb1
.word 0xf9443231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xf9402ba0
.word 0xf9401800
.word 0xf9004fa0
.word 0xaa1803e0
.word 0xf9402ba0
.word 0xf9401002
.word 0xaa0103e0
.word 0xaa0203e0
.word 0xf940005e
bl _p_172
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9447231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba2
.word 0xf9404fa3
.word 0xaa0303e0
.word 0xaa1803e1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x11000700
.word 0xaa0003f8
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf944ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9402ba0
.word 0xf9401800
.word 0xb9801800
.word 0x6b00031f
.word 0x54fff44b
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000135
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9452631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400c00
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf90053a0
.word 0xf9402fb1
.word 0xf9455231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401001
.word 0xaa0103e0
.word 0xf940003e
bl _p_173
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf9457a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xf94053a3
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf945c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf945da31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800015
.word 0xf9402fb1
.word 0xf945ee31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400009d
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9461231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401800
.word 0xaa1503e1
.word 0x93407ea1
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002e09
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400000
.word 0xb4000fa0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9466631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400c00
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9006ba0
.word 0xf9402fb1
.word 0xf946aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba3
.word 0xf9402ba0
.word 0xf9401c01
.word 0xaa1503e0
.word 0xaa0303e0
.word 0xaa1503e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1896]
.word 0x928007f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf946fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a0
.word 0xf90063a0
.word 0xaa0003f4
.word 0xf9402fb1
.word 0xf9471631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf9402ba1
.word 0xf9400c21
.word 0xf9005fa1
.word 0xf9402ba1
.word 0xf9401c21
.word 0xf90053a1
.word 0xaa1503e1
.word 0xf90057a0
.word 0xf9402ba0
.word 0xf9401800
.word 0xaa1503e1
.word 0x93407ea1
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x540024c9
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400001
.word 0xaa0103e0
.word 0xf940003e
bl _p_55
.word 0xf9005ba0
.word 0xf9402fb1
.word 0xf9478a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xf94057a3
.word 0xf9405ba4
.word 0xf9405fa5
.word 0xaa0503e0
.word 0xaa1503e2
.word 0xf94000be
bl _p_122
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf947be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf9004ba0
.word 0xaa0003f3
.word 0xf9402fb1
.word 0xf947da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba2
.word 0xf9402ba0
.word 0xf9401800
.word 0xaa1503e1
.word 0x93407ea1
.word 0xb9801803
.word 0xeb01007f
.word 0x10000011
.word 0x54001fa9
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400003
.word 0xaa1603e0
.word 0xaa0203e0
.word 0xaa0303e0
.word 0xaa1603e1
.word 0xf940007e
bl _p_174
.word 0xf9402fb1
.word 0xf9483631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9485631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0x110006a0
.word 0xaa0003f5
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9488231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xf9402ba0
.word 0xf9401800
.word 0xb9801800
.word 0x6b0002bf
.word 0x54ffeaeb
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf948ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400c02
.word 0xaa1603e0
.word 0xaa0203e0
.word 0xaa1603e1
.word 0xf9400042
.word 0xf9403050
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf948ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf948fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9004ba0
.word 0xf9003bb6
.word 0xf9402ba0
.word 0xf9400000
bl _p_175
.word 0xaa0003e2
.word 0xf9400441
.word 0xf9403ba0
bl _p_124
.word 0xf9404ba1
.word 0xf9000820
.word 0x91004021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9496631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xd2800021
.word 0xd280003e
.word 0xb900401e
.word 0xf9402fb1
.word 0xf9498631
.word 0xb4000051
.word 0xd63f0220
.word 0xd280003a
.word 0xf9402fb1
.word 0xf9499a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000077
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf949be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x92800041
.word 0xf2bfffe1
.word 0x9280005e
.word 0xf2bffffe
.word 0xb900401e
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf949f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400c00
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf94a3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa2
.word 0xf9402ba0
.word 0xf9401c01
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1664]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf94a8231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xd2800c81
.word 0xd2800c9e
.word 0x6b1e001f
.word 0x54ffd3a0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94ab631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xd2800001
.word 0xf900181f
.word 0xf9402fb1
.word 0xf94ad231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_171
.word 0xf9402fb1
.word 0xf94aea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94afa31
.word 0xb4000051
.word 0xd63f0220
.word 0xd280001a
.word 0xf9402fb1
.word 0xf94b0e31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400001a
.word 0xf90047be
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94b3631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94b4631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_176
.word 0xf9402fb1
.word 0xf94b5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94b6e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047be
.word 0xd61f03c0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94b9631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94bba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x14000001
.word 0xf9402fb1
.word 0xf94bd231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8ce7bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_64:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF__m__Finally1
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF__m__Finally1:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2752]
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
.word 0x92800001
.word 0xf2bfffe1
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900401e
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400c00
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba2
.word 0xf9400ba0
.word 0xf9401c01
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1744]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
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
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_65:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_Generic_IEnumerator_T_get_Current
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_Generic_IEnumerator_T_get_Current:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2760]
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

Lme_66:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_IEnumerator_Reset
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_IEnumerator_Reset:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2768]
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
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_0
bl _p_33
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_67:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_IEnumerator_get_Current
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_IEnumerator_get_Current:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2776]
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

Lme_68:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_Generic_IEnumerable_T_GetEnumerator
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_Generic_IEnumerable_T_GetEnumerator:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2784]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xd280001a
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
.word 0xf9400fa0
.word 0xb9804000
.word 0x92800021
.word 0xf2bfffe1
.word 0x9280003e
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x540004a1
.word 0xf94013b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xb9804400
.word 0xf90023a0
bl _p_170
.word 0x93407c00
.word 0xf90027a0
.word 0xf94013b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94027a1
.word 0x6b01001f
.word 0x54000261
.word 0xf94013b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xd2800001
.word 0xb900401f
.word 0xf94013b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xaa0003fa
.word 0xf94013b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0x1400002c
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9400fa0
.word 0xf9400000
bl _p_177
.word 0xd2800901
.word 0xd2800901
bl _p_20
.word 0xf90023a0
.word 0xd2800001
bl _p_178
.word 0xf94013b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xaa0003fa
.word 0xf94013b1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400fa0
.word 0xf9400c00
.word 0xf9000f40
.word 0x91006341
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400fa0
.word 0xf9401400
.word 0xf9001340
.word 0x91008341
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf9422231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf94013b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_69:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_IEnumerable_GetEnumerator
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_REF_System_Collections_IEnumerable_GetEnumerator:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2792]
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
bl _p_179
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_6a:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__cctor
SQLite_Net_SQLiteConnection__cctor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2800]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2808]
.word 0x39400000
.word 0x34000380
.word 0xf9400bb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2816]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xf9001ba0
bl _p_180
.word 0xf9400bb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2824]
.word 0xaa0203e0
.word 0xf940005e
bl _p_181
.word 0xf9400bb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_6b:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__ctor_SQLite_Net_Interop_ISQLitePlatform_string_bool_SQLite_Net_IBlobSerializer_System_Collections_Generic_IDictionary_2_string_SQLite_Net_TableMapping_System_Collections_Generic_IDictionary_2_System_Type_string_SQLite_Net_IContractResolver
SQLite_Net_SQLiteConnection__ctor_SQLite_Net_Interop_ISQLitePlatform_string_bool_SQLite_Net_IBlobSerializer_System_Collections_Generic_IDictionary_2_string_SQLite_Net_TableMapping_System_Collections_Generic_IDictionary_2_System_Type_string_SQLite_Net_IContractResolver:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xd2800210
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3
.word 0xf9001ba4
.word 0xf9001fa5
.word 0xf90023a6
.word 0xf90027a7

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2832]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xf9402bb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fa1
.word 0xf94013a2
.word 0xd28000c3
.word 0x3940a3a4
.word 0xf9401ba5
.word 0xf9401fa6
.word 0xf94023a7
.word 0xf94027a9
.word 0xd28000c3
.word 0xf90003e9
bl _p_182
.word 0xf9402bb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_6c:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__ctor_SQLite_Net_Interop_ISQLitePlatform_string_SQLite_Net_Interop_SQLiteOpenFlags_bool_SQLite_Net_IBlobSerializer_System_Collections_Generic_IDictionary_2_string_SQLite_Net_TableMapping_System_Collections_Generic_IDictionary_2_System_Type_string_SQLite_Net_IContractResolver
SQLite_Net_SQLiteConnection__ctor_SQLite_Net_Interop_ISQLitePlatform_string_SQLite_Net_Interop_SQLiteOpenFlags_bool_SQLite_Net_IBlobSerializer_System_Collections_Generic_IDictionary_2_string_SQLite_Net_TableMapping_System_Collections_Generic_IDictionary_2_System_Type_string_SQLite_Net_IContractResolver:
.loc 1 1 0
.word 0xa9af7bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002bbc
.word 0x910443bc
.word 0xaa0003fa
.word 0xf9002fa1
.word 0xf90033a2
.word 0xf90037a3
.word 0xf9003ba4
.word 0xf9003fa5
.word 0xf90043a6
.word 0xf90047a7

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2840]
.word 0xf9004bb0
.word 0xf9400a11
.word 0xf9004fb1
.word 0xf9005bbf
.word 0xd2800018
.word 0xd2800017
.word 0xf9404bb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2848]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9006ba0
bl _p_183
.word 0xf9404bb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf9000b40
.word 0x91004341
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9404bb1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9404bb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb5000240
.word 0xf9404bb1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2806861
.word 0xd2806861
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9404bb1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf94047a0
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e1
.word 0xaa1a03f5
.word 0xaa0003f4
.word 0xb5000236
.word 0xaa1503e0
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2856]
.word 0xd2800901
.word 0xd2800901
bl _p_20
.word 0xf9006ba0
bl _p_184
.word 0xf9404bb1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xaa0003f4
.word 0xaa1503e0
.word 0xaa1403e0
.word 0xaa1503e0
.word 0xaa1403e1
bl _p_185
.word 0xf9404bb1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9403fa1
.word 0xaa1a03e0
bl _p_186
.word 0xf9404bb1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf9424e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9402fa1
.word 0xaa1a03e0
bl _p_187
.word 0xf9404bb1
.word 0xf9426e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf9427e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400380
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xaa1303e1
.word 0xaa1a03f5
.word 0xaa0003f4
.word 0xb5000173
.word 0xaa1503e0
.word 0xaa1403e0
bl _p_188
.word 0xf9006ba0
.word 0xf9404bb1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xaa0003f4
.word 0xaa1503e0
.word 0xaa1403e0
.word 0xaa1503e0
.word 0xaa1403e1
bl _p_189
.word 0xf9404bb1
.word 0xf942ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf942fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf94043a0
.word 0xaa0003f9
.word 0xaa1903e0
.word 0xaa1903e1
.word 0xaa1a03f5
.word 0xaa0003f4
.word 0xb5000239
.word 0xaa1503e0
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2864]
.word 0xd2800901
.word 0xd2800901
bl _p_20
.word 0xf9006ba0
bl _p_190
.word 0xf9404bb1
.word 0xf9435231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xaa0003f4
.word 0xaa1503e0
.word 0xaa1403e0
.word 0xf9000eb4
.word 0x910062a0
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf9404bb1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2872]
.word 0xd2800201
.word 0xd2800201
bl _p_20
.word 0xf9006fa0
bl _p_191
.word 0xf9404bb1
.word 0xf943d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xf9001340
.word 0x91008341
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9404bb1
.word 0xf9441a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_51
.word 0x53001c00
.word 0xf9006ba0
.word 0xf9404bb1
.word 0xf9443a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0x34000340
.word 0xf9404bb1
.word 0xf9445231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2806c21
.word 0xd2806c21
bl _p_32
.word 0xf9006ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28070a1
.word 0xd28070a1
bl _p_32
.word 0xaa0003e2
.word 0xf9406ba1
.word 0xd2801140
.word 0xf2a04000
.word 0xd2801140
.word 0xf2a04000
bl _mono_create_corlib_exception_2
bl _p_33
.word 0xf9404bb1
.word 0xf944b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf94033a1
.word 0xaa1a03e0
bl _p_192
.word 0xf9404bb1
.word 0xf944d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf944e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_193
.word 0xf9007ba0
.word 0xf9404bb1
.word 0xf9450631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba0
bl _p_194
.word 0xf90077a0
.word 0xf9404bb1
.word 0xf9452231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xaa0003f8
.word 0xf9404bb1
.word 0xf9453a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_95
.word 0xf90073a0
.word 0xf9404bb1
.word 0xf9455a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9006fa0
.word 0xf9404bb1
.word 0xf9459631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa5
.word 0xaa1803e1
.word 0x9102c3a2
.word 0xb9806ba3

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2880]
.word 0xf9400004
.word 0xaa0503e0
.word 0xf94000a5

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2888]
.word 0x92800af0
.word 0xf2bffff0
.word 0xf87068b0
.word 0xd63f0200
.word 0x93407c00
.word 0xf9006ba0
.word 0xf9404bb1
.word 0xf945f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xaa0003f7
.word 0xf9404bb1
.word 0xf9460a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9405ba1
.word 0xaa1a03e0
bl _p_195
.word 0xf9404bb1
.word 0xf9462a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf9463a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x34000a00
.word 0xf9404bb1
.word 0xf9465231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28073e1
.word 0xd28073e1
bl _p_32
.word 0xf90073a0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800041
bl _p_16
.word 0xf90063a0
.word 0xf94063a0
.word 0xf9007fa0
.word 0xf94063a0
.word 0xf90087a0
.word 0xd2800000
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_193
.word 0xf90083a0
.word 0xf9404bb1
.word 0xf946c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a2
.word 0xf94087a3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407fa0
.word 0xf90067a0
.word 0xf94067a0
.word 0xf90077a0
.word 0xf94067a0
.word 0xf9007ba0
.word 0xd2800020
.word 0xaa1703e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1704]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf9407ba3
.word 0xb9001057
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94073a0
.word 0xf94077a1
bl _p_18
.word 0xf9006fa0
.word 0xf9404bb1
.word 0xf9475631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0xaa1703e0
bl _p_101
.word 0xf9006ba0
.word 0xf9404bb1
.word 0xf9477631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
bl _p_33
.word 0xf9404bb1
.word 0xf9478e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xb5000240
.word 0xf9404bb1
.word 0xf947a631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2807de1
.word 0xd2807de1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e60
.word 0xf2a04000
.word 0xd2801e60
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9404bb1
.word 0xf947ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9405ba1
.word 0xaa1a03e0
bl _p_195
.word 0xf9404bb1
.word 0xf9480a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf9481a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800020
.word 0xd280003e
.word 0x3902035e
.word 0xf9404bb1
.word 0xf9483a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x3941c3a1
.word 0xaa1a03e0
bl _p_196
.word 0xf9404bb1
.word 0xf9485a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf9486a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd293335e
.word 0xf2b3333e
.word 0xf2d3333e
.word 0xf2e7f73e
.word 0x9e6703c0
.word 0x9102a3a0
.word 0xf9005fa0
.word 0xd293335e
.word 0xf2b3333e
.word 0xf2d3333e
.word 0xf2e7f73e
.word 0x9e6703c0
bl _p_197
.word 0xf9405fbe
.word 0xf90003c0
.word 0xf9404bb1
.word 0xf948ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0x9102a3a1
.word 0xf94057a1
bl _p_198
.word 0xf9404bb1
.word 0xf948da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf948ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bb1
.word 0xf948fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0xf9402bbc
.word 0x910003bf
.word 0xa8d17bfd
.word 0xd65f03c0

Lme_6d:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_Serializer
SQLite_Net_SQLiteConnection_get_Serializer:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2896]
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
.word 0xf9401c00
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

Lme_6e:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_set_Serializer_SQLite_Net_IBlobSerializer
SQLite_Net_SQLiteConnection_set_Serializer_SQLite_Net_IBlobSerializer:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2904]
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
.word 0xf9001c20
.word 0x9100e021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_6f:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_Handle
SQLite_Net_SQLiteConnection_get_Handle:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2912]
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

Lme_70:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_set_Handle_SQLite_Net_Interop_IDbHandle
SQLite_Net_SQLiteConnection_set_Handle_SQLite_Net_Interop_IDbHandle:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2920]
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
.word 0xf9002020
.word 0x91010021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_71:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_DatabasePath
SQLite_Net_SQLiteConnection_get_DatabasePath:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2928]
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
.word 0xf9402400
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

Lme_72:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_set_DatabasePath_string
SQLite_Net_SQLiteConnection_set_DatabasePath_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2936]
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
.word 0xf9002420
.word 0x91012021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_73:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_TimeExecution
SQLite_Net_SQLiteConnection_get_TimeExecution:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2944]
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
.word 0x39422000
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

Lme_74:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_TraceListener
SQLite_Net_SQLiteConnection_get_TraceListener:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2952]
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

Lme_75:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_StoreDateTimeAsTicks
SQLite_Net_SQLiteConnection_get_StoreDateTimeAsTicks:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2960]
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
.word 0x39422400
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

Lme_76:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_set_StoreDateTimeAsTicks_bool
SQLite_Net_SQLiteConnection_set_StoreDateTimeAsTicks_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2968]
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
.word 0x39022401
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

Lme_77:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_ExtraTypeMappings
SQLite_Net_SQLiteConnection_get_ExtraTypeMappings:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2976]
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
.word 0xf9402c00
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

Lme_78:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_set_ExtraTypeMappings_System_Collections_Generic_IDictionary_2_System_Type_string
SQLite_Net_SQLiteConnection_set_ExtraTypeMappings_System_Collections_Generic_IDictionary_2_System_Type_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2984]
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
.word 0xf9002c20
.word 0x91016021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_79:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_Resolver
SQLite_Net_SQLiteConnection_get_Resolver:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #2992]
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

Lme_7a:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_set_Resolver_SQLite_Net_IContractResolver
SQLite_Net_SQLiteConnection_set_Resolver_SQLite_Net_IContractResolver:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3000]
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
.word 0xf9003020
.word 0x91018021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_7b:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_set_BusyTimeout_System_TimeSpan
SQLite_Net_SQLiteConnection_set_BusyTimeout_System_TimeSpan:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3008]
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
.word 0xaa1a03e0
.word 0x910063a0
.word 0x910103a0
.word 0xf9400fa0
.word 0xf90023a0
.word 0x910103a0
.word 0x9101c340
.word 0xf94023a1
.word 0xf9000001
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_100
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3016]
.word 0xf9400021
.word 0xeb01001f
.word 0x54000840
.word 0xf94017b1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_95
.word 0xf90037a0
.word 0xf94017b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_100
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xeb1f035f
.word 0x10000011
.word 0x54000560
.word 0x9101c340
bl _p_199
.word 0xfd0033a0
.word 0xf94017b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba1
.word 0xf9402fa3
.word 0xfd4033a0
.word 0x9e780002
.word 0x93407c42
.word 0xaa0303e0
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3024]
.word 0x928007f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf94017b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_7c:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_get_Platform
SQLite_Net_SQLiteConnection_get_Platform:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3032]
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
.word 0xf9403400
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

Lme_7d:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_set_Platform_SQLite_Net_Interop_ISQLitePlatform
SQLite_Net_SQLiteConnection_set_Platform_SQLite_Net_Interop_ISQLitePlatform:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3040]
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
.word 0xf9003420
.word 0x9101a021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_7e:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Dispose
SQLite_Net_SQLiteConnection_Dispose:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3048]
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
.word 0xd2800020
.word 0xaa1a03e0
.word 0xd2800021
.word 0xf9400342
.word 0xf9403450
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_86
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_7f:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_GetNullTerminatedUtf8_string
SQLite_Net_SQLiteConnection_GetNullTerminatedUtf8_string:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3056]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xd2800019
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
bl _p_200
.word 0xf90033a0
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a2
.word 0xaa1a03e0
.word 0xaa0203e0
.word 0xaa1a03e1
.word 0xf9400042
.word 0xf9409c50
.word 0xd63f0200
.word 0x93407c00
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0x11000401

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3064]
bl _p_16
.word 0xaa0003f9
.word 0xf94013b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
bl _p_200
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xd2800000
.word 0xaa1a03e0
.word 0xb9801340
.word 0xf90027a0
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a3
.word 0xf9402ba6
.word 0xaa1903e4
.word 0xd2800000
.word 0xaa0603e0
.word 0xaa1a03e1
.word 0xd2800002
.word 0xd2800005
.word 0xf94000c6
.word 0xf9407cd0
.word 0xd63f0200
.word 0x93407c00
.word 0xf94013b1
.word 0xf9414e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf9419a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_80:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_GetMapping_System_Type_SQLite_Net_Interop_CreateFlags
SQLite_Net_SQLiteConnection_GetMapping_System_Type_SQLite_Net_Interop_CreateFlags:
.loc 1 1 0
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf90023a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3072]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xf90033bf
.word 0x3901a3bf
.word 0xf9003bbf
.word 0xd2800017
.word 0xf94027b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9401300
.word 0xf90033a0
.word 0xf94027b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0x3901a3bf
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b6
.word 0x9101a3b5
.word 0xaa1603e0
.word 0xaa1503e1
bl _mono_monitor_enter_v4_fast
.word 0x35000080
.word 0xaa1603e0
.word 0xaa1503e1
bl _p_201
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400f00
.word 0xf90053a0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf941e430
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf94027b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xf94053a3
.word 0x9101c3a2
.word 0xaa0303e0
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3080]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x53001c00
.word 0xf9004ba0
.word 0xf94027b1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x35000320
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xb98043a2
.word 0xaa1803e0
.word 0xf9400f03
.word 0xaa1803e0
.word 0xaa1903e1
bl _p_202
.word 0xf9004ba0
.word 0xf94027b1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f4
.word 0x1400000b
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xaa1403f7
.word 0xf94027b1
.word 0xf9422e31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000020
.word 0xf90047be
.word 0xf94027b1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0x3941a3a0
.word 0x340001e0
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9427231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_203
.word 0xf94027b1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf942aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047be
.word 0xd61f03c0
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf942d231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf942f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94027b1
.word 0xf9430a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_81:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_CreateAndSetMapping_System_Type_SQLite_Net_Interop_CreateFlags_System_Collections_Generic_IDictionary_2_string_SQLite_Net_TableMapping
SQLite_Net_SQLiteConnection_CreateAndSetMapping_System_Type_SQLite_Net_Interop_CreateFlags_System_Collections_Generic_IDictionary_2_string_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xf90013b8
.word 0xf90017a0
.word 0xaa0103f8
.word 0xf9001ba2
.word 0xf9001fa3

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3088]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800016
.word 0xd2800015
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
.word 0xf94017a0
bl _p_95
.word 0xf90057a0
.word 0xf94023b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3096]
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90053a0
.word 0xf94023b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a2
.word 0xaa1803e0
.word 0xaa0203e0
.word 0xaa1803e1
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3104]
.word 0x928006f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf94023b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf9004ba0
.word 0xaa0003f6
.word 0xf94023b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa1803e1
.word 0xf90043a0
.word 0xb98033a0
.word 0xf90047a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3112]
.word 0xd2800a01
.word 0xd2800a01
bl _p_20
.word 0xf94043a2
.word 0xf94047a3
.word 0xf9003fa0
.word 0xaa1803e1
bl _p_204
.word 0xf94023b1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xaa0003f5
.word 0xf94023b1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
.word 0xf9003ba0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf941e430
.word 0xd63f0200
.word 0xf90037a0
.word 0xf94023b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xf9403ba3
.word 0xaa1503e2
.word 0xaa0303e0
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3120]
.word 0x928000f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf94023b1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xf90033a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9423631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf94023b1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xf94013b8
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_82:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_CreateTable_T_REF_SQLite_Net_Interop_CreateFlags
SQLite_Net_SQLiteConnection_CreateTable_T_REF_SQLite_Net_Interop_CreateFlags:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9001faf
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3128]
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
.word 0xf9400ba0
.word 0xf90027a0
.word 0xf9401fa0
bl _p_205
.word 0xaa0003e1
.word 0xf94027a0
.word 0xb9801ba2
bl _p_206
.word 0x93407c00
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

Lme_83:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_CreateTable_System_Type_SQLite_Net_Interop_CreateFlags
SQLite_Net_SQLiteConnection_CreateTable_System_Type_SQLite_Net_Interop_CreateFlags:
.loc 1 1 0
.word 0xd2806a10
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xf90033a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3136]
.word 0xf90037b0
.word 0xf9400a11
.word 0xf9003bb1
.word 0xf9012bbf
.word 0xf9012fbf
.word 0xf90133bf
.word 0xf90137bf
.word 0xf9013bbf
.word 0xb9027bbf
.word 0xd2800017
.word 0xf90143bf
.word 0xd2800016
.word 0xd2800013
.word 0xf90147bf
.word 0xd2800014
.word 0xd280001a
.word 0x9108c3a0
.word 0xd2800000
.word 0xf9011ba0
.word 0xf9011fa0
.word 0xf90123a0
.word 0xf90127a0
.word 0x910843a0
.word 0xd2800000
.word 0xf9010ba0
.word 0xf9010fa0
.word 0xf90113a0
.word 0xf90117a0
.word 0x910803a0
.word 0xd2800000
.word 0xf90103a0
.word 0xf90107a0
.word 0x9107a3a0
.word 0xd2800000
.word 0xf900f7a0
.word 0xf900fba0
.word 0xf900ffa0
.word 0xf9014bbf
.word 0x910723a0
.word 0xd2800000
.word 0xf900e7a0
.word 0xf900eba0
.word 0xf900efa0
.word 0xf900f3a0
.word 0xf9014fbf
.word 0xf94037b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xb98063a2
bl _p_110
.word 0xf9018fa0
.word 0xf94037b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9418fa0
.word 0xf9012ba0
.word 0xf94037b1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3144]
.word 0xf90187a0
.word 0xf9412ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf9018ba0
.word 0xf94037b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94187a0
.word 0xf9418ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3152]
bl _p_30
.word 0xf90183a0
.word 0xf94037b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94183a0
.word 0xf9012fa0
.word 0xf94037b1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_13
.word 0xf9017fa0
.word 0xf94037b1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417fa0
.word 0xf90133a0
.word 0xf94037b1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #320]
bl _p_12
.word 0x53001c00
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0x350002c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9426631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2808c21
.word 0xd2808c21
bl _p_32
.word 0xaa0003e1
.word 0xd2801700
.word 0xf2a04000
.word 0xd2801700
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a0
.word 0xf9019fa0
.word 0xf9402ba0
.word 0xf901a3a0
.word 0xeb1f001f
.word 0x10000011
.word 0x54009100

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #392]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf9419fa0
.word 0xf941a3a2
.word 0xf9001022
.word 0x91008023
.word 0xd349fc63
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #16]
.word 0x8b040063
.word 0xd280003e
.word 0x3900007e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3160]
.word 0xf9001422

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3168]
.word 0xf9002022

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3176]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #424]
bl _p_21
.word 0xf9019ba0
.word 0xf94037b1
.word 0xf9439231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9419ba0
.word 0xf90137a0
.word 0xf94037b1
.word 0xf943aa31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3184]
.word 0xf90193a0
.word 0xf94137a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #432]
bl _p_22
.word 0xf90197a0
.word 0xf94037b1
.word 0xf943e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94193a0
.word 0xf94197a1
bl _p_23
.word 0xf9018fa0
.word 0xf94037b1
.word 0xf9440231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9418fa0
.word 0xf9013ba0
.word 0xf94037b1
.word 0xf9441a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa0
.word 0xf9413ba1
bl _p_49
.word 0xf9018ba0
.word 0xf94037b1
.word 0xf9443a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9418ba0
.word 0xf9012fa0
.word 0xf94037b1
.word 0xf9445231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1216]
bl _p_49
.word 0xf90187a0
.word 0xf94037b1
.word 0xf9447a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94187a0
.word 0xf9012fa0
.word 0xf94037b1
.word 0xf9449231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9017fa0
.word 0xf9412fa0
.word 0xf90183a0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf9417fa0
.word 0xf94183a1
bl _p_207
.word 0x93407c00
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf944e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xb9027ba0
.word 0xf94037b1
.word 0xf944fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xb9827ba0
.word 0x35000200
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9452231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9412ba1
bl _p_208
.word 0xf94037b1
.word 0xf9453e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9455e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3192]
.word 0xd2800901
.word 0xd2800901
bl _p_20
.word 0xf9017ba0
bl _p_209
.word 0xf94037b1
.word 0xf9458e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xaa0003f7
.word 0xf94037b1
.word 0xf945a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94133a0
.word 0xf90143a0
.word 0xf94037b1
.word 0xf945be31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800016
.word 0xf94037b1
.word 0xf945d231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400022a
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf945f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94143a0
.word 0xaa1603e1
.word 0x93407ec1
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54007789
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f3
.word 0xf94037b1
.word 0xf9463631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0xaa1303e0
.word 0xf940027e
bl _p_210
.word 0xf9017fa0
.word 0xf94037b1
.word 0xf9465a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3200]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf9469631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xf90147a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf946be31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000199
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf946e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94147a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3208]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90183a0
.word 0xf94037b1
.word 0xf9471e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94183a0
.word 0xf9017fa0
.word 0xaa0003f4
.word 0xf94037b1
.word 0xf9473a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417fa1
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf940003e
bl _p_211
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf9476231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xaa0003f5
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa0003f8
.word 0xb50004e0
.word 0xaa1803e0
.word 0xf94037b1
.word 0xf9478e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9412ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf9017fa0
.word 0xf94037b1
.word 0xf947b231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3216]
.word 0xf90183a0
.word 0xaa1303e0
.word 0xaa1303e0
.word 0xf940027e
bl _p_29
.word 0xf90187a0
.word 0xf94037b1
.word 0xf947e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417fa0
.word 0xf94183a1
.word 0xf94187a2
bl _p_30
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf9480a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xaa0003f8
.word 0xaa1803e0
.word 0xaa1803fa
.word 0xf94037b1
.word 0xf9482a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1803e0
.word 0x9108c3a2
.word 0xaa1703e0
.word 0xaa1803e1
.word 0xf94002fe
bl _p_212
.word 0x53001c00
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf9485e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0x35001540
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9488631
.word 0xb4000051
.word 0xd63f0220
.word 0x910843a0
.word 0xd2800000
.word 0xf9010ba0
.word 0xf9010fa0
.word 0xf90113a0
.word 0xf90117a0
.word 0xf94037b1
.word 0xf948ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x910843a0
.word 0xaa1a03e1
.word 0xf9010fba
.word 0x91002000
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94037b1
.word 0xf948f631
.word 0xb4000051
.word 0xd63f0220
.word 0x910843a0
.word 0xf9018ba0
.word 0xf9412ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf90187a0
.word 0xf94037b1
.word 0xf9492231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94187a0
.word 0xf9418ba1
.word 0xf90113a0
.word 0x91004021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94037b1
.word 0xf9496a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910843a0
.word 0xaa1403e0
.word 0xaa1403e0
.word 0xf9400281
.word 0xf9403c30
.word 0xd63f0200
.word 0xf90183a0
.word 0x53001c00
.word 0xf94037b1
.word 0xf9499a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94183a0
.word 0x3908a3a0
.word 0xf94037b1
.word 0xf949b231
.word 0xb4000051
.word 0xd63f0220
.word 0x910843a0
.word 0xf9017fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3224]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9017ba0
bl _p_213
.word 0xf94037b1
.word 0xf949ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xf9417fa1
.word 0xf9010ba0
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94037b1
.word 0xf94a2e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910843a0
.word 0x910343a0
.word 0xf9410ba0
.word 0xf9006ba0
.word 0xf9410fa0
.word 0xf9006fa0
.word 0xf94113a0
.word 0xf90073a0
.word 0xf94117a0
.word 0xf90077a0
.word 0x910343a0
.word 0x9108c3a0
.word 0xf9406ba0
.word 0xf9011ba0
.word 0xf9406fa0
.word 0xf9011fa0
.word 0xf94073a0
.word 0xf90123a0
.word 0xf94077a0
.word 0xf90127a0
.word 0xf94037b1
.word 0xf94a8e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1a03e0
.word 0x9108c3a0
.word 0x9102c3a0
.word 0xf9411ba0
.word 0xf9005ba0
.word 0xf9411fa0
.word 0xf9005fa0
.word 0xf94123a0
.word 0xf90063a0
.word 0xf94127a0
.word 0xf90067a0
.word 0xaa1703e0
.word 0xaa1a03e1
.word 0x9102c3a2
.word 0x910243a2
.word 0xf9405ba3
.word 0xf9004ba3
.word 0xf9405fa3
.word 0xf9004fa3
.word 0xf94063a3
.word 0xf90053a3
.word 0xf94067a3
.word 0xf90057a3
.word 0xaa0203e3
.word 0xf94002fe
bl _p_214
.word 0xf94037b1
.word 0xf94b0a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94b2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xaa1403e0
.word 0xf9400281
.word 0xf9403c30
.word 0xd63f0200
.word 0x53001c00
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf94b5631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0x9108c3a1
.word 0x9106a3a1
.word 0xf9411ba1
.word 0xf900d7a1
.word 0xf9411fa1
.word 0xf900dba1
.word 0xf94123a1
.word 0xf900dfa1
.word 0xf94127a1
.word 0xf900e3a1
.word 0x9106a3a1
.word 0x394703a1
.word 0x6b01001f
.word 0x540002c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94bb231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28094e1
.word 0xd28094e1
bl _p_32
.word 0xaa0003e1
.word 0xd2801700
.word 0xf2a04000
.word 0xd2801700
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94c0631
.word 0xb4000051
.word 0xd63f0220
.word 0x9108c3a0
.word 0x910623a0
.word 0xf9411ba0
.word 0xf900c7a0
.word 0xf9411fa0
.word 0xf900cba0
.word 0xf94123a0
.word 0xf900cfa0
.word 0xf94127a0
.word 0xf900d3a0
.word 0x910623a0
.word 0xf940c7a0
.word 0xf90183a0
.word 0x910803a0
.word 0xd2800000
.word 0xf90103a0
.word 0xf90107a0
.word 0x910803a0
.word 0xaa1403e0
.word 0xaa1403e0
.word 0xf940029e
bl _p_215
.word 0x93407c00
.word 0xf90187a0
.word 0xf94037b1
.word 0xf94c7631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94187a0
.word 0xb9020ba0
.word 0x910803a0
.word 0xf9017fa0
.word 0xaa1303e0
.word 0xaa1303e0
.word 0xf940027e
bl _p_29
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf94caa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xf9417fa1
.word 0xf94183a3
.word 0xf90103a0
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0x910803a0
.word 0x910203a0
.word 0xf94103a0
.word 0xf90043a0
.word 0xf94107a0
.word 0xf90047a0
.word 0xaa0303e0
.word 0x910203a1
.word 0xf94043a1
.word 0xf94047a2
.word 0xf940007e
bl _p_216
.word 0xf94037b1
.word 0xf94d2231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94d4231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94147a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf94d8231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0x35ffc9e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94daa31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000028
.word 0xf9016bbe
.word 0xf94037b1
.word 0xf94dc631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94147a0
.word 0xb40002e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94dee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94147a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94037b1
.word 0xf94e2631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94e4631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9416bbe
.word 0xd61f03c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94e6e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0x110006c0
.word 0xaa0003f6
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94e9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94143a0
.word 0xb9801800
.word 0x6b0002df
.word 0x54ffb96b
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94ece31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_217
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf94ef231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba1
.word 0x9105c3a0
.word 0xaa0003e8
.word 0xaa0103e0
.word 0xf940003e
bl _p_218
.word 0xf94037b1
.word 0xf94f1a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9105c3a0
.word 0x9107a3a0
.word 0xf940bba0
.word 0xf900f7a0
.word 0xf940bfa0
.word 0xf900fba0
.word 0xf940c3a0
.word 0xf900ffa0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94f5a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000111
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94f7e31
.word 0xb4000051
.word 0xd63f0220
.word 0x9107a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3232]
bl _p_219
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf94fa631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xf9014ba0
.word 0xf94037b1
.word 0xf94fbe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9414ba1
.word 0x910543a0
.word 0xaa0003e8
.word 0xaa1703e0
.word 0xf94002fe
bl _p_220
.word 0xf94037b1
.word 0xf94fea31
.word 0xb4000051
.word 0xd63f0220
.word 0x910543a0
.word 0x910723a0
.word 0xf940aba0
.word 0xf900e7a0
.word 0xf940afa0
.word 0xf900eba0
.word 0xf940b3a0
.word 0xf900efa0
.word 0xf940b7a0
.word 0xf900f3a0
.word 0xf94037b1
.word 0xf9502231
.word 0xb4000051
.word 0xd63f0220
.word 0x910723a0
.word 0x9104c3a0
.word 0xf940e7a0
.word 0xf9009ba0
.word 0xf940eba0
.word 0xf9009fa0
.word 0xf940efa0
.word 0xf900a3a0
.word 0xf940f3a0
.word 0xf900a7a0
.word 0x9104c3a0
.word 0xf9409ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3240]
.word 0xf9400000
.word 0xf90153a0
.word 0xf94153a1
.word 0xf94153a0
.word 0xaa0203f8
.word 0xaa0103f9
.word 0xb50006e0
.word 0xaa1803e0
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3248]
.word 0xf9400000
.word 0xf9017ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x540022a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3256]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xf9417ba1
.word 0xf9001001
.word 0x91008002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3264]
.word 0xf9001401

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3272]
.word 0xf9002001

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3280]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf9015fa0
.word 0xf9415fa0
.word 0xf9415fa2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3240]
.word 0xf9000022
.word 0xaa0003f9
.word 0xaa1803e0
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3288]
.word 0xaa1803e0
.word 0xaa1903e1
bl _p_221
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf9518631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3296]
.word 0xf9400000
.word 0xf90157a0
.word 0xf94157a1
.word 0xf94157a0
.word 0xaa0203f8
.word 0xaa0103f9
.word 0xb50006e0
.word 0xaa1803e0
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3248]
.word 0xf9400000
.word 0xf9017ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x540018e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3304]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xf9417ba1
.word 0xf9001001
.word 0x91008002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3312]
.word 0xf9001401

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3320]
.word 0xf9002001

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3328]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf9015ba0
.word 0xf9415ba0
.word 0xf9415ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3296]
.word 0xf9000022
.word 0xaa0003f9
.word 0xaa1803e0
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3336]
.word 0xaa1803e0
.word 0xaa1903e1
bl _p_222
.word 0xf90187a0
.word 0xf94037b1
.word 0xf952be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94187a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #432]
bl _p_22
.word 0xf90183a0
.word 0xf94037b1
.word 0xf952e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94183a0
.word 0xf9014fa0
.word 0xf94037b1
.word 0xf952fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xb9827ba0
.word 0xf9017ba0
.word 0xf9402ba0
.word 0xf9414ba1
.word 0x910723a2
.word 0x910443a2
.word 0xf940e7a2
.word 0xf9008ba2
.word 0xf940eba2
.word 0xf9008fa2
.word 0xf940efa2
.word 0xf90093a2
.word 0xf940f3a2
.word 0xf90097a2
.word 0x910443a2
.word 0xf94093a2
.word 0xf9414fa3
.word 0x910723a4
.word 0x9103c3a4
.word 0xf940e7a4
.word 0xf9007ba4
.word 0xf940eba4
.word 0xf9007fa4
.word 0xf940efa4
.word 0xf90083a4
.word 0xf940f3a4
.word 0xf90087a4
.word 0x9103c3a4
.word 0x394423a4
bl _p_223
.word 0x93407c00
.word 0xf9017fa0
.word 0xf94037b1
.word 0xf9538e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xf9417fa1
.word 0xb010000
.word 0xb9027ba0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf953be31
.word 0xb4000051
.word 0xd63f0220
.word 0x9107a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3232]
bl _p_224
.word 0x53001c00
.word 0xf9017ba0
.word 0xf94037b1
.word 0xf953ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0x35ffdb80
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9541231
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000015
.word 0xf90173be
.word 0xf94037b1
.word 0xf9542e31
.word 0xb4000051
.word 0xd63f0220
.word 0x9107a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3232]
bl _p_225
.word 0xf94037b1
.word 0xf9545231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9546231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94173be
.word 0xd61f03c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9548a31
.word 0xb4000051
.word 0xd63f0220
.word 0xb9827ba0
.word 0xf9017ba0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf954b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9417ba0
.word 0xf94037b1
.word 0xf954c631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa9407bfd
.word 0xd2806a10
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_84:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_CreateIndex_string_string_string___bool
SQLite_Net_SQLiteConnection_CreateIndex_string_string_string___bool:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xf9001bb7
.word 0xa903ebb9
.word 0xf90027a0
.word 0xf9002ba1
.word 0xf9002fa2
.word 0xf90033a3
.word 0xf90037a4

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3344]
.word 0xf9003bb0
.word 0xf9400a11
.word 0xf9003fb1
.word 0xf90047bf
.word 0xf9403bb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3352]
.word 0xf90057a0
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800081
bl _p_16
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf90063a0
.word 0xaa1403e0
.word 0xd2800000
.word 0xf9402fa2
.word 0xaa1403e0
.word 0xd2800001
.word 0xf9400283
.word 0xf9407870
.word 0xd63f0200
.word 0xf94063a0
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xf90053a0
.word 0xaa1303e0
.word 0xf9005fa0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3360]
.word 0xf94033a1
bl _p_23
.word 0xf9005ba0
.word 0xf9403bb1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba2
.word 0xf9405fa3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94053a0
.word 0xf94057a2
.word 0xaa0003f7
.word 0xaa1703e1
.word 0xaa1703e0
.word 0xd2800040
.word 0x3941a3a0
.word 0xaa0203f6
.word 0xaa0103f5
.word 0xaa1703fa
.word 0xd2800059
.word 0x35000140
.word 0xaa1603e0
.word 0xaa1503e0
.word 0xaa1a03e0
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1816]
.word 0xf9004ba0
.word 0x14000009
.word 0xaa1603e0
.word 0xaa1503e0
.word 0xaa1a03e0
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3368]
.word 0xf9004ba0
.word 0xaa1603e0
.word 0xaa1503e0
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0xf9404ba2
.word 0xaa1a03e0
.word 0xaa1903e1
.word 0xf9400343
.word 0xf9407870
.word 0xd63f0200
.word 0xf9004fb5
.word 0xf9404fa0
.word 0xf90063a0
.word 0xf9404fa3
.word 0xd2800060
.word 0xf9402ba2
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94063a1
.word 0xaa1603e0
bl _p_18
.word 0xf9005fa0
.word 0xf9403bb1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xf90047a0
.word 0xf9403bb1
.word 0xf9422a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf90057a0
.word 0xf94047a0
.word 0xf9005ba0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf94057a0
.word 0xf9405ba1
bl _p_207
.word 0x93407c00
.word 0xf90053a0
.word 0xf9403bb1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9429a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf9403bb1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xf9401bb7
.word 0xa943ebb9
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_85:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_GetTableInfo_string
SQLite_Net_SQLiteConnection_GetTableInfo_string:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fa0
.word 0xf90013a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3376]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3384]
.word 0xf94013a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3392]
bl _p_30
.word 0xf90033a0
.word 0xf94017b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9002fa0
.word 0xaa0003f8
.word 0xf94017b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9400fa1
.word 0xf90027a1
.word 0xf9002ba0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf94027a0
.word 0xf9402ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3400]
bl _p_226
.word 0xf90023a0
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
.word 0xf94023a0
.word 0xf94017b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_86:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_MigrateTable_SQLite_Net_TableMapping
SQLite_Net_SQLiteConnection_MigrateTable_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9a97bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f9
.word 0xf9002ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3408]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xd2800013
.word 0x9102e3a0
.word 0xd2800000
.word 0xf9005fa0
.word 0xf90063a0
.word 0xf90067a0
.word 0xd280001a
.word 0x910283a0
.word 0xd2800000
.word 0xf90053a0
.word 0xf90057a0
.word 0xf9005ba0
.word 0xf9006bbf
.word 0xf9006fbf
.word 0xf9402fb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf90097a0
.word 0xf9402fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94097a1
.word 0xaa1903e0
bl _p_227
.word 0xf90093a0
.word 0xf9402fb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94093a0
.word 0xaa0003f8
.word 0xf9402fb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3416]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9008fa0
bl _p_228
.word 0xf9402fb1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa0
.word 0xaa0003f7
.word 0xf9402fb1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_13
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800015
.word 0xf9402fb1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0x140000dd
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1503e0
.word 0x93407ea0
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54003689
.word 0xd37df000
.word 0x8b0002c0
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f4
.word 0xf9402fb1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800013
.word 0xf9402fb1
.word 0xf9422231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x910223a0
.word 0xaa0003e8
.word 0xaa1803e0
.word 0xf940031e
bl _p_229
.word 0xf9402fb1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910223a0
.word 0x9102e3a0
.word 0xf94047a0
.word 0xf9005fa0
.word 0xf9404ba0
.word 0xf90063a0
.word 0xf9404fa0
.word 0xf90067a0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400004b
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x9102e3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3424]
bl _p_230
.word 0xf90097a0
.word 0xf9402fb1
.word 0xf942d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94097a0
.word 0xaa0003fa
.word 0xf9402fb1
.word 0xf942ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xaa1403e0
.word 0xf940029e
bl _p_29
.word 0xf9008fa0
.word 0xf9402fb1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e1
.word 0xaa0103e0
.word 0xf940003e
bl _p_231
.word 0xf90093a0
.word 0xf9402fb1
.word 0xf9433631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa0
.word 0xf94093a1
.word 0xd28000a2
.word 0xd28000a2
bl _p_19
.word 0x93407c00
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf9436231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xaa0003f3
.word 0xf9402fb1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0x34000173
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf943ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000021
.word 0x14000034
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf943d631
.word 0xb4000051
.word 0xd63f0220
.word 0x9102e3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3424]
bl _p_232
.word 0x53001c00
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf9440231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba0
.word 0x35fff440
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9442a31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000015
.word 0xf9007bbe
.word 0xf9402fb1
.word 0xf9444631
.word 0xb4000051
.word 0xd63f0220
.word 0x9102e3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3424]
bl _p_233
.word 0xf9402fb1
.word 0xf9446a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9447a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407bbe
.word 0xd61f03c0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf944a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0x35000273
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf944ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1403e0
.word 0xaa1703e0
.word 0xaa1403e1
.word 0xf94002fe
bl _p_234
.word 0xf9402fb1
.word 0xf944f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9451231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0x110006a0
.word 0xaa0003f5
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9453e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xb9801ac0
.word 0x6b0002bf
.word 0x54ffe30b
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9457231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x9101c3a0
.word 0xaa0003e8
.word 0xaa1703e0
.word 0xf94002fe
bl _p_235
.word 0xf9402fb1
.word 0xf9459a31
.word 0xb4000051
.word 0xd63f0220
.word 0x9101c3a0
.word 0x910283a0
.word 0xf9403ba0
.word 0xf90053a0
.word 0xf9403fa0
.word 0xf90057a0
.word 0xf94043a0
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf945da31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000071
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf945fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x910283a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3432]
bl _p_236
.word 0xf900b3a0
.word 0xf9402fb1
.word 0xf9462631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940b3a0
.word 0xf9006ba0
.word 0xf9402fb1
.word 0xf9463e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3440]
.word 0xf90093a0
.word 0xf9402ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf90097a0
.word 0xf9402fb1
.word 0xf9467231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3448]
.word 0xf9009ba0
.word 0xf9406ba0
.word 0xf900a3a0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_97
.word 0x53001c00
.word 0xf900a7a0
.word 0xf9402fb1
.word 0xf946ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_98
.word 0xf900aba0
.word 0xf9402fb1
.word 0xf946ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_237
.word 0xf900afa0
.word 0xf9402fb1
.word 0xf946ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0
.word 0xf940a7a1
.word 0xf940aba2
.word 0xf940afa3
bl _p_238
.word 0xf9009fa0
.word 0xf9402fb1
.word 0xf9471631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94093a0
.word 0xf94097a1
.word 0xf9409ba2
.word 0xf9409fa3
bl _p_52
.word 0xf9008fa0
.word 0xf9402fb1
.word 0xf9473e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa0
.word 0xf9006fa0
.word 0xf9402fb1
.word 0xf9475631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9406fa0
.word 0xf9008ba0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf9408ba1
.word 0xaa1903e0
bl _p_207
.word 0x93407c00
.word 0xf9402fb1
.word 0xf9479e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf947be31
.word 0xb4000051
.word 0xd63f0220
.word 0x910283a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3432]
bl _p_239
.word 0x53001c00
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf947ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba0
.word 0x35ffef80
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9481231
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000015
.word 0xf90083be
.word 0xf9402fb1
.word 0xf9482e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910283a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3432]
bl _p_240
.word 0xf9402fb1
.word 0xf9485231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9486231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083be
.word 0xd61f03c0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9488a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9489a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d77bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_87:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_NewCommand
SQLite_Net_SQLiteConnection_NewCommand:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3456]
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
bl _p_95
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3464]
.word 0xd2800601
.word 0xd2800601
bl _p_20
.word 0xf9401fa1
.word 0xf9001ba0
.word 0xaa1a03e2
bl _p_241
.word 0xf9400fb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_88:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_CreateCommand_string_object__
SQLite_Net_SQLiteConnection_CreateCommand_string_object__:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xf9001bb8
.word 0xaa0003f8
.word 0xf9001fa1
.word 0xf90023a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3472]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xf94027b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x39420300
.word 0x350002c0
.word 0xf94027b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd280c5c1
.word 0xd280c5c1
bl _p_32
.word 0xaa0003e1
.word 0xd2800020
bl _p_101
.word 0xf90033a0
.word 0xf94027b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
bl _p_33
.word 0xf94027b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400301
.word 0xf9403830
.word 0xd63f0200
.word 0xf90037a0
.word 0xf94027b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf90033a0
.word 0xaa0003f7
.word 0xf94027b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a2
.word 0xaa0203e0
.word 0xf9401fa1
.word 0xaa0203e0
.word 0xf940005e
bl _p_106
.word 0xf94027b1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xaa0003f6
.word 0xf94027b1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800015
.word 0xf94027b1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000026
.word 0xf94027b1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1503e0
.word 0x93407ea0
.word 0xb9801ac1
.word 0xeb00003f
.word 0x10000011
.word 0x54000809
.word 0xd37df000
.word 0x8b0002c0
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f4
.word 0xf94027b1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1403e0
.word 0xaa1703e0
.word 0xaa1403e1
.word 0xf94002fe
bl _p_242
.word 0xf94027b1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0x110006a0
.word 0xaa0003f5
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1603e0
.word 0xb9801ac0
.word 0x6b0002bf
.word 0x54fff9eb
.word 0xf94027b1
.word 0xf9426a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9428e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf94027b1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94157b4
.word 0xa9425fb6
.word 0xf9401bb8
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_89:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Execute_string_object__
SQLite_Net_SQLiteConnection_Execute_string_object__:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xf90013b8
.word 0xaa0003f8
.word 0xf90017a1
.word 0xf9001ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3480]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf9401fb1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94017a1
.word 0xf9401ba2
.word 0xaa1803e0
bl _p_243
.word 0xf9002fa0
.word 0xf9401fb1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_244
.word 0x53001c00
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xaa0103f7
.word 0x34000b20
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xf9401b00
.word 0xb50006c0
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_95
.word 0xf90033a0
.word 0xf9401fb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3488]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9002fa0
.word 0xf9401fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3496]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9001b00
.word 0x9100c301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xf9401b01
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3504]
.word 0x928003f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9401b01
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3512]
.word 0x928005f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_245
.word 0x93407c00
.word 0xf9002fa0
.word 0xf9401fb1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_244
.word 0x53001c00
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9426a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xaa0103f6
.word 0x34000d00
.word 0xaa1603e0
.word 0xaa1803e0
.word 0xf9401b01
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3520]
.word 0x928005f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9403f00
.word 0xf9003fa0
.word 0xaa1803e0
.word 0xf9401b01
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3528]
.word 0x928012f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90043a0
.word 0xf9401fb1
.word 0xf9430a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xf94043a1
.word 0x8b010000
.word 0xf9003f00
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_89
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9433a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3536]
.word 0xf9002fa0
.word 0xaa1803e0
.word 0xf9401b01
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3528]
.word 0x928012f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf9438a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2560]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xf9403ba1
.word 0xf9000801
.word 0xf90033a0
.word 0xaa1803e0
.word 0xf9403f00
.word 0x9e620000
.word 0xd280001e
.word 0xf2c8001e
.word 0xf2e811fe
.word 0x9e6703c1
.word 0x1e611800
.word 0xfd0037a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2504]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xaa0003e3
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xf94033a2
.word 0xfd4037a0
.word 0xfd000860
bl _p_246
.word 0xf9401fb1
.word 0xf9441631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9443a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9401fb1
.word 0xf9444e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xf94013b8
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0

Lme_8a:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Query_T_REF_string_object__
SQLite_Net_SQLiteConnection_Query_T_REF_string_object__:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf90023af
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3544]
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
.word 0xf9400ba0
.word 0xf9400fa1
.word 0xf94013a2
bl _p_243
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
bl _p_247
.word 0xaa0003ef
.word 0xf9402fa0
.word 0xf940001e
bl _p_248
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_8b:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Table_T_REF
SQLite_Net_SQLiteConnection_Table_T_REF:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9001baf
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3552]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400fb1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_95
.word 0xf90027a0
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9401ba0
bl _p_249
.word 0xd2800f01
.word 0xd2800f01
bl _p_20
.word 0xf94027a1
.word 0xf90023a0
.word 0xaa1a03e2
bl _p_250
.word 0xf9400fb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400fb1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_8c:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_BeginTransaction
SQLite_Net_SQLiteConnection_BeginTransaction:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3560]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9001bbf
.word 0xb9003bbf
.word 0xf9400fb1
.word 0xf9403a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001c00
.word 0x91021000
.word 0xd2800021
.word 0xd2800002
.word 0x885f7c10
.word 0x6b02021f
.word 0x54000061
.word 0x8811fc01
.word 0x35ffff91
.word 0xd50330bf
.word 0xaa1003e0
.word 0xf90043a0
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35001560
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf90043a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3568]
.word 0xf90047a0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf94043a0
.word 0xf94047a1
bl _p_207
.word 0x93407c00
.word 0xf9400fb1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400009e
.word 0xf90023a0
.word 0xf94023a0
.word 0xf90033a0
.word 0xf94033a0
.word 0xf90037a0
.word 0xf94033a0
.word 0xeb1f001f
.word 0x540001a0
.word 0xf94033a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3576]
.word 0xeb01001f
.word 0x54000060
.word 0xf90037bf
.word 0x14000001
.word 0xf94037a0
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xb4000920
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf941c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_43
.word 0x93407c00
.word 0xf90043a0
.word 0xf9400fb1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xb9003ba0
.word 0xf9400fb1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xb9803ba0
.word 0x51001400
.word 0xf9003ba0
.word 0xf9403ba0
.word 0xd280013e
.word 0x6b1e001f
.word 0x54000142
.word 0xf9403ba0
.word 0xd37df000
.word 0x2a0003e1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3584]
.word 0x8b010000
.word 0xf9400000
.word 0xd61f0000
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9426631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000031
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xd2800001
.word 0xd2800021
.word 0xd2800001
.word 0xd2800022
bl _p_251
.word 0xf9400fb1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400001a
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x540007a0
.word 0x91021000
.word 0x92800001
.word 0xf2bfffe1
.word 0x885f7c10
.word 0x8b010210
.word 0x8811fc10
.word 0x35ffffb1
.word 0xd50330bf
.word 0xaa1003e0
.word 0xf9400fb1
.word 0xf9432a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9434a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
bl _p_252
.word 0x14000001
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9437631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd280df01
.word 0xd280df01
bl _p_32
.word 0xaa0003e1
.word 0xd2801c80
.word 0xf2a04000
.word 0xd2801c80
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf943ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_8d:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_SaveTransactionPoint
SQLite_Net_SQLiteConnection_SaveTransactionPoint:
.loc 1 1 0
.word 0xa9b07bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xf90023a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3592]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xd280001a
.word 0xd2800019
.word 0xf90033bf
.word 0xb9006bbf
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
.word 0xf94023a0
.word 0xeb1f001f
.word 0x10000011
.word 0x540028e0
.word 0x91021000
.word 0xd2800021
.word 0x885f7c10
.word 0x8b010210
.word 0x8811fc10
.word 0x35ffffb1
.word 0xd50330bf
.word 0xaa1003e0
.word 0xf9007fa0
.word 0xf94027b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa0
.word 0x51000400
.word 0xaa0003fa
.word 0xf94027b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800081
bl _p_16
.word 0xaa0003f8
.word 0xaa1803e0
.word 0xf9007ba0
.word 0xaa1803e0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3600]
.word 0xaa1803e0
.word 0xd2800001
.word 0xf9400303
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407ba0
.word 0xaa0003f7
.word 0xaa1703e0
.word 0xf9006fa0
.word 0xaa1703e0
.word 0xf90077a0
.word 0xd2800020
.word 0xf94023a0
.word 0xf9400802
.word 0xd28fffe0
.word 0xaa0203e0
.word 0xd28fffe1
.word 0xf9400042
.word 0xf9403450
.word 0xd63f0200
.word 0x93407c00
.word 0xf90073a0
.word 0xf94027b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf94073a0
.word 0xf94077a3
.word 0xb9001040
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406fa0
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xf9006ba0
.word 0xaa1603e0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3608]
.word 0xaa1603e0
.word 0xd2800041
.word 0xf94002c3
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406ba0
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xf9005fa0
.word 0xaa1503e0
.word 0xf90067a0
.word 0xd2800060
.word 0xaa1a03e0
.word 0xf90063a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf94063a0
.word 0xf94067a3
.word 0xb9001040
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9405fa0
bl _p_54
.word 0xf9005ba0
.word 0xf94027b1
.word 0xf9425e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9428631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9005ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3616]
.word 0xaa1903e1
.word 0xaa1903e1
bl _p_49
.word 0xf9005fa0
.word 0xf94027b1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf9405ba0
.word 0xf9405fa1
bl _p_207
.word 0x93407c00
.word 0xf94027b1
.word 0xf942f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9430631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000088
.word 0xf9003ba0
.word 0xf9403ba0
.word 0xf9004ba0
.word 0xf9404ba0
.word 0xf9004fa0
.word 0xf9404ba0
.word 0xeb1f001f
.word 0x540001a0
.word 0xf9404ba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3576]
.word 0xeb01001f
.word 0x54000060
.word 0xf9004fbf
.word 0x14000001
.word 0xf9404fa0
.word 0xf90033a0
.word 0xf94027b1
.word 0xf9437231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xb4000920
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9439a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_43
.word 0x93407c00
.word 0xf9005ba0
.word 0xf94027b1
.word 0xf943c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xb9006ba0
.word 0xf94027b1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0xb9806ba0
.word 0x51001400
.word 0xf90053a0
.word 0xf94053a0
.word 0xd280013e
.word 0x6b1e001f
.word 0x54000142
.word 0xf94053a0
.word 0xd37df000
.word 0x2a0003e1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3624]
.word 0x8b010000
.word 0xf9400000
.word 0xd61f0000
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9443a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000031
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9445e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xd2800001
.word 0xd2800021
.word 0xd2800001
.word 0xd2800022
bl _p_251
.word 0xf94027b1
.word 0xf9448631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9449631
.word 0xb4000051
.word 0xd63f0220
.word 0x1400001a
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf944ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000680
.word 0x91021000
.word 0x92800001
.word 0xf2bfffe1
.word 0x885f7c10
.word 0x8b010210
.word 0x8811fc10
.word 0x35ffffb1
.word 0xd50330bf
.word 0xaa1003e0
.word 0xf94027b1
.word 0xf944fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9451e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
bl _p_252
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9454631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9456a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94027b1
.word 0xf9457e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8d07bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_8e:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Rollback
SQLite_Net_SQLiteConnection_Rollback:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3632]
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
.word 0xd2800001
.word 0xd2800001
.word 0xd2800001
.word 0xd2800002
bl _p_251
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
.word 0x910003bf
.word 0xa8c37bfd
.word 0xd65f03c0

Lme_8f:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_RollbackTo_string_bool
SQLite_Net_SQLiteConnection_RollbackTo_string_bool:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003f9
.word 0xaa0103fa
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3640]
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
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_51
.word 0x53001c00
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0x340007c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xeb1f033f
.word 0x10000011
.word 0x54001020
.word 0x91021320
.word 0xd2800001
.word 0x885f7c10
.word 0x8811fc01
.word 0x35ffffd1
.word 0xd50330bf
.word 0xaa1003e0
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xd2800001
.word 0x6b1f001f
.word 0x540006cd
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3648]
.word 0xf9003ba0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf9403ba1
.word 0xaa1903e0
bl _p_207
.word 0x93407c00
.word 0xf94017b1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000015
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3656]
.word 0xaa1903e0
.word 0xaa1a03e1
bl _p_253
.word 0xf94017b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000022
.word 0xf90023a0
.word 0xf94023a0
.word 0xf94017b1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0x394083a0
.word 0x35000160
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
bl _p_252
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
bl _p_254
.word 0xf90033a0
.word 0xf94033a0
.word 0xb4000060
.word 0xf94033a0
bl _p_33
.word 0x14000001
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9429631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000001
.word 0xf94017b1
.word 0xf942aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_90:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Release_string
SQLite_Net_SQLiteConnection_Release_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3664]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3672]
bl _p_253
.word 0xf94013b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
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

Lme_91:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_DoSavePointExecute_string_string
SQLite_Net_SQLiteConnection_DoSavePointExecute_string_string:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xf90013b9
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf90017a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3680]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xd2800017
.word 0xb9004bbf
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
.word 0xd2800880
.word 0xaa1903e0
.word 0xd2800881
.word 0xf940033e
bl _p_255
.word 0x93407c00
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9002ba0
.word 0xaa0003f7
.word 0xf9401bb1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa0003e1
.word 0xd2800041
.word 0xd280005e
.word 0x6b1e001f
.word 0x540011ab
.word 0xf9401bb1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xb9801320
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa1703e1
.word 0x110006e1
.word 0x6b01001f
.word 0x54000fad
.word 0xf9401bb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1703e0
.word 0x110006e1
.word 0xaa1903e0
.word 0xf940033e
bl _p_256
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0x910123a1
bl _p_257
.word 0x53001c00
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x34000c60
.word 0xf9401bb1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xb9804ba1
.word 0x6b01001f
.word 0x54000b6c
.word 0xf9401bb1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xb9804ba0
.word 0xaa1803e1
.word 0xb9808701
.word 0x6b01001f
.word 0x54000a4a
.word 0xf9401bb1
.word 0xf941ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_95
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3688]
.word 0x92800af0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa3
.word 0xaa1803e0
.word 0xeb1f031f
.word 0x10000011
.word 0x54000b00
.word 0x91021301
.word 0xb9804ba2
.word 0xaa0303e0
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3696]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9428631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94017a0
.word 0xaa1903e1
.word 0xaa1903e1
bl _p_49
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf9402ba1
.word 0xaa1803e0
bl _p_207
.word 0x93407c00
.word 0xf9401bb1
.word 0xf942ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf942fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400001e
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9431e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd280f941
.word 0xd280f941
bl _p_32
.word 0xf9002ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2810e61
.word 0xd2810e61
bl _p_32
.word 0xaa0003e2
.word 0xf9402ba1
.word 0xd2801140
.word 0xf2a04000
.word 0xd2801140
.word 0xf2a04000
bl _mono_create_corlib_exception_2
bl _p_33
.word 0xf9401bb1
.word 0xf9438231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xf94013b9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_92:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Commit
SQLite_Net_SQLiteConnection_Commit:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3704]
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
.word 0xeb1f035f
.word 0x10000011
.word 0x540006e0
.word 0x91021340
.word 0xd2800001
.word 0x885f7c10
.word 0x8811fc01
.word 0x35ffffd1
.word 0xd50330bf
.word 0xaa1003e0
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0x34000320
.word 0xf9400fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3712]
.word 0xf9001ba0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800001
bl _p_16
.word 0xaa0003e2
.word 0xf9401ba1
.word 0xaa1a03e0
bl _p_207
.word 0x93407c00
.word 0xf9400fb1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_93:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_RunInTransaction_System_Action
SQLite_Net_SQLiteConnection_RunInTransaction_System_Action:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xf9000fa0
.word 0xf90013a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3720]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800019
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
.word 0xf9400fa0
bl _p_258
.word 0xf90037a0
.word 0xf94017b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a1
.word 0xaa0103e0
.word 0xf90033a1
.word 0xf9400c30
.word 0xd63f0200
.word 0xf94033a0
.word 0xf94017b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xaa1903e1
bl _p_259
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000013
.word 0xf90023a0
.word 0xf94023a0
.word 0xf94017b1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
bl _p_260
.word 0xf94017b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
bl _p_252
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000001
.word 0xf94017b1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_94:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_InsertAll_System_Collections_IEnumerable_bool
SQLite_Net_SQLiteConnection_InsertAll_System_Collections_IEnumerable_bool:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xaa0003f8
.word 0xf9001ba1
.word 0xf9001fa2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3728]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800017
.word 0xf9002fbf
.word 0xd2800016
.word 0xf90033bf
.word 0xf94023b1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3736]
.word 0xd2800501
.word 0xd2800501
bl _p_20
.word 0xf90053a0
bl _p_261
.word 0xf94023b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xaa0003f7
.word 0xf94023b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1803e0
.word 0xf9000ef8
.word 0x910062e0
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94023b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9401ba0
.word 0xf9000ae0
.word 0x910042e1
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94023b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xd2800000
.word 0xb90022ff
.word 0xf94023b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0x3940e3a0
.word 0x340007a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xeb1f02ff
.word 0x10000011
.word 0x540022c0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3744]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf9001037
.word 0x91008020
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020000
.word 0xd280003e
.word 0x3900001e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3752]
.word 0xf9001420

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3760]
.word 0xf9002020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3768]
.word 0xf9401402
.word 0xf9000c22
.word 0xf9401000
.word 0xf9000820
.word 0xd2800000
.word 0x3901803f
.word 0xaa1803e0
bl _p_262
.word 0xf94023b1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0x140000ca
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9427e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9400ae1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3776]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90053a0
.word 0xf94023b1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf9002fa0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000034
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9430a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3784]
.word 0x928003f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9005fa0
.word 0xf94023b1
.word 0xf9434631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xf9005ba0
.word 0xaa0003f6
.word 0xf94023b1
.word 0xf9436231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba1
.word 0xaa1703e0
.word 0xaa1703f5
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xb98022e0
.word 0xf90053a0
.word 0xaa1803e0
.word 0xaa0103e0
.word 0xaa1803e0
bl _p_263
.word 0x93407c00
.word 0xf90057a0
.word 0xf94023b1
.word 0xf943a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf94057a1
.word 0xb010000
.word 0xb90022e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf943d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf90053a0
.word 0xf94023b1
.word 0xf9441631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0x35fff680
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9443e31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000050
.word 0xf9004bbe
.word 0xf94023b1
.word 0xf9445a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf90037a0
.word 0xf94037a0
.word 0xf9003ba0
.word 0xf94037a0
.word 0xeb1f001f
.word 0x54000380
.word 0xf94037a0
.word 0xf9400000
.word 0xf9003fa0
.word 0xf9403fa0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3792]
.word 0xeb01001f
.word 0x540001e3
.word 0xf9403fa0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3792]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000080
.word 0x14000001
.word 0xf9003bbf
.word 0x14000001
.word 0xf9403ba0
.word 0xf90033a0
.word 0xf94023b1
.word 0xf944fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xb40002e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9452231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94023b1
.word 0xf9455a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9457a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404bbe
.word 0xd61f03c0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf945a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xb98022e0
.word 0xf90053a0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf945ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf94023b1
.word 0xf945e231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_95:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Insert_object
SQLite_Net_SQLiteConnection_Insert_object:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3800]
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
.word 0xb500021a
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x14000028
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf90027a0
.word 0xaa1a03e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1816]
.word 0xf9002ba0
.word 0xaa1a03e0
.word 0xf9400340
.word 0xf9400c00
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9402ba2
.word 0xf9402fa3
.word 0xaa1a03e1
bl _p_264
.word 0x93407c00
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_96:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Insert_object_string_System_Type
SQLite_Net_SQLiteConnection_Insert_object_string_System_Type:
.loc 1 1 0
.word 0xa9af7bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xf90033a2
.word 0xaa0303fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3808]
.word 0xf90037b0
.word 0xf9400a11
.word 0xf9003bb1
.word 0xf90053bf
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xd2800013
.word 0xf90057bf
.word 0xd2800019
.word 0xf94037b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb4000160
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb500029a
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x1400026a
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa1a03e1
.word 0xd2800001
.word 0xaa1a03e1
.word 0xd2800002
bl _p_110
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf90053a0
.word 0xf94037b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_265
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xb40015e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_265
.word 0xf90077a0
.word 0xf94037b1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_266
.word 0x53001c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0x34001240
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf94053a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_265
.word 0xf9007fa0
.word 0xf94037b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_267
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf9427e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba1
.word 0xaa1a03e0
bl _p_268
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9429e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf90073a0
.word 0xaa0003f4
.word 0xf94037b1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xaa0003e1
.word 0xb4000ca0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xf9402fa1
.word 0xd2800000
.word 0xaa1403e0
.word 0xd2800002
.word 0xf9400283
.word 0xf9406c70
.word 0xd63f0200
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9431a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3816]
.word 0x910203a1
.word 0xf9400001
.word 0xf90043a1
.word 0xf9400400
.word 0xf90047a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2624]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xaa0003e1
.word 0xf94077a2
.word 0x910203a0
.word 0x91004020
.word 0xf94043a3
.word 0xf9000003
.word 0xf94047a3
.word 0xf9000403
.word 0xaa0203e0
.word 0xf9400042
.word 0xf9402c50
.word 0xd63f0200
.word 0x53001c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9439a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0x340005c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf943c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xf9402fa0
.word 0xf90073a0
.word 0x910243a0
.word 0xf9005fa0
bl _p_269
.word 0xf9405fbe
.word 0xf90003c0
.word 0xf90007c1
.word 0xf94037b1
.word 0xf943f631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2624]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xaa0003e2
.word 0xf94073a1
.word 0x910243a0
.word 0x91004040
.word 0xf9404ba3
.word 0xf9000003
.word 0xf9404fa3
.word 0xf9000403
.word 0xd2800000
.word 0xaa1403e0
.word 0xd2800003
.word 0xf9400284
.word 0xf9406490
.word 0xd63f0200
.word 0xf94037b1
.word 0xf9445631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9447631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #352]
.word 0xd28000a2
.word 0xd28000a2
bl _p_19
.word 0x93407c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf944aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0x350002a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf944de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_11
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf9005ba0
.word 0x14000014
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9452e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_13
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9455231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf9005ba0
.word 0xf9405ba0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf9457231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb9801b01

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
bl _p_16
.word 0xaa0003f7
.word 0xf94037b1
.word 0xf9459e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800013
.word 0xf94037b1
.word 0xf945b231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400002c
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf945d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1303e0
.word 0xaa1803e0
.word 0xaa1303e0
.word 0x93407e60
.word 0xb9801b01
.word 0xeb00003f
.word 0x10000011
.word 0x54002749
.word 0xd37df000
.word 0x8b000300
.word 0x91008000
.word 0xf9400002
.word 0xf9402fa1
.word 0xaa0203e0
.word 0xf940005e
bl _p_45
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9462e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a2
.word 0xaa1703e0
.word 0xaa1303e1
.word 0xf94002e3
.word 0xf9407870
.word 0xd63f0200
.word 0xf94037b1
.word 0xf9465631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0x11000660
.word 0xaa0003f3
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9468231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0xaa1703e0
.word 0xb9801ae0
.word 0x6b00027f
.word 0x54fff92b
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf946b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94053a1
.word 0xf94033a2
bl _p_270
.word 0xf90073a0
.word 0xf94037b1
.word 0xf946da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xaa0003f6
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9470231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1703e0
.word 0xaa1603e0
.word 0xaa1703e1
.word 0xf94002de
bl _p_271
.word 0x93407c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9473231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xaa0003f5
.word 0xf94037b1
.word 0xf9474a31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400006d
.word 0xf90063a0
.word 0xf94063a0
.word 0xf90057a0
.word 0xf94037b1
.word 0xf9476a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_95
.word 0xf9007fa0
.word 0xf94037b1
.word 0xf9478631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf947c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_100
.word 0xf90077a0
.word 0xf94037b1
.word 0xf947de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a1
.word 0xf9407ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1696]
.word 0x928006f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9482231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xd280a261
.word 0xd280a27e
.word 0x6b1e001f
.word 0x54000521
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9485631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_43
.word 0x93407c00
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9487e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405030
.word 0xd63f0200
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf948a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf9407ba1
.word 0xf94053a2
.word 0xf9402fa3
bl _p_272
.word 0xf90073a0
.word 0xf94037b1
.word 0xf948ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
bl _p_33
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf948f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
bl _p_252
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9491e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_273
.word 0x53001c00
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9494631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0x34000900
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9496e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_95
.word 0xf90083a0
.word 0xf94037b1
.word 0xf9498a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9007fa0
.word 0xf94037b1
.word 0xf949c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_100
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf949e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba1
.word 0xf9407fa2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3824]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf90077a0
.word 0xf94037b1
.word 0xf94a2231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf90073a0
.word 0xaa0003f9
.word 0xf94037b1
.word 0xf94a3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a2
.word 0xf94053a3
.word 0xf9402fa1
.word 0xaa0203e0
.word 0xaa0303e0
.word 0xf940007e
bl _p_274
.word 0xf94037b1
.word 0xf94a6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94a8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94aae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xf94037b1
.word 0xf94ac231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d17bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_97:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_GetInsertCommand_SQLite_Net_TableMapping_string
SQLite_Net_SQLiteConnection_GetInsertCommand_SQLite_Net_TableMapping_string:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3832]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf90023bf
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
.word 0xaa1803e0
.word 0xf9401700
.word 0xb50003e0
.word 0xf94017b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3840]
.word 0xd2800901
.word 0xd2800901
bl _p_20
.word 0xf9002ba0
bl _p_275
.word 0xf94017b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9001700
.word 0x9100a301
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9401703
.word 0xaa1903e0
.word 0x910103a2
.word 0xaa0303e0
.word 0xaa1903e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3848]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x53001c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x35000560
.word 0xf94017b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3856]
.word 0xd2800501
.word 0xd2800501
bl _p_20
.word 0xf9002ba0
.word 0xaa1903e1
bl _p_276
.word 0xf94017b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90023a0
.word 0xf94017b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9401703
.word 0xaa1903e0
.word 0xf94023a2
.word 0xaa0303e0
.word 0xaa1903e1
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3864]
.word 0x928000f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf94017b1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a3
.word 0xaa1803e0
.word 0xf94013a2
.word 0xaa0303e0
.word 0xaa1803e1
.word 0xf940007e
bl _p_277
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9425631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf9428a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_98:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Update_object
SQLite_Net_SQLiteConnection_Update_object:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fba
.word 0xf90013a0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3872]
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
.word 0xaa1a03e0
.word 0xb500021a
.word 0xf94017b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x14000025
.word 0xf94017b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf90027a0
.word 0xaa1a03e0
.word 0xaa1a03f8
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400340
.word 0xf9400c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9402ba2
.word 0xaa1a03e1
bl _p_278
.word 0x93407c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94017b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0xf9400fba
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_99:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Update_object_System_Type
SQLite_Net_SQLiteConnection_Update_object_System_Type:
.loc 1 1 0
.word 0xa9a87bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xf90023ba
.word 0xf90027a0
.word 0xf9002ba1
.word 0xaa0203fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #3880]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0xf9003bbf
.word 0xb9007bbf
.word 0xf90043bf
.word 0xd2800017
.word 0xf90047bf
.word 0xf9004bbf
.word 0xf9004fbf
.word 0xf9402fb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3888]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90083a0
bl _p_279
.word 0xf9402fb1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xf9003ba0
.word 0xf9402fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xf9402ba0
.word 0xf9000c20
.word 0x91006021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9400c00
.word 0xb4000160
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb500029a
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x14000289
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xaa1a03e1
.word 0xd2800001
.word 0xaa1a03e1
.word 0xd2800002
bl _p_110
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba0
.word 0xf90043a0
.word 0xf9402fb1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf90087a0
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_265
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xf94087a1
.word 0xf9000820
.word 0x91004021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9426e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9400800
.word 0xb50005e0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9429a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28112a1
.word 0xd28112a1
bl _p_32
.word 0xf90087a0
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2811661
.word 0xd2811661
bl _p_32
.word 0xaa0003e2
.word 0xf94087a0
.word 0xf9408ba1
bl _p_30
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf9431631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9435231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_13
.word 0xf900b7a0
.word 0xf9402fb1
.word 0xf9437631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf900bba0
.word 0xeb1f001f
.word 0x10000011
.word 0x540042e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #776]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf940b7a0
.word 0xf940bba2
.word 0xf9001022
.word 0x91008023
.word 0xd349fc63
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #16]
.word 0x8b040063
.word 0xd280003e
.word 0x3900007e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3896]
.word 0xf9001422

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3904]
.word 0xf9002022

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3912]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #808]
bl _p_40
.word 0xf900b3a0
.word 0xf9402fb1
.word 0xf9444631
.word 0xb4000051
.word 0xd63f0220
.word 0xf940b3a0
.word 0xf900afa0
.word 0xaa0003f7
.word 0xf9402fb1
.word 0xf9446231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940afa0
.word 0xf900a7a0
.word 0xf9403ba0
.word 0xf900aba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54003b40

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3920]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf940a7a0
.word 0xf940aba2
.word 0xf9001022
.word 0x91008023
.word 0xd349fc63
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #16]
.word 0x8b040063
.word 0xd280003e
.word 0x3900007e

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3928]
.word 0xf9001422

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3936]
.word 0xf9002022

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #3944]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3952]
bl _p_280
.word 0xf900a3a0
.word 0xf9402fb1
.word 0xf9453a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf940a3a1
.word 0xf9009fa0
bl _p_281
.word 0xf9402fb1
.word 0xf9456e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9409fa0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf90093a0
.word 0xaa1403e0
.word 0xf9009ba0
.word 0xf9403ba0
.word 0xf9400802
.word 0xf9403ba0
.word 0xf9400c01
.word 0xaa0203e0
.word 0xf940005e
bl _p_45
.word 0xf90097a0
.word 0xf9402fb1
.word 0xf945b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94097a1
.word 0xf9409ba2
.word 0xaa0203e0
.word 0xf940005e
bl _p_282
.word 0xf9402fb1
.word 0xf945da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94093a0
.word 0xf90047a0
.word 0xf9402fb1
.word 0xf945f231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3968]
.word 0xf90087a0
.word 0xd2800060

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800061
bl _p_16
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xf90083a0
.word 0xaa1303e0
.word 0xf9008fa0
.word 0xd2800000
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf9465631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba2
.word 0xf9408fa3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94083a0
.word 0xf94087a6
.word 0xf90053a0
.word 0xf94053a5
.word 0xf94053a4
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #368]
.word 0xaa1703e2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3976]
.word 0xf9400000
.word 0xf90057a0
.word 0xf94057a1
.word 0xf94057a0
.word 0xaa0603f6
.word 0xaa0503f8
.word 0xaa0403f5
.word 0xd280003e
.word 0xb900b3be
.word 0xf9005fa3
.word 0xf90063a2
.word 0xf90067a1
.word 0xb50008a0
.word 0xaa1603e0
.word 0xaa1803e0
.word 0xaa1503e0
.word 0xb980b3a0
.word 0xf9008ba0
.word 0xf9405fa0
.word 0xf90087a0
.word 0xf94063a0
.word 0xf90083a0
.word 0xf94067a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3248]
.word 0xf9400000
.word 0xf9008fa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54002620

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #392]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xf94083a1
.word 0xf94087a2
.word 0xf9408ba3
.word 0xf9408fa4
.word 0xf9001004
.word 0x91008005
.word 0xd349fca5
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e00a5

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x6, [x16, #16]
.word 0x8b0600a5
.word 0xd280003e
.word 0x390000be

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #3984]
.word 0xf9001404

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #3992]
.word 0xf9002004

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #4000]
.word 0xf9401485
.word 0xf9000c05
.word 0xf9401084
.word 0xf9000804
.word 0xd2800004
.word 0x3901801f
.word 0xf9006fa0
.word 0xf9406fa0
.word 0xf9406fa5

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #3976]
.word 0xf9000085
.word 0xb900b3a3
.word 0xf9005fa2
.word 0xf90063a1
.word 0xf90067a0
.word 0xaa1603e0
.word 0xaa1803e0
.word 0xaa1503e0
.word 0xb980b3a0
.word 0xf90093a0
.word 0xf9405fa0
.word 0xf9009ba0
.word 0xf94063a0
.word 0xf94067a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #424]
bl _p_21
.word 0xf900a3a0
.word 0xf9402fb1
.word 0xf9483231
.word 0xb4000051
.word 0xd63f0220
.word 0xf940a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #432]
bl _p_22
.word 0xf9009fa0
.word 0xf9402fb1
.word 0xf9485a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9409ba0
.word 0xf9409fa1
bl _p_23
.word 0xf90097a0
.word 0xf9402fb1
.word 0xf9487a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94093a1
.word 0xf94097a2
.word 0xaa1503e0
.word 0xf94002a3
.word 0xf9407870
.word 0xd63f0200
.word 0xf9006bb8
.word 0xf9406ba0
.word 0xf90087a0
.word 0xf9406ba0
.word 0xf9008fa0
.word 0xd2800040
.word 0xf9403ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf948d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba2
.word 0xf9408fa3
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94087a1
.word 0xaa1603e0
bl _p_18
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf9490e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xf9004ba0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9493631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf90087a0
.word 0xf9404ba0
.word 0xf9008ba0
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_283
.word 0xf9008fa0
.word 0xf9402fb1
.word 0xf9496a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94087a0
.word 0xf9408ba1
.word 0xf9408fa2
bl _p_207
.word 0x93407c00
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf9499231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xb9007ba0
.word 0xf9402fb1
.word 0xf949aa31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000070
.word 0xf90073a0
.word 0xf94073a0
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf949ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_43
.word 0x93407c00
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf949f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xd2800261
.word 0xd280027e
.word 0x6b1e001f
.word 0x54000a01
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94a2631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
bl _p_95
.word 0xf9008fa0
.word 0xf9402fb1
.word 0xf94a4231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9008ba0
.word 0xf9402fb1
.word 0xf94a7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
bl _p_100
.word 0xf90087a0
.word 0xf9402fb1
.word 0xf94a9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94087a1
.word 0xf9408ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1696]
.word 0x928006f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf94ade31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xd280a261
.word 0xd280a27e
.word 0x6b1e001f
.word 0x540002a1
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94b1231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf94043a1
.word 0xf9403ba2
.word 0xf9400c42
bl _p_284
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf94b3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
bl _p_33
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94b6231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
bl _p_252
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94b8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xb9807ba0
.word 0xf90083a0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94bb231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xf9402fb1
.word 0xf94bc631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xf94023ba
.word 0x910003bf
.word 0xa8d87bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_9a:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Delete_object
SQLite_Net_SQLiteConnection_Delete_object:
.loc 1 1 0
.word 0xa9b27bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f9
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #4008]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf9402bb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xf9400340
.word 0xf9400c00
.word 0xf9004ba0
.word 0xf9402bb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xd2800000
.word 0xaa1903e0
.word 0xd2800002
bl _p_110
.word 0xf90047a0
.word 0xf9402bb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf90043a0
.word 0xaa0003f8
.word 0xf9402bb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf940003e
bl _p_265
.word 0xf9003fa0
.word 0xf9402bb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xf9003ba0
.word 0xaa0003f7
.word 0xf9402bb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003e1
.word 0xb5000560
.word 0xf9402bb1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2812321
.word 0xd2812321
bl _p_32
.word 0xf9003fa0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf940031e
bl _p_17
.word 0xf90043a0
.word 0xf9402bb1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2811661
.word 0xd2811661
bl _p_32
.word 0xaa0003e2
.word 0xf9403fa0
.word 0xf94043a1
bl _p_30
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402bb1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #4016]
.word 0xf90057a0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800041
bl _p_16
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xf90067a0
.word 0xaa1503e0
.word 0xf9006fa0
.word 0xd2800000
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf940031e
bl _p_17
.word 0xf9006ba0
.word 0xf9402bb1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba2
.word 0xf9406fa3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94067a0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf9005ba0
.word 0xaa1403e0
.word 0xf90063a0
.word 0xd2800020
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_29
.word 0xf9005fa0
.word 0xf9402bb1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa2
.word 0xf94063a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94057a0
.word 0xf9405ba1
bl _p_18
.word 0xf90053a0
.word 0xf9402bb1
.word 0xf942de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf9004fa0
.word 0xaa0003f6
.word 0xf9402bb1
.word 0xf942fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xaa1903e1
.word 0xf9003fa0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xf90043a0
.word 0xaa1303e0
.word 0xf9004ba0
.word 0xd2800000
.word 0xaa1703e0
.word 0xaa1a03e0
.word 0xaa1703e0
.word 0xaa1a03e1
.word 0xf94002fe
bl _p_45
.word 0xf90047a0
.word 0xf9402bb1
.word 0xf9436231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a2
.word 0xf9404ba3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9403fa1
.word 0xf94043a2
.word 0xaa1903e0
bl _p_207
.word 0x93407c00
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf943a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf943c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9402bb1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8ce7bfd
.word 0xd65f03c0

Lme_9b:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Delete_T_REF_object
SQLite_Net_SQLiteConnection_Delete_T_REF_object:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xf90023b9
.word 0xf90037af
.word 0xaa0003f9
.word 0xf90027a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #4024]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf9402bb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94037a0
bl _p_285
.word 0xaa0003e1
.word 0xd2800000
.word 0xaa1903e0
.word 0xd2800002
bl _p_110
.word 0xf90047a0
.word 0xf9402bb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf90043a0
.word 0xaa0003f8
.word 0xf9402bb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf940003e
bl _p_265
.word 0xf9003fa0
.word 0xf9402bb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xf9003ba0
.word 0xaa0003f7
.word 0xf9402bb1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003e1
.word 0xb5000560
.word 0xf9402bb1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2812321
.word 0xd2812321
bl _p_32
.word 0xf9003fa0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf940031e
bl _p_17
.word 0xf90043a0
.word 0xf9402bb1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2811661
.word 0xd2811661
bl _p_32
.word 0xaa0003e2
.word 0xf9403fa0
.word 0xf94043a1
bl _p_30
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf941a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402bb1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #4016]
.word 0xf9004fa0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800041
bl _p_16
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xf9005fa0
.word 0xaa1503e0
.word 0xf90067a0
.word 0xd2800000
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf940031e
bl _p_17
.word 0xf90063a0
.word 0xf9402bb1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a2
.word 0xf94067a3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9405fa0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf90053a0
.word 0xaa1403e0
.word 0xf9005ba0
.word 0xd2800020
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_29
.word 0xf90057a0
.word 0xf9402bb1
.word 0xf9428e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a2
.word 0xf9405ba3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9404fa0
.word 0xf94053a1
bl _p_18
.word 0xf9004ba0
.word 0xf9402bb1
.word 0xf942ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xf90047a0
.word 0xaa0003f6
.word 0xf9402bb1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xaa1903e1
.word 0xf9003fa0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xf90043a0
.word 0xaa1303e0
.word 0xd2800000
.word 0xf94027a2
.word 0xaa1303e0
.word 0xd2800001
.word 0xf9400263
.word 0xf9407870
.word 0xd63f0200
.word 0xf9403fa1
.word 0xf94043a2
.word 0xaa1903e0
bl _p_207
.word 0x93407c00
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf9435e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9437e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9402bb1
.word 0xf9439231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xf94023b9
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_9c:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Finalize
SQLite_Net_SQLiteConnection_Finalize:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #4032]
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
.word 0xf9400ba2
.word 0xd2800000
.word 0xaa0203e0
.word 0xd2800001
.word 0xf9400042
.word 0xf9403450
.word 0xd63f0200
.word 0xf9400fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000012
.word 0xf90023be
.word 0xf9400fb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_87
.word 0xf9400fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023be
.word 0xd61f03c0
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000001
.word 0xf9400fb1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_9d:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Dispose_bool
SQLite_Net_SQLiteConnection_Dispose_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #4040]
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
bl _p_286
.word 0xf94013b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
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

Lme_9e:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Close
SQLite_Net_SQLiteConnection_Close:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #4048]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd280001a
.word 0xf90023bf
.word 0xd2800019
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
.word 0xf94013a0
.word 0x39420000
.word 0x34002d80
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
bl _p_100
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3016]
.word 0xf9400021
.word 0xeb01001f
.word 0x54002ac0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf9401400
.word 0xb40013a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf9401401
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #4056]
.word 0x928007f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003fa0
.word 0xf94017b1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #4064]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000020
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #4072]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_287
.word 0xf94017b1
.word 0xf9423631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9425631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf9429631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0x35fff900
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000028
.word 0xf90033be
.word 0xf94017b1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xb40002e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9430231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94017b1
.word 0xf9433a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9435a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033be
.word 0xd61f03c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9438231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
bl _p_95
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf9439e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90047a0
.word 0xf94017b1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
bl _p_100
.word 0xf90043a0
.word 0xf94017b1
.word 0xf943f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xf94047a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #4080]
.word 0x928005f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9003fa0
.word 0xf94017b1
.word 0xf9443a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xf9003ba0
.word 0xaa0003fa
.word 0xf94017b1
.word 0xf9445631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003e1
.word 0x34000920
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9448231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
bl _p_95
.word 0xf9004fa0
.word 0xf94017b1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9004ba0
.word 0xf94017b1
.word 0xf944da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
bl _p_100
.word 0xf90047a0
.word 0xf94017b1
.word 0xf944f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xf9404ba2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1688]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf90043a0
.word 0xf94017b1
.word 0xf9453631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf9003fa0
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf9455231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa1
.word 0xaa1a03e0
.word 0xaa0103e0
.word 0xaa1a03e0
bl _p_101
.word 0xf9003ba0
.word 0xf94017b1
.word 0xf9457a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
bl _p_33
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf945a231
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x1400001d
.word 0xf90037be
.word 0xf94017b1
.word 0xf945be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3016]
.word 0xf9400021
bl _p_195
.word 0xf94017b1
.word 0xf945e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf945f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xd2800001
.word 0x3902001f
.word 0xf94017b1
.word 0xf9461231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037be
.word 0xd61f03c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9463a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9464a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0

Lme_9f:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__CreateTableb__66_0_SQLite_Net_TableMapping_Column
SQLite_Net_SQLiteConnection__CreateTableb__66_0_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #4088]
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
.word 0xf9400fa0
.word 0xf90027a0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_97
.word 0x53001c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_98
.word 0xf9002fa0
.word 0xf94013b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_237
.word 0xf90033a0
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9402ba1
.word 0xf9402fa2
.word 0xf94033a3
bl _p_238
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf9412231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_a0:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_ColumnInfo_get_Name
SQLite_Net_SQLiteConnection_ColumnInfo_get_Name:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #0]
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

Lme_a1:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_ColumnInfo_set_Name_string
SQLite_Net_SQLiteConnection_ColumnInfo_set_Name_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #8]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_a2:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_ColumnInfo_ToString
SQLite_Net_SQLiteConnection_ColumnInfo_ToString:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #16]
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
bl _p_231
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_a3:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_ColumnInfo__ctor
SQLite_Net_SQLiteConnection_ColumnInfo__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #24]
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

Lme_a4:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__cctor
SQLite_Net_SQLiteConnection__c__cctor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #32]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #40]
.word 0xd2800201
.word 0xd2800201
bl _p_20
.word 0xf9001ba0
bl _p_288
.word 0xf9400bb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3248]
.word 0xf9000001
.word 0xf9400bb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_a5:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__ctor
SQLite_Net_SQLiteConnection__c__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #48]
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

Lme_a6:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__CreateTableb__66_1_SQLite_Net_SQLiteConnection_IndexedColumn
SQLite_Net_SQLiteConnection__c__CreateTableb__66_1_SQLite_Net_SQLiteConnection_IndexedColumn:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #56]
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
.word 0x910063a0
.word 0x910103a0
.word 0xf9400fa0
.word 0xf90023a0
.word 0xf94013a0
.word 0xf90027a0
.word 0x910103a0
.word 0xb9804ba0
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_a7:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__CreateTableb__66_2_SQLite_Net_SQLiteConnection_IndexedColumn
SQLite_Net_SQLiteConnection__c__CreateTableb__66_2_SQLite_Net_SQLiteConnection_IndexedColumn:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #64]
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
.word 0x910063a0
.word 0x910103a0
.word 0xf9400fa0
.word 0xf90023a0
.word 0xf94013a0
.word 0xf90027a0
.word 0x910103a0
.word 0xf94023a0
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94017b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_a8:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__Updateb__116_2_SQLite_Net_TableMapping_Column
SQLite_Net_SQLiteConnection__c__Updateb__116_2_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #72]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #512]
.word 0xf90027a0
.word 0xf9400fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9402ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #80]
bl _p_30
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_a9:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__DisplayClass100_0__ctor
SQLite_Net_SQLiteConnection__c__DisplayClass100_0__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #88]
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

Lme_aa:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__DisplayClass100_0__InsertAllb__0
SQLite_Net_SQLiteConnection__c__DisplayClass100_0__InsertAllb__0:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #96]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9001fbf
.word 0xd2800019
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
.word 0xaa1a03e0
.word 0xf9400b41
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3776]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90043a0
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf9001fa0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000032
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3784]
.word 0x928003f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf94013b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf9004ba0
.word 0xaa0003f9
.word 0xf94013b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xb9802340
.word 0xf90043a0
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xaa0103e2
bl _p_263
.word 0x93407c00
.word 0xf90047a0
.word 0xf94013b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf94047a1
.word 0xb010000
.word 0xb9002340
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf90043a0
.word 0xf94013b1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0x35fff6c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000050
.word 0xf9003bbe
.word 0xf94013b1
.word 0xf9423631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
.word 0xf90027a0
.word 0xf94027a0
.word 0xf9002ba0
.word 0xf94027a0
.word 0xeb1f001f
.word 0x54000380
.word 0xf94027a0
.word 0xf9400000
.word 0xf9002fa0
.word 0xf9402fa0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3792]
.word 0xeb01001f
.word 0x540001e3
.word 0xf9402fa0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3792]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000080
.word 0x14000001
.word 0xf9002bbf
.word 0x14000001
.word 0xf9402ba0
.word 0xf90023a0
.word 0xf94013b1
.word 0xf942d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xb40002e0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf942fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94013b1
.word 0xf9433631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bbe
.word 0xd61f03c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9437e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9438e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0

Lme_ab:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__DisplayClass116_0__ctor
SQLite_Net_SQLiteConnection__c__DisplayClass116_0__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #104]
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

Lme_ac:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__DisplayClass116_0__Updateb__0_SQLite_Net_TableMapping_Column
SQLite_Net_SQLiteConnection__c__DisplayClass116_0__Updateb__0_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #112]
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
.word 0xf9400ba1
.word 0xf9400821
.word 0xeb01001f
.word 0x9a9f17e0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_ad:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection__c__DisplayClass116_0__Updateb__1_SQLite_Net_TableMapping_Column
SQLite_Net_SQLiteConnection__c__DisplayClass116_0__Updateb__1_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #120]
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
.word 0xf9400ba0
.word 0xf9400c01
.word 0xaa0203e0
.word 0xf940005e
bl _p_45
.word 0xf90023a0
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
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_ae:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping__ctor_System_Type_System_Collections_Generic_IEnumerable_1_System_Reflection_PropertyInfo_SQLite_Net_Interop_CreateFlags
SQLite_Net_TableMapping__ctor_System_Type_System_Collections_Generic_IEnumerable_1_System_Reflection_PropertyInfo_SQLite_Net_Interop_CreateFlags:
.loc 1 1 0
.word 0xa9ae7bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xaa0103f8
.word 0xf9002fa2
.word 0xf90033a3

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #128]
.word 0xf90037b0
.word 0xf9400a11
.word 0xf9003bb1
.word 0xd2800016
.word 0xd2800015
.word 0xf90043bf
.word 0xd2800014
.word 0xd2800013
.word 0xd280001a
.word 0xd2800019
.word 0xd2800017
.word 0xf94037b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94037b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa1803e1
.word 0xaa1803e1
bl _p_289
.word 0xf94037b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_56
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #136]
bl _p_290
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #144]
bl _p_291
.word 0xf9006fa0
.word 0xf94037b1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xf9006ba0
.word 0xaa0003f6
.word 0xf94037b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf9402ba1
.word 0xaa0003e2
.word 0xf90047a1
.word 0xb5000320
.word 0xf94047a0
.word 0xf9006fa0
.word 0xf9402ba0
bl _p_173
.word 0xf90073a0
.word 0xf94037b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf9406fa1
.word 0xf90047a1
.word 0xf9004ba0
.word 0x14000010
.word 0xf94047a0
.word 0xf9006fa0
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_292
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf9406fa1
.word 0xf90047a1
.word 0xf9004ba0
.word 0xf94047a0
.word 0xf9404ba1
bl _p_293
.word 0xf94037b1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf90073a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3416]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9006fa0
bl _p_228
.word 0xf94037b1
.word 0xf9428231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xf94073a1
.word 0xaa0003f5
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #152]
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf942c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf90043a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf942ee31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400006c
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #160]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9434e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf90073a0
.word 0xaa0003f4
.word 0xf94037b1
.word 0xf9436a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xaa0003e1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #168]
.word 0xd2800022
.word 0xd2800022
bl _p_294
.word 0x53001c00
.word 0xf9006fa0
.word 0xf94037b1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xaa0003f3
.word 0xf94037b1
.word 0xf943ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9408830
.word 0xd63f0200
.word 0x53001c00
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf943e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0x340005a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9440e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0x35000473
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9443631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1403e0
.word 0xb98063a0
.word 0xf9006fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #176]
.word 0xd2800a01
.word 0xd2800a01
bl _p_20
.word 0xf9406fa2
.word 0xf9006ba0
.word 0xaa1403e1
bl _p_295
.word 0xf94037b1
.word 0xf9447e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba1
.word 0xaa1503e0
.word 0xf94002be
bl _p_234
.word 0xf94037b1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf944be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf944fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0x35ffef80
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9452631
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000028
.word 0xf90063be
.word 0xf94037b1
.word 0xf9454231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xb40002e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9456a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94037b1
.word 0xf945a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf945c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063be
.word 0xd61f03c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf945ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9006fa0
.word 0xaa1503e0
.word 0xaa1503e0
.word 0xf94002be
bl _p_296
.word 0xf90073a0
.word 0xf94037b1
.word 0xf9461631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xf94073a1
bl _p_297
.word 0xf94037b1
.word 0xf9463231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9464231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_13
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf9465e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xaa0003fa
.word 0xf94037b1
.word 0xf9467631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800019
.word 0xf94037b1
.word 0xf9468a31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400007e
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf946ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1903e0
.word 0x93407f20
.word 0xb9801b41
.word 0xeb00003f
.word 0x10000011
.word 0x54002789
.word 0xd37df000
.word 0x8b000340
.word 0x91008000
.word 0xf9400000
.word 0xaa0003f7
.word 0xf94037b1
.word 0xf946ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_15
.word 0x53001c00
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf9471631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0x34000560
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9473e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_48
.word 0x53001c00
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf9476631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0x340002e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9478e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa1703e1
.word 0xf9000817
.word 0x91004000
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf947e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_48
.word 0x53001c00
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf9480e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0x34000220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9483631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa1703e1
.word 0xaa1703e1
bl _p_298
.word 0xf94037b1
.word 0xf9485631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9487631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x11000720
.word 0xaa0003f9
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf948a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xb9801b40
.word 0x6b00033f
.word 0x54ffeeeb
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf948d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402ba1
.word 0xf9400821
.word 0xd2800002
.word 0xeb1f003f
.word 0x9a9f97e1
bl _p_299
.word 0xf94037b1
.word 0xf9490231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9491231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_265
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf9492e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xb4000b60
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9495631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9006ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #184]
.word 0xf90073a0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800041
bl _p_16
.word 0xf90053a0
.word 0xf94053a0
.word 0xf90087a0
.word 0xf94053a0
.word 0xf9008fa0
.word 0xd2800000
.word 0xf9402ba0
bl _p_17
.word 0xf9008ba0
.word 0xf94037b1
.word 0xf949ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408ba2
.word 0xf9408fa3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94087a0
.word 0xf90057a0
.word 0xf94057a0
.word 0xf90077a0
.word 0xf94057a0
.word 0xf9007fa0
.word 0xd2800020
.word 0xf9402ba0
bl _p_265
.word 0xf90083a0
.word 0xf94037b1
.word 0xf94a0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf94a3231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba2
.word 0xf9407fa3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94073a0
.word 0xf94077a1
bl _p_18
.word 0xf9006fa0
.word 0xf94037b1
.word 0xf94a6e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf9406fa1
bl _p_300
.word 0xf94037b1
.word 0xf94a8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94a9a31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400003c
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94abe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9006ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #192]
.word 0xf90073a0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf9004fa0
.word 0xf9404fa0
.word 0xf90077a0
.word 0xf9404fa0
.word 0xf9007fa0
.word 0xd2800000
.word 0xf9402ba0
bl _p_17
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf94b2231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba2
.word 0xf9407fa3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94073a0
.word 0xf94077a1
bl _p_18
.word 0xf9006fa0
.word 0xf94037b1
.word 0xf94b5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf9406fa1
bl _p_300
.word 0xf94037b1
.word 0xf94b7a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94b8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94b9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d27bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_af:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_get_MappedType
SQLite_Net_TableMapping_get_MappedType:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #200]
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

Lme_b0:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_set_MappedType_System_Type
SQLite_Net_TableMapping_set_MappedType_System_Type:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #208]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_b1:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_get_TableName
SQLite_Net_TableMapping_get_TableName:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #216]
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

Lme_b2:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_set_TableName_string
SQLite_Net_TableMapping_set_TableName_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #224]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_b3:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_get_Columns
SQLite_Net_TableMapping_get_Columns:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_b4:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_set_Columns_SQLite_Net_TableMapping_Column__
SQLite_Net_TableMapping_set_Columns_SQLite_Net_TableMapping_Column__:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #240]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_b5:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_get_PK
SQLite_Net_TableMapping_get_PK:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #248]
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
.word 0xf9401c00
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

Lme_b6:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_set_PK_SQLite_Net_TableMapping_Column
SQLite_Net_TableMapping_set_PK_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #256]
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
.word 0xf9001c20
.word 0x9100e021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_b7:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_set_GetByPrimaryKeySql_string
SQLite_Net_TableMapping_set_GetByPrimaryKeySql_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #264]
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
.word 0xf9002020
.word 0x91010021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_b8:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_get_HasAutoIncPK
SQLite_Net_TableMapping_get_HasAutoIncPK:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #272]
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
.word 0x39412000
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

Lme_b9:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_set_HasAutoIncPK_bool
SQLite_Net_TableMapping_set_HasAutoIncPK_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #280]
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
.word 0x39012001
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

Lme_ba:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_get_InsertColumns
SQLite_Net_TableMapping_get_InsertColumns:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #288]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xd2800019
.word 0xf9402bb1
.word 0xf9404631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xaa0003f8
.word 0xaa1803e0
.word 0xaa1803e1
.word 0xaa0003f7
.word 0xb5001078
.word 0xaa1703e0
.word 0xf9402bb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_13
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #296]
.word 0xf9400000
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xaa1603e2
.word 0xaa1a03f7
.word 0xaa0103f5
.word 0xaa0003f4
.word 0xb5000716
.word 0xaa1703e0
.word 0xaa1503e0
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #304]
.word 0xf9400000
.word 0xf9003ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000e60

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #776]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xf9403ba1
.word 0xf9001001
.word 0x91008002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #312]
.word 0xf9001401

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #320]
.word 0xf9002001

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #328]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf90037a0
.word 0xf94037a0
.word 0xf94037a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #296]
.word 0xf9000022
.word 0xaa0003f4
.word 0xaa1703e0
.word 0xaa1503e0
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #808]
.word 0xaa1503e0
.word 0xaa1403e1
bl _p_40
.word 0xf90043a0
.word 0xf9402bb1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #336]
bl _p_301
.word 0xf9003fa0
.word 0xf9402bb1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xaa0003f3
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa0003f9
.word 0xf9000ee0
.word 0x910062e1
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9003ba0
.word 0xf9402bb1
.word 0xf9427e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003e1
.word 0xaa0003f7
.word 0xaa1703e0
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9402bb1
.word 0xf942c231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_bb:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_SetAutoIncPK_object_long
SQLite_Net_TableMapping_SetAutoIncPK_object_long:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xaa0003f8
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #344]
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
.word 0xaa1803e0
.word 0xf9400b00
.word 0xb4000660
.word 0xf94017b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400b00
.word 0xf9002ba0
.word 0xf9400fa0
.word 0xf90023a0
.word 0xf94013a0
.word 0xf90037a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2560]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xf94037a1
.word 0xf9000801
.word 0xf9002fa0
.word 0xaa1803e0
.word 0xf9400b01
.word 0xaa0103e0
.word 0xf940003e
bl _p_55
.word 0xf90033a0
.word 0xf94017b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf94033a1
.word 0xd2800002
.word 0xd2800002
bl _p_302
.word 0xf90027a0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xf94027a2
.word 0xf9402ba3
.word 0xaa0303e0
.word 0xf940007e
bl _p_174
.word 0xf94017b1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_bc:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_FindColumnWithPropertyName_string
SQLite_Net_TableMapping_FindColumnWithPropertyName_string:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fa0
.word 0xf90013a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #352]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #360]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xf9002fa0
bl _p_303
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
bl _p_13
.word 0xf90027a0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9002ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000820

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #776]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf94027a0
.word 0xf9402ba2
.word 0xf9001022
.word 0x91008023
.word 0xd349fc63
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #16]
.word 0x8b040063
.word 0xd280003e
.word 0x3900007e

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #368]
.word 0xf9001422

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #376]
.word 0xf9002022

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #384]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #392]
bl _p_304
.word 0xf90023a0
.word 0xf94017b1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94017b1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_bd:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_FindColumn_string
SQLite_Net_TableMapping_FindColumn_string:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fa0
.word 0xf90013a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #400]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #408]
.word 0xd2800301
.word 0xd2800301
bl _p_20
.word 0xf9002fa0
bl _p_305
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
bl _p_13
.word 0xf90027a0
.word 0xf94017b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9002ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000820

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #776]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf94027a0
.word 0xf9402ba2
.word 0xf9001022
.word 0x91008023
.word 0xd349fc63
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x4, [x16, #16]
.word 0x8b040063
.word 0xd280003e
.word 0x3900007e

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #416]
.word 0xf9001422

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #424]
.word 0xf9002022

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #432]
.word 0xf9401443
.word 0xf9000c23
.word 0xf9401042
.word 0xf9000822
.word 0xd2800002
.word 0x3901803f

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #392]
bl _p_304
.word 0xf90023a0
.word 0xf94017b1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94017b1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_be:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column__ctor_System_Reflection_PropertyInfo_SQLite_Net_Interop_CreateFlags
SQLite_Net_TableMapping_Column__ctor_System_Reflection_PropertyInfo_SQLite_Net_Interop_CreateFlags:
.loc 1 1 0
.word 0xa9b57bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf9002ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #440]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0xd2800017
.word 0xd2800016
.word 0xf9402fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9402fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #448]
.word 0xaa1903e0
.word 0xd2800021
bl _p_306
.word 0xf90053a0
.word 0xf9402fb1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #456]
bl _p_307
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf9004ba0
.word 0xaa0003f7
.word 0xf9402fb1
.word 0xf9410e31
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf9402fb1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa1803e1
.word 0xaa0003e1
.word 0xaa1803f5
.word 0xb40001c0
.word 0xaa1503e0
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_308
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f4
.word 0x1400000e
.word 0xaa1503e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9405830
.word 0xd63f0200
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf941ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f4
.word 0xaa1503e0
.word 0xaa1403e0
.word 0xaa1503e0
.word 0xaa1403e1
bl _p_309
.word 0xf9402fb1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9407c30
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf9423231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
bl _p_121
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9424e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f3
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1803f5
.word 0xaa0003f4
.word 0xb50001e0
.word 0xaa1503e0
.word 0xaa1403e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9407c30
.word 0xd63f0200
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9429a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f4
.word 0xaa1503e0
.word 0xaa1403e0
.word 0xaa1503e0
.word 0xaa1403e1
bl _p_310
.word 0xf9402fb1
.word 0xf942c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf942d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_311
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf942fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa1
.word 0xaa1803e0
bl _p_312
.word 0xf9402fb1
.word 0xf9431631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9432631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_313
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9434e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa1803f5
.word 0x35000560
.word 0xaa1503e0
.word 0xb98053a0
.word 0xd280003e
.word 0xa1e0000
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x540003e1
.word 0xaa1503e0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9405830
.word 0xd63f0200
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf943a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #464]
.word 0xd28000a2
.word 0xd28000a2
bl _p_19
.word 0x93407c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf943da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xaa0003fa
.word 0x14000008
.word 0xaa1503e0
.word 0xd2800000
.word 0xd280001a
.word 0x14000004
.word 0xaa1503e0
.word 0xd2800020
.word 0xd280003a
.word 0xaa1503e0
.word 0xaa1a03e0
.word 0xaa1503e0
.word 0xaa1a03e1
bl _p_314
.word 0xf9402fb1
.word 0xf9443231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9444231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_315
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9446631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x35000480
.word 0xf9402fb1
.word 0xf9447e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_48
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf944a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x340001c0
.word 0xf9402fb1
.word 0xf944ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xb98053a0
.word 0xd280009e
.word 0xa1e0000
.word 0xd2800081
.word 0xd280009e
.word 0x6b1e001f
.word 0x9a9f17e0
.word 0xb9007ba0
.word 0x1400000f
.word 0xf9402fb1
.word 0xf944ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xb9007bbf
.word 0x14000008
.word 0xf9402fb1
.word 0xf9450a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800020
.word 0xd280003e
.word 0xb9007bbe
.word 0xb9807ba0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf9452e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1603e0
.word 0xaa1803f5
.word 0x34000256
.word 0xaa1503e0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_55
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9456231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1312]
.word 0xeb01001f
.word 0x9a9f17e0
.word 0xaa0003fa
.word 0x14000004
.word 0xaa1503e0
.word 0xd2800000
.word 0xd280001a
.word 0xaa1503e0
.word 0xaa1a03e0
.word 0xaa1503e0
.word 0xaa1a03e1
bl _p_316
.word 0xf9402fb1
.word 0xf945b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf945c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1603e0
.word 0xaa1803f5
.word 0x34000236
.word 0xaa1503e0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_266
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf945fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xaa0003fa
.word 0x14000004
.word 0xaa1503e0
.word 0xd2800000
.word 0xd280001a
.word 0xaa1503e0
.word 0xaa1a03e0
.word 0xaa1503e0
.word 0xaa1a03e1
bl _p_317
.word 0xf9402fb1
.word 0xf9464231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9465231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_318
.word 0xf90057a0
.word 0xf9402fb1
.word 0xf9467631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a1
.word 0xaa1803e0
bl _p_319
.word 0xf9402fb1
.word 0xf9469231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf946a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_320
.word 0xf90053a0
.word 0xf9402fb1
.word 0xf946c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a1
.word 0xaa1803e0
bl _p_321
.word 0xf9402fb1
.word 0xf946e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf946f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_210
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf9471231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #472]
bl _p_322
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9473e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x35000c60
.word 0xf9402fb1
.word 0xf9475631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_48
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9477a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x35000a80
.word 0xf9402fb1
.word 0xf9479231
.word 0xb4000051
.word 0xd63f0220
.word 0xb98053a0
.word 0xd280005e
.word 0xa1e0000
.word 0xd2800041
.word 0xd280005e
.word 0x6b1e001f
.word 0x54000921
.word 0xf9402fb1
.word 0xf947be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_29
.word 0xf9004fa0
.word 0xf9402fb1
.word 0xf947de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa3

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #464]
.word 0xd28000a0
.word 0xaa0303e0
.word 0xd28000a2
.word 0xf940007e
bl _p_323
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9481a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0x34000580
.word 0xf9402fb1
.word 0xf9483231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #480]
.word 0xd2800021
bl _p_16
.word 0xf90047a0
.word 0xf94047a0
.word 0xf9004ba0
.word 0xf94047a0
.word 0xf90053a0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #488]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9004fa0
bl _p_324
.word 0xf9402fb1
.word 0xf9489631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa2
.word 0xf94053a3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9404ba1
.word 0xaa1803e0
bl _p_321
.word 0xf9402fb1
.word 0xf948ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf948ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_48
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9491631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa1803f5
.word 0x35000220
.word 0xaa1503e0
.word 0xaa1903e0
.word 0xaa1903e0
bl _p_325
.word 0x53001c00
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9494a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xaa0003fa
.word 0x14000004
.word 0xaa1503e0
.word 0xd2800000
.word 0xd280001a
.word 0xaa1503e0
.word 0xaa1a03e0
.word 0xaa1503e0
.word 0xaa1a03e1
bl _p_326
.word 0xf9402fb1
.word 0xf9499231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf949a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0x9101c3a0
.word 0xf90043a0
.word 0xaa1903e0
bl _p_327
.word 0xf94043be
.word 0xf90003c0
.word 0xf9402fb1
.word 0xf949d231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0x9101c3a1
.word 0xf9403ba1
bl _p_328
.word 0xf9402fb1
.word 0xf949f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94a0231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94a1231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8cb7bfd
.word 0xd65f03c0

Lme_bf:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_Name
SQLite_Net_TableMapping_Column_get_Name:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #496]
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

Lme_c0:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_Name_string
SQLite_Net_TableMapping_Column_set_Name_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #504]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_c1:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_PropertyName
SQLite_Net_TableMapping_Column_get_PropertyName:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
.word 0xf9400801
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
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

Lme_c2:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_ColumnType
SQLite_Net_TableMapping_Column_get_ColumnType:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #520]
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

Lme_c3:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_ColumnType_System_Type
SQLite_Net_TableMapping_Column_set_ColumnType_System_Type:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #528]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_c4:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_Collation
SQLite_Net_TableMapping_Column_get_Collation:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #536]
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

Lme_c5:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_Collation_string
SQLite_Net_TableMapping_Column_set_Collation_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #544]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_c6:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_IsAutoInc
SQLite_Net_TableMapping_Column_get_IsAutoInc:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #552]
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
.word 0x39410000
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

Lme_c7:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_IsAutoInc_bool
SQLite_Net_TableMapping_Column_set_IsAutoInc_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
.word 0x394063a1
.word 0x39010001
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

Lme_c8:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_IsAutoGuid
SQLite_Net_TableMapping_Column_get_IsAutoGuid:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
.word 0x39410400
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

Lme_c9:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_IsAutoGuid_bool
SQLite_Net_TableMapping_Column_set_IsAutoGuid_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #576]
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
.word 0x39010401
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

Lme_ca:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_IsPK
SQLite_Net_TableMapping_Column_get_IsPK:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #584]
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
.word 0x39410800
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

Lme_cb:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_IsPK_bool
SQLite_Net_TableMapping_Column_set_IsPK_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #592]
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
.word 0x39010801
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

Lme_cc:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_Indices
SQLite_Net_TableMapping_Column_get_Indices:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #600]
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

Lme_cd:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_Indices_System_Collections_Generic_IEnumerable_1_SQLite_Net_Attributes_IndexedAttribute
SQLite_Net_TableMapping_Column_set_Indices_System_Collections_Generic_IEnumerable_1_SQLite_Net_Attributes_IndexedAttribute:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #608]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_ce:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_IsNullable
SQLite_Net_TableMapping_Column_get_IsNullable:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #616]
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
.word 0x39410c00
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

Lme_cf:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_IsNullable_bool
SQLite_Net_TableMapping_Column_set_IsNullable_bool:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #624]
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
.word 0x39010c01
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

Lme_d0:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_MaxStringLength
SQLite_Net_TableMapping_Column_get_MaxStringLength:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf90013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #632]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94017b1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0x91011000
.word 0x910103a1
.word 0xf9400000
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0x910103a0
.word 0x910043a0
.word 0xf94023a0
.word 0xf9000ba0
.word 0xf94017b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_d1:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_MaxStringLength_System_Nullable_1_int
SQLite_Net_TableMapping_Column_set_MaxStringLength_System_Nullable_1_int:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #640]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94017b1
.word 0xf9403631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0x910063a1
.word 0x910103a1
.word 0xf9400fa1
.word 0xf90023a1
.word 0x910103a1
.word 0x91011000
.word 0xf94023a1
.word 0xf9000001
.word 0xf94017b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_d2:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_get_DefaultValue
SQLite_Net_TableMapping_Column_get_DefaultValue:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
.word 0xf9401c00
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

Lme_d3:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_set_DefaultValue_object
SQLite_Net_TableMapping_Column_set_DefaultValue_object:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #656]
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
.word 0xf9001c20
.word 0x9100e021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_d4:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_SetValue_object_object
SQLite_Net_TableMapping_Column_SetValue_object_object:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa90157b4
.word 0xa9025fb6
.word 0xa90367b8
.word 0xf90023ba
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xaa0203fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #664]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xf94027b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400b01
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9407c30
.word 0xd63f0200
.word 0xf90043a0
.word 0xf94027b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf9003fa0
.word 0xaa0003f7
.word 0xf94027b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xaa0003e1
bl _p_56
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf90037a0
.word 0xaa0003f6
.word 0xf94027b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf9400021
.word 0xf940fc30
.word 0xd63f0200
.word 0x53001c00
.word 0xf90033a0
.word 0xf94027b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x340011a0
.word 0xf94027b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002e1
.word 0xf940a030
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94027b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #672]
.word 0xeb01001f
.word 0x54000f21
.word 0xf94027b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
bl _p_56
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf940a430
.word 0xd63f0200
.word 0xf90037a0
.word 0xf94027b1
.word 0xf941de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xf90033a0
.word 0xaa0003f5
.word 0xf94027b1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003e1
.word 0xb9801800
.word 0x34001340
.word 0xf94027b1
.word 0xf9421a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xd2800000
.word 0xb9801aa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001469
.word 0xf94012a0
.word 0xaa0003f4
.word 0xf94027b1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xaa1403e0
bl _p_56
.word 0xf90037a0
.word 0xf94027b1
.word 0xf9426a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf941d430
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94027b1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #680]
.word 0xeb01001f
.word 0x540002e1
.word 0xf94027b1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1403e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1903e1
.word 0xaa1403e2
.word 0xaa1a03e3
bl _p_329
.word 0xf94027b1
.word 0xf942ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf942fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000064
.word 0xf94027b1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400b04
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xd2800000
.word 0xaa0403e0
.word 0xaa1903e1
.word 0xaa1a03e2
.word 0xd2800003
.word 0xf9400084
.word 0xf9406490
.word 0xd63f0200
.word 0xf94027b1
.word 0xf9435231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9436231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400004b
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002c1
.word 0xf941d430
.word 0xd63f0200
.word 0xf90033a0
.word 0xf94027b1
.word 0xf943ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #680]
.word 0xeb01001f
.word 0x540002e1
.word 0xf94027b1
.word 0xf943d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1903e0
.word 0xaa1703e0
.word 0xaa1a03e0
.word 0xaa1803e0
.word 0xaa1903e1
.word 0xaa1703e2
.word 0xaa1a03e3
bl _p_329
.word 0xf94027b1
.word 0xf9440a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9441a31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400001d
.word 0xf94027b1
.word 0xf9442e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9400b04
.word 0xaa1903e0
.word 0xaa1a03e0
.word 0xd2800000
.word 0xaa0403e0
.word 0xaa1903e1
.word 0xaa1a03e2
.word 0xd2800003
.word 0xf9400084
.word 0xf9406490
.word 0xd63f0200
.word 0xf94027b1
.word 0xf9446e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9448e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9449e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94157b4
.word 0xa9425fb6
.word 0xa94367b8
.word 0xf94023ba
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_d5:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_SetEnumValue_object_System_Type_object
SQLite_Net_TableMapping_Column_SetEnumValue_object_System_Type_object:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bb6
.word 0xf9000fa0
.word 0xf90013a1
.word 0xf90017a2
.word 0xf9001ba3

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #688]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xd2800016
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
.word 0xf9401ba0
.word 0xaa0003f6
.word 0xf9401fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xb4000496
.word 0xf9401fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017a0
.word 0xaa1603e1
.word 0xaa1603e1
bl _p_330
.word 0xf9002fa0
.word 0xf9401fb1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9002ba0
.word 0xaa0003f6
.word 0xf9401fb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba2
.word 0xf9400fa0
.word 0xf9400804
.word 0xf94013a1
.word 0xaa0203e0
.word 0xd2800000
.word 0xaa0403e0
.word 0xd2800003
.word 0xf9400084
.word 0xf9406490
.word 0xd63f0200
.word 0xf9401fb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb6
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_d6:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping_Column_GetValue_object
SQLite_Net_TableMapping_Column_GetValue_object:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #696]
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
.word 0xf9400803
.word 0xf9400fa1
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf9400063
.word 0xf9406c70
.word 0xd63f0200
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_d7:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping__c__cctor
SQLite_Net_TableMapping__c__cctor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #704]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #712]
.word 0xd2800201
.word 0xd2800201
bl _p_20
.word 0xf9001ba0
bl _p_331
.word 0xf9400bb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #304]
.word 0xf9000001
.word 0xf9400bb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_d8:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping__c__ctor
SQLite_Net_TableMapping__c__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_d9:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping__c__get_InsertColumnsb__28_0_SQLite_Net_TableMapping_Column
SQLite_Net_TableMapping__c__get_InsertColumnsb__28_0_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #728]
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
.word 0xf9400fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_15
.word 0x53001c00
.word 0xf90027a0
.word 0xf94013b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xf90023a0
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

Lme_da:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping__c__DisplayClass30_0__ctor
SQLite_Net_TableMapping__c__DisplayClass30_0__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_db:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping__c__DisplayClass30_0__FindColumnWithPropertyNameb__0_SQLite_Net_TableMapping_Column
SQLite_Net_TableMapping__c__DisplayClass30_0__FindColumnWithPropertyNameb__0_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #744]
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
.word 0xf9400fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_267
.word 0xf90027a0
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9400ba1
.word 0xf9400821
bl _p_332
.word 0x53001c00
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_dc:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping__c__DisplayClass31_0__ctor
SQLite_Net_TableMapping__c__DisplayClass31_0__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_dd:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableMapping__c__DisplayClass31_0__FindColumnb__0_SQLite_Net_TableMapping_Column
SQLite_Net_TableMapping__c__DisplayClass31_0__FindColumnb__0_SQLite_Net_TableMapping_Column:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #760]
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
.word 0xf9400fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf90027a0
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9400ba1
.word 0xf9400821
bl _p_332
.word 0x53001c00
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_de:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection_SQLite_Net_TableMapping
SQLite_Net_TableQuery_1_T_REF__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2
.word 0xf90017a3

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #768]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf9401bb1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
bl _p_333
.word 0xf9401bb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9408631
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9401bb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf94013a1
bl _p_334
.word 0xf9401bb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf94017a1
bl _p_335
.word 0xf9401bb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_df:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection
SQLite_Net_TableQuery_1_T_REF__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #776]
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
bl _p_333
.word 0xf94017b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9408231
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf94013a1
bl _p_334
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf90023a0
.word 0xf9400ba0
bl _p_336
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400000
bl _p_337
.word 0xaa0003e1
.word 0xf9402ba3
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf940007e
bl _p_110
.word 0xf90027a0
.word 0xf94017b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94027a1
bl _p_335
.word 0xf94017b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_e0:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_get_Connection
SQLite_Net_TableQuery_1_T_REF_get_Connection:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #784]
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

Lme_e1:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_set_Connection_SQLite_Net_SQLiteConnection
SQLite_Net_TableQuery_1_T_REF_set_Connection_SQLite_Net_SQLiteConnection:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #792]
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
.word 0xf9002820
.word 0x91014021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_e2:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_get_Table
SQLite_Net_TableQuery_1_T_REF_get_Table:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #800]
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
.word 0xf9402c00
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

Lme_e3:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_set_Table_SQLite_Net_TableMapping
SQLite_Net_TableQuery_1_T_REF_set_Table_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #808]
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
.word 0xf9002c20
.word 0x91016021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_e4:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_GetEnumerator
SQLite_Net_TableQuery_1_T_REF_GetEnumerator:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #816]
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
.word 0x39418000
.word 0x35000b60
.word 0xf9400fb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #824]
bl _p_338
.word 0xf90033a0
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400000
bl _p_339
.word 0xaa0003ef
.word 0xf94033a0
.word 0xf940001e
bl _p_340
.word 0xf9002fa0
.word 0xf9400fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400000
bl _p_341
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402fa1
.word 0x9100c3a0
.word 0xaa0003e8
.word 0xaa0103e0
.word 0xf940003e
bl _p_343
.word 0xf9400fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400000
bl _p_344
.word 0xd2800501
.word 0xd2800501
bl _p_20
.word 0x9100c3a1
.word 0xf9002ba0
.word 0x91004002
.word 0xaa0203e0
.word 0xf9401ba1
.word 0xf9000041
.word 0xd349fc02
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0x91002000
.word 0xf9401fa1
.word 0xf9000001
.word 0x91002001
.word 0xf94023a0
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x14000034
.word 0xf9400fb1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #824]
bl _p_338
.word 0xf90033a0
.word 0xf9400fb1
.word 0xf941fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400000
bl _p_345
.word 0xaa0003ef
.word 0xf94033a0
.word 0xf940001e
bl _p_346
.word 0xf9002fa0
.word 0xf9400fb1
.word 0xf9422e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400000
bl _p_347
.word 0xaa0003ef
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0x928002f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9002ba0
.word 0xf9400fb1
.word 0xf9426e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9428e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400fb1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_e5:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_System_Collections_IEnumerable_GetEnumerator
SQLite_Net_TableQuery_1_T_REF_System_Collections_IEnumerable_GetEnumerator:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #832]
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
bl _p_348
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_e6:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_Clone_U_REF
SQLite_Net_TableQuery_1_T_REF_Clone_U_REF:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9003faf
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #840]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xf9402bb1
.word 0xf9404631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9406631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400b40
.word 0xf9005fa0
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_336
.word 0xf90063a0
.word 0xf9402bb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_349
.word 0xf90067a0
.word 0xf9402bb1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
bl _p_350
.word 0xd2800f01
.word 0xd2800f01
bl _p_20
.word 0xf9405fa1
.word 0xf94063a2
.word 0xf94067a3
.word 0xf9005ba0
bl _p_351
.word 0xf9402bb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xaa1903e0
.word 0xaa1903e1
.word 0xaa1a03e1
.word 0xf9402741
.word 0xf9002721
.word 0x91012322
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xaa0003f8
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0x39418341
.word 0x39018001
.word 0xaa0003f7
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0x91019341
.word 0x9101c3a2
.word 0xf9400021
.word 0xf9003ba1
.word 0x9101c3a1
.word 0x91019001
.word 0xf9403ba2
.word 0xf9000022
.word 0xaa0003f6
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0x9101b341
.word 0x9101a3a2
.word 0xf9400021
.word 0xf90037a1
.word 0x9101a3a1
.word 0x9101b001
.word 0xf94037a2
.word 0xf9000022
.word 0xaa0003f5
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0xf9400f41
.word 0xf9000c01
.word 0x91006002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xaa0003f4
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0xf9401341
.word 0xf9001001
.word 0x91008002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xaa0003f3
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0xf9401741
.word 0xf9001401
.word 0x9100a002
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xf90043a0
.word 0xf94043a0
.word 0xf94043a2
.word 0xaa1a03e1
.word 0xf9401b41
.word 0xf9001841
.word 0x9100c042
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xf90047a0
.word 0xf94047a0
.word 0xf94047a2
.word 0xaa1a03e1
.word 0xf9401f41
.word 0xf9001c41
.word 0x9100e042
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xf9004ba0
.word 0xf9404ba2
.word 0xf9404ba1
.word 0xaa1a03e0
.word 0xf9402340
.word 0xf9004fa2
.word 0xf90053a1
.word 0xb4000440
.word 0xf9404fa0
.word 0xf90063a0
.word 0xf94053a0
.word 0xf9005fa0
.word 0xaa1a03e0
.word 0xf9402340
.word 0xf90067a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0x3980b410
.word 0xb5000050
bl _p_342

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf94067a1
.word 0xf9005ba0
bl _p_352
.word 0xf9402bb1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf9405fa1
.word 0xf94063a2
.word 0xf9004fa2
.word 0xf90053a1
.word 0xf90057a0
.word 0x14000007
.word 0xf9404fa1
.word 0xf94053a0
.word 0xd2800002
.word 0xf9004fa1
.word 0xf90053a0
.word 0xf90057bf
.word 0xf9404fa0
.word 0xf9005ba0
.word 0xf94053a1
.word 0xf94057a0
.word 0xf9002020
.word 0x91010021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9441a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf9402bb1
.word 0xf9442e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_e7:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_Where_System_Linq_Expressions_Expression_1_System_Func_2_T_REF_bool
SQLite_Net_TableQuery_1_T_REF_Where_System_Linq_Expressions_Expression_1_System_Func_2_T_REF_bool:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xf90017a0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #856]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xd2800019
.word 0xf9401bb1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb500025a
.word 0xf9401bb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2814261
.word 0xd2814261
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9401bb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xd2800241
.word 0xd280025e
.word 0x6b1e001f
.word 0x54000240
.word 0xf9401bb1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28144a1
.word 0xd28144a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9401bb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_353
.word 0xf90037a0
.word 0xf9401bb1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xaa0003f9
.word 0xf9401bb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017a0
.word 0xf90033a0
.word 0xf94017a0
.word 0xf9400000
bl _p_354
.word 0xaa0003ef
.word 0xf94033a0
bl _p_355
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa2
.word 0xaa0203f8
.word 0xaa0203e0
.word 0xf9002ba0
.word 0xaa0203e0
.word 0xaa1903e1
.word 0xaa0203e0
.word 0xf940005e
bl _p_356
.word 0xf9401bb1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401bb1
.word 0xf9422a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_e8:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_Take_int
SQLite_Net_TableQuery_1_T_REF_Take_int:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xf90013b9
.word 0xf90017a0
.word 0xf9001ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #864]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xd2800019
.word 0x9101a3a0
.word 0xd2800000
.word 0xf90037a0
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
.word 0xf9004ba0
.word 0xf94017a0
.word 0xf9400000
bl _p_357
.word 0xaa0003ef
.word 0xf9404ba0
bl _p_355
.word 0xf90047a0
.word 0xf9401fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf90043a0
.word 0xaa0003f9
.word 0xf9401fb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xaa0003e1
.word 0xf9003fa1
.word 0xaa0003e1
.word 0x91019000
.word 0x910163a1
.word 0xf9400000
.word 0xf9002fa0
.word 0x910163a0
.word 0x9101a3a0
.word 0xf9402fa0
.word 0xf90037a0
.word 0x9101a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9403fa1
.word 0xaa0103f8
.word 0x350000e0
.word 0xaa1803e0
.word 0xd29fffe0
.word 0xf2afffe0
.word 0xd29ffff7
.word 0xf2affff7
.word 0x1400000f
.word 0xaa1803e0
.word 0x9101a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_358
.word 0x93407c00
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003f7
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xb98033a1
.word 0xaa1703e0
bl _p_359
.word 0x93407c00
.word 0xf9003ba0
.word 0xf9401fb1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0x910183a0
.word 0xd2800000
.word 0xf90033a0
.word 0x910183a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_73
.word 0x910183a0
.word 0x910143a0
.word 0xf94033a0
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0x910143a0
.word 0x91019300
.word 0xf9402ba1
.word 0xf9000001
.word 0xf9401fb1
.word 0xf9420a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9422e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401fb1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xf94013b9
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0

Lme_e9:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_Delete_System_Linq_Expressions_Expression_1_System_Func_2_T_REF_bool
SQLite_Net_TableQuery_1_T_REF_Delete_System_Linq_Expressions_Expression_1_System_Func_2_T_REF_bool:
.loc 1 1 0
.word 0xa9b17bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xf9001fa0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #872]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xd2800019
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf94023b1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb500025a
.word 0xf94023b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2814261
.word 0xd2814261
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94023b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf90033a0
.word 0xf94023b1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xd2800241
.word 0xd280025e
.word 0x6b1e001f
.word 0x54000240
.word 0xf94023b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28144a1
.word 0xd28144a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94023b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54002440
.word 0x91019000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x34000240
.word 0xf94023b1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28149a1
.word 0xd28149a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94023b1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001f80
.word 0x9101b000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9423631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0x34000240
.word 0xf94023b1
.word 0xf9424e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28154a1
.word 0xd28154a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94023b1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_353
.word 0xf90033a0
.word 0xf94023b1
.word 0xf942b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f9
.word 0xf94023b1
.word 0xf942ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
.word 0xf9402400
.word 0xb4000220
.word 0xf94023b1
.word 0xf942ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401fa0
.word 0xf9402401
.word 0xaa1903e0
bl _p_360
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xaa0003f9
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9433a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90073a0
bl _p_361
.word 0xf94023b1
.word 0xf9438231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xaa0003f8
.word 0xf94023b1
.word 0xf9439a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
.word 0xaa1903e1
.word 0xaa1803e1
.word 0xaa1903e1
.word 0xaa1803e2
bl _p_362
.word 0xf9006fa0
.word 0xf94023b1
.word 0xf943c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xaa0003f7
.word 0xf94023b1
.word 0xf943de31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xf90063a0
.word 0xf9401fa0
bl _p_349
.word 0xf9006ba0
.word 0xf94023b1
.word 0xf9440a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf90067a0
.word 0xf94023b1
.word 0xf9442e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf94067a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #512]
bl _p_30
.word 0xf9005fa0
.word 0xf94023b1
.word 0xf9445a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xf9005ba0
.word 0xaa0003f6
.word 0xf94023b1
.word 0xf9447631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf9004fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #888]
.word 0xf90053a0
.word 0xaa1703e1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf90057a0
.word 0xf94023b1
.word 0xf944b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf94053a1
.word 0xf94057a2
bl _p_30
.word 0xf9004ba0
.word 0xf94023b1
.word 0xf944d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f6
.word 0xf94023b1
.word 0xf944ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0
bl _p_336
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9450a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9003ba0
.word 0xaa1803e0
.word 0xf90047a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_283
.word 0xf9003fa0
.word 0xf94023b1
.word 0xf9455631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba1
.word 0xf9403fa2
.word 0xf94043a3
.word 0xaa0303e0
.word 0xf940007e
bl _p_243
.word 0xf90037a0
.word 0xf94023b1
.word 0xf9458231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_245
.word 0x93407c00
.word 0xf90033a0
.word 0xf94023b1
.word 0xf945aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf945ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf94023b1
.word 0xf945de31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_ea:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_OrderBy_TValue_REF_System_Linq_Expressions_Expression_1_System_Func_2_T_REF_TValue_REF
SQLite_Net_TableQuery_1_T_REF_OrderBy_TValue_REF_System_Linq_Expressions_Expression_1_System_Func_2_T_REF_TValue_REF:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9001faf
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #896]
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
.word 0xf9400ba0
.word 0xf90027a0
.word 0xf9400fa0
.word 0xf9002ba0
.word 0xd2800020
.word 0xf9401fa0
bl _p_364
.word 0xaa0003ef
.word 0xf94027a0
.word 0xf9402ba1
.word 0xd2800022
bl _p_365
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_eb:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_AddOrderBy_TValue_REF_System_Linq_Expressions_Expression_1_System_Func_2_T_REF_TValue_REF_bool
SQLite_Net_TableQuery_1_T_REF_AddOrderBy_TValue_REF_System_Linq_Expressions_Expression_1_System_Func_2_T_REF_TValue_REF_bool:
.loc 1 1 0
.word 0xa9b07bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf90053af
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf9002ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #904]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
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
.word 0xaa1903e0
.word 0xb5000259
.word 0xf9402fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28165a1
.word 0xd28165a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf9005ba0
.word 0xf9402fb1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xd2800241
.word 0xd280025e
.word 0x6b1e001f
.word 0x54000240
.word 0xf9402fb1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28144a1
.word 0xd28144a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402fb1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903f7
.word 0xf9402fb1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf940033e
bl _p_353
.word 0xaa0003f3
.word 0xf9402fb1
.word 0xf941aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303fa
.word 0xeb1f027f
.word 0x54000160
.word 0xf9400260
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #912]
.word 0xeb01001f
.word 0x54000040
.word 0xd280001a
.word 0xaa1a03e0
.word 0xaa1a03f5
.word 0xf9402fb1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb400073a
.word 0xf9402fb1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1503e0
.word 0xf94002a1
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf9005ba0
.word 0xf9402fb1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xd2800141
.word 0xd280015e
.word 0x6b1e001f
.word 0x540004a1
.word 0xf9402fb1
.word 0xf9425e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1503e0
.word 0xf94002be
bl _p_366
.word 0xf9004ba0
.word 0xf9402fb1
.word 0xf9428231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xf9004fa0
.word 0xf9404ba0
.word 0xeb1f001f
.word 0x54000180
.word 0xf9404ba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #920]
.word 0xeb01001f
.word 0x54000040
.word 0xf9004fbf
.word 0xf9404fa0
.word 0xaa0003f6
.word 0xf9402fb1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000024
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf942fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_353
.word 0xf9003ba0
.word 0xf9402fb1
.word 0xf9432231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf9003fa0
.word 0xf9403ba0
.word 0xeb1f001f
.word 0x54000180
.word 0xf9403ba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #920]
.word 0xeb01001f
.word 0x54000040
.word 0xf9003fbf
.word 0xf9403fa0
.word 0xaa0003f6
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9438a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xb40003d6
.word 0xf9402fb1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_367
.word 0xf9005fa0
.word 0xf9402fb1
.word 0xf943c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf9005ba0
.word 0xf9402fb1
.word 0xf943f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xd28004c1
.word 0xd28004de
.word 0x6b1e001f
.word 0x540003c0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9442631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2816821
.word 0xd2816821
bl _p_32
.word 0xaa1903e1
.word 0xaa1903e1
bl _p_63
.word 0xf9005ba0
.word 0xf9402fb1
.word 0xf9445e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402fb1
.word 0xf9448a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400300
bl _p_368
.word 0xaa0003ef
.word 0xaa1803e0
bl _p_355
.word 0xf9005fa0
.word 0xf9402fb1
.word 0xf944ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xf9005ba0
.word 0xaa0003f4
.word 0xf9402fb1
.word 0xf944d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003e1
.word 0xf9402000
.word 0xb50004a0
.word 0xf9402fb1
.word 0xf944f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0x3980b410
.word 0xb5000050
bl _p_342

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9005ba0
bl _p_369
.word 0xf9402fb1
.word 0xf9454231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf9002280
.word 0x91010281
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9459631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xf9402280
.word 0xf9005fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #928]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9007fa0
bl _p_370
.word 0xf9402fb1
.word 0xf945d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa0
.word 0xf90043a0
.word 0xf94043a0
.word 0xf90063a0
.word 0xf94043a0
.word 0xf9006ba0
.word 0xaa1803e0
.word 0xaa1803e0
bl _p_349
.word 0xf90077a0
.word 0xf9402fb1
.word 0xf9460a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_371
.word 0xf9007ba0
.word 0xf9402fb1
.word 0xf9462e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf90073a0
.word 0xf9402fb1
.word 0xf9465631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a1
.word 0xf94077a2
.word 0xaa0203e0
.word 0xf940005e
bl _p_372
.word 0xf9006fa0
.word 0xf9402fb1
.word 0xf9467e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf946a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xf9406ba2
.word 0xaa0203e0
.word 0xf940005e
bl _p_373
.word 0xf9402fb1
.word 0xf946c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf90047a0
.word 0xf94047a0
.word 0xf9005ba0
.word 0xf94047a2
.word 0x394143a1
.word 0xaa0203e0
.word 0xf940005e
bl _p_374
.word 0xf9402fb1
.word 0xf946fa31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9405ba1
.word 0xf9405fa2
.word 0xaa0203e0
.word 0xf940005e
bl _p_375
.word 0xf9402fb1
.word 0xf9473631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9474631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9476a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xf9402fb1
.word 0xf9477e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d07bfd
.word 0xd65f03c0

Lme_ec:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_AddWhere_System_Linq_Expressions_Expression
SQLite_Net_TableQuery_1_T_REF_AddWhere_System_Linq_Expressions_Expression:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #936]
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
.word 0xb500025a
.word 0xf94013b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2816f21
.word 0xd2816f21
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94013b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001000
.word 0x91019000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x350002c0
.word 0xf94013b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000d60
.word 0x9101b000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x340002c0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2817061
.word 0xd2817061
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94013b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9402400
.word 0xb5000300
.word 0xf94013b1
.word 0xf941ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xaa1a03e1
.word 0xf900241a
.word 0x91012000
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94013b1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000023
.word 0xf94013b1
.word 0xf9422a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf90027a0
.word 0xf9400fa0
.word 0xf9402400
.word 0xaa1a03e1
.word 0xaa1a03e1
bl _p_360
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94027a1
.word 0xf9002420
.word 0x91012021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94013b1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_ed:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_GenerateCommand_string
SQLite_Net_TableQuery_1_T_REF_GenerateCommand_string:
.loc 1 1 0
.word 0xa9b07bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #944]
.word 0xf90033b0
.word 0xf9400a11
.word 0xf90037b1
.word 0xd2800019
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf94033b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb5000240
.word 0xf94033b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2817aa1
.word 0xd2817aa1
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94033b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400c00
.word 0xb4000320
.word 0xf94033b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401400
.word 0xb4000240
.word 0xf94033b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2817e21
.word 0xd2817e21
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xf9007fa0
.word 0xaa1503e0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #952]
.word 0xaa1503e0
.word 0xd2800001
.word 0xf94002a3
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407fa0
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf9007ba0
.word 0xaa1403e0
.word 0xd2800020
.word 0xf9402fa2
.word 0xaa1403e0
.word 0xd2800021
.word 0xf9400283
.word 0xf9407870
.word 0xd63f0200
.word 0xf9407ba0
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xf90077a0
.word 0xaa1303e0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #960]
.word 0xaa1303e0
.word 0xd2800041
.word 0xf9400263
.word 0xf9407870
.word 0xd63f0200
.word 0xf94077a0
.word 0xaa0003fa
.word 0xaa1a03e0
.word 0xf90067a0
.word 0xaa1a03e0
.word 0xf9006fa0
.word 0xd2800060
.word 0xf9402ba0
bl _p_349
.word 0xf90073a0
.word 0xf94033b1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf9006ba0
.word 0xf94033b1
.word 0xf9426e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba2
.word 0xf9406fa3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94067a0
.word 0xf9003fa0
.word 0xf9403fa0
.word 0xf90063a0
.word 0xf9403fa3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #512]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94063a0
bl _p_47
.word 0xf9005fa0
.word 0xf94033b1
.word 0xf942de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xaa0003f9
.word 0xf94033b1
.word 0xf942f631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9005ba0
bl _p_361
.word 0xf94033b1
.word 0xf9433e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f8
.word 0xf94033b1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402400
.word 0xb4000620
.word 0xf94033b1
.word 0xf9437231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402ba1
.word 0xf9402421
.word 0xaa1803e2
.word 0xaa1803e2
bl _p_362
.word 0xf9006ba0
.word 0xf94033b1
.word 0xf9439e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf90067a0
.word 0xaa0003f7
.word 0xf94033b1
.word 0xf943ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #888]
.word 0xf9005fa0
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf90063a0
.word 0xf94033b1
.word 0xf943f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa1
.word 0xf94063a2
.word 0xaa1903e0
bl _p_30
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9441a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9444231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402000
.word 0xb40018c0
.word 0xf94033b1
.word 0xf9445e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402000
.word 0xf9005fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9405fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_376
.word 0x93407c00
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf944aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xd2800001
.word 0x6b1f001f
.word 0x5400156d
.word 0xf94033b1
.word 0xf944ca31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #968]
.word 0xf9005fa0
.word 0xf9402ba0
.word 0xf9402000
.word 0xf9005ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_377
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_378
.word 0xf9405ba2
.word 0xf9405fa3
.word 0x91002001
.word 0xf9400400
.word 0xf90043a0
.word 0xf94043a1
.word 0xf94043a0
.word 0xf90047a3
.word 0xf9004ba2
.word 0xf9004fa1
.word 0xb5000ae0
.word 0xf94047a0
.word 0xf90063a0
.word 0xf9404ba0
.word 0xf9005fa0
.word 0xf9404fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_377
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_378
.word 0xf9400000
.word 0xf90073a0
.word 0xeb1f001f
.word 0x10000011
.word 0x54002d80

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #976]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf94073a0
.word 0xf9001020
.word 0xf9006fa1
.word 0x91008021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402ba0
.word 0xf9400000
bl _p_379
.word 0xaa0003e1
.word 0xf9406fa0
.word 0xf9001401
.word 0xf9006ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_380
.word 0xaa0003e1
.word 0xf9406ba0
.word 0xf9002001

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #984]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf90053a0
.word 0xf94053a0
.word 0xf9005ba0
.word 0xf94053a0
.word 0xf90067a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_377
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_378
.word 0xaa0003e3
.word 0xf9405ba0
.word 0xf9405fa1
.word 0xf94063a2
.word 0xf94067a4
.word 0x91002063
.word 0xf9000064
.word 0xf90047a2
.word 0xf9004ba1
.word 0xf9004fa0
.word 0xf94047a0
.word 0xf90067a0
.word 0xf9404ba0
.word 0xf9404fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #992]
bl _p_381
.word 0xf9006fa0
.word 0xf94033b1
.word 0xf946c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #432]
bl _p_22
.word 0xf9006ba0
.word 0xf94033b1
.word 0xf946ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a0
.word 0xf9406ba1
bl _p_23
.word 0xf90063a0
.word 0xf94033b1
.word 0xf9470a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf9005fa0
.word 0xaa0003f6
.word 0xf94033b1
.word 0xf9472631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa2
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1000]
.word 0xaa0203e0
.word 0xaa1903e0
bl _p_30
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9475a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9478231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001ca0
.word 0x91019000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf947be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0x34000580
.word 0xf94033b1
.word 0xf947d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1008]
.word 0xf90063a0
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001960
.word 0x91019000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_61
.word 0x93407c00
.word 0xf9005fa0
.word 0xf94033b1
.word 0xf9482631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf9405fa0
.word 0xf94063a1
.word 0xb9001040
.word 0xaa1903e0
bl _p_62
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9486a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9489231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001420
.word 0x9101b000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf948ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0x34000ac0
.word 0xf94033b1
.word 0xf948e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001180
.word 0x91019000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_60
.word 0x53001c00
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9492231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0x35000240
.word 0xf94033b1
.word 0xf9493a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1016]
.word 0xaa1903e0
bl _p_49
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9496631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9498e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1024]
.word 0xf90063a0
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000ba0
.word 0x9101b000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_61
.word 0x93407c00
.word 0xf9005fa0
.word 0xf94033b1
.word 0xf949de31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf9405fa0
.word 0xf94063a1
.word 0xb9001040
.word 0xaa1903e0
bl _p_62
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf94a2231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f9
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf94a4a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_336
.word 0xf90063a0
.word 0xf94033b1
.word 0xf94a6631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xaa1803e0
.word 0xf940031e
bl _p_283
.word 0xf9005fa0
.word 0xf94033b1
.word 0xf94aa631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa2
.word 0xf94063a3
.word 0xaa0303e0
.word 0xaa1903e1
.word 0xf940007e
bl _p_243
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf94ad231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf94af231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf94033b1
.word 0xf94b0631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d07bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_ee:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_CompileExpr_System_Linq_Expressions_Expression_System_Collections_Generic_List_1_object
SQLite_Net_TableQuery_1_T_REF_CompileExpr_System_Linq_Expressions_Expression_System_Collections_Generic_List_1_object:
.loc 1 1 0
.word 0xd2808a10
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xf90033a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1032]
.word 0xf90037b0
.word 0xf9400a11
.word 0xf9003bb1
.word 0xf90043bf
.word 0xf90047bf
.word 0xf9004bbf
.word 0xf9004fbf
.word 0xf90053bf
.word 0xf90057bf
.word 0xf9005bbf
.word 0xd280001a
.word 0xd2800015
.word 0xd2800016
.word 0xd2800018
.word 0xd2800017
.word 0xf9005fbf
.word 0xf90063bf
.word 0xf90067bf
.word 0xf9006bbf
.word 0xf9006fbf
.word 0xf90073bf
.word 0xf90077bf
.word 0xf9007bbf
.word 0xf9007fbf
.word 0xf90083bf
.word 0xf90087bf
.word 0xf9008bbf
.word 0xf9008fbf
.word 0xf94037b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb50002c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28192e1
.word 0xd28192e1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9415a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf90093a0
.word 0xf94093a0
.word 0xf90097a0
.word 0xf94093a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf94093a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1040]
.word 0xeb01001f
.word 0x54000040
.word 0xf90097bf
.word 0xf94097a0
.word 0xb4003320
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf941ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf901bfa0
.word 0xf941bfa0
.word 0xb4000180
.word 0xf941bfa0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1040]
.word 0xeb01001f
.word 0x10000011
.word 0x5401e661
.word 0xf941bfa0
.word 0xf90043a0
.word 0xf94037b1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9020ba0
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_382
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf9424a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420ba0
.word 0xf9420fa1
.word 0xf94033a2
bl _p_362
.word 0xf90207a0
.word 0xf94037b1
.word 0xf9426e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94207a0
.word 0xf90047a0
.word 0xf94037b1
.word 0xf9428631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf901ffa0
.word 0xf94043a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_383
.word 0xf90203a0
.word 0xf94037b1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0
.word 0xf94203a1
.word 0xf94033a2
bl _p_362
.word 0xf901fba0
.word 0xf94037b1
.word 0xf942d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf9004ba0
.word 0xf94037b1
.word 0xf942ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9431231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #528]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9433e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x34000580
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9436631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_384
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9438a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xb5000320
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf943b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94043a1
.word 0xf9404ba2
bl _p_385
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf943d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf9004fa0
.word 0xf94037b1
.word 0xf943ee31
.word 0xb4000051
.word 0xd63f0220
.word 0x140000dc
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9441231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9443631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #528]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9446231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x34000580
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9448a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_384
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf944ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xb5000320
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf944d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94043a1
.word 0xf94047a2
bl _p_385
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf944fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf9004fa0
.word 0xf94037b1
.word 0xf9451231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000093
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9453631
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000e1
bl _p_16
.word 0xf901c3a0
.word 0xf941c3a0
.word 0xf90227a0
.word 0xf941c3a3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94227a0
.word 0xf901c7a0
.word 0xf941c7a0
.word 0xf9021ba0
.word 0xf941c7a0
.word 0xf90223a0
.word 0xd2800020
.word 0xf94047a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf9021fa0
.word 0xf94037b1
.word 0xf945c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9421fa2
.word 0xf94223a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9421ba0
.word 0xf901cba0
.word 0xf941cba0
.word 0xf90217a0
.word 0xf941cba3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #896]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf901cfa0
.word 0xf941cfa0
.word 0xf9020ba0
.word 0xf941cfa0
.word 0xf90213a0
.word 0xd2800060
.word 0xf9402ba0
.word 0xf94043a1
bl _p_386
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf9465231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa2
.word 0xf94213a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9420ba0
.word 0xf901d3a0
.word 0xf941d3a0
.word 0xf90207a0
.word 0xf941d3a3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #896]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94207a0
.word 0xf901d7a0
.word 0xf941d7a0
.word 0xf901fba0
.word 0xf941d7a0
.word 0xf90203a0
.word 0xd28000a0
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf946e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xf94203a3
.word 0xaa0303e0
.word 0xd28000a1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941fba0
.word 0xf901dba0
.word 0xf941dba0
.word 0xf901f7a0
.word 0xf941dba3
.word 0xd28000c0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
.word 0xaa0303e0
.word 0xd28000c1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941f7a0
bl _p_47
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9475631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf9004fa0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9477e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_387
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf901f7a0
bl _p_388
.word 0xf94037b1
.word 0xf947ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf901dfa0
.word 0xf941dfa0
.word 0xf901f3a0
.word 0xf941dfa2
.word 0xf9404fa1
.word 0xaa0203e0
.word 0xf940005e
bl _p_389
.word 0xf94037b1
.word 0xf947e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9480231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x14000da3
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9482a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9485631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xd2800441
.word 0xd280045e
.word 0x6b1e001f
.word 0x54001a01
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9488a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf901aba0
.word 0xf941aba0
.word 0xb4000180
.word 0xf941aba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #912]
.word 0xeb01001f
.word 0x10000011
.word 0x5401b061
.word 0xf941aba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_366
.word 0xf901fba0
.word 0xf94037b1
.word 0xf948ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf90053a0
.word 0xf94037b1
.word 0xf9490231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94053a1
.word 0xf94033a2
bl _p_362
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9492631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf90057a0
.word 0xf94037b1
.word 0xf9493e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_384
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9496231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf9005ba0
.word 0xf94037b1
.word 0xf9497a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf901afa0
.word 0xf941afa0
.word 0xf901b3a0
.word 0xf941afa0
.word 0xeb1f001f
.word 0x54000180
.word 0xf941afa0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2168]
.word 0xeb01001f
.word 0x54000040
.word 0xf901b3bf
.word 0xf941b3a0
.word 0xb40004c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf949ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x5401a681
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2168]
.word 0xeb02003f
.word 0x10000011
.word 0x5401a581
.word 0x91004001
.word 0x39404000
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xf901f3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2488]
.word 0xd2800221
.word 0xd2800221
bl _p_20
.word 0xf941f3a1
.word 0x39004001
.word 0xf9005ba0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94a7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_387
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9020ba0
bl _p_388
.word 0xf94037b1
.word 0xf94aae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420ba0
.word 0xf901b7a0
.word 0xf941b7a0
.word 0xf901f7a0
.word 0xf941b7a0
.word 0xf901ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1056]
.word 0xf90203a0
.word 0xf94057a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf90207a0
.word 0xf94037b1
.word 0xf94afa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a0
.word 0xf94207a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
bl _p_30
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94b2631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xf941ffa2
.word 0xaa0203e0
.word 0xf940005e
bl _p_389
.word 0xf94037b1
.word 0xf94b4a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf901bba0
.word 0xf941bba0
.word 0xf901f3a0
.word 0xf941bba2
.word 0xf9405ba1
.word 0xaa0203e0
.word 0xf940005e
bl _p_390
.word 0xf94037b1
.word 0xf94b7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94b9e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x14000cbc
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94bc631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf94bf231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xd28000c1
.word 0xd28000de
.word 0x6b1e001f
.word 0x5400e341
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94c2631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf90107a0
.word 0xf94107a0
.word 0xb4000180
.word 0xf94107a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1064]
.word 0xeb01001f
.word 0x10000011
.word 0x54019381
.word 0xf94107a0
.word 0xaa0003fa
.word 0xf94037b1
.word 0xf94c7a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_391
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94c9e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_392
.word 0x93407c00
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf94cc631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_393
.word 0xf941f7a1
bl _p_16
.word 0xaa0003f5
.word 0xf94037b1
.word 0xf94cee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e1
.word 0xaa0103e0
.word 0xf940003e
bl _p_394
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf94d1231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xb5000180
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94d3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800019
.word 0x1400001f
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94d6631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf901f7a0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_394
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94d9231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf941fba1
.word 0xf94033a2
bl _p_362
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf94db631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f9
.word 0xaa1903e0
.word 0xaa1903f6
.word 0xf94037b1
.word 0xf94dd631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800017
.word 0xf94037b1
.word 0xf94dea31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000037
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94e0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1703e0
.word 0xf9402ba0
.word 0xf901f7a0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_391
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf94e4231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xaa1703e0
.word 0xaa0203e0
.word 0xaa1703e1
.word 0xf940005e
bl _p_395
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94e6e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf941fba1
.word 0xf94033a2
bl _p_362
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf94e9231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a2
.word 0xaa1503e0
.word 0xaa1703e1
.word 0xf94002a3
.word 0xf9407870
.word 0xd63f0200
.word 0xf94037b1
.word 0xf94eba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x110006e0
.word 0xaa0003f7
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94ee631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xaa1503e0
.word 0xb9801aa0
.word 0x6b0002ff
.word 0x54fff7cb
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94f1a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1816]
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf94f3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94f5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf94f8631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1072]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf94fb231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x340011e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94fda31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xb9801aa0
.word 0xd2800041
.word 0xd280005e
.word 0x6b1e001f
.word 0x54001021
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9501231
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf90197a0
.word 0xf94197a0
.word 0xf90217a0
.word 0xf94197a3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf9019ba0
.word 0xf9419ba0
.word 0xf9020ba0
.word 0xf9419ba0
.word 0xf90213a0
.word 0xd2800020
.word 0xaa1503e0
.word 0xd2800000
.word 0xb9801aa0
.word 0xeb1f001f
.word 0x10000011
.word 0x540172c9
.word 0xf94012a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf950b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa2
.word 0xf94213a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9420ba0
.word 0xf9019fa0
.word 0xf9419fa0
.word 0xf90207a0
.word 0xf9419fa3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1080]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94207a0
.word 0xf901a3a0
.word 0xf941a3a0
.word 0xf901fba0
.word 0xf941a3a0
.word 0xf90203a0
.word 0xd2800060
.word 0xaa1503e0
.word 0xd2800020
.word 0xb9801aa0
.word 0xd280003e
.word 0xeb1e001f
.word 0x10000011
.word 0x54016d49
.word 0xf94016a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9516631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xf94203a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941fba0
.word 0xf901a7a0
.word 0xf941a7a0
.word 0xf901f7a0
.word 0xf941a7a3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941f7a0
bl _p_47
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf951d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf951ee31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000573
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9521231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9523631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9525e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1088]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9528a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x340011e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf952b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xb9801aa0
.word 0xd2800041
.word 0xd280005e
.word 0x6b1e001f
.word 0x54001021
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf952ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf90183a0
.word 0xf94183a0
.word 0xf90217a0
.word 0xf94183a3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf90187a0
.word 0xf94187a0
.word 0xf9020ba0
.word 0xf94187a0
.word 0xf90213a0
.word 0xd2800020
.word 0xaa1503e0
.word 0xd2800020
.word 0xb9801aa0
.word 0xd280003e
.word 0xeb1e001f
.word 0x10000011
.word 0x54015be9
.word 0xf94016a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf9539231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa2
.word 0xf94213a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9420ba0
.word 0xf9018ba0
.word 0xf9418ba0
.word 0xf90207a0
.word 0xf9418ba3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1096]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94207a0
.word 0xf9018fa0
.word 0xf9418fa0
.word 0xf901fba0
.word 0xf9418fa0
.word 0xf90203a0
.word 0xd2800060
.word 0xaa1503e0
.word 0xd2800000
.word 0xb9801aa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54015689
.word 0xf94012a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9543e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xf94203a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941fba0
.word 0xf90193a0
.word 0xf94193a0
.word 0xf901f7a0
.word 0xf94193a3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941f7a0
bl _p_47
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf954ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf954c631
.word 0xb4000051
.word 0xd63f0220
.word 0x140004bd
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf954ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9550e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9553631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1088]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9556231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x340026a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9558a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xb9801aa0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x540024e1
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf955c231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_394
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf955e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xb4001360
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9560e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_394
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9563231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404030
.word 0xd63f0200
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9565a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1104]
.word 0xeb01001f
.word 0x54000f41
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9569231
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf9016fa0
.word 0xf9416fa0
.word 0xf90217a0
.word 0xf9416fa3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf90173a0
.word 0xf94173a0
.word 0xf9020ba0
.word 0xf94173a0
.word 0xf90213a0
.word 0xd2800020
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_363
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf9571e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa2
.word 0xf94213a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9420ba0
.word 0xf90177a0
.word 0xf94177a0
.word 0xf90207a0
.word 0xf94177a3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1112]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94207a0
.word 0xf9017ba0
.word 0xf9417ba0
.word 0xf901fba0
.word 0xf9417ba0
.word 0xf90203a0
.word 0xd2800060
.word 0xaa1503e0
.word 0xd2800000
.word 0xb9801aa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54013a29
.word 0xf94012a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf957ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xf94203a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941fba0
.word 0xf9017fa0
.word 0xf9417fa0
.word 0xf901f7a0
.word 0xf9417fa3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1120]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941f7a0
bl _p_47
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9583a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf9585231
.word 0xb4000051
.word 0xd63f0220
.word 0x140003da
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9587631
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf9015ba0
.word 0xf9415ba0
.word 0xf90217a0
.word 0xf9415ba3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf9015fa0
.word 0xf9415fa0
.word 0xf9020ba0
.word 0xf9415fa0
.word 0xf90213a0
.word 0xd2800020
.word 0xaa1503e0
.word 0xd2800000
.word 0xb9801aa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54012fa9
.word 0xf94012a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf9591a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa2
.word 0xf94213a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9420ba0
.word 0xf90163a0
.word 0xf94163a0
.word 0xf90207a0
.word 0xf94163a3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1096]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94207a0
.word 0xf90167a0
.word 0xf94167a0
.word 0xf901fba0
.word 0xf94167a0
.word 0xf90203a0
.word 0xd2800060
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_363
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf959ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xf94203a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941fba0
.word 0xf9016ba0
.word 0xf9416ba0
.word 0xf901f7a0
.word 0xf9416ba3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941f7a0
bl _p_47
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf95a1e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf95a3631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000361
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf95a5a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf901fba0
.word 0xf94037b1
.word 0xf95a7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf95aa631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1128]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf95ad231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x34001100
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf95afa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xb9801aa0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000f41
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf95b3231
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf90147a0
.word 0xf94147a0
.word 0xf90217a0
.word 0xf94147a3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf9014ba0
.word 0xf9414ba0
.word 0xf9020ba0
.word 0xf9414ba0
.word 0xf90213a0
.word 0xd2800020
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_363
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf95bbe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa2
.word 0xf94213a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9420ba0
.word 0xf9014fa0
.word 0xf9414fa0
.word 0xf90207a0
.word 0xf9414fa3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1136]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94207a0
.word 0xf90153a0
.word 0xf94153a0
.word 0xf901fba0
.word 0xf94153a0
.word 0xf90203a0
.word 0xd2800060
.word 0xaa1503e0
.word 0xd2800000
.word 0xb9801aa0
.word 0xeb1f001f
.word 0x10000011
.word 0x54011529
.word 0xf94012a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf95c6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xf94203a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941fba0
.word 0xf90157a0
.word 0xf94157a0
.word 0xf901f7a0
.word 0xf94157a3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1120]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941f7a0
bl _p_47
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf95cda31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf95cf231
.word 0xb4000051
.word 0xd63f0220
.word 0x140002b2
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf95d1631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf901fba0
.word 0xf94037b1
.word 0xf95d3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf95d6231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1144]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf95d8e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x34001100
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf95db631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xb9801aa0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000f41
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf95dee31
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf90133a0
.word 0xf94133a0
.word 0xf90217a0
.word 0xf94133a3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf90137a0
.word 0xf94137a0
.word 0xf9020ba0
.word 0xf94137a0
.word 0xf90213a0
.word 0xd2800020
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_363
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf95e7a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa2
.word 0xf94213a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9420ba0
.word 0xf9013ba0
.word 0xf9413ba0
.word 0xf90207a0
.word 0xf9413ba3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1112]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94207a0
.word 0xf9013fa0
.word 0xf9413fa0
.word 0xf901fba0
.word 0xf9413fa0
.word 0xf90203a0
.word 0xd2800060
.word 0xaa1503e0
.word 0xd2800000
.word 0xb9801aa0
.word 0xeb1f001f
.word 0x10000011
.word 0x5400ff49
.word 0xf94012a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf95f2631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xf94203a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941fba0
.word 0xf90143a0
.word 0xf94143a0
.word 0xf901f7a0
.word 0xf94143a3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1152]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941f7a0
bl _p_47
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf95f9631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf95fae31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000203
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf95fd231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf901fba0
.word 0xf94037b1
.word 0xf95ff631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9601e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1160]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9604a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x34001100
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9607231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xb9801aa0
.word 0xd2800021
.word 0xd280003e
.word 0x6b1e001f
.word 0x54000f41
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf960aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf9011fa0
.word 0xf9411fa0
.word 0xf90217a0
.word 0xf9411fa3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf90123a0
.word 0xf94123a0
.word 0xf9020ba0
.word 0xf94123a0
.word 0xf90213a0
.word 0xd2800020
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_363
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf9613631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa2
.word 0xf94213a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9420ba0
.word 0xf90127a0
.word 0xf94127a0
.word 0xf90207a0
.word 0xf94127a3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1168]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94207a0
.word 0xf9012ba0
.word 0xf9412ba0
.word 0xf901fba0
.word 0xf9412ba0
.word 0xf90203a0
.word 0xd2800060
.word 0xaa1503e0
.word 0xd2800000
.word 0xb9801aa0
.word 0xeb1f001f
.word 0x10000011
.word 0x5400e969
.word 0xf94012a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf961e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa2
.word 0xf94203a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941fba0
.word 0xf9012fa0
.word 0xf9412fa0
.word 0xf901f7a0
.word 0xf9412fa3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1152]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941f7a0
bl _p_47
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9625231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf9626a31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000154
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9628e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf901fba0
.word 0xf94037b1
.word 0xf962b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf962da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1176]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9630631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x34000500
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9632e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1184]
.word 0xf901f7a0
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_363
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9636231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf941fba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1152]
bl _p_30
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9638e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf963a631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000105
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf963ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf901fba0
.word 0xf94037b1
.word 0xf963ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9641631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1192]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9644231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x34000500
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9646a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1200]
.word 0xf901f7a0
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_363
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9649e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf941fba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1152]
bl _p_30
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf964ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf964e231
.word 0xb4000051
.word 0xd63f0220
.word 0x140000b6
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9650631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf90203a0
.word 0xf94037b1
.word 0xf9652a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9655231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_397
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9657631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1048]
.word 0xf901f7a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #368]
.word 0xf901f3a0
.word 0xaa1503e0
.word 0xf9402ba0
.word 0xf9400000
bl _p_398
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_399
.word 0xf941f3a2
.word 0xf941f7a3
.word 0xf941fba4
.word 0x91004001
.word 0xf9400800
.word 0xf9010ba0
.word 0xf9410ba1
.word 0xf9410ba0
.word 0xaa0403f9
.word 0xaa0303f3
.word 0xaa0203f4
.word 0xf9010fb5
.word 0xf90113a1
.word 0xb50007e0
.word 0xaa1903e0
.word 0xaa1303e0
.word 0xaa1403e0
.word 0xf9410fa0
.word 0xf901f7a0
.word 0xf94113a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_398
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_399
.word 0xf9400000
.word 0xf90203a0
.word 0xeb1f001f
.word 0x10000011
.word 0x5400c4c0
.word 0xf9402ba0
.word 0xf9400000
bl _p_400
bl _p_401
.word 0xf90207a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_402
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xf94203a1
.word 0xf94207a2
.word 0xf901ffa0
bl _p_403
.word 0xf94037b1
.word 0xf9669231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0
.word 0xf9011ba0
.word 0xf9411ba0
.word 0xf901f3a0
.word 0xf9411ba0
.word 0xf901fba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_398
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_399
.word 0xaa0003e2
.word 0xf941f3a0
.word 0xf941f7a1
.word 0xf941fba3
.word 0x91004042
.word 0xf9000043
.word 0xf9010fa1
.word 0xf90113a0
.word 0xaa1903e0
.word 0xaa1303e0
.word 0xaa1403e0
.word 0xf9410fa0
.word 0xf90203a0
.word 0xf94113a0
.word 0xf90207a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_404
.word 0xaa0003ef
.word 0xf94203a0
.word 0xf94207a1
bl _p_405
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9673a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #432]
bl _p_22
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9676231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xaa1403e0
bl _p_23
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf9678231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #1216]
.word 0xaa1903e0
.word 0xaa1303e1
bl _p_52
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf967b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f8
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf967da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_387
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf901f7a0
bl _p_388
.word 0xf94037b1
.word 0xf9680a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf90117a0
.word 0xf94117a0
.word 0xf901f3a0
.word 0xf94117a2
.word 0xaa1803e0
.word 0xaa0203e0
.word 0xaa1803e1
.word 0xf940005e
bl _p_389
.word 0xf94037b1
.word 0xf9684231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9686231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x1400058b
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9688a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf968b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xd2800121
.word 0xd280013e
.word 0x6b1e001f
.word 0x54000f21
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf968ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf900fba0
.word 0xf940fba0
.word 0xb4000180
.word 0xf940fba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1208]
.word 0xeb01001f
.word 0x10000011
.word 0x5400ad61
.word 0xf940fba0
.word 0xf9005fa0
.word 0xf94037b1
.word 0xf9693e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9020ba0
.word 0xf9405fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_406
.word 0xf90207a0
.word 0xf94037b1
.word 0xf9696a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf94207a1
.word 0xf9420ba2
.word 0xaa0203e0
.word 0xf940005e
bl _p_282
.word 0xf94037b1
.word 0xf969a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf969b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_387
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90203a0
bl _p_388
.word 0xf94037b1
.word 0xf969e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a0
.word 0xf900ffa0
.word 0xf940ffa0
.word 0xf901ffa0
.word 0xf940ffa2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #528]
.word 0xaa0203e0
.word 0xf940005e
bl _p_389
.word 0xf94037b1
.word 0xf96a2231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0
.word 0xf90103a0
.word 0xf94103a0
.word 0xf901f3a0
.word 0xf94103a0
.word 0xf901fba0
.word 0xf9405fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_406
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf96a5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a1
.word 0xf941fba2
.word 0xaa0203e0
.word 0xf940005e
bl _p_390
.word 0xf94037b1
.word 0xf96a8231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf96aa231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x140004fb
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf96aca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf96af631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xd2800141
.word 0xd280015e
.word 0x6b1e001f
.word 0x54001661
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf96b2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf900efa0
.word 0xf940efa0
.word 0xb4000180
.word 0xf940efa0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #912]
.word 0xeb01001f
.word 0x10000011
.word 0x54009b61
.word 0xf940efa0
.word 0xf90063a0
.word 0xf94037b1
.word 0xf96b7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404030
.word 0xd63f0200
.word 0xf9021ba0
.word 0xf94037b1
.word 0xf96ba631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9421ba0
.word 0xf90067a0
.word 0xf94037b1
.word 0xf96bbe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90213a0
.word 0xf94063a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_366
.word 0xf90217a0
.word 0xf94037b1
.word 0xf96bea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94213a0
.word 0xf94217a1
.word 0xf94033a2
bl _p_362
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf96c0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420fa0
.word 0xf9006ba0
.word 0xf94037b1
.word 0xf96c2631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_387
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9020ba0
bl _p_388
.word 0xf94037b1
.word 0xf96c5631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420ba0
.word 0xf900f3a0
.word 0xf940f3a0
.word 0xf901ffa0
.word 0xf940f3a0
.word 0xf90207a0
.word 0xf9406ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf90203a0
.word 0xf94037b1
.word 0xf96c9231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a1
.word 0xf94207a2
.word 0xaa0203e0
.word 0xf940005e
bl _p_389
.word 0xf94037b1
.word 0xf96cb631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0
.word 0xf900f7a0
.word 0xf940f7a0
.word 0xf901fba0
.word 0xf940f7a0
.word 0xf901f7a0
.word 0xf9406ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_384
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf96cf231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf941f7a1
.word 0xf941fba2
.word 0xaa0203f9
.word 0xaa0103f3
.word 0xb50000c0
.word 0xaa1903e0
.word 0xaa1303e0
.word 0xd2800000
.word 0xd2800014
.word 0x14000019
.word 0xaa1903e0
.word 0xaa1303e0
.word 0xf9402ba0
.word 0xf901f7a0
.word 0xf9406ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_384
.word 0xf901fba0
.word 0xf94037b1
.word 0xf96d5231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf941fba1
.word 0xf94067a2
bl _p_407
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf96d7631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xaa0003f4
.word 0xaa1903e0
.word 0xaa1303e0
.word 0xaa1403e0
.word 0xaa1303e0
.word 0xaa1403e1
.word 0xf940027e
bl _p_390
.word 0xf94037b1
.word 0xf96daa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf96dca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x14000431
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf96df231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf96e1e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xd28002e1
.word 0xd28002fe
.word 0x6b1e001f
.word 0x54007d01
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf96e5231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9009ba0
.word 0xf9409ba0
.word 0xb4000180
.word 0xf9409ba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #920]
.word 0xeb01001f
.word 0x10000011
.word 0x54008221
.word 0xf9409ba0
.word 0xf9006fa0
.word 0xf94037b1
.word 0xf96ea631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_367
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf96eca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xb40011e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf96ef231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_367
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf96f1631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf96f4231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xd28004c1
.word 0xd28004de
.word 0x6b1e001f
.word 0x54000dc1
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf96f7631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
bl _p_349
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf96f9231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_371
.word 0xf90213a0
.word 0xf94037b1
.word 0xf96fb631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94213a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf9020ba0
.word 0xf94037b1
.word 0xf96fde31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9420ba1
.word 0xf9420fa2
.word 0xaa0203e0
.word 0xf940005e
bl _p_372
.word 0xf90207a0
.word 0xf94037b1
.word 0xf9700631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94207a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf90203a0
.word 0xf94037b1
.word 0xf9702a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a0
.word 0xf9007ba0
.word 0xf94037b1
.word 0xf9704231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_387
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf901ffa0
bl _p_388
.word 0xf94037b1
.word 0xf9707231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0
.word 0xf900eba0
.word 0xf940eba0
.word 0xf901f3a0
.word 0xf940eba0
.word 0xf901fba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #512]
.word 0xf9407ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #512]
bl _p_30
.word 0xf901f7a0
.word 0xf94037b1
.word 0xf970be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a1
.word 0xf941fba2
.word 0xaa0203e0
.word 0xf940005e
bl _p_389
.word 0xf94037b1
.word 0xf970e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9710231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x14000363
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9712a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf90073bf
.word 0xf94037b1
.word 0xf9713e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_367
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9716231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xb4001180
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9718a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf901ffa0
.word 0xf9406fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_367
.word 0xf90203a0
.word 0xf94037b1
.word 0xf971b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0
.word 0xf94203a1
.word 0xf94033a2
bl _p_362
.word 0xf901fba0
.word 0xf94037b1
.word 0xf971da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf900dfa0
.word 0xf940dfa0
.word 0xf901f7a0
.word 0xf940dfa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_384
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9720e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf941f7a1
.word 0xaa0103f9
.word 0xb50001e0
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd281b8e1
.word 0xd281b8e1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xaa1903e0
.word 0xf900e3b9
.word 0xf940e3a0
.word 0xf901f7a0
.word 0xf940e3a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_363
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9728a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #528]
bl _p_332
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf972b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf941f7a1
.word 0xaa0103f9
.word 0x34000540
.word 0xaa1903e0
.word 0xf94033a0
.word 0xf900e7a0
.word 0xf940e7a0
.word 0xf901f7a0
.word 0xf940e7a0
.word 0xf901ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf941ffa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_408
.word 0x93407c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9732231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0x51000400
.word 0xf901f3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf941f3a1
.word 0xf941f7a2
.word 0xaa0203e0
.word 0xf940005e
bl _p_409
.word 0xf94037b1
.word 0xf9736a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf940033e
bl _p_384
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9738e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf90073a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf973b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400801
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3096]
.word 0x928011f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90203a0
.word 0xf94037b1
.word 0xf973f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf901f7a0
.word 0xf9402fa0
.word 0xf901fba0
.word 0xf9406fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_371
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9742a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a1
.word 0xf941fba2
.word 0xf941ffa3
.word 0xf94203a4
.word 0xaa0403e0
.word 0xf9400084

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1216]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706890
.word 0xd63f0200
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf9747231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf90077a0
.word 0xf94037b1
.word 0xf9748a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xb4003fe0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf974b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf900a7a0
.word 0xf940a7a0
.word 0xf900aba0
.word 0xf940a7a0
.word 0xeb1f001f
.word 0x54000320
.word 0xf940a7a0
.word 0xf9400000
.word 0xf900afa0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1224]
.word 0xeb01001f
.word 0x540001e3
.word 0xf940afa0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1224]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000040
.word 0xf900abbf
.word 0xf940aba0
.word 0xb4003ac0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9755631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf900b3a0
.word 0xf940b3a0
.word 0xf900b7a0
.word 0xf940b3a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf940b3a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400400

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2032]
.word 0xeb01001f
.word 0x54000040
.word 0xf900b7bf
.word 0xf940b7a0
.word 0xb5003740
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf975c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf900bba0
.word 0xf940bba0
.word 0xf900bfa0
.word 0xf940bba0
.word 0xeb1f001f
.word 0x54000500
.word 0xf940bba0
.word 0xf9400000
.word 0xf900c3a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1232]
.word 0xeb01001f
.word 0x540001e3
.word 0xf940c3a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1232]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000220
.word 0xf940c3a0
.word 0x3940b000
.word 0xd280003e
.word 0xeb1e001f
.word 0x54000161

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1240]

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1248]
.word 0xf940bba0
bl _p_410
.word 0xf900bfa0
.word 0x14000002
.word 0xf900bfbf
.word 0xf940bfa0
.word 0xb5003040
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf976a631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1256]
.word 0xd2800601
.word 0xd2800601
bl _p_20
.word 0xf901f3a0
bl _p_411
.word 0xf94037b1
.word 0xf976d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf9007fa0
.word 0xf94037b1
.word 0xf976ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1048]
.word 0xaa0203e0
.word 0xf940005e
bl _p_412
.word 0xf94037b1
.word 0xf9771a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9772a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1816]
.word 0xf90083a0
.word 0xf94037b1
.word 0xf9774a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf900c7a0
.word 0xf940c7a0
.word 0xb4000320
.word 0xf940c7a0
.word 0xf9400000
.word 0xb9402801

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1224]
.word 0xeb02003f
.word 0x10000011
.word 0x54003aa3
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1224]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xeb1f001f
.word 0x10000011
.word 0x540038c0
.word 0xf940c7a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3776]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf977f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0xf90087a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9781e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000055
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9784231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94087a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #3784]
.word 0x928003f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9787e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf9008ba0
.word 0xf94037b1
.word 0xf9789631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf901f7a0
.word 0xf9408ba0
.word 0xf901f3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf941f3a1
.word 0xf941f7a2
.word 0xaa0203e0
.word 0xf940005e
bl _p_282
.word 0xf94037b1
.word 0xf978e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf978f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa2
.word 0xf94083a1
.word 0xaa0203e0
.word 0xf940005e
bl _p_412
.word 0xf94037b1
.word 0xf9791631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9792631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #528]
.word 0xaa0203e0
.word 0xf940005e
bl _p_412
.word 0xf94037b1
.word 0xf9795231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9796231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #368]
.word 0xf90083a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9799231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94087a1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1400]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0x53001c00
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf979d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x35fff260
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf979fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000050
.word 0xf901ebbe
.word 0xf94037b1
.word 0xf97a1631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94087a0
.word 0xf900cba0
.word 0xf940cba0
.word 0xf900cfa0
.word 0xf940cba0
.word 0xeb1f001f
.word 0x54000380
.word 0xf940cba0
.word 0xf9400000
.word 0xf900d3a0
.word 0xf940d3a0
.word 0xb9402800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3792]
.word 0xeb01001f
.word 0x540001e3
.word 0xf940d3a0
.word 0xf9401000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #3792]
.word 0x9343fc22
.word 0x8b020000
.word 0x39400000
.word 0xd28000fe
.word 0xa1e0022
.word 0xd2800021
.word 0x1ac22021
.word 0xa010000
.word 0xb5000080
.word 0x14000001
.word 0xf900cfbf
.word 0x14000001
.word 0xf940cfa0
.word 0xf9008fa0
.word 0xf94037b1
.word 0xf97ab631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa0
.word 0xb40002e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97ade31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa1
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1408]
.word 0x928004f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf94037b1
.word 0xf97b1631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97b3631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ebbe
.word 0xd61f03c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97b5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1216]
.word 0xaa0203e0
.word 0xf940005e
bl _p_412
.word 0xf94037b1
.word 0xf97b8a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97b9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_387
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90203a0
bl _p_388
.word 0xf94037b1
.word 0xf97bca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a0
.word 0xf900d7a0
.word 0xf940d7a0
.word 0xf901f7a0
.word 0xf940d7a0
.word 0xf901ffa0
.word 0xf9407fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9402030
.word 0xd63f0200
.word 0xf901fba0
.word 0xf94037b1
.word 0xf97c0a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba1
.word 0xf941ffa2
.word 0xaa0203e0
.word 0xf940005e
bl _p_389
.word 0xf94037b1
.word 0xf97c2e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf900dba0
.word 0xf940dba0
.word 0xf901f3a0
.word 0xf940dba2
.word 0xf94077a1
.word 0xaa0203e0
.word 0xf940005e
bl _p_390
.word 0xf94037b1
.word 0xf97c6231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97c8231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x14000083
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97caa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf90203a0
.word 0xf94077a0
.word 0xf901ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf941ffa1
.word 0xf94203a2
.word 0xaa0203e0
.word 0xf940005e
bl _p_282
.word 0xf94037b1
.word 0xf97cf631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97d0631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_387
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf901fba0
bl _p_388
.word 0xf94037b1
.word 0xf97d3631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf9009fa0
.word 0xf9409fa0
.word 0xf901f7a0
.word 0xf9409fa2

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #528]
.word 0xaa0203e0
.word 0xf940005e
bl _p_389
.word 0xf94037b1
.word 0xf97d7231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f7a0
.word 0xf900a3a0
.word 0xf940a3a0
.word 0xf901f3a0
.word 0xf940a3a2
.word 0xf94077a1
.word 0xaa0203e0
.word 0xf940005e
bl _p_390
.word 0xf94037b1
.word 0xf97da631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97dc631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a0
.word 0x14000032
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf97dee31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd281c3a1
.word 0xd281c3a1
bl _p_32
.word 0xf901f7a0
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf97e3631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1264]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e1
.word 0xf941f7a0
.word 0xf941fba2
.word 0xb9001022
bl _p_63
.word 0xf901f3a0
.word 0xf94037b1
.word 0xf97e7631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941f3a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94037b1
.word 0xf97ea231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa9407bfd
.word 0xd2808a10
.word 0x910003f1
.word 0x8b100231
.word 0x9100023f
.word 0xd65f03c0
.word 0xd2801c60
.word 0xaa1103e1
bl _p_27
.word 0xd2801140
.word 0xaa1103e1
bl _p_27
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_ef:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_ConvertTo_object_System_Type
SQLite_Net_TableQuery_1_T_REF_ConvertTo_object_System_Type:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa90167b8
.word 0xf90013ba
.word 0xf90017a0
.word 0xaa0103f9
.word 0xaa0203fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1272]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xd2800018
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
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_121
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9002ba0
.word 0xaa0003f8
.word 0xf9401bb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa0003e1
.word 0xb40006a0
.word 0xf9401bb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xb5000219
.word 0xf9401bb1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0x1400003e
.word 0xf9401bb1
.word 0xf9411231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1803e0
bl _p_413
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa2
.word 0xaa1903e0
.word 0xaa1803e1
bl _p_302
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9417631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x1400001f
.word 0xf9401bb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1a03e0
bl _p_413
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa2
.word 0xaa1903e0
.word 0xaa1a03e1
bl _p_302
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf941d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401bb1
.word 0xf9420631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94167b8
.word 0xf94013ba
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_f0:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_CompileNullBinaryExpression_System_Linq_Expressions_BinaryExpression_SQLite_Net_TableQuery_1_CompileResult_T_REF
SQLite_Net_TableQuery_1_T_REF_CompileNullBinaryExpression_System_Linq_Expressions_BinaryExpression_SQLite_Net_TableQuery_1_CompileResult_T_REF:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a0
.word 0xaa0103f9
.word 0xaa0203fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1280]
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
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xd28001a1
.word 0xd28001be
.word 0x6b1e001f
.word 0x540004e1
.word 0xf94017b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1048]
.word 0xf90027a0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_363
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9402ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1288]
bl _p_30
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x14000068
.word 0xf94017b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xd2800461
.word 0xd280047e
.word 0x6b1e001f
.word 0x540004e1
.word 0xf94017b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1048]
.word 0xf90027a0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_363
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf941ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf9402ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1296]
bl _p_30
.word 0xf90023a0
.word 0xf94017b1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9421631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x1400002e
.word 0xf94017b1
.word 0xf9422e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd281cc61
.word 0xd281cc61
bl _p_32
.word 0xf90027a0
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1264]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e1
.word 0xf94027a0
.word 0xf9402ba2
.word 0xb9001022
bl _p_63
.word 0xf90023a0
.word 0xf94017b1
.word 0xf942b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94017b1
.word 0xf942e231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_f1:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_GetSqlName_System_Linq_Expressions_BinaryExpression
SQLite_Net_TableQuery_1_T_REF_GetSqlName_System_Linq_Expressions_BinaryExpression:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1304]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xd2800019
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
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf90027a0
.word 0xf94017b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a0
.word 0xf90023a0
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xaa0003e1
.word 0xd28001e1
.word 0xd28001fe
.word 0x6b1e001f
.word 0x54000261
.word 0xf94017b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1312]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x14000178
.word 0xf94017b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800200
.word 0xd280021e
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1320]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x1400015d
.word 0xf94017b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800280
.word 0xd280029e
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1328]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x14000142
.word 0xf94017b1
.word 0xf941ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd28002a0
.word 0xd28002be
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf9421231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1336]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9424231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x14000127
.word 0xf94017b1
.word 0xf9425a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800040
.word 0xd280005e
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf9427e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1344]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf942ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x1400010c
.word 0xf94017b1
.word 0xf942c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800060
.word 0xd280007e
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf942ea31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1352]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9431a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x140000f1
.word 0xf94017b1
.word 0xf9433231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800480
.word 0xd280049e
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1360]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9438631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x140000d6
.word 0xf94017b1
.word 0xf9439e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd28004a0
.word 0xd28004be
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf943c231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1368]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf943f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x140000bb
.word 0xf94017b1
.word 0xf9440a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd28001a0
.word 0xd28001be
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf9442e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1376]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9445e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x140000a0
.word 0xf94017b1
.word 0xf9447631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800460
.word 0xd280047e
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf9449a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1384]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf944ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x14000085
.word 0xf94017b1
.word 0xf944e231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x35000859
.word 0xf94017b1
.word 0xf944fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_382
.word 0xf90027a0
.word 0xf94017b1
.word 0xf9451e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404030
.word 0xd63f0200
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9454631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1104]
.word 0xeb01001f
.word 0x54000261
.word 0xf94017b1
.word 0xf9456e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1392]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9459e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x14000050
.word 0xf94017b1
.word 0xf945b631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1400]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf945e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x1400003e
.word 0xf94017b1
.word 0xf945fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800540
.word 0xd280055e
.word 0x6b1e033f
.word 0x54000261
.word 0xf94017b1
.word 0xf9462231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1408]
.word 0xf90023a0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9465231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0x14000023
.word 0xf94017b1
.word 0xf9466a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd281e0a1
.word 0xd281e0a1
bl _p_32
.word 0xf90027a0
.word 0xaa1903e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1264]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e1
.word 0xf94027a0
.word 0xb9001039
bl _p_63
.word 0xf90023a0
.word 0xf94017b1
.word 0xf946c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94017b1
.word 0xf946f231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_f2:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_Count
SQLite_Net_TableQuery_1_T_REF_Count:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1416]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1424]
bl _p_338
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1432]
.word 0xf940001e
bl _p_414
.word 0x93407c00
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_f3:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_REF_First
SQLite_Net_TableQuery_1_T_REF_First:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1440]
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
.word 0xd2800021
.word 0xd2800021
bl _p_415
.word 0xf90023a0
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400000
bl _p_416
.word 0xaa0003ef
.word 0xf94023a0
bl _p_417
.word 0xf9001fa0
.word 0xf9400fb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400000
bl _p_418
.word 0xaa0003ef
.word 0xf9401fa0
bl _p_419
.word 0xf9001ba0
.word 0xf9400fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400fb1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_f4:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_CompileResult_T_REF_get_CommandText
SQLite_Net_TableQuery_1_CompileResult_T_REF_get_CommandText:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1448]
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

Lme_f5:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_CompileResult_T_REF_set_CommandText_string
SQLite_Net_TableQuery_1_CompileResult_T_REF_set_CommandText_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1456]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_f6:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_CompileResult_T_REF_get_Value
SQLite_Net_TableQuery_1_CompileResult_T_REF_get_Value:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1464]
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

Lme_f7:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_CompileResult_T_REF_set_Value_object
SQLite_Net_TableQuery_1_CompileResult_T_REF_set_Value_object:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1472]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_f8:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_CompileResult_T_REF__ctor
SQLite_Net_TableQuery_1_CompileResult_T_REF__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1480]
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

Lme_f9:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1__c_T_REF__cctor
SQLite_Net_TableQuery_1__c_T_REF__cctor:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf90017af

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1488]
.word 0xf9000bb0
.word 0xf9400a11
.word 0xf9000fb1
.word 0xf9400bb1
.word 0xf9403231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017a0
bl _p_420
.word 0xd2800201
.word 0xd2800201
bl _p_20
.word 0xf9001ba0
bl _p_421
.word 0xf9400bb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017a0
bl _p_422
.word 0xf9401ba1
.word 0xf9000001
.word 0xf9400bb1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_fa:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1__c_T_REF__ctor
SQLite_Net_TableQuery_1__c_T_REF__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1496]
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

Lme_fb:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1__c_T_REF__GenerateCommandb__37_0_SQLite_Net_BaseTableQuery_Ordering
SQLite_Net_TableQuery_1__c_T_REF__GenerateCommandb__37_0_SQLite_Net_BaseTableQuery_Ordering:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001bba
.word 0xf9001fa0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1504]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xf94023b1
.word 0xf9404231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #512]
.word 0xf9003fa0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_423
.word 0xf9003ba0
.word 0xf94023b1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #512]
.word 0xf90037a0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_424
.word 0x53001c00
.word 0xf90033a0
.word 0xf94023b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf94037a1
.word 0xf9403ba2
.word 0xf9403fa3
.word 0xaa0303f9
.word 0xaa0203f8
.word 0xaa0103f7
.word 0x35000120
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xaa1703e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1512]
.word 0xaa0003f6
.word 0x14000008
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xaa1703e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1816]
.word 0xaa0003f6
.word 0xaa1903e0
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xaa1603e0
.word 0xaa1903e0
.word 0xaa1803e1
.word 0xaa1703e2
.word 0xaa1603e3
bl _p_52
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf94023b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xa94267b8
.word 0xf9401bba
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_fc:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1__c_T_REF__CompileExprb__38_0_SQLite_Net_TableQuery_1_CompileResult_T_REF
SQLite_Net_TableQuery_1__c_T_REF__CompileExprb__38_0_SQLite_Net_TableQuery_1_CompileResult_T_REF:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1520]
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
.word 0xf9400fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_425
.word 0xf90023a0
.word 0xf94013b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_fd:
.text
	.align 4
	.no_dead_strip SQLite_Net_TraceListenerExtensions_WriteLine_SQLite_Net_ITraceListener_string_object
SQLite_Net_TraceListenerExtensions_WriteLine_SQLite_Net_ITraceListener_string_object:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xaa0003f8
.word 0xf90013a1
.word 0xf90017a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1528]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf9401bb1
.word 0xf9403e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xb50000d8
.word 0xf9401bb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0x1400003a
.word 0xf9401bb1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
bl _p_157
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf90033a0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xaa0003f7
.word 0xaa1703e0
.word 0xf90037a0
.word 0xaa1703e0
.word 0xd2800000
.word 0xf94017a2
.word 0xaa1703e0
.word 0xd2800001
.word 0xf94002e3
.word 0xf9407870
.word 0xd63f0200
.word 0xf9402fa0
.word 0xf94033a1
.word 0xf94037a2
bl _p_426
.word 0xf9002ba0
.word 0xf9401bb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba1
.word 0xaa1803e0
.word 0xf9400302

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1536]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf9401bb1
.word 0xf9414e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9415e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9416e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_ff:
.text
	.align 4
	.no_dead_strip SQLite_Net_TraceListenerExtensions_WriteLine_SQLite_Net_ITraceListener_string_object_object
SQLite_Net_TraceListenerExtensions_WriteLine_SQLite_Net_ITraceListener_string_object_object:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xf90013b7
.word 0xaa0003f7
.word 0xf90017a1
.word 0xf9001ba2
.word 0xf9001fa3

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1544]
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
.word 0xaa1703e0
.word 0xb50000d7
.word 0xf94023b1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000046
.word 0xf94023b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
bl _p_157
.word 0xf90037a0
.word 0xf94023b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017a0
.word 0xf9003ba0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800041
bl _p_16
.word 0xaa0003f6
.word 0xaa1603e0
.word 0xf90043a0
.word 0xaa1603e0
.word 0xd2800000
.word 0xf9401ba2
.word 0xaa1603e0
.word 0xd2800001
.word 0xf94002c3
.word 0xf9407870
.word 0xd63f0200
.word 0xf94043a0
.word 0xaa0003f5
.word 0xaa1503e0
.word 0xf9003fa0
.word 0xaa1503e0
.word 0xd2800020
.word 0xf9401fa2
.word 0xaa1503e0
.word 0xd2800021
.word 0xf94002a3
.word 0xf9407870
.word 0xd63f0200
.word 0xf94037a0
.word 0xf9403ba1
.word 0xf9403fa2
bl _p_426
.word 0xf90033a0
.word 0xf94023b1
.word 0xf9414e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a1
.word 0xaa1703e0
.word 0xf94002e2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1536]
.word 0x928010f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf94023b1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xf94013b7
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0

Lme_100:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_DefaultAttribute_get_UseProperty
SQLite_Net_Attributes_DefaultAttribute_get_UseProperty:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
.word 0x39406000
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

Lme_128:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_DefaultAttribute_get_Value
SQLite_Net_Attributes_DefaultAttribute_get_Value:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1560]
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

Lme_129:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_AutoIncrementAttribute__ctor
SQLite_Net_Attributes_AutoIncrementAttribute__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
bl _p_1
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

Lme_12a:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_CollationAttribute_get_Value
SQLite_Net_Attributes_CollationAttribute_get_Value:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1576]
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

Lme_12b:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_ColumnAttribute__ctor_string
SQLite_Net_Attributes_ColumnAttribute__ctor_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xaa0003f9
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1584]
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
.word 0xaa1903e0
bl _p_1
.word 0xf94013b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9400fa1
.word 0xaa1903e0
bl _p_427
.word 0xf94013b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_12c:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_ColumnAttribute_get_Name
SQLite_Net_Attributes_ColumnAttribute_get_Name:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1592]
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

Lme_12d:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_ColumnAttribute_set_Name_string
SQLite_Net_Attributes_ColumnAttribute_set_Name_string:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1600]
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

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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

Lme_12e:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_IgnoreAttribute__ctor
SQLite_Net_Attributes_IgnoreAttribute__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
bl _p_1
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

Lme_12f:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_IndexedAttribute__ctor
SQLite_Net_Attributes_IndexedAttribute__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
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
bl _p_1
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

Lme_130:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_IndexedAttribute_get_Name
SQLite_Net_Attributes_IndexedAttribute_get_Name:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1624]
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

Lme_131:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_IndexedAttribute_get_Order
SQLite_Net_Attributes_IndexedAttribute_get_Order:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1632]
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
.word 0xb9801800
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

Lme_132:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_IndexedAttribute_get_Unique
SQLite_Net_Attributes_IndexedAttribute_get_Unique:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1640]
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
.word 0x39407000
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

Lme_133:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_MaxLengthAttribute_get_Value
SQLite_Net_Attributes_MaxLengthAttribute_get_Value:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1648]
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
.word 0xb9801000
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

Lme_134:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_PrimaryKeyAttribute__ctor
SQLite_Net_Attributes_PrimaryKeyAttribute__ctor:
.loc 1 1 0
.word 0xa9bd7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1656]
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
bl _p_1
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

Lme_135:
.text
	.align 4
	.no_dead_strip SQLite_Net_Attributes_TableAttribute_get_Name
SQLite_Net_Attributes_TableAttribute_get_Name:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1664]
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

Lme_136:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteDeferredQuery_T_GSHAREDVT
SQLite_Net_SQLiteCommand_ExecuteDeferredQuery_T_GSHAREDVT:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9001baf
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1672]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9401ba0
bl _p_428
.word 0xf9001fa0
.word 0xf9401fa0
.word 0xb9800000
.word 0xd2800000
.word 0xf90023bf
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xf90037a0
.word 0xf9401ba0
bl _p_429
.word 0xaa0003e1
.word 0xf94037a3
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf940007e
bl _p_110
.word 0xf9002fa0
.word 0xf9400fb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
bl _p_430
.word 0xf90033a0
.word 0xf9401ba0
bl _p_431
.word 0xaa0003e2
.word 0xf9402fa1
.word 0xf94033af
.word 0xaa1a03e0
.word 0xd63f0040
.word 0xf9002ba0
.word 0xf9400fb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400fb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_139:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteQuery_T_GSHAREDVT
SQLite_Net_SQLiteCommand_ExecuteQuery_T_GSHAREDVT:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9001baf
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1680]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9401ba0
bl _p_432
.word 0xf9001fa0
.word 0xf9401fa0
.word 0xb9800000
.word 0xd2800000
.word 0xf90023bf
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400f40
.word 0xf9003fa0
.word 0xf9401ba0
bl _p_433
.word 0xaa0003e1
.word 0xf9403fa3
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf940007e
bl _p_110
.word 0xf90037a0
.word 0xf9400fb1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
bl _p_434
.word 0xf9003ba0
.word 0xf9401ba0
bl _p_435
.word 0xaa0003e2
.word 0xf94037a1
.word 0xf9403baf
.word 0xaa1a03e0
.word 0xd63f0040
.word 0xf9002fa0
.word 0xf9400fb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
bl _p_436
.word 0xf90033a0
.word 0xf9401ba0
bl _p_437
.word 0xaa0003e1
.word 0xf9402fa0
.word 0xf94033af
.word 0xd63f0020
.word 0xf9002ba0
.word 0xf9400fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400fb1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_13a:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteDeferredQuery_T_GSHAREDVT_SQLite_Net_TableMapping
SQLite_Net_SQLiteCommand_ExecuteDeferredQuery_T_GSHAREDVT_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xf90013b8
.word 0xf9002baf
.word 0xf90017a0
.word 0xf9001ba1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1688]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf9402ba0
bl _p_438
.word 0xaa0003f8
.word 0xb9800300
.word 0xd2800000
.word 0xf9002fbf
.word 0xf9401fb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0x92800020
.word 0xf2bfffe0
.word 0xf9402ba0
bl _p_439
bl _p_440
.word 0xf9003ba0
.word 0xf9402ba0
bl _p_441
.word 0xaa0003e2
.word 0xf9403ba0
.word 0xf90037a0
.word 0x92800021
.word 0xf2bfffe1
.word 0xd63f0040
.word 0xf9401fb1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037a0
.word 0xaa0003f7
.word 0xaa1703e1
.word 0xaa1703e0
.word 0xf94017a0
.word 0xf9400702
.word 0xd1000442
.word 0x8b0202e2
.word 0xf9000040
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xaa0103f6
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf9401ba0
.word 0xf9400b02
.word 0xd1000442
.word 0xf90033a1
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9401fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xf94013b8
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_13b:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand_ExecuteScalar_T_GSHAREDVT
SQLite_Net_SQLiteCommand_ExecuteScalar_T_GSHAREDVT:
.loc 1 1 0
.word 0xa9b37bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba8
.word 0xf9003faf
.word 0xf9002fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1696]
.word 0xf90033b0
.word 0xf9400a11
.word 0xf90037b1
.word 0xf9403fa0
bl _p_442
.word 0xaa0003fa
.word 0xb9800340
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
.word 0x910003f9
.word 0xb9803b40
.word 0xaa1903e1
.word 0x8b000320
.word 0xf9401341
.word 0xf9401742
.word 0xd63f0040
.word 0xf90043bf
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xf94033b1
.word 0xf940ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_89
.word 0xf9005fa0
.word 0xf94033b1
.word 0xf9410231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1888]
.word 0xf9402fa2
bl _p_91
.word 0xf94033b1
.word 0xf9412a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xb9803b41
.word 0xaa1903e0
.word 0x8b010000
.word 0xf9401341
.word 0xf9401742
.word 0xd63f0040
.word 0xf94033b1
.word 0xf9416231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
bl _p_107
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf90043a0
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90063a0
.word 0xf94033b1
.word 0xf941e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a2
.word 0xf94043a1
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1664]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf9005fa0
.word 0xf94033b1
.word 0xf9422a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xf9005ba0
.word 0xaa0003f8
.word 0xf94033b1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003e1
.word 0xd2800c81
.word 0xd2800c9e
.word 0x6b1e001f
.word 0x54001421
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9427e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90063a0
.word 0xf94033b1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a3
.word 0xf94043a1
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1896]
.word 0x928007f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9005fa0
.word 0xf94033b1
.word 0xf9430a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xaa0003f7
.word 0xf94033b1
.word 0xf9432231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
bl _p_443
bl _p_121
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9434231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xaa0003f5
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa0003f4
.word 0xb5000120
.word 0xaa1403e0
.word 0xf94033b1
.word 0xf9436e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
bl _p_443
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xaa1403f6
.word 0xf94033b1
.word 0xf9439231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xd28000a0
.word 0xd28000be
.word 0x6b1e02ff
.word 0x54001460
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf943c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf94043a1
.word 0xd2800002
.word 0xaa1703e2
.word 0xaa1603e2
.word 0xd2800002
.word 0xaa1703e3
.word 0xaa1603e4
bl _p_122
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf943fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf9400741
bl _p_444
.word 0xaa0003f3
.word 0xf9400b40
.word 0xf90047a0
.word 0xd280005e
.word 0xeb1e001f
.word 0x54000100
.word 0xf94047a0
.word 0xd280007e
.word 0xeb1e001f
.word 0x54000140
.word 0x91004260
.word 0xf9004ba0
.word 0x14000013
.word 0xb9804340
.word 0xaa1903e1
.word 0x8b000320
.word 0xf9004ba0
.word 0xf9000013
.word 0x1400000d
.word 0xf9400f41
.word 0xb9804b40
.word 0xaa1903e2
.word 0x8b000320
.word 0xaa0003e8
.word 0xaa1303e0
.word 0xd63f0020
.word 0xb9804b40
.word 0xaa1903e1
.word 0x8b000320
.word 0xf9004ba0
.word 0x14000001
.word 0xf9404ba1
.word 0xb9805340
.word 0xaa1903e2
.word 0x8b000320
.word 0xf9401342
.word 0xf9401b43
.word 0xd63f0060
.word 0xb9805340
.word 0xaa1903e1
.word 0x8b000321
.word 0xb9803b40
.word 0xaa1903e2
.word 0x8b000320
.word 0xf9401342
.word 0xf9401b43
.word 0xd63f0060
.word 0xf94033b1
.word 0xf944d631
.word 0xb4000051
.word 0xd63f0220
.word 0x94000060
.word 0x14000071
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf944fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xd2800ca0
.word 0xd2800cbe
.word 0x6b1e031f
.word 0x54000161
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9453231
.word 0xb4000051
.word 0xd63f0220
.word 0x94000049
.word 0x1400005a
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9455a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf9402fa0
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90067a0
.word 0xf94033b1
.word 0xf9459e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_100
.word 0xf90063a0
.word 0xf94033b1
.word 0xf945c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a1
.word 0xf94067a2
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1688]
.word 0x92800bf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0xf9005fa0
.word 0xf94033b1
.word 0xf9460631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa1
.word 0xaa1803e0
bl _p_101
.word 0xf9005ba0
.word 0xf94033b1
.word 0xf9462631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
bl _p_33
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9464e31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000013
.word 0xf90057be
.word 0xf94033b1
.word 0xf9466a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf94043a1
bl _p_108
.word 0xf94033b1
.word 0xf9468631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9469631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057be
.word 0xd61f03c0
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf946be31
.word 0xb4000051
.word 0xd63f0220
.word 0xb9803b40
.word 0xaa1903e1
.word 0x8b000321
.word 0xb9805b40
.word 0xaa1903e2
.word 0x8b000320
.word 0xf9401342
.word 0xf9401b43
.word 0xd63f0060
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9470231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xb9805b41
.word 0xaa1903e2
.word 0x8b010321
.word 0xf9005fa1
.word 0xf9005ba0
.word 0xf9401340
.word 0xf9401b40
.word 0xf9403fa0
bl _p_445
.word 0xaa0003e2
.word 0xf9405ba0
.word 0xf9405fa1
bl _mono_gsharedvt_value_copy
.word 0xf94033b1
.word 0xf9474a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8cd7bfd
.word 0xd65f03c0

Lme_13c:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT__ctor_int
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT__ctor_int:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xf9000fa0
.word 0xf90013a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1704]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf9400fa0
.word 0xf9400000
bl _p_446
.word 0xaa0003f9
.word 0xb9800320
.word 0xd2800000
.word 0xf90023bf
.word 0xf94017b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf94017b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xb98023a1
.word 0xf9400722
.word 0xd1000442
.word 0x8b020000
.word 0xb9000001
.word 0xf94017b1
.word 0xf940c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9002ba0
bl _p_170
.word 0x93407c00
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xf9400b22
.word 0xd1000442
.word 0x8b020000
.word 0xb9000001
.word 0xf94017b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_13d:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_IDisposable_Dispose
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_IDisposable_Dispose:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1712]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94013a0
.word 0xf9400000
bl _p_447
.word 0xaa0003fa
.word 0xb9800340
.word 0xd2800000
.word 0xf90023bf
.word 0xd2800019
.word 0xf94017b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0xb9800000
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0x92800040
.word 0xf2bfffe0
.word 0x9280005e
.word 0xf2bffffe
.word 0x6b1e033f
.word 0x540001c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xd2800020
.word 0xd280003e
.word 0x6b1e033f
.word 0x54000541
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0x94000002
.word 0x14000018
.word 0xf9002fbe
.word 0xf94017b1
.word 0xf9414a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf90033a0
.word 0xf94013a0
.word 0xf9400000
bl _p_448
.word 0xaa0003e1
.word 0xf94033a0
.word 0xd63f0020
.word 0xf94017b1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fbe
.word 0xd61f03c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_13e:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_MoveNext
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_MoveNext:
.loc 1 1 0
.word 0xa9b07bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1720]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0xf9402ba0
.word 0xf9400000
bl _p_449
.word 0xaa0003fa
.word 0xb9800340
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
.word 0x910003f9
.word 0x3901c3bf
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xd2800013
.word 0xd2800018
.word 0xf9003fbf
.word 0xf9402fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0xb9800000
.word 0xaa0003f7
.word 0xf9402fb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x34000397
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xd2800020
.word 0xd280003e
.word 0x6b1e02ff
.word 0x540054c0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9415231
.word 0xb4000051
.word 0xd63f0220
.word 0x3901c3bf
.word 0xf9402fb1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000326
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9418a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x92800001
.word 0xf2bfffe1
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900001e
.word 0xf9402fb1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_89
.word 0xf90077a0
.word 0xf9402fb1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1888]
.word 0xf9402ba2
.word 0xf9400b43
.word 0xd1000463
.word 0x8b030042
.word 0xf9400042
bl _p_91
.word 0xf9402fb1
.word 0xf9422e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90073a0
.word 0xf9402ba0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400001
.word 0xaa0103e0
.word 0xf940003e
bl _p_107
.word 0xf9006fa0
.word 0xf9402fb1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xf94073a1
.word 0xf9400f42
.word 0xd1000442
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf942ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x92800041
.word 0xf2bfffe1
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0x9280005e
.word 0xf2bffffe
.word 0xb900001e
.word 0xf9402fb1
.word 0xf942fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90063a0
.word 0xf9402ba0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9006ba0
.word 0xf9402fb1
.word 0xf9435631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba2
.word 0xf9402ba0
.word 0xf9400f41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400001
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2728]
.word 0x92800cf0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf943aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2736]
bl _p_16
.word 0xf94063a1
.word 0xf9401342
.word 0xd1000442
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9440a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800016
.word 0xf9402fb1
.word 0xf9441e31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400005e
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9444231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90073a0
.word 0xf9402fb1
.word 0xf9449231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a3
.word 0xf9402ba0
.word 0xf9400f41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400001
.word 0xaa1603e0
.word 0xaa0303e0
.word 0xaa1603e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2744]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf9006fa0
.word 0xf9402fb1
.word 0xf944ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xf9006ba0
.word 0xaa0003f5
.word 0xf9402fb1
.word 0xf9450631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba1
.word 0xf9402ba0
.word 0xf9401342
.word 0xd1000442
.word 0x8b020000
.word 0xf9400000
.word 0xf90067a0
.word 0xaa1603e0
.word 0xf9402ba0
.word 0xf9401742
.word 0xd1000442
.word 0x8b020000
.word 0xf9400002
.word 0xaa0103e0
.word 0xaa0203e0
.word 0xf940005e
bl _p_172
.word 0xf90063a0
.word 0xf9402fb1
.word 0xf9455e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a2
.word 0xf94067a3
.word 0xaa0303e0
.word 0xaa1603e1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf9458a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0x110006c0
.word 0xaa0003f6
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf945b631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9402ba0
.word 0xf9401341
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb9801800
.word 0x6b0002df
.word 0x54fff26b
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf945fa31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000184
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9461e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9400c01
.word 0xaa0103e0
.word 0xf940003e
bl _p_161
.word 0xf9006ba0
.word 0xf9402fb1
.word 0xf9465631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401741
.word 0xd1000421
.word 0x8b010000
.word 0xf9400001
.word 0xaa0103e0
.word 0xf940003e
bl _p_173
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf9468a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xf9406ba3
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #2472]
.word 0x92800df0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0xf90063a0
.word 0xf9402fb1
.word 0xf946d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f4
.word 0xf9402fb1
.word 0xf946ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800013
.word 0xf9402fb1
.word 0xf946fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x140000b0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9472231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401341
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xaa1303e1
.word 0x93407e61
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54003989
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400000
.word 0xb40011a0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9478231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf9007fa0
.word 0xf9402fb1
.word 0xf947d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407fa3
.word 0xf9402ba0
.word 0xf9400f41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400001
.word 0xaa1303e0
.word 0xaa0303e0
.word 0xaa1303e2
.word 0xf9400063

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1896]
.word 0x928007f0
.word 0xf2bffff0
.word 0xf8706870
.word 0xd63f0200
.word 0x93407c00
.word 0xf9007ba0
.word 0xf9402fb1
.word 0xf9482e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba0
.word 0xf90077a0
.word 0xaa0003f8
.word 0xf9402fb1
.word 0xf9484a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf9402ba1
.word 0xf9400b42
.word 0xd1000442
.word 0x8b020021
.word 0xf9400021
.word 0xf90073a1
.word 0xf9402ba1
.word 0xf9400f42
.word 0xd1000442
.word 0x8b020021
.word 0xf9400021
.word 0xf90067a1
.word 0xaa1303e1
.word 0xf9006ba0
.word 0xf9402ba0
.word 0xf9401341
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xaa1303e1
.word 0x93407e61
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002e69
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400001
.word 0xaa0103e0
.word 0xf940003e
bl _p_55
.word 0xf9006fa0
.word 0xf9402fb1
.word 0xf948e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xf9406ba3
.word 0xf9406fa4
.word 0xf94073a5
.word 0xaa0503e0
.word 0xaa1303e2
.word 0xf94000be
bl _p_122
.word 0xf90063a0
.word 0xf9402fb1
.word 0xf9491631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf9003fa0
.word 0xf9402fb1
.word 0xf9492e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401341
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xaa1303e1
.word 0x93407e61
.word 0xb9801802
.word 0xeb01005f
.word 0x10000011
.word 0x54002929
.word 0xd37df021
.word 0x8b010000
.word 0x91008000
.word 0xf9400003
.word 0xaa1403e0
.word 0xf9403fa2
.word 0xaa0303e0
.word 0xaa1403e1
.word 0xf940007e
bl _p_174
.word 0xf9402fb1
.word 0xf9499231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf949b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0x11000660
.word 0xaa0003f3
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf949de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0xf9402ba0
.word 0xf9401341
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb9801800
.word 0x6b00027f
.word 0x54ffe82b
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94a2231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400002
.word 0xaa1403e0
.word 0xaa0203e0
.word 0xaa1403e1
.word 0xf9400042
.word 0xf9403050
.word 0xd63f0200
.word 0xf9402fb1
.word 0xf94a5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94a6e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90043a0
.word 0xaa1403e0
.word 0xf9401b41
.word 0xaa1403e0
bl _p_444
.word 0xf90047a0
.word 0xf9401f40
.word 0xf9004ba0
.word 0xd280005e
.word 0xeb1e001f
.word 0x54000120
.word 0xf9404ba0
.word 0xd280007e
.word 0xeb1e001f
.word 0x54000180
.word 0xf94047a0
.word 0x91004000
.word 0xf9004fa0
.word 0x14000014
.word 0xb9806340
.word 0xaa1903e1
.word 0x8b000320
.word 0xf9004fa0
.word 0xf94047a1
.word 0xf9000001
.word 0x1400000d
.word 0xf9402341
.word 0xb9806b40
.word 0xaa1903e2
.word 0x8b000320
.word 0xaa0003e8
.word 0xf94047a0
.word 0xd63f0020
.word 0xb9806b40
.word 0xaa1903e1
.word 0x8b000320
.word 0xf9004fa0
.word 0x14000001
.word 0xf9404fa1
.word 0xb9807340
.word 0xaa1903e2
.word 0x8b000320
.word 0xf9402b42
.word 0xf9402f43
.word 0xd63f0060
.word 0xf9402740
.word 0xd1000401
.word 0xf94043a0
.word 0x8b010000
.word 0xb9807341
.word 0xaa1903e2
.word 0x8b010321
.word 0xf90067a1
.word 0xf90063a0
.word 0xf9402b40
.word 0xf9402f40
.word 0xf9402ba0
.word 0xf9400000
bl _p_450
.word 0xaa0003e2
.word 0xf94063a0
.word 0xf94067a1
bl _mono_gsharedvt_value_copy
.word 0xf9402fb1
.word 0xf94b7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xd2800021
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0xd280003e
.word 0xb900001e
.word 0xf9402fb1
.word 0xf94baa31
.word 0xb4000051
.word 0xd63f0220
.word 0xd280003e
.word 0x3901c3be
.word 0xf9402fb1
.word 0xf94bc231
.word 0xb4000051
.word 0xd63f0220
.word 0x1400008f
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94be631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x92800041
.word 0xf2bfffe1
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0x9280005e
.word 0xf2bffffe
.word 0xb900001e
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94c2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf94c7a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a2
.word 0xf9402ba0
.word 0xf9400f41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400001
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1664]
.word 0x92800ef0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf90063a0
.word 0xf9402fb1
.word 0xf94cce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xd2800c81
.word 0xd2800c9e
.word 0x6b1e001f
.word 0x54ffc900
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94d0231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xd2800001
.word 0xf9401341
.word 0xd1000421
.word 0x8b010000
.word 0xf900001f
.word 0xf9402fb1
.word 0xf94d2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90063a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_451
.word 0xaa0003e1
.word 0xf94063a0
.word 0xd63f0020
.word 0xf9402fb1
.word 0xf94d5a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94d6a31
.word 0xb4000051
.word 0xd63f0220
.word 0x3901c3bf
.word 0xf9402fb1
.word 0xf94d7e31
.word 0xb4000051
.word 0xd63f0220
.word 0x14000020
.word 0xf9005bbe
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94da631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94db631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90063a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_452
.word 0xaa0003e1
.word 0xf94063a0
.word 0xd63f0020
.word 0xf9402fb1
.word 0xf94de631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94df631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405bbe
.word 0xd61f03c0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94e1e31
.word 0xb4000051
.word 0xd63f0220
.word 0x3941c3a0
.word 0xf90063a0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf94e4631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x14000001
.word 0xf9402fb1
.word 0xf94e5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d07bfd
.word 0xd65f03c0
.word 0xd2801bc0
.word 0xaa1103e1
bl _p_27

Lme_13f:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT__m__Finally1
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT__m__Finally1:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1728]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9400fa0
.word 0xf9400000
bl _p_453
.word 0xaa0003fa
.word 0xb9800340
.word 0xd2800000
.word 0xf9001fbf
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
.word 0xf9400fa0
.word 0x92800001
.word 0xf2bfffe1
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0x9280001e
.word 0xf2bffffe
.word 0xb900001e
.word 0xf94013b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9401001
.word 0xaa0103e0
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1656]
.word 0x928001f0
.word 0xf2bffff0
.word 0xf8706830
.word 0xd63f0200
.word 0xf90023a0
.word 0xf94013b1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a2
.word 0xf9400fa0
.word 0xf9400f41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400001
.word 0xaa0203e0
.word 0xf9400042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1744]
.word 0x928008f0
.word 0xf2bffff0
.word 0xf8706850
.word 0xd63f0200
.word 0x93407c00
.word 0xf94013b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9415631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_140:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_Generic_IEnumerator_T_get_Current
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_Generic_IEnumerator_T_get_Current:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a8
.word 0xf90017a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1736]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf94017a0
.word 0xf9400000
bl _p_454
.word 0xaa0003fa
.word 0xb9800340
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
.word 0x910003f9
.word 0xf9401bb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017a0
.word 0xf9400741
.word 0xd1000421
.word 0x8b010001
.word 0xb9802340
.word 0xaa1903e2
.word 0x8b000320
.word 0xf9400b42
.word 0xf9400f43
.word 0xd63f0060
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xb9802342
.word 0xaa1903e1
.word 0x8b020021
.word 0xf9002fa1
.word 0xf9002ba0
.word 0xf9400b40
.word 0xf9400f40
.word 0xf94017a0
.word 0xf9400000
bl _p_455
.word 0xaa0003e2
.word 0xf9402ba0
.word 0xf9402fa1
bl _mono_gsharedvt_value_copy
.word 0xf9401bb1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_141:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_IEnumerator_Reset
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_IEnumerator_Reset:
.loc 1 1 0
.word 0xa9bc7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1744]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400ba0
.word 0xf9400000
bl _p_456
.word 0xf9001ba0
.word 0xf9401ba0
.word 0xb9800000
.word 0xd2800000
.word 0xf9001fbf
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_0
bl _p_33
.word 0xf9400fb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c47bfd
.word 0xd65f03c0

Lme_142:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_IEnumerator_get_Current
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_IEnumerator_get_Current:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xf9001ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1752]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf9401ba0
.word 0xf9400000
bl _p_457
.word 0xaa0003fa
.word 0xb9800340
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
.word 0x910003f9
.word 0xf9401fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400741
.word 0xd1000421
.word 0x8b010001
.word 0xb9803340
.word 0xaa1903e2
.word 0x8b000320
.word 0xf9401342
.word 0xf9401743
.word 0xd63f0060
.word 0xf9400b58
.word 0xd280005e
.word 0xeb1e031f
.word 0x54000360
.word 0xd280007e
.word 0xeb1e031f
.word 0x540003a0
.word 0xf9401ba0
.word 0xf9400000
bl _p_458
bl _p_440
.word 0xb9803341
.word 0xaa1903e2
.word 0x8b010321
.word 0xf90033a1
.word 0xf9002ba0
.word 0x91004000
.word 0xf9002fa0
.word 0xf9401340
.word 0xf9401740
.word 0xf9401ba0
.word 0xf9400000
bl _p_459
.word 0xaa0003e2
.word 0xf9402fa0
.word 0xf94033a1
bl _mono_gsharedvt_value_copy
.word 0xf9402ba0
.word 0xaa0003f7
.word 0x1400000c
.word 0xb9803340
.word 0xaa1903e1
.word 0x8b000320
.word 0xf9400017
.word 0x14000007
.word 0xf9400f41
.word 0xb9803340
.word 0xaa1903e2
.word 0x8b000320
.word 0xd63f0020
.word 0xaa0003f7
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0xf9401fb1
.word 0xf941a631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_143:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_Generic_IEnumerable_T_GetEnumerator
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_Generic_IEnumerable_T_GetEnumerator:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1760]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94013a0
.word 0xf9400000
bl _p_460
.word 0xaa0003fa
.word 0xb9800340
.word 0xd2800000
.word 0xf90023bf
.word 0xd2800019
.word 0xf94017b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0xb9800000
.word 0x92800021
.word 0xf2bfffe1
.word 0x9280003e
.word 0xf2bffffe
.word 0x6b1e001f
.word 0x54000561
.word 0xf94017b1
.word 0xf940b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf9400b41
.word 0xd1000421
.word 0x8b010000
.word 0xb9800000
.word 0xf9002ba0
bl _p_170
.word 0x93407c00
.word 0xf9002fa0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402fa1
.word 0x6b01001f
.word 0x540002c1
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xd2800001
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0xb900001f
.word 0xf94017b1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf9414631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000035
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9416a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xf94013a0
.word 0xf9400000
bl _p_461
bl _p_440
.word 0xf9002fa0
.word 0xf94013a0
.word 0xf9400000
bl _p_462
.word 0xaa0003e2
.word 0xf9402fa0
.word 0xf9002ba0
.word 0xd2800001
.word 0xd63f0040
.word 0xf94017b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xaa0003f9
.word 0xf94017b1
.word 0xf941ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94013a0
.word 0xf9400f41
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9400f41
.word 0xd1000421
.word 0x8b010321
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9423a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94013a0
.word 0xf9401341
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9401741
.word 0xd1000421
.word 0x8b010321
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf9429a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94017b1
.word 0xf942d231
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_144:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_IEnumerable_GetEnumerator
SQLite_Net_SQLiteCommand__ExecuteDeferredQueryd__15_1_T_GSHAREDVT_System_Collections_IEnumerable_GetEnumerator:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1768]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400ba0
.word 0xf9400000
bl _p_463
.word 0xf9001ba0
.word 0xf9401ba0
.word 0xb9800000
.word 0xd2800000
.word 0xf9001fbf
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf90027a0
.word 0xf9400ba0
.word 0xf9400000
bl _p_464
.word 0xaa0003e1
.word 0xf94027a0
.word 0xd63f0020
.word 0xf90023a0
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_145:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_CreateTable_T_GSHAREDVT_SQLite_Net_Interop_CreateFlags
SQLite_Net_SQLiteConnection_CreateTable_T_GSHAREDVT_SQLite_Net_Interop_CreateFlags:
.loc 1 1 0
.word 0xa9ba7bfd
.word 0x910003fd
.word 0xf9001faf
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1776]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9401fa0
bl _p_465
.word 0xf90023a0
.word 0xf94023a0
.word 0xb9800000
.word 0xd2800000
.word 0xf90027bf
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9002fa0
.word 0xf9401fa0
bl _p_466
.word 0xaa0003e1
.word 0xf9402fa0
.word 0xb9801ba2
bl _p_206
.word 0x93407c00
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c67bfd
.word 0xd65f03c0

Lme_146:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Query_T_GSHAREDVT_string_object__
SQLite_Net_SQLiteConnection_Query_T_GSHAREDVT_string_object__:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf90023af
.word 0xf9000ba0
.word 0xf9000fa1
.word 0xf90013a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1784]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94023a0
bl _p_467
.word 0xf90027a0
.word 0xf94027a0
.word 0xb9800000
.word 0xd2800000
.word 0xf9002bbf
.word 0xf94017b1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9400fa1
.word 0xf94013a2
bl _p_243
.word 0xf9003fa0
.word 0xf94017b1
.word 0xf9409e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
bl _p_468
.word 0xaa0003e1
.word 0xf9403fa0
.word 0xf9003ba1
.word 0xf940001e
.word 0xf90037a0
.word 0xf94023a0
bl _p_469
.word 0xaa0003e1
.word 0xf94037a0
.word 0xf9403baf
.word 0xd63f0020
.word 0xf90033a0
.word 0xf94017b1
.word 0xf940e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9410631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf94017b1
.word 0xf9411a31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_147:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Table_T_GSHAREDVT
SQLite_Net_SQLiteConnection_Table_T_GSHAREDVT:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9001baf
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1792]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9401ba0
bl _p_470
.word 0xf9001fa0
.word 0xf9401fa0
.word 0xb9800000
.word 0xd2800000
.word 0xf90023bf
.word 0xf9400fb1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
bl _p_95
.word 0xf90033a0
.word 0xf9400fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9401ba0
bl _p_471
bl _p_440
.word 0xf9002fa0
.word 0xf9401ba0
bl _p_472
.word 0xaa0003e3
.word 0xf9402fa0
.word 0xf94033a1
.word 0xf9002ba0
.word 0xaa1a03e2
.word 0xd63f0060
.word 0xf9400fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400fb1
.word 0xf9410e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_148:
.text
	.align 4
	.no_dead_strip SQLite_Net_SQLiteConnection_Delete_T_GSHAREDVT_object
SQLite_Net_SQLiteConnection_Delete_T_GSHAREDVT_object:
.loc 1 1 0
.word 0xa9b27bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xf9001bb7
.word 0xf9001fb9
.word 0xf90033af
.word 0xaa0003f9
.word 0xf90023a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1800]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xf94033a0
bl _p_473
.word 0xf90037a0
.word 0xf94037a0
.word 0xb9800000
.word 0xd2800000
.word 0xf9003bbf
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xf94027b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xf94033a0
bl _p_474
.word 0xaa0003e1
.word 0xd2800000
.word 0xaa1903e0
.word 0xd2800002
bl _p_110
.word 0xf9004fa0
.word 0xf94027b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf9004ba0
.word 0xaa0003f7
.word 0xf94027b1
.word 0xf940e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xaa0103e0
.word 0xf940003e
bl _p_265
.word 0xf90047a0
.word 0xf94027b1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf90043a0
.word 0xaa0003f6
.word 0xf94027b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xaa0003e1
.word 0xb5000560
.word 0xf94027b1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2812321
.word 0xd2812321
bl _p_32
.word 0xf90047a0
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_17
.word 0xf9004ba0
.word 0xf94027b1
.word 0xf9418231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2811661
.word 0xd2811661
bl _p_32
.word 0xaa0003e2
.word 0xf94047a0
.word 0xf9404ba1
bl _p_30
.word 0xf90043a0
.word 0xf94027b1
.word 0xf941be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94027b1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #4016]
.word 0xf90057a0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800041
bl _p_16
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf90067a0
.word 0xaa1403e0
.word 0xf9006fa0
.word 0xd2800000
.word 0xaa1703e0
.word 0xaa1703e0
.word 0xf94002fe
bl _p_17
.word 0xf9006ba0
.word 0xf94027b1
.word 0xf9424e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba2
.word 0xf9406fa3
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94067a0
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xf9005ba0
.word 0xaa1303e0
.word 0xf90063a0
.word 0xd2800020
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_29
.word 0xf9005fa0
.word 0xf94027b1
.word 0xf942aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa2
.word 0xf94063a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94057a0
.word 0xf9405ba1
bl _p_18
.word 0xf90053a0
.word 0xf94027b1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf9004fa0
.word 0xaa0003f5
.word 0xf94027b1
.word 0xf9430231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xaa1903e1
.word 0xf90047a0
.word 0xd2800020

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #344]
.word 0xd2800021
bl _p_16
.word 0xf9003fa0
.word 0xf9403fa0
.word 0xf9004ba0
.word 0xf9403fa3
.word 0xd2800000
.word 0xf94023a2
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94047a1
.word 0xf9404ba2
.word 0xaa1903e0
bl _p_207
.word 0x93407c00
.word 0xf90043a0
.word 0xf94027b1
.word 0xf9437a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9439a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf94027b1
.word 0xf943ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xf9401bb7
.word 0xf9401fb9
.word 0x910003bf
.word 0xa8ce7bfd
.word 0xd65f03c0

Lme_149:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection_SQLite_Net_TableMapping
SQLite_Net_TableQuery_1_T_GSHAREDVT__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000bb7
.word 0xf9000fa0
.word 0xf90013a1
.word 0xf90017a2
.word 0xf9001ba3

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1808]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf9400fa0
.word 0xf9400000
bl _p_475
.word 0xaa0003f7
.word 0xb98002e0
.word 0xd2800000
.word 0xf9002bbf
.word 0xf9401fb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
bl _p_333
.word 0xf9401fb1
.word 0xf9409631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa1
.word 0xf94013a0
.word 0xf94006e2
.word 0xd1000442
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9401fb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9003ba0
.word 0xf94017a0
.word 0xf9003fa0
.word 0xf9400fa0
.word 0xf9400000
bl _p_476
.word 0xaa0003e2
.word 0xf9403ba0
.word 0xf9403fa1
.word 0xd63f0040
.word 0xf9401fb1
.word 0xf9413231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf90033a0
.word 0xf9401ba0
.word 0xf90037a0
.word 0xf9400fa0
.word 0xf9400000
bl _p_477
.word 0xaa0003e2
.word 0xf94033a0
.word 0xf94037a1
.word 0xd63f0040
.word 0xf9401fb1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9418e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb7
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_14a:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection
SQLite_Net_TableQuery_1_T_GSHAREDVT__ctor_SQLite_Net_Interop_ISQLitePlatform_SQLite_Net_SQLiteConnection:
.loc 1 1 0
.word 0xa9b87bfd
.word 0x910003fd
.word 0xf9000bb8
.word 0xf9000fa0
.word 0xf90013a1
.word 0xf90017a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1816]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf9400fa0
.word 0xf9400000
bl _p_478
.word 0xaa0003f8
.word 0xb9800300
.word 0xd2800000
.word 0xf90027bf
.word 0xf9401bb1
.word 0xf9405a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9407a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
bl _p_333
.word 0xf9401bb1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf940a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa1
.word 0xf94013a0
.word 0xf9400702
.word 0xd1000442
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9401bb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9003ba0
.word 0xf94017a0
.word 0xf9003fa0
.word 0xf9400fa0
.word 0xf9400000
bl _p_479
.word 0xaa0003e2
.word 0xf9403ba0
.word 0xf9403fa1
.word 0xd63f0040
.word 0xf9401bb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9002ba0
.word 0xf9400fa0
.word 0xf90037a0
.word 0xf9400fa0
.word 0xf9400000
bl _p_480
.word 0xaa0003e1
.word 0xf94037a0
.word 0xd63f0020
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400000
bl _p_481
.word 0xaa0003e1
.word 0xf94033a3
.word 0xd2800000
.word 0xaa0303e0
.word 0xd2800002
.word 0xf940007e
bl _p_110
.word 0xf9002fa0
.word 0xf9401bb1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa0
.word 0xf9400000
bl _p_482
.word 0xaa0003e2
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xd63f0040
.word 0xf9401bb1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf941f231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9420231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb8
.word 0x910003bf
.word 0xa8c87bfd
.word 0xd65f03c0

Lme_14b:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_get_Connection
SQLite_Net_TableQuery_1_T_GSHAREDVT_get_Connection:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1824]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9400fa0
.word 0xf9400000
bl _p_483
.word 0xaa0003fa
.word 0xb9800340
.word 0xd2800000
.word 0xf9001fbf
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
.word 0xf9400fa0
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_14c:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_set_Connection_SQLite_Net_SQLiteConnection
SQLite_Net_TableQuery_1_T_GSHAREDVT_set_Connection_SQLite_Net_SQLiteConnection:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xf9000fa0
.word 0xf90013a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1832]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf9400fa0
.word 0xf9400000
bl _p_484
.word 0xaa0003f9
.word 0xb9800320
.word 0xd2800000
.word 0xf90023bf
.word 0xf94017b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa1
.word 0xf94013a0
.word 0xf9400722
.word 0xd1000442
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_14d:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_get_Table
SQLite_Net_TableQuery_1_T_GSHAREDVT_get_Table:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bba
.word 0xf9000fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1840]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9400fa0
.word 0xf9400000
bl _p_485
.word 0xaa0003fa
.word 0xb9800340
.word 0xd2800000
.word 0xf9001fbf
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
.word 0xf9400fa0
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf90023a0
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf94013b1
.word 0xf940be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bba
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_14e:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_set_Table_SQLite_Net_TableMapping
SQLite_Net_TableQuery_1_T_GSHAREDVT_set_Table_SQLite_Net_TableMapping:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000bb9
.word 0xf9000fa0
.word 0xf90013a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1848]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf9400fa0
.word 0xf9400000
bl _p_486
.word 0xaa0003f9
.word 0xb9800320
.word 0xd2800000
.word 0xf90023bf
.word 0xf94017b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fa1
.word 0xf94013a0
.word 0xf9400722
.word 0xd1000442
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf940d631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400bb9
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_14f:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_GetEnumerator
SQLite_Net_TableQuery_1_T_GSHAREDVT_GetEnumerator:
.loc 1 1 0
.word 0xa9b77bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xa9026bb9
.word 0xf9001ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1856]
.word 0xf9001fb0
.word 0xf9400a11
.word 0xf90023b1
.word 0xf9401ba0
.word 0xf9400000
bl _p_487
.word 0xaa0003fa
.word 0xb9800340
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
.word 0x910003f9
.word 0xf9401fb1
.word 0xf9408631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400741
.word 0xd1000421
.word 0x8b010000
.word 0x39400000
.word 0x35000e40
.word 0xf9401fb1
.word 0xf940ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9003ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #824]
.word 0xf9003fa0
.word 0xf9401ba0
.word 0xf9400000
bl _p_488
.word 0xaa0003e2
.word 0xf9403ba0
.word 0xf9403fa1
.word 0xd63f0040
.word 0xf90037a0
.word 0xf9401fb1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400000
bl _p_489
.word 0xaa0003e1
.word 0xf94037a0
.word 0xf90033a1
.word 0xf940001e
.word 0xf9002fa0
.word 0xf9401ba0
.word 0xf9400000
bl _p_490
.word 0xaa0003e1
.word 0xf9402fa0
.word 0xf94033af
.word 0xd63f0020
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9416631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400000
bl _p_491
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9401ba0
.word 0xf9400000
bl _p_492
.word 0xaa0003e1
.word 0xf9402ba0
.word 0xb9803342
.word 0xaa1903e3
.word 0x8b020322
.word 0xaa0203e8
.word 0xd63f0020
.word 0xf9401fb1
.word 0xf941b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400b58
.word 0xd280005e
.word 0xeb1e031f
.word 0x54000360
.word 0xd280007e
.word 0xeb1e031f
.word 0x540003a0
.word 0xf9401ba0
.word 0xf9400000
bl _p_493
bl _p_440
.word 0xb9803341
.word 0xaa1903e2
.word 0x8b010321
.word 0xf90033a1
.word 0xf9002ba0
.word 0x91004000
.word 0xf9002fa0
.word 0xf9401340
.word 0xf9401740
.word 0xf9401ba0
.word 0xf9400000
bl _p_494
.word 0xaa0003e2
.word 0xf9402fa0
.word 0xf94033a1
bl _mono_gsharedvt_value_copy
.word 0xf9402ba0
.word 0xaa0003f7
.word 0x1400000c
.word 0xb9803340
.word 0xaa1903e1
.word 0x8b000320
.word 0xf9400017
.word 0x14000007
.word 0xf9400f41
.word 0xb9803340
.word 0xaa1903e2
.word 0x8b000320
.word 0xd63f0020
.word 0xaa0003f7
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9427a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1703e0
.word 0x14000044
.word 0xf9401fb1
.word 0xf9429231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf90043a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #824]
.word 0xf90047a0
.word 0xf9401ba0
.word 0xf9400000
bl _p_488
.word 0xaa0003e2
.word 0xf94043a0
.word 0xf94047a1
.word 0xd63f0040
.word 0xf9003fa0
.word 0xf9401fb1
.word 0xf942da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400000
bl _p_495
.word 0xaa0003e1
.word 0xf9403fa0
.word 0xf9003ba1
.word 0xf940001e
.word 0xf90037a0
.word 0xf9401ba0
.word 0xf9400000
bl _p_496
.word 0xaa0003e1
.word 0xf94037a0
.word 0xf9403baf
.word 0xd63f0020
.word 0xf9002fa0
.word 0xf9401fb1
.word 0xf9432a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9400000
bl _p_497
.word 0xf90033a0
.word 0xf9401ba0
.word 0xf9400000
bl _p_498
.word 0xaa0003e1
.word 0xf9402fa0
.word 0xf94033af
.word 0xd63f0020
.word 0xf9002ba0
.word 0xf9401fb1
.word 0xf9436a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9438a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401fb1
.word 0xf9439e31
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xa9426bb9
.word 0x910003bf
.word 0xa8c97bfd
.word 0xd65f03c0

Lme_150:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_System_Collections_IEnumerable_GetEnumerator
SQLite_Net_TableQuery_1_T_GSHAREDVT_System_Collections_IEnumerable_GetEnumerator:
.loc 1 1 0
.word 0xa9bb7bfd
.word 0x910003fd
.word 0xf9000ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1864]
.word 0xf9000fb0
.word 0xf9400a11
.word 0xf90013b1
.word 0xf9400ba0
.word 0xf9400000
bl _p_499
.word 0xf9001ba0
.word 0xf9401ba0
.word 0xb9800000
.word 0xd2800000
.word 0xf9001fbf
.word 0xf9400fb1
.word 0xf9405231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf90027a0
.word 0xf9400ba0
.word 0xf9400000
bl _p_500
.word 0xaa0003e1
.word 0xf94027a0
.word 0xd63f0020
.word 0xf90023a0
.word 0xf9400fb1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400fb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400fb1
.word 0xf940da31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c57bfd
.word 0xd65f03c0

Lme_151:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_Clone_U_GSHAREDVT
SQLite_Net_TableQuery_1_T_GSHAREDVT_Clone_U_GSHAREDVT:
.loc 1 1 0
.word 0xa9b17bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9003faf
.word 0xaa0003fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1872]
.word 0xf9002bb0
.word 0xf9400a11
.word 0xf9002fb1
.word 0xf9403fa0
bl _p_501
.word 0xaa0003f9
.word 0xb9800320
.word 0xd2800000
.word 0xf90043bf
.word 0xf9402bb1
.word 0xf9405e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9407e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xf9400720
.word 0xd1000400
.word 0x8b000340
.word 0xf9400000
.word 0xf9006ba0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400340
bl _p_502
.word 0xaa0003e1
.word 0xaa1a03e0
.word 0xd63f0020
.word 0xf9006fa0
.word 0xf9402bb1
.word 0xf940c631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400340
bl _p_503
.word 0xaa0003e1
.word 0xaa1a03e0
.word 0xd63f0020
.word 0xf90073a0
.word 0xf9402bb1
.word 0xf940f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
bl _p_504
bl _p_440
.word 0xf90067a0
.word 0xf9403fa0
bl _p_505
.word 0xaa0003e4
.word 0xf94067a0
.word 0xf9406ba1
.word 0xf9406fa2
.word 0xf94073a3
.word 0xf90063a0
.word 0xd63f0080
.word 0xf9402bb1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f8
.word 0xaa1803e0
.word 0xaa1803e1
.word 0xaa1a03e1
.word 0xf9400b21
.word 0xd1000421
.word 0x8b010341
.word 0xf9400021
.word 0xf9400f22
.word 0xd1000442
.word 0x8b020302
.word 0xf9000041
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xaa0003f7
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0xf9401321
.word 0xd1000421
.word 0x8b010341
.word 0x39400022
.word 0xf9401721
.word 0xd1000421
.word 0x8b010001
.word 0x39000022
.word 0xaa0003f6
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0xf9401b21
.word 0xd1000421
.word 0x8b010341
.word 0x9101c3a2
.word 0xf9400021
.word 0xf9003ba1
.word 0xf9401f21
.word 0xd1000421
.word 0x8b010001
.word 0x9101c3a2
.word 0xaa0103e2
.word 0xf9403ba2
.word 0xf9000022
.word 0xaa0003f5
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0xf9402321
.word 0xd1000421
.word 0x8b010341
.word 0x9101a3a2
.word 0xf9400021
.word 0xf90037a1
.word 0xf9402721
.word 0xd1000421
.word 0x8b010001
.word 0x9101a3a2
.word 0xaa0103e2
.word 0xf94037a2
.word 0xf9000022
.word 0xaa0003f4
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0xf9402b21
.word 0xd1000421
.word 0x8b010341
.word 0xf9400021
.word 0xf9402f22
.word 0xd1000442
.word 0x8b020002
.word 0xf9000041
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xaa0003f3
.word 0xaa0003e1
.word 0xaa0003e1
.word 0xaa1a03e1
.word 0xf9403321
.word 0xd1000421
.word 0x8b010341
.word 0xf9400021
.word 0xf9403722
.word 0xd1000442
.word 0x8b020002
.word 0xf9000041
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xf90047a0
.word 0xf94047a0
.word 0xf94047a2
.word 0xaa1a03e1
.word 0xf9403b21
.word 0xd1000421
.word 0x8b010341
.word 0xf9400021
.word 0xf9403f23
.word 0xd1000463
.word 0x8b030042
.word 0xf9000041
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xf9004ba0
.word 0xf9404ba0
.word 0xf9404ba2
.word 0xaa1a03e1
.word 0xf9404321
.word 0xd1000421
.word 0x8b010341
.word 0xf9400021
.word 0xf9404723
.word 0xd1000463
.word 0x8b030042
.word 0xf9000041
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xf9004fa0
.word 0xf9404fa0
.word 0xf9404fa2
.word 0xaa1a03e1
.word 0xf9404b21
.word 0xd1000421
.word 0x8b010341
.word 0xf9400021
.word 0xf9404f23
.word 0xd1000463
.word 0x8b030042
.word 0xf9000041
.word 0xd349fc42
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0042

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x3, [x16, #16]
.word 0x8b030042
.word 0xd280003e
.word 0x3900005e
.word 0xf90053a0
.word 0xf94053a2
.word 0xf94053a1
.word 0xaa1a03e0
.word 0xf9405320
.word 0xd1000400
.word 0x8b000340
.word 0xf9400000
.word 0xf90057a2
.word 0xf9005ba1
.word 0xb40004a0
.word 0xf94057a0
.word 0xf9006ba0
.word 0xf9405ba0
.word 0xf90067a0
.word 0xaa1a03e0
.word 0xf9405320
.word 0xd1000400
.word 0x8b000340
.word 0xf9400000
.word 0xf9006fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0x3980b410
.word 0xb5000050
bl _p_342

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9406fa1
.word 0xf90063a0
bl _p_352
.word 0xf9402bb1
.word 0xf944a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf94067a1
.word 0xf9406ba2
.word 0xf90057a2
.word 0xf9005ba1
.word 0xf9005fa0
.word 0x14000007
.word 0xf94057a1
.word 0xf9405ba0
.word 0xd2800002
.word 0xf90057a1
.word 0xf9005ba0
.word 0xf9005fbf
.word 0xf94057a0
.word 0xf90063a0
.word 0xf9405ba1
.word 0xf9405fa0
.word 0xf9405722
.word 0xd1000442
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9454231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf9402bb1
.word 0xf9455631
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8cf7bfd
.word 0xd65f03c0

Lme_152:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_Where_System_Linq_Expressions_Expression_1_System_Func_2_T_GSHAREDVT_bool
SQLite_Net_TableQuery_1_T_GSHAREDVT_Where_System_Linq_Expressions_Expression_1_System_Func_2_T_GSHAREDVT_bool:
.loc 1 1 0
.word 0xa9b67bfd
.word 0x910003fd
.word 0xa90163b7
.word 0xf90013ba
.word 0xf90017a0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1880]
.word 0xf9001bb0
.word 0xf9400a11
.word 0xf9001fb1
.word 0xf94017a0
.word 0xf9400000
bl _p_506
.word 0xf90027a0
.word 0xf94027a0
.word 0xb9800000
.word 0xd2800000
.word 0xf9002bbf
.word 0xd2800018
.word 0xf9401bb1
.word 0xf9406231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9408231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb500025a
.word 0xf9401bb1
.word 0xf9409a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2814261
.word 0xd2814261
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9401bb1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf90033a0
.word 0xf9401bb1
.word 0xf9410a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xd2800241
.word 0xd280025e
.word 0x6b1e001f
.word 0x54000240
.word 0xf9401bb1
.word 0xf9412e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28144a1
.word 0xd28144a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9401bb1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_353
.word 0xf9004ba0
.word 0xf9401bb1
.word 0xf9419631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404ba0
.word 0xaa0003f8
.word 0xf9401bb1
.word 0xf941ae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017a0
.word 0xf90043a0
.word 0xf94017a0
.word 0xf9400000
bl _p_507
.word 0xf90047a0
.word 0xf94017a0
.word 0xf9400000
bl _p_508
.word 0xaa0003e1
.word 0xf94043a0
.word 0xf94047af
.word 0xd63f0020
.word 0xf9003fa0
.word 0xf9401bb1
.word 0xf941f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa0
.word 0xaa0003f7
.word 0xaa0003e1
.word 0xf90033a1
.word 0xf90037a0
.word 0xaa1803e0
.word 0xf9003ba0
.word 0xf94017a0
.word 0xf9400000
bl _p_509
.word 0xaa0003e2
.word 0xf94037a0
.word 0xf9403ba1
.word 0xd63f0040
.word 0xf9401bb1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401fb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9425e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf9401bb1
.word 0xf9427231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94163b7
.word 0xf94013ba
.word 0x910003bf
.word 0xa8ca7bfd
.word 0xd65f03c0

Lme_153:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_Take_int
SQLite_Net_TableQuery_1_T_GSHAREDVT_Take_int:
.loc 1 1 0
.word 0xa9b47bfd
.word 0x910003fd
.word 0xa9015fb6
.word 0xa90267b8
.word 0xf9001ba0
.word 0xf9001fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1888]
.word 0xf90023b0
.word 0xf9400a11
.word 0xf90027b1
.word 0xf9401ba0
.word 0xf9400000
bl _p_510
.word 0xaa0003f9
.word 0xb9800320
.word 0xd2800000
.word 0xf9003fbf
.word 0xd2800018
.word 0x9101c3a0
.word 0xd2800000
.word 0xf9003ba0
.word 0xf94023b1
.word 0xf9406a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf9408a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401ba0
.word 0xf9005ba0
.word 0xf9401ba0
.word 0xf9400000
bl _p_511
.word 0xf9005fa0
.word 0xf9401ba0
.word 0xf9400000
bl _p_512
.word 0xaa0003e1
.word 0xf9405ba0
.word 0xf9405faf
.word 0xd63f0020
.word 0xf90057a0
.word 0xf94023b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
.word 0xf90053a0
.word 0xaa0003f8
.word 0xf94023b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xaa0003e1
.word 0xf90047a1
.word 0xaa0003e1
.word 0xf9400721
.word 0xd1000421
.word 0x8b010000
.word 0x910183a1
.word 0xf9400000
.word 0xf90033a0
.word 0x910183a0
.word 0x9101c3a0
.word 0xf94033a0
.word 0xf9003ba0
.word 0x9101c3a0
.word 0xf9004ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf9004fa0
.word 0xf9401ba0
.word 0xf9400000
bl _p_513
.word 0xaa0003e1
.word 0xf9404ba0
.word 0xf9404faf
.word 0xd63f0020
.word 0x53001c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9417231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xf94047a1
.word 0xaa0103f7
.word 0x350000e0
.word 0xaa1703e0
.word 0xd29fffe0
.word 0xf2afffe0
.word 0xd29ffff6
.word 0xf2affff6
.word 0x14000017
.word 0xaa1703e0
.word 0x9101c3a0
.word 0xf90047a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf9004ba0
.word 0xf9401ba0
.word 0xf9400000
bl _p_514
.word 0xaa0003e1
.word 0xf94047a0
.word 0xf9404baf
.word 0xd63f0020
.word 0x93407c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a0
.word 0xaa0003f6
.word 0xaa1703e0
.word 0xaa1603e0
.word 0xb9803ba1
.word 0xaa1603e0
bl _p_359
.word 0x93407c00
.word 0xf90043a0
.word 0xf94023b1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0x9101a3a0
.word 0xd2800000
.word 0xf90037a0
.word 0x9101a3a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x15, [x16, #1192]
bl _p_73
.word 0x9101a3a0
.word 0x910163a0
.word 0xf94037a0
.word 0xf9002fa0
.word 0xf94023b1
.word 0xf9426231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400720
.word 0xd1000400
.word 0x8b0002e0
.word 0x910163a1
.word 0xaa0003e1
.word 0xf9402fa1
.word 0xf9000001
.word 0xf94023b1
.word 0xf9428e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94027b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023b1
.word 0xf942b231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94023b1
.word 0xf942c631
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415fb6
.word 0xa94267b8
.word 0x910003bf
.word 0xa8cc7bfd
.word 0xd65f03c0

Lme_154:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_Delete_System_Linq_Expressions_Expression_1_System_Func_2_T_GSHAREDVT_bool
SQLite_Net_TableQuery_1_T_GSHAREDVT_Delete_System_Linq_Expressions_Expression_1_System_Func_2_T_GSHAREDVT_bool:
.loc 1 1 0
.word 0xa9ae7bfd
.word 0x910003fd
.word 0xa9015bb5
.word 0xa90263b7
.word 0xa9036bb9
.word 0xf90023a0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1896]
.word 0xf90027b0
.word 0xf9400a11
.word 0xf9002bb1
.word 0xf94023a0
.word 0xf9400000
bl _p_515
.word 0xaa0003f9
.word 0xb9800320
.word 0xd2800000
.word 0xf90033bf
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xf94027b1
.word 0xf9406e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb500025a
.word 0xf94027b1
.word 0xf940a631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2814261
.word 0xd2814261
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94027b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf9400341
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xd2800241
.word 0xd280025e
.word 0x6b1e001f
.word 0x54000240
.word 0xf94027b1
.word 0xf9413a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28144a1
.word 0xd28144a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94027b1
.word 0xf9417e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xeb1f001f
.word 0x10000011
.word 0x54002aa0
.word 0xf9400721
.word 0xd1000421
.word 0x8b010000
.word 0xf9003fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf90043a0
.word 0xf94023a0
.word 0xf9400000
bl _p_516
.word 0xaa0003e1
.word 0xf9403fa0
.word 0xf94043af
.word 0xd63f0020
.word 0x53001c00
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf941e231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0x34000240
.word 0xf94027b1
.word 0xf941fa31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28149a1
.word 0xd28149a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94027b1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xeb1f001f
.word 0x10000011
.word 0x540024a0
.word 0xf9400b21
.word 0xd1000421
.word 0x8b010000
.word 0xf9003fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf90043a0
.word 0xf94023a0
.word 0xf9400000
bl _p_516
.word 0xaa0003e1
.word 0xf9403fa0
.word 0xf94043af
.word 0xd63f0020
.word 0x53001c00
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf942a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0x34000240
.word 0xf94027b1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28154a1
.word 0xd28154a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94027b1
.word 0xf942fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_353
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf9432231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003f8
.word 0xf94027b1
.word 0xf9433a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf9400f21
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb4000280
.word 0xf94027b1
.word 0xf9436231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xf94023a0
.word 0xf9400f21
.word 0xd1000421
.word 0x8b010000
.word 0xf9400001
.word 0xaa1803e0
bl _p_360
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf9439631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xaa0003f8
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf943be31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf9008fa0
bl _p_361
.word 0xf94027b1
.word 0xf9440631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9408fa0
.word 0xaa0003f7
.word 0xf94027b1
.word 0xf9441e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf90087a0
.word 0xaa1803e0
.word 0xaa1703e0
.word 0xf9008ba0
.word 0xf94023a0
.word 0xf9400000
bl _p_517
.word 0xaa0003e3
.word 0xf94087a0
.word 0xf9408ba2
.word 0xaa1803e1
.word 0xd63f0060
.word 0xf90083a0
.word 0xf94027b1
.word 0xf9446631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a0
.word 0xaa0003f6
.word 0xf94027b1
.word 0xf9447e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xf90073a0
.word 0xf94023a0
.word 0xf9007fa0
.word 0xf94023a0
.word 0xf9400000
bl _p_518
.word 0xaa0003e1
.word 0xf9407fa0
.word 0xd63f0020
.word 0xf9007ba0
.word 0xf94027b1
.word 0xf944c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf90077a0
.word 0xf94027b1
.word 0xf944e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xf94077a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #512]
bl _p_30
.word 0xf9006fa0
.word 0xf94027b1
.word 0xf9451231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xf9006ba0
.word 0xaa0003f5
.word 0xf94027b1
.word 0xf9452e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf9005ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #888]
.word 0xf9005fa0
.word 0xaa1603e0
.word 0xf90067a0
.word 0xf94023a0
.word 0xf9400000
bl _p_519
.word 0xaa0003e1
.word 0xf94067a0
.word 0xd63f0020
.word 0xf90063a0
.word 0xf94027b1
.word 0xf9457a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405ba0
.word 0xf9405fa1
.word 0xf94063a2
bl _p_30
.word 0xf90057a0
.word 0xf94027b1
.word 0xf9459e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
.word 0xaa0003f5
.word 0xf94027b1
.word 0xf945b631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94023a0
.word 0xf90053a0
.word 0xf94023a0
.word 0xf9400000
bl _p_520
.word 0xaa0003e1
.word 0xf94053a0
.word 0xd63f0020
.word 0xf9004ba0
.word 0xf94027b1
.word 0xf945ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xf90043a0
.word 0xaa1703e0
.word 0xf9004fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf94023a0
.word 0xf9400000
bl _p_521
.word 0xaa0003e1
.word 0xf9404fa0
.word 0xd63f0020
.word 0xf90047a0
.word 0xf94027b1
.word 0xf9463e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94043a1
.word 0xf94047a2
.word 0xf9404ba3
.word 0xaa0303e0
.word 0xf940007e
bl _p_243
.word 0xf9003fa0
.word 0xf94027b1
.word 0xf9466a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403fa1
.word 0xaa0103e0
.word 0xf940003e
bl _p_245
.word 0x93407c00
.word 0xf9003ba0
.word 0xf94027b1
.word 0xf9469231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94027b1
.word 0xf946b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403ba0
.word 0xf94027b1
.word 0xf946c631
.word 0xb4000051
.word 0xd63f0220
.word 0xa9415bb5
.word 0xa94263b7
.word 0xa9436bb9
.word 0x910003bf
.word 0xa8d27bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_155:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_OrderBy_TValue_GSHAREDVT_System_Linq_Expressions_Expression_1_System_Func_2_T_GSHAREDVT_TValue_GSHAREDVT
SQLite_Net_TableQuery_1_T_GSHAREDVT_OrderBy_TValue_GSHAREDVT_System_Linq_Expressions_Expression_1_System_Func_2_T_GSHAREDVT_TValue_GSHAREDVT:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xf9001faf
.word 0xf9000ba0
.word 0xf9000fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1904]
.word 0xf90013b0
.word 0xf9400a11
.word 0xf90017b1
.word 0xf9401fa0
bl _p_522
.word 0xf90023a0
.word 0xf94023a0
.word 0xb9800000
.word 0xd2800000
.word 0xf90027bf
.word 0xf94013b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9400ba0
.word 0xf9002fa0
.word 0xf9400fa0
.word 0xf90033a0
.word 0xd2800020
.word 0xf9401fa0
bl _p_523
.word 0xf90037a0
.word 0xf9401fa0
bl _p_524
.word 0xaa0003e3
.word 0xf9402fa0
.word 0xf94033a1
.word 0xf94037af
.word 0xd2800022
.word 0xd63f0060
.word 0xf9002ba0
.word 0xf94013b1
.word 0xf940ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013b1
.word 0xf940ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf94013b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0

Lme_156:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_AddOrderBy_TValue_GSHAREDVT_System_Linq_Expressions_Expression_1_System_Func_2_T_GSHAREDVT_TValue_GSHAREDVT_bool
SQLite_Net_TableQuery_1_T_GSHAREDVT_AddOrderBy_TValue_GSHAREDVT_System_Linq_Expressions_Expression_1_System_Func_2_T_GSHAREDVT_TValue_GSHAREDVT_bool:
.loc 1 1 0
.word 0xa9af7bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9003baf
.word 0xaa0003f8
.word 0xaa0103f9
.word 0xf9002ba2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1912]
.word 0xf9002fb0
.word 0xf9400a11
.word 0xf90033b1
.word 0xf9403ba0
bl _p_525
.word 0xaa0003f7
.word 0xb98002e0
.word 0xd2800000
.word 0xf9003fbf
.word 0xd2800016
.word 0xd2800015
.word 0xd2800014
.word 0xd2800013
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
.word 0xaa1903e0
.word 0xb5000259
.word 0xf9402fb1
.word 0xf940ae31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28165a1
.word 0xd28165a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402fb1
.word 0xf940f231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf9400321
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf90063a0
.word 0xf9402fb1
.word 0xf9411e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xd2800241
.word 0xd280025e
.word 0x6b1e001f
.word 0x54000240
.word 0xf9402fb1
.word 0xf9414231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28144a1
.word 0xd28144a1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402fb1
.word 0xf9418631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903f6
.word 0xf9402fb1
.word 0xf9419e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1903e0
.word 0xaa1903e0
.word 0xf940033e
bl _p_353
.word 0xaa0003fa
.word 0xf9402fb1
.word 0xf941c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf90043ba
.word 0xeb1f035f
.word 0x54000160
.word 0xf9400340
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #912]
.word 0xeb01001f
.word 0x54000040
.word 0xf90043bf
.word 0xf94043a0
.word 0xaa0003f4
.word 0xf9402fb1
.word 0xf9420e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xb4000734
.word 0xf9402fb1
.word 0xf9422631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xaa1403e0
.word 0xf9400281
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf90063a0
.word 0xf9402fb1
.word 0xf9425231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xd2800141
.word 0xd280015e
.word 0x6b1e001f
.word 0x540004a1
.word 0xf9402fb1
.word 0xf9427631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xaa1403e0
.word 0xf940029e
bl _p_366
.word 0xf90057a0
.word 0xf9402fb1
.word 0xf9429a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94057a0
.word 0xf9005ba0
.word 0xf94057a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf94057a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #920]
.word 0xeb01001f
.word 0x54000040
.word 0xf9005bbf
.word 0xf9405ba0
.word 0xaa0003f5
.word 0xf9402fb1
.word 0xf942f231
.word 0xb4000051
.word 0xd63f0220
.word 0x14000024
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9431631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1603e0
.word 0xf94002de
bl _p_353
.word 0xf90047a0
.word 0xf9402fb1
.word 0xf9433a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94047a0
.word 0xf9004ba0
.word 0xf94047a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf94047a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #920]
.word 0xeb01001f
.word 0x54000040
.word 0xf9004bbf
.word 0xf9404ba0
.word 0xaa0003f5
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xb40003d5
.word 0xf9402fb1
.word 0xf943ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1503e0
.word 0xf94002be
bl _p_367
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf943de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf90063a0
.word 0xf9402fb1
.word 0xf9440a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xd28004c1
.word 0xd28004de
.word 0x6b1e001f
.word 0x540003c0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf9443e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2816821
.word 0xd2816821
bl _p_32
.word 0xaa1903e1
.word 0xaa1903e1
bl _p_63
.word 0xf90063a0
.word 0xf9402fb1
.word 0xf9447631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9402fb1
.word 0xf944a231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400300
bl _p_526
.word 0xf9006ba0
.word 0xaa1803e0
.word 0xf9400300
bl _p_527
.word 0xaa0003e1
.word 0xf9406baf
.word 0xaa1803e0
.word 0xd63f0020
.word 0xf90067a0
.word 0xf9402fb1
.word 0xf944e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a0
.word 0xf90063a0
.word 0xaa0003f3
.word 0xf9402fb1
.word 0xf9450231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003e1
.word 0xf94006e1
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb50004e0
.word 0xf9402fb1
.word 0xf9452e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0x3980b410
.word 0xb5000050
bl _p_342

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90063a0
bl _p_369
.word 0xf9402fb1
.word 0xf9457a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf94006e1
.word 0xd1000421
.word 0x8b010261
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf945d631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0xf94006e0
.word 0xd1000400
.word 0x8b000260
.word 0xf9400000
.word 0xf90063a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #928]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90087a0
bl _p_370
.word 0xf9402fb1
.word 0xf9461e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94087a0
.word 0xf9004fa0
.word 0xf9404fa0
.word 0xf9006ba0
.word 0xf9404fa0
.word 0xf90073a0
.word 0xaa1803e0
.word 0xaa1803e0
.word 0xf9400300
bl _p_528
.word 0xaa0003e1
.word 0xaa1803e0
.word 0xd63f0020
.word 0xf9007fa0
.word 0xf9402fb1
.word 0xf9466631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1503e0
.word 0xaa1503e0
.word 0xf94002be
bl _p_371
.word 0xf90083a0
.word 0xf9402fb1
.word 0xf9468a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94083a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf9007ba0
.word 0xf9402fb1
.word 0xf946b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba1
.word 0xf9407fa2
.word 0xaa0203e0
.word 0xf940005e
bl _p_372
.word 0xf90077a0
.word 0xf9402fb1
.word 0xf946da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_29
.word 0xf9006fa0
.word 0xf9402fb1
.word 0xf946fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa1
.word 0xf94073a2
.word 0xaa0203e0
.word 0xf940005e
bl _p_373
.word 0xf9402fb1
.word 0xf9472231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf90053a0
.word 0xf94053a0
.word 0xf90067a0
.word 0xf94053a2
.word 0x394143a1
.word 0xaa0203e0
.word 0xf940005e
bl _p_374
.word 0xf9402fb1
.word 0xf9475631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xaa1803e0
.word 0xf9400300
bl _p_529
.word 0xaa0003e2
.word 0xf94063a0
.word 0xf94067a1
.word 0xd63f0040
.word 0xf9402fb1
.word 0xf9479a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf947aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0xf94033b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fb1
.word 0xf947ce31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1303e0
.word 0xf9402fb1
.word 0xf947e231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d17bfd
.word 0xd65f03c0

Lme_157:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_AddWhere_System_Linq_Expressions_Expression
SQLite_Net_TableQuery_1_T_GSHAREDVT_AddWhere_System_Linq_Expressions_Expression:
.loc 1 1 0
.word 0xa9b97bfd
.word 0x910003fd
.word 0xa9016bb9
.word 0xf90013a0
.word 0xaa0103fa

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1920]
.word 0xf90017b0
.word 0xf9400a11
.word 0xf9001bb1
.word 0xf94013a0
.word 0xf9400000
bl _p_530
.word 0xaa0003f9
.word 0xb9800320
.word 0xd2800000
.word 0xf90023bf
.word 0xf94017b1
.word 0xf9405631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9407631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xb500025a
.word 0xf94017b1
.word 0xf9408e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2816f21
.word 0xd2816f21
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94017b1
.word 0xf940d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xeb1f001f
.word 0x10000011
.word 0x540013c0
.word 0xf9400721
.word 0xd1000421
.word 0x8b010000
.word 0xf9002fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf90033a0
.word 0xf94013a0
.word 0xf9400000
bl _p_531
.word 0xaa0003e1
.word 0xf9402fa0
.word 0xf94033af
.word 0xd63f0020
.word 0x53001c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf9413631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x35000400
.word 0xf94017b1
.word 0xf9414e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000fe0
.word 0xf9400b21
.word 0xd1000421
.word 0x8b010000
.word 0xf9002fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf90033a0
.word 0xf94013a0
.word 0xf9400000
bl _p_531
.word 0xaa0003e1
.word 0xf9402fa0
.word 0xf94033af
.word 0xd63f0020
.word 0x53001c00
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf941b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0x340002c0
.word 0xf9401bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf941da31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2817061
.word 0xd2817061
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94017b1
.word 0xf9421e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf9400f21
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb5000340
.word 0xf94017b1
.word 0xf9424631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xaa1a03e1
.word 0xf9400f21
.word 0xd1000421
.word 0x8b010000
.word 0xf900001a
.word 0xd349fc00
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0000

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #16]
.word 0x8b010000
.word 0xd280003e
.word 0x3900001e
.word 0xf94017b1
.word 0xf9429631
.word 0xb4000051
.word 0xd63f0220
.word 0x14000028
.word 0xf94017b1
.word 0xf942aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94013a0
.word 0xf9002fa0
.word 0xf94013a0
.word 0xf9400f21
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xaa1a03e1
.word 0xaa1a03e1
bl _p_360
.word 0xf9002ba0
.word 0xf94017b1
.word 0xf942e631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9402fa1
.word 0xf9400f22
.word 0xd1000442
.word 0x8b020021
.word 0xf9000020
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf94017b1
.word 0xf9433631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94017b1
.word 0xf9434631
.word 0xb4000051
.word 0xd63f0220
.word 0xa9416bb9
.word 0x910003bf
.word 0xa8c77bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27

Lme_158:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_GenerateCommand_string
SQLite_Net_TableQuery_1_T_GSHAREDVT_GenerateCommand_string:
.loc 1 1 0
.word 0xa9ae7bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1928]
.word 0xf90033b0
.word 0xf9400a11
.word 0xf90037b1
.word 0xf9402ba0
.word 0xf9400000
bl _p_532
.word 0xaa0003f9
.word 0xb9800320
.word 0xd2800000
.word 0xf9003fbf
.word 0xd2800018
.word 0xd2800017
.word 0xd2800016
.word 0xd2800015
.word 0xf94033b1
.word 0xf9407231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9409231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb5000240
.word 0xf94033b1
.word 0xf940aa31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2817aa1
.word 0xd2817aa1
bl _p_32
.word 0xaa0003e1
.word 0xd2801160
.word 0xf2a04000
.word 0xd2801160
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94033b1
.word 0xf940ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400721
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb4000380
.word 0xf94033b1
.word 0xf9411631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400b21
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb4000240
.word 0xf94033b1
.word 0xf9413e31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd2817e21
.word 0xd2817e21
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf9419231
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xaa0003f4
.word 0xaa1403e0
.word 0xf9008ba0
.word 0xaa1403e0
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #952]
.word 0xaa1403e0
.word 0xd2800001
.word 0xf9400283
.word 0xf9407870
.word 0xd63f0200
.word 0xf9408ba0
.word 0xaa0003f3
.word 0xaa1303e0
.word 0xf90087a0
.word 0xaa1303e0
.word 0xd2800020
.word 0xf9402fa2
.word 0xaa1303e0
.word 0xd2800021
.word 0xf9400263
.word 0xf9407870
.word 0xd63f0200
.word 0xf94087a0
.word 0xaa0003fa
.word 0xaa1a03e0
.word 0xf90083a0
.word 0xaa1a03e0
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #960]
.word 0xaa1a03e0
.word 0xd2800041
.word 0xf9400343
.word 0xf9407870
.word 0xd63f0200
.word 0xf94083a0
.word 0xf90043a0
.word 0xf94043a0
.word 0xf9006fa0
.word 0xf94043a0
.word 0xf90077a0
.word 0xd2800060
.word 0xf9402ba0
.word 0xf9007fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_533
.word 0xaa0003e1
.word 0xf9407fa0
.word 0xd63f0020
.word 0xf9007ba0
.word 0xf94033b1
.word 0xf9429631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9407ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_17
.word 0xf90073a0
.word 0xf94033b1
.word 0xf942ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a2
.word 0xf94077a3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406fa0
.word 0xf90047a0
.word 0xf94047a0
.word 0xf9006ba0
.word 0xf94047a3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #512]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9406ba0
bl _p_47
.word 0xf90067a0
.word 0xf94033b1
.word 0xf9432a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a0
.word 0xaa0003f8
.word 0xf94033b1
.word 0xf9434231
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0xd2800401
.word 0xd2800401
bl _p_20
.word 0xf90063a0
bl _p_361
.word 0xf94033b1
.word 0xf9438a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f7
.word 0xf94033b1
.word 0xf943a231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400f21
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb40007e0
.word 0xf94033b1
.word 0xf943ca31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9007ba0
.word 0xf9402ba0
.word 0xf9400f21
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9007fa0
.word 0xaa1703e0
.word 0xf9402ba0
.word 0xf9400000
bl _p_534
.word 0xaa0003e3
.word 0xf9407ba0
.word 0xf9407fa1
.word 0xaa1703e2
.word 0xd63f0060
.word 0xf90077a0
.word 0xf94033b1
.word 0xf9442231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94077a0
.word 0xf90073a0
.word 0xaa0003f6
.word 0xf94033b1
.word 0xf9443e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94073a0
.word 0xaa1803e1

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #888]
.word 0xf90067a1
.word 0xf9006fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_535
.word 0xaa0003e1
.word 0xf9406fa0
.word 0xd63f0020
.word 0xf9006ba0
.word 0xf94033b1
.word 0xf9448631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a1
.word 0xf9406ba2
.word 0xaa1803e0
bl _p_30
.word 0xf90063a0
.word 0xf94033b1
.word 0xf944aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf944d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401321
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xb4001e60
.word 0xf94033b1
.word 0xf944fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9401321
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf90067a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #848]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_536
.word 0xaa0003e1
.word 0xf94067a0
.word 0xd63f0020
.word 0x93407c00
.word 0xf90063a0
.word 0xf94033b1
.word 0xf9455a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xd2800001
.word 0x6b1f001f
.word 0x54001a6d
.word 0xf94033b1
.word 0xf9457a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #968]
.word 0xf9006ba0
.word 0xf9402ba0
.word 0xf9401321
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf90067a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_537
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_538
.word 0xf90063a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_539
.word 0xaa0003e1
.word 0xf94063a0
.word 0xf94067a2
.word 0xf9406ba3
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9004ba0
.word 0xf9404ba1
.word 0xf9404ba0
.word 0xf9004fa3
.word 0xf90053a2
.word 0xf90057a1
.word 0xb5000ca0
.word 0xf9404fa0
.word 0xf9006ba0
.word 0xf94053a0
.word 0xf90067a0
.word 0xf94057a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_537
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_538
.word 0xf90083a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_540
.word 0xaa0003e1
.word 0xf94083a0
.word 0xd1000421
.word 0x8b010000
.word 0xf9400000
.word 0xf9007fa0
.word 0xeb1f001f
.word 0x10000011
.word 0x540037a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #976]
.word 0xd2800e01
.word 0xd2800e01
bl _p_20
.word 0xaa0003e1
.word 0xf9407fa0
.word 0xf9001020
.word 0xf9007ba1
.word 0x91008021
.word 0xd349fc21
.word 0xd29ffffe
.word 0xf2a00ffe
.word 0x8a1e0021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #16]
.word 0x8b020021
.word 0xd280003e
.word 0x3900003e
.word 0xf9402ba0
.word 0xf9400000
bl _p_541
.word 0xaa0003e1
.word 0xf9407ba0
.word 0xf9001401
.word 0xf90077a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_542
.word 0xaa0003e1
.word 0xf94077a0
.word 0xf9002001

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #984]
.word 0xf9401422
.word 0xf9000c02
.word 0xf9401021
.word 0xf9000801
.word 0xd2800001
.word 0x3901801f
.word 0xf9005ba0
.word 0xf9405ba0
.word 0xf90063a0
.word 0xf9405ba0
.word 0xf90073a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_537
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_538
.word 0xf9006fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_539
.word 0xaa0003e5
.word 0xf94063a0
.word 0xf94067a1
.word 0xf9406ba2
.word 0xf9406fa3
.word 0xf94073a4
.word 0xd10004a5
.word 0x8b050063
.word 0xf9000064
.word 0xf9004fa2
.word 0xf90053a1
.word 0xf90057a0
.word 0xf9404fa0
.word 0xf9006fa0
.word 0xf94053a0
.word 0xf9007fa0
.word 0xf94057a0
.word 0xf90083a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #992]
.word 0xf90087a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_543
.word 0xaa0003e2
.word 0xf9407fa0
.word 0xf94083a1
.word 0xf94087af
.word 0xd63f0040
.word 0xf90077a0
.word 0xf94033b1
.word 0xf947fa31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #432]
.word 0xf9007ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_544
.word 0xaa0003e1
.word 0xf94077a0
.word 0xf9407baf
.word 0xd63f0020
.word 0xf90073a0
.word 0xf94033b1
.word 0xf9483a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406fa0
.word 0xf94073a1
bl _p_23
.word 0xf9006ba0
.word 0xf94033b1
.word 0xf9485a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9406ba0
.word 0xf90067a0
.word 0xaa0003f5
.word 0xf94033b1
.word 0xf9487631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a2
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1000]
.word 0xaa0203e0
.word 0xaa1803e0
bl _p_30
.word 0xf90063a0
.word 0xf94033b1
.word 0xf948aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf948d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54002400
.word 0xf9401721
.word 0xd1000421
.word 0x8b010000
.word 0xf90067a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf9006ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_545
.word 0xaa0003e1
.word 0xf94067a0
.word 0xf9406baf
.word 0xd63f0020
.word 0x53001c00
.word 0xf90063a0
.word 0xf94033b1
.word 0xf9493631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x340006c0
.word 0xf94033b1
.word 0xf9494e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1008]
.word 0xf9006ba0
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001f80
.word 0xf9401721
.word 0xd1000421
.word 0x8b010000
.word 0xf9006fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf90073a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_546
.word 0xaa0003e1
.word 0xf9406fa0
.word 0xf94073af
.word 0xd63f0020
.word 0x93407c00
.word 0xf90067a0
.word 0xf94033b1
.word 0xf949c631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf94067a0
.word 0xf9406ba1
.word 0xb9001040
.word 0xaa1803e0
bl _p_62
.word 0xf90063a0
.word 0xf94033b1
.word 0xf94a0a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf94a3231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001900
.word 0xf9401b21
.word 0xd1000421
.word 0x8b010000
.word 0xf90067a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf9006ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_545
.word 0xaa0003e1
.word 0xf94067a0
.word 0xf9406baf
.word 0xd63f0020
.word 0x53001c00
.word 0xf90063a0
.word 0xf94033b1
.word 0xf94a9631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x34000d40
.word 0xf94033b1
.word 0xf94aae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54001520
.word 0xf9401721
.word 0xd1000421
.word 0x8b010000
.word 0xf90067a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf9006ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_545
.word 0xaa0003e1
.word 0xf94067a0
.word 0xf9406baf
.word 0xd63f0020
.word 0x53001c00
.word 0xf90063a0
.word 0xf94033b1
.word 0xf94b1231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0x35000240
.word 0xf94033b1
.word 0xf94b2a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1016]
.word 0xaa1803e0
bl _p_49
.word 0xf90063a0
.word 0xf94033b1
.word 0xf94b5631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf94b7e31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1024]
.word 0xf9006ba0
.word 0xf9402ba0
.word 0xeb1f001f
.word 0x10000011
.word 0x54000e00
.word 0xf9401b21
.word 0xd1000421
.word 0x8b010000
.word 0xf9006fa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1192]
.word 0xf90073a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_546
.word 0xaa0003e1
.word 0xf9406fa0
.word 0xf94073af
.word 0xd63f0020
.word 0x93407c00
.word 0xf90067a0
.word 0xf94033b1
.word 0xf94bf631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1208]
.word 0xd2800281
.word 0xd2800281
bl _p_20
.word 0xaa0003e2
.word 0xf94067a0
.word 0xf9406ba1
.word 0xb9001040
.word 0xaa1803e0
bl _p_62
.word 0xf90063a0
.word 0xf94033b1
.word 0xf94c3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xaa0003f8
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf94c6231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9006fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_547
.word 0xaa0003e1
.word 0xf9406fa0
.word 0xd63f0020
.word 0xf9006ba0
.word 0xf94033b1
.word 0xf94c9631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1803e0
.word 0xaa1703e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #3960]
.word 0x3980b410
.word 0xb5000050
bl _p_342
.word 0xf9402ba0
.word 0xf9400000
bl _p_548
.word 0xaa0003e1
.word 0xaa1703e0
.word 0xd63f0020
.word 0xf90067a0
.word 0xf94033b1
.word 0xf94ce231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94067a2
.word 0xf9406ba3
.word 0xaa0303e0
.word 0xaa1803e1
.word 0xf940007e
bl _p_243
.word 0xf90063a0
.word 0xf94033b1
.word 0xf94d0e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033b1
.word 0xf94d2e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf94033b1
.word 0xf94d4231
.word 0xb4000051
.word 0xd63f0220
.word 0xa94153b3
.word 0xa9425bb5
.word 0xa94363b7
.word 0xa9446bb9
.word 0x910003bf
.word 0xa8d27bfd
.word 0xd65f03c0
.word 0xd2801e60
.word 0xaa1103e1
bl _p_27
.word 0xd2801140
.word 0xaa1103e1
bl _p_27

Lme_159:
.text
	.align 4
	.no_dead_strip SQLite_Net_TableQuery_1_T_GSHAREDVT_CompileExpr_System_Linq_Expressions_Expression_System_Collections_Generic_List_1_object
SQLite_Net_TableQuery_1_T_GSHAREDVT_CompileExpr_System_Linq_Expressions_Expression_System_Collections_Generic_List_1_object:
.loc 1 1 0
.word 0xd2809010
.word 0x910003f1
.word 0xcb100231
.word 0x9100023f
.word 0xa9007bfd
.word 0x910003fd
.word 0xa90153b3
.word 0xa9025bb5
.word 0xa90363b7
.word 0xa9046bb9
.word 0xf9002ba0
.word 0xf9002fa1
.word 0xf90033a2

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x16, [x16, #1936]
.word 0xf90037b0
.word 0xf9400a11
.word 0xf9003bb1
.word 0xf9402ba0
.word 0xf9400000
bl _p_549
.word 0xf90043a0
.word 0xf94043a0
.word 0xb9800000
.word 0xd2800000
.word 0xf90047bf
.word 0xf9004bbf
.word 0xf9004fbf
.word 0xf90053bf
.word 0xf90057bf
.word 0xf9005bbf
.word 0xf9005fbf
.word 0xf90063bf
.word 0xd280001a
.word 0xd2800014
.word 0xd2800015
.word 0xd2800017
.word 0xd2800016
.word 0xf90067bf
.word 0xf9006bbf
.word 0xf9006fbf
.word 0xf90073bf
.word 0xf90077bf
.word 0xf9007bbf
.word 0xf9007fbf
.word 0xf90083bf
.word 0xf90087bf
.word 0xf9008bbf
.word 0xf9008fbf
.word 0xf90093bf
.word 0xf90097bf
.word 0xf94037b1
.word 0xf940de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf940fe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xb50002c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9412631
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #0]
.word 0xd28192e1
.word 0xd28192e1
bl _p_32
.word 0xaa0003e1
.word 0xd2801e40
.word 0xf2a04000
.word 0xd2801e40
.word 0xf2a04000
bl _mono_create_corlib_exception_1
bl _p_33
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9417a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9009ba0
.word 0xf9409ba0
.word 0xf9009fa0
.word 0xf9409ba0
.word 0xeb1f001f
.word 0x54000180
.word 0xf9409ba0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1040]
.word 0xeb01001f
.word 0x54000040
.word 0xf9009fbf
.word 0xf9409fa0
.word 0xb4003c60
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf941ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf901c7a0
.word 0xf941c7a0
.word 0xb4000180
.word 0xf941c7a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1040]
.word 0xeb01001f
.word 0x10000011
.word 0x54021321
.word 0xf941c7a0
.word 0xf9004ba0
.word 0xf94037b1
.word 0xf9423e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9021ba0
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_382
.word 0xf9021fa0
.word 0xf94037b1
.word 0xf9426a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf90223a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_550
.word 0xaa0003e3
.word 0xf9421ba0
.word 0xf9421fa1
.word 0xf94223a2
.word 0xd63f0060
.word 0xf90217a0
.word 0xf94037b1
.word 0xf942a631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94217a0
.word 0xf9004fa0
.word 0xf94037b1
.word 0xf942be31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9020ba0
.word 0xf9404ba1
.word 0xaa0103e0
.word 0xf940003e
bl _p_383
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf942ea31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf90213a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_550
.word 0xaa0003e3
.word 0xf9420ba0
.word 0xf9420fa1
.word 0xf94213a2
.word 0xd63f0060
.word 0xf90207a0
.word 0xf94037b1
.word 0xf9432631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94207a0
.word 0xf90053a0
.word 0xf94037b1
.word 0xf9433e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf90203a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf94203a0
.word 0xd63f0020
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9437231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #528]
bl _p_332
.word 0x53001c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9439e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0x34000740
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf943c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9404fa0
.word 0xf901ffa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_552
.word 0xaa0003e1
.word 0xf941ffa0
.word 0xd63f0020
.word 0xf901fba0
.word 0xf94037b1
.word 0xf943fa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xb5000460
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9442231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf901ffa0
.word 0xf9404ba0
.word 0xf90203a0
.word 0xf94053a0
.word 0xf90207a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_553
.word 0xaa0003e3
.word 0xf941ffa0
.word 0xf94203a1
.word 0xf94207a2
.word 0xd63f0060
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9446e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf90057a0
.word 0xf94037b1
.word 0xf9448631
.word 0xb4000051
.word 0xd63f0220
.word 0x140000fe
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf944aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf90203a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf94203a0
.word 0xd63f0020
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf944de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #528]
bl _p_332
.word 0x53001c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9450a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0x34000740
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9453231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94053a0
.word 0xf901ffa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_552
.word 0xaa0003e1
.word 0xf941ffa0
.word 0xd63f0020
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9456631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xb5000460
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9458e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf901ffa0
.word 0xf9404ba0
.word 0xf90203a0
.word 0xf9404fa0
.word 0xf90207a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_553
.word 0xaa0003e3
.word 0xf941ffa0
.word 0xf94203a1
.word 0xf94207a2
.word 0xd63f0060
.word 0xf901fba0
.word 0xf94037b1
.word 0xf945da31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf90057a0
.word 0xf94037b1
.word 0xf945f231
.word 0xb4000051
.word 0xd63f0220
.word 0x140000a3
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9461631
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000e0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000e1
bl _p_16
.word 0xf901cba0
.word 0xf941cba0
.word 0xf9023fa0
.word 0xf941cba3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9423fa0
.word 0xf901cfa0
.word 0xf941cfa0
.word 0xf9022fa0
.word 0xf941cfa0
.word 0xf90237a0
.word 0xd2800020
.word 0xf9404fa0
.word 0xf9023ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf9423ba0
.word 0xd63f0020
.word 0xf90233a0
.word 0xf94037b1
.word 0xf946b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94233a2
.word 0xf94237a3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9422fa0
.word 0xf901d3a0
.word 0xf941d3a0
.word 0xf9022ba0
.word 0xf941d3a3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #896]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf9422ba0
.word 0xf901d7a0
.word 0xf941d7a0
.word 0xf90217a0
.word 0xf941d7a0
.word 0xf9021fa0
.word 0xd2800060
.word 0xf9402ba0
.word 0xf90223a0
.word 0xf9404ba0
.word 0xf90227a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_554
.word 0xaa0003e2
.word 0xf94223a0
.word 0xf94227a1
.word 0xd63f0040
.word 0xf9021ba0
.word 0xf94037b1
.word 0xf9476231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9421ba2
.word 0xf9421fa3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf901dba0
.word 0xf941dba0
.word 0xf90213a0
.word 0xf941dba3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #896]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94213a0
.word 0xf901dfa0
.word 0xf941dfa0
.word 0xf90203a0
.word 0xf941dfa0
.word 0xf9020ba0
.word 0xd28000a0
.word 0xf94053a0
.word 0xf9020fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf9420fa0
.word 0xd63f0020
.word 0xf90207a0
.word 0xf94037b1
.word 0xf9480631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94207a2
.word 0xf9420ba3
.word 0xaa0303e0
.word 0xd28000a1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94203a0
.word 0xf901e3a0
.word 0xf941e3a0
.word 0xf901ffa0
.word 0xf941e3a3
.word 0xd28000c0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
.word 0xaa0303e0
.word 0xd28000c1
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941ffa0
bl _p_47
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9487631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf90057a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9489e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_555
bl _p_440
.word 0xf9020ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_556
.word 0xaa0003e1
.word 0xf9420ba0
.word 0xf90207a0
.word 0xd63f0020
.word 0xf94037b1
.word 0xf948de31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94207a0
.word 0xf901e7a0
.word 0xf941e7a0
.word 0xf901fba0
.word 0xf941e7a0
.word 0xf901ffa0
.word 0xf94057a0
.word 0xf90203a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_557
.word 0xaa0003e2
.word 0xf941ffa0
.word 0xf94203a1
.word 0xd63f0040
.word 0xf94037b1
.word 0xf9492a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9494a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0x14000ebe
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9497231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9499e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xd2800441
.word 0xd280045e
.word 0x6b1e001f
.word 0x54001dc1
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf949d231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf901b3a0
.word 0xf941b3a0
.word 0xb4000180
.word 0xf941b3a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #912]
.word 0xeb01001f
.word 0x10000011
.word 0x5401d3e1
.word 0xf941b3a1
.word 0xaa0103e0
.word 0xf940003e
bl _p_366
.word 0xf90213a0
.word 0xf94037b1
.word 0xf94a3231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94213a0
.word 0xf9005ba0
.word 0xf94037b1
.word 0xf94a4a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf90207a0
.word 0xf9405ba0
.word 0xf9020ba0
.word 0xf94033a0
.word 0xf9020fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_550
.word 0xaa0003e3
.word 0xf94207a0
.word 0xf9420ba1
.word 0xf9420fa2
.word 0xd63f0060
.word 0xf90203a0
.word 0xf94037b1
.word 0xf94a9631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a0
.word 0xf9005fa0
.word 0xf94037b1
.word 0xf94aae31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9405fa0
.word 0xf901ffa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_552
.word 0xaa0003e1
.word 0xf941ffa0
.word 0xd63f0020
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94ae231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xf90063a0
.word 0xf94037b1
.word 0xf94afa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf901b7a0
.word 0xf941b7a0
.word 0xf901bba0
.word 0xf941b7a0
.word 0xeb1f001f
.word 0x54000180
.word 0xf941b7a0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #2168]
.word 0xeb01001f
.word 0x54000040
.word 0xf901bbbf
.word 0xf941bba0
.word 0xb40004c0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94b6a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94063a0
.word 0xf9400001
.word 0x3940b022
.word 0xeb1f005f
.word 0x10000011
.word 0x5401c841
.word 0xf9400021
.word 0xf9400021

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #2168]
.word 0xeb02003f
.word 0x10000011
.word 0x5401c741
.word 0x91004001
.word 0x39404000
.word 0xd2800001
.word 0x6b1f001f
.word 0x9a9f17e0
.word 0xf901fba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #2488]
.word 0xd2800221
.word 0xd2800221
bl _p_20
.word 0xf941fba1
.word 0x39004001
.word 0xf90063a0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94bfe31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_555
bl _p_440
.word 0xf90223a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_556
.word 0xaa0003e1
.word 0xf94223a0
.word 0xf9021fa0
.word 0xd63f0020
.word 0xf94037b1
.word 0xf94c3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9421fa0
.word 0xf901bfa0
.word 0xf941bfa0
.word 0xf90207a0
.word 0xf941bfa0
.word 0xf9020ba0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1056]
.word 0xf90213a0
.word 0xf9405fa0
.word 0xf9021ba0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf9421ba0
.word 0xd63f0020
.word 0xf90217a0
.word 0xf94037b1
.word 0xf94c9a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94213a0
.word 0xf94217a1

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
bl _p_30
.word 0xf9020fa0
.word 0xf94037b1
.word 0xf94cc631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_557
.word 0xaa0003e2
.word 0xf9420ba0
.word 0xf9420fa1
.word 0xd63f0040
.word 0xf94037b1
.word 0xf94cf231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94207a0
.word 0xf901c3a0
.word 0xf941c3a0
.word 0xf901fba0
.word 0xf941c3a0
.word 0xf901ffa0
.word 0xf94063a0
.word 0xf90203a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_558
.word 0xaa0003e2
.word 0xf941ffa0
.word 0xf94203a1
.word 0xd63f0040
.word 0xf94037b1
.word 0xf94d3e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94d5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0x14000db9
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94d8631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9404430
.word 0xd63f0200
.word 0x93407c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94db231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xd28000c1
.word 0xd28000de
.word 0x6b1e001f
.word 0x5400f1e1
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94de631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402fa0
.word 0xf9010fa0
.word 0xf9410fa0
.word 0xb4000180
.word 0xf9410fa0
.word 0xf9400000
.word 0xf9400000
.word 0xf9400800
.word 0xf9400800

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1064]
.word 0xeb01001f
.word 0x10000011
.word 0x5401b341
.word 0xf9410fa0
.word 0xaa0003fa
.word 0xf94037b1
.word 0xf94e3a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_391
.word 0xf90203a0
.word 0xf94037b1
.word 0xf94e5e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_559
.word 0xaa0003e1
.word 0xf94203a0
.word 0xd63f0020
.word 0x93407c00
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf94e8e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf9400000
bl _p_560
.word 0xf941ffa1
bl _p_16
.word 0xaa0003f4
.word 0xf94037b1
.word 0xf94eb631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e1
.word 0xaa0103e0
.word 0xf940003e
bl _p_394
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94eda31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xb5000180
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94f0231
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800000
.word 0xd2800019
.word 0x14000025
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94f2e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf9402ba0
.word 0xf901ffa0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_394
.word 0xf90203a0
.word 0xf94037b1
.word 0xf94f5a31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf90207a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_550
.word 0xaa0003e3
.word 0xf941ffa0
.word 0xf94203a1
.word 0xf94207a2
.word 0xd63f0060
.word 0xf901fba0
.word 0xf94037b1
.word 0xf94f9631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xaa0003f9
.word 0xaa1903e0
.word 0xaa1903f5
.word 0xf94037b1
.word 0xf94fb631
.word 0xb4000051
.word 0xd63f0220
.word 0xd2800016
.word 0xf94037b1
.word 0xf94fca31
.word 0xb4000051
.word 0xd63f0220
.word 0x1400003f
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf94fee31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xaa1603e0
.word 0xf9402ba0
.word 0xf901ffa0
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_391
.word 0xf9020ba0
.word 0xf94037b1
.word 0xf9502231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xf9402ba0
.word 0xf9400000
bl _p_561
.word 0xaa0003e2
.word 0xf9420ba0
.word 0xaa1603e1
.word 0xd63f0040
.word 0xf90203a0
.word 0xf94037b1
.word 0xf9505631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94033a0
.word 0xf90207a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_550
.word 0xaa0003e3
.word 0xf941ffa0
.word 0xf94203a1
.word 0xf94207a2
.word 0xd63f0060
.word 0xf901fba0
.word 0xf94037b1
.word 0xf9509231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba2
.word 0xaa1403e0
.word 0xaa1603e1
.word 0xf9400283
.word 0xf9407870
.word 0xd63f0200
.word 0xf94037b1
.word 0xf950ba31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0x110006c0
.word 0xaa0003f6
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf950e631
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1603e0
.word 0xaa1403e0
.word 0xb9801a80
.word 0x6b0002df
.word 0x54fff6cb
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9511a31
.word 0xb4000051
.word 0xd63f0220

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #1816]
.word 0xaa0003f7
.word 0xf94037b1
.word 0xf9513a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf90203a0
.word 0xf94037b1
.word 0xf9515e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9518631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1072]
bl _p_332
.word 0x53001c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf951b231
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0x340012e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf951da31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xb9801a80
.word 0xd2800041
.word 0xd280005e
.word 0x6b1e001f
.word 0x54001121
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9521231
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf9019fa0
.word 0xf9419fa0
.word 0xf90227a0
.word 0xf9419fa3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94227a0
.word 0xf901a3a0
.word 0xf941a3a0
.word 0xf90217a0
.word 0xf941a3a0
.word 0xf9021fa0
.word 0xd2800020
.word 0xaa1403e0
.word 0xd2800000
.word 0xb9801a80
.word 0xeb1f001f
.word 0x10000011
.word 0x54019089
.word 0xf9401280
.word 0xf90223a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf94223a0
.word 0xd63f0020
.word 0xf9021ba0
.word 0xf94037b1
.word 0xf952c631
.word 0xb4000051
.word 0xd63f0220
.word 0xf9421ba2
.word 0xf9421fa3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf901a7a0
.word 0xf941a7a0
.word 0xf90213a0
.word 0xf941a7a3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1080]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94213a0
.word 0xf901aba0
.word 0xf941aba0
.word 0xf90203a0
.word 0xf941aba0
.word 0xf9020ba0
.word 0xd2800060
.word 0xaa1403e0
.word 0xd2800020
.word 0xb9801a80
.word 0xd280003e
.word 0xeb1e001f
.word 0x10000011
.word 0x54018a89
.word 0xf9401680
.word 0xf9020fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf9420fa0
.word 0xd63f0020
.word 0xf90207a0
.word 0xf94037b1
.word 0xf9538631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94207a2
.word 0xf9420ba3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94203a0
.word 0xf901afa0
.word 0xf941afa0
.word 0xf901ffa0
.word 0xf941afa3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941ffa0
bl _p_47
.word 0xf901fba0
.word 0xf94037b1
.word 0xf953f631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xaa0003f7
.word 0xf94037b1
.word 0xf9540e31
.word 0xb4000051
.word 0xd63f0220
.word 0x140005c8
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9543231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf90203a0
.word 0xf94037b1
.word 0xf9545631
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9547e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1088]
bl _p_332
.word 0x53001c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf954aa31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0x340012e0
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf954d231
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1403e0
.word 0xb9801a80
.word 0xd2800041
.word 0xd280005e
.word 0x6b1e001f
.word 0x54001121
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9550a31
.word 0xb4000051
.word 0xd63f0220
.word 0xd28000a0

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x0, [x16, #880]
.word 0xd28000a1
bl _p_16
.word 0xf9018ba0
.word 0xf9418ba0
.word 0xf90227a0
.word 0xf9418ba3
.word 0xd2800000

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1048]
.word 0xaa0303e0
.word 0xd2800001
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94227a0
.word 0xf9018fa0
.word 0xf9418fa0
.word 0xf90217a0
.word 0xf9418fa0
.word 0xf9021fa0
.word 0xd2800020
.word 0xaa1403e0
.word 0xd2800020
.word 0xb9801a80
.word 0xd280003e
.word 0xeb1e001f
.word 0x10000011
.word 0x540178a9
.word 0xf9401680
.word 0xf90223a0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf94223a0
.word 0xd63f0020
.word 0xf9021ba0
.word 0xf94037b1
.word 0xf955c231
.word 0xb4000051
.word 0xd63f0220
.word 0xf9421ba2
.word 0xf9421fa3
.word 0xaa0303e0
.word 0xd2800021
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94217a0
.word 0xf90193a0
.word 0xf94193a0
.word 0xf90213a0
.word 0xf94193a3
.word 0xd2800040

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1096]
.word 0xaa0303e0
.word 0xd2800041
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94213a0
.word 0xf90197a0
.word 0xf94197a0
.word 0xf90203a0
.word 0xf94197a0
.word 0xf9020ba0
.word 0xd2800060
.word 0xaa1403e0
.word 0xd2800000
.word 0xb9801a80
.word 0xeb1f001f
.word 0x10000011
.word 0x540172c9
.word 0xf9401280
.word 0xf9020fa0
.word 0xf9402ba0
.word 0xf9400000
bl _p_551
.word 0xaa0003e1
.word 0xf9420fa0
.word 0xd63f0020
.word 0xf90207a0
.word 0xf94037b1
.word 0xf9567e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94207a2
.word 0xf9420ba3
.word 0xaa0303e0
.word 0xd2800061
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf94203a0
.word 0xf9019ba0
.word 0xf9419ba0
.word 0xf901ffa0
.word 0xf9419ba3
.word 0xd2800080

adrp x16, mono_aot_SQLite_Net_got@PAGE+0
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x2, [x16, #1216]
.word 0xaa0303e0
.word 0xd2800081
.word 0xf9400063
.word 0xf9407870
.word 0xd63f0200
.word 0xf941ffa0
bl _p_47
.word 0xf901fba0
.word 0xf94037b1
.word 0xf956ee31
.word 0xb4000051
.word 0xd63f0220
.word 0xf941fba0
.word 0xaa0003f7
.word 0xf94037b1
.word 0xf9570631
.word 0xb4000051
.word 0xd63f0220
.word 0x1400050a
.word 0xf9403bb1
.word 0xf9400231
.word 0xb4000051
.word 0xd63f0220
.word 0xf94037b1
.word 0xf9572a31
.word 0xb4000051
.word 0xd63f0220
.word 0xaa1a03e0
.word 0xaa1a03e0
.word 0xf940035e
bl _p_396
.word 0xf90203a0
.word 0xf94037b1
.word 0xf9574e31
.word 0xb4000051
.word 0xd63f0220
.word 0xf94203a1
.word 0xaa0103e0
.word 0xf9400021
.word 0xf9405830
.word 0xd63f0200
.word 0xf901ffa0
.word 0xf94037b1
.word 0xf9577631
.word 0xb4000051
.word 0xd63f0220
.word 0xf941ffa0

adrp x16, mono_aot_SQLite_Net_got@PAGE+4096
add x16, x16, mono_aot_SQLite_Net_got@PAGEOFF
ldr x1, [x16, #1088]
bl _p_332
.word 0x53001c00
.word 0xf901fba0
.word 0xf94037b1
.word 0xf957a231
.word 0xb4000051
.word 0xd63f0220


