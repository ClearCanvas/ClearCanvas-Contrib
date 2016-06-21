CustomDefaultStackingOrder
==========================

A Plugin that can change the default stacking order.
 

Setup
-----

To change your new default stacking order you need to modify the CustomDefaultStackingOrder.cmd. Search for the following code:

    static readonly IComparer<IPresentationImage> comparer =
    /*
     * Choose between InstanceAndFrameNumberComparer, AcquisitionTimeComparer 
     * or SliceLocationComparer and remove the ReverseComparer<IPresentationImage>
     * as needed.
     */
        new ReverseComparer<IPresentationImage>(new SliceLocationComparer());

Install
-------

Run the CustomDefaultStackingOrder.cmd and provide your ClearCanvas Workstation or DicomViewer installation directory.