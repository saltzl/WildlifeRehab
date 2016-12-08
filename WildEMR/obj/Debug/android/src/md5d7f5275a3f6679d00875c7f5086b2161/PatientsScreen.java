package md5d7f5275a3f6679d00875c7f5086b2161;


public class PatientsScreen
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
		mono.android.Runtime.register ("WildEMR.PatientsScreen, WildEMR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PatientsScreen.class, __md_methods);
	}


	public PatientsScreen () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PatientsScreen.class)
			mono.android.TypeManager.Activate ("WildEMR.PatientsScreen, WildEMR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
