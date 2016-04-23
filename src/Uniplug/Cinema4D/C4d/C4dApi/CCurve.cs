//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.8
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace C4d {

public class CCurve : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CCurve(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(CCurve obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int GetKeyCount() {
    int ret = C4dApiPINVOKE.CCurve_GetKeyCount(swigCPtr);
    return ret;
  }

  public CKey GetKey(int index) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_GetKey__SWIG_0(swigCPtr, index);
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    return ret;
  }

  public CKey FindKey(BaseTime time, SWIGTYPE_p_Int32 idx, FINDANIM match) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_FindKey__SWIG_0(swigCPtr, BaseTime.getCPtr(time), SWIGTYPE_p_Int32.getCPtr(idx), (int)match);
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CKey FindKey(BaseTime time, SWIGTYPE_p_Int32 idx) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_FindKey__SWIG_1(swigCPtr, BaseTime.getCPtr(time), SWIGTYPE_p_Int32.getCPtr(idx));
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CKey FindKey(BaseTime time) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_FindKey__SWIG_2(swigCPtr, BaseTime.getCPtr(time));
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CKey AddKey(BaseTime time, SWIGTYPE_p_Int32 nidx) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_AddKey__SWIG_0(swigCPtr, BaseTime.getCPtr(time), SWIGTYPE_p_Int32.getCPtr(nidx));
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CKey AddKey(BaseTime time) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_AddKey__SWIG_1(swigCPtr, BaseTime.getCPtr(time));
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CKey AddKeyAdaptTangent(BaseTime time, bool bUndo, SWIGTYPE_p_Int32 nidx) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_AddKeyAdaptTangent__SWIG_0(swigCPtr, BaseTime.getCPtr(time), bUndo, SWIGTYPE_p_Int32.getCPtr(nidx));
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CKey AddKeyAdaptTangent(BaseTime time, bool bUndo) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_AddKeyAdaptTangent__SWIG_1(swigCPtr, BaseTime.getCPtr(time), bUndo);
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool InsertKey(CKey ckey) {
    bool ret = C4dApiPINVOKE.CCurve_InsertKey(swigCPtr, CKey.getCPtr(ckey));
    return ret;
  }

  public bool DelKey(int index) {
    bool ret = C4dApiPINVOKE.CCurve_DelKey(swigCPtr, index);
    return ret;
  }

  public int MoveKey(BaseTime time, int idx, CCurve dseq) {
    int ret = C4dApiPINVOKE.CCurve_MoveKey__SWIG_0(swigCPtr, BaseTime.getCPtr(time), idx, CCurve.getCPtr(dseq));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int MoveKey(BaseTime time, int idx) {
    int ret = C4dApiPINVOKE.CCurve_MoveKey__SWIG_1(swigCPtr, BaseTime.getCPtr(time), idx);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void FlushKeys() {
    C4dApiPINVOKE.CCurve_FlushKeys(swigCPtr);
  }

  public double CalcHermite(double time, double t1, double t2, double val1, double val2, double tan1_val, double tan2_val, double tan1_t, double tan2_t, bool deriv) {
    double ret = C4dApiPINVOKE.CCurve_CalcHermite(swigCPtr, time, t1, t2, val1, val2, tan1_val, tan2_val, tan1_t, tan2_t, deriv);
    return ret;
  }

  public void CalcSoftTangents(int kidx, SWIGTYPE_p_Float vl, SWIGTYPE_p_Float vr, BaseTime tl, BaseTime tr) {
    C4dApiPINVOKE.CCurve_CalcSoftTangents(swigCPtr, kidx, SWIGTYPE_p_Float.getCPtr(vl), SWIGTYPE_p_Float.getCPtr(vr), BaseTime.getCPtr(tl), BaseTime.getCPtr(tr));
  }

  public void GetTangents(int kidx, SWIGTYPE_p_Float64 vl, SWIGTYPE_p_Float64 vr, SWIGTYPE_p_Float64 tl, SWIGTYPE_p_Float64 tr) {
    C4dApiPINVOKE.CCurve_GetTangents(swigCPtr, kidx, SWIGTYPE_p_Float64.getCPtr(vl), SWIGTYPE_p_Float64.getCPtr(vr), SWIGTYPE_p_Float64.getCPtr(tl), SWIGTYPE_p_Float64.getCPtr(tr));
  }

  public double GetValue(BaseTime time) {
    double ret = C4dApiPINVOKE.CCurve_GetValue(swigCPtr, BaseTime.getCPtr(time));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public CTrack GetTrack() {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_GetTrack(swigCPtr);
    CTrack ret = (cPtr == global::System.IntPtr.Zero) ? null : new CTrack(cPtr, false);
    return ret;
  }

  public void SetKeyDefault(BaseDocument doc, int kidx) {
    C4dApiPINVOKE.CCurve_SetKeyDefault(swigCPtr, BaseDocument.getCPtr(doc), kidx);
  }

  public void SetKeyDirty() {
    C4dApiPINVOKE.CCurve_SetKeyDirty(swigCPtr);
  }

  public void SortKeysByTime() {
    C4dApiPINVOKE.CCurve_SortKeysByTime(swigCPtr);
  }

  public BaseTime GetStartTime() {
    BaseTime ret = new BaseTime(C4dApiPINVOKE.CCurve_GetStartTime(swigCPtr), true);
    return ret;
  }

  public BaseTime GetEndTime() {
    BaseTime ret = new BaseTime(C4dApiPINVOKE.CCurve_GetEndTime(swigCPtr), true);
    return ret;
  }

  public CKey FindNextUnmuted(int idx, SWIGTYPE_p_Int32 ret_idx) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_FindNextUnmuted__SWIG_0(swigCPtr, idx, SWIGTYPE_p_Int32.getCPtr(ret_idx));
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    return ret;
  }

  public CKey FindNextUnmuted(int idx) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_FindNextUnmuted__SWIG_1(swigCPtr, idx);
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    return ret;
  }

  public CKey FindPrevUnmuted(int idx, SWIGTYPE_p_Int32 ret_idx) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_FindPrevUnmuted__SWIG_0(swigCPtr, idx, SWIGTYPE_p_Int32.getCPtr(ret_idx));
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    return ret;
  }

  public CKey FindPrevUnmuted(int idx) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.CCurve_FindPrevUnmuted__SWIG_1(swigCPtr, idx);
    CKey ret = (cPtr == global::System.IntPtr.Zero) ? null : new CKey(cPtr, false);
    return ret;
  }

}

}