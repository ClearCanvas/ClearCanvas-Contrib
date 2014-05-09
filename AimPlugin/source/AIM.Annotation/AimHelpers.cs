#region License

// Copyright © 2008-2010 Northwestern University

// Authors:
// Vladimir Kleper
// Northwestern University
// Feinberg School of Medicine
// Department of Radiology
// Imaging Informatics Section
// Chicago, IL

// The Academic Free License v. 2.1

// This Academic Free License (the "License") applies to any original work of
// authorship (the "Original Work") whose owner (the "Licensor") has placed the
// following notice immediately following the copyright notice for the Original
// Work:

// Licensed under the Academic Free License version 2.1

// 1) Grant of Copyright License. Licensor hereby grants You a world-wide, royalty-
// free, non-exclusive, perpetual, sublicenseable license to do the following:

// a) to reproduce the Original Work in copies;

// b) to prepare derivative works ("Derivative Works") based upon the Original Work;

// c) to distribute copies of the Original Work and Derivative Works to the public;

// d) to perform the Original Work publicly; and

// e) to display the Original Work publicly.

// 2) Grant of Patent License. Licensor hereby grants You a world-wide, royalty-
// free, non-exclusive, perpetual, sublicenseable license, under patent claims
// owned or controlled by the Licensor that are embodied in the Original Work as
// furnished by the Licensor, to make, use, sell and offer for sale the Original
// Work and Derivative Works.

// 3) Grant of Source Code License. The term "Source Code" means the preferred form
// of the Original Work for making modifications to it and all available
// documentation describing how to modify the Original Work. Licensor hereby agrees
// to provide a machine-readable copy of the Source Code of the Original Work along
// with each copy of the Original Work that Licensor distributes. Licensor reserves
// the right to satisfy this obligation by placing a machine-readable copy of the
// Source Code in an information repository reasonably calculated to permit
// inexpensive and convenient access by You for as long as Licensor continues to
// distribute the Original Work, and by publishing the address of that information
// repository in a notice immediately following the copyright notice that applies
// to the Original Work.

// 4) Exclusions From License Grant. Neither the names of Licensor, nor the names
// of any contributors to the Original Work, nor any of their trademarks or service
// marks, may be used to endorse or promote products derived from this Original
// Work without express prior written permission of the Licensor. Nothing in this
// License shall be deemed to grant any rights to trademarks, copyrights, patents,
// trade secrets or any other intellectual property of Licensor except as expressly
// stated herein. No patent license is granted to make, use, sell or offer to sell
// embodiments of any patent claims other than the licensed claims defined in
// Section 2. No right is granted to the trademarks of Licensor even if such marks
// are included in the Original Work. Nothing in this License shall be interpreted
// to prohibit Licensor from licensing under different terms from this License any
// Original Work that Licensor otherwise would have a right to license.

// 5) This section intentionally omitted.

// 6) Attribution Rights. You must retain, in the Source Code of any Derivative
// Works that You create, all copyright, patent or trademark notices from the
// Source Code of the Original Work, as well as any notices of licensing and any
// descriptive text identified therein as an "Attribution Notice." You must cause
// the Source Code for any Derivative Works that You create to carry a prominent
// Attribution Notice reasonably calculated to inform recipients that You have
// modified the Original Work.

// 7) Warranty of Provenance and Disclaimer of Warranty. Licensor warrants that the
// copyright in and to the Original Work and the patent rights granted herein by
// Licensor are owned by the Licensor or are sublicensed to You under the terms of
// this License with the permission of the contributor(s) of those copyrights and
// patent rights. Except as expressly stated in the immediately proceeding
// sentence, the Original Work is provided under this License on an "AS IS" BASIS
// and WITHOUT WARRANTY, either express or implied, including, without limitation,
// the warranties of NON-INFRINGEMENT, MERCHANTABILITY or FITNESS FOR A PARTICULAR
// PURPOSE. THE ENTIRE RISK AS TO THE QUALITY OF THE ORIGINAL WORK IS WITH YOU.
// This DISCLAIMER OF WARRANTY constitutes an essential part of this License. No
// license to Original Work is granted hereunder except under this disclaimer.

// 8) Limitation of Liability. Under no circumstances and under no legal theory,
// whether in tort (including negligence), contract, or otherwise, shall the
// Licensor be liable to any person for any direct, indirect, special, incidental,
// or consequential damages of any character arising as a result of this License or
// the use of the Original Work including, without limitation, damages for loss of
// goodwill, work stoppage, computer failure or malfunction, or any and all other
// commercial damages or losses. This limitation of liability shall not apply to
// liability for death or personal injury resulting from Licensor's negligence to
// the extent applicable law prohibits such limitation. Some jurisdictions do not
// allow the exclusion or limitation of incidental or consequential damages, so
// this exclusion and limitation may not apply to You.

// 9) Acceptance and Termination. If You distribute copies of the Original Work or
// a Derivative Work, You must make a reasonable effort under the circumstances to
// obtain the express assent of recipients to the terms of this License. Nothing
// else but this License (or another written agreement between Licensor and You)
// grants You permission to create Derivative Works based upon the Original Work or
// to exercise any of the rights granted in Section 1 herein, and any attempt to do
// so except under the terms of this License (or another written agreement between
// Licensor and You) is expressly prohibited by U.S. copyright law, the equivalent
// laws of other countries, and by international treaty. Therefore, by exercising
// any of the rights granted to You in Section 1 herein, You indicate Your
// acceptance of this License and all of its terms and conditions.

// 10) Termination for Patent Action. This License shall terminate automatically
// and You may no longer exercise any of the rights granted to You by this License
// as of the date You commence an action, including a cross-claim or counterclaim,
// against Licensor or any licensee alleging that the Original Work infringes a
// patent. This termination provision shall not apply for an action alleging patent
// infringement by combinations of the Original Work with other software or 
// hardware.

// 11) Jurisdiction, Venue and Governing Law. Any action or suit relating to this
// License may be brought only in the courts of a jurisdiction wherein the Licensor
// resides or in which Licensor conducts its primary business, and under the laws
// of that jurisdiction excluding its conflict-of-law provisions. The application
// of the United Nations Convention on Contracts for the International Sale of
// Goods is expressly excluded. Any use of the Original Work outside the scope of
// this License or after its termination shall be subject to the requirements and
// penalties of the U.S. Copyright Act, 17 U.S.C. § 101 et seq., the equivalent
// laws of other countries, and international treaty. This section shall survive
// the termination of this License.

// 12) Attorneys Fees. In any action to enforce the terms of this License or
// seeking damages relating thereto, the prevailing party shall be entitled to
// recover its costs and expenses, including, without limitation, reasonable
// attorneys' fees and costs incurred in connection with such action, including any
// appeal of such action. This section shall survive the termination of this
// License.

// 13) Miscellaneous. This License represents the complete agreement concerning the
// subject matter hereof. If any provision of this License is held to be
// unenforceable, such provision shall be reformed only to the extent necessary to
// make it enforceable.

// 14) Definition of "You" in This License. "You" throughout this License, whether
// in upper or lower case, means an individual or a legal entity exercising rights
// under, and complying with all of the terms of, this License. For legal entities,
// "You" includes any entity that controls, is controlled by, or is under common
// control with you. For purposes of this definition, "control" means (i) the
// power, direct or indirect, to cause the direction or management of such entity,
// whether by contract or otherwise, or (ii) ownership of fifty percent (50%) or
// more of the outstanding shares, or (iii) beneficial ownership of such entity.

// 15) Right to Use. You may use the Original Work in all ways not otherwise
// restricted or conditioned by this License or by law, and Licensor promises not
// to interfere with or be responsible for such uses by You.

// This license is Copyright (C) 2003-2004 Lawrence E. Rosen. All rights reserved.
// Permission is hereby granted to copy and distribute this license without
// modification. This license may not be modified without the express written
// permission of its copyright owner.

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;

using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Utilities;
using ClearCanvas.ImageViewer;
using ClearCanvas.ImageViewer.Graphics;
using ClearCanvas.ImageViewer.Imaging;
using ClearCanvas.ImageViewer.InteractiveGraphics;
using ClearCanvas.ImageViewer.Mathematics;
using ClearCanvas.ImageViewer.RoiGraphics;
using ClearCanvas.ImageViewer.RoiGraphics.Analyzers;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.ImageViewer.Tools.Measurement;

using AIM.Annotation.Graphics;

namespace AIM.Annotation
{
	internal class AimHelpers
	{
		public static List<aim_dotnet.Annotation> CreateAimAnnotations(IEnumerable<IPresentationImage> presentationImages, AimAnnotationCreationContext creationContext, out List<IGraphic> annotationsGraphic)
		{
			List<aim_dotnet.Annotation> annotations = new List<aim_dotnet.Annotation>();
			annotationsGraphic = new List<IGraphic>(); // list that includes all IGraphic objects used to create new annotations

			if (creationContext.SelectedAnnotationKind == aim_dotnet.AnnotationKind.AK_ImageAnnotation)
			{
				List<string> annotationSOPs = creationContext.SOPImageUIDs;
					// list of UIDs to include in the annotation. If empty, include all images from the same set which have markup.

				foreach (IPresentationImage currentImage in presentationImages)
				{
					IImageSopProvider currentImageSOP = currentImage as IImageSopProvider;
					if (currentImageSOP == null) // not an image?
						continue;

					// Skip the image if it's not included in our image annotation set
					if (annotationSOPs.Count > 0 && !annotationSOPs.Contains(currentImageSOP.ImageSop.SopInstanceUid))
						continue;

					// Find all markups on the image
					IOverlayGraphicsProvider currentOverlayGraphics = currentImage as IOverlayGraphicsProvider;
					if (currentOverlayGraphics != null) // all images should implement IOverlayGraphicsProvider
					{
						// Create one annotation per calculation/geo shape
						int shapeIdentifier = 0;
						List<aim_dotnet.IGeometricShape> geoShapes = new List<aim_dotnet.IGeometricShape>();
						List<aim_dotnet.Calculation> calculations = creationContext.includeCalculations ? new List<aim_dotnet.Calculation>() : null;
						foreach (IGraphic graphic in currentOverlayGraphics.OverlayGraphics)
						{
							RoiGraphic currentRoiGraphic = graphic as RoiGraphic;
							if (currentRoiGraphic != null && currentRoiGraphic.Roi != null)
							{
								// Create Geo Shape for this ROI
								aim_dotnet.IGeometricShape geoShape = Create2DGeoShape(currentRoiGraphic, currentImageSOP.ImageSop.SopInstanceUid, currentImageSOP.Frame.FrameNumber);
								if (geoShape != null)
								{
									// Shape Identifier is just a count
									geoShape.ShapeIdetifier = shapeIdentifier++;

									// Get calcualtions for this ROI, if required
									if (calculations != null)
										calculations.AddRange(CreateCalculations(currentRoiGraphic, geoShape.ShapeIdetifier));

									geoShapes.Add(geoShape);
									annotationsGraphic.Add(graphic);
								}
							}
						}
						// Create annotation if:
						// 1. we are explicitly asked to create an annotation for this SOP, or
						// 2. image has markup(s), or
						// 3. image has calculation(s)
						if (annotationSOPs.Count > 0 || geoShapes.Count > 0 || (calculations != null && calculations.Count > 0))
						{
							aim_dotnet.ImageAnnotation imgAnn = CreateImageAnnotation(currentImageSOP, creationContext);

							if (geoShapes.Count > 0)
								imgAnn.GeometricShapeCollection = geoShapes;

							if (calculations != null && calculations.Count > 0)
								imgAnn.CalculationCollection = calculations;

							// Add current image reference to the annotation
							AddDicomImageReference(imgAnn, currentImageSOP);

							annotations.Add(imgAnn);
						}
					}
				}

				return annotations;
			}
			else
			{
				throw new NotImplementedException("Creation of AnnotationOfAnnotation objects is not yet supported ");
			}
		}

		private static aim_dotnet.ImageAnnotation CreateImageAnnotation(IImageSopProvider imageSop, AimAnnotationCreationContext creationContext)
		{
			// Create Basic Image Annotation
			Platform.CheckTrue(creationContext.SelectedAnnotationKind == aim_dotnet.AnnotationKind.AK_ImageAnnotation, "ImageAnnotationKind");
			Platform.CheckForEmptyString(creationContext.AnnotationName, "AnnotationName");

			aim_dotnet.ImageAnnotation imgAnnotation = new aim_dotnet.ImageAnnotation();
			imgAnnotation.CodeValue = creationContext.AnnotationTypeCode.CodeValue;
			imgAnnotation.CodeMeaning = creationContext.AnnotationTypeCode.CodeMeaning;
			imgAnnotation.CodingSchemeDesignator = creationContext.AnnotationTypeCode.CodingSchemeDesignator;
			imgAnnotation.CodingSchemeVersion = creationContext.AnnotationTypeCode.CodingSchemeVersion;
			imgAnnotation.UniqueIdentifier = DicomUid.GenerateUid().UID; // TODO - replace ClearCanvas root UID with an appropriate value
			imgAnnotation.Name = creationContext.AnnotationName;
			imgAnnotation.DateTime = DateTime.Now;
			imgAnnotation.Patient = CreatePatient(imageSop);
			imgAnnotation.Equipment = CreateEquipment();
			if (creationContext.AnnotationUser != null)
				imgAnnotation.User = new aim_dotnet.User(creationContext.AnnotationUser);
			if (!string.IsNullOrEmpty(creationContext.AnnotationComment))
				imgAnnotation.Comment = creationContext.AnnotationComment;

			// add AnatomicEntity
			if (creationContext.SelectedAnatomicEntities != null)
				imgAnnotation.AnatomyEntityCollection = new List<aim_dotnet.AnatomicEntity>(creationContext.SelectedAnatomicEntities);

			// add ImagingObservation and ImagingObservationCharacteristic values
			if (creationContext.SelectedImagingObservations != null)
				imgAnnotation.ImagingObservationCollection = new List<aim_dotnet.ImagingObservation>(creationContext.SelectedImagingObservations);

			// add Inferences
			if (creationContext.SelectedInferences != null)
				imgAnnotation.InferenceCollection = new List<aim_dotnet.Inference>(creationContext.SelectedInferences);

			return imgAnnotation;
		}

		private static aim_dotnet.Person CreatePatient(IImageSopProvider image)
		{
			Platform.CheckForNullReference(image, "Image");

			aim_dotnet.Person patient = new aim_dotnet.Person();
			int personId;
			if (int.TryParse(image.Frame.ParentImageSop.PatientId, out personId))
				patient.Id = personId;
			DateTime birthDate;
			DateParser.Parse(image.Frame.ParentImageSop.PatientsBirthDate, out birthDate);
			patient.BirthDate = birthDate;
			patient.Name = image.Frame.ParentImageSop.PatientsName;
			patient.Sex = image.Frame.ParentImageSop.PatientsSex;
			patient.EthnicGroup = image.ImageSop[DicomTags.EthnicGroup].ToString();

			return patient;
		}

		private static aim_dotnet.Equipment CreateEquipment()
		{
			aim_dotnet.Equipment equipment = new aim_dotnet.Equipment();
			equipment.ManufacturerName = SR.EquipmentManufacturer;
			equipment.ManufacturerModelName = SR.EquipmentManufacturerModelName;
			equipment.SoftwareVersion = string.Format("{0}.{1}.{2}.{3}", Application.Version.Major, Application.Version.Minor, Application.Version.Revision,
			                                          Application.Version.Build);

			return equipment;
		}

		private static aim_dotnet.IGeometricShape Create2DGeoShape(RoiGraphic roiGraphic, string imageUID, int frameNumber)
		{
			Platform.CheckForEmptyString(imageUID, "imageUID");

			aim_dotnet.IGeometricShape geoShape = null;
			Roi roi = roiGraphic.Roi;

			//Platform.CheckTrue(graphics.CoordinateSystem == CoordinateSystem.Source, "Source Coordinate System");

			if (roi is EllipticalRoi)
			{
				EllipticalRoi ellipticalRoi = roi as EllipticalRoi;
				Platform.CheckForNullReference(ellipticalRoi, "ellipticalRoi");

				aim_dotnet.Ellipse ellipseShape = new aim_dotnet.Ellipse();
				ellipseShape.EllipseCollection = new List<aim_dotnet.ISpatialCoordinate>();

				// Bounding box coordinates to DICOM ellipse conversion.
				// Since ellipse's bounding box is not rotated, we just need to find major axis
				// and store the center points of bounding box' side as ellipse vertices.

				//    if (ellipseGraphics.Width >= ellipseGraphics.Height)
				{
					// Horizontal major axis points
					ellipseShape.EllipseCollection.Add(
						Create2DSpatialCoordinate(ellipticalRoi.BoundingBox.Left, ellipticalRoi.BoundingBox.Top + ellipticalRoi.BoundingBox.Height/2, imageUID, frameNumber, 0));

					ellipseShape.EllipseCollection.Add(
						Create2DSpatialCoordinate(ellipticalRoi.BoundingBox.Right, ellipticalRoi.BoundingBox.Top + ellipticalRoi.BoundingBox.Height/2, imageUID, frameNumber, 1));

					// Vertical minor axis points
					ellipseShape.EllipseCollection.Add(
						Create2DSpatialCoordinate(ellipticalRoi.BoundingBox.Left + ellipticalRoi.BoundingBox.Width/2, ellipticalRoi.BoundingBox.Top, imageUID, frameNumber, 2));

					ellipseShape.EllipseCollection.Add(
						Create2DSpatialCoordinate(ellipticalRoi.BoundingBox.Left + ellipticalRoi.BoundingBox.Width/2, ellipticalRoi.BoundingBox.Bottom, imageUID, frameNumber, 3));
				}
				//else
				//{
				//    // Vertical major axis
				//    ellipseShape.EllipseCollection.Add(Create2DSpatialCoordinate(
				//                                           ellipseGraphics.Left + ellipseGraphics.Width/2, ellipseGraphics.Top, imageUID, frameNumber, 0));

				//    ellipseShape.EllipseCollection.Add(Create2DSpatialCoordinate(
				//                                           ellipseGraphics.Left + ellipseGraphics.Width/2, ellipseGraphics.Bottom, imageUID, frameNumber, 1));

				//    // Horizontal minor axis
				//    ellipseShape.EllipseCollection.Add(Create2DSpatialCoordinate(
				//                                           ellipseGraphics.Left, ellipseGraphics.Top + ellipseGraphics.Height/2, imageUID, frameNumber, 2));

				//    ellipseShape.EllipseCollection.Add(Create2DSpatialCoordinate(
				//                                           ellipseGraphics.Right, ellipseGraphics.Top + ellipseGraphics.Height/2, imageUID, frameNumber, 3));
				//}

				geoShape = ellipseShape;
			}
			else if (roi is PolygonalRoi)
			{
				PolygonalRoi polygonalRoi = roi as PolygonalRoi;
				Platform.CheckForNullReference(polygonalRoi, "polygonalRoi");
				//if (!polygonalRoi.IsClosed)
				//{
				//    Platform.Log(LogLevel.Error, "Object state error: Given polygon is not closed");
				//    return null;
				//}

				aim_dotnet.Polyline polylineShape = new aim_dotnet.Polyline();
				polylineShape.SpatialCoordinateCollection = new List<aim_dotnet.ISpatialCoordinate>();
				polylineShape.IsIncludeFlag = true;
				for (int i = 0; i < polygonalRoi.Polygon.Vertices.Count; i++)
				{
					polylineShape.SpatialCoordinateCollection.Add(
						Create2DSpatialCoordinate(polygonalRoi.Polygon.Vertices[i].X, polygonalRoi.Polygon.Vertices[i].Y, imageUID, frameNumber, i));
				}
				geoShape = polylineShape;
			}
			else if (roi is ProtractorRoiInfo)
			{
				ProtractorRoiInfo protractorRoi = roi as ProtractorRoiInfo;
				Platform.CheckForNullReference(protractorRoi, "protractorRoi");

				aim_dotnet.MultiPoint multipointShape = new aim_dotnet.MultiPoint();
				multipointShape.SpatialCoordinateCollection = new List<aim_dotnet.ISpatialCoordinate>();
				multipointShape.IsIncludeFlag = true;
				for (int i = 0; i < protractorRoi.Points.Count; i++)
				{
					multipointShape.SpatialCoordinateCollection.Add(
						Create2DSpatialCoordinate(protractorRoi.Points[i].X, protractorRoi.Points[i].Y, imageUID, frameNumber, i));
				}
				geoShape = multipointShape;
			}
			else if (roi is LinearRoi)
			{
				LinearRoi linearRoi = roi as LinearRoi;
				Platform.CheckForNullReference(linearRoi, "linearRoi");

				aim_dotnet.MultiPoint multipointShape = new aim_dotnet.MultiPoint();
				multipointShape.SpatialCoordinateCollection = new List<aim_dotnet.ISpatialCoordinate>();
				multipointShape.IsIncludeFlag = true;
				for (int i = 0; i < linearRoi.Points.Count; i++)
				{
					multipointShape.SpatialCoordinateCollection.Add(
						Create2DSpatialCoordinate(linearRoi.Points[i].X, linearRoi.Points[i].Y, imageUID, frameNumber, i));
				}
				geoShape = multipointShape;
			}
				//else if (roi is PolygonalRoi)
				//{
				//    PolygonalRoi polylineGraphics = roi as PolygonalRoi;
				//    Platform.CheckForNullReference(polylineGraphics, "polylineGraphics");

				//    if (polylineGraphics.Polygon.CountVertices > 2 &&
				//        polylineGraphics.Polygon.Vertices[0] == polylineGraphics.Polygon.Vertices[polylineGraphics.Polygon.Vertices.Count - 1])
				//    {
				//        // Closed polyline graphics
				//        Polyline polylineShape = new Polyline();
				//        polylineShape.SpatialCoordinateCollection = new List<ISpatialCoordinate>();
				//        polylineShape.IsIncludeFlag = true;
				//        for (int i = 0; i < polylineGraphics.Polygon.CountVertices - 1; i++) // no need for the end point - we will have implicitly closed Polyline
				//        {
				//            polylineShape.SpatialCoordinateCollection.Add(
				//                Create2DSpatialCoordinate(polylineGraphics.Polygon.Vertices[i].X, polylineGraphics.Polygon.Vertices[i].Y, imageUID, frameNumber, i));
				//        }
				//        geoShape = polylineShape;
				//    }
				//    else
				//    {
				//        MultiPoint multipointShape = new MultiPoint();
				//        multipointShape.SpatialCoordinateCollection = new List<ISpatialCoordinate>();
				//        multipointShape.IsIncludeFlag = true;
				//        for (int i = 0; i < polylineGraphics.Polygon.CountVertices; i++)
				//        {
				//            multipointShape.SpatialCoordinateCollection.Add(
				//                Create2DSpatialCoordinate(polylineGraphics.Polygon.Vertices[i].X, polylineGraphics.Polygon.Vertices[i].Y, imageUID, frameNumber, i));
				//        }
				//        geoShape = multipointShape;
				//    }
				//}
			else if (roi is RectangularRoi)
			{
				RectangularRoi rectangularRoi = roi as RectangularRoi;
				Platform.CheckForNullReference(rectangularRoi, "rectangularRoi");

				aim_dotnet.Polyline polylineShape = new aim_dotnet.Polyline();
				polylineShape.SpatialCoordinateCollection = new List<aim_dotnet.ISpatialCoordinate>();

				// Top Left
				polylineShape.SpatialCoordinateCollection.Add(
					Create2DSpatialCoordinate(rectangularRoi.BoundingBox.Left, rectangularRoi.BoundingBox.Top, imageUID, frameNumber, 0));

				// Top Right
				polylineShape.SpatialCoordinateCollection.Add(
					Create2DSpatialCoordinate(rectangularRoi.BoundingBox.Right, rectangularRoi.BoundingBox.Top, imageUID, frameNumber, 1));

				// Buttom Right
				polylineShape.SpatialCoordinateCollection.Add(
					Create2DSpatialCoordinate(rectangularRoi.BoundingBox.Right, rectangularRoi.BoundingBox.Bottom, imageUID, frameNumber, 2));

				// Buttom Left
				polylineShape.SpatialCoordinateCollection.Add(
					Create2DSpatialCoordinate(rectangularRoi.BoundingBox.Left, rectangularRoi.BoundingBox.Bottom, imageUID, frameNumber, 3));

				geoShape = polylineShape;
			}
			else
				Console.WriteLine("AIMHelper.CreateGeoShape. Unhandled ROI type: " + roi.GetType().FullName);

			return geoShape;
		}

		private static aim_dotnet.TwoDimensionSpatialCoordinate Create2DSpatialCoordinate(float x, float y, string imageUID, int frameNumber, int coordinateIndex)
		{
			aim_dotnet.TwoDimensionSpatialCoordinate spatialCoord = new aim_dotnet.TwoDimensionSpatialCoordinate();
			spatialCoord.X = x;
			spatialCoord.Y = y;
			spatialCoord.ImageReferenceUID = imageUID;
			spatialCoord.ReferencedFrameNumber = frameNumber;
			spatialCoord.CoordinateIndex = coordinateIndex;

			return spatialCoord;
		}

		// Returns 'false' if the image already exists in the Image collection or 'true' when a new Image is added.
		private static bool AddDicomImageReference(aim_dotnet.ImageAnnotation imageAnnotation, IImageSopProvider image)
		{
			Platform.CheckForNullReference(imageAnnotation, "ImageAnnotation");

			if (imageAnnotation.ImageReferenceCollection == null)
				imageAnnotation.ImageReferenceCollection = new List<aim_dotnet.ImageReference>();
			List<aim_dotnet.ImageReference> imageReferences = imageAnnotation.ImageReferenceCollection;

			aim_dotnet.ImageStudy aimImageStudy = null;
			foreach (aim_dotnet.ImageReference imgRef in imageReferences)
			{
				aim_dotnet.DICOMImageReference dicomImgRef = imgRef as aim_dotnet.DICOMImageReference;
				if (dicomImgRef != null)
				{
					if (dicomImgRef.Study.InstanceUID == image.ImageSop.StudyInstanceUid &&
					    dicomImgRef.Study.Series.InstanceUID == image.ImageSop.SeriesInstanceUid)
						aimImageStudy = dicomImgRef.Study;
				}
			}

			// Create new Study/Series
			if (aimImageStudy == null)
			{
				aimImageStudy = CreateStudy(image);
				aimImageStudy.Series = CreateSeries(image);
				aimImageStudy.Series.ImageCollection = new List<aim_dotnet.Image>();
				aim_dotnet.DICOMImageReference dicomImgRef = new aim_dotnet.DICOMImageReference();
				dicomImgRef.Study = aimImageStudy;
				imageReferences.Add(dicomImgRef);
			}

			foreach (aim_dotnet.Image existingImage in aimImageStudy.Series.ImageCollection)
			{
				if (existingImage.SopInstanceUID == image.ImageSop.SopInstanceUid)
					return false; // already have this image
			}

			aim_dotnet.Image aimImage = CreateImage(image);
			aimImageStudy.Series.ImageCollection.Add(aimImage);

			return true;
		}

		private static aim_dotnet.ImageStudy CreateStudy(IImageSopProvider image)
		{
			Platform.CheckForNullReference(image, "Image");

			aim_dotnet.ImageStudy aimStudy = new aim_dotnet.ImageStudy();
			aimStudy.InstanceUID = image.ImageSop.StudyInstanceUid;
			DateTime studyDate;
			if (DateTimeParser.ParseDateAndTime(null, image.ImageSop.StudyDate, image.ImageSop.StudyTime, out studyDate))
				aimStudy.StartDate = studyDate;

			return aimStudy;
		}

		private static aim_dotnet.ImageSeries CreateSeries(IImageSopProvider image)
		{
			Platform.CheckForNullReference(image, "Image");

			aim_dotnet.ImageSeries aimSeries = new aim_dotnet.ImageSeries();
			aimSeries.InstanceUID = image.ImageSop.SeriesInstanceUid;

			return aimSeries;
		}

		private static aim_dotnet.Image CreateImage(IImageSopProvider image)
		{
			Platform.CheckForNullReference(image, "Image");

			aim_dotnet.Image aimImage = new aim_dotnet.Image();

			aimImage.SopClassUID = image.ImageSop.SopClassUid;
			aimImage.SopInstanceUID = image.ImageSop.SopInstanceUid;

			// TODO - get Image View and Image View Modifiers
			//string sValue;
			//bool bExist;
			//image.ImageSop.GetTag(DicomTags.ViewCodeSequence, out sValue, out bExist);

			return aimImage;
		}

		private static List<aim_dotnet.Calculation> CreateCalculations(RoiGraphic roiGraphic, int referencedGeoShapeId)
		{
			List<aim_dotnet.Calculation> calculations = new List<aim_dotnet.Calculation>();
			List<aim_dotnet.ReferencedGeometricShape> referencedGeometricShapes = null;
			if (referencedGeoShapeId >= 0)
			{
				referencedGeometricShapes = new List<aim_dotnet.ReferencedGeometricShape>();
				referencedGeometricShapes.Add(new aim_dotnet.ReferencedGeometricShape {ReferencedShapeIdentifier = referencedGeoShapeId});
			}

			Roi roi = roiGraphic.Roi;
			foreach (IRoiAnalyzer analyzer in roiGraphic.Callout.RoiAnalyzers)
			{
				if (analyzer.SupportsRoi(roi))
				{
					aim_dotnet.Calculation calculation = new aim_dotnet.Calculation();
					calculation.UID = DicomUid.GenerateUid().UID;
					calculation.CalculationResultCollection = new List<aim_dotnet.CalculationResult>();
					if (analyzer is RoiLengthAnalyzer)
					{
						calculation.CodeValue = CodeList.CalculationCodeForLength.CodeValue;
						calculation.CodeMeaning = CodeList.CalculationCodeForLength.CodeMeaning;
						calculation.CodingSchemeDesignator = CodeList.CalculationCodeForLength.CodingSchemeDesignator;
						calculation.CodingSchemeVersion = CodeList.CalculationCodeForLength.CodingSchemeVersion;
						calculation.Description = "Length";
						calculation.ReferencedGeometricShapeCollection = referencedGeometricShapes;
						IRoiLengthProvider roiLengthProvider = (IRoiLengthProvider) roi;

						Units oldUnits = roiLengthProvider.Units;
						roiLengthProvider.Units = roiLengthProvider.IsCalibrated ? Units.Millimeters : Units.Pixels;
						calculation.CalculationResultCollection.Add(CreateScalarCalculationResult(roiLengthProvider.Length, UnitsToName(roiLengthProvider.Units), "Value"));
						roiLengthProvider.Units = oldUnits;

						calculations.Add(calculation);
					}
					else if (analyzer is RoiAreaAnalyzer)
					{
						calculation.CodeValue = CodeList.CalculationCodeForArea.CodeValue;
						calculation.CodeMeaning = CodeList.CalculationCodeForArea.CodeMeaning;
						calculation.CodingSchemeDesignator = CodeList.CalculationCodeForArea.CodingSchemeDesignator;
						calculation.CodingSchemeVersion = CodeList.CalculationCodeForArea.CodingSchemeVersion;
						calculation.Description = "Area";
						calculation.ReferencedGeometricShapeCollection = referencedGeometricShapes;
						IRoiAreaProvider roiAreaProvider = (IRoiAreaProvider) roi;

						Units oldUnits = roiAreaProvider.Units;
						roiAreaProvider.Units = roiAreaProvider.IsCalibrated ? Units.Millimeters : Units.Pixels;
						calculation.CalculationResultCollection.Add(CreateScalarCalculationResult(roiAreaProvider.Area, UnitsToName(roiAreaProvider.Units), "Value"));
						roiAreaProvider.Units = oldUnits;

						calculations.Add(calculation);
					}
					else if (analyzer is RoiStatisticsAnalyzer)
					{
						if (roi.PixelData is GrayscalePixelData && IsBoundingBoxInImage(roi.BoundingBox, roi.ImageColumns, roi.ImageRows))
						{
							IRoiStatisticsProvider statisticsProvider = (IRoiStatisticsProvider) roi;

							double mean = statisticsProvider.Mean;
							double stdDev = statisticsProvider.StandardDeviation;

							aim_dotnet.Calculation calcStdDev = new aim_dotnet.Calculation(calculation);

							calculation.CodeValue = CodeList.CalculationCodeForMean.CodeValue;
							calculation.CodeMeaning = CodeList.CalculationCodeForMean.CodeMeaning;
							calculation.CodingSchemeDesignator = CodeList.CalculationCodeForMean.CodingSchemeDesignator;
							calculation.CodingSchemeVersion = CodeList.CalculationCodeForMean.CodingSchemeVersion;
							calculation.Description = "Mean";
							calculation.ReferencedGeometricShapeCollection = referencedGeometricShapes;
							calculation.CalculationResultCollection.Add(CreateScalarCalculationResult(mean, roi.Modality == "CT" ? "HU" : "1", "Value"));

							calcStdDev.CodeValue = CodeList.CalculationCodeForStandardDeviation.CodeValue;
							calcStdDev.CodeMeaning = CodeList.CalculationCodeForStandardDeviation.CodeMeaning;
							calcStdDev.CodingSchemeDesignator = CodeList.CalculationCodeForStandardDeviation.CodingSchemeDesignator;
							calcStdDev.CodingSchemeVersion = CodeList.CalculationCodeForStandardDeviation.CodingSchemeVersion;
							calcStdDev.Description = "Standard Deviation";
							calcStdDev.ReferencedGeometricShapeCollection = referencedGeometricShapes;
							calcStdDev.CalculationResultCollection.Add(CreateScalarCalculationResult(stdDev, roi.Modality == "CT" ? "HU" : "1", "Value"));

							calculations.Add(calculation);
							calculations.Add(calcStdDev);
						}
					}
					else if (analyzer is ProtractorAngleCalculator)
					{
						ProtractorRoiInfo protractorRoiInfo = roi as ProtractorRoiInfo;
						if (protractorRoiInfo != null && protractorRoiInfo.Points.Count >= 3)
						{
							List<PointF> normalizedPoints = NormalizePoints(protractorRoiInfo);
							double angle = Math.Abs(Vector.SubtendedAngle(normalizedPoints[0], normalizedPoints[1], normalizedPoints[2]));

							calculation.CodeValue = CodeList.CalculationCodeForAngle.CodeValue;
							calculation.CodeMeaning = CodeList.CalculationCodeForAngle.CodeMeaning;
							calculation.CodingSchemeDesignator = CodeList.CalculationCodeForAngle.CodingSchemeDesignator;
							calculation.CodingSchemeVersion = CodeList.CalculationCodeForAngle.CodingSchemeVersion;
							calculation.Description = "Angle";
							calculation.ReferencedGeometricShapeCollection = referencedGeometricShapes;
							//       calculation.CalculationResultCollection.Add(CreateScalarCalculationResult(angle, "rad", "Value"));
							calculation.CalculationResultCollection.Add(CreateScalarCalculationResult(angle, "deg", "Value"));

							calculations.Add(calculation);
						}
					}
				}
			}

			return calculations;
		}

		private static string UnitsToName(Units units)
		{
			switch (units)
			{
				case Units.Pixels:
					return "pixel";
				case Units.Centimeters:
					return "cm";
				case Units.Millimeters:
					return "mm";
				default:
					System.Diagnostics.Debug.Assert(false, "Unexpected calculation units");
					break;
			}
			return string.Empty;
		}

		private static aim_dotnet.CalculationResult CreateScalarCalculationResult(double value, string unit, string label)
		{
			aim_dotnet.Dimension dimension = new aim_dotnet.Dimension();
			dimension.Index = 0;
			dimension.Size = 1;
			dimension.Label = label;

			aim_dotnet.Coordinate coordinate = new aim_dotnet.Coordinate();
			coordinate.DimensionIndex = 0;
			coordinate.Position = 0;

			aim_dotnet.CalculationData calculationData = new aim_dotnet.CalculationData();
			calculationData.Value = value;
			calculationData.CoordinateCollection = new List<aim_dotnet.Coordinate>();
			calculationData.CoordinateCollection.Add(coordinate);

			aim_dotnet.CalculationResult result = new aim_dotnet.CalculationResult();
			result.TypeOfCalculationResult = aim_dotnet.CalculationResultIdentifier.Scalar;
			result.UnitOfMeasure = unit;
			result.DimensionCollection = new List<aim_dotnet.Dimension>();
			result.DimensionCollection.Add(dimension);
			result.NumberOfDimensions = result.DimensionCollection.Count;
			result.CalculationDataCollection = new List<aim_dotnet.CalculationData>();
			result.CalculationDataCollection.Add(calculationData);

			return result;
		}

		private static bool IsBoundingBoxInImage(RectangleF boundingBox, float imageColumns, float imageRows)
		{
			boundingBox = RectangleUtilities.ConvertToPositiveRectangle(boundingBox);

			if (boundingBox.Width == 0 || boundingBox.Height == 0)
				return false;

			if (boundingBox.Left < 0 ||
				boundingBox.Top < 0 ||
				boundingBox.Right > (imageColumns - 1) ||
				boundingBox.Bottom > (imageRows - 1))
				return false;

			return true;
		}

		private static List<PointF> NormalizePoints(ProtractorRoiInfo roiInfo)
		{
			float aspectRatio = 1F;

			if (roiInfo.PixelAspectRatio.IsNull)
			{
				if (!roiInfo.NormalizedPixelSpacing.IsNull)
					aspectRatio = (float)roiInfo.NormalizedPixelSpacing.AspectRatio;
			}
			else
			{
				aspectRatio = roiInfo.PixelAspectRatio.Value;
			}

			List<PointF> normalized = new List<PointF>();
			foreach (PointF point in roiInfo.Points)
				normalized.Add(new PointF(point.X, point.Y * aspectRatio));

			return normalized;
		}

		// ====================================================================
		//
		//  Read and display annotation back
		//
		// ====================================================================

		public static bool ReadGraphicsFromAnnotation(aim_dotnet.Annotation annotation, IPresentationImage presentationImage)
		{
			bool hasNewRoiGraphic = false;

			IOverlayGraphicsProvider graphicsProvider = presentationImage as IOverlayGraphicsProvider;
			//IApplicationGraphicsProvider graphicsProvider = presentationImage as IApplicationGraphicsProvider;
			IImageSopProvider currentImageSOP = presentationImage as IImageSopProvider;
			if (graphicsProvider == null || currentImageSOP == null)
				return false;

			if (annotation is aim_dotnet.ImageAnnotation)
			{
				aim_dotnet.ImageAnnotation imgAnnotation = annotation as aim_dotnet.ImageAnnotation;
				if (imgAnnotation.GeometricShapeCollection != null)
				{
					foreach (aim_dotnet.IGeometricShape geoShape in imgAnnotation.GeometricShapeCollection)
					{
						// Check if the image is the one on which the annotation was originally drawn
						if (GetImageSOPInstanceUID(geoShape) != currentImageSOP.ImageSop.SopInstanceUid)
							break;

						// Prevent from adding the same annotation again
						bool isAlreadyDisplayed = false;
						foreach (IGraphic graphic in graphicsProvider.OverlayGraphics)
						{
							AimGraphic aimGraphic = graphic as AimGraphic;
							isAlreadyDisplayed = aimGraphic != null && aimGraphic.AimAnnotation.UniqueIdentifier == imgAnnotation.UniqueIdentifier &&
							                     aimGraphic.ShapeIdentifier == geoShape.ShapeIdetifier;
							if (isAlreadyDisplayed)
								break;
						}
						if (isAlreadyDisplayed)
							continue;

						//hasNewRoiGraphic = AddRoiGraphic(geoShape, graphicsProvider) || hasNewRoiGraphic;
						RoiGraphic roiGraphic = GeoShapeToRoiGraphic(geoShape);
						if (roiGraphic != null)
						{
							AimGraphic aimGraphic = new AimGraphic(roiGraphic, imgAnnotation, geoShape.ShapeIdetifier);
							if (!String.IsNullOrEmpty(imgAnnotation.Name))
								roiGraphic.Name = imgAnnotation.Name;
							aimGraphic.Color = Color.SlateBlue;
							graphicsProvider.OverlayGraphics.Add(aimGraphic);
						}

						hasNewRoiGraphic |= roiGraphic == null;
					}
				}
			}

			return hasNewRoiGraphic;
		}

		// Returns SOP Instance UID of the image the given shape belongs to
		// 2D images are supported only
		private static string GetImageSOPInstanceUID(aim_dotnet.IGeometricShape geoShape)
		{
			if (geoShape.SpatialCoordinateCollection != null && geoShape.SpatialCoordinateCollection.Count > 0 &&
			    geoShape.SpatialCoordinateCollection[0].CoordinateType == aim_dotnet.SpatialCoordinateType.SPATIAL_COORD_2D)
			{
				aim_dotnet.TwoDimensionSpatialCoordinate twoDSpatialCoord = geoShape.SpatialCoordinateCollection[0] as aim_dotnet.TwoDimensionSpatialCoordinate;
				if (twoDSpatialCoord != null)
					return twoDSpatialCoord.ImageReferenceUID;
			}

			return string.Empty;
		}

		private static RoiGraphic GeoShapeToRoiGraphic(aim_dotnet.IGeometricShape geoShape)
		{
			RoiGraphic roiGraphic = null;

			if (geoShape.SpatialCoordinateCollection == null || geoShape.SpatialCoordinateCollection.Count == 0)
				return null;

			if (geoShape is aim_dotnet.Circle)
			{
				// Ellipse
				aim_dotnet.Circle shapeCircle = geoShape as aim_dotnet.Circle;
				PointF centerPt = AsPointF(shapeCircle.Center);
				PointF radiusPt = AsPointF(shapeCircle.RadiusPoint);
				double radiusLength = Vector.Distance(centerPt, radiusPt);
				roiGraphic = RoiGraphic.CreateEllipse();
				BoundableGraphic boundableGraphic = roiGraphic.Subject as BoundableGraphic;
				if (boundableGraphic != null)
				{
					roiGraphic.Suspend(); // prevent callout location calculation untill all points are set
					boundableGraphic.TopLeft = new PointF((float) (centerPt.X - radiusLength), (float) (centerPt.Y - radiusLength));
					boundableGraphic.BottomRight = new PointF((float) (centerPt.X + radiusLength), (float) (centerPt.Y + radiusLength));
					roiGraphic.Resume(true); // Force callout location calculation
				}
			}
			else if (geoShape is aim_dotnet.Ellipse)
			{
				// Ellipse
				aim_dotnet.Ellipse shapeEllipse = geoShape as aim_dotnet.Ellipse;
				PointF firstMajorAxisPt = AsPointF(shapeEllipse.EllipseCollection[0]);
				PointF secondMajorAxisPt = AsPointF(shapeEllipse.EllipseCollection[1]);
				PointF firstMinorAxisPt = AsPointF(shapeEllipse.EllipseCollection[2]);
				PointF secondMinorAxisPt = AsPointF(shapeEllipse.EllipseCollection[3]);

				roiGraphic = RoiGraphic.CreateEllipse();
				BoundableGraphic boundableGraphic = roiGraphic.Subject as BoundableGraphic;
				if (boundableGraphic != null)
				{
					roiGraphic.Suspend(); // prevent callout location calculation untill all points are set
					boundableGraphic.TopLeft = new PointF(firstMajorAxisPt.X, firstMinorAxisPt.Y);
					boundableGraphic.BottomRight = new PointF(secondMajorAxisPt.X, secondMinorAxisPt.Y);
					roiGraphic.Resume(true); // Force callout location calculation
				}
			}
			else if (geoShape is aim_dotnet.Point)
			{
				throw new NotImplementedException("Reading Point shape is not implemented");
			}
			else if (geoShape is aim_dotnet.MultiPoint)
			{
				// How this case works:
				// If we have 2 points, it's a line
				// If we have 3 points, it's a protractor
				// All others - unknown unclosed object (not supported)

				aim_dotnet.MultiPoint shapeMultiPoint = geoShape as aim_dotnet.MultiPoint;
				switch (shapeMultiPoint.SpatialCoordinateCollection.Count)
				{
					case 2:
						{
							// Line
							VerticesControlGraphic interactiveGraphic = new VerticesControlGraphic(new MoveControlGraphic(new PolylineGraphic()));
							PointF firstPt = AsPointF(shapeMultiPoint.SpatialCoordinateCollection[0]);
							PointF secondPt = AsPointF(shapeMultiPoint.SpatialCoordinateCollection[1]);
							interactiveGraphic.Subject.Points.Add(firstPt);
							interactiveGraphic.Subject.Points.Add(secondPt);

							roiGraphic = CreateRoiGraphic(interactiveGraphic, null);
							roiGraphic.Resume(true); // Force callout location calculation
						}
						break;
					case 3:
						{
							// Protractor
							VerticesControlGraphic interactiveGraphic = new VerticesControlGraphic(new MoveControlGraphic(new ProtractorGraphic()));
							PointF firstPt = AsPointF(shapeMultiPoint.SpatialCoordinateCollection[0]);
							PointF secondPt = AsPointF(shapeMultiPoint.SpatialCoordinateCollection[1]);
							PointF thirdPt = AsPointF(shapeMultiPoint.SpatialCoordinateCollection[2]);
							interactiveGraphic.Subject.Points.Add(firstPt);
							interactiveGraphic.Subject.Points.Add(secondPt);
							interactiveGraphic.Subject.Points.Add(thirdPt);

							roiGraphic = CreateRoiGraphic(interactiveGraphic, new ProtractorRoiCalloutLocationStrategy());
							roiGraphic.Resume(true); // Force callout location calculation
						}
						break;
					default:
						throw new NotImplementedException("Reading non-linear or non-triangular MultiPoint shape is not implemented");
				}
			}
			else if (geoShape is aim_dotnet.Polyline)
			{
				aim_dotnet.Polyline shapePolyline = geoShape as aim_dotnet.Polyline;

				bool isRectangle = false; // true - if we have CC's rectangle coordinates
				// Check if this is a non-rotated rectangle
				if (shapePolyline.SpatialCoordinateCollection.Count == 4)
				{
					PointF twoDPoint1 = AsPointF(shapePolyline.SpatialCoordinateCollection[0]);
					PointF twoDPoint2 = AsPointF(shapePolyline.SpatialCoordinateCollection[1]);
					PointF twoDPoint3 = AsPointF(shapePolyline.SpatialCoordinateCollection[2]);
					PointF twoDPoint4 = AsPointF(shapePolyline.SpatialCoordinateCollection[3]);
					// If it's a rectangle with sides parallel to the axes
					if ((twoDPoint1.X == twoDPoint2.X && twoDPoint2.Y == twoDPoint3.Y && twoDPoint3.X == twoDPoint4.X && twoDPoint4.Y == twoDPoint1.Y) ||
					    (twoDPoint1.Y == twoDPoint2.Y && twoDPoint2.X == twoDPoint3.X && twoDPoint3.Y == twoDPoint4.Y && twoDPoint4.X == twoDPoint1.X))
					{
						isRectangle = true;

						roiGraphic = RoiGraphic.CreateRectangle();
						BoundableGraphic boundableGraphic = roiGraphic.Subject as BoundableGraphic;
						if (boundableGraphic != null)
						{
							roiGraphic.Suspend(); // prevent callout location calculation untill all points are set
							// Assume that the points are in order and start at the top left corner.
							boundableGraphic.TopLeft = twoDPoint1;
							boundableGraphic.BottomRight = twoDPoint3;

							roiGraphic.Resume(true); // Force callout location calculation
						}
					}
				}
				// It's a CC's polygon if it's not a rectangle
				if (!isRectangle)
				{
					roiGraphic = RoiGraphic.CreatePolygon();
					PolylineGraphic polylineGraphic = roiGraphic.Subject as PolylineGraphic;
					if (polylineGraphic != null)
					{
						roiGraphic.Suspend();
						for (int i = 0; i < shapePolyline.SpatialCoordinateCollection.Count; i++)
						{
							PointF twoDPoint = AsPointF(shapePolyline.SpatialCoordinateCollection[i]);
							polylineGraphic.Points.Add(twoDPoint);
						}
						// We deal with closed polygons only
						if (polylineGraphic.Points.Count > 0)
							polylineGraphic.Points.Add(polylineGraphic.Points[0]);
						roiGraphic.Resume(true); // Force callout location calculation
					}
				}
			}
			else
				throw new Exception("Unknown shape type encountered: " + geoShape.GetType().FullName);

			return roiGraphic;
		}


		// Helper method
		private static RoiGraphic CreateRoiGraphic(IGraphic interactiveGraphic, IAnnotationCalloutLocationStrategy strategy)
		{
			RoiGraphic roiGraphic;
			if (strategy == null)
				roiGraphic = new RoiGraphic(interactiveGraphic);
			else
				roiGraphic = new RoiGraphic(interactiveGraphic, strategy);

			roiGraphic.Name = "ROI"; // string.Empty;
			roiGraphic.State = roiGraphic.CreateInactiveState();

			return roiGraphic;
		}

		private static PointF AsPointF(aim_dotnet.ISpatialCoordinate spatialCoord)
		{
			Platform.CheckTrue(spatialCoord != null, "Spatial Coordinate Exists");
			Platform.CheckTrue(spatialCoord.CoordinateType == aim_dotnet.SpatialCoordinateType.SPATIAL_COORD_2D, "SpatialCoordinate is 2D");

			aim_dotnet.TwoDimensionSpatialCoordinate twoDCoord = spatialCoord as aim_dotnet.TwoDimensionSpatialCoordinate;
			if (twoDCoord == null)
				throw new ArgumentException("Spatial coordinate is not 2D");

			return new PointF((float)twoDCoord.X, (float)twoDCoord.Y);
		}

		// ====================================================================
		//
		//  Annotation creation and manipulation helpers
		//
		// ====================================================================

		public static bool IsImageMarkupPresent(IPresentationImage image)
		{
			IOverlayGraphicsProvider currentOverlayGraphics = image as IOverlayGraphicsProvider;
			if (currentOverlayGraphics != null)
			{
				foreach (IGraphic graphic in currentOverlayGraphics.OverlayGraphics)
				{
					RoiGraphic currentRoiGraphic = graphic as RoiGraphic;
					if (currentRoiGraphic != null)
						return true;
				}
			}
			return false;
		}

		public static bool WriteXmlAnnotationToFile(aim_dotnet.Annotation annotation, string filePathName)
		{
			aim_dotnet.XmlModel xmlModel = new aim_dotnet.XmlModel();
			try
			{
				xmlModel.WriteAnnotationToFile(annotation, filePathName);
				return true;
			}
			catch (Exception ex)
			{
				Platform.Log(LogLevel.Error, ex, "Failed to save annotation to file \"{0}\"", filePathName);
			}
			finally
			{
				xmlModel = null;
			}

			return false;
		}

		// Writes all annotations to a given folder. Returns number of annotations written.
		public static int WriteXmlAnnotationsToFolder(List<aim_dotnet.Annotation> annotations, string folderPath)
		{
			int cnt = 0;
			aim_dotnet.XmlModel xmlModel = new aim_dotnet.XmlModel();
			foreach (aim_dotnet.Annotation annotation in annotations)
			{
				string tempFileNameXml = null;
				string fileName = string.IsNullOrEmpty(annotation.UniqueIdentifier) ? System.IO.Path.GetRandomFileName() : annotation.UniqueIdentifier;
				try
				{
					tempFileNameXml = System.IO.Path.ChangeExtension(System.IO.Path.Combine(folderPath, fileName), "xml");
					xmlModel.WriteAnnotationToFile(annotation, tempFileNameXml);
					cnt++;
				}
				catch (Exception ex)
				{
					Platform.Log(LogLevel.Error, ex, "Failed to save annotation to file \"{0}\"", tempFileNameXml);
					try
					{
						if (!string.IsNullOrEmpty(tempFileNameXml))
							System.IO.File.Delete(tempFileNameXml);
					}
					catch (Exception)
					{
					}
				}
			}
			xmlModel = null;

			return cnt;
		}
	}
}
