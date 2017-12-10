using System;
using System.ComponentModel;
using System.Diagnostics;
using SignUp.CustomControls;
using SignUp.iOS.CustomRenderers;
using SignUp.PCL.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageCircle), typeof(ImageCircleRenderer))]
namespace SignUp.iOS.CustomRenderers
{
    /// <summary>
    /// Image circle renderer.
    /// </summary>
    public class ImageCircleRenderer : ImageRenderer
    {
        /// <summary>
        /// Creates the circle.
        /// </summary>
        private void CreateCircle()
        {
            try
            {
                double min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (float)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.Layer.BorderColor = Color.FromHex("#CC4422").ToCGColor(); // Color.White.ToCGColor();
                Control.Layer.BorderWidth = 2; // 3;
                Control.ClipsToBounds = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }

        /// <summary>
        /// Ons the element changed.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            CreateCircle();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName
               )
            {
                CreateCircle();
            }
        }
    }
}
