= UpdateOriginalAttributesSequence plugin =

The UpdateOriginalAttributesSequence plugin updates the 
OriginalAttributesSequence (0400,0561) DICOM attribute after a study got 
edited using the Image Server's web-front-end. 

This is useful if you want to view the history of a study not only at 
the Image Server's web-front-end but also within a DICOM viewer or if 
you simply want to maintain the history of a DICOM file within itself
and so independent from the database. 


== Install ==

Make sure to not only copy the plugin DLL into the Image Server's 
.\plugin directory but also to .\web\bin directory. Otherwise you will 
see the following error if you try to access the Image Server's 
web-front-end:

  Unable to Read Xml Data for Abstract Type 
  'BaseImageLevelUpdateCommand' because the type specified in the XML
  was not found.