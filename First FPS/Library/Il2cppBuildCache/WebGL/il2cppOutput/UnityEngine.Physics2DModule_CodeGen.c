#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.Void UnityEngine.Physics2D::.cctor()
extern void Physics2D__cctor_m84E6BF1DDF3530A4A43A775DAADB6047D993E6A1 (void);
// 0x00000002 UnityEngine.Collider2D UnityEngine.Collision2D::get_collider()
extern void Collision2D_get_collider_mA7687EDB0D47A2F211BFE8DB89266B9AA05CFDDD (void);
// 0x00000003 UnityEngine.Rigidbody2D UnityEngine.Collision2D::get_rigidbody()
extern void Collision2D_get_rigidbody_m82AF533E110DFDBDED6D6C74EB479902E813D42E (void);
// 0x00000004 UnityEngine.GameObject UnityEngine.Collision2D::get_gameObject()
extern void Collision2D_get_gameObject_m6F07B9CA1FAD187933EB6D8E44BD9F870658F89F (void);
// 0x00000005 UnityEngine.Vector2 UnityEngine.RaycastHit2D::get_point()
extern void RaycastHit2D_get_point_m10D5AB3B26EAE62583BE35CFA13A3E40BDAF30AE (void);
// 0x00000006 UnityEngine.Vector2 UnityEngine.RaycastHit2D::get_normal()
extern void RaycastHit2D_get_normal_m6F8B9F4018EFA126CC33126E8E42B09BB5A82637 (void);
// 0x00000007 System.Single UnityEngine.RaycastHit2D::get_distance()
extern void RaycastHit2D_get_distance_mA910B45BD349A8F70139F6BC1E686F47F40A1662 (void);
// 0x00000008 UnityEngine.Collider2D UnityEngine.RaycastHit2D::get_collider()
extern void RaycastHit2D_get_collider_m00F7EC55C36F39E2ED64B31354FB4D9C8938D563 (void);
static Il2CppMethodPointer s_methodPointers[8] = 
{
	Physics2D__cctor_m84E6BF1DDF3530A4A43A775DAADB6047D993E6A1,
	Collision2D_get_collider_mA7687EDB0D47A2F211BFE8DB89266B9AA05CFDDD,
	Collision2D_get_rigidbody_m82AF533E110DFDBDED6D6C74EB479902E813D42E,
	Collision2D_get_gameObject_m6F07B9CA1FAD187933EB6D8E44BD9F870658F89F,
	RaycastHit2D_get_point_m10D5AB3B26EAE62583BE35CFA13A3E40BDAF30AE,
	RaycastHit2D_get_normal_m6F8B9F4018EFA126CC33126E8E42B09BB5A82637,
	RaycastHit2D_get_distance_mA910B45BD349A8F70139F6BC1E686F47F40A1662,
	RaycastHit2D_get_collider_m00F7EC55C36F39E2ED64B31354FB4D9C8938D563,
};
extern void RaycastHit2D_get_point_m10D5AB3B26EAE62583BE35CFA13A3E40BDAF30AE_AdjustorThunk (void);
extern void RaycastHit2D_get_normal_m6F8B9F4018EFA126CC33126E8E42B09BB5A82637_AdjustorThunk (void);
extern void RaycastHit2D_get_distance_mA910B45BD349A8F70139F6BC1E686F47F40A1662_AdjustorThunk (void);
extern void RaycastHit2D_get_collider_m00F7EC55C36F39E2ED64B31354FB4D9C8938D563_AdjustorThunk (void);
static Il2CppTokenAdjustorThunkPair s_adjustorThunks[4] = 
{
	{ 0x06000005, RaycastHit2D_get_point_m10D5AB3B26EAE62583BE35CFA13A3E40BDAF30AE_AdjustorThunk },
	{ 0x06000006, RaycastHit2D_get_normal_m6F8B9F4018EFA126CC33126E8E42B09BB5A82637_AdjustorThunk },
	{ 0x06000007, RaycastHit2D_get_distance_mA910B45BD349A8F70139F6BC1E686F47F40A1662_AdjustorThunk },
	{ 0x06000008, RaycastHit2D_get_collider_m00F7EC55C36F39E2ED64B31354FB4D9C8938D563_AdjustorThunk },
};
static const int32_t s_InvokerIndices[8] = 
{
	3556,
	2242,
	2242,
	2242,
	2283,
	2283,
	2264,
	2242,
};
extern const CustomAttributesCacheGenerator g_UnityEngine_Physics2DModule_AttributeGenerators[];
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_UnityEngine_Physics2DModule_CodeGenModule;
const Il2CppCodeGenModule g_UnityEngine_Physics2DModule_CodeGenModule = 
{
	"UnityEngine.Physics2DModule.dll",
	8,
	s_methodPointers,
	4,
	s_adjustorThunks,
	s_InvokerIndices,
	0,
	NULL,
	0,
	NULL,
	0,
	NULL,
	NULL,
	g_UnityEngine_Physics2DModule_AttributeGenerators,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
