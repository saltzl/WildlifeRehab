package md5d7f5275a3f6679d00875c7f5086b2161;


public class NewPatientScreen2
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("WildEMR.NewPatientScreen2, WildEMR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NewPatientScreen2.class, __md_methods);
	}


	public NewPatientScreen2 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NewPatientScreen2.class)
			mono.android.TypeManager.Activate ("WildEMR.NewPatientScreen2, WildEMR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
