#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class AkMusicSyncCallbackInfo : AkCallbackInfo {
  private IntPtr swigCPtr;

  internal AkMusicSyncCallbackInfo(IntPtr cPtr, bool cMemoryOwn) : base(AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = cPtr;
  }

  internal static IntPtr getCPtr(AkMusicSyncCallbackInfo obj) {
    return (obj == null) ? IntPtr.Zero : obj.swigCPtr;
  }

  ~AkMusicSyncCallbackInfo() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkMusicSyncCallbackInfo(swigCPtr);
        }
        swigCPtr = IntPtr.Zero;
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public uint playingID {
    get {
      uint ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_playingID_get(swigCPtr);

      return ret;
    } 
  }

  public int segmentInfo_iCurrentPosition {
    get {
      int ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_iCurrentPosition_get(swigCPtr);

      return ret;
    } 
  }

  public int segmentInfo_iPreEntryDuration {
    get {
      int ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_iPreEntryDuration_get(swigCPtr);

      return ret;
    } 
  }

  public int segmentInfo_iActiveDuration {
    get {
      int ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_iActiveDuration_get(swigCPtr);

      return ret;
    } 
  }

  public int segmentInfo_iPostExitDuration {
    get {
      int ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_iPostExitDuration_get(swigCPtr);

      return ret;
    } 
  }

  public int segmentInfo_iRemainingLookAheadTime {
    get {
      int ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_iRemainingLookAheadTime_get(swigCPtr);

      return ret;
    } 
  }

  public float segmentInfo_fBeatDuration {
    get {
      float ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_fBeatDuration_get(swigCPtr);

      return ret;
    } 
  }

  public float segmentInfo_fBarDuration {
    get {
      float ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_fBarDuration_get(swigCPtr);

      return ret;
    } 
  }

  public float segmentInfo_fGridDuration {
    get {
      float ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_fGridDuration_get(swigCPtr);

      return ret;
    } 
  }

  public float segmentInfo_fGridOffset {
    get {
      float ret = AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_segmentInfo_fGridOffset_get(swigCPtr);

      return ret;
    } 
  }

  public AkCallbackType musicSyncType {
    get {
      AkCallbackType ret = (AkCallbackType)AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_musicSyncType_get(swigCPtr);

      return ret;
    } 
  }

  public string userCueName { get { return AkSoundEngine.StringFromIntPtrString(AkSoundEnginePINVOKE.CSharp_AkMusicSyncCallbackInfo_userCueName_get(swigCPtr));
 } 
  }

  public AkMusicSyncCallbackInfo() : this(AkSoundEnginePINVOKE.CSharp_new_AkMusicSyncCallbackInfo(), true) {

  }

}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.